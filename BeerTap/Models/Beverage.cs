namespace BeerTap.Models
{
    public class Beverage
    {
        public int Id { get; set; }  // Primary key
        public Guid BeverageId { get; set; }  // Unique constraint
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
