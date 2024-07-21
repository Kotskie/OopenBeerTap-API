namespace BeerTap.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Beverage Beverage { get; set; }
        public int Quantity { get; set; }
    }
}
