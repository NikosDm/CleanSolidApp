using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Client.MVCClient;

public static class SD
{
    public static string ApiUrl { get; set; }

    public enum ApiType 
    {
        Get,
        Post,
        Put,
        Delete
    }
}
