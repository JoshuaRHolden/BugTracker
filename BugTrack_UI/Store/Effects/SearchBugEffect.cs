using AppData.Models;
using BugTrack_UI.Services;
using BugTrack_UI.Store.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace BugTrack_UI.Store.Effects
{
    public class SearchBugEffect : Effect<SearchBugEffect.EffectSearchBug>
    {
        public record EffectSearchBug(String SearchTerm, bool MyAssigned, bool IncludeCancelled);
        private readonly ILogger<SearchBugEffect> _logger;
        private readonly IHttpService _httpService;
        private AuthenticationStateProvider _authentication;

        public SearchBugEffect(ILogger<SearchBugEffect> logger, IHttpService httpService, AuthenticationStateProvider authentication) =>
            (_logger, _httpService, _authentication) = (logger, httpService, authentication);

        public override async Task HandleAsync(EffectSearchBug action, IDispatcher dispatcher)
        {
            try
            {
                dispatcher.Dispatch(new StartSearchAction());
                _logger.LogInformation("Loading bugs...");
                var bugResponse = await _httpService.SearchBugList(action.SearchTerm);
                if (action.MyAssigned)
                {
                    var authstate = await _authentication.GetAuthenticationStateAsync();
                    var user = authstate.User;
                    var name = user!.Identity!.Name;
                    bugResponse = bugResponse!.Where(x => x.AssignedTo == name).ToList();
                }
                if (!action.IncludeCancelled)
                {
                    bugResponse = bugResponse!.Where(x => x.BugStatus == BugStatus.Open).ToList();
                }
                _logger.LogInformation("bugs loaded successfully!");
                dispatcher.Dispatch(new LoadBugSuccessAction(bugResponse!.OrderBy(x => x.PriorityStatus).ThenBy(x => x.CreatedDate).ToList()));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("The JSON value could not be converted to AppData.Models.Bug."))
                {
                    //this search reutned nothing.
                    dispatcher.Dispatch(new LoadBugSuccessAction(new List<Bug> { }));
                }
                else
                {
                    _logger.LogError($"Error loading bugs, reason: {e.Message}");
                    dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
                }
            }
        }
    }
}