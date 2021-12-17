using System;
using System.Security.Cryptography.X509Certificates;
using Raven.Client.Documents;

namespace RavenDB.Tryout.App
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { "https://a.qa-tests.development.run/" },
                    Database = "CRUD",
                    Certificate = new X509Certificate2(@"C:\Users\Bartosz Piekarski\Downloads\qa-tests.Cluster.Settings\admin.client.certificate.qa-tests.pfx")
                };

                return store.Initialize();
            });

        public static IDocumentStore Store => LazyStore.Value;
    }
}