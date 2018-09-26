using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class Order : BaseEntity
    {
        [Key]
        public int order_id {get;set;}
        public int user_id {get;set;}
        public Customer Customer {get;set;}
        public double total {get;set;}
        public List<ProductsOrders> ProductsOrders {get;set;}
        public Order()
        {
            ProductsOrders = new List<ProductsOrders>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}