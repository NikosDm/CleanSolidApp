using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;

public class CreateLeaveAllocationDTOValidator : AbstractValidator<CreateLeaveAllocationDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {       
        _leaveTypeRepository = leaveTypeRepository;

        Include(new LeaveAllocationDTOValidator(_leaveTypeRepository));
    }
}
