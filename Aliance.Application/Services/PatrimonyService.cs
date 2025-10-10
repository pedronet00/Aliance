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

public class PatrimonyService : IPatrimonyService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _userContext;
    private readonly IPatrimonyRepository _repo;

    public PatrimonyService(IMapper mapper, IUnitOfWork uow, IUserContextService userContext, IPatrimonyRepository repo)
    {
        _mapper = mapper;
        _uow = uow;
        _userContext = userContext;
        _repo = repo;
    }

    public async Task<DomainNotificationsResult<PatrimonyViewModel>> DeletePatrimony(Guid guid)
    {
        var result = new DomainNotificationsResult<PatrimonyViewModel>();
        var churchId = _userContext.GetChurchId();

        var patrimony = await _repo.GetPatrimonyByGuid(churchId, guid);

        if(patrimony is null)
            result.Notifications.Add("Patrimônio não encontrado.");

        if (result.HasNotifications)
            return result;

        await _repo.DeletePatrimony(churchId, patrimony.Id);

        await _uow.Commit();

        result.Result = _mapper.Map<PatrimonyViewModel>(patrimony);

        return result;
    }

    public async Task<IEnumerable<PatrimonyViewModel>> GetAllPatrimonies()
    {
        var churchId = _userContext.GetChurchId();

        var patrimonies = await _repo.GetAllPatrimonies(churchId);

        return _mapper.Map<IEnumerable<PatrimonyViewModel>>(patrimonies);
    }

    public async Task<DomainNotificationsResult<PatrimonyViewModel>> GetPatrimonyByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<PatrimonyViewModel>();

        var churchId = _userContext.GetChurchId();

        var patrimony = await _repo.GetPatrimonyByGuid(churchId, guid);

        if(patrimony is null)
            result.Notifications.Add("Patrimônio não encontrado.");

        if (result.HasNotifications)
            return result;

        result.Result = _mapper.Map<PatrimonyViewModel>(patrimony);

        return result;
    }

    public async Task<DomainNotificationsResult<PatrimonyViewModel>> InsertPatrimony(PatrimonyDTO patrimony)
    {
        var result = new DomainNotificationsResult<PatrimonyViewModel>();

        var churchId = _userContext.GetChurchId();

        var patrimonyEntity = _mapper.Map<Domain.Entities.Patrimony>(patrimony);

        await _repo.InsertPatrimony(patrimonyEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<PatrimonyViewModel>(patrimonyEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<PatrimonyViewModel>> UpdatePatrimony(PatrimonyDTO patrimony)
    {
        var result = new DomainNotificationsResult<PatrimonyViewModel>();

        var churchId = _userContext.GetChurchId();

        var patrimonyEntity = _mapper.Map<Domain.Entities.Patrimony>(patrimony);

        var existingPatrimony = await _repo.GetPatrimonyByGuid(churchId, patrimony.Guid);

        if(existingPatrimony is null)
            result.Notifications.Add("Patrimônio não encontrado.");

        if (result.HasNotifications)
            return result;

        await _repo.UpdatePatrimony(churchId, patrimonyEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<PatrimonyViewModel>(patrimonyEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<PatrimonyDocumentViewModel>> UploadDocumentAsync(Guid patrimonyGuid, IFormFile file)
    {
        var result = new DomainNotificationsResult<PatrimonyDocumentViewModel>();
        var churchId = _userContext.GetChurchId();

        var patrimony = await _repo.GetPatrimonyByGuid(churchId, patrimonyGuid);
        if (patrimony == null)
        {
            result.Notifications.Add("Patrimônio não encontrado.");
            return result;
        }

        if (file == null || file.Length == 0)
        {
            result.Notifications.Add("Arquivo inválido.");
            return result;
        }

        // Salvar arquivo (exemplo simples em FileSystem local, depois pode trocar por Blob/S3)
        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "patrimonies");
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Criar documento
        var document = new PatrimonyDocument(file.FileName, file.ContentType, filePath, patrimony.Id);
        patrimony.AddDocument(document);

        _repo.UpdatePatrimony(churchId,patrimony);
        await _uow.Commit();

        result.Result = new PatrimonyDocumentViewModel
        {
            Guid = document.Guid,
            FileName = document.FileName,
            ContentType = document.ContentType,
            FileUrl = document.FilePath, 
            UploadedAt = document.UploadedAt
        };

        return result;
    }

    public async Task<IEnumerable<PatrimonyDocumentViewModel>> GetDocumentsByPatrimony(Guid patrimonyGuid)
    {
        var churchId = _userContext.GetChurchId();
        var patrimony = await _repo.GetByGuidWithDocumentsAsync(churchId,patrimonyGuid);
        if (patrimony == null) return Enumerable.Empty<PatrimonyDocumentViewModel>();

        return patrimony.Documents.Select(d => new PatrimonyDocumentViewModel
        {
            Guid = d.Guid,
            FileName = d.FileName,
            ContentType = d.ContentType,
            FileUrl = d.FilePath,
            UploadedAt = d.UploadedAt
        });
    }

    public async Task<DomainNotificationsResult<int>> PatrimoniesCount()
    {
        var result = new DomainNotificationsResult<int>();
        var churchId = _userContext.GetChurchId();

        result.Result = await _repo.PatrimoniesCount(churchId);

        return result;
    }
}
