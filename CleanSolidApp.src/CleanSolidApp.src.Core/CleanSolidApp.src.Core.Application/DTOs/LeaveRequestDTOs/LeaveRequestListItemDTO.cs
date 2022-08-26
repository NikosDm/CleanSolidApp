using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Application.Models.Identity;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;

public class LeaveRequestListItemDTO : BaseDTO
{
    public Employee Employee { get; set; }
    public string RequestingEmployeeID { get; set; }
    public LeaveTypeDTO LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public bool Approved { get; set; }
}
