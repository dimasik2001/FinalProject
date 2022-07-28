using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OlxAPI.Services.Services.Abstractions
{
    public interface IImagesService
    {
        Task AddUserIconAsync(string Iconpath, string userId);
        Task AddImagesAsync(IEnumerable<string> Imagepaths, int adId);
        Task DeleteImagesAsync(IEnumerable<string> imagepaths, int adId);
        public bool DeleteFile(string filePath);
        Task<string> SaveFormFile(Stream stream, string directoryPath, string fileExtension);
    }
}