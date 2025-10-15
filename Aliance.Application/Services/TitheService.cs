using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services;

public class TitheService : ITitheService
{
    private readonly IMapper _mapper;
    private readonly ITitheRepository _repo;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _userContext;

    public TitheService(
        IMapper mapper,
        ITitheRepository repo,
        IIncomeRepository incomeRepository,
        IUnitOfWork uow,
        IUserContextService userContext)
    {
        _mapper = mapper;
        _repo = repo;
        _incomeRepository = incomeRepository;
        _uow = uow;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<TitheViewModel>> InsertTithe(TitheDTO tithe)
    {
        var result = new DomainNotificationsResult<TitheViewModel>();
        var churchId = _userContext.GetChurchId();

        // Cria e persiste o dízimo
        var titheEntity = _mapper.Map<Tithe>(tithe);
        await _repo.AddTithe(titheEntity);

        // Gera a descrição da entrada
        string incomeDescription = $"Dízimo de {titheEntity.User?.UserName ?? "membro"} ({titheEntity.UserId})";

        // Cria a entrada financeira (Income)
        var income = new Income(
            description: incomeDescription,
            amount: titheEntity.Amount,
            accountReceivableId: null,
            category: FinancialIncomingCategory.Dizimo,
            churchId: titheEntity.ChurchId
        );

        await _incomeRepository.InsertIncome(income);

        await _uow.Commit();

        result.Result = _mapper.Map<TitheViewModel>(titheEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<TitheViewModel>> UpdateTithe(TitheDTO tithe)
    {
        var result = new DomainNotificationsResult<TitheViewModel>();
        var churchId = _userContext.GetChurchId();
        var existingTithe = await _repo.GetTitheByGuid(churchId, tithe.Guid);

        if (existingTithe == null)
        {
            result.Notifications.Add("Dízimo não encontrado.");
            return result;
        }

        var titheEntity = _mapper.Map<Tithe>(tithe);
        await _repo.UpdateTithe(titheEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<TitheViewModel>(titheEntity);
        return result;
    }

    public async Task<DomainNotificationsResult<TitheViewModel>> DeleteTithe(Guid guid)
    {
        var result = new DomainNotificationsResult<TitheViewModel>();
        var churchId = _userContext.GetChurchId();

        await _repo.DeleteTithe(churchId, guid);
        await _uow.Commit();

        result.Result = _mapper.Map<TitheViewModel>(await _repo.GetTitheByGuid(churchId, guid));
        return result;
    }

    public async Task<DomainNotificationsResult<TitheViewModel>> GetTitheByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<TitheViewModel>();
        var churchId = _userContext.GetChurchId();
        var tithe = await _repo.GetTitheByGuid(churchId, guid);

        if (tithe == null)
        {
            result.Notifications.Add("Dízimo não encontrado.");
            return result;
        }

        result.Result = _mapper.Map<TitheViewModel>(tithe);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<TitheViewModel>>> GetTithes()
    {
        var result = new DomainNotificationsResult<IEnumerable<TitheViewModel>>();
        var churchId = _userContext.GetChurchId();

        var tithes = await _repo.GetTithes(churchId);
        result.Result = _mapper.Map<IEnumerable<TitheViewModel>>(tithes);

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<TitheViewModel>>> GetTithesByUser(Guid userId)
    {
        var result = new DomainNotificationsResult<IEnumerable<TitheViewModel>>();
        var churchId = _userContext.GetChurchId();

        var tithes = await _repo.GetTithesByUser(churchId, userId.ToString());
        result.Result = _mapper.Map<IEnumerable<TitheViewModel>>(tithes);

        return result;
    }

    public async Task<DomainNotificationsResult<decimal>> GetTotalTithes()
    {
        var result = new DomainNotificationsResult<decimal>();
        var churchId = _userContext.GetChurchId();
        var total = await _repo.GetTotalTithes(churchId);
        result.Result = total;
        return result;
    }

    public async Task<DomainNotificationsResult<decimal>> GetTotalTithesByUser(Guid userId)
    {
        var result = new DomainNotificationsResult<decimal>();
        var churchId = _userContext.GetChurchId();
        var total = await _repo.GetTotalTithesByUser(churchId, userId.ToString());
        result.Result = total;
        return result;
    }
}
