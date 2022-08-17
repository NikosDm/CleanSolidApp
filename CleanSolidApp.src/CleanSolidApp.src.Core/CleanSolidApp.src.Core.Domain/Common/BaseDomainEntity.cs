using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Core.Domain.Common;

public abstract class BaseDomainEntity
{
    public int ID { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
}
