using System;

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
            throw new NotImplementedException("TODO");
        }

        private static void CreateGroup()
        {
            throw new NotImplementedException("TODO");
        }

        private static void AddUserToGroup()
        {
            throw new NotImplementedException("TODO");
        }

        private static void ShowGroupUsers()
        {
            throw new NotImplementedException("TODO");
        }

        private static void RemoveUserFromGroup()
        {
            throw new NotImplementedException("TODO");
        }

        private static void DeleteUser()
        {
            throw new NotImplementedException("TODO");
        }

        private static void ShowUserActivity()
        {
            throw new NotImplementedException("TODO");
        }
    }
}
