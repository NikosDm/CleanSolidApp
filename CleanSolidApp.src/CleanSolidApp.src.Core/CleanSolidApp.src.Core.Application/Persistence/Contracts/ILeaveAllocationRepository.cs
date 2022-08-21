using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Core.Application.Persistence.Contracts;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
}
