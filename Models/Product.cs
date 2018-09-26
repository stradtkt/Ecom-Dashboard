using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomStore.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public int product_id {get;set;}
        [Required(ErrorMessage="Product Name is required")]
        [MinLength(3, ErrorMessage="Product Name has a min length of 3")]
        [MaxLength(40, ErrorMessage="Product Name has a max length of 40")]
        public string name {get;set;}
        [Required(ErrorMessage="Short description is required")]
        [MinLength(5, ErrorMessage="Short description has a min length of 5")]
        [MaxLength(100, ErrorMessage="Short description has a max length of 100")]
        public string short_desc {get;set;}
        [Required(ErrorMessage="Description is required")]
        [MinLength(10, ErrorMessage="Description has a min length of 10")]
        public string desc {get;set;}
        [Required(ErrorMessage="Image is required")]
        public string image {get;set;}
        [Required(ErrorMessage="Price is required")]
        [Display(Name="Price")]
        public double price {get;set;}
        [Required(ErrorMessage="Weight is required")]
        [Display(Name="Weight")]
        public double weight {get;set;}
        [Required(ErrorMessage="Quantity is required")]
        [Display(Name="Quantity")]
        public int qty {get;set;}
        public List<Order> Orders {get;set;}
        public List<ProductsCategories> ProductsCategories {get;set;}
        public Product()
        {
            Orders = new List<Order>();
            ProductsCategories = new List<ProductsCategories>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}