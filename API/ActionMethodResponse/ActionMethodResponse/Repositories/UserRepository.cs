using ActionMethodResponse.Models; // Importing the User model
using System.Collections.Generic; // Importing the List collection
using System.Linq; // Importing LINQ for querying collections

namespace ActionMethodResponse.Repositories
{
    /// <summary>
    /// Repository class for managing user data.
    /// </summary>
    public class UserRepository
    {
        // Static list to store user data
        private static List<User> _users = new List<User>
        {
            new User
            {
                UserId = 1,
                UserName = "Alice Johnson",
                Location = "New York",
                PIN = 10001,
                Age = 30,
                Sex = "Female",
                Occupation = "Software Engineer",
                AnnualIncome = 85000.50f,
                MaritalStatus = "Single",
                Nationality = "American"
            },
            new User
            {
                UserId = 2,
                UserName = "Bob Smith",
                Location = "Los Angeles",
                PIN = 90001,
                Age = 40,
                Sex = "Male",
                Occupation = "Accountant",
                AnnualIncome = 70000.00f,
                MaritalStatus = "Married",
                Nationality = "American"
            },
            new User
            {
                UserId = 3,
                UserName = "Clara Barton",
                Location = "Chicago",
                PIN = 60601,
                Age = 28,
                Sex = "Female",
                Occupation = "Doctor",
                AnnualIncome = 95000.75f,
                MaritalStatus = "Single",
                Nationality = "American"
            },
            new User
            {
                UserId = 4,
                UserName = "David Lee",
                Location = "Houston",
                PIN = 77001,
                Age = 35,
                Sex = "Male",
                Occupation = "Civil Engineer",
                AnnualIncome = 80000.00f,
                MaritalStatus = "Married",
                Nationality = "American"
            },
            new User
            {
                UserId = 5,
                UserName = "Eva Green",
                Location = "Phoenix",
                PIN = 85001,
                Age = 32,
                Sex = "Female",
                Occupation = "Graphic Designer",
                AnnualIncome = 65000.25f,
                MaritalStatus = "Divorced",
                Nationality = "American"
            }
        };

        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        public List<User> GetAllUsers()
        {
            return _users;
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The user ID to retrieve data.</param>
        /// <returns>Returns the user with the specified ID, or null if not found.</returns>
        public User GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>Returns true if the user was added successfully, false otherwise.</returns>
        public bool AddUser(User user)
        {
            if (user == null || _users.Any(u => u.UserId == user.UserId))
            {
                return false; // Avoid null user or duplicate UserId
            }

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
            if (updatedUser == null)
            {
                return false;
            }

            User existingUser = GetUserById(updatedUser.UserId);
            if (existingUser != null)
            {
                existingUser.UserName = updatedUser.UserName;
                existingUser.Location = updatedUser.Location;
                existingUser.PIN = updatedUser.PIN;
                existingUser.Age = updatedUser.Age;
                existingUser.Sex = updatedUser.Sex;
                existingUser.Occupation = updatedUser.Occupation;
                existingUser.AnnualIncome = updatedUser.AnnualIncome;
                existingUser.MaritalStatus = updatedUser.MaritalStatus;
                existingUser.Nationality = updatedUser.Nationality;
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
                _users.Remove(userToDelete);
                return true;
            }

            return false; // User not found
        }
    }
}
