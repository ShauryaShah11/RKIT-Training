using DebuggingInVisualStudio.Models;
using System.Collections.Generic;
using System.Linq;

namespace DebuggingInVisualStudio.Repositories
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>
        {
            new User{Id = 1, Name = "user1", Email = "user1@example.com", Password = "Password123" },
            new User{Id = 2, Name = "user2", Email = "user2@example.com", Password = "Password123" },
            new User{Id = 3, Name = "user3", Email = "user3@example.com", Password = "Password123" },
            new User{Id = 4, Name = "user4", Email = "user4@example.com", Password = "Password123" },
            //new User{Id = 5, Name = "user5", Email = "user5@example.com", Password = "Password123" },
        };

        public List<User> GetAllUser()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public bool AddUser(User user)
        {
            if (user == null) return false;
            if (_users.Any(u => u.Id == user.Id)) return false;

            _users.Add(user);
            return true;
        }

        public bool UpdateUser(User user)
        {
            if (user == null)
            {
                return false;
            }
            User existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                return true;
            }
            return false;

        }

        public bool DeleteUser(int id)
        {
            User user = GetUserById(id);
            if (user != null)
            {
                _users.Remove(user);
                return true;
            }
            return false;
        }

    }
}