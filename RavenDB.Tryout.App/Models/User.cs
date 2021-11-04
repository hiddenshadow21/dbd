using System;
using System.Collections.Generic;
using System.Text;

namespace RavenDB.Tryout.App.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }

        public string GroupId { get; set; }

    }
}
