using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs.Validators;

public class UpdateLeaveRequestDTOValidator : AbstractValidator<UpdateLeaveRequestDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new LeaveRequestDTOValidator(_leaveTypeRepository));

        RuleFor(p => p.ID).NotNull().WithMessage("{PropertyMessage} must be present");
    }
}
