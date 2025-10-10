using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;

namespace Aliance.Application.Interfaces;

public interface ICellService
{
    Task<IEnumerable<CellViewModel>> GetAllCells();

    Task<DomainNotificationsResult<CellViewModel>> GetCellById(Guid guid);

    Task<DomainNotificationsResult<CellViewModel>> AddCell(CellDTO cell);

    Task<DomainNotificationsResult<CellViewModel>> UpdateCell(CellDTO cell);

    Task<DomainNotificationsResult<bool>> DeleteCell(Guid guid);
}
