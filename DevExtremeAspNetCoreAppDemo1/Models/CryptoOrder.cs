using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Models
{
    public class CryptoOrder
    {
        public int CryptoId { get; set; }
        public string CustomerId { get; set; }
        public string Symbol { get; set; }
        public double Quantity { get; set; }
        public string Price { get; set; }
        public Customer Customers { get; set; }
    }
}
