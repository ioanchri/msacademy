using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                // --------Added 30 customers ---------------

                //for (int i = 0; i < 30; i++)
                //{
                //    var customer = new Customer()
                //    {
                //        FirstName = $"Firstname {i} ",
                //        LastName = $"Lastname {i} ",
                //        IsActive = true,
                //        Gross = 0

                //    };

                //    db.Add(customer);
                //---------------------------------------------


                //--------Added 10 Orders(with 1 Product in each) to random Customers--------------------

                //for (int i = 0; i < 10; i++)
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
                //        CustomerId = randomCustomer.Id,
                //    };

                //    Product product = new Product()
                //    {
                //        Name = "Huawei Y40",
                //        Price = 200,
                //        Code = Guid.NewGuid().ToString()
                //};
                //    List<OrderProduct> orderProducts = new List<OrderProduct>()
                //    {
                //        new OrderProduct()
                //        {
                //          Order = order,
                //          Product = product,
                //          OrderId = order.Id,
                //          ProductId = product.Id
                //        }
                //    };

                //    order.Products = orderProducts;
                //    randomCustomer.Gross += product.Price;
                //    randomCustomer.Orders.Add(order);
                //}

                // --------------------------------------------------------------------------

                //// Display Top 3 Customers
                //var TopGross = (from c in db.Customer
                //                orderby c.Gross descending
                //                select c).Take(3).ToList();

                //TopGross.ForEach(a => { Console.WriteLine($"Customer Name: {a.FirstName} Surname: {a.LastName} Total Gross: {a.Gross}"); }

                //);
                //Console.ReadLine();


                db.SaveChanges();



            };


        }
    }
}
