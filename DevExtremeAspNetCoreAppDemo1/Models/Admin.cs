using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExtremeAspNetCoreAppDemo1.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public double Phone { get; set; }
        public string Email { get; set; }
    }
}
