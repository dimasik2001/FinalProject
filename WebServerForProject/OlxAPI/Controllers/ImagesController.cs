using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Data.Entities;
using OlxAPI.Models.PostModels;
using OlxAPI.Services.Services;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private UserManager<User> _userManager;
        private IImagesService _imagesService;

        public ImagesController(UserManager<User> userManager, IImagesService imagesService)
        {
            _userManager = userManager;
            _imagesService = imagesService;
        } 
        [HttpPost]
        [Route("profile")]
        [Authorize(Roles = "User")]
        public async Task AddProfileIcon([FromBody] IconPostModel iconPostModel)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var imagePath = $"wwwroot/profiles/{user.Email}.jpg";
            Base64ToFile(iconPostModel.encodedImage, imagePath);

            await _imagesService.AddUserIconAsync(imagePath.Trim("wwwroot/".ToCharArray()), user.Id);
        }

        [HttpPost]
        [Route("profile/{adId}")]
        [Authorize(Roles ="User")]
        public async Task AddImagesToAd([FromBody] ImagesPostModel imagesPostModel, int adId)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var pathList = new List<string>();
            foreach (var encodedImage in imagesPostModel.encodedImages)
            {
                var imagePath = $"wwwroot/ads/{Guid.NewGuid()}.jpg";
                Base64ToFile(encodedImage, imagePath);
                pathList.Add(imagePath.Trim("wwwroot/".ToCharArray()));
            }
           await _imagesService.AddPhotosAsync(pathList, adId, user.Id);
            
        }

        private void Base64ToFile(string encodedImage, string fileName)
        {
            var bytes = Convert.FromBase64String(encodedImage);
            using(var ms = new MemoryStream(bytes))
            {
                var image = System.Drawing.Image.FromStream(ms);
                image.Save(fileName);
            }
        }
    }
}
