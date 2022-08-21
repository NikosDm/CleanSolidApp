using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Queries;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Queries;

public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDTO>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveAllocationDTO>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync();

        return _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);
    }
}
