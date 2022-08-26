using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Data.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly DataContext _dbContext;

    public LeaveAllocationRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
    {
        throw new NotImplementedException();
    }
}

