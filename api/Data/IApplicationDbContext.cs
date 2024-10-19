using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<Stock> Stocks { get; set; }
    }
}