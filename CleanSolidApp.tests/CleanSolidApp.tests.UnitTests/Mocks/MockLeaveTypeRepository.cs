using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using CleanSolidApp.src.Core.Domain;
using Moq;

namespace CleanSolidApp.tests.UnitTests.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                ID = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new LeaveType
            {
                ID = 2,
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new LeaveType
            {
                ID = 3,
                DefaultDays = 15,
                Name = "Test Maternity"
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

        mockRepo.Setup(r => r.AddAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) => 
        {
            leaveTypes.Add(leaveType);
            return leaveType;
        });

        return mockRepo;

    }
}
