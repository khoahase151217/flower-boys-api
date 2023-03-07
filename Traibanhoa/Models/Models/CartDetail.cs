﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class CartDetail
    {
        public Guid CartId { get; set; }
        public Guid ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}