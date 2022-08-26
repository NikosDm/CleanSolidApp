using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Handlers.Queries;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Queries;
using CleanSolidApp.src.Core.Application.Profiles;
using CleanSolidApp.tests.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CleanSolidApp.tests.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeListRequestHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypeListRequestHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
    }

    [Fact]
    public async Task GetLeaveTypeListTest() 
    {
        var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDTO>>();

        result.Count.ShouldBe(2);
    }
}
