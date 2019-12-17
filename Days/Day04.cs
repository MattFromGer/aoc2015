using System.Security.Cryptography;
using System.Text;
using ClassLib.util;

namespace ClassLib
{
    public class Day04 : AocDay
    {
        public int GetLowestPositiveNumber(string expectedStartOfHash)
        {
            var secretKey = Input[0];
            int i = 0;
            
            using (MD5 md5Hash = MD5.Create())
            {
                var hash = "";
                do
                {
                    i++;
                    hash = GetMd5Hash(md5Hash, secretKey + i);
                } while (hash.Substring(0, expectedStartOfHash.Length) != expectedStartOfHash);
            }

            return i;
        }
        
        /// <summary>
        /// Taken from: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?view=netcore-3.1
        /// </summary>
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (var b in data)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}