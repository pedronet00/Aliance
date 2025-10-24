using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces
{
    public interface IChurchRepository
    {
        Task<IEnumerable<Church>> GetChurches();

        Task<string> GetChurchesFirstUserMail(string churchAsaasId);

        Task<Church> InsertChurch(Church church);

        void UpdateChurch(Church church);

        Task<Church> DeleteChurch(int id);

        Task<Church> GetChurchById(int id);

        Task<bool> ChurchAlreadyExists(string cnpj);
        
        Task<Church> GetChurchByAsaasCustomerId(string asaasCustomerId);
    }
}
