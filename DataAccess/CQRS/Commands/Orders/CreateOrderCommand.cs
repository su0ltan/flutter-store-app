using DataAccess.Data.DTOs.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Commands.Orders
{
    public class CreateOrderCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public int ConfirmationNumber { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } // Simplified item list
    }


}
