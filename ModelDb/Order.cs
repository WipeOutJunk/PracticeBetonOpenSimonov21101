using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Order
    {
        public Order()
        {
            Materialtransactions = new HashSet<Materialtransaction>();
            Payments = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Quantity { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public string Status { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Materialtransaction> Materialtransactions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
