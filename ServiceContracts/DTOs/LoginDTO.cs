using Entities;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

public class LoginDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }

    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
    public RegisteredAccounts ToRegisteredAccount()
    {
        return new RegisteredAccounts() { AccountID = Guid.NewGuid(), Email = Email, HashedPassword=HashPassword(Password) };
    }
}
    public static class  LoginExtension
    {
        public static LoginDTO ToLoginDTO(this RegisteredAccounts registeredAccounts)
        {
            return new LoginDTO() { Email = registeredAccounts.Email, Password = registeredAccounts.HashedPassword };
        }
    }
    
