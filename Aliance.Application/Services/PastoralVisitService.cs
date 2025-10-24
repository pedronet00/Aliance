using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;

namespace Aliance.Application.Services;

public class PastoralVisitService : IPastoralVisitService
{
    private readonly IMapper _mapper;
    private readonly IPastoralVisitRepository _repo;
    private readonly IUserContextService _context;
    private readonly IUnitOfWork _uow;

    public PastoralVisitService(
        IMapper mapper,
        IPastoralVisitRepository repo,
        IUserContextService context,
        IUnitOfWork uow)
    {
        _mapper = mapper;
        _repo = repo;
        _context = context;
        _uow = uow;
    }

    public async Task<DomainNotificationsResult<PastoralVisitViewModel>> AddPastoralVisit(PastoralVisitDTO visit)
    {
        var result = new DomainNotificationsResult<PastoralVisitViewModel>();

        if (visit is null)
        {
            result.Notifications.Add("Visita vazia");
            return result;
        }

        var churchId = _context.GetChurchId();
        var entity = _mapper.Map<PastoralVisit>(visit);
        entity.ChurchId = churchId;

        var added = await _repo.AddPastoralVisit(entity);
        await _uow.Commit();

        result.Result = _mapper.Map<PastoralVisitViewModel>(added);
        return result;
    }

    public async Task<DomainNotificationsResult<PastoralVisitViewModel>> DeletePastoralVisit(Guid guid)
    {
        var result = new DomainNotificationsResult<PastoralVisitViewModel>();

        var churchId = _context.GetChurchId();
        var deleted = await _repo.DeletePastoralVisit(churchId, guid);

        if (deleted is null)
        {
            result.Notifications.Add("Visita não encontrada");
            return result;
        }

        await _uow.Commit();
        result.Result = _mapper.Map<PastoralVisitViewModel>(deleted);
        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<PastoralVisitViewModel>>> GetAllVisits(int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<PastoralVisitViewModel>>();

        var churchId = _context.GetChurchId();
        var visits = await _repo.GetAllVisits(churchId, pageNumber, pageSize);

        var visitsVMs = _mapper.Map<IEnumerable<PastoralVisitViewModel>>(visits.Items);

        result.Result = new PagedResult<PastoralVisitViewModel>(
                visitsVMs,
                visits.TotalCount,
                visits.CurrentPage,
                visits.PageSize
            );
        return result;
    }

    public async Task<DomainNotificationsResult<PastoralVisitViewModel>> GetPastoralVisitByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<PastoralVisitViewModel>();

        var churchId = _context.GetChurchId();
        var visit = await _repo.GetPastoralVisitByGuid(churchId, guid);

        if (visit is null)
        {
            result.Notifications.Add("Visita não encontrada");
            return result;
        }

        result.Result = _mapper.Map<PastoralVisitViewModel>(visit);
        return result;
    }

    public async Task<DomainNotificationsResult<PastoralVisitViewModel>> TogglePastoralVisitStatus(Guid guid, MeetingStatus status)
    {
        var result = new DomainNotificationsResult<PastoralVisitViewModel>();

        var churchId = _context.GetChurchId();
        var updated = await _repo.TogglePastoralVisitStatus(churchId, guid, status);

        if (updated is null)
        {
            result.Notifications.Add("Visita não encontrada");
            return result;
        }

        await _uow.Commit();
        result.Result = _mapper.Map<PastoralVisitViewModel>(updated);
        return result;
    }

    public async Task<DomainNotificationsResult<PastoralVisitViewModel>> UpdatePastoralVisit(PastoralVisitDTO visit)
    {
        var result = new DomainNotificationsResult<PastoralVisitViewModel>();

        if (visit is null)
        {
            result.Notifications.Add("Visita vazia");
            return result;
        }

        var churchId = _context.GetChurchId();
        var entity = _mapper.Map<PastoralVisit>(visit);

        var updated = await _repo.UpdatePastoralVisit(churchId, entity);

        if (updated is null)
        {
            result.Notifications.Add("Visita não encontrada");
            return result;
        }

        await _uow.Commit();
        result.Result = _mapper.Map<PastoralVisitViewModel>(updated);
        return result;
    }
}
