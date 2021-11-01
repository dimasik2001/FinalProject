using System.Collections.Generic;
using System.Threading.Tasks;
using OlxAPI.Data.Entities;

namespace OlxAPI.Data.Repositories.Abstractions
{
    public interface IAdsRepository
    {
        Task<Ad> CreateAsync(Ad ad);
        Task DeleteAsync(int id);
        Task<IEnumerable<Ad>> GetAsync();
        Task<Ad> GetAsync(int id);
        Task<Ad> UpdateAsync(Ad ad);
    }
}