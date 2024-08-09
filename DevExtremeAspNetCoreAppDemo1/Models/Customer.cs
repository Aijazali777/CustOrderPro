using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Models
{
    public class Customer : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CryptoOrder> CryptoOrders { get; set; } = new List<CryptoOrder>();
    }
}
