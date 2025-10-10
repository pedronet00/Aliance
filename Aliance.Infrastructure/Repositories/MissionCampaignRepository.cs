using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
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

    public async Task<bool> DeleteAsync(int id)
    {
        var campaign = await _context.MissionCampaign.FirstOrDefaultAsync(x => x.Id == id);

        if(campaign is null)
            return false;

        _context.MissionCampaign.Remove(campaign);

        return true;
    }

    public async Task<IEnumerable<MissionCampaign>> GetAllAsync()
    {
        return await _context.MissionCampaign.AsNoTracking().ToListAsync();
    }

    public async Task<MissionCampaign?> GetByIdAsync(int id)
    {
        return await _context.MissionCampaign.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<MissionCampaign> UpdateAsync(MissionCampaign missionCampaign)
    {

        var existingCampaign = await _context.MissionCampaign.AsNoTracking().FirstOrDefaultAsync(c => c.Id == missionCampaign.Id);

        if (existingCampaign is null)
            throw new ArgumentNullException(nameof(missionCampaign));

        _context.MissionCampaign.Update(missionCampaign);

        return missionCampaign;
    }
}
