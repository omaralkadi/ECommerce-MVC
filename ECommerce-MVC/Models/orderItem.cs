using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_MVC.Models
{
    public class orderItem
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }


    }
}