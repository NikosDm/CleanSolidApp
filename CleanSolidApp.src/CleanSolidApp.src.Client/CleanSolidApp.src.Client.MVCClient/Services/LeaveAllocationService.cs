using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.DTOs;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Services;

public class LeaveAllocationService : BaseService, ILeaveAllocationService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILocalStorageService _localStorage;

    public LeaveAllocationService(IHttpClientFactory clientFactory, ILocalStorageService localStorage) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        _localStorage = localStorage;
    }

    public async Task<ApiResponse<int>> CreateLeaveAllocations(int leaveTypeID)
    {
        try 
        {
            var createLeaveAllocation = new CreateLeaveAllocationDTO { LeaveTypeID = leaveTypeID };
            await this.SendAsync<int>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Post,
                Url = SD.ApiUrl + "/api/LeaveAllocations",
                Data = createLeaveAllocation,
                Token = _localStorage.Exists("token") ? _localStorage.GetStorageValue<string>("token") : string.Empty
            });

            return new ApiResponse<int> { Success = true };
        }
        catch 
        {
            return new ApiResponse<int> { Success = false }; 
        }
    }
}
