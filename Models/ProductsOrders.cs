using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class ProductsOrders
    {
        [Key]
        public int products_orders_id {get;set;}
        public int order_id {get;set;}
        public Order Orders {get;set;}
        public int product_id {get;set;}
        public Product Products {get;set;}
        public int customer_id {get;set;}
        public Customer Customer {get;set;}
        public int order_qty {get;set;}
    }
}