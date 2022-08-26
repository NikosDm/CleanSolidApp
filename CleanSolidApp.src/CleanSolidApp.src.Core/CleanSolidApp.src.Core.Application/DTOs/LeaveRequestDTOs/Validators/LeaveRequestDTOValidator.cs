using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs.Validators;

public class LeaveRequestDTOValidator : AbstractValidator<ILeaveRequestDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.StartDate)
            .LessThan(x => x.StartDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(p => p.EndDate)
            .GreaterThan(x => x.EndDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeID)
            .GreaterThan(0)
            .MustAsync(async (id, token) => 
            {
                var leaveTypeExists = await _leaveTypeRepository.ExistsAsync(id);
                return !leaveTypeExists;
            }).WithMessage("{PropertyName} does not exist");
    }
}
