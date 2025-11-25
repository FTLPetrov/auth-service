using Microsoft.AspNetCore.Identity;

namespace Server.Persistence.Identity;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
