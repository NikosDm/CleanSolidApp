using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using CleanSolidApp.src.Core.Domain;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDTOValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDTO);

        leaveAllocation = await _leaveAllocationRepository.AddAsync(leaveAllocation);

        return leaveAllocation.ID;
    }
}
