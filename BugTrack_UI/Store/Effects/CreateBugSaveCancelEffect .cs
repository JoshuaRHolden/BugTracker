using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{
    public class CreateBugSaveCancelEffect : Effect<CreateBugSaveCancelEffect.EffectCreateCancelBug>
    {
        public record EffectCreateCancelBug();
        private readonly ILogger<CreateBugSaveCancelEffect> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;

        public CreateBugSaveCancelEffect(ILogger<CreateBugSaveCancelEffect> logger, IHttpClientFactory httpClient, IConfiguration config) =>
            (_logger, _httpClient, _config) = (logger, httpClient, config);

        public override async Task HandleAsync(EffectCreateCancelBug action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new CloseCreateModalAction());
            //dispatcher.Dispatch(new LoadBugsAction());
        }
    }
}