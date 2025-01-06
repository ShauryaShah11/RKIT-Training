using System.Collections.Generic;
using System.Linq;
using WebAPICaching.Models;

namespace WebAPICaching.Repositories
{
    /// <summary>
    /// Repository class for managing user data.
    /// </summary>
    public class UserRepository
    {
        #region Private Members
        /// <summary>
        /// It represents static in-memory database for users.
        /// </summary>
        private static List<User> users = new List<User>
        {
            new User { UserId = 1, Username = "user1", Password = "password1", Email = "user1@example.com" },
            new User { UserId = 2, Username = "user2", Password = "password2", Email = "user2@example.com" },
            new User { UserId = 3, Username = "user3", Password = "password3", Email = "user3@example.com" },
            new User { UserId = 4, Username = "user4", Password = "password4", Email = "user4@example.com" },
            new User { UserId = 5, Username = "user5", Password = "password5", Email = "user5@example.com" },
            new User { UserId = 6, Username = "user6", Password = "password6", Email = "user6@example.com" },
            new User { UserId = 7, Username = "user7", Password = "password7", Email = "user7@example.com" },
            new User { UserId = 8, Username = "user8", Password = "password8", Email = "user8@example.com" },
            new User { UserId = 9, Username = "user9", Password = "password9", Email = "user9@example.com" },
            new User { UserId = 10, Username = "user10", Password = "password10", Email = "user10@example.com" },
            new User { UserId = 11, Username = "user11", Password = "password11", Email = "user11@example.com" },
            new User { UserId = 12, Username = "user12", Password = "password12", Email = "user12@example.com" },
            new User { UserId = 13, Username = "user13", Password = "password13", Email = "user13@example.com" },
            new User { UserId = 14, Username = "user14", Password = "password14", Email = "user14@example.com" },
            new User { UserId = 15, Username = "user15", Password = "password15", Email = "user15@example.com" },
            new User { UserId = 16, Username = "user16", Password = "password16", Email = "user16@example.com" },
            new User { UserId = 17, Username = "user17", Password = "password17", Email = "user17@example.com" },
            new User { UserId = 18, Username = "user18", Password = "password18", Email = "user18@example.com" },
            new User { UserId = 19, Username = "user19", Password = "password19", Email = "user19@example.com" },
            new User { UserId = 20, Username = "user20", Password = "password20", Email = "user20@example.com" }
        };
        #endregion

        #region Public Methods
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
        #endregion
    }
}