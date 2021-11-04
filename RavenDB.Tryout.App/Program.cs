using RavenDB.Tryout.App.Models;
using System;
using System.Linq;

namespace RavenDB.Tryout.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No argument provided.");
                return;
            }
            
            var arg = args[0];

            switch (arg)
            {
                case "create-user":
                    CreateUser();
                    break;
                
                case "create-group":
                    CreateGroup();
                    break;
                
                case "add-user-to-group":
                    AddUserToGroup();
                    break;
                
                case "show-group-users":
                    ShowGroupUsers();
                    break;
                
                case "remove-user-from-group":
                    RemoveUserFromGroup();
                    break;
                
                case "delete-user":
                    DeleteUser();
                    break;
                
                case "show-user-activity":
                    ShowUserActivity();
                    break;
                
                default:
                    Console.WriteLine("No matching argument provided.");
                    break;
            }
        }

        private static void CreateUser()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var user = new User
                {
                    FirtsName = "B",
                    LastName = "P"
                };

                session.Store(user);
                session.SaveChanges();
            }
        }

        private static void CreateGroup()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var group = new Group
                {
                    Name = "test"
                };

                session.Store(group);
                session.SaveChanges();
            }
        }

        private static void AddUserToGroup()
        {
            var groupId = "groups/1";
            var userId = "user/1";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var user = session.Load<User>(userId);
                
                user.GroupId = groupId;

                session.SaveChanges();
            }
        }

        private static void ShowGroupUsers()
        {
            var groupId = "groups/1";

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var users = session.Query<User>().Where(x=> x.GroupId == groupId);

                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
        }

        private static void RemoveUserFromGroup()
        {
            var userId = "user/1";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var user = session.Load<User>(userId);

                user.GroupId = "";

                session.SaveChanges();
            }
        }

        private static void DeleteUser()
        {
            var userId = "user/1";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(userId);
                session.SaveChanges();
            }
        }

        private static void ShowUserActivity()
        {
            var userId = "user/1";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var userHistory = session.Load<History>(userId + "/history");
                Console.WriteLine(userHistory.LastSuccesfullLogin);
            }
        }
    }
}
