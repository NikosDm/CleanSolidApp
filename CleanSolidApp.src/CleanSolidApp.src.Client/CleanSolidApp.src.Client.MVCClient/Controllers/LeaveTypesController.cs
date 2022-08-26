using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanSolidApp.src.Client.MVCClient.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class LeaveTypesController : Controller
{
    private readonly ILogger<LeaveTypesController> _logger;
    private readonly ILeaveTypeService _leaveTypesService;
    private readonly ILeaveAllocationService _leaveAllocationService;

    public LeaveTypesController(ILogger<LeaveTypesController> logger, ILeaveTypeService leaveTypesService, ILeaveAllocationService leaveAllocationService)
    {
        _logger = logger;
        _leaveTypesService = leaveTypesService;
        _leaveAllocationService = leaveAllocationService;
    }

    public async Task<ActionResult> Index()
    {
        var model = await _leaveTypesService.GetLeaveTypes();
        return View(model);
    }

    // GET: LeaveTypesController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var model = await _leaveTypesService.GetLeaveTypeDetails(id);

        return View(model);
    }

    // GET: LeaveTypesController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveTypeViewModel leaveType)
    {
        try
        {
            var response = await _leaveTypesService.CreateLeaveType(leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View(leaveType);
    }

    // GET: LeaveTypesController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var model = await _leaveTypesService.GetLeaveTypeDetails(id);

        return View(model);
    }

    // POST: LeaveTypesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(LeaveTypeViewModel leaveType)
    {
        try
        {
            var response = await _leaveTypesService.UpdateLeaveType(leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View(leaveType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var response = await _leaveTypesService.DeleteLeaveType(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Allocate(int id)
    {
        try
        {
            var response = await _leaveAllocationService.CreateLeaveAllocations(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View();
    }

}