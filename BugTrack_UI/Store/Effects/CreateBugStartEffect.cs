using AppData.Models;
using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{
    public class CreateBugStartEffect : Effect<CreateBugStartEffect.EffectStartCreateBug>
    {
        public record EffectStartCreateBug();
        private readonly ILogger<CreateBugStartEffect> _logger;        

        public CreateBugStartEffect(ILogger<CreateBugStartEffect> logger) =>
            (_logger ) = (logger);

        public override async Task HandleAsync(EffectStartCreateBug action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("opening create modal");
                dispatcher.Dispatch(new ActionCreateBug(new Bug()));
            }
            catch (Exception e)
            {
                _logger.LogError($"bugs loading bugs, reason: {e.Message}");
                dispatcher.Dispatch(new LoadBugFailureAction(e.Message));
            }
        }
    }
}