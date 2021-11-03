using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OlxAPI.Data.Entities;
using OlxAPI.Data.Repositories.Abstractions;
using System.Linq;

namespace OlxAPI.Data.Repositories
{
    public class AdsRepository : IAdsRepository
    {
        private ApplicationDbContext _ctx;

        public AdsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<Ad>> GetAsync()
        {
            return await _ctx.Ads
                .Include(a => a.AdsCategories)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Ad> GetAsync(int id)
        {
            return await _ctx.Ads
                .Include(a => a.AdsCategories)
                .ThenInclude(c => c.Category)
                .Include(a => a.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Ad> CreateAsync(Ad ad)
        {
            ad.ChangeDate = DateTime.UtcNow;
            await _ctx.Ads.AddAsync(ad); 
            await _ctx.SaveChangesAsync();
            return await GetAsync(ad.Id);
           
        }
        public async Task DeleteAsync(int id)
        {
            var current = await _ctx.Ads.FindAsync(id);
            _ctx.Remove(current);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Ad> UpdateAsync(Ad ad)
        {
            var current = await _ctx.Ads.FindAsync(ad.Id);
            if(!(current.UserId == ad.UserId))
            {
                return null;
            }
            _ctx.AdsCategories.RemoveRange(_ctx.AdsCategories.Where(c => c.AdId == ad.Id));
            current.Header = ad.Header;
            current.Description = ad.Description;
            current.Images = ad.Images;
            current.ChangeDate = DateTime.UtcNow;
            current.AdsCategories = ad.AdsCategories;

            await _ctx.SaveChangesAsync();
            return await GetAsync(ad.Id);
        }
    }
}
