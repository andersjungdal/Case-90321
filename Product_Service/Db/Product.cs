using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Service.Db
{
    public class Product
    {
        public int Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Size { get; set; }
        public string Color { get; set; }
        public decimal Prize { get; set; }

        public string Picture { get; set; }
        public string Categories { get; set; }

    }
}
