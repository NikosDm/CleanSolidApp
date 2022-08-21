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

public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListItemDTO>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestListItemDTO>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();

        return _mapper.Map<List<LeaveRequestListItemDTO>>(leaveRequests);
    }
}
