namespace ECommerce_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<orderItem> OrderItems { get; set; }=new HashSet<orderItem>();

    }
}
