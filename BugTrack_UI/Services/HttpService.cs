using AppData.Models;
using System;
using System.Security.Policy;
using System.Text;

namespace BugTrack_UI.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;
        private string _url;
        public HttpService(IHttpClientFactory httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _url = _config.GetValue<string>("APIDetails:APIURI");
        }

        public async Task<HttpResponseMessage> EditBug(Bug bug)
        {
            var client = _httpClient.CreateClient();
            string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(bug);
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            return await client.PostAsJsonAsync<Bug>(_url + $"/Bug/UpdateBug?Id={bug.Id}", bug);
        }

        public async Task<Bug> GetBugByID(int bugID)
        {            
            var client = _httpClient.CreateClient();            
            return await client.GetFromJsonAsync<Bug>(_url + $"/Bug/GetBugById?Id={bugID}");
        }

        public async Task<IEnumerable<Bug>> GetBugList()
        {
            var client = _httpClient.CreateClient();
            return await client.GetFromJsonAsync<IEnumerable<Bug>>(_url + @"/bug/getallbugs");
        }

        public async Task<HttpResponseMessage> SaveBug(Bug bug)
        {
            var client = _httpClient.CreateClient();
            string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(bug);
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
             return await client.PostAsJsonAsync<Bug>(_url + $"/Bug/CreateBug", bug);
        }

        public async Task<IEnumerable<Bug>> SearchBugList(string searchTerm)
        {
            var client = _httpClient.CreateClient();
            return await client.GetFromJsonAsync<List<Bug>?>(_url + $"/bug/SearchBugs?SearchQuery={searchTerm}");
        }
    }
}
