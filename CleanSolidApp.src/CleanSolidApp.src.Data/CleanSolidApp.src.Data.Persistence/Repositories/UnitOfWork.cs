using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;

namespace CleanSolidApp.src.Data.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    private ILeaveAllocationRepository _leaveAllocationRepository;
    private ILeaveRequestRepository _leaveRequestRepository;
    private ILeaveTypeRepository _leaveTypeRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnitOfWork(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

    public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);

    public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task Save()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst("UID")?.Value;
        await _context.SaveChangesAsync(username);
    }
}
