using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Queries;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using CleanSolidApp.src.Core.Application.Contracts.Identity;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListItemDTO>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<List<LeaveRequestListItemDTO>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        var leaveRequests = new List<LeaveRequest>();
        var requests = new List<LeaveRequestListItemDTO>();

        if (request.IsLoggedInUser)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                q => q.Type == "UID")?.Value;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await _userService.GetEmployee(userId);
            requests = _mapper.Map<List<LeaveRequestListItemDTO>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = employee;
            }
        }
        else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();
            requests = _mapper.Map<List<LeaveRequestListItemDTO>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = await _userService.GetEmployee(req.RequestingEmployeeID);
            }
        }

        return requests;
    }
}
