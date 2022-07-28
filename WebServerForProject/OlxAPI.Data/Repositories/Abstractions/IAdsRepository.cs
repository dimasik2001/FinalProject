using System.Collections.Generic;
using System.Threading.Tasks;
using OlxAPI.Data.Entities;
using OlxAPI.Data.Parameters;

namespace OlxAPI.Data.Repositories.Abstractions
{
    public interface IAdsRepository
    {
        Task CreateAsync(Ad ad);
        Task DeleteAsync(int id);
        Task<IEnumerable<Ad>> GetAsync(PaginationParameters pagination,
            FilterParameters filter = null,
            SortParameters sort = null);
        Task<Ad> GetByIdAsync(int id);
        Task UpdateAsync(Ad ad);
        Task<IEnumerable<Category>> GetCategories();
    }
}