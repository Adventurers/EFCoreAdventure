namespace EFCoreAdventure.Models
{
    public class OrderItem : Entity
    {

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Unit { get; set; }

        public decimal Discount { get; set; }


    }
}
