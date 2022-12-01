using AppData.Models;
using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{


    public class EditBugEffect : Effect<EditBugEffect.EffectEditBug>
    {
        public record EffectEditBug(int Id);
        private readonly ILogger<EditBugEffect> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;
        public EditBugEffect(ILogger<EditBugEffect> logger, IHttpClientFactory httpClient, IConfiguration config) =>
            (_logger, _httpClient, _config) = (logger, httpClient, config);

        public override async Task HandleAsync(EffectEditBug action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading bug...");
                var url = _config.GetValue<string>("APIDetails:APIURI");
                var client = _httpClient.CreateClient();
                //throw new Exception("bug/!!");
                var bugResponse = await client.GetFromJsonAsync<Bug>(url + $"/Bug/GetBugById?Id={action.Id}");

                _logger.LogInformation("bugs loaded successfully!");
                dispatcher.Dispatch(new ActionEditBug(bugResponse));
            }
            catch (Exception e)
            {
                _logger.LogError($"bugs loading bugs, reason: {e.Message}");
                dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
            }

        }


    }
}
