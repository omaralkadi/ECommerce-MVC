namespace ECommerce_MVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
