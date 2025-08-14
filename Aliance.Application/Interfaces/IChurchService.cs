﻿using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;

namespace Aliance.Application.Interfaces
{
    public interface IChurchService
    {
        Task<IEnumerable<ChurchViewModel>> GetAllChurches();

        Task<ChurchViewModel> GetChurchById(int id);

        Task<ChurchViewModel> InsertChurch(ChurchDTO church);

        Task<bool> UpdateChurch(ChurchDTO church);

        Task<bool> DeleteChurch(int id);
    }
}
