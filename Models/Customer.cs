using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public int customer_id {get;set;}
        public string first_name {get;set;}
        public string last_name {get;set;}
        public string address {get;set;}
        public string city {get;set;}
        public string state {get;set;}
        public string zip {get;set;}
        public string phone {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public List<Order> Orders {get;set;}
        public Customer()
        {
            Orders = new List<Order>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }   
}