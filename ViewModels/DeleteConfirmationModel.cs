using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace MyBlazorApp.Models
{
    public class DeleteConfirmationModel
    {
        public bool IsOpen { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
    }
}
