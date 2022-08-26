using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Client.MVCClient.DTOs;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLeaveTypeDTO, CreateLeaveTypeViewModel>().ReverseMap();
        CreateMap<LeaveTypeDTO, LeaveTypeViewModel>().ReverseMap();
    }
}
