using System.Security.Claims;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");
        var claim = new Claim(type: "api1", value: "api1");


        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
            await roleManager.AddClaimAsync(administratorRole, claim);

        }

        var administrator = new ApplicationUser { UserName = "administrator@localhost.hr", Email = "administrator@localhost.hr" };

        

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary
        if (!context.TodoLists.Any())
        {
            context.TodoLists.Add(new TodoList
            {
                Title = "Shopping",
                Colour = Colour.Blue,
                Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
            });

            await context.SaveChangesAsync();
        }


        if (!context.TodoLists.Any())
        {
            context.TodoLists.Add(new TodoList
            {
                Title = "Shopping",
                Colour = Colour.Blue,
                Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
            });

            await context.SaveChangesAsync();
        }

        if (!context.MusicTracks.Any())
        {
            context.MusicTracks.Add(new MusicTrack { Title = "Sadness", Performer = "Enigma", Filename = "Enigma - Sadeness (Full Version).mp3", CoverPicture= "Enigma-Sadness.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Absloute Beginners", Performer = "David Bowie", Filename = "David Bowie - Absolute Beginners (Official Video).mp3", CoverPicture= "AbsoluteBeginners-DavidBowie.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Devotion", Performer = "Hurts ft Kylie Minogue", Filename = "Hurts ft Kylie Minogue - Devotion (Music Video).mp3" ,CoverPicture= "Devotion-Hurts.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Only Time", Performer = "Enya", Filename = "Enya - Only Time (Official 4K Music Video).mp3", CoverPicture = "OnlyTime-Enya.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Great Southern Land", Performer = "Icehouse", Filename = "Icehouse - Great Southern Land (_Young Einstein_ video clip).mp3", CoverPicture = "GreatSouthernLand-IceHouse.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Kids", Performer = "MGMT", Filename = "MGMT - Kids (Official HD Video).mp3", CoverPicture = "Kids-MGMT.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "In The Dark", Performer = "Purple Disco Machine", Filename = "Purple Disco Machine, ophie and the Giants - In The Dark (Official Music Video).mp3", CoverPicture = "InTheDark-PurpleDiscoMachine.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Rum na usnama", Performer = "Vatra feat. Božo Vrećo", Filename = "Vatra feat. Božo Vrećo - Rum na usnama (Official video).mp3", CoverPicture = "VatraBozoVrećo-RumNaUsnama.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Viva La Vida", Performer = "Gregorian", Filename = "Gregorian _Viva La Vida_ (Official Music Video) - Album out November 22nd.mp3", CoverPicture = "VivaLaVida-Gregorian.jpeg" });
            context.MusicTracks.Add(new MusicTrack { Title = "Najbolje za nas", Performer = "Pocket Palma", Filename = "Najbolje za nas (Fantom Remix).mp3", CoverPicture = "PockePalma-NajboljeZaNas.jpeg" });

            await context.SaveChangesAsync();
        }
    }
}
