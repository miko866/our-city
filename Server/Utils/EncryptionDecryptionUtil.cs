using System.Security.Cryptography;
using System.Text;

namespace Server.Utils;

public static class EncryptionDecryptionUtil
{
    /// <summary>
    /// Encrypt
    /// </summary>
    /// <param name="encryptionKey"></param>
    /// <param name="clearText"></param>
    /// <returns></returns>
    private static string Encrypt(string encryptionKey, string clearText)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(clearText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    /// <summary>
    /// Decrypt
    /// </summary>
    /// <param name="encryptionKey"></param>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    private static string Decrypt(string encryptionKey, string cipherText)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }

    /// <summary>
    /// Encrypt into token
    /// </summary>
    /// <param name="encryptionKey"></param>
    /// <param name="passwordToken"></param>
    /// <param name="emailToken"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public static string EncryptIntoToken(string encryptionKey, string passwordToken, string emailToken, string email)
    {
        // base64 only does not do the trick always, so i just urlEncoded double
        string retval = $"{passwordToken}|||{emailToken}|||{email}";
        retval = System.Web.HttpUtility.UrlEncode(Base64Utility.UrlSafeEncode(Encrypt(encryptionKey, retval)));
        return retval;
    }

    /// <summary>
    /// Decrypt from token
    /// </summary>
    /// <param name="encryptionKey"></param>
    /// <param name="encryptedToken"></param>
    /// <returns></returns>
    public static string[] DecryptFromToken(string encryptionKey, string encryptedToken)
    {
        string decryptedToken = Decrypt(
            encryptionKey,
            Base64Utility.UrlSafeDecode(System.Web.HttpUtility.UrlDecode(encryptedToken))
        );
        string[] arr = decryptedToken.Split("|||");

        return arr;
    }
}
