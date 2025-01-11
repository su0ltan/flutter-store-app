using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public int ConfirmationNumber { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        // Navigation properties
        public ICollection<OrderHasProduct> OrderProducts { get; set; }
    }
}
