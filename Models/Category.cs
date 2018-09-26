using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int category_id {get;set;}
        public string name {get;set;}
        public List<ProductsCategories> ProductsCategories {get;set;}
        public Category()
        {
            ProductsCategories = new List<ProductsCategories>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}