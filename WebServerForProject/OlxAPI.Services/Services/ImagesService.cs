using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OlxAPI.Data.Repositories.Abstractions;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Services.Services
{
    public class ImagesService : IImagesService
    {
        private IImagesRepository _imagesRepository;
        private IAdsRepository _adsRepository;

        public ImagesService(IImagesRepository repository, IAdsRepository adsRepository)
        {
            _imagesRepository = repository;
            _adsRepository = adsRepository;
        }
        public async Task AddImagesAsync(IEnumerable<string> imagepaths, int adId)
        {
            await _imagesRepository.AddImagesAsync(imagepaths, adId);

        }

        public async Task AddUserIconAsync(string Iconpath, string userId)
        {
            await _imagesRepository.AddUserIconAsync(Iconpath, userId);
        }
        public async Task DeleteImagesAsync(IEnumerable<string> imagepaths, int adId)
        {
            await _imagesRepository.DeleteImagesAsync(imagepaths, adId);
        }

        public bool DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        public async Task<string> SaveFormFile(Stream stream, string directoryPath, string fileExtension)
        {
            fileExtension = Path.GetExtension(fileExtension);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(directoryPath, newFileName);
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            await File.WriteAllBytesAsync(fullPath, buffer);
            return newFileName;
        }
    }
}
