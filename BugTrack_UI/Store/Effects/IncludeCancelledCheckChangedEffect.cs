using AppData.Models;
using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{


    public class IncludeCancelledCheckChangedEffect : Effect<IncludeCancelledCheckChangedEffect.EffectCheckChanged>
    {
        public record EffectCheckChanged(bool selected);
        private readonly ILogger<IncludeCancelledCheckChangedEffect> _logger;

        public IncludeCancelledCheckChangedEffect(ILogger<IncludeCancelledCheckChangedEffect> logger) =>
            (_logger) = (logger);

        public override async Task HandleAsync(EffectCheckChanged action, IDispatcher dispatcher)
        {
            _logger.LogInformation("Setting check...");
            dispatcher.Dispatch(new ActionIncludeCancelledCheckChanged(action.selected));
        }
    }
}