using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.DTOs.ShoppingCartDto
{
    public class AddToCartDto

    {

        public string UserId {  get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
