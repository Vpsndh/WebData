using System;
using System.Collections.Generic;

namespace WebData.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Rank { get; set; }

    public int? RolesId { get; set; }

    public virtual ICollection<Nguon> Nguons { get; set; } = new List<Nguon>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Role? Roles { get; set; }
}
