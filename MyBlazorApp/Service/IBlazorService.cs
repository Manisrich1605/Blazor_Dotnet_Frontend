using ViewModels;

namespace MyBlazorApp.Service
{
    public interface IBlazorService
    {
        Task<List<LeaveTypeVM>> GetAll();
        Task<int> Add(LeaveTypeVM leavetype);
        Task Delete(int leavetypeId);
        Task<LeaveTypeVM> GetLeavetypeById(int leavetypeId);
        Task Update(LeaveTypeVM updatedLeavetype);
    }
}
