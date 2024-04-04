using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Material
    {
        public Material()
        {
            Deliveries = new HashSet<Delivery>();
            Materialinventories = new HashSet<Materialinventory>();
        }

        public int MaterialId { get; set; }
        public string Name { get; set; } = null!;
        public string MeasureUnit { get; set; } = null!;
        public decimal PricePerUnit { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Materialinventory> Materialinventories { get; set; }
    }
}
