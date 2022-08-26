using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanSolidApp.src.Data.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly DataContext _dbContext;

    public LeaveAllocationRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _dbContext.AddRangeAsync(allocations);
    }

    public async Task<bool> AllocationExists(string userID, int leaveTypeID, int period)
    {
        return await _dbContext.LeaveAllocations.AnyAsync(q => q.EmployeeID == userID
            && q.LeaveTypeID == leaveTypeID
            && q.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userID)
    {
        var leaveAllocations = await _dbContext.LeaveAllocations.Where(q => q.EmployeeID == userID)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
    {
        var leaveAllocation = await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.ID == id);

        return leaveAllocation;
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userID, int leaveTypeID)
    {
        return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeID == userID
            && q.LeaveTypeID == leaveTypeID);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }
}

