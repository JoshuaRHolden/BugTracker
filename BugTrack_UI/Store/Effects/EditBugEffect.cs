using AppData.Models;
using BugTrack_UI.Services;
using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{
    public class EditBugEffect : Effect<EditBugEffect.EffectEditBug>
    {
        public record EffectEditBug(int Id);
        private readonly ILogger<EditBugEffect> _logger;
        private readonly IHttpService _httpService;

        public EditBugEffect(ILogger<EditBugEffect> logger, IHttpService httpService) =>
            (_logger,_httpService) = (logger,  httpService);

        public override async Task HandleAsync(EffectEditBug action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading bug...");                                
                var bugResponse = await _httpService.GetBugByID(action.Id);
                _logger.LogInformation("bugs loaded successfully!");
                dispatcher.Dispatch(new ActionEditBug(bugResponse!));
            }
            catch (Exception e)
            {
                _logger.LogError($"bugs loading bugs, reason: {e.Message}");
                dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
            }
        }
    }
}