using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao salvar alterações no banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado durante a confirmação da transação", ex);
            }
        }

        public async Task Rollback()
        {
            await _context.DisposeAsync();
        }
    }
}
