using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories
{
    public class ChurchRepository : IChurchRepository
    {

        private readonly AppDbContext _context;

        public ChurchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChurchAlreadyExists(string cnpj)
        {
            var church = await _context.Church
                .Where(c => c.CNPJ == cnpj)
                .FirstOrDefaultAsync();

            return church != null;
        }

        public async Task<Church> DeleteChurch(int id)
        {
            var church = await _context.Church.FindAsync(id);

            if (church is null)
                return null;

            _context.Church.Remove(church);

            return church;
        }

        public async Task<Church> GetChurchByAsaasCustomerId(string asaasCustomerId)
        {
            var church = await _context.Church
                .Where(c => c.AsaasCustomerId == asaasCustomerId)
                .FirstOrDefaultAsync();

            return church;
        }

        public async Task<Church> GetChurchById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Id");

            var church = await _context.Church.FindAsync(id);

            return church;
        }

        public async Task<IEnumerable<Church>> GetChurches()
        {
            var churches = await _context.Church.AsNoTracking().ToListAsync();

            return churches;
        }

        public async Task<string> GetChurchesFirstUserMail(string churchAsaasId)
        {
            var church = _context.Church.FirstOrDefaultAsync(c => c.AsaasCustomerId == churchAsaasId);

            var response = church.Result.Email;

            return response;
        }

        public async Task<Church> InsertChurch(Church church)
        {
            if (church is null)
                throw new ArgumentNullException(nameof(church));

            await _context.Church.AddAsync(church);

            return church;
        }

        public void UpdateChurch(Church church)
        {
            if (church is null)
                throw new ArgumentNullException(nameof(church));

            _context.Church.Update(church);
        }
    }
}
