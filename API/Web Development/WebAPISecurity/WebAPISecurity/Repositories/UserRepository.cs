using System.Collections.Generic;
using System.Linq;
using WebAPISecurity.Models;

namespace WebAPISecurity.Repositories
{
    /// <summary>
    /// The UserRepository class provides CRUD (Create, Read, Update, Delete) operations 
    /// for managing users in an in-memory collection. It serves as a simple data repository
    /// for storing and retrieving user information, simulating database operations without
    /// actual database interaction. The class includes methods to get all users, get a user by 
    /// ID, add a new user, update an existing user, and delete a user by ID.
    /// </summary>
    public class UserRepository
    {
        #region Private Members
        /// <summary>
        /// Sample list of users acting as an in-memory database
        /// </summary>
        private static List<User> users = new List<User>
        {
            new User { UserId = 1, Username = "user1", Password = "password1", Email = "user1@example.com" },
            new User { UserId = 2, Username = "user2", Password = "password2", Email = "user2@example.com" },
            new User { UserId = 3, Username = "user3", Password = "password3", Email = "user3@example.com" }
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
            return users;
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The user ID to retrieve data.</param>
        /// <returns>Returns the user with the specified ID, or null if not found.</returns>
        public User GetUserById(int userId)
        {
            // Finds and returns the user by their UserId, or null if not found
            return users.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>Returns true if the user was added successfully, false otherwise.</returns>
        public bool AddUser(User user)
        {
            // Check for null user or duplicate UserId before adding
            if (user == null || users.Any(u => u.UserId == user.UserId))
            {
                return false; // Cannot add null user or user with an existing UserId
            }

            // Adds the user to the list
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
                existingUser.Username = updatedUser.Username;
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
                users.Remove(userToDelete);
                return true; // Successfully deleted
            }

            return false; // User not found, delete failed
        }
        #endregion
    }
}
