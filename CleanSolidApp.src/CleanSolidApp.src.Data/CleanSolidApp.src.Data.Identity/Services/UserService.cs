using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Contracts.Identity;
using CleanSolidApp.src.Core.Application.Models.Identity;
using CleanSolidApp.src.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanSolidApp.src.Data.Identity.Services;

public class UserService : IUserService
{
    public readonly UserManager<ApplicationUser> _userManager;
    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Employee> GetEmployee(string userID)
    {
        var user = await _userManager.FindByIdAsync(userID);

        return new Employee 
        {
            ID = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");

        return employees.Select(x => new Employee 
        {
            ID = x.Id,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName
        }).ToList();
    }
}
