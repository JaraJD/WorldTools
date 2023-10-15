using System.Security.Cryptography;
using System.Text;

namespace WorldTools.API.Utils.Encrypt
{
    public class PasswordEncryption
    {
        //public static string GenerateSalt()
        //{
        //    byte[] saltBytes = new byte[16];
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(saltBytes);
        //    }
        //    return Convert.ToBase64String(saltBytes);
        //}

        //public static string EncryptPassword(string password, string salt)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        byte[] saltBytes = Convert.FromBase64String(salt);
        //        byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);

        //        byte[] hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
        //        return Convert.ToBase64String(hashedPasswordBytes);
        //    }
        //}

        //public static bool VerifyPassword(string enteredPassword, string salt, string storedHash)
        //{
        //    string enteredPasswordHash = EncryptPassword(enteredPassword, salt);
        //    return enteredPasswordHash == storedHash;
        //}
    }
}
