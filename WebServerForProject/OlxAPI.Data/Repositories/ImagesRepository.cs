using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlxAPI.Data.Entities;
using OlxAPI.Data.Repositories.Abstractions;

namespace OlxAPI.Data.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private ApplicationDbContext _ctx;

        public ImagesRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddImagesAsync(IEnumerable<string> imagepaths, int adId)
        {
            var ad = await _ctx.Ads.FindAsync(adId);
            var images = imagepaths.Select(p => new Image { Path = p });
            if (ad.Images == null)
            {
                ad.Images = new List<Image>();
            }
            foreach (var item in images)
            {
                ad.Images.Add(item);
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task AddUserIconAsync(string IconPath, string userId)
        {
            var user = await _ctx.Users.FindAsync(userId);
            user.ImagePath = IconPath;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteImagesAsync(IEnumerable<string> imagepaths, int adId)
        {
            var images = _ctx.Images.Where(i => i.AdId == adId);
            var imagesToDelete = images.Where(i => imagepaths.Contains(i.Path));
            foreach (var img in imagesToDelete)
            {
                _ctx.Images.Remove(img);
            }
            await _ctx.SaveChangesAsync();
        }
    }
}
