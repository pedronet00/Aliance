using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces
{
    public interface ICellRepository
    {
        Task<IEnumerable<Cell>> GetAllCells();

        Task<Cell> GetCellById(int id);

        Task<Cell> AddCell(Cell cell);

        void UpdateCell(Cell cell);

        Task<bool> DeleteCell(int id);
    }
}
