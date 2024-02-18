using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Handlers.Commands;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Commands;
using CleanSolidApp.src.Core.Application.Profiles;
using CleanSolidApp.tests.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CleanSolidApp.tests.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly CreateLeaveTypeDTO _leaveType;
    private readonly CreateLeaveTypeCommandHandler _handler;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockUow = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateLeaveTypeCommandHandler(_mockUow.Object, _mapper);

        _leaveType = new CreateLeaveTypeDTO
        {
            DefaultDays = 15,
            Name = "Test DTO"
        };
    }

    [Fact]
    public async Task Valid_LeaveType_Added()
    {
        var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDTO = _leaveType }, CancellationToken.None);

        var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAllAsync();

        result.ShouldBeOfType<int>();

        leaveTypes.Count().ShouldBe(3);
    }

    [Fact]
    public async Task InValid_LeaveType_Added()
    {
        _leaveType.DefaultDays = -1;

        ValidationException ex = await Should.ThrowAsync<ValidationException>
        (
            async () => await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDTO = _leaveType }, CancellationToken.None)
        );

        var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAllAsync();
        
        leaveTypes.Count().ShouldBe(2);

        ex.ShouldNotBeNull();
    }
}

