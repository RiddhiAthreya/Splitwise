using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Splitwise.Constants;
using Splitwise.DTO;
using Splitwise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController: ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly UserService userService;
        private readonly ExpenseManager expenseManager;

        public ExpenseController(ILogger<ExpenseController> logger, UserService userService, ExpenseManager expenseManager)
        {
            _logger = logger;
            this.userService = userService;
            this.expenseManager = expenseManager;
        }
        [HttpPost("AddExpense")]
        public async Task<IActionResult> AddExpense([FromQuery] ExpenseType expenseType, [FromBody] ExpenseDTO expenseDTO)
        {
            expenseManager.addExpense(expenseType, expenseDTO);
            return Ok();
        }

        [HttpGet("ShowBalance")]
        public async Task<IActionResult> ShowBalance([FromQuery] String userName)
        {
            String result = expenseManager.ShowBalance(userName);
            return Ok(result);
        }

        [HttpGet("SettleBalance")]
        public async Task<IActionResult> SettleBalance([FromQuery] String userName)
        {
            expenseManager.SettleExpense(userName);
            return Ok();
        }
    }
}
