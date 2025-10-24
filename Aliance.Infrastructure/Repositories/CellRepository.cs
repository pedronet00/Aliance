using Aliance.Application.Interfaces;
using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories
{
    public class CellRepository : ICellRepository
    {

        private readonly AppDbContext _context;

        public CellRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cell> AddCell(Cell cell)
        {
            await _context.Cell.AddAsync(cell);

            return cell;
            
        }

        public async Task<bool> DeleteCell(int churchId, Guid guid)
        {
            var cell = await this.GetCellByGuid(churchId, guid);

            _context.Cell.Remove(cell);

            return true;
        }

        public async Task<PagedResult<Cell>> GetAllCells(int churchId, int pageNumber, int pageSize)
        {
            var query = _context.Cell
                .Where(c => c.ChurchId == churchId)
                .Include(c => c.Location)
                .Include(c => c.Leader)
                .Include(c => c.Church);

            var totalCount = await query.CountAsync();

            var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return new PagedResult<Cell>(items, totalCount, pageNumber, pageSize);

        }

        public async Task<Cell> GetCellByGuid(int churchId, Guid guid)
        {
            var cell = await _context.Cell
                .Where(c => c.ChurchId == churchId)
                .Include(c => c.Location)
                .Include(c => c.Leader)
                .Include(c => c.Church)
                .FirstOrDefaultAsync(c => c.Guid == guid);

            return cell;
        }

        public async Task<Cell> UpdateCell(Cell cell)
        {
            _context.Cell.Update(cell);

            return cell;
        }
    }
}
