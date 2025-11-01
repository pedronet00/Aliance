using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class MissionCampaignService : IMissionCampaignService
{

    private readonly IMissionCampaignRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;

    public MissionCampaignService(IMissionCampaignRepository repo, IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContext)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<MissionCampaignViewModel>> AddAsync(MissionCampaignDTO missionCampaign)
    {
        var result = new DomainNotificationsResult<MissionCampaignViewModel>();

        var campaignEntity = _mapper.Map<MissionCampaign>(missionCampaign);

        var insert = await _repo.AddAsync(campaignEntity);

        if(insert is null)
        {
            result.Notifications.Add("Houve um erro ao adicionar a campanha.");
            return result;
        }

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<MissionCampaignViewModel>(campaignEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteAsync(Guid guid)
    {
        var result = new DomainNotificationsResult<bool>();

        await _repo.DeleteAsync(guid);

        await _unitOfWork.Commit();

        result.Result = true;

        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<MissionCampaignViewModel>>> GetAllAsync(int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<MissionCampaignViewModel>>();
        var churchId = _userContext.GetChurchId();

        var campaigns = await _repo.GetAllAsync(churchId, pageNumber, pageSize);

        var campaignsVMs = _mapper.Map<IEnumerable<MissionCampaignViewModel>>(campaigns.Items);

        result.Result = new PagedResult<MissionCampaignViewModel>(
                campaignsVMs,
                campaigns.TotalCount,
                campaigns.CurrentPage,
                campaigns.PageSize
            );

        return result;
    }

    public async Task<DomainNotificationsResult<MissionCampaignViewModel>> GetByGuidAsync(Guid guid)
    {
        var result = new DomainNotificationsResult<MissionCampaignViewModel>();
        var churchId = _userContext.GetChurchId();

        var campaign = await _repo.GetByGuidAsync(churchId, guid);

        result.Result = _mapper.Map<MissionCampaignViewModel>(campaign);

        return result;
    }

    public async Task<DomainNotificationsResult<MissionCampaignViewModel>> UpdateAsync(MissionCampaignDTO missionCampaign)
    {
        var result = new DomainNotificationsResult<MissionCampaignViewModel>();
        var churchId = _userContext.GetChurchId();

        var existingCampaign = await _repo.GetByGuidAsync(churchId, missionCampaign.Guid);

        if (existingCampaign == null)
        {
            result.Notifications.Add("Campanha não encontrada.");
            return result;
        }

        _mapper.Map(missionCampaign, existingCampaign);

        await _repo.UpdateAsync(existingCampaign);
        var commit = await _unitOfWork.Commit();

        if (!commit)
        {
            result.Notifications.Add("Houve um erro ao tentar atualizar a campanha.");
            return result;
        }

        result.Result = _mapper.Map<MissionCampaignViewModel>(existingCampaign);
        return result;
    }
}
