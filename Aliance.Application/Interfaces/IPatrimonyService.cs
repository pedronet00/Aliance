using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IPatrimonyService
{
    Task<IEnumerable<PatrimonyViewModel>> GetAllPatrimonies();

    Task<DomainNotificationsResult<PatrimonyViewModel>> GetPatrimonyByGuid(Guid guid);

    Task<DomainNotificationsResult<PatrimonyViewModel>> InsertPatrimony(PatrimonyDTO patrimony);

    Task<DomainNotificationsResult<PatrimonyViewModel>> UpdatePatrimony(PatrimonyDTO patrimony);

    Task<DomainNotificationsResult<PatrimonyViewModel>> DeletePatrimony(Guid guid);

    Task<DomainNotificationsResult<PatrimonyDocumentViewModel>> UploadDocumentAsync(Guid patrimonyGuid, IFormFile file);
    Task<IEnumerable<PatrimonyDocumentViewModel>> GetDocumentsByPatrimony(Guid patrimonyGuid);

    Task<DomainNotificationsResult<int>> PatrimoniesCount();
}
