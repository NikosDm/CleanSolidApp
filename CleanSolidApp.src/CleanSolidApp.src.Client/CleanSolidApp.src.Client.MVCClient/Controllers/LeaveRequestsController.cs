using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CleanSolidApp.src.Client.MVCClient.Controllers;

[Route("[controller]")]
[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly ILeaveTypeService _leaveTypeService;
    private readonly ILeaveRequestService _leaveRequestService;
    private readonly IMapper _mapper;

    public LeaveRequestsController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService,
        IMapper mapper)
    {
        this._leaveTypeService = leaveTypeService;
        this._leaveRequestService = leaveRequestService;
        this._mapper = mapper;
    }

    // GET: LeaveRequest/Create
    public async Task<ActionResult> Create()
    {
        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "ID", "Name");
        var model = new CreateLeaveRequestViewModel
        {
            LeaveTypes = leaveTypeItems
        };
        return View(model);
    }

    // POST: LeaveRequest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveRequestViewModel leaveRequest)
    {
        if (ModelState.IsValid)
        {
            var response = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
        leaveRequest.LeaveTypes = leaveTypeItems;

        return View(leaveRequest);
    }

    [Authorize(Roles = "Administrator")]
    // GET: LeaveRequest
    public async Task<ActionResult> Index()
    {
        var model = await _leaveRequestService.GetAdminLeaveRequestList();
        return View(model);
    }

    public async Task<ActionResult> Details(int id)
    {
        var model = await _leaveRequestService.GetLeaveRequest(id);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> ApproveRequest(int id, bool approved)
    {
        try
        {
            await _leaveRequestService.ApproveLeaveRequest(id, approved);
            return RedirectToAction(nameof(Index));
        }
        catch 
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
