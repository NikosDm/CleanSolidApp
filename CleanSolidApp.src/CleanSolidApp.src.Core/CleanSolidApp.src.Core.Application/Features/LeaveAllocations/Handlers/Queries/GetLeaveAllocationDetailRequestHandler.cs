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

public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, LeaveAllocationDTO>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    
    public async Task<LeaveAllocationDTO> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetailsAsync(request.ID);

        return _mapper.Map<LeaveAllocationDTO>(leaveAllocation);
    }
}