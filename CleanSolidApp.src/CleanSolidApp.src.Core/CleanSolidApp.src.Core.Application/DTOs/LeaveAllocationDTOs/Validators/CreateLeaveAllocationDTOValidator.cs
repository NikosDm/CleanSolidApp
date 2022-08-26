using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;

public class CreateLeaveAllocationDTOValidator : AbstractValidator<CreateLeaveAllocationDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {       
        _leaveTypeRepository = leaveTypeRepository;
        
        RuleFor(p => p.LeaveTypeID)
            .GreaterThan(0)
            .MustAsync(async (id, token) => 
            {
                var leaveTypeExists = await _leaveTypeRepository.ExistsAsync(id);
                return leaveTypeExists;
            }).WithMessage("{PropertyName} does not exist");
    }
}
