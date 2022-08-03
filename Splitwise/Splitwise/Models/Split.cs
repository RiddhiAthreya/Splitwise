using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Models
{
    public class Split
    {
        public Split(User user, double amount)
        {
            User = user;
            Amount = amount;
        }

        public User User { get; set; }
        public double Amount { get; set; }
    }
}
