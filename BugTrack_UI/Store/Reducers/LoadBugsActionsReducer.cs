using BugTrack_UI.Store.Actions;
using BugTrack_UI.Store.State;
using Fluxor;

namespace BugTrack_UI.Store.Reducers
{
    public static class LoadBugActionsReducer
    {
        [ReducerMethod]
        public static BugState ReduceCancelCreateBugsAction(BugState state,  CloseCreateModalAction action) =>
        state with { CreateBug = null };

        [ReducerMethod]
        public static BugState ReduceLoadBugsAction(BugState state, StartSearchAction action) =>
         state with { IsLoading = true, Messages = $"called reducer method StartSearchAction with state values IsLoading:{state.IsLoading}, search text:{state.SearchText}  " };

        [ReducerMethod]
        public static BugState ReduceLoadBugsAction(BugState state, LoadBugsAction action) =>
            state with { IsLoading = true, Messages = $"called reducer method ReduceLoadBugsAction with state values IsLoading:{state.IsLoading}, search text:{state.SearchText}  " };

        [ReducerMethod]
        public static BugState OnEditBug(BugState state, ActionEditBug action) =>
        state with { CurrentBug = action.Bug, Messages = $"called reducer method OnEditBug with state values IsLoading:{state.IsLoading}, search text:{state.SearchText}  " };

        [ReducerMethod]
        public static BugState OnSaveEditBug(BugState state, ActionSaveBug action) =>
        state with { CurrentBug = null, Messages = $"called reducer method OnSaveEditBug with state values IsLoading:{state.IsLoading}, search text:{state.SearchText}  " };

        [ReducerMethod]
        public static BugState OnOpenCreateBug(BugState state, ActionCreateBug action) =>
       state with { CreateBug = action.newBug, Messages = $"called reducer method OnOpenCreateBug with state values IsLoading:{state.IsLoading}, search text:{state.SearchText}  " };

        [ReducerMethod]
        public static BugState OnOpenCreateSaveBug(BugState state, ActionCreateSaveBug action) =>
       state with { CreateBug = null, Messages = $"called reducer method OnOpenCreateSaveBug with state values IsLoading:{state.IsLoading}, search text:{state.SearchText}  " };

        [ReducerMethod]
        public static BugState ReduceLoadBugsuccessAction(BugState state, LoadBugSuccessAction action) =>
            state with { CurrentBugs = action.Bugs, IsLoading = false, Messages = $"called reducer method ReduceLoadBugsuccessAction with state values IsLoading:{state.IsLoading}, search text:{state.SearchText} " };

        [ReducerMethod]
        public static BugState ReduceLoadBugsFailureAction(BugState state, LoadBugFailureAction action) =>
            state with { IsLoading = false, CurrentErrorMessage = action.ErrorMessage, Messages = $"called reducer method ReduceLoadBugsFailureAction with state values IsLoading:{state.IsLoading}, search text:{state.SearchText} " };

        [ReducerMethod]
        public static BugState ReduceMyAssignedCheckboxAction(BugState state, ActionCheckChanged action) =>
          state with { AssignedToMe = action.Selected, Messages = $"called reducer method ReduceLoadBugsFailureAction with state values IsLoading:{state.IsLoading}, search text:{state.SearchText} " };

        [ReducerMethod]
        public static BugState ReduceIncludeCancelledCheckboxAction(BugState state, ActionIncludeCancelledCheckChanged action) =>
          state with { IncludeClosed = action.Selected, Messages = $"called reducer method ReduceLoadBugsFailureAction with state values IsLoading:{state.IsLoading}, search text:{state.SearchText} " };
    }
}