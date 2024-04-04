using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Productionsection
    {
        public Productionsection()
        {
            Products = new HashSet<Product>();
        }

        public int SectionId { get; set; }
        public string Name { get; set; } = null!;
        public string Function { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
