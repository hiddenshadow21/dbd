using System.Linq;
using Raven.Client.Documents.Indexes;
using RavenDB.Tryout.App.Models;

namespace RavenDB.Tryout.App
{
    public class Employees_ByFirstNameAndLastName : AbstractIndexCreationTask<Employee, Employees_ByFirstNameAndLastName.Result>
    {
        public class Result
        {
            public string FullName { get; set; }
            public int WorkingYears { get; set; }
        }

        public Employees_ByFirstNameAndLastName()
        {
            Map = employees => from employee in employees
                select new Result
                {
                    FullName = employee.FirstName + " " + employee.LastName,
                    WorkingYears = (int)(employee.HiredAt - employee.Birthday).TotalDays/365
                };
            Stores.Add(x=> x.FullName, FieldStorage.Yes);
            Stores.Add(x=>x.WorkingYears, FieldStorage.Yes);
        }
    }
    
    public class People_Search :
        AbstractMultiMapIndexCreationTask<People_Search.Result>
    {
        public class Result
        {
            public string SourceId { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }

        public People_Search()
        {
            AddMap<Company>(companies =>
                from company in companies
                select new Result
                {
                    SourceId = company.Id,
                    Name = company.Contact.Name,
                    Type = "Company's contact"
                }
            );

            AddMap<Supplier>(suppliers =>
                from supplier in suppliers
                select new Result
                {
                    SourceId = supplier.Id,
                    Name = supplier.Contact.Name,
                    Type = "Supplier's contact"
                }
            );

            AddMap<Employee>(employees =>
                from employee in employees
                select new Result
                {
                    SourceId = employee.Id,
                    Name = $"{employee.FirstName} {employee.LastName}",
                    Type = "Employee"
                }
            );

            Index(entry => entry.Name, FieldIndexing.Search);

            Store(entry => entry.SourceId, FieldStorage.Yes);
            Store(entry => entry.Name, FieldStorage.Yes);
            Store(entry => entry.Type, FieldStorage.Yes);
        }
    }
}