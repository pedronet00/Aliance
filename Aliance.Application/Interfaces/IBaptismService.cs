using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;

namespace Aliance.Application.Interfaces;

public interface IBaptismService
{
    Task<IEnumerable<BaptismViewModel>> GetAllBaptisms();

    Task<BaptismViewModel> GetBaptismById(int id);

    Task<BaptismViewModel> InsertBaptism(BaptismDTO church);

    Task<bool> UpdateBaptism(BaptismDTO church);

    Task<bool> DeleteBaptism(int id);
}
