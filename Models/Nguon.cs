using System;
using System.Collections.Generic;

namespace WebData.Models;

public partial class Nguon
{
    public int NguonId { get; set; }

    public string? NguonName { get; set; }

    public int? LoaiDb { get; set; }

    public int? Loai1 { get; set; }

    public int? Loai2 { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }
}
