using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs.Validators;

public class CreateLeaveTypeDTOValidator : AbstractValidator<CreateLeaveTypeDTO>
{
    public CreateLeaveTypeDTOValidator()
    {
        Include(new LeaveTypeDTOValidator());
    }
}
