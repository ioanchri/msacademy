using System;
using System.Linq;
using CrmAzure.Data;
using CrmAzure.Model;
using Microsoft.EntityFrameworkCore;

// ReSharper disable All

namespace CrmAzure
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CrmAzureDbContext())
            {
                // fetch customer with id 2
                //var c = db
                //    .Customer
                //    .Where(c => c.Id == 2)
                //    .Include(c => c.Orders)
                //    .ThenInclude(o => o.Products)
                //    .SingleOrDefault();

                // --------Added 30 customers ---------------
                
                //for (int i = 0; i < 30 ; i++)
                //{
                //    var customer = new Customer()
                //    {
                //    FirstName = $"Firstname {i} ",
                //    LastName = $"Lastname {i} ",
                //    IsActive = true,
                //    Gross = 0

                //    };

                //    db.Add(customer);
                //---------------------------------------------
             

                //--------Added 30 orders to random Customers-----------------------

                //for (int i = 0; i < 30; i++)
                //{
                //    var randomCustomer = db.Customer
                //    .OrderBy(r => Guid.NewGuid())
                //    .Skip(0)
                //    .Take(1)
                //    .Include(c => c.Orders)
                //    .ThenInclude(o => o.Products)
                //    .SingleOrDefault();


                //    var order = new Order()
                //    {
                //        Description = $"This is Description number {i}",
                //        Created = DateTimeOffset.UtcNow,
                //        Address = $"Address number {i} ",
                //        Customer = randomCustomer,
                //        CustomerId = randomCustomer.Id
                //    };

                //    randomCustomer.Orders.Add(order);
                //}
                //-----------------------------------------------


                var c = db.Customer
                    .Where(c=>c.Id == 2)                   
                    .Include(c => c.Orders)
                    .ThenInclude(o =>o.Products)
                    .SingleOrDefault();


                var firstCustomerOrder = c
                    .Orders
                    .First();
                firstCustomerOrder.Products.Add(new OrderProduct()
                {
                    Product = new Product
                    {
                        Name = "Xiaomi",
                        Price = 300
                    }                    
                });
                           
                db.SaveChanges();
                
            };
                //db.Customer.Add(customer);
                //Console.WriteLine($"{customer.FirstName} {customer.LastName}");



                //Customer customer = db.Customer.First();
                //Console.WriteLine($"{customer.FirstName} {customer.LastName}");
                // get first order
                //var firstCustomerOrder = c.Orders.First();

                // save changes


                //var c = db.Customer.Where(c => c.Id == 5).First();
                //Console.WriteLine(c.FirstName);

            }
        }
}
