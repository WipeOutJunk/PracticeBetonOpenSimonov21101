using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Supplier
    {
        public Supplier()
        {
            Deliveries = new HashSet<Delivery>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
