using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationDTOValidator(_unitOfWork.LeaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.UpdateLeaveAllocationDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.UpdateLeaveAllocationDTO.ID);

        _mapper.Map(request.UpdateLeaveAllocationDTO, leaveAllocation);

        await _unitOfWork.LeaveAllocationRepository.UpdateAsync(leaveAllocation);

        await _unitOfWork.Save();

        return Unit.Value;
    }
}
