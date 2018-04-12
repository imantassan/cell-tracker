using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace CellTracker.Repository
{
    public interface IDbContext
    {
        DbSet<T> GetDbSet<T>()
            where T : class;

        int Save();

        Task<int> SaveAsync();
    }
}