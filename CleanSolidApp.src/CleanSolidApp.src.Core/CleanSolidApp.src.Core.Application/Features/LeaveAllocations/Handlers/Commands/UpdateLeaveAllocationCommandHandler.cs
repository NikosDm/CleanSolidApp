using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationDTOValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.UpdateLeaveAllocationDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.UpdateLeaveAllocationDTO.ID);

        _mapper.Map(request.UpdateLeaveAllocationDTO, leaveAllocation);

        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
