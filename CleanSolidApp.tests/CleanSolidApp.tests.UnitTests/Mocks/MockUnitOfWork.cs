using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Persistence;
using Moq;

namespace CleanSolidApp.tests.UnitTests.Mocks;

public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var mockLeaveRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

        mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveRepo.Object);

        return mockUow;
    }
}
