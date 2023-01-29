using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionRepository(NZWalksDbContext context)
        {
            this._context = context;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            var regions = await _context.Regions.ToListAsync();
            return regions.OrderBy(x => x.Name).ToList();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x =>x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.Name= region.Name;
            existingRegion.Code= region.Code;
            existingRegion.Area= region.Area;
            existingRegion.Lat= region.Lat;
            existingRegion.Long= region.Long;
            existingRegion.Population= region.Population;

            await _context.SaveChangesAsync();
            return existingRegion;
        }
    }
}
