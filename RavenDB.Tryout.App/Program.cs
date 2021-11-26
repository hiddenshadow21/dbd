using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using RavenDB.Tryout.App.Models;

namespace RavenDB.Tryout.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            //new Employees_ByFirstNameAndLastName().Execute(DocumentStoreHolder.Store);
            //new People_Search().Execute(DocumentStoreHolder.Store);
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var employees = session
                    .Query<Employees_ByFirstNameAndLastName.Result, Employees_ByFirstNameAndLastName>()
                    .Where(x => x.WorkingYears >= 30)
                    .Select(x=> new
                    {
                        x.FullName,
                        x.WorkingYears
                    })
                    .ToList();
                
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.FullName);
                }
            }
            
            Console.Title = "Multi-map sample";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                while (true)
                {
                    Console.Write("\nSearch terms: ");
                    var searchTerms = Console.ReadLine();
                    if(string.IsNullOrEmpty(searchTerms))
                        break;
                    foreach (var result in Search(session, searchTerms))
                    {
                        Console.WriteLine($"{result.SourceId}\t{result.Type}\t{result.Name}");
                    }
                }
            }
        }
        
        public static IEnumerable<People_Search.Result> Search(
            IDocumentSession session,
            string searchTerms
        )
        {
            var results = session.Query<People_Search.Result, People_Search>()
                .Search(
                    r => r.Name,
                    searchTerms
                )
                .ProjectInto<People_Search.Result>()
                .ToList();

            return results;
        }
    }
}
