using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Client.MVCClient.DTOs;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;

namespace CleanSolidApp.src.Client.MVCClient.Services;

public class LeaveRequestService : BaseService, ILeaveRequestService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _clientFactory;

    public LeaveRequestService(IMapper mapper, IHttpClientFactory clientFactory, ILocalStorageService localStorageService) : base(clientFactory)
    {
        _localStorageService = localStorageService;
        _mapper = mapper;
        _clientFactory = clientFactory;
    }

    public async Task ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            var request = new ChangeLeaveRequestApprovalDTO { Approved = approved };
            await this.SendAsync<int>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Post,
                Url = SD.ApiUrl + "/api/LeaveRequests/changeApproval/" + id,
                Data = request,
                Token = _localStorageService.Exists("token") ? _localStorageService.GetStorageValue<string>("token") : string.Empty
            });
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApiResponse<int>> CreateLeaveRequest(CreateLeaveRequestViewModel leaveRequest)
    {
        try
        {
            var response = new ApiResponse<int>();
            CreateLeaveRequestDTO createLeaveRequest = _mapper.Map<CreateLeaveRequestDTO>(leaveRequest);
            var apiResponse = await this.SendAsync<int>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Post,
                Url = SD.ApiUrl + "/api/LeaveRequests",
                Data = createLeaveRequest,
                Token = _localStorageService.Exists("token") ? _localStorageService.GetStorageValue<string>("token") : string.Empty
            });

            return new ApiResponse<int> { Success = true };
        }
        catch 
        {
            return new ApiResponse<int> { Success = false };
        }
    }

    public Task DeleteLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AdminLeaveRequestViewViewModel> GetAdminLeaveRequestList()
    {
        var leaveRequests = await this.SendAsync<List<LeaveRequestDTO>>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ApiUrl + "/api/LeaveRequests",
            Token = _localStorageService.Exists("token") ? _localStorageService.GetStorageValue<string>("token") : string.Empty
        });

        var model = new AdminLeaveRequestViewViewModel
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            PendingRequests = leaveRequests.Count(q => q.Approved == false),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests)
        };
        return model;
    }

    public async Task<LeaveRequestViewModel> GetLeaveRequest(int id)
    {
        var leaveRequest = await this.SendAsync<LeaveRequestDTO>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ApiUrl + "/api/LeaveRequests/" + id,
            Token = _localStorageService.Exists("token") ? _localStorageService.GetStorageValue<string>("token") : string.Empty
        });

        return _mapper.Map<LeaveRequestViewModel>(leaveRequest);
    }

    public async Task<EmployeeLeaveRequestViewViewModel> GetUserLeaveRequests()
    {
        var leaveRequests = await this.SendAsync<List<LeaveRequestDTO>>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ApiUrl + "/api/LeaveRequests",
            Token = _localStorageService.Exists("token") ? _localStorageService.GetStorageValue<string>("token") : string.Empty
        });

        var allocations = await this.SendAsync<List<LeaveRequestDTO>>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ApiUrl + "/api/LeaveAllocations",
            Token = _localStorageService.Exists("token") ? _localStorageService.GetStorageValue<string>("token") : string.Empty
        });

        var model = new EmployeeLeaveRequestViewViewModel
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationViewModel>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests)
        };

        return model;
    }

}
