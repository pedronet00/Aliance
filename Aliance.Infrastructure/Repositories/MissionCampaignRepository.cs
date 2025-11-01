using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class MissionCampaignRepository : IMissionCampaignRepository
{

    private readonly AppDbContext _context;

    public MissionCampaignRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MissionCampaign> AddAsync(MissionCampaign missionCampaign)
    {
        await _context.MissionCampaign.AddAsync(missionCampaign);

        return missionCampaign;
    }

    public async Task<bool> DeleteAsync(Guid guid)
    {
        var campaign = await _context.MissionCampaign.FirstOrDefaultAsync(x => x.Guid == guid);

        _context.MissionCampaign.Remove(campaign!);

        return true;
    }

    public async Task<PagedResult<MissionCampaign>> GetAllAsync(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.MissionCampaign
            .Where(mc => mc.ChurchId == churchId);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<MissionCampaign>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<MissionCampaign?> GetByGuidAsync(int churchId, Guid guid)
    {
        return await _context.MissionCampaign.FirstOrDefaultAsync(c => c.Guid == guid && c.ChurchId == churchId);
    }

    public async Task<MissionCampaign> UpdateAsync(MissionCampaign missionCampaign)
    {
        var existingCampaign = await _context.MissionCampaign.AsNoTracking().FirstOrDefaultAsync(c => c.Id == missionCampaign.Id);

        _context.MissionCampaign.Update(missionCampaign);

        return missionCampaign;
    }
}
