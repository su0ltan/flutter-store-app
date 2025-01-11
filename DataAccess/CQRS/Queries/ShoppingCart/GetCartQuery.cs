using DataAccess.Data.DTOs.ShoppingCartDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Queries.ShoppingCart
{
    public class GetCartQuery : IRequest<ShoppingCartDto>
    {
        public string UserId { get; set; }
    }
}
