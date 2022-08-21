using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Commands;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using CleanSolidApp.src.Core.Domain;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDTOValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDTO);

        leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

        return leaveRequest.ID;
    }
}

