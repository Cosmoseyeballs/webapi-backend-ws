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
        [Route("")]
        public List<Product> GetAllProducts()
        {
            return products;
        }

        [Route("{id:int}")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return Ok(product) ;
            }
            return NotFound();
        }
    }
}
