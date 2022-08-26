using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.Models;

namespace CleanSolidApp.src.Core.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(Email email);
}
