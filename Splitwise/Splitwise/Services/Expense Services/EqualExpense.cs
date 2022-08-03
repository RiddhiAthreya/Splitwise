using Splitwise.DTO;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Services
{
    public class EqualExpense 
    {
        private readonly UserService userService;

        public EqualExpense(UserService userService)
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
            int noOfUsers = equalExpenseDTO.PaidTo.Count;
            double splitAmount = equalExpenseDTO.Amount / noOfUsers;
            for(int i = 0; i < noOfUsers; i++)
            {
                User user = userService.getUser(equalExpenseDTO.PaidTo[i]);
                Split split = new Split(user, splitAmount);
                expense.Splits.Add(split);
            }
        }
    }
}
