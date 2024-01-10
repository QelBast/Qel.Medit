using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging;
using Qel.Medit.Dal;

namespace Qel.Medit.Addons.Services;

public class AuthService(DbContextMain dbContext)
{
    private readonly DbContextMain _dbContext = dbContext;

    public void RegisterUser(string userName, string password)
    {
        // Hash the password (use a secure hashing algorithm)
        string hashedPassword = BC.HashPassword(password);

        // Add the user to the database
        _dbContext.Users.Add(new Dal.Entities.User 
        { 
            Id = Guid.NewGuid(),
            UserName = userName,
            PasswordHash = hashedPassword
        });
        _dbContext.SaveChanges();
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

    private static bool ValidatePassword(string password, string hashedPassword)
    {
        // Implement password validation logic here
        return BC.Verify(password, hashedPassword);
    }
}
