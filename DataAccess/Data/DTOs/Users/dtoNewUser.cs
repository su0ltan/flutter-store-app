using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.DTOs.Users
{
    public class dtoNewUser
    {
        [Required]
        public string userName { get; set; }

        public string phoneNumber { get; set; }

        [Required]
        public string password { get; set; }
    }
}
