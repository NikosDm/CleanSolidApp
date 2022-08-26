using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Interfaces;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeViewModel>> GetLeaveTypes();
    Task<LeaveTypeViewModel> GetLeaveTypeDetails(int leaveTypeId);
    Task<ApiResponse<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveTypeViewModel);
    Task<ApiResponse<int>> UpdateLeaveType(LeaveTypeViewModel leaveTypeViewModel);
    Task<ApiResponse<int>> DeleteLeaveType(int leaveTypeId);
}
