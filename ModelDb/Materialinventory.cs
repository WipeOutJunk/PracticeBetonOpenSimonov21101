using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Materialinventory
    {
        public Materialinventory()
        {
            Materialtransactions = new HashSet<Materialtransaction>();
        }

        public int InventoryId { get; set; }
        public int MaterialId { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public decimal Quantity { get; set; }
        public string BatchNumber { get; set; } = null!;
        public DateOnly? ExpirationDate { get; set; }

        public virtual Material Material { get; set; } = null!;
        public virtual ICollection<Materialtransaction> Materialtransactions { get; set; }
    }
}
