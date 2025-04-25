namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        public double Price { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}
