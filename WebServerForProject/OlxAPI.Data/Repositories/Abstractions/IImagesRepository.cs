using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlxAPI.Data.Repositories.Abstractions
{
    public interface IImagesRepository
    {
        Task AddUserIconAsync(string Iconpath, string userId);

        Task AddImagesAsync(IEnumerable<string> Imagepaths, int adId);
        Task DeleteImagesAsync(IEnumerable<string> imagepaths, int adId);
    }
}