using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _userContext;
    private readonly IAccountPayableService _accountPayableService;

    public EventService(IEventRepository repo, IMapper mapper, IUnitOfWork uow, IUserContextService userContext, IAccountPayableService accountPayableService)
    {
        _repo = repo;
        _mapper = mapper;
        _uow = uow;
        _userContext = userContext;
        _accountPayableService = accountPayableService;
    }

    public async Task<DomainNotificationsResult<EventViewModel>> AddEvent(EventDTO newEvent)
    {
        var result = new DomainNotificationsResult<EventViewModel>();

        var eventEntity = _mapper.Map<Domain.Entities.Event>(newEvent);

        await _repo.AddEvent(eventEntity);

        var commit = await _uow.Commit();

        if(!commit)
        {
            result.Notifications.Add("Houve um erro ao tentar adicionar o evento.");
            return result;
        }

        result.Result = _mapper.Map<EventViewModel>(eventEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteEvent(Guid guid)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _userContext.GetChurchId();

        var deleted = await _repo.DeleteEvent(churchId, guid);

        var commit = await _uow.Commit();

        if(!commit || !deleted)
        {
            result.Notifications.Add("Houve um erro ao tentar deletar o evento.");
            result.Result = false;
            return result;
        }

        result.Result = true;
        return result;
    }

    public async Task<DomainNotificationsResult<EventViewModel>> GetEventByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<EventViewModel>();
        var churchId = _userContext.GetChurchId();

        var ev = await _repo.GetEventByGuid(churchId, guid);

        if(ev == null)
        {
            result.Notifications.Add("Evento não encontrado.");
            return result;
        }   

        result.Result = _mapper.Map<EventViewModel>(ev);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<EventViewModel>>> GetEvents()
    {
        var result = new DomainNotificationsResult<IEnumerable<EventViewModel>>();
        var churchId = _userContext.GetChurchId();

        var events = await _repo.GetEvents(churchId);

        result.Result = _mapper.Map<IEnumerable<EventViewModel>>(events);

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<EventViewModel>>> GetEventsByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = new DomainNotificationsResult<IEnumerable<EventViewModel>>();
        var churchId = _userContext.GetChurchId();

        var events = await _repo.GetEventsByDateRange(churchId, startDate, endDate);

        result.Result = _mapper.Map<IEnumerable<EventViewModel>>(events);

        return result;
    }

    public async Task<DomainNotificationsResult<EventViewModel>> GetNextEvent()
    {
        var result = new DomainNotificationsResult<EventViewModel>();
        var churchId = _userContext.GetChurchId();

        var nextEvent = await _repo.GetNextEvent(churchId);

        if(nextEvent == null)
        {
            result.Notifications.Add("Nenhum próximo evento encontrado.");
            return result;
        }

        result.Result = _mapper.Map<EventViewModel>(nextEvent);
        return result;
    }

    public async Task<DomainNotificationsResult<EventViewModel>> ToggleStatus(Guid guid, MeetingStatus status)
    {
        var result = new DomainNotificationsResult<EventViewModel>();
        var churchId = _userContext.GetChurchId();

        var ev = await _repo.GetEventByGuid(churchId, guid);

        if(ev == null)
        {
            result.Notifications.Add("Evento não encontrado.");
            return result;
        }

        switch (status)
        {
            case MeetingStatus.Adiado: 
                await _repo.ToggleStatus(churchId, guid, MeetingStatus.Adiado);
                break;
            case MeetingStatus.Cancelado:
                await _repo.ToggleStatus(churchId, guid, MeetingStatus.Cancelado);
                break;
            case MeetingStatus.Completado:
                await _repo.ToggleStatus(churchId, guid, MeetingStatus.Completado);

                var accountPayable = new AccountPayableDTO
                {
                    Description = $"Conta a pagar gerada automaticamente pelo sistema para o evento: {ev.Name}",
                    Amount = ev.Cost,
                    DueDate = DateTime.Now,
                    CostCenterId = ev.CostCenterId,
                    AccountStatus = AccountStatus.Pendente
                };

                await _accountPayableService.AddAsync(accountPayable);
                break;
            default:
                result.Notifications.Add("Status inválido.");
                return result;
        }

        await _uow.Commit();

        result.Result = _mapper.Map<EventViewModel>(ev);

        return result;
    }

    public async Task<DomainNotificationsResult<EventViewModel>> UpdateEvent(EventDTO eventUpdated)
    {
        var result = new DomainNotificationsResult<EventViewModel>();
        var churchId = _userContext.GetChurchId();

        var existingEvent = await _repo.GetEventByGuid(churchId, eventUpdated.Guid);

        if (existingEvent == null)
        {
            result.Notifications.Add("Evento não encontrado.");
            return result;
        }

        _mapper.Map(eventUpdated, existingEvent);

        await _repo.UpdateEvent(existingEvent);
        var commit = await _uow.Commit();

        if (!commit)
        {
            result.Notifications.Add("Houve um erro ao tentar atualizar o evento.");
            return result;
        }

        result.Result = _mapper.Map<EventViewModel>(existingEvent);
        return result;
    }

}
