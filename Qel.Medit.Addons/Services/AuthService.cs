using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging;
using Qel.Medit.Dal;

namespace Qel.Medit.Addons.Services;

public class AuthService
{
    private readonly DbContextMain _dbContext;

    public AuthService(DbContextMain dbContext)
    {
        _dbContext = dbContext;
    }

    public void RegisterUser(string userName, string password)
    {
        // Hash the password (use a secure hashing algorithm)
        string hashedPassword = BC.HashPassword(password);

        // Add the user to the database
        _dbContext.Users.Add(new Medit.Dal.Entities.User 
        { 
            UserName = userName,
            PasswordHash = hashedPassword
        });
        _dbContext.SaveChanges();
    }

    // Hash password using a secure hashing algorithm (e.g., BCrypt)
    private string HashPassword(string password)
    {
        // Implement password hashing logic here
        // Use a library like BCrypt.Net for secure password hashing
        // Example: BCrypt.Net.BCrypt.HashPassword(password)
        throw new NotImplementedException();
    }
    public bool ValidateUserCredentials(string userName, string password)
    {

        var user = _dbContext.Users.SingleOrDefault(u => u.UserName == userName);

        // Check if the user exists and the provided password is correct
        if (user != null && ValidatePassword(password, user.PasswordHash))
        {
            return true;
        }

        return false;
    }

    private bool ValidatePassword(string password, string hashedPassword)
    {
        // Implement password validation logic here
        return BC.Verify(password, hashedPassword);
    }
}
