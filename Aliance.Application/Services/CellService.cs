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

namespace Aliance.Application.Services;

public class CellService : ICellService
{

    private readonly ICellRepository _cellRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CellService(ICellRepository cellRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cellRepository = cellRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CellViewModel> AddCell(CellDTO cellDTO)
    {
        if(cellDTO is null)
            throw new ArgumentNullException(nameof(cellDTO));

        var cell = _mapper.Map<Cell>(cellDTO);

        await _cellRepository.AddCell(cell);

        await _unitOfWork.Commit();

        var cellVM = _mapper.Map<CellViewModel>(cell);

        return cellVM;
    }

    public async Task<bool> DeleteCell(int id)
    {
        if(id < 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        await _cellRepository.DeleteCell(id);

        await _unitOfWork.Commit();

        return true;
    }

    public async Task<IEnumerable<CellViewModel>> GetAllCells()
    {
        var cells = await _cellRepository.GetAllCells();

        var cellsVM = _mapper.Map<IEnumerable<CellViewModel>>(cells);

        return cellsVM;
    }

    public async Task<CellViewModel> GetCellById(int id)
    {
        if(id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        var cell = await _cellRepository.GetCellById(id);

        if(cell is null)
            throw new KeyNotFoundException("Cell not found");

        var cellVM = _mapper.Map<CellViewModel>(cell);

        return cellVM;
    }

    public async Task<bool> UpdateCell(CellDTO cellDTO)
    {
        if (cellDTO is null)
            throw new ArgumentNullException(nameof(cellDTO));

        var existingCell = await _cellRepository.GetCellById(cellDTO.Id);

        if (existingCell == null)
            return false; 

        _mapper.Map(cellDTO, existingCell);

        _cellRepository.UpdateCell(existingCell);
        await _unitOfWork.Commit();

        return true;
    }
}
