using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Client.MVCClient.Models;

public class LeaveAllocationViewModel
{
    public int ID { get; set; }
    
    [Display(Name = "Number Of Days")]
    public int NumberOfDays { get; set; }
    public DateTime DateCreated { get; set; }
    public int Period { get; set; }
    
    public LeaveTypeViewModel LeaveType { get; set; }
    public int LeaveTypeID { get; set; }
}

public class CreateLeaveAllocationViewModel
{
    public int LeaveTypeID { get; set; }
}

public class UpdateLeaveAllocationViewModel
{
    public int ID { get; set; }

    [Display(Name="Number Of Days")]
    [Range(1,50, ErrorMessage = "Enter Valid Number")]
    public int NumberOfDays { get; set; }
    public LeaveTypeViewModel LeaveType { get; set; }

}

public class ViewLeaveAllocationsViewModel
{
    public string EmployeeID { get; set; }
    public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
}
