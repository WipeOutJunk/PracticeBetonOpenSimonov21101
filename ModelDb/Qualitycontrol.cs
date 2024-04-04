using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Qualitycontrol
    {
        public int QcId { get; set; }
        public int ProductId { get; set; }
        public DateOnly CheckDate { get; set; }
        public string Result { get; set; } = null!;
        public string? Notes { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
