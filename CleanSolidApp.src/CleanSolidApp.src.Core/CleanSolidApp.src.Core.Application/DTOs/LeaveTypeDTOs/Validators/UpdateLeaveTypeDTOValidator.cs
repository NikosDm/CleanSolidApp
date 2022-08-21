using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs.Validators;

public class UpdateLeaveTypeDTOValidator : AbstractValidator<LeaveTypeDTO>
{
    public UpdateLeaveTypeDTOValidator()
    {
        Include(new LeaveTypeDTOValidator());

        RuleFor(p => p.ID).NotNull().WithMessage("{PropertyMessage} must be present");
    }
}
