using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class CellService : ICellService
{

    private readonly ICellRepository _cellRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContextService _context;

    public CellService(ICellRepository cellRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserContextService context)
    {
        _cellRepository = cellRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    public async Task<DomainNotificationsResult<CellViewModel>> AddCell(CellDTO cellDTO)
    {
        var result = new DomainNotificationsResult<CellViewModel>();
        var churchId = _context.GetChurchId();

        if (cellDTO is null)
            result.Notifications.Add("Célula inválida.");

        if (result.Notifications.Any())
            return result;

            var cell = _mapper.Map<Cell>(cellDTO);

        await _cellRepository.AddCell(cell);

        await _unitOfWork.Commit();

        var cellVM = _mapper.Map<CellViewModel>(cell);

        result.Result = cellVM;

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteCell(Guid guid)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();


        if(result.Notifications.Any())
            return result;

        await _cellRepository.DeleteCell(churchId, guid);

        await _unitOfWork.Commit();

        result.Result = true;

        return result;
    }

    public async Task<IEnumerable<CellViewModel>> GetAllCells()
    {
        var churchId = _context.GetChurchId();

        var cells = await _cellRepository.GetAllCells(churchId);

        var cellsVM = _mapper.Map<IEnumerable<CellViewModel>>(cells);

        return cellsVM;
    }

    public async Task<DomainNotificationsResult<CellViewModel>> GetCellById(Guid guid)
    {
        var result = new DomainNotificationsResult<CellViewModel>();
        var churchId = _context.GetChurchId();

        var cell = await _cellRepository.GetCellByGuid(churchId, guid);

        if(cell is null)
            result.Notifications.Add("Célula não encontrada.");

        if (result.Notifications.Any())
            return result;

        var cellVM = _mapper.Map<CellViewModel>(cell);

        result.Result = cellVM;

        return result;
    }

    public async Task<DomainNotificationsResult<CellViewModel>> UpdateCell(CellDTO cellDTO)
    {
        var result = new DomainNotificationsResult<CellViewModel>();
        var churchId = _context.GetChurchId();

        if (cellDTO is null)
        {
            result.Notifications.Add("Célula inválida.");
            return result;
        }

        var existingCell = await _cellRepository.GetCellByGuid(churchId, cellDTO.Guid);

        if (existingCell == null)
        {
            result.Notifications.Add("Célula não encontrada.");
            return result;
        }

        existingCell.Name = cellDTO.Name;
        existingCell.MeetingDay = cellDTO.MeetingDay;
        existingCell.LeaderId = cellDTO.LeaderId;
        existingCell.LocationId = cellDTO.LocationId;
        existingCell.CellBanner = cellDTO.CellBanner;

        _cellRepository.UpdateCell(existingCell);
        await _unitOfWork.Commit();

        var cellVM = _mapper.Map<CellViewModel>(existingCell);
        result.Result = cellVM;

        return result;
    }

}
