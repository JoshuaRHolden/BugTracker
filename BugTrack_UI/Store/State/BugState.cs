using AppData.Models;

namespace BugTrack_UI.Store.State;

public record BugState(bool IsLoading, string? CurrentErrorMessage, IEnumerable<Bug>? CurrentBugs, Bug? CurrentBug, string SearchText, string Messages, Bug? CreateBug, bool AssignedToMe, bool IncludeClosed)
{
    public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);
}