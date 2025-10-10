using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services
{
    public class CostCenterService : ICostCenterService
    {

        private readonly ICostCenterRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _context;

        public CostCenterService(ICostCenterRepository repo, IMapper mapper, IUnitOfWork unitOfWork, IUserContextService context)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<DomainNotificationsResult<CostCenterViewModel>> Activate(Guid guid)
        {
            var result = new DomainNotificationsResult<CostCenterViewModel>();
            var churchId = _context.GetChurchId();

            if (result.Notifications.Any())
                return result;

            var costCenter = await _repo.GetByGuid(churchId, guid);

            if (costCenter is null)
                result.Notifications.Add("Centro de custo não encontrado.");

            await _repo.Activate(churchId, guid);

            await _unitOfWork.Commit();

            result.Result = _mapper.Map<CostCenterViewModel>(costCenter);

            return result;
        }
        public async Task<DomainNotificationsResult<CostCenterViewModel>> Add(CostCenterDTO costCenterDTO)
        {
            var result = new DomainNotificationsResult<CostCenterViewModel>();

            if (costCenterDTO is null)
                result.Notifications.Add("Centro de custo não localizado.");

            if (result.Notifications.Any())
                return result;

            var costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            await _repo.Add(costCenter);

            await _unitOfWork.Commit();

            result.Result = _mapper.Map<CostCenterViewModel>(costCenter);

            return result;
        }

        public async Task<DomainNotificationsResult<CostCenterViewModel>> Deactivate(Guid guid)
        {
            var result = new DomainNotificationsResult<CostCenterViewModel>();
            var churchId = _context.GetChurchId();

            var costCenter = await _repo.GetByGuid(churchId, guid);

            if (costCenter is null)
                result.Notifications.Add("Centro de custo não encontrado.");

            if (result.Notifications.Any())
                return result;

            await _repo.Deactivate(churchId, guid);

            await _unitOfWork.Commit();

            result.Result = _mapper.Map<CostCenterViewModel>(costCenter);

            return result;
        }

        public async Task<DomainNotificationsResult<bool>> Delete(Guid guid)
        {
            var result = new DomainNotificationsResult<bool>();
            var churchId = _context.GetChurchId();

            if(result.Notifications.Any())
                return result;

            await _repo.Delete(churchId, guid);

            await _unitOfWork.Commit();

            result.Result = true;

            return result;
        }

        public async Task<IEnumerable<CostCenterViewModel>> GetAllCenters()
        {
            var churchId = _context.GetChurchId();

            var centers = await _repo.GetAllCenters(churchId);

            var costCenterViewModels = _mapper.Map<IEnumerable<CostCenterViewModel>>(centers);

            return costCenterViewModels;
        }

        public async Task<DomainNotificationsResult<CostCenterViewModel>> GetByGuid(Guid guid)
        {
            var result = new DomainNotificationsResult<CostCenterViewModel>();
            var churchId = _context.GetChurchId();

            var center = await _repo.GetByGuid(churchId, guid);

            if (center is null)
                result.Notifications.Add("Centro de custo não encontrado.");

            if (result.Notifications.Any())
                return result;

            result.Result = _mapper.Map<CostCenterViewModel>(center);

            return result;
        }

        public async Task<DomainNotificationsResult<CostCenterViewModel>> Update(CostCenterDTO costCenterDTO)
        {
            var result = new DomainNotificationsResult<CostCenterViewModel>();
            var churchId = _context.GetChurchId();

            if (costCenterDTO is null)
                result.Notifications.Add("Centro de custo não localizado.");

            var costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            if (costCenter.Id <= 0)
                result.Notifications.Add("Id inválido.");

            if(result.Notifications.Any())
                return result;

            await _repo.Update(churchId, costCenter);

            await _unitOfWork.Commit();

            result.Result = _mapper.Map<CostCenterViewModel>(costCenter);

            return result;

        }
    }
}
