using AppData.Models;
using BugTrack_UI.Store.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace BugTrack_UI.Store.Effects
{


    public class LoadBugEffect : Effect<LoadBugEffect.EffectBugAction>
    {
        public record EffectBugAction(bool AssignedToMe,bool IncludeCancelled);
        private readonly ILogger<LoadBugEffect> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;
        private AuthenticationStateProvider _authentication;
        public LoadBugEffect(ILogger<LoadBugEffect> logger, IHttpClientFactory httpClient, IConfiguration config, AuthenticationStateProvider authentication) =>
            (_logger, _httpClient, _config,_authentication) = (logger, httpClient, config,authentication);

        public override async Task HandleAsync(EffectBugAction action, IDispatcher dispatcher)
        {
            try
            {
                dispatcher.Dispatch(new LoadBugsAction());
                _logger.LogInformation("Loading bugs...");
                var url = _config.GetValue<string>("APIDetails:APIURI");
                var client = _httpClient.CreateClient();
                //throw new Exception("bug/!!");
                var bugResponse = await client.GetFromJsonAsync<IEnumerable<Bug>>(url + @"/bug/getallbugs");
                if (action.AssignedToMe)
                {
                    var authstate = await _authentication.GetAuthenticationStateAsync();
                    var user = authstate.User;
                    var name = user.Identity.Name;
                    bugResponse = bugResponse.Where(x => x.AssignedTo == name);

                }
                if (!action.IncludeCancelled)
                {
                    bugResponse = bugResponse.Where(x => x.BugStatus == BugStatus.Open);
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
