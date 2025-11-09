using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services;

public class CellMemberService : ICellMemberService
{
    private readonly ICellMemberRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _userContext;
    private readonly ICellRepository _cellRepo;

    public CellMemberService(
        ICellMemberRepository repo,
        IUnitOfWork uow,
        IUserContextService userContext,
        IMapper mapper,
        ICellRepository cellRepo)
    {
        _repo = repo;
        _uow = uow;
        _userContext = userContext;
        _mapper = mapper;
        _cellRepo = cellRepo;
    }

    public async Task<DomainNotificationsResult<IEnumerable<CellMemberViewModel>>> GetCellMembers(Guid cellGuid)
    {
        var result = new DomainNotificationsResult<IEnumerable<CellMemberViewModel>>();
        var churchId = _userContext.GetChurchId();

        var members = await _repo.GetCellMembers(churchId, cellGuid);
        result.Result = _mapper.Map<IEnumerable<CellMemberViewModel>>(members);

        return result;
    }

    public async Task<DomainNotificationsResult<CellMemberViewModel>> InsertCellMember(Guid cellGuid, string memberId)
    {
        var result = new DomainNotificationsResult<CellMemberViewModel>();

        var member = await _repo.InsertCellMember(cellGuid, memberId);
        await _uow.Commit();

        result.Result = _mapper.Map<CellMemberViewModel>(member);
        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteCellMember(Guid cellGuid, string memberId)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _userContext.GetChurchId();

        var cell = await _cellRepo.GetCellByGuid(churchId, cellGuid);

        if(cell.LeaderId == memberId)
        {
            result.Notifications.Add("Não é possível remover o líder da célula.");
            return result;
        }

        var member = await _repo.DeleteCellMember(cellGuid, memberId);
        if (member != null)
        {
            await _uow.Commit();
            result.Result = true;
        }
        else
        {
            result.Notifications.Add("O membro não foi encontrado na célula especificada.");
            result.Result = false;
        }

        return result;
    }

    public async Task<DomainNotificationsResult<CellMemberViewModel>> ToggleMemberStatus(Guid cellGuid, string memberId)
    {
        var result = new DomainNotificationsResult<CellMemberViewModel>();
        var churchId = _userContext.GetChurchId();

        var member = await _repo.GetMemberById(churchId, cellGuid, memberId);

        if (member == null)
        {
            result.Notifications.Add("O membro não foi encontrado.");
            return result;
        }

        member.Status = !member.Status;
        await _uow.Commit();

        result.Result = _mapper.Map<CellMemberViewModel>(member);
        
        return result;
    }
}
