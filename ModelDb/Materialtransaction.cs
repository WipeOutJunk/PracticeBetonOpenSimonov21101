using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Materialtransaction
    {
        public int TransactionId { get; set; }
        public int InventoryId { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string TransactionType { get; set; } = null!;
        public decimal Quantity { get; set; }
        public int? RelatedOrderId { get; set; }

        public virtual Materialinventory Inventory { get; set; } = null!;
        public virtual Order? RelatedOrder { get; set; }
    }
}
