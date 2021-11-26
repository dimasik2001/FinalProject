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
        public async Task AddPhotosAsync(IEnumerable<string> Imagepaths, int adId)
        {
            var ad = await _ctx.Ads.FindAsync(adId);
            var images = Imagepaths.Select(p => new Image { Path = p });
            if(ad.Images == null)
            {
                ad.Images = new List<Image>();
            }
            foreach (var item in images)
            {
                ad.Images.Add(item);
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task AddUserIconAsync(string Iconpath, string userId)
        {
            var user = await _ctx.Users.FindAsync(userId);
            user.ImagePath = Iconpath;
            await _ctx.SaveChangesAsync();
        }
    }
}
