using System;
using System.Threading.Tasks;
using Raven.Client.Documents;
using RavenDB.Tryout.App.Models;

namespace RavenDB.Tryout.App
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { "http://localhost:8080" },
                    Database = "Zadanie2"
                };

                store.Conventions.RegisterAsyncIdConvention<History>(
                    (dbname, history) => Task.FromResult(string.Format("{0}/history", history.UserId)));

                return store.Initialize();
            });

        public static IDocumentStore Store => LazyStore.Value;
    }
}