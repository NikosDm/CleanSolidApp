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
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using FluentValidation.Results;
using System.Security.Claims;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _emailSender = emailSender;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDTOValidator(_unitOfWork.LeaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDTO);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var userID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UID")?.Value;

        var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userID, request.CreateLeaveRequestDTO.LeaveTypeID);
        
        int daysRequested = (int)(request.CreateLeaveRequestDTO.EndDate - request.CreateLeaveRequestDTO.StartDate).TotalDays;
        
        if (daysRequested > allocation.NumberOfDays) 
        {
            validationResult.Errors.Add(new ValidationFailure(nameof(request.CreateLeaveRequestDTO.EndDate), "You do not have enough days for this request"));
            throw new ValidationException(validationResult);
        }

        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDTO);

        leaveRequest.RequestingEmployeeID = userID;

        leaveRequest = await _unitOfWork.LeaveRequestRepository.AddAsync(leaveRequest);

        try 
        {
            
            var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var email = new Email 
            {
                To = emailAddress,
                Body = $"Your leave request for {request.CreateLeaveRequestDTO.StartDate} to {request.CreateLeaveRequestDTO.EndDate} has been submitted successfully",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmailAsync(email);
        }
        catch(Exception ex)
        {
            //Log this
            Console.WriteLine(ex.Message);
        }

        await _unitOfWork.Save();

        return leaveRequest.ID;
    }
}

