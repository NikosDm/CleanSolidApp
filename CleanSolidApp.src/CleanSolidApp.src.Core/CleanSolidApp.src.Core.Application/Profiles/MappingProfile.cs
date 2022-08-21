using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Domain;

namespace CleanSolidApp.src.Core.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDTO>().ReverseMap();
        CreateMap<LeaveRequest, CreateLeaveRequestDTO>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestListItemDTO>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
        CreateMap<LeaveType, CreateLeaveTypeDTO>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDTO>().ReverseMap();
        CreateMap<LeaveAllocation, CreateLeaveAllocationDTO>().ReverseMap();
    }
}
