using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class Order : BaseEntity
    {
        [Key]
        public int order_id {get;set;}
        public int customer_id {get;set;}
        public Customer Customer {get;set;}
        public int product_id {get;set;}
        public Product Products {get;set;}
        public int order_qty {get;set;}
        public Order()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}