﻿using ETradeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public List<ETradeAPI.Domain.Entities.Product> Products { get; set; }
    }
}