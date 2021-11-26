using System;
using System.Collections.Generic;
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
        public async Task AddPhotosAsync(IEnumerable<string> Imagepaths, int adId, string userId)
        {
            var adModel = await _adsRepository.GetByIdAsync(adId);
            if(userId == adModel.UserId)
            {
                await _imagesRepository.AddPhotosAsync(Imagepaths, adId);
            }
        }

        public async Task AddUserIconAsync(string Iconpath, string userId)
        {
            await _imagesRepository.AddUserIconAsync(Iconpath, userId);
        }
    }
}
