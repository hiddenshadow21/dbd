using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB.Tryout.App.Models
{
    public class Candy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float WeightInGrams { get; set; }
        public List<string> Ingredients { get; set; }
        public DateTime EatBefore { get; set; }
        public string PartNumber { get; set; }
    }
}
