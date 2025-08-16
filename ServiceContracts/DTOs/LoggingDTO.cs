using Entities;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

    public class LoggingDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes); // Store this in DB
        }
    }
    public RegisteredAccounts ToRegisteredAccount()
        {
        return new RegisteredAccounts() { AccountID = Guid.NewGuid(), Email = Email, HashedPassword=HashPassword(Password)};
        }
    }
