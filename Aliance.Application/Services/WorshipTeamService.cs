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

public class WorshipTeamService : IWorshipTeamService
{
    private readonly IWorshipTeamRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _context;
    private readonly IMapper _mapper;

    public WorshipTeamService(IWorshipTeamRepository repo, IUnitOfWork uow, IUserContextService context, IMapper mapper)
    {
        _repo = repo;
        _uow = uow;
        _context = context;
        _mapper = mapper;
    }

    public async Task<DomainNotificationsResult<WorshipTeamViewModel>> ActivateWorshipTeam(Guid guid)
    {
        var result = new DomainNotificationsResult<WorshipTeamViewModel>();
        var churchId = _context.GetChurchId();

        var worshipTeam = await _repo.GetWorshipTeamByGuid(churchId, guid);

        if (worshipTeam is null)
            result.Notifications.Add("WorshipTeamo não encontrado.");

        if (result.Notifications.Any())
            return result;

        await _repo.ActivateWorshipTeam(churchId, worshipTeam.Id);

        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamViewModel>(worshipTeam);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamViewModel>> DeactivateWorshipTeam(Guid guid)
    {
        var result = new DomainNotificationsResult<WorshipTeamViewModel>();
        var churchId = _context.GetChurchId();

        var worshipTeam = await _repo.GetWorshipTeamByGuid(churchId, guid);

        if (worshipTeam is null)
            result.Notifications.Add("WorshipTeam não encontrado.");

        if (result.Notifications.Any())
            return result;

        await _repo.DeactivateWorshipTeam(churchId, worshipTeam.Id);

        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamViewModel>(worshipTeam);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteWorshipTeam(Guid guid)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var worshipTeam = await _repo.GetWorshipTeamByGuid(churchId, guid);

        if (worshipTeam is null)
            result.Notifications.Add("WorshipTeam não encontrado.");

        if (result.Notifications.Any())
            return result;


        await _repo.DeleteWorshipTeam(churchId, worshipTeam.Id);

        await _uow.Commit();

        result.Result = true;

        return result;
    }

    public async Task<PagedResult<WorshipTeamViewModel>> GetWorshipTeamsPaged(int pageNumber, int pageSize)
    {
        var churchId = _context.GetChurchId();
        var pagedWorshipTeams = await _repo.GetAllWorshipTeams(churchId, pageNumber, pageSize);

        var WorshipTeamVMs = _mapper.Map<IEnumerable<WorshipTeamViewModel>>(pagedWorshipTeams.Items);

        return new PagedResult<WorshipTeamViewModel>(
            WorshipTeamVMs,
            pagedWorshipTeams.TotalCount,
            pagedWorshipTeams.CurrentPage,
            pagedWorshipTeams.PageSize
        );
    }

    public async Task<DomainNotificationsResult<WorshipTeamViewModel>> GetWorshipTeamByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<WorshipTeamViewModel>();
        var churchId = _context.GetChurchId();
        var WorshipTeam = await _repo.GetWorshipTeamByGuid(churchId, guid);

        if (WorshipTeam is null)
            result.Notifications.Add("WorshipTeam não encontrado.");

        if (result.Notifications.Any())
            return result;

        result.Result = _mapper.Map<WorshipTeamViewModel>(WorshipTeam);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamViewModel>> InsertWorshipTeam(WorshipTeamDTO WorshipTeamDTO)
    {
        var result = new DomainNotificationsResult<WorshipTeamViewModel>();

        if (WorshipTeamDTO is null)
            result.Notifications.Add("Dados inválidos.");

        if (result.Notifications.Any())
            return result;

        var WorshipTeam = _mapper.Map<WorshipTeam>(WorshipTeamDTO);

        await _repo.InsertWorshipTeam(WorshipTeam);

        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamViewModel>(WorshipTeam);

        return result;
    }


    public async Task<DomainNotificationsResult<WorshipTeamViewModel>> UpdateWorshipTeam(WorshipTeamDTO WorshipTeamDTO)
    {
        var result = new DomainNotificationsResult<WorshipTeamViewModel>();
        var churchId = _context.GetChurchId();

        if (WorshipTeamDTO is null)
            result.Notifications.Add("Dados inválidos.");

        if (result.Notifications.Any())
            return result;

        var WorshipTeam = _mapper.Map<WorshipTeam>(WorshipTeamDTO);

        await _repo.UpdateWorshipTeam(churchId, WorshipTeam);

        await _uow.Commit();

        result.Result = _mapper.Map<WorshipTeamViewModel>(WorshipTeam);

        return result;
    }
}
