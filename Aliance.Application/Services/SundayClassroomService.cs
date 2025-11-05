using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class SundaySchoolClassroomService : ISundaySchoolClassroomService
{
    private readonly ISundaySchoolClassroomRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContext;

    public SundaySchoolClassroomService(ISundaySchoolClassroomRepository repo, IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContext)
    {
        _repo = repo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<PagedResult<SundaySchoolClassroomViewModel>>> GetClassrooms(int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<SundaySchoolClassroomViewModel>>();
        var churchId = _userContext.GetChurchId();

        var classrooms = await _repo.GetSundaySchoolClassrooms(churchId, pageNumber, pageSize);

        var classroomsVMs = _mapper.Map<IEnumerable<SundaySchoolClassroomViewModel>>(classrooms.Items);

        result.Result = new PagedResult<SundaySchoolClassroomViewModel>(
                classroomsVMs,
                classrooms.TotalCount,
                classrooms.CurrentPage,
                classrooms.PageSize
            );

        return result;
    }

    public async Task<DomainNotificationsResult<SundaySchoolClassroomViewModel>> Insert(SundaySchoolClassroomDTO classroomDTO)
    {
        var result = new DomainNotificationsResult<SundaySchoolClassroomViewModel>();
        var churchId = _userContext.GetChurchId();

        var classroom = _mapper.Map<SundaySchoolClassroom>(classroomDTO);

        await _repo.InsertSundaySchoolClassroom(classroom);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<SundaySchoolClassroomViewModel>(classroom);

        return result;
    }
}
