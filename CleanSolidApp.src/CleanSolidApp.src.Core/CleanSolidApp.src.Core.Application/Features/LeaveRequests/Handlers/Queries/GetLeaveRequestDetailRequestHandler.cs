using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Queries;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDTO>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<LeaveRequestDTO> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.ID);

        return _mapper.Map<LeaveRequestDTO>(leaveRequest);
    }
}
