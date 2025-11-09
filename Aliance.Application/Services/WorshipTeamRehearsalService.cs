using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class WorshipTeamRehearsalService : IWorshipTeamRehearsalService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IWorshipTeamRehearsalRepository _repo;
    private readonly IUserContextService _userContext;
    private readonly IWorshipTeamRepository _teamRepo;

    public WorshipTeamRehearsalService(IUnitOfWork uow, IMapper mapper, IWorshipTeamRehearsalRepository repo, IUserContextService userContext, IWorshipTeamRepository teamRepo)
    {
        _uow = uow;
        _mapper = mapper;
        _repo = repo;
        _userContext = userContext;
        _teamRepo = teamRepo;
    }

    public async Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> DeleteWorshipTeamRehearsal(Guid guid)
    {
        var result = new DomainNotificationsResult<WorshipTeamRehearsalViewModel>();
        var churchId = _userContext.GetChurchId();

        await _repo.DeleteRehearsal(churchId, guid);

        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamRehearsalViewModel>(await _repo.GetRehearsalByGuid(churchId, guid));

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> GetNextWorshipTeamRehearsal(Guid teamGuid)
    {
        var result = new DomainNotificationsResult<WorshipTeamRehearsalViewModel>();
        var churchId = _userContext.GetChurchId();

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, teamGuid);

        var nextRehearsal = await _repo.GetNextRehearsal(churchId, team.Id);

        result.Result = _mapper.Map<WorshipTeamRehearsalViewModel>(nextRehearsal);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> GetWorshipTeamRehearsalByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<WorshipTeamRehearsalViewModel>();
        var churchId = _userContext.GetChurchId();

        var rehearsal = await _repo.GetRehearsalByGuid(churchId, guid);

        result.Result = _mapper.Map<WorshipTeamRehearsalViewModel>(rehearsal);

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<WorshipTeamRehearsalViewModel>>> GetWorshipTeamRehearsals(Guid teamGuid)
    {
        var result = new DomainNotificationsResult<IEnumerable<WorshipTeamRehearsalViewModel>>();
        var churchId = _userContext.GetChurchId();
        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, teamGuid);

        var rehearsals = await _repo.GetTeamRehearsals(churchId, team.Id);

        result.Result = _mapper.Map<IEnumerable<WorshipTeamRehearsalViewModel>>(rehearsals);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> InsertWorshipTeamRehearsal(WorshipTeamRehearsalDTO rehearsal)
    {
        var result = new DomainNotificationsResult<WorshipTeamRehearsalViewModel>();
        var churchId = _userContext.GetChurchId();

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, rehearsal.WorshipTeamId);

        var rehearsalEntity = _mapper.Map<WorshipTeamRehearsal>(rehearsal);

        rehearsalEntity.WorshipTeamId = team.Id;

        var newRehearsal = await _repo.InsertRehearsal(rehearsalEntity);
        await _uow.Commit();
        result.Result = _mapper.Map<WorshipTeamRehearsalViewModel>(newRehearsal);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> ToggleStatus(Guid guid, MeetingStatus status)
    {
        var result = new DomainNotificationsResult<WorshipTeamRehearsalViewModel>();
        var churchId = _userContext.GetChurchId();

        var rehearsal = await _repo.GetRehearsalByGuid(churchId, guid);

        if(rehearsal == null)
        {
            result.Notifications.Add("Ensaio não encontrado.");
        }

        var rehearsalEntity = _mapper.Map<WorshipTeamRehearsal>(rehearsal);

        rehearsalEntity.Status = status;

        await _repo.UpdateRehearsal(churchId, rehearsalEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamRehearsalViewModel>(rehearsalEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> UpdateWorshipTeamRehearsal(WorshipTeamRehearsalDTO rehearsal)
    {
        var result = new DomainNotificationsResult<WorshipTeamRehearsalViewModel>();
        var churchId = _userContext.GetChurchId();

        var existing = await _repo.GetRehearsalByGuid(churchId, rehearsal.Guid);
        if (existing == null)
        {
            result.Notifications.Add("Ensaio não encontrado.");
            return result;
        }

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, rehearsal.WorshipTeamId);
        if (team == null)
        {
            result.Notifications.Add("Grupo de louvor não encontrado.");
            return result;
        }

        existing.RehearsalDate = rehearsal.RehearsalDate;

        await _repo.UpdateRehearsal(churchId, existing);
        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamRehearsalViewModel>(existing);
        return result;
    }

}
