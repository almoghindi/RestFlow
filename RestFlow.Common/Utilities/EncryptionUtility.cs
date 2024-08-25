using System;
using System.Text;

namespace RestFlow.Common.Utilities
{
    public static class EncryptionUtility
    {
        private static readonly string Key = "12345678901234567890123456789012"; // 32-character key

        public static string Encrypt(string input)
        {
            return XorCipher(input, Key);
        }

        public static string Decrypt(string input)
        {
            return XorCipher(input, Key);
        }

        private static string XorCipher(string input, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append((char)(input[i] ^ key[i % key.Length]));
            }
            return result.ToString();
        }
    }
}