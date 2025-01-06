using System.Collections.Generic;
using System.Linq;
using WebAPIFinalDemo.Models;

namespace WebAPIFinalDemo.Repositories
{
    public class UserRepository
    {
        #region Private Members
        /// <summary>
        /// Sample list of users acting as an in-memory database
        /// </summary>
        private static List<User> _users = new List<User>
        {
            new User
            {
                UserId = 1,
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                Password = "Password123", // Ensure this is hashed in real applications
            },
            new User
            {
                UserId = 2,
                Name = "Bob Smith",
                Email = "bob.smith@example.com",
                Password = "Password123", // Ensure this is hashed in real applications
            },
            new User
            {
                UserId = 3,
                Name = "Charlie Davis",
                Email = "charlie.davis@example.com",
                Password = "Password123", // Ensure this is hashed in real applications
            },
            new User
            {
                UserId = 4,
                Name = "David Lee",
                Email = "david.lee@example.com",
                Password = "Password123", // Ensure this is hashed in real applications
            },
            new User
            {
                UserId = 5,
                Name = "Eve White",
                Email = "eve.white@example.com",
                Password = "Password123", // Ensure this is hashed in real applications
            }
        };
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        public List<User> GetAllUsers()
        {
            // Returns the complete list of users
            return _users;
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The user ID to retrieve data.</param>
        /// <returns>Returns the user with the specified ID, or null if not found.</returns>
        public User GetUserById(int userId)
        {
            // Finds and returns the user by their UserId, or null if not found
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>Returns true if the user was added successfully, false otherwise.</returns>
        public bool AddUser(User user)
        {
            // Check for null user or duplicate UserId before adding
            if (user == null || _users.Any(u => u.UserId == user.UserId))
            {
                return false; // Cannot add null user or user with an existing UserId
            }

            // Adds the user to the list
            _users.Add(user);
            return true;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="updatedUser">The updated user data.</param>
        /// <returns>Returns true if the user was updated successfully, false otherwise.</returns>
        public bool UpdateUser(User updatedUser)
        {
            // Ensure the updated user is not null
            if (updatedUser == null)
            {
                return false;
            }

            // Find the existing user by ID
            User existingUser = GetUserById(updatedUser.UserId);
            if (existingUser != null)
            {
                // Update the existing user's information
                existingUser.Name = updatedUser.Name;
                existingUser.Password = updatedUser.Password;
                existingUser.Email = updatedUser.Email;
                return true; // Successfully updated
            }

            return false; // User not found, update failed
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="userId">The user ID to delete.</param>
        /// <returns>Returns true if the user was deleted successfully, false otherwise.</returns>
        public bool DeleteUser(int userId)
        {
            // Find the user to delete by ID
            User userToDelete = GetUserById(userId);
            if (userToDelete != null)
            {
                // Remove the user from the list
                _users.Remove(userToDelete);
                return true; // Successfully deleted
            }

            return false; // User not found, delete failed
        }
        #endregion
    }
}