using CleanArchiTemplate.Domain.Entities;
using CleanArchiTemplate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
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
        //     //Seed, if necessary
        //     if (!context.Movies.Any())
        //     {
        //         Random r = new Random();

        //         string temp = "Batman";
        //         int pop = r.Next(0,100);
        //         int rat = r.Next(0,10);

        //         context.Movies.Add(new Movie
        //         {
        //             Title = temp,
        //             Url = temp + ".com",
        //             Content = temp + ".content",
        //             Popularity = pop,
        //             Emotion = temp + ".emotion",
        //             Rating = rat
        //         });

        //         temp = "Robin";
        //         pop = r.Next(0,100);
        //         rat = r.Next(0,10);

        //         context.Movies.Add(new Movie
        //         {
        //             Title = temp,
        //             Url = temp + ".com",
        //             Content = temp + ".content",
        //             Popularity = pop,
        //             Emotion = temp + ".emotion",
        //             Rating = rat
        //         });

        //         temp = "IceCube";
        //         pop = r.Next(0,100);
        //         rat = r.Next(0,10);

        //         context.Movies.Add(new Movie
        //         {
        //             Title = temp,
        //             Url = temp + ".com",
        //             Content = temp + ".content",
        //             Popularity = pop,
        //             Emotion = temp + ".emotion",
        //             Rating = rat
        //         });

        //         temp = "Joker";
        //         pop = r.Next(0,100);
        //         rat = r.Next(0,10);

        //         context.Movies.Add(new Movie
        //         {
        //             Title = temp,
        //             Url = temp + ".com",
        //             Content = temp + ".content",
        //             Popularity = pop,
        //             Emotion = temp + ".emotion",
        //             Rating = rat
        //         });

        //         temp = "CatWoman";
        //         pop = r.Next(0,100);
        //         rat = r.Next(0,10);

        //         context.Movies.Add(new Movie
        //         {
        //             Title = temp,
        //             Url = temp + ".com",
        //             Content = temp + ".content",
        //             Popularity = pop,
        //             Emotion = temp + ".emotion",
        //             Rating = rat
        //         });

        //         temp = "Dark Knight";
        //         pop = r.Next(0,100);
        //         rat = r.Next(0,10);

        //         context.Movies.Add(new Movie
        //         {
        //             Title = temp,
        //             Url = temp + ".com",
        //             Content = temp + ".content",
        //             Popularity = pop,
        //             Emotion = temp + ".emotion",
        //             Rating = rat
        //         });




                  await context.SaveChangesAsync();
        //    }
        }
    }
}
