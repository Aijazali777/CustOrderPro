using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Models
{
    public class OrderReceipt
    {
        public int OrderReceiptId { get; set; }
        public string ReceiptDetails { get; set; }
        public int OrderId { get; set; }
        public Order POrder { get; set; }
    }
}
