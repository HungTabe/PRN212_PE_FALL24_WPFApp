﻿using System;
using System.Collections.Generic;

namespace PerfumeManagement.DAL.Entities;

public partial class PerfumeInformation
{
    public string PerfumeId { get; set; } = null!;

    public string PerfumeName { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }

    public string Concentration { get; set; } = null!;

    public string Longevity { get; set; } = null!;

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public string? ProductionCompanyId { get; set; }

    public virtual ProductionCompany? ProductionCompany { get; set; }
}
