using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Models
{
    public class Expense
    {
        public User PaidBy { get; set; }
        public double Amount { get; set; }
        public List<Split> Splits { get; set; }

        public Expense(User paidBy, double amount)
        {
            PaidBy = paidBy;
            Amount = amount;
            Splits = new List<Split>();
        }
    }
}
