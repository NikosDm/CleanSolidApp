using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler: IRequestHandler<DeleteLeaveTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.LeaveTypeID);

        if (leaveType == null) throw new NotFoundException(nameof(LeaveType), request.LeaveTypeID);

        await _unitOfWork.LeaveTypeRepository.DeleteAsync(leaveType);

        await _unitOfWork.Save();

        return Unit.Value;
    }
}