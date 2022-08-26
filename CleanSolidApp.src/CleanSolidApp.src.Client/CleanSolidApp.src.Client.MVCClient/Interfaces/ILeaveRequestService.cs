using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Interfaces;

public interface ILeaveRequestService
{
    Task<AdminLeaveRequestViewViewModel> GetAdminLeaveRequestList();
    Task<EmployeeLeaveRequestViewViewModel> GetUserLeaveRequests();
    Task<ApiResponse<int>> CreateLeaveRequest(CreateLeaveRequestViewModel leaveRequest);
    Task<LeaveRequestViewModel> GetLeaveRequest(int id);
    Task DeleteLeaveRequest(int id);
    Task ApproveLeaveRequest(int id, bool approved);
}
