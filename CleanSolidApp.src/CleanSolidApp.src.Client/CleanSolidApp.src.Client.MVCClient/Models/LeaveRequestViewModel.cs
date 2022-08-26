using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanSolidApp.src.Client.MVCClient.Models;

public class LeaveRequestViewModel : CreateLeaveRequestViewModel
{
    public int ID { get; set; }

    [Display(Name = "Date Requested")]
    public DateTime DateRequested { get; set; }
    
    [Display(Name = "Date Actioned")]
    public DateTime DateActioned { get; set; }
    
    [Display(Name = "Approval State")]
    public bool? Approved { get; set; }

    public bool Cancelled { get; set; }
    public LeaveTypeViewModel LeaveType { get; set; }
    public EmployeeViewModel Employee { get; set; }

}

public class CreateLeaveRequestViewModel
{

    [Display(Name = "Start Date")]
    [Required]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [Required]
    public DateTime EndDate { get; set; }

    public SelectList LeaveTypes { get; set; }

    [Display(Name = "Leave Type")]
    [Required]
    public int LeaveTypeID { get; set; }

    [Display(Name = "Comments")]
    [MaxLength(300)]
    public string RequestComments { get; set; }
}

public class AdminLeaveRequestViewViewModel
{
    [Display(Name = "Total Number Of Requests")]
    public int TotalRequests { get; set; }
    [Display(Name = "Approved Requests")]
    public int ApprovedRequests { get; set; }
    [Display(Name = "Pending Requests")]
    public int PendingRequests { get; set; }
    [Display(Name = "Rejected Requests")]
    public int RejectedRequests { get; set; }
    public List<LeaveRequestViewModel> LeaveRequests { get; set; }
}


public class EmployeeLeaveRequestViewViewModel
{
    public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
    public List<LeaveRequestViewModel> LeaveRequests { get; set; }
}