using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveTypes.Handlers.Commands;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeDTOValidator();

        var validationResult = await validator.ValidateAsync(request.LeaveTypeDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);
        
        var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.LeaveTypeDTO.ID);

        _mapper.Map(request.LeaveTypeDTO, leaveType);

        await _unitOfWork.LeaveTypeRepository.UpdateAsync(leaveType);

        await _unitOfWork.Save();

        return Unit.Value;
    }
}
