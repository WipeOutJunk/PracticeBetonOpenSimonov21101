using System;
using System.Collections.Generic;

namespace PracticeBetonNetV
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
