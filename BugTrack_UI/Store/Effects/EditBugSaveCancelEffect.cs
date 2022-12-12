using BugTrack_UI.Store.Actions;
using Fluxor;

namespace BugTrack_UI.Store.Effects
{
    public class EditBugSaveCancelEffect : Effect<EditBugSaveCancelEffect.EffectSaveCancelBug>
    {
        public record EffectSaveCancelBug();
        private readonly ILogger<EditBugSaveCancelEffect> _logger;

        public EditBugSaveCancelEffect(ILogger<EditBugSaveCancelEffect> logger) =>
            (_logger) = (logger);

        public override async Task HandleAsync(EffectSaveCancelBug action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new LoadBugsAction());
        }
    }
}