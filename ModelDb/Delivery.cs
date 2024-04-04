﻿using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Delivery
    {
        public Delivery()
        {
            Supplierpayments = new HashSet<Supplierpayment>();
        }

        public int DeliveryId { get; set; }
        public int MaterialId { get; set; }
        public int SupplierId { get; set; }
        public DateTime DeliveryTime { get; set; }

        public virtual Material Material { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<Supplierpayment> Supplierpayments { get; set; }
    }
}
