using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlxAPI.Data.Repositories.Abstractions
{
    public interface IImagesRepository
    {
        Task AddUserIconAsync(string Iconpath, string userId);

        Task AddPhotosAsync(IEnumerable<string> Imagepaths, int adId);

    }
}