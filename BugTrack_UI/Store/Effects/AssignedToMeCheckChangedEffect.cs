using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{
    public class AssignedToMeCheckChangedEffect : Effect<AssignedToMeCheckChangedEffect.EffectCheckChanged>
    {
        public record EffectCheckChanged(bool selected);
        private readonly ILogger<AssignedToMeCheckChangedEffect> _logger;

        public AssignedToMeCheckChangedEffect(ILogger<AssignedToMeCheckChangedEffect> logger) =>
            (_logger) = (logger);

        public override async Task HandleAsync(EffectCheckChanged action, IDispatcher dispatcher)
        {
            _logger.LogInformation("Setting check...");
            dispatcher.Dispatch(new ActionCheckChanged(action.selected));
        }
    }
}