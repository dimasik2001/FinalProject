using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlxAPI.Services.Services.Abstractions
{
    public interface IImagesService
    {
        Task AddUserIconAsync(string Iconpath, string userId);
        Task AddPhotosAsync(IEnumerable<string> Imagepaths, int adId, string userId);
    }
}