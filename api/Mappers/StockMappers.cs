using api.Dtos.Stock;
using api.Models;


namespace api.Mappers
{
    public static class StockMappers // static keyword needed because we are creating extension methods 
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Price = stockModel.Price,
                Industry = stockModel.Industry,
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Price = stockDto.Price,
                Industry = stockDto.Industry,
            };
        }
    }
}
