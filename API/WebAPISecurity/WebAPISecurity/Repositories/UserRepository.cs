using System.Collections.Generic;
using System.Linq;
using WebAPISecurity.Models;

namespace WebAPISecurity.Repositories
{
    public class UserRepository
    {
        private static List<User> users = new List<User>
        {
            new User { UserId = 1, Username = "user1", Password = "password1", Email = "user1@example.com" },
            new User { UserId = 2, Username = "user2", Password = "password2", Email = "user2@example.com" },
            new User { UserId = 3, Username = "user3", Password = "password3", Email = "user3@example.com" }
        };

        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        public List<User> GetAllUsers()
        {
            return users;
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The user ID to retrieve data.</param>
        /// <returns>Returns the user with the specified ID, or null if not found.</returns>
        public User GetUserById(int userId)
        {
            return users.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>Returns true if the user was added successfully, false otherwise.</returns>
        public bool AddUser(User user)
        {
            if (user == null || users.Any(u => u.UserId == user.UserId))
            {
                return false; // Avoid null user or duplicate UserId
            }

            users.Add(user);
            return true;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="updatedUser">The updated user data.</param>
        /// <returns>Returns true if the user was updated successfully, false otherwise.</returns>
        public bool UpdateUser(User updatedUser)
        {
            if (updatedUser == null)
            {
                return false;
            }

            User existingUser = GetUserById(updatedUser.UserId);
            if (existingUser != null)
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Password = updatedUser.Password;
                existingUser.Email = updatedUser.Email;
                return true;
            }

            return false; // User not found
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="userId">The user ID to delete.</param>
        /// <returns>Returns true if the user was deleted successfully, false otherwise.</returns>
        public bool DeleteUser(int userId)
        {
            User userToDelete = GetUserById(userId);
            if (userToDelete != null)
            {
                users.Remove(userToDelete);
                return true;
            }

            return false; // User not found
        }
    }
}
