using ViewModels;

namespace MyBlazorApp.Service
{
    public interface IBlazorReqService
    {
        Task<List<LeaveRequestVM>> GetAll();
        
    }
}
