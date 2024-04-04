using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
