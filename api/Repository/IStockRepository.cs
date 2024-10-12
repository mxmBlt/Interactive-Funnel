using api.Dtos.Stock;
using api.Models;

namespace api.Repository
{
    public interface IStockRepository
    {
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> DeleteAsync(int id);
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    }
}