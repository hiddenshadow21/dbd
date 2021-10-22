using System;

namespace RavenDB.Tryout.App
{
    class Program
    {
        //#region Pogram 1 i 2
        //static void Main(string[] args)
        //{
        //    if (args.Length == 0)
        //    {
        //        Console.WriteLine("No argument provided.");
        //        return;
        //    }

        //    var arg = args[0];

        //    switch (arg)
        //    {
        //        case "create":
        //            CreateDocument();
        //            break;

        //        case "read":
        //        case "update":
        //            ReadAndUpdateDocument();
        //            break;

        //        case "delete":
        //            DeleteDocument();
        //            break;

        //        default:
        //            Console.WriteLine("No matching argument provided.");
        //            break;
        //    }
        //}

        //private static void CreateDocument()
        //{
        //    using (var session = DocumentStoreHolder.Store.OpenSession())
        //    {
        //        //var newShipper = new Models.Shipper
        //        //{
        //        //    Name = "My New Shipper",
        //        //    Phone="124243515"
        //        //};

        //        //session.Store(newShipper);
        //        //Console.WriteLine(newShipper.Id);
        //        //session.SaveChanges();

        //        var newCandy = new Models.Candy()
        //        {
        //            Name = "krówka",
        //            WeightInGrams = 40,
        //            EatBefore = DateTime.Now.AddMonths(2)
        //        };

        //        session.Store(newCandy);
        //        session.SaveChanges();
        //    }
        //}

        //private static void ReadAndUpdateDocument()
        //{
        //    using (var session = DocumentStoreHolder.Store.OpenSession())
        //    {
        //        //var shipper = session.Load<Models.Shipper>("shippers/1-A");
        //        //shipper.Name = "changed name";
        //        //Console.WriteLine(shipper.Name);
        //        //session.SaveChanges();

        //        var candy = session.Load<Models.Candy>("candies/1-A");
        //        candy.Ingredients = new System.Collections.Generic.List<string>() { "sugar", "flavouring" };
        //        session.SaveChanges();
        //    }
        //}

        //private static void DeleteDocument()
        //{
        //    using (var session = DocumentStoreHolder.Store.OpenSession())
        //    {
        //        //session.Delete("shippers/2-A");
        //        //session.SaveChanges();
        //        session.Delete("candy/1-A");
        //        session.SaveChanges();
        //    }
        //}
        //#endregion

        #region Program 3
        static void Main()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var product = session
                    .Include<Models.Product>(x => x.Supplier)
                    .Include<Models.Product>(x => x.Category)
                    .Load<Models.Product>("products/1-A");

                var supplier = session.Load<Models.Supplier>(product.Supplier);
                var category= session.Load<Models.Category>(product.Category);

                supplier.Name = "new supplier";
                Console.WriteLine(category.Description);

                session.SaveChanges();
            }
        }
        #endregion
    }
}
