using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    public class Loan
    {
        public Loan(string user)
        {
            this.User = user;
            this.Borrowed = DateTime.Today;
        }

        public string User { get; set; }

        public DateTime Borrowed { get; set; }

        public DateTime Returned { get; set; }

        public void Return()
        {
            this.Returned = DateTime.Today;
        }
    }
}
