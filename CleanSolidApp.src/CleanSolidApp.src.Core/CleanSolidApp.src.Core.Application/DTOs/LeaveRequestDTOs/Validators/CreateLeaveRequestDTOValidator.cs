using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs.Validators;

public class CreateLeaveRequestDTOValidator : AbstractValidator<CreateLeaveRequestDTO>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new LeaveRequestDTOValidator(_leaveTypeRepository));
    }
}
