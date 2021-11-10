using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OlxAPI.Data.Entities;
using OlxAPI.Data.Repositories.Abstractions;
using System.Linq;
using OlxAPI.Data.Parameters;

namespace OlxAPI.Data.Repositories
{
    public class AdsRepository : IAdsRepository
    {
        private ApplicationDbContext _ctx;

        public AdsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<Ad>> GetAsync(PaginationParameters pagination,
            FilterParameters filter = null,
            SortParameters sort = null)
        {
           
            IQueryable<Ad> result = _ctx.Ads;
            if (filter?.Predicates != null)
            {
                foreach (var predicate in filter.Predicates)
                {
                   result = result.Where(predicate);
                }    
            }
            var skipCount = (pagination.Page - 1) * pagination.PageSize;
            var takeCount = pagination.PageSize;
            pagination.TotalPages = (int)Math.Ceiling(result.Count() / (double)pagination.PageSize);

            if (sort != null)
            {
                if(sort.IsAscending)
                {
                    result = result.OrderBy(sort.SortFunc);
                }
                else 
                {
                    result = result.OrderByDescending(sort.SortFunc);
                }
            }
            return await result
                .Skip(skipCount)
                .Take(takeCount)
                .Include(a => a.AdsCategories)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Ad> GetByIdAsync(int id)
        {
            return await _ctx.Ads
                .Include(a => a.AdsCategories)
                .ThenInclude(c => c.Category)
                .Include(a => a.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateAsync(Ad ad)
        {
            ad.ChangeDate = DateTime.UtcNow;
            await _ctx.Ads.AddAsync(ad); 
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var current = await _ctx.Ads.FindAsync(id);
            _ctx.Remove(current);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ad ad)
        {
            var current = await _ctx.Ads.FindAsync(ad.Id);
            if(current.UserId != ad.UserId)
            {
                return;
            }
            _ctx.AdsCategories.RemoveRange(_ctx.AdsCategories.Where(c => c.AdId == ad.Id));
            current.Header = ad.Header;
            current.Description = ad.Description;
            current.Images = ad.Images;
            current.ChangeDate = DateTime.UtcNow;
            current.AdsCategories = ad.AdsCategories;
            current.Price = ad.Price;

            await _ctx.SaveChangesAsync();
        }
    }
}
