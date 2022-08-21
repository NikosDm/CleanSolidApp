using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using CleanSolidApp.src.Core.Domain;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Commands;

public class DeleteLeaveAllocationCommandHandler: IRequestHandler<DeleteLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.LeaveAllocationID);

        if (leaveAllocation == null) throw new NotFoundException(nameof(LeaveAllocation), request.LeaveAllocationID);

        await _leaveAllocationRepository.DeleteAsync(leaveAllocation);

        return Unit.Value;
    }
}
