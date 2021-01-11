using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentNegotiationAndMediaFormatter.Models
{
    public class Product
    {
        public Product(int id, string name, string category, int price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}