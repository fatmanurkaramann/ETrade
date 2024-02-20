﻿using ETradeAPI.Domain.Entities.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Domain.Entities
{
    public class BasketItem:BaseEntity
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }
        public float Quantity { get; set; }
    }
}