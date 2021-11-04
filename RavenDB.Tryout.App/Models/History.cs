using System;
using System.Collections.Generic;
using System.Text;

namespace RavenDB.Tryout.App.Models
{
    public class History
    {
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastSuccesfullLogin { get; set; }
        public DateTime LastFailedLogin { get; set; }
        public DateTime LastLogout { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
