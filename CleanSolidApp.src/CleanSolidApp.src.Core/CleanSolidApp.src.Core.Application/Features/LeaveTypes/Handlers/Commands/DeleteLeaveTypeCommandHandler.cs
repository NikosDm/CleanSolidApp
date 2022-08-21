using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Commands;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using CleanSolidApp.src.Core.Domain;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler: IRequestHandler<DeleteLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeID);

        if (leaveType == null) throw new NotFoundException(nameof(LeaveType), request.LeaveTypeID);

        await _leaveTypeRepository.DeleteAsync(leaveType);

        return Unit.Value;
    }
}