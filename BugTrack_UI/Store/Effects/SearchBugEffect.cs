using AppData.Models;
using BugTrack_UI.Store.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace BugTrack_UI.Store.Effects
{
    public class SearchBugEffect : Effect<SearchBugEffect.EffectSearchBug>
    {
        public record EffectSearchBug(String SearchTerm, bool MyAssigned, bool IncludeCancelled);
        private readonly ILogger<SearchBugEffect> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;
        private AuthenticationStateProvider _authentication;

        public SearchBugEffect(ILogger<SearchBugEffect> logger, IHttpClientFactory httpClient, IConfiguration config, AuthenticationStateProvider authentication) =>
            (_logger, _httpClient, _config, _authentication) = (logger, httpClient, config, authentication);

        public override async Task HandleAsync(EffectSearchBug action, IDispatcher dispatcher)
        {
            try
            {
                dispatcher.Dispatch(new StartSearchAction());
                _logger.LogInformation("Loading bugs...");
                var url = _config.GetValue<string>("APIDetails:APIURI");
                var client = _httpClient.CreateClient();
                //throw new Exception("bug/!!");
                var bugResponse = await client.GetFromJsonAsync<List<Bug>?>(url + $"/bug/SearchBugs?SearchQuery={action.SearchTerm}");
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