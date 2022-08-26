using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Core.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
    Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userID);
    Task<bool> AllocationExists(string userID, int leaveTypeID, int period);
    Task AddAllocations(List<LeaveAllocation> allocations);
    Task<LeaveAllocation> GetUserAllocations(string userID, int leaveTypeID);
}
