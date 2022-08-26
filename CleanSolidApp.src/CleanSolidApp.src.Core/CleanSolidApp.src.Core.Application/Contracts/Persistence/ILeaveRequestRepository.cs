using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Core.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
    Task ChangeApprovalStatusAsync(LeaveRequest leaveRequest, bool? Approved);
}
