using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces
{
    public interface ICellRepository
    {
        Task<PagedResult<Cell>> GetAllCells(int churchId, int pageNumber, int pageSize);

        Task<Cell> GetCellByGuid(int churchId, Guid guid);

        Task<Cell> AddCell(Cell cell);

        Task<Cell> UpdateCell(Cell cell);

        Task<bool> DeleteCell(int churchId,Guid guid);
    }
}
