using Aliance.Application.Interfaces;
using Aliance.Domain.Entities;
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
            if(cell is null)
                throw new ArgumentNullException(nameof(cell), "Cell cannot be null");

            await _context.Cell.AddAsync(cell);

            return cell;
            
        }

        public async Task<bool> DeleteCell(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid cell ID", nameof(id));

            var cell = await _context.Cell.FindAsync(id);

            if (cell is null)
                throw new KeyNotFoundException("Cell not found");

            _context.Cell.Remove(cell);

            return true;
        }

        public async Task<IEnumerable<Cell>> GetAllCells()
        {
            var cells = await _context.Cell.ToListAsync();

            return cells;
        }

        public async Task<Cell> GetCellById(int id)
        {
            var cell = await _context.Cell
                .Include(c => c.Location)
                .Include(c => c.Leader)
                .Include(c => c.Church)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cell is null)
                throw new KeyNotFoundException("Cell not found");

            return cell;
        }

        public void UpdateCell(Cell cell)
        {
            if (cell is null)
                throw new ArgumentNullException(nameof(cell), "Cell cannot be null");

            if (cell.Id <= 0)
                throw new ArgumentException("Invalid cell ID", nameof(cell.Id));

            _context.Cell.Update(cell);

        }
    }
}
