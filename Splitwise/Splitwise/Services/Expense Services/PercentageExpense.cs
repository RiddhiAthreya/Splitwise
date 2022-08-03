using Splitwise.DTO;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Services
{
    public class PercentageExpense 

    {
        private readonly UserService userService;

        public PercentageExpense(UserService userService)
        {
            this.userService = userService;
        }

        public Expense CreateExpense(ExpenseDTO expenseDTO)
        {
            User user = userService.getUser(expenseDTO.PaidBy);
            Expense expense = new Expense(user, expenseDTO.Amount);
            CalculateSplits(expenseDTO, expense);
            return expense;
        }

        private void CalculateSplits(ExpenseDTO percentageExpenseDTO, Expense expense)
        {
            for (int i = 0; i < percentageExpenseDTO.PaidTo.Count; i++)
            {
                User user = userService.getUser(percentageExpenseDTO.PaidTo[i]);
                double amount = ((percentageExpenseDTO.Amount * percentageExpenseDTO.PercentageSplit[i])) / 100;
                Split split = new Split(user, amount);
                expense.Splits.Add(split);
            }
        }
    }
}
