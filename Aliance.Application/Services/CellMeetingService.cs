using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services;

public class CellMeetingService : ICellMeetingService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly ICellMeetingRepository _repo;
    private readonly IUserContextService _userContext;
    private readonly ICellRepository _cellRepository;
    private readonly ILocationRepository _locationRepository;

    public CellMeetingService(IMapper mapper, IUnitOfWork uow, ICellMeetingRepository repo, IUserContextService userContext, ICellRepository cellRepository, ILocationRepository locationRepository)
    {
        _mapper = mapper;
        _uow = uow;
        _repo = repo;
        _userContext = userContext;
        _cellRepository = cellRepository;
        _locationRepository = locationRepository;
    }

    public async Task<DomainNotificationsResult<CellMeetingViewModel>> DeleteCellMeeting(Guid guid)
    {
        var result = new DomainNotificationsResult<CellMeetingViewModel>();
        var churchId = _userContext.GetChurchId();

        var meeting = await _repo.GetCellMeetingByGuid(churchId, guid);

        if (meeting == null)
        {
            result.Notifications.Add("Encontro da célula não existe.");
            return result;
        }

        await _repo.DeleteCellMeeting(churchId, guid);

        await _uow.Commit();

        result.Result = _mapper.Map<CellMeetingViewModel>(meeting);

        return result;

    }

    public async Task<DomainNotificationsResult<CellMeetingViewModel>> GetCellMeetingByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<CellMeetingViewModel>();
        var churchId = _userContext.GetChurchId();

        var meeting = await _repo.GetCellMeetingByGuid(churchId, guid);

        if (meeting == null)
        {
            result.Notifications.Add("Encontro da célula não existe.");
            return result;
        }

        result.Result = _mapper.Map<CellMeetingViewModel>(meeting);

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<CellMeetingViewModel>>> GetCellMeetings(Guid cellGuid)
    {
        var result = new DomainNotificationsResult<IEnumerable<CellMeetingViewModel>>();
        var churchId = _userContext.GetChurchId();

        var meetings = await _repo.GetCellMeetings(churchId, cellGuid);

        result.Result = _mapper.Map<IEnumerable<CellMeetingViewModel>>(meetings);

        return result;
    }

    public async Task<DomainNotificationsResult<CellMeetingViewModel>> GetNextCellMeeting()
    {
        var result = new DomainNotificationsResult<CellMeetingViewModel>();
        var churchId = _userContext.GetChurchId();

        var nextMeeting = await _repo.GetNextCellMeeting(churchId);

        if(nextMeeting is null)
        {
            result.Notifications.Add("Nenhuma próxima reunião para esta célula.");
            return result;
        }

        result.Result = _mapper.Map<CellMeetingViewModel>(nextMeeting);

        return result;
    }

    public async Task<DomainNotificationsResult<CellMeetingViewModel>> InsertCellMeeting(CellMeetingDTO cellMeeting)
    {
        var result = new DomainNotificationsResult<CellMeetingViewModel>();
        var churchId = _userContext.GetChurchId();

        // Cria a entidade diretamente usando os GUIDs
        var meetingEntity = new CellMeeting
        {
            Theme = cellMeeting.Theme,
            Date = cellMeeting.Date,
            CellGuid = cellMeeting.CellGuid,         // GUID da célula
            LocationGuid = cellMeeting.LocationGuid, // GUID do local
            LeaderGuid = cellMeeting.LeaderGuid, // GUID do líder
            Status = Domain.Enums.MeetingStatus.Agendado
        };

        // Insere o registro
        var insert = await _repo.InsertCellMeeting(meetingEntity);
        if (insert == null)
        {
            result.Notifications.Add("Houve um erro ao inserir a reunião de célula.");
            return result;
        }

        await _uow.Commit();

        // Mapeia a entidade persistida para ViewModel
        result.Result = _mapper.Map<CellMeetingViewModel>(insert);

        return result;
    }

    public async Task<DomainNotificationsResult<CellMeetingViewModel>> ToggleStatus(Guid guid, MeetingStatus status)
    {
        var result = new DomainNotificationsResult<CellMeetingViewModel>();
        var churchId = _userContext.GetChurchId();

        var meeting = await _repo.GetCellMeetingByGuid(churchId, guid);

        if (meeting == null) 
        {
            result.Notifications.Add("Encontro não existe.");
            return result;
        }

        var meetingEntity = _mapper.Map<CellMeeting>(meeting);

        meetingEntity.Status = status;

        await _repo.UpdateCellMeeting(churchId, meetingEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<CellMeetingViewModel>(meetingEntity);

        return result;

    }

    public async Task<DomainNotificationsResult<CellMeetingViewModel>> UpdateCellMeeting(CellMeetingDTO cellMeeting)
    {
        var result = new DomainNotificationsResult<CellMeetingViewModel>();
        var churchId = _userContext.GetChurchId();

        // 1. Buscar a entidade existente
        var entity = await _repo.GetCellMeetingByGuid(churchId, cellMeeting.Guid);
        if (entity == null)
        {
            result.Notifications.Add("Registro não encontrado");
            return result;
        }

        // 2. Atualizar os campos
        entity.Theme = cellMeeting.Theme;
        entity.Date = cellMeeting.Date;
        entity.CellGuid = cellMeeting.CellGuid;
        entity.LocationGuid = cellMeeting.LocationGuid;
        entity.LeaderGuid = cellMeeting.LeaderGuid;
        entity.Status = Domain.Enums.MeetingStatus.Agendado;

        // 3. Salvar alterações
        await _repo.UpdateCellMeeting(churchId, entity); // ou apenas Commit se o repo já estiver trackeando a entidade
        await _uow.Commit();

        result.Result = _mapper.Map<CellMeetingViewModel>(entity);
        return result;
    }

}
