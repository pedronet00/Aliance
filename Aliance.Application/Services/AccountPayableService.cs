using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services
{
    public class AccountPayableService : IAccountPayableService
    {
        private readonly IAccountPayableRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountPayableService(IAccountPayableRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountPayableViewModel> AddAsync(AccountPayableDTO accountPayable)
        {
            if (accountPayable is null)
                throw new ArgumentNullException(nameof(accountPayable), "Account payable cannot be null.");

            var entity = _mapper.Map<AccountPayableDTO, AccountPayable>(accountPayable);

            var addedEntity = await _repo.AddAsync(entity);
            
            await _unitOfWork.Commit();
            
            return _mapper.Map<AccountPayable, AccountPayableViewModel>(addedEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {  
            var accountPayable = await _repo.GetByIdAsync(id);
            
            if (accountPayable is null)
                return false;
            
            var result = await _repo.DeleteAsync(id);
            
            if (result)
                await _unitOfWork.Commit();
            
            return result;
        }

        public async Task<IEnumerable<AccountPayableViewModel>> GetAllAsync()
        {
            var accountPayables = await _repo.GetAllAsync();
            
            return _mapper.Map<IEnumerable<AccountPayable>, IEnumerable<AccountPayableViewModel>>(accountPayables);
        }

        public async Task<AccountPayableViewModel?> GetByIdAsync(int id)
        {
            var accountPayable = await _repo.GetByIdAsync(id);
            
            if (accountPayable is null)
                return null;
            
            return _mapper.Map<AccountPayable, AccountPayableViewModel>(accountPayable);
        }

        public async Task<AccountPayableViewModel> UpdateAsync(AccountPayableDTO accountPayable)
        {
            if (accountPayable is null)
                throw new ArgumentNullException(nameof(accountPayable), "Account payable cannot be null.");
            
            var existingEntity = await _repo.GetByIdAsync(accountPayable.Id);
            
            if (existingEntity is null)
                throw new KeyNotFoundException($"Account payable with ID {accountPayable.Id} not found.");
            
            var entity = _mapper.Map<AccountPayableDTO, AccountPayable>(accountPayable, existingEntity);
            
            var updatedEntity = await _repo.UpdateAsync(entity);
            
            await _unitOfWork.Commit();
            
            return _mapper.Map<AccountPayable, AccountPayableViewModel>(updatedEntity);
        }
    }
}
