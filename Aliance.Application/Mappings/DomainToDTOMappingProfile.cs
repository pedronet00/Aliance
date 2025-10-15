using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Department, DepartmentDTO>().ReverseMap();
        CreateMap<Department, DepartmentViewModel>().ReverseMap();

        CreateMap<Church, ChurchDTO>().ReverseMap();
        CreateMap<Church, ChurchViewModel>().ReverseMap();

        CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        CreateMap<ApplicationUser, UserViewModel>().ReverseMap();

        CreateMap<Baptism, BaptismDTO>().ReverseMap();
        CreateMap<Baptism, BaptismViewModel>().ReverseMap();

        CreateMap<Patrimony, PatrimonyDTO>().ReverseMap();
        CreateMap<Patrimony, PatrimonyViewModel>().ReverseMap();

        CreateMap<Income, IncomeDTO>().ReverseMap();
        CreateMap<Income, IncomeViewModel>().ReverseMap();

        CreateMap<Expense, ExpenseDTO>().ReverseMap();
        CreateMap<Expense, ExpenseViewModel>().ReverseMap();
        CreateMap<CellMember, CellMemberViewModel>()
            .ForMember(dest => dest.CellGuId, opt => opt.MapFrom(src => src.Cell.Guid))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

        CreateMap<Tithe, TitheDTO>().ReverseMap();
        CreateMap<Tithe, TitheViewModel>().ReverseMap();
        CreateMap<Tithe, TitheViewModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));


        CreateMap<Location, LocationViewModel>().ReverseMap();

        CreateMap<CellMeeting, CellMeetingDTO>().ReverseMap();
        CreateMap<CellMeetingDTO, CellMeeting>()
            .ConstructUsing(src => new CellMeeting())
            .ForMember(dest => dest.LocationGuid, opt => opt.Ignore())
            .ForMember(dest => dest.CellGuid, opt => opt.Ignore());
        CreateMap<CellMeetingDTO, CellMeetingViewModel>().ReverseMap();

        CreateMap<CellMeeting, CellMeetingViewModel>().ReverseMap();
        CreateMap<CellMeeting, CellMeetingViewModel>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.UserName)).ReverseMap();


        CreateMap<PastoralVisit, PastoralVisitDTO>().ReverseMap();
        CreateMap<PastoralVisit, PastoralVisitViewModel>().ReverseMap();
        CreateMap<PastoralVisit, PastoralVisitViewModel>()
            .ForMember(dest => dest.VisitedMemberName, opt => opt.MapFrom(src => src.VisitedMember.UserName))
            .ForMember(dest => dest.PastorName, opt => opt.MapFrom(src => src.Pastor.UserName)).ReverseMap();

        CreateMap<Cell, CellDTO>().ReverseMap();
        CreateMap<Cell, CellViewModel>().ReverseMap();
        CreateMap<Cell, CellViewModel>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ReverseMap();
        CreateMap<Cell, CellViewModel>()
            .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.UserName))
            .ReverseMap();

        CreateMap<MissionCampaign, MissionCampaignDTO>().ReverseMap();
        CreateMap<MissionCampaign, MissionCampaignViewModel>().ReverseMap();

        CreateMap<CostCenter, CostCenterDTO>().ReverseMap();
        CreateMap<CostCenter, CostCenterViewModel>().ReverseMap();
        CreateMap<CostCenter, CostCenterViewModel>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ReverseMap();

        CreateMap<Event, EventDTO>().ReverseMap();
        CreateMap<Event, EventViewModel>().ReverseMap();
        CreateMap<Event, EventViewModel>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ReverseMap();

        CreateMap<AccountPayable, AccountPayableDTO>().ReverseMap();
        CreateMap<AccountPayable, AccountPayableViewModel>().ReverseMap();
        CreateMap<AccountPayable, AccountPayableViewModel>()
            .ForMember(dest => dest.CostCenterName, opt => opt.MapFrom(src => src.CostCenter.Name))
            .ReverseMap();

        CreateMap<AccountReceivable, AccountReceivableDTO>().ReverseMap();
        CreateMap<AccountReceivable, AccountReceivableViewModel>().ReverseMap();
        CreateMap<AccountReceivable, AccountReceivableViewModel>()
            .ForMember(dest => dest.CostCenterName, opt => opt.MapFrom(src => src.CostCenter.Name))
            .ReverseMap();

        CreateMap<PatrimonyMaintenance, PatrimonyMaintenanceDTO>().ReverseMap();

        CreateMap<PatrimonyMaintenance, PatrimonyMaintenanceViewModel>()
            .ForMember(dest => dest.PatrimonyName, opt => opt.MapFrom(src => src.Patrimony.Name))
            .ReverseMap();

        CreateMap<PatrimonyMaintenanceDTO, PatrimonyMaintenance>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ConstructUsing(dto => new PatrimonyMaintenance(dto.MaintenanceDate, dto.Description, dto.PatrimonyId));

        CreateMap<Budget, BudgetDTO>().ReverseMap();
        CreateMap<Budget, BudgetViewModel>().ReverseMap();
        CreateMap<Budget, BudgetViewModel>()
            .ForMember(dest => dest.CostCenterName, opt => opt.MapFrom(src => src.CostCenter.Name))
            .ReverseMap();
    }
}
