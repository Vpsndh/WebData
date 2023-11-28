using System;
using System.Collections.Generic;

namespace WebData.Models;

public partial class Category
{
    public int CatId { get; set; }

    public string? CatName { get; set; }

    public string? Alias { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
