using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Interfaces;

public interface IBaseService : IDisposable
{
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}
