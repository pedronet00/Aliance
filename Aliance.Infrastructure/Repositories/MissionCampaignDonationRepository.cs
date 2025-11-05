using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Domain.ValueObjects;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Aliance.Infrastructure.Repositories;

public class MissionCampaignDonationRepository : IMissionCampaignDonationRepository
{
    private readonly AppDbContext _context;

    public MissionCampaignDonationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MissionCampaignDonation> AddDonation(MissionCampaignDonation donation)
    {
        await _context.MissionCampaignDonation.AddAsync(donation);

        return donation;
    }

    public async Task<MissionCampaignDonation> DeleteDonation(int donationId)
    {
        var donation = await _context.MissionCampaignDonation
            .Where(d => d.Id == donationId)
            .FirstOrDefaultAsync();

        _context.MissionCampaignDonation.Remove(donation);

        return donation;
    }

    public async Task<MissionCampaignDonation> GetByGuid(Guid guid)
    {
        var donation = await _context.MissionCampaignDonation
            .Where(d => d.Guid == guid)
            .FirstOrDefaultAsync();

        return donation;
    }

    public async Task<PagedResult<MissionCampaignDonation>> ListByCampaign(int churchId, int campaignId, int pageNumber, int pageSize)
    {
        var query = _context.MissionCampaignDonation
            .Include(d => d.User)
            .Where(d => d.CampaignId == campaignId && d.Campaign.ChurchId == churchId)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<MissionCampaignDonation>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<IEnumerable<MissionCampaignDonation>> ListByUser(int churchId, string userId)
    {
        var list = await _context.MissionCampaignDonation
            .Where(d => d.UserId == userId && d.Campaign.ChurchId == churchId)
            .ToListAsync();

        return list;
    }
}
