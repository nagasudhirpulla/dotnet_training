using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Entities;

public class AppUser : IdentityUser
{
    public required string DisplayName { get; set; }
}