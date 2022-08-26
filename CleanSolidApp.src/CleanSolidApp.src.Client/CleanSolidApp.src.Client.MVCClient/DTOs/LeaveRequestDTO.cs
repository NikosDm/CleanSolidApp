using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Client.MVCClient.DTOs;

public class LeaveRequestDTO 
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveTypeDTO LeaveType { get; set; }
    public int LeaveTypeID { get; set; }
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; }
    public DateTime DateActioned { get; set; }
    public bool Approved { get; set; }
    public bool Cancelled { get; set; }
}
