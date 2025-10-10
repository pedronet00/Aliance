using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class PatrimonyMaintenanceService : IPatrimonyMaintenanceService
{
    private readonly IPatrimonyMaintenanceRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private IUserContextService _context;

    public PatrimonyMaintenanceService(IPatrimonyMaintenanceRepository repo, IMapper mapper, IUnitOfWork uow, IUserContextService context)
    {
        _repo = repo;
        _mapper = mapper;
        _uow = uow;
        _context = context;
    }

    public async Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> DeleteMaintenance(Guid guid)
    {
        var churchId = _context.GetChurchId();
        var result = new DomainNotificationsResult<PatrimonyMaintenanceViewModel>();

        var maintenance = await _repo.GetMaintenanceByGuid(churchId, guid);

        if (maintenance is null)
        {
            result.Notifications.Add("Manutenção não encontrada.");
            return result;
        }

        var deletedMaintenance = await _repo.DeleteMaintenance(churchId, guid);

        await _uow.Commit();

        result.Result = _mapper.Map<PatrimonyMaintenanceViewModel>(deletedMaintenance);

        return result;
    }

    public async Task<IEnumerable<PatrimonyMaintenanceViewModel>> GetAllMaintenances()
    {
        var churchId = _context.GetChurchId();

        var maintenances = await _repo.GetAllMaintenances(churchId);

        return _mapper.Map<IEnumerable<PatrimonyMaintenanceViewModel>>(maintenances);
    }

    public async Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> GetMaintenanceByGuid(Guid guid)
    {
        var churchId = _context.GetChurchId();

        var result = new DomainNotificationsResult<PatrimonyMaintenanceViewModel>();

        var maintenance = await _repo.GetMaintenanceByGuid(churchId, guid);

        if (maintenance == null)
        {
            result.Notifications.Add("Manutenção não encontrada.");
            return result;
        }

        result.Result = _mapper.Map<PatrimonyMaintenanceViewModel>(maintenance);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<PatrimonyMaintenanceViewModel>>> GetMaintenancesByPatrimonyGuid(Guid patrimonyGuid)
    {
        var churchId = _context.GetChurchId();
        var result = new DomainNotificationsResult<IEnumerable<PatrimonyMaintenanceViewModel>>();

        var maintenances = await _repo.GetMaintenancesByPatrimonyGuid(churchId, patrimonyGuid);

        result.Result = _mapper.Map<IEnumerable<PatrimonyMaintenanceViewModel>>(maintenances);

        return result;
    }

    public async Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> InsertMaintenance(PatrimonyMaintenanceDTO maintenance)
    {
        var churchId = _context.GetChurchId();
        var result = new DomainNotificationsResult<PatrimonyMaintenanceViewModel>();

        var maintenanceAlreadyExists = await _repo.MaintenanceAlreadyExists(maintenance.MaintenanceDate, maintenance.PatrimonyId);

        if (maintenanceAlreadyExists)
        {
            result.Notifications.Add("Já existe uma manutenção cadastrada para este patrimônio nesta data.");
            return result;
        }

        var maintenanceEntity = _mapper.Map<Domain.Entities.PatrimonyMaintenance>(maintenance);

        var insertedMaintenance = await _repo.InsertMaintenance(maintenanceEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<PatrimonyMaintenanceViewModel>(insertedMaintenance);

        return result;
    }

    public async Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> UpdateMaintenance(PatrimonyMaintenanceDTO maintenance)
    {
        var churchId = _context.GetChurchId();

        var result = new DomainNotificationsResult<PatrimonyMaintenanceViewModel>();

        var existingMaintenance = await _repo.GetMaintenanceByGuid(churchId, maintenance.Guid);

        if (existingMaintenance is null)
        {
            result.Notifications.Add("Manutenção não encontrada.");
            return result;
        }

        var maintenanceAlreadyExists = await _repo.MaintenanceAlreadyExists(maintenance.MaintenanceDate, maintenance.PatrimonyId);

        if (maintenanceAlreadyExists)
        {
            result.Notifications.Add("Já existe uma manutenção cadastrada para este patrimônio nesta data.");
            return result;
        }

        _mapper.Map(maintenance, existingMaintenance);

        await _repo.UpdateMaintenance(churchId, existingMaintenance);
        await _uow.Commit();

        result.Result = _mapper.Map<PatrimonyMaintenanceViewModel>(existingMaintenance);

        return result;
    }


    public async Task<DomainNotificationsResult<PatrimonyMaintenanceDocumentViewModel>> UploadDocumentAsync(Guid maintenanceGuid, IFormFile file)
    {
        var result = new DomainNotificationsResult<PatrimonyMaintenanceDocumentViewModel>();
        var churchId = _context.GetChurchId();

        var maintenance = await _repo.GetMaintenanceByGuid(churchId, maintenanceGuid);
        if (maintenance == null)
        {
            result.Notifications.Add("Manutenção não encontrada.");
            return result;
        }

        if (file == null || file.Length == 0)
        {
            result.Notifications.Add("Arquivo inválido.");
            return result;
        }

        // Salvar arquivo (exemplo simples em FileSystem local, depois pode trocar por Blob/S3)
        var uploadsPath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "uploads",
        "patrimonies",
        maintenance.Patrimony.Guid.ToString(),
        "manutencoes",
        maintenance.Guid.ToString(),
        "arquivos"
);
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Criar documento
        var document = new PatrimonyMaintenanceDocument(file.FileName, file.ContentType, filePath, maintenance.Id);
        maintenance.AddDocument(document);

        _repo.UpdateMaintenance(churchId, maintenance);
        await _uow.Commit();

        result.Result = new PatrimonyMaintenanceDocumentViewModel
        {
            Guid = document.Guid,
            FileName = document.FileName,
            ContentType = document.ContentType,
            FileUrl = document.FilePath,
            UploadedAt = document.UploadedAt
        };

        return result;
    }

    public async Task<IEnumerable<PatrimonyMaintenanceDocumentViewModel>> GetDocumentsByMaintenance(Guid maintenanceGuid)
    {
        var churchId = _context.GetChurchId();
        var maintenance = await _repo.GetByGuidWithDocumentsAsync(churchId, maintenanceGuid);
        if (maintenance == null) return Enumerable.Empty<PatrimonyMaintenanceDocumentViewModel>();

        return maintenance.Documents.Select(d => new PatrimonyMaintenanceDocumentViewModel
        {
            Guid = d.Guid,
            FileName = d.FileName,
            ContentType = d.ContentType,
            FileUrl = d.FilePath,
            UploadedAt = d.UploadedAt
        });
    }
}
