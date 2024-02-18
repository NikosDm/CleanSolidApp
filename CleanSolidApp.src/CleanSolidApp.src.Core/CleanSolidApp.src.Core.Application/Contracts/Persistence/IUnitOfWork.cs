using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Core.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    ILeaveAllocationRepository LeaveAllocationRepository { get; }
    ILeaveRequestRepository LeaveRequestRepository { get; }
    ILeaveTypeRepository LeaveTypeRepository { get; }
    Task Save();
}
