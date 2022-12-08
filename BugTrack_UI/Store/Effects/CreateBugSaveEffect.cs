using AppData.Models;
using BugTrack_UI.Store.Actions;
using Fluxor;
using System.Text;

namespace BugTrack_UI.Store.Effects
{
    public class CreateBugSaveEffect : Effect<CreateBugSaveEffect.EffectCreateBug>
    {
        public record EffectCreateBug(Bug bug, string UserName, bool AssignedToMe, bool IncludeClosed);
        private readonly ILogger<CreateBugSaveEffect> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;

        public CreateBugSaveEffect(ILogger<CreateBugSaveEffect> logger, IHttpClientFactory httpClient, IConfiguration config) =>
            (_logger, _httpClient, _config) = (logger, httpClient, config);

        public override async Task HandleAsync(EffectCreateBug action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading bug...");
                var url = _config.GetValue<string>("APIDetails:APIURI");
                var client = _httpClient.CreateClient();
                action.bug.CreatedBy = action.UserName;
                string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(action.bug);
                HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
                var bugResponse = await client.PostAsJsonAsync<Bug>(url + $"/Bug/CreateBug", action.bug);
                string responsebody = bugResponse.Content.ReadAsStringAsync().Result;
                _logger.LogInformation("bugs saved successfully!");

                dispatcher.Dispatch(new ActionCreateSaveBug());
                dispatcher.Dispatch(new LoadBugEffect.EffectBugAction(action.AssignedToMe, action.IncludeClosed));
            }
            catch (Exception e)
            {
                _logger.LogError($"bugs saving bugs, reason: {e.Message}");
                dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
            }
        }
    }
}