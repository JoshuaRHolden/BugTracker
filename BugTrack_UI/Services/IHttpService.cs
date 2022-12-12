using AppData.Models;
using System.Threading.Tasks;

namespace BugTrack_UI.Services
{
    public interface IHttpService
    {
        Task<Bug>  GetBugByID(int bugID);
        Task<HttpResponseMessage> EditBug(Bug bug);
        Task<IEnumerable<Bug>> GetBugList();
        Task<IEnumerable<Bug>> SearchBugList(string SearchTerm);
        Task<HttpResponseMessage> SaveBug(Bug bug);
    }
}
