﻿using DataAccess.Data.DTOs.ProductDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Queries.Products
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}