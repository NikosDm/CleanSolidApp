using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSolidApp.src.Data.Persistence.Configuration.Entities;

public class LeaveTypeConfiguration  : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType 
            {
                ID = 1,
                DefaultDays = 10,
                Name = "Vacation"
            },
            new LeaveType 
            {
                ID = 2,
                DefaultDays = 12,
                Name = "Sick"
            }
        );
    }
}
