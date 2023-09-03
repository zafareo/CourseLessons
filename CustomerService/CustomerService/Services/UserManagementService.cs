using CustomerService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Services
{
    public class UserManagementService
    {
        private List<User> users;

        public UserManagementService()
        {
            users = new List<User>();
        }

        public User CreateUser(string username, string password)
        {
            int userId = GenerateUniqueId();

            User newUser = new User
            {
                Id = userId,
                Username = username,
                Password = password
            };

            users.Add(newUser);

            return newUser;
        }

        public User AuthenticateUser(string username, string password)
        {
            User? user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public void UpdateUserProfile(User user)
        {

            User? existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
            }
        }

        private int GenerateUniqueId()
        {
            int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
            return maxId + 1;
        }


    }
}
