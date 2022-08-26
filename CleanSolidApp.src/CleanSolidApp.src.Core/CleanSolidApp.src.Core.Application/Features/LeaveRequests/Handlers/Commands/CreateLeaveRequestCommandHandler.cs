using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs.Validators;
using CleanSolidApp.src.Core.Application.Exceptions;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Commands;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using MediatR;
using CleanSolidApp.src.Core.Application.Contracts.Infrastructure;
using CleanSolidApp.src.Core.Application.Models;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
        ILeaveTypeRepository leaveTypeRepository, 
        IMapper mapper,
        IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDTOValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDTO);

        leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

        var email = new Email 
        {
            To = "employee@org.com",
            Body = $"Your leave request for {request.CreateLeaveRequestDTO.StartDate} to {request.CreateLeaveRequestDTO.EndDate} has been submitted successfully",
            Subject = "Leave Request Submitted"
        };

        try 
        {
            await _emailSender.SendEmailAsync(email);
        }
        catch(Exception ex)
        {
            //Log this
            Console.WriteLine(ex.Message);
        }

        return leaveRequest.ID;
    }
}

