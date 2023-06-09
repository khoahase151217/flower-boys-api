﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class Basket
    {
        public Basket()
        {
            BasketDetails = new HashSet<BasketDetail>();
            OrderBasketDetails = new HashSet<OrderBasketDetail>();
        }

        public Guid BasketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? View { get; set; }
        public decimal? BasketPrice { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
        public virtual ICollection<OrderBasketDetail> OrderBasketDetails { get; set; }
    }
}
