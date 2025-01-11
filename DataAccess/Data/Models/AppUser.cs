using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; }
        public string CityRegion { get; set; }
        public string CcNumber { get; set; }

        // Navigation properties
        public ICollection<UserHasRole> UserRoles { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
