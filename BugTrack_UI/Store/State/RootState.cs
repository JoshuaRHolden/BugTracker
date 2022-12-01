namespace BugTrack_UI.Store.State
{
    public abstract record RootState
    {
        public RootState(bool isLoading, string? currentErrorMessage) =>
            (IsLoading, CurrentErrorMessage) = (isLoading, currentErrorMessage);

        public bool IsLoading { get; }

        public string? CurrentErrorMessage { get; }

        public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);
    }
}
