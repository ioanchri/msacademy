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

                //var customer = new Customer()
                //{
                //    FirstName = "John",
                //    LastName = "Taramas",
                //    IsActive = true,
                //    Gross = 0

                //};


                //db.Customer.Add(customer);


                //Customer customer = db.Customer.First();
                //Console.WriteLine($"{customer.FirstName} {customer.LastName}");
                // get first order
                //var firstCustomerOrder = c.Orders.First();

                // save changes
                db.SaveChanges();
            }
        }
    }
}
