using System;
using System.Collections.Generic;

namespace WebData.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? Contents { get; set; }

    public string? NhanXet { get; set; }

    public int? SoBct { get; set; }

    public string? NguonBct { get; set; }

    public int? AccountId { get; set; }

    public string? Author { get; set; }

    public int? CatId { get; set; }

    public DateTime? CreateAt { get; set; }

    public int? DanhGia { get; set; }

    public string? Keys { get; set; }

    public string? TtthuTin { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Category? Cat { get; set; }
}
