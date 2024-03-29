using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Commands;

public class DeleteLeaveAllocationCommandHandler: IRequestHandler<DeleteLeaveAllocationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.LeaveAllocationID);

        if (leaveAllocation == null) throw new NotFoundException(nameof(LeaveAllocation), request.LeaveAllocationID);

        await _unitOfWork.LeaveAllocationRepository.DeleteAsync(leaveAllocation);

        await _unitOfWork.Save();

        return Unit.Value;
    }
}
