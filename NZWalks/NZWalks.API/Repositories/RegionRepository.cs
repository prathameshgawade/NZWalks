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

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            var regions = await this._context.Regions.ToListAsync();
            return regions;
        }
    }
}
