using AppData.Models;

namespace BugTrack_UI.Store.Actions;

public record LoadBugSuccessAction(IReadOnlyList<Bug> Bugs);
