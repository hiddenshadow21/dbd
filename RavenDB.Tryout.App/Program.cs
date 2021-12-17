using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Attachments;

namespace RavenDB.Tryout.App
{
    class Program
    {
        private static readonly Random _random = new Random();
        
        static void Main(string[] args)
        {

            // 1. Create document in collection: TermsAndConditions
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                /*session.Store(new TermsAndConditions
                {
                    Name = "test"
                });
                session.SaveChanges();*/


                // 2. Create attachment for newly created document (256 KB in size)
                
                var attStream = GenerateRandomBytes(256);
                session.Advanced.Attachments.Store("TermsAndConditions/1-A","test",new MemoryStream(attStream));

                // 3. Calculate md5 of attachment

                var hash = MD5.HashData(attStream);
                session.SaveChanges();
                
                // 4. Modify attachment 2 times (using different random generated data) and save
                // calculate md5 of each new attachment and store
                attStream = GenerateRandomBytes(256);
                hash = MD5.HashData(attStream);
                session.Advanced.Attachments.Store("TermsAndConditions/1-A", "test", new MemoryStream(attStream));
                session.SaveChanges();
                
                attStream = GenerateRandomBytes(256);
                hash = MD5.HashData(attStream);
                session.Advanced.Attachments.Store("TermsAndConditions/1-A", "test", new MemoryStream(attStream));
                session.SaveChanges();
                // 5. Download 3 versions of document and attachments 
                // compare md5 with stored one

                var revisions = session.Advanced.Revisions.GetFor<TermsAndConditions>("TermsAndConditions/1-A");
            }
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
