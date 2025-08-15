using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using AutoMapper;

namespace Aliance.Application.Services
{
    public class CostCenterService : ICostCenterService
    {

        private readonly ICostCenterRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CostCenterService(ICostCenterRepository repo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CostCenterViewModel> Add(CostCenterDTO costCenterDTO)
        {
            if (costCenterDTO is null)
                throw new ArgumentNullException(nameof(costCenterDTO), "CostCenterDTO cannot be null");

            var costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            await _repo.Add(costCenter);

            await _unitOfWork.Commit();

            var costCenterViewModel = _mapper.Map<CostCenterViewModel>(costCenter);

            return costCenterViewModel;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero");

            await _repo.Delete(id);

            return true;
        }

        public async Task<IEnumerable<CostCenterViewModel>> GetAllCenters()
        {
            var centers = await _repo.GetAllCenters();

            var costCenterViewModels = _mapper.Map<IEnumerable<CostCenterViewModel>>(centers);

            return costCenterViewModels;
        }

        public async Task<CostCenterViewModel> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero");

            var center = await _repo.GetById(id);

            if (center is null)
                throw new KeyNotFoundException($"CostCenter with id {id} not found");

            var costCenterViewModel = _mapper.Map<CostCenterViewModel>(center);

            return costCenterViewModel;
        }

        public async Task<CostCenterViewModel> Update(CostCenterDTO costCenterDTO)
        {
            if (costCenterDTO is null)
                throw new ArgumentNullException(nameof(costCenterDTO), "CostCenterDTO cannot be null");

            var costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            if (costCenter.Id <= 0)
                throw new ArgumentOutOfRangeException(nameof(costCenter.Id), "Id must be greater than zero");

            await _repo.Update(costCenter);

            await _unitOfWork.Commit();

            var costCenterViewModel = _mapper.Map<CostCenterViewModel>(costCenter);

            return costCenterViewModel;

        }
    }
}
