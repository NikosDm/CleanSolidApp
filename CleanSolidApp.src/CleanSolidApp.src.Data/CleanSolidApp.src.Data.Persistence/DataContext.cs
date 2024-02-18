using CleanSolidApp.src.Core.Domain;
using CleanSolidApp.src.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanSolidApp.src.Data.Persistence;

public class DataContext : AudibleDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
}

