using Splitwise.Constants;
using Splitwise.DTO;
using Splitwise.Models;
using Splitwise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise
{
    public class ExpenseManager
    {
        private List<Expense> expenses;
        private Dictionary<String, Dictionary<String, Double>> balanceSheet;
        private readonly UserService userService;
        private readonly EqualExpense equalExpense;
        private readonly ExactExpense exactExpense;
        private readonly PercentageExpense percentageExpense;


        public ExpenseManager(UserService userService, EqualExpense equalExpense, ExactExpense exactExpense, PercentageExpense percentageExpense)
        {
            this.expenses = new List<Expense>();
            this.balanceSheet = new Dictionary<String, Dictionary<String, Double>>();
            this.userService = userService;
            this.equalExpense = equalExpense;
            this.exactExpense = exactExpense;
            this.percentageExpense = percentageExpense;
        }



        public void addExpense(ExpenseType expenseType, ExpenseDTO expenseDTO)
        {
            Expense expense;
            switch (expenseType)
            {
                case ExpenseType.EQUAL_EXPENSE:
                    expense = equalExpense.CreateExpense(expenseDTO);
                    break;
                case ExpenseType.EXACT_EXPENSE:
                    expense = exactExpense.CreateExpense(expenseDTO);
                    break;
                case ExpenseType.PERCENT_EXPENSE:
                    expense = percentageExpense.CreateExpense(expenseDTO);
                    break;
                default:
                    return;

            }
            expenses.Add(expense);
            foreach (Split split in expense.Splits)
            {
                String paidTo = split.User.UserName;
                if (!balanceSheet.ContainsKey(expense.PaidBy.UserName))
                {
                    balanceSheet.Add(expense.PaidBy.UserName, new Dictionary<String, double>());
                }
                Dictionary<String, Double> balance = balanceSheet[expense.PaidBy.UserName];
                if (!balance.ContainsKey(paidTo))
                {
                    balance.Add(paidTo, 0.0);

                }
                balance[paidTo] = balance[paidTo] + split.Amount;
                if (!balanceSheet.ContainsKey(paidTo))
                {
                    balanceSheet.Add(paidTo, new Dictionary<String, double>());
                }
                balance = balanceSheet[paidTo];

                if (!balance.ContainsKey(expense.PaidBy.UserName))
                {
                    balance.Add(expense.PaidBy.UserName, 0.0);

                }
                balance[expense.PaidBy.UserName] = balance[expense.PaidBy.UserName] - split.Amount;
            }

        }

        public void SettleExpense(String userName)
        {
            foreach (String userBalance in balanceSheet[userName].Keys.ToList())
            {
                if (balanceSheet[userName][userBalance] < 0)
                {
                    balanceSheet[userBalance].Remove(userName);
                    balanceSheet[userName].Remove(userBalance);
                }
            }
        }

        public String ShowBalance(String userName)
        {
            String result = "";
            bool isEmpty = true;
            foreach (KeyValuePair<String, Double> userBalance in balanceSheet[userName])
            {
                if (userBalance.Value != 0)
                {
                    isEmpty = false;
                    result += printBalance(userName,userBalance.Key, userBalance.Value);
                }
            }

            if (isEmpty)
                return "No balance";
            return result;

        }

        private String printBalance(String user1, String user2, double amount)
        {
            String result = "";
            if (amount < 0)
            {
                result += (user1 + " owes " + user2 + ": " + Math.Abs(amount)+"\n");
            }
            else if (amount > 0)
            {
                result +=(user2 + " owes " + user1 + ": " + Math.Abs(amount)+"\n");
            }
            return result;
        }
    }
}
