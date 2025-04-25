namespace api.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}
