using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
