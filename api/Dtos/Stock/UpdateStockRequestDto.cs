namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public double Price { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}
