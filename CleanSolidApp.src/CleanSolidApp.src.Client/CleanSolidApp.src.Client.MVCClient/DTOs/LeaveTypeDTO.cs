using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Client.MVCClient.DTOs;

public class LeaveTypeDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}
