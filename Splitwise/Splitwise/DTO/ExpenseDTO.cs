using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DTO
{
    public class ExpenseDTO 
    {
        public String PaidBy { get; set; }
        public double Amount { get; set; }
        public List<double> AmountSplit { get; set; }
        public List<String> PaidTo { get; set; }
        public List<double> PercentageSplit { get; set; }

        public ExpenseDTO()
        {
        }


    }
}
