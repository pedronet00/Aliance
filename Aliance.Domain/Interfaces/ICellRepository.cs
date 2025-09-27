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
        Task<IEnumerable<Cell>> GetAllCells(int churchId);

        Task<Cell> GetCellById(int churchId, int id);

        Task<Cell> AddCell(Cell cell);

        Task<Cell> UpdateCell(Cell cell);

        Task<bool> DeleteCell(int churchId,int id);
    }
}
