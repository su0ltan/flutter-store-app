using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.DTOs.Orders
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } // User placing the order
        public int ConfirmationNumber { get; set; } // Optional confirmation number
        public List<CreateOrderItemDto> Items { get; set; } // Only include necessary fields
    }

    public class CreateOrderItemDto
    {
        public int ProductId { get; set; } // Only Product ID is needed
        public int Quantity { get; set; } // Quantity of the product
    }


}
