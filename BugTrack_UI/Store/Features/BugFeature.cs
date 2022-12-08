using BugTrack_UI.Store.State;
using Fluxor;

namespace BugTrack_UI.Store.Features
{
    public class BugFeature : Feature<BugState>
    {
        public override string GetName() => "Bugs";

        protected override BugState GetInitialState() =>
            new(false, null, null, null, string.Empty, "State Initialised", null, false, false);
    }
}