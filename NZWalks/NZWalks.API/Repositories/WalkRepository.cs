using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;
        public WalkRepository(NZWalksDbContext context)
        {
            this._context = context;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await _context.Walks.FindAsync(id);

            if(walk == null)
            {
                return null;
            }

            _context.Walks.Remove(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            var walks = await _context.Walks
                                    .Include(w => w.Region)
                                    .Include(w => w.WalkDifficulty)
                                    .ToListAsync();

            return walks.OrderBy(x => x.Name).ToList();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            var walk = await _context.Walks
                                    .Include(w => w.Region)
                                    .Include(w => w.WalkDifficulty)
                                    .FirstOrDefaultAsync(x => x.Id == id);
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await _context.Walks.FindAsync(id);

            if(existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await _context.SaveChangesAsync();
            return existingWalk;
        }
    }
}
