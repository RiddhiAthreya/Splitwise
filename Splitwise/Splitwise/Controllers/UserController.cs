using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Splitwise.Constants;
using Splitwise.DTO;
using Splitwise.Models;
using Splitwise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService userService;
        private readonly ExpenseManager expenseManager;

        public UserController(ILogger<UserController> logger, UserService userService, ExpenseManager expenseManager)
        {
            _logger = logger;
            this.userService = userService;
            this.expenseManager = expenseManager;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            userService.AddUser(user);
            return Ok();
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = userService.getUsers();
            return Ok(result);
        }
    }
}
