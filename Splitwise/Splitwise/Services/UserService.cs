using Splitwise.DTO;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Services
{
    public class UserService
    {
        Dictionary<String, User> users = new Dictionary<string, User>();
        public  User getUser(String userName)
        {
            return users[userName];
        }

        public void AddUser(User user)
        {
            users.Add(user.UserName, user);
        }

        public List<String> getUsers()
        {
            List<String> userNames = new List<string>();
            foreach (KeyValuePair<String, User> user in users)
            {
                userNames.Add(user.Key);
            }
            return userNames;
        }

        
    }
}
