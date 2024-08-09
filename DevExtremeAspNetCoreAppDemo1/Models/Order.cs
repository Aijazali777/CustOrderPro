using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int NumberOfItems { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        [JsonIgnore]
        public Customer Customers { get; set; }
        public OrderReceipt OReceipt { get; set; }
    }
}
