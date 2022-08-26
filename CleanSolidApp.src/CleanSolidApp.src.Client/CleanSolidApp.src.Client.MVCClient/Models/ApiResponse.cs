using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Client.MVCClient.Models;

public class ApiResponse<T>
{
    public string Message { get; set; }
    public string ValidationErrors { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
}
