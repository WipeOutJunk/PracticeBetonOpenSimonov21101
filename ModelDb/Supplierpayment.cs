using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Supplierpayment
    {
        public int SupplierPaymentId { get; set; }
        public int DeliveryId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Delivery Delivery { get; set; } = null!;
    }
}
