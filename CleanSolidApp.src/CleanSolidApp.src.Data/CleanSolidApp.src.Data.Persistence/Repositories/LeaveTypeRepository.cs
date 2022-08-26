using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Data.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    private readonly DataContext _dbContext;

    public LeaveTypeRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
