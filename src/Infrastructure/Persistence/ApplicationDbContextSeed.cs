using CleanArchiTemplate.Domain.Entities;
using CleanArchiTemplate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchiTemplate.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            // if (!context.Movies.Any())
            // {
            //     context.Movies.Add(new Movie
            //     {
            //         Title = "Shopping",
            //         Url = "notaurl.com",
            //         Content = "blah",
            //         Popularity = 1,
            //         Emotion = "happy",
            //         Rating = 5
            //     });
            //     context.Movies.Add(new Movie
            //     {
            //         Title = "Action",
            //         Url = "notarealurl.com",
            //         Content = "blahblah",
            //         Popularity = 2,
            //         Emotion = "excited",
            //         Rating = 4
            //     });

                 await context.SaveChangesAsync();
            // }
        }
    }
}
