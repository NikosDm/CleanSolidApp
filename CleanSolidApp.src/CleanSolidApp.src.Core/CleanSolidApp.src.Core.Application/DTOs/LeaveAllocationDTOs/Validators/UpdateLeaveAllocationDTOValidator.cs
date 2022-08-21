using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;

public class UpdateLeaveAllocationDTOValidator : AbstractValidator<UpdateLeaveAllocationDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {       
        _leaveTypeRepository = leaveTypeRepository;

        Include(new LeaveAllocationDTOValidator(_leaveTypeRepository));
        
        RuleFor(p => p.ID).NotNull().WithMessage("{PropertyMessage} must be present");
    }
}
