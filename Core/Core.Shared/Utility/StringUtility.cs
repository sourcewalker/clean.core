using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Core.Shared.Utility
{
    public static class StringUtility
    {
        public static string Md5HashEncode(string input)
        {
            string hash;
            using (var md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, input);
            }
            return hash;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            var md5Hash = MD5.Create();

            // Hash the input.
            var hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

        public static string SmartTrim(string data)
        {
            return Regex.Replace(data, @"[^a-zA-Z0-9]", String.Empty);
        }

        public static string NormalHash(string text)
        {

            return CheckSum.GenerateChecksum(text);
        }

        public static string SanitizeHash(string text)
        {
            var sanitizeText = SanitizeText(text);
            return CheckSum.GenerateChecksum(sanitizeText);
        }

        public static string SanitizeText(string text)
        {
            string sanitizeText = text.ToLower();

            sanitizeText = RemoveDiacritics(sanitizeText);

            sanitizeText = Regex.Replace(sanitizeText, @"[^a-zA-Z0-9]", string.Empty);

            return sanitizeText;
        }

        public static string SanitizeText(string text, string ReplaceSpaceWith)
        {
            string sanitizeText = text.ToLower();

            sanitizeText = RemoveDiacritics(sanitizeText);

            sanitizeText = Regex.Replace(sanitizeText, @"[^a-zA-Z0-9]", " ");
            sanitizeText = Regex.Replace(sanitizeText, @"\s{2,}", " ", RegexOptions.Singleline);
            sanitizeText = sanitizeText.Trim().Replace(" ", "-");
            return sanitizeText;
        }

        public static string RemoveDiacritics(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (char ch in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string SerializeObject<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static string RemoveDiacriticsManual(string input)
        {
            char[] replacement = { 'a', 'a', 'a', 'a', 'a', 'a', 'c', 'e', 'e', 'e', 'e', 'i', 'i', 'i', 'i', 'n', 'o', 'o', 'o', 'o', 'o', 'u', 'u', 'u', 'u', 'y', 'y' };
            char[] accents = { 'à', 'á', 'â', 'ã', 'ä', 'å', 'ç', 'é', 'è', 'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ñ', 'ò', 'ó', 'ô', 'ö', 'õ', 'ù', 'ú', 'û', 'ü', 'ý', 'ÿ' };

            for (int i = 0; i < accents.Length; i++)
            {
                input = input.Replace(accents[i], replacement[i]);
            }
            return input;
        }

        public static string ToPascalCase(string value)
        {
            char[] array = value.ToCharArray();

            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return (new string(array)).Replace(" ", string.Empty);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string ReplaceCharacter(string value, string finder, string replacer)
        {
            return !string.IsNullOrWhiteSpace(value) ? value.Replace(finder, replacer).Trim() : string.Empty;
        }

        public static string HttpToHttps(string value)
        {
            return Regex.Replace(value, "^http", "https");
        }
    }
}
