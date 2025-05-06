using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class Products
    {
        // Properties of the Products class
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }
}