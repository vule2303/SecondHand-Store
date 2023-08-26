using Microsoft.AspNetCore.Identity;

namespace SecondHand.Models.Domain
{

    public static class UserManagerExtensions
    {
        public static async Task<string> GetFirstNameAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
           return await userManager.GetClaimValueAsync(user, "FirstName");
        }

        public static async Task<string> GetLastNameAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            return await userManager.GetClaimValueAsync(user, "LastName");
        }

        private static async Task<string> GetClaimValueAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string claimType)
        {
            var claims = await userManager.GetClaimsAsync(user);
            var claim = claims.FirstOrDefault(c => c.Type == claimType);

            return claim?.Value;
        }
    }
}