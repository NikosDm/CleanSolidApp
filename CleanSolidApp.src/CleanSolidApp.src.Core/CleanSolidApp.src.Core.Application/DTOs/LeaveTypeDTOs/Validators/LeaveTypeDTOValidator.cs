using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs.Validators;

public class LeaveTypeDTOValidator : AbstractValidator<ILeaveTypeDTO>
{
    public LeaveTypeDTOValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            
        RuleFor(p => p.DefaultDays)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be between 0 and 100")
            .LessThan(100).WithMessage("{PropertyName} must be between 0 and 100");
    }
}