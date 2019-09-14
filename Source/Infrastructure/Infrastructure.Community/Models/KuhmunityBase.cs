using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Infrastructure.Community.Models
{
    public abstract class KuhmunityBase
    {
        protected string GetUserIdFromSessionTicket(string sessionTicket)
        {
            string[] components = sessionTicket.Split('|');
            string sessionToken = components[0];
            string userId = components[1];
            string persistentTicket = components[2];
            string dataHash = components[3];

            return userId;
        }

        protected string GetKuhmunityApiEndpoint(IConfiguration configuration)
        {
            return configuration["Milka:Kuhmunity:ApiEndpoint"];
        }

        public static Dictionary<string, string> ExtractCookieData(string cookieValue)
        {
            var cookieItems = new Dictionary<string, string>();
            var components = cookieValue.Split('&');

            foreach (string item in components)
            {
                var itemParts = item.Split('=');
                cookieItems.Add(itemParts[0], itemParts[1]);
            }

            return cookieItems;
        }

        public static string DecryptConsumerId(string encryptedConsumerId)
        {
            string decrypted;

            var keyBytes = Encoding.ASCII.GetBytes("$w33tCtx");
            byte[] bytesToDecrypt;
            try
            {
                var decodedCryptedData = HttpUtility.UrlDecode(encryptedConsumerId);
                if (!string.IsNullOrEmpty(decodedCryptedData))
                {
                    bytesToDecrypt = Convert.FromBase64String(decodedCryptedData);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                bytesToDecrypt = Convert.FromBase64String(encryptedConsumerId);
            }

            using (var cryptoProvider = new DESCryptoServiceProvider())
            {
                using (var memoryStream = new MemoryStream(bytesToDecrypt))
                {
                    using (
                        var cryptoStream = new CryptoStream(
                            memoryStream,
                            cryptoProvider.CreateDecryptor(keyBytes, keyBytes),
                            CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            decrypted = reader.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted;
        }

    }
}
