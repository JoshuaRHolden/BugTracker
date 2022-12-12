using AppData.Models;
using BugTrack_UI.Services;
using BugTrack_UI.Store.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace BugTrack_UI.Store.Effects
{
    public class LoadBugEffect : Effect<LoadBugEffect.EffectBugAction>
    {
        public record EffectBugAction(bool AssignedToMe, bool IncludeCancelled);
        private readonly ILogger<LoadBugEffect> _logger;
        private readonly IHttpService _httpService;
        private AuthenticationStateProvider _authentication;

        public LoadBugEffect(ILogger<LoadBugEffect> logger, IHttpService httpService, AuthenticationStateProvider authentication) =>
            (_logger, _httpService, _authentication) = (logger, httpService, authentication);

        public override async Task HandleAsync(EffectBugAction action, IDispatcher dispatcher)
        {
            try
            {
                dispatcher.Dispatch(new LoadBugsAction());
                _logger.LogInformation("Loading bugs...");
                var bugResponse = await _httpService.GetBugList();
                if (action.AssignedToMe)
                {
                    var authstate = await _authentication.GetAuthenticationStateAsync();
                    var user = authstate.User;
                    var name = user!.Identity!.Name;
                    bugResponse = bugResponse!.Where(x => x.AssignedTo == name);
                }
                if (!action.IncludeCancelled)
                {
                    bugResponse = bugResponse!.Where(x => x.BugStatus == BugStatus.Open);
                }
                _logger.LogInformation("bugs loaded successfully!");
                dispatcher.Dispatch(new LoadBugSuccessAction(bugResponse!.OrderBy(x => x.CreatedDate).ThenBy(d => d.PriorityStatus).ToArray()));
            }
            catch (Exception e)
            {
                _logger.LogError($"bugs loading bugs, reason: {e.Message}");
                dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
            }
        }
    }
}