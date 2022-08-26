using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Queries;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using MediatR;
using CleanSolidApp.src.Core.Application.Contracts.Identity;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDTO>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<LeaveRequestDTO> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.ID);

        var leaveRequestDTO = _mapper.Map<LeaveRequestDTO>(leaveRequest);

        leaveRequestDTO.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeID);

        return leaveRequestDTO;
    }
}
