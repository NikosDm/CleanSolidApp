using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;

public class LeaveAllocationDTOValidator : AbstractValidator<ILeaveAllocationDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {       
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.NumberOfDays)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(p => p.LeaveTypeID)
            .GreaterThan(0)
            .MustAsync(async (id, token) => 
            {
                var leaveTypeExists = await _leaveTypeRepository.ExistsAsync(id);
                return !leaveTypeExists;
            }).WithMessage("{PropertyName} does not exist");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");
    }
}
