using System;

namespace Kundestyring.Models
{
    public class Kundestyring
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