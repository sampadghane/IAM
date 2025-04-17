using System.Security.Cryptography;
using System.Text;

namespace AccessManagement.security
{
    public class RSADecryptPassword
    {
        public static string DecryptPassword(string encryptedPassword, string privateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
                byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(encryptedPassword), RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
