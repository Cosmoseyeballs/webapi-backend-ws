using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            new Product { PartitionKey="1",RowKey="1", Name="Rick and Morty 10\" Foamy Mr. Meeseeks Plush",Price= 19.49 , Category="Plush Toys" },
            new Product {  PartitionKey="1",RowKey="2", Name="Funko POP Animation: Rick & Morty - Rick Action Figure",Price= 9.75 , Category="Action Figures" },
            new Product {  PartitionKey="1",RowKey="3", Name="Rick and Morty Morty Plush Stuffed Doll ",Price= 14.99 , Category="Plush Toys" },
            new Product {  PartitionKey="1",RowKey="4", Name="Rick and Morty Total Rickall Cooperative Card Game ",Price= 33.20 , Category="Card Games" }
        };

        internal List<Reviews> reviews = new List<Reviews>
        {
            new Reviews { PartitionKey="1",RowKey="1",  Rating=8, Text="Nice Mr. Meeseeks doll" },
            new Reviews { PartitionKey="1",RowKey="2",  Rating=3, Text="Not the best Mr. Meeseeks doll" },
            new Reviews { PartitionKey="1",RowKey="3",  Rating=10, Text="How about that Mr. Meeseeks doll" },
            new Reviews { PartitionKey="2",RowKey="4", Rating=9, Text="I lovvvve Rick and Morty!" },
            new Reviews { PartitionKey="2",RowKey="5", Rating=7, Text="Funko POP of Rick and Morty! Super nice " },
            new Reviews { PartitionKey="2",RowKey="6",  Rating=2, Text="Funko POP Sucks" },
            new Reviews { PartitionKey="3",RowKey="7",  Rating=7, Text="25 Schmeckles" },
            new Reviews { PartitionKey="3",RowKey="8", Rating=5, Text="Stuffed Doll is for kits" },
        };

        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
           
            CloudTableClient tableClient = CreateTableClient();

            CloudTable ProductTable = tableClient
                   .GetTableReference(nameof(Product));




            IEnumerable<Product> product = from p in ProductTable.CreateQuery<Product>()
                               select p;

            if (product != null)
            {
                return product;
            }
            throw new NotFoundException();

        }

        [Route("{id}")]
        public Product GetProduct(string id)
        {

            CloudTableClient tableClient = CreateTableClient();

            CloudTable ProductTable = tableClient
                   .GetTableReference(nameof(Product));




            Product product = (from p in ProductTable.CreateQuery<Product>()
                                where p.RowKey == id
                                select p).FirstOrDefault();

            if (product != null)
            {
                return product;
            }
            throw new NotFoundException();
        }

        [Route("{id}/reviews")]
        public IEnumerable<Reviews> GetReviewsForProduct(string id)
        {
            CloudTableClient tableClient = CreateTableClient();

            CloudTable table = tableClient
                   .GetTableReference(nameof(Reviews));

            IEnumerable<Reviews> reviews = from r in table.CreateQuery<Reviews>()
                                           where r.PartitionKey == id 
                                           select r;

            if (reviews != null)
            {
                return reviews;
            }
            throw new NotFoundException();
        }




        private CloudTableClient CreateTableClient()
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);  //CloudConfigurationManager.GetSetting("StorageConnectionString");

            // Create the table client.
            CloudTableClient tableClient = storageAccount
                .CreateCloudTableClient();


            try
            {
                // Retrieve a reference to the table.
                CloudTable table = tableClient
                    .GetTableReference(nameof(Product));

                // Create the table if it doesn't exist.
                table.CreateIfNotExists();

                // Retrieve a reference to the table.
                CloudTable table2 = tableClient
                    .GetTableReference(nameof(Reviews));

                // Create the table if it doesn't exist.
                table2.CreateIfNotExists();

            }
            catch (Exception)
            {

                throw;
            }


            return tableClient;
        }



        internal void InitializeSampleData()
        {


            CloudTableClient tableClient = CreateTableClient();

            CloudTable ProductTable = tableClient
                   .GetTableReference(nameof(Product));
            
            CloudTable ReviewsTable = tableClient
                .GetTableReference(nameof(Reviews));


          

           

            products.ForEach(p =>{
                TableOperation InsertOperation = TableOperation.InsertOrReplace(p);
                ProductTable.Execute(InsertOperation);
            });


            reviews.ForEach(p => {
                TableOperation InsertOperation = TableOperation.InsertOrReplace(p);
                ReviewsTable.Execute(InsertOperation);
            });

            // Execute the insert operation.

        }



    }
}
