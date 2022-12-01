namespace BugTrack_UI.Store.Actions;

public record LoadBugFailureAction(string ErrorMessage) : FailureAction(ErrorMessage);

