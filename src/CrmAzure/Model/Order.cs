using System;
using System.Collections.Generic;

namespace CrmAzure.Model
{
    public class Order
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        public string Address { get; set; }

        public int CustomerId { get; set; }

        // this is an "inverse" navigation property
        public Customer Customer { get; set; }

        // works in EF Core 5.0
        // public List<Product> Products { get; set; }

        public List<OrderProduct> Products { get; set; }

        public Order()
        {
            Products = new List<OrderProduct>();
        }
    }
}
