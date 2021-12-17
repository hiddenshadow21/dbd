using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RavenDB.Tryout.App
{
    class Program
    {
        private static readonly Random _random = new Random();
        
        static void Main(string[] args)
        {

            // 1. Create document in collection: TermsAndConditions
            
            //TODO
            
            // 2. Create attachment for newly created document (256 KB in size)

            //TODO:

            // 3. Calculate md5 of attachment

            // TODO

            // 4. Modify attachment 2 times (using different random generated data) and save
            // calculate md5 of each new attachment and store

            //TODO

            // 5. Download 3 versions of document and attachments 
            // compare md5 with stored one

            // TODO
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            var result = new byte[length];
            _random.NextBytes(result);
            return result;
        }

        private static string ComputeHash(byte[] input)
        {
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(input);
                StringBuilder sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
