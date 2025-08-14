using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;

namespace Aliance.Application.Interfaces;

public interface ICellService
{
    Task<IEnumerable<CellViewModel>> GetAllCells();

    Task<CellViewModel> GetCellById(int id);

    Task<CellViewModel> AddCell(CellDTO cell);

    Task<bool> UpdateCell(CellDTO cell);

    Task<bool> DeleteCell(int id);
}
