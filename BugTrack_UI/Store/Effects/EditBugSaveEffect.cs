using AppData.Models;
using BugTrack_UI.Services;
using BugTrack_UI.Store.Actions;
using Fluxor;
using System.Text;

namespace BugTrack_UI.Store.Effects
{
    public class EditBugSaveEffect : Effect<EditBugSaveEffect.EffectSaveBug>
    {
        public record EffectSaveBug(Bug bug, string UserName, bool AssignedToMe, bool IncludeClosed);
        private readonly ILogger<EditBugSaveEffect> _logger;
        private readonly IHttpService _httpService;

        public EditBugSaveEffect(ILogger<EditBugSaveEffect> logger, IHttpService httpService) =>
            (_logger, _httpService) = (logger, httpService);

        public override async Task HandleAsync(EffectSaveBug action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading bug...");
                var bugResponse = await _httpService.EditBug(action.bug);
                _logger.LogInformation("bugs saved successfully!");
                dispatcher.Dispatch(new LoadBugEffect.EffectBugAction(action.AssignedToMe, action.IncludeClosed));
                dispatcher.Dispatch(new ActionSaveBug());
            }
            catch (Exception e)
            {
                _logger.LogError($"bugs saving bugs, reason: {e.Message}");
                dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
            }
        }
    }
}