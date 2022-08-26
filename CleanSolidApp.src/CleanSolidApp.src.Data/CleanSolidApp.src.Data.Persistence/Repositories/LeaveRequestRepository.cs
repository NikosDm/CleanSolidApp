using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanSolidApp.src.Data.Persistence.Repositories;

public class LeaveRequestRepository: GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly DataContext _dbContext;

    public LeaveRequestRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ChangeApprovalStatusAsync(LeaveRequest leaveRequest, bool? Approved)
    {
        leaveRequest.Approved = Approved;
        _dbContext.Entry(leaveRequest).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
    {
        return await _dbContext.LeaveRequests.Include(p => p.LeaveType).ToListAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
    {
        return await _dbContext.LeaveRequests.Include(p => p.LeaveType).FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await _dbContext.LeaveRequests.Where(q=> q.RequestingEmployeeID == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }
}
