using System;
using System.Collections.Generic;

namespace WebData.Models;

public partial class Role
{
    public int RolesId { get; set; }

    public string? RolesName { get; set; }

    public string? RolesDes { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
