using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; } // Primary key
        public string UserId { get; set; } // Foreign key to AppUser
        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; } // Quantity of the product in the cart

        // Navigation properties
        public AppUser User { get; set; }
        public Product Product { get; set; }
    }

}
