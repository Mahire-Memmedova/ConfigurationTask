using Microsoft.AspNetCore.Identity;

namespace ConfigurationsTask.Entities.Auth;

public class AppUser:IdentityUser
{
    public string FirstName { get; set; }
}