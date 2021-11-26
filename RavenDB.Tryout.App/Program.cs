using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using RavenDB.Tryout.App.Models;

namespace RavenDB.Tryout.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Z1();
            Z3();
            Z5();
        }

        private static void Z3()
        {
            List<Employee> filteredEmployees;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {

                string country = "set";
                var filteredQuery = session.Query<Employee>()
                    .Where(x =>
                        x.FirstName.In("Anne", "John") ||

                        (x.Address.Country == country &&
                         x.Territories.Count > 2 &&
                         x.Title.StartsWith("Sales")));

                filteredEmployees = filteredQuery.ToList();
            }
        }

        private static void Z5()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var projectedQueryWithFunctions = from employee in session.Query<Employee>()

                    let formatTitle = (Func<Employee, string>)(e => "Title: " + e.Title)
                    let formatName = (Func<Employee, string>)(e => "Name: " + e.FirstName + " " + e.LastName)

                    select new EmployeeDetails
                    {
                        Title = formatTitle(employee),
                        Name = formatName(employee)
                    };

                var projectedResults = projectedQueryWithFunctions.ToList();
                foreach (var projectedResult in projectedResults)
                {
                    
                    Console.WriteLine(projectedResult.Title);
                }
            }
        }

        private static void Z1()
        {
            int companyId = 13;
            string companyReference = $"companies/{companyId}-A";

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var orders = session.Advanced.RawQuery<Order>(
                        "from Orders " +
                        "where Company== $companyId " +
                        "include Company"
                    ).AddParameter("companyId", companyReference)
                    .ToList();

                var company = session.Load<Company>(companyReference);

                if (company == null)
                {
                    Console.WriteLine("Company not found.");
                    return;
                }

                Console.WriteLine($"Orders for {company.Name}");

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id} - {order.OrderedAt}");
                }
            }
        }
    }

    internal class EmployeeDetails
    {
        public string Title { get; set; }
        public string Name { get; set; }
    }
}
