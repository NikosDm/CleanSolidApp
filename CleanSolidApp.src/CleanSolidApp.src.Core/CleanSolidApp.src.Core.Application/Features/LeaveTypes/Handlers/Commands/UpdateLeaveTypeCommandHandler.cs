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
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeDTOValidator();

        var validationResult = await validator.ValidateAsync(request.LeaveTypeDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);
        
        var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeDTO.ID);

        _mapper.Map(request.LeaveTypeDTO, leaveType);

        await _leaveTypeRepository.UpdateAsync(leaveType);

        return Unit.Value;
    }
}
