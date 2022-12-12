using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{
    public class EditBugSaveCancelEffect : Effect<EditBugSaveCancelEffect.EffectSaveCancelBug>
    {
        public record EffectSaveCancelBug();
        private readonly ILogger<EditBugSaveCancelEffect> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;

        public EditBugSaveCancelEffect(ILogger<EditBugSaveCancelEffect> logger, IHttpClientFactory httpClient, IConfiguration config) =>
            (_logger, _httpClient, _config) = (logger, httpClient, config);

        public override async Task HandleAsync(EffectSaveCancelBug action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new LoadBugsAction());
        }
    }
}