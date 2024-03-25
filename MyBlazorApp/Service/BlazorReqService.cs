using ViewModels;

namespace MyBlazorApp.Service
{
    public class BlazorReqService : IBlazorReqService//blazor implements iblazor
    {
        private readonly HttpClient _httpClient;//initializing private field _httpClient with provided HttpClient
        private List<LeaveRequestVM> leaverequests; //create empty list
        public BlazorReqService(HttpClient httpClient)//handle http req
        {
            _httpClient = httpClient;
            leaverequests = new List<LeaveRequestVM>();
        }
        public async Task<List<LeaveRequestVM>> GetAll()
        {
            //req http to get data
            var fetchedLeaverequests = await _httpClient.GetFromJsonAsync<List<LeaveRequestVM>>("https://localhost:44350/api/LeaveRequest");

            leaverequests.Clear();//clear older 
            leaverequests.AddRange(fetchedLeaverequests);//aftr clear populate fetchedproducts
            return leaverequests;//return list
        }
        
    }
}
