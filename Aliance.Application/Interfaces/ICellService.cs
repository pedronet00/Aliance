using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;

namespace Aliance.Application.Interfaces;

public interface ICellService
{
    Task<PagedResult<CellViewModel>> GetAllCells(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<CellViewModel>> GetCellById(Guid guid);

    Task<DomainNotificationsResult<CellViewModel>> AddCell(CellDTO cell);

    Task<DomainNotificationsResult<CellViewModel>> UpdateCell(CellDTO cell);

    Task<DomainNotificationsResult<bool>> DeleteCell(Guid guid);
}
