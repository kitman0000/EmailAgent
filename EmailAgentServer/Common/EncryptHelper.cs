using System.Security.Cryptography;
using System.Text;

namespace EmailAgentServer.Common;

public class EncryptHelper
{ 
    static byte[] aesIV = Encoding.UTF8.GetBytes("zvb%s^uytS^BCs@B");
    
    public static string MD5Hash(string text,string key)
    {
        return MD5Hash(text + key);
    }  
    
    public static string MD5Hash(string text)  
    {  
        MD5 md5 = new MD5CryptoServiceProvider();  

        //compute hash from the bytes of text  
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));  
  
        //get hash result after compute it  
        byte[] result = md5.Hash;  

        StringBuilder strBuilder = new StringBuilder();  
        foreach (var t in result)
        {
            //change it into 2 hexadecimal digits  
            //for each byte  
            strBuilder.Append(t.ToString("x2"));
        }  

        return strBuilder.ToString();  
    }  
    
    public static string AesEncryptString(string plainText, string key)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("key");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = aesIV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return Convert.ToBase64String(encrypted);
    }

    public static string AesDecryptString(string cipherText, string key)
    {
        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;
        var cipherTextBytes = Convert.FromBase64String(cipherText);
        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);;
            aesAlg.IV = aesIV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }

    public static string CreateRandomKey(int length)
    {
        var random = new Random();

        var stringBuilder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(Convert.ToChar(random.Next('!', 'z' + 1)));
        }

        return stringBuilder.ToString();
    }

}