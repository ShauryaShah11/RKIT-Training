using System.Security.Claims;

namespace WebAPIFinalDemo.Services
{
    /// <summary>
    /// Provides methods for extracting user claims from a ClaimsPrincipal.
    /// </summary>
    public static class UserClaimService
    {
        /// <summary>
        /// Retrieves the username (subject) claim from the ClaimsPrincipal.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal containing the user's claims.</param>
        /// <returns>The username (sub) claim value or null if not found.</returns>
        public static string GetUsername(ClaimsPrincipal user)
        {
            return user?.FindFirst("sub")?.Value;
        }

        /// <summary>
        /// Retrieves the email claim from the ClaimsPrincipal.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal containing the user's claims.</param>
        /// <returns>The email claim value or null if not found.</returns>
        public static string GetEmail(ClaimsPrincipal user)
        {
            return user?.FindFirst("email")?.Value;
        }

        /// <summary>
        /// Retrieves the user ID claim from the ClaimsPrincipal and parses it to an integer.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal containing the user's claims.</param>
        /// <returns>The user ID as an integer or 0 if the claim is not found or cannot be parsed.</returns>
        public static int GetUserId(ClaimsPrincipal user)
        {
            string userIdValue = user?.FindFirst("userId")?.Value;
            return int.TryParse(userIdValue, out int userId) ? userId : 0;
        }
    }
}
