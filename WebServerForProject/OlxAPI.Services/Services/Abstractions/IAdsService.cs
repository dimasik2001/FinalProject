using System.Collections.Generic;
using System.Threading.Tasks;
using OlxAPI.Services.Models;

namespace OlxAPI.Services.Services.Abstractions
{
    public interface IAdsService
    {
        Task<AdModel> CreateAsync(AdModel model);
        Task DeleteAsync(int id);
        Task<IEnumerable<AdModel>> GetAsync(PaginationParametersModel paginationModel,
            FilterParametersModel filterModel,
            SortParametersModel sortModel);
        Task<AdModel> GetAsync(int id);
        Task<AdModel> UpdateAsync(AdModel model);
        Task<IEnumerable<CategoryModel>> GetCategories();
    }
}