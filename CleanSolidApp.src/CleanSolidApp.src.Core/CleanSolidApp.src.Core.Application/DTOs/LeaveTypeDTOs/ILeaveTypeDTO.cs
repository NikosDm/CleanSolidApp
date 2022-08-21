using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;

public interface ILeaveTypeDTO
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}
