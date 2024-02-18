using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using MediatR;
using CleanSolidApp.src.Core.Application.Contracts.Identity;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDTOValidator(_unitOfWork.LeaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);
        else 
        {
            var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.CreateLeaveAllocationDTO.LeaveTypeID);
            var employees = await _userService.GetEmployees();
            var period = DateTime.Now.Year;
            var allocations = new List<LeaveAllocation>();
            foreach (var emp in employees)
            {
                if (await _unitOfWork.LeaveAllocationRepository.AllocationExists(emp.ID, leaveType.ID, period))
                    continue;
                allocations.Add(new LeaveAllocation
                {
                    EmployeeID = emp.ID,
                    LeaveTypeID = leaveType.ID,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }

            await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);

            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
