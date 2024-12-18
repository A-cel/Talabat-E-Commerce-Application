using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity
{
    public static class IdentityDataSeed
    {
        public static async Task IdentitySeedAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                var user = new AppUser() { DisplayName = "Ahmed Fathi", Email = "afathi@gmail.com", PhoneNumber = "01128844995", UserName = "AF_99" };
                await userManager.CreateAsync(user, "Aasd@#0123");
            }
        }
    }
}
