using ViewModels;

namespace WebAPIs.Models
{
    public class DeleteConfirmationModel
    {
        public bool IsOpen { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
    }
}
