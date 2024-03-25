using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WebAPIs.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public string Approved { get; set; }
    }
    
}