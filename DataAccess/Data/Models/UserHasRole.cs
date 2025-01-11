using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Models
{
    public class UserHasRole
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
