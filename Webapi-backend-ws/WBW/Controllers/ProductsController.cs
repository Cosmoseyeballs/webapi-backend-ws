using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBW.Models;

namespace WBW.Controllers
{
    [RoutePrefix("produkter")]
    public class ProductsController : ApiController
    {
        internal List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name="Rick and Morty 10\" Foamy Mr. Meeseeks Plush",Price= 19.49M , Category="Plush Toys" },
            new Product { Id = 2, Name="Funko POP Animation: Rick & Morty - Rick Action Figure",Price= 9.75M , Category="Action Figures" },
            new Product { Id = 3, Name="Rick and Morty Morty Plush Stuffed Doll ",Price= 14.99M , Category="Plush Toys" },
            new Product { Id = 4, Name="Rick and Morty Total Rickall Cooperative Card Game ",Price= 33.20M , Category="Card Games" }
        };

        internal List<Reviews> reviews = new List<Reviews>
        {
            new Reviews { Id= 1, ProductId = 1, Rating=8, Text="Nice Mr. Meeseeks doll" },
            new Reviews { Id= 2, ProductId = 1, Rating=3, Text="Not the best Mr. Meeseeks doll" },
            new Reviews { Id= 3, ProductId = 1, Rating=10, Text="How about that Mr. Meeseeks doll" },
            new Reviews { Id= 4, ProductId = 2, Rating=9, Text="I lovvvve Rick and Morty!" },
            new Reviews { Id= 5, ProductId = 2, Rating=7, Text="Funko POP of Rick and Morty! Super nice " },
            new Reviews { Id= 6, ProductId = 2, Rating=2, Text="Funko POP Sucks" },
            new Reviews { Id= 7, ProductId = 3, Rating=7, Text="25 Schmeckles" },
            new Reviews { Id= 7, ProductId = 3, Rating=5, Text="Stuffed Doll is for kits" },
        };

        [Route("")]
        public List<Product> GetAllProducts()
        {
            return products;
        }

        [Route("{id:int}")]
        public Product GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            throw new NotFoundException();
        }

        [Route("{id:int}/reviews")]
        public IEnumerable<Reviews> GetReviewsForProduct(int id)
        {
            return reviews.Where(r=>r.ProductId == id);
        }
    }
}
