using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestDTOValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.LeaveRequestDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveRequest = await _leaveRequestRepository.GetAsync(request.LeaveRequestID);

        if (request.LeaveRequestDTO != null)
        {
            _mapper.Map(request.LeaveRequestDTO, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);
        }
        else if (request.ChangeLeaveRequestApprovalDTO != null)
        {
            await _leaveRequestRepository.ChangeApprovalStatusAsync(leaveRequest, request.ChangeLeaveRequestApprovalDTO.Approved);
        }

        return Unit.Value;
    }
}
