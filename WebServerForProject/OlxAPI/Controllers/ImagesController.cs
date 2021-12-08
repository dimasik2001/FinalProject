using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Data.Entities;
using OlxAPI.Helpers;
using OlxAPI.Models.PostModels;
using OlxAPI.Services.Services;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : IdentityControllerBase<User>
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserHelper _userHelper;
        private readonly IImagesService _imagesService;
        private const string _profilesPath = "profiles";
        private const string _adsPath = "ads";
        public ImagesController(UserManager<User> userManager, IImagesService imagesService, IWebHostEnvironment appEnvironment, UserHelper userHelper)
            :base(userManager)
        {
            _imagesService = imagesService;
            _appEnvironment = appEnvironment;
            _userHelper = userHelper;
        }
        [HttpPost]
        [Route("profile")]
        [Authorize(Roles = "User")]
        public async Task AddProfileIcon([FromForm] IFormFile image)
        {
            var temp = HttpContext.Request.Form.Files;
            if (image is null)
            {
                return;
            }
            var user = await GetIdentityUserAsync();
            var imagePath = Path.Combine(_appEnvironment.WebRootPath, user.ImagePath);

            _imagesService.DeleteFile(imagePath);

            var imageName = await SaveFormFile(image, Path.Combine(_appEnvironment.WebRootPath, _profilesPath));
            await _imagesService.AddUserIconAsync(Path.Combine(_profilesPath, imageName), user.Id);
        }

        [HttpPost]
        [Route("ads/{adId}")]
        [Authorize(Roles = "User")]
        public async Task AddImagesToAd([FromForm]IFormFileCollection images, int adId)
        {
            var user = await GetIdentityUserAsync();
            if(images is null || images.Count == 0)
            {
                return;
            }
            var isOwner = await _userHelper.IsOwner(user.Id, adId);
            if(!isOwner)
            {
                throw new BadHttpRequestException("Forbidden", 403);
            }
            var pathList = new List<string>();
            foreach (var image in images)
            {
                var imageName = await SaveFormFile(image, Path.Combine(_appEnvironment.WebRootPath, _adsPath));
                pathList.Add(Path.Combine(_adsPath, imageName));
            }
            await _imagesService.AddImagesAsync(pathList, adId);
        }


        [HttpPost]
        [Route("ads/delete/{adId}")]
        [Authorize(Roles = "User")]
        public async Task DeleteImagesFromAd(ICollection<string> paths, int adId)
        {
            var user = await GetIdentityUserAsync();
            var isOwner = await _userHelper.IsOwner(user.Id, adId);
            
            if (!isOwner)
            {
                throw new BadHttpRequestException("Forbidden", 403);
            }

            foreach (var path in paths)
            {
                var fullPath = Path.Combine(_appEnvironment.WebRootPath, path);
                _imagesService.DeleteFile(fullPath);
            }
            await _imagesService.DeleteImagesAsync(paths, adId);
        }

        private async Task<string> SaveFormFile(IFormFile file, string path)
        {
            using var readStream = file.OpenReadStream();
            var fileName = await _imagesService.SaveFormFile(readStream, path, file.FileName);
            return fileName;
        }
    }
}
