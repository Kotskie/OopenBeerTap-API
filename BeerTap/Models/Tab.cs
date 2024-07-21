namespace BeerTap.Models
{
    public class Tab
    {
        public Guid Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Total { get; set; }
        public int? SplitBetween { get; set; }
        public decimal? TotalPerPerson { get; set; }
    }
}
