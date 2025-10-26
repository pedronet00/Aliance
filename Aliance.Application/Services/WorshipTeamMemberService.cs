using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;

namespace Aliance.Application.Services;

public class WorshipTeamMemberService : IWorshipTeamMemberService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;
    private readonly IWorshipTeamMemberRepository _repo;
    private readonly IWorshipTeamRepository _teamRepo;

    public WorshipTeamMemberService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContext, IWorshipTeamMemberRepository repo, IWorshipTeamRepository teamRepo)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userContext = userContext;
        _repo = repo;
        _teamRepo = teamRepo;
    }

    public async Task<DomainNotificationsResult<WorshipTeamMemberViewModel>> DeleteWorshipTeamMember(Guid teamGuid, string memberId)
    {
        var result = new DomainNotificationsResult<WorshipTeamMemberViewModel>();
        var churchId = _userContext.GetChurchId();

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, teamGuid);

        if (team is null)
        {
            result.Notifications.Add("Grupo de louvor não encontrado.");
            return result;
        }

        var member = await _repo.DeleteWorshipTeamMember(team.Id, memberId);

        if (member is null)
        {
            result.Notifications.Add("Membro não encontrado no grupo de louvor.");
            return result;
        }

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<WorshipTeamMemberViewModel>(member);

        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<WorshipTeamMemberViewModel>>> GetWorshipTeamMembers(Guid teamGuid, int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<WorshipTeamMemberViewModel>>();
        var churchId = _userContext.GetChurchId();

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, teamGuid);
        if (team is null)
        {
            result.Notifications.Add("Grupo de louvor não encontrado.");
            return result;
        }

        var pagedMembers = await _repo.GetWorshipTeamMembers(churchId, team.Id, pageNumber, pageSize);

        var mappedMembers = _mapper.Map<IEnumerable<WorshipTeamMemberViewModel>>(pagedMembers.Items);

        result.Result = new PagedResult<WorshipTeamMemberViewModel>(mappedMembers, pagedMembers.TotalCount, pageNumber, pageSize);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamMemberViewModel>> InsertWorshipTeamMember(Guid teamGuid, string memberId)
    {
        var result = new DomainNotificationsResult<WorshipTeamMemberViewModel>();

        var churchId = _userContext.GetChurchId();

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, teamGuid);

        if (team is null)
        {
            result.Notifications.Add("Grupo de louvor não encontrado.");
            return result;
        }

        var validateMemberAlreadyInGroup = await _repo.MemberAlreadyInGroup(team.Id, memberId);

        if (validateMemberAlreadyInGroup)
        {
            result.Notifications.Add("Membro já está no grupo.");
            return result;
        }

        var member = await _repo.InsertWorshipTeamMember(team.Id, memberId);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<WorshipTeamMemberViewModel>(member);

        return result;
    }

    public async Task<DomainNotificationsResult<WorshipTeamMemberViewModel>> ToggleMemberStatus(Guid teamGuid, string memberId, bool status)
    {
        var result = new DomainNotificationsResult<WorshipTeamMemberViewModel>();
        var churchId = _userContext.GetChurchId();

        var team = await _teamRepo.GetWorshipTeamByGuid(churchId, teamGuid);
        if (team is null)
        {
            result.Notifications.Add("Grupo de louvor não encontrado.");
            return result;
        }

        var member = await _repo.ToggleMemberStatus(churchId, team.Id, memberId, status);

        if (member is null)
        {
            result.Notifications.Add("Membro não encontrado no grupo de louvor.");
            return result;
        }

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<WorshipTeamMemberViewModel>(member);

        return result;
    }
}
