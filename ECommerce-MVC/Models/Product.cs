using ECommerce_MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_MVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public ProductColor Color { get; set; }


        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
