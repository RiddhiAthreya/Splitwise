using Splitwise.DTO;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Services
{
    public class ExactExpense
    {
        private readonly UserService userService;

        public ExactExpense(UserService userService)
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

        private void CalculateSplits(ExpenseDTO equalExpenseDTO, Expense expense)
        {
            for (int i = 0; i < equalExpenseDTO.PaidTo.Count; i++)
            {
                User user = userService.getUser(equalExpenseDTO.PaidTo[i]);
                Split split = new Split(user, equalExpenseDTO.AmountSplit[i]);
                expense.Splits.Add(split);
            }
        }
    }
}
