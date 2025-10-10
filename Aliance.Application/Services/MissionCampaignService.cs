using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
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

    public MissionCampaignService(IMissionCampaignRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MissionCampaignViewModel> AddAsync(MissionCampaignDTO missionCampaign)
    {
        if (missionCampaign is null)
            throw new ArgumentNullException(nameof(missionCampaign));

        if(missionCampaign.TargetAmount <= 0)
            throw new ArgumentException("Target amount must be greater than zero.", nameof(missionCampaign.TargetAmount));

        if(missionCampaign.EndDate <= missionCampaign.StartDate)
            throw new ArgumentException("End date must be greater than start date.", nameof(missionCampaign.EndDate));
        
        var entity = _mapper.Map<MissionCampaignDTO, MissionCampaign>(missionCampaign);

        var addedCampaign = await _repo.AddAsync(entity);

        var missionCampaignViewModel = _mapper.Map<MissionCampaign, MissionCampaignViewModel>(addedCampaign);

        await _unitOfWork.Commit();

        return missionCampaignViewModel;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<MissionCampaignViewModel>> GetAllAsync()
    {
        var campaigns = await _repo.GetAllAsync();

        var campaignViewModels = _mapper.Map<IEnumerable<MissionCampaign>, IEnumerable<MissionCampaignViewModel>>(campaigns);

        return campaignViewModels;
    }

    public async Task<MissionCampaignViewModel> GetByIdAsync(int id)
    {
        var campaign = await _repo.GetByIdAsync(id);

        if (campaign is null)
            throw new KeyNotFoundException($"Mission campaign with ID {id} not found.");

        var campaignViewModel = _mapper.Map<MissionCampaign, MissionCampaignViewModel>(campaign);

        return campaignViewModel;
    }

    public async Task<MissionCampaignViewModel> UpdateAsync(MissionCampaignDTO missionCampaign)
    {
        if(missionCampaign is null)
            throw new ArgumentNullException(nameof(missionCampaign));

        if(missionCampaign.TargetAmount <= 0)
            throw new ArgumentException("Target amount must be greater than zero.", nameof(missionCampaign.TargetAmount));

        if(missionCampaign.EndDate <= missionCampaign.StartDate)
            throw new ArgumentException("End date must be greater than start date.", nameof(missionCampaign.EndDate));

        var entity = _mapper.Map<MissionCampaignDTO, MissionCampaign>(missionCampaign);

        var updatedCampaign = await _repo.UpdateAsync(entity);

        if (updatedCampaign is null)
            throw new KeyNotFoundException($"Mission campaign with ID {missionCampaign.Id} not found.");

        await _unitOfWork.Commit();

        var missionCampaignViewModel = _mapper.Map<MissionCampaign, MissionCampaignViewModel>(updatedCampaign);

        return missionCampaignViewModel;
    }
}
