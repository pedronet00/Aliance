using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IWorshipTeamRepository
{
    Task<PagedResult<WorshipTeam>> GetAllWorshipTeams(int churchId, int pageNumber, int pageSize);

    Task<WorshipTeam> GetWorshipTeamByGuid(int churchId, Guid guid);

    Task<WorshipTeam> InsertWorshipTeam(WorshipTeam worshipTeam);

    Task<WorshipTeam> UpdateWorshipTeam(int churchId, WorshipTeam worshipTeam);

    Task<bool> DeleteWorshipTeam(int churchId, int id);

    Task<WorshipTeam> ActivateWorshipTeam(int churchId, int id);

    Task<WorshipTeam> DeactivateWorshipTeam(int churchId, int id);
}
