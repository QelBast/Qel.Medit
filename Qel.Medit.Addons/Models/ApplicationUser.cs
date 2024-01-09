namespace Qel.Medit.Addons.Models;

public class ApplicationUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    // Add other user properties as needed
}
