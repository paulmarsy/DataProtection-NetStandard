using System;

namespace DataProtection.Extensions
{
    public static class EncodingExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string ToBase64(this byte[] buffer, Base64FormattingOptions formatting = Base64FormattingOptions.InsertLineBreaks)
        {
            return Convert.ToBase64String(buffer, formatting);
        }

        public static byte[] FromBase64(this string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return new byte[] { };

            try
            {
                return Convert.FromBase64String(base64);
            }
            catch (SystemException e) when (e is ArgumentNullException || e is FormatException)
            {
                return new byte[] { };
            }
        }
    }
}