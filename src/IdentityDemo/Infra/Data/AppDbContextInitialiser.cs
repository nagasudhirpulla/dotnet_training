using IdentityDemo.Constants;
using IdentityDemo.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Infra.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<AppUser> store)
{
    public async Task InitialiseAsync()
    {
        try
        {
            // See https://jasontaylor.dev/ef-core-database-initialisation-strategies
            //await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        string[] appRoles = [Roles.Administrator, Roles.User];
        foreach (var role in appRoles)
        {
            if (roleManager.Roles.All(r => r.Name != role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Default users
        var administrator = new AppUser { UserName = "administrator", Email = "administrator@localhost", DisplayName = "Adminstrator" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            if (store is IUserEmailStore<AppUser> emailStore)
            {
                // confirm user's email programmatically
                await emailStore.SetEmailConfirmedAsync(administrator, true, CancellationToken.None);
            }
            if (!string.IsNullOrWhiteSpace(Roles.Administrator))
            {
                await userManager.AddToRolesAsync(administrator, [Roles.Administrator]);
            }
        }

        // Default data
        // Seed, if necessary
        //if (!_context.TodoLists.Any())
        //{
        //    _context.TodoLists.Add(new TodoList
        //    {
        //        Title = "Todo List",
        //        Items =
        //        {
        //            new TodoItem { Title = "Make a todo list 📃" },
        //            new TodoItem { Title = "Check off the first item ✅" },
        //            new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
        //        }
        //    });
        //
        //    await _context.SaveChangesAsync();
        //}
    }
}