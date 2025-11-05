using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class MissionCampaignDonationService : IMissionCampaignDonationService
{
    private readonly IMissionCampaignDonationRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _userContext;
    private readonly IMissionCampaignRepository _campaignRepo;

    public MissionCampaignDonationService(IMissionCampaignDonationRepository repo, IMapper mapper, IUnitOfWork uow, IUserContextService userContext, IMissionCampaignRepository campaignRepo)
    {
        _repo = repo;
        _mapper = mapper;
        _uow = uow;
        _userContext = userContext;
        _campaignRepo = campaignRepo;
    }

    public async Task<DomainNotificationsResult<MissionCampaignDonationViewModel>> AddDonation(MissionCampaignDonationDTO donation)
    {
        var result = new DomainNotificationsResult<MissionCampaignDonationViewModel>();
        var churchId = _userContext.GetChurchId();

        var entity = _mapper.Map<MissionCampaignDonation>(donation);

        var campaign = await _campaignRepo.GetByGuidAsync(churchId, donation.CampaignGuid);

        entity.CampaignId = campaign!.Id;

        var insert = await _repo.AddDonation(entity);

        await _uow.Commit();

        campaign.CollectedAmount = campaign.CollectedAmount + donation.Amount;

        await _campaignRepo.UpdateAsync(campaign);

        await _uow.Commit();

        result.Result = _mapper.Map<MissionCampaignDonationViewModel>(insert);

        return result;
    }

    public async Task<DomainNotificationsResult<MissionCampaignDonationViewModel>> DeleteDonation(Guid donationGuid)
    {
        var result = new DomainNotificationsResult<MissionCampaignDonationViewModel>();

        var entity = await _repo.GetByGuid(donationGuid);

        if(entity == null)
        {
            result.Notifications.Add("Doação para campanha não encontrada.");
        }

        var delete = await _repo.DeleteDonation(entity.Id);

        await _uow.Commit();

        result.Result = _mapper.Map<MissionCampaignDonationViewModel>(entity);

        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<MissionCampaignDonationViewModel>>> ListByCampaign(Guid campaignGuid, int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<MissionCampaignDonationViewModel>>();
        var churchId = _userContext.GetChurchId();

        var campaign = await _campaignRepo.GetByGuidAsync(churchId, campaignGuid);

        if(campaign == null)
        {
            result.Notifications.Add("Campanhã não existe.");
            return result;
        }

        var donations = await _repo.ListByCampaign(churchId, campaign.Id, pageNumber, pageSize);

        var donationsVMs = _mapper.Map<IEnumerable<MissionCampaignDonationViewModel>>(donations.Items);


        result.Result = new PagedResult<MissionCampaignDonationViewModel>(
                donationsVMs,
                donations.TotalCount,
                donations.CurrentPage,
                donations.PageSize
            );

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<MissionCampaignDonationViewModel>>> ListByUser(string userId)
    {
        var result = new DomainNotificationsResult<IEnumerable<MissionCampaignDonationViewModel>>();
        var churchId = _userContext.GetChurchId();

        var donations = await _repo.ListByUser(churchId, userId);

        result.Result = _mapper.Map<IEnumerable<MissionCampaignDonationViewModel>>(donations);

        return result;
    }
}
