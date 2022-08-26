using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Client.MVCClient.DTOs;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Services;

public class LeaveTypeService : BaseService, ILeaveTypeService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILocalStorageService _localStorage;
    private readonly IMapper _mapper;

    public LeaveTypeService(IHttpClientFactory clientFactory, IMapper mapper, ILocalStorageService localStorage) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _localStorage = localStorage;
    }

    public async Task<ApiResponse<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveTypeViewModel)
    {
        try 
        {
            var generatedId = await this.SendAsync<int>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Post,
                Url = SD.ApiUrl + "/api/LeaveTypes",
                Data = leaveTypeViewModel,
                Token = _localStorage.Exists("token") ? _localStorage.GetStorageValue<string>("token") : string.Empty
            });

            return new ApiResponse<int> { Success = true, Data = generatedId };
        }
        catch 
        {
            return new ApiResponse<int> { Success = false }; 
        }
    }

    public async Task<ApiResponse<int>> DeleteLeaveType(int leaveTypeId)
    {
        try 
        {
            await this.SendAsync<string>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Delete,
                Url = SD.ApiUrl + "/api/LeaveTypes/" + leaveTypeId,
                Token = _localStorage.Exists("token") ? _localStorage.GetStorageValue<string>("token") : string.Empty
            });

            return new ApiResponse<int> { Success = true, Data = leaveTypeId };
        }
        catch 
        {
            return new ApiResponse<int> { Success = false };
        }
    }

    public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int leaveTypeId)
    {
        var response = await this.SendAsync<LeaveTypeDTO>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ApiUrl + "/api/LeaveTypes/" + leaveTypeId,
            Token = _localStorage.Exists("token") ? _localStorage.GetStorageValue<string>("token") : string.Empty
        });

        return _mapper.Map<LeaveTypeViewModel>(response);
    }

    public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
    {
        var response = await this.SendAsync<List<LeaveTypeDTO>>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ApiUrl + "/api/LeaveTypes",
            Token = _localStorage.Exists("token") ? _localStorage.GetStorageValue<string>("token") : string.Empty
        });

        return _mapper.Map<List<LeaveTypeViewModel>>(response);
    }

    public async Task<ApiResponse<int>> UpdateLeaveType(LeaveTypeViewModel leaveTypeViewModel)
    {
        try 
        {
            await this.SendAsync<string>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Put,
                Url = SD.ApiUrl + "/api/LeaveTypes",
                Data = leaveTypeViewModel,
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
