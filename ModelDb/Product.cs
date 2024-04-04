using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
            Qualitycontrols = new HashSet<Qualitycontrol>();
        }

        public int ProductId { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; } = null!;
        public int ProductionTime { get; set; }
        public decimal Price { get; set; }

        public virtual Productionsection Section { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Qualitycontrol> Qualitycontrols { get; set; }
    }
}
