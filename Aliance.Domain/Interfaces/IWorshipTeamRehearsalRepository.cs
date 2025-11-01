using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IWorshipTeamRehearsalRepository
{
    Task<IEnumerable<WorshipTeamRehearsal>> GetTeamRehearsals(int churchId, int teamId);

    Task<WorshipTeamRehearsal> GetRehearsalByGuid(int churchId, Guid guid);

    Task<WorshipTeamRehearsal> InsertRehearsal(WorshipTeamRehearsal rehearsal);

    Task<WorshipTeamRehearsal> UpdateRehearsal(int churchId, WorshipTeamRehearsal rehearsal);

    Task<WorshipTeamRehearsal> DeleteRehearsal(int churchId, Guid guid);

    Task<WorshipTeamRehearsal> GetNextRehearsal(int churchId, int teamId);
}
