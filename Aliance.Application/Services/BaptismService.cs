using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class BaptismService : IBaptismService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBaptismRepository _repo;

    public BaptismService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IBaptismRepository repo)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<bool> DeleteBaptism(int id)
    {
        var result = await _repo.Delete(id);

        await _unitOfWork.Commit();

        return result;
    }

    public async Task<IEnumerable<BaptismViewModel>> GetAllBaptisms()
    {
        var baptisms = await _repo.GetAllBaptisms();

        var baptismVMs = _mapper.Map<IEnumerable<BaptismViewModel>>(baptisms);

        return baptismVMs;
    }

    public async Task<BaptismViewModel> GetBaptismById(int id)
    {
        var baptism = await _repo.GetById(id);

        if(baptism is null)
            throw new KeyNotFoundException("Baptism not found");

        var baptismVM = _mapper.Map<BaptismViewModel>(baptism);

        return baptismVM;
    }

    public async Task<BaptismViewModel> InsertBaptism(BaptismDTO baptismDTO)
    {

        if(baptismDTO is null)
            throw new ArgumentNullException(nameof(baptismDTO), "BaptismDTO cannot be null");

        var pastor = await _userManager.FindByIdAsync(baptismDTO.PastorId!);

        if (pastor is null)
            throw new KeyNotFoundException("Pastor not found");

        var user = await _userManager.FindByIdAsync(baptismDTO.UserId!);

        if (user is null)
            throw new KeyNotFoundException("User not found");

        if(user.ChurchId != pastor.ChurchId)
            throw new ArgumentException("User and Pastor must belong to the same church", nameof(baptismDTO));

        if (baptismDTO.UserId == baptismDTO.PastorId)
            throw new ArgumentException("User and Pastor cannot be the same", nameof(baptismDTO));

        var baptismEntity = _mapper.Map<Baptism>(baptismDTO);

        var result = await _repo.Add(baptismEntity);

        await _unitOfWork.Commit();

        return _mapper.Map<BaptismViewModel>(result);

    }

    public async Task<bool> UpdateBaptism(BaptismDTO baptismDTO)
    {
        if (baptismDTO is null)
            throw new ArgumentNullException(nameof(baptismDTO), "BaptismDTO cannot be null");

        var pastor = await _userManager.FindByIdAsync(baptismDTO.PastorId!);

        if (pastor is null)
            throw new KeyNotFoundException("Pastor not found");

        var user = await _userManager.FindByIdAsync(baptismDTO.UserId!);

        if (user is null)
            throw new KeyNotFoundException("User not found");

        if (user.ChurchId != pastor.ChurchId)
            throw new ArgumentException("User and Pastor must belong to the same church", nameof(baptismDTO));

        if (baptismDTO.UserId == baptismDTO.PastorId)
            throw new ArgumentException("User and Pastor cannot be the same", nameof(baptismDTO));

        var baptismEntity = _mapper.Map<Baptism>(baptismDTO);

        _repo.Update(baptismEntity);

        await _unitOfWork.Commit();

        return true;
    }
}
