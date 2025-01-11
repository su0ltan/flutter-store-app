using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Commands.ShoppingCart
{
    public class ClearCartCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
