using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class ProductsCategories
    {
        [Key]
        public int product_category_id {get;set;}
        public int category_id {get;set;}
        public Category Categories {get;set;}
        public int product_id {get;set;}
        public Product Products {get;set;}
    }
}