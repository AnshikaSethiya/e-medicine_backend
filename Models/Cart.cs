namespace EMedicine_Backend.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; } 
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int MedicineId { get; set; }
    }
}
