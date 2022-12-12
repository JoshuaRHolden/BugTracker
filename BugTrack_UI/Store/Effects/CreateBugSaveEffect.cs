using AppData.Models;
using BugTrack_UI.Services;
using BugTrack_UI.Store.Actions;
using Fluxor;
using System.Text;

namespace BugTrack_UI.Store.Effects
{
    public class CreateBugSaveEffect : Effect<CreateBugSaveEffect.EffectCreateBug>
    {
        public record EffectCreateBug(Bug bug, string UserName, bool AssignedToMe, bool IncludeClosed);
        private readonly ILogger<CreateBugSaveEffect> _logger;
        private readonly IHttpService _httpService;

        public CreateBugSaveEffect(ILogger<CreateBugSaveEffect> logger, IHttpService httpService ) =>
            (_logger,_httpService) = (logger,httpService);

        public override async Task HandleAsync(EffectCreateBug action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading bug...");             
                action.bug.CreatedBy = action.UserName;
                var bugResponse = await _httpService.SaveBug(action.bug);
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