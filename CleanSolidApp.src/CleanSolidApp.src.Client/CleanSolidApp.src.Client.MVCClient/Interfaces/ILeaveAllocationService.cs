using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Interfaces;

public interface ILeaveAllocationService
{
    Task<ApiResponse<int>> CreateLeaveAllocations(int leaveTypeID);
}
