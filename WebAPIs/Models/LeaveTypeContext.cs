using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
namespace WebAPIs.Models
{
    public class LeaveTypeContext : DbContext
    {
        public LeaveTypeContext(DbContextOptions<LeaveTypeContext> options) : base(options)
        {
        }
        public DbSet<LeaveType> LeaveTypess { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaveRequest>()
                .Property(p => p.DateRequested)
                .HasColumnType("date");
            modelBuilder.Entity<LeaveRequest>()
                .Property(p => p.StartDate)
                .HasColumnType("date");
            modelBuilder.Entity<LeaveRequest>()
                .Property(p => p.EndDate)
                .HasColumnType("date");
            base.OnModelCreating(modelBuilder);
        }
    }
}