using api.Models;
namespace api.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
