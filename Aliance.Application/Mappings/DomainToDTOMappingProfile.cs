using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
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

        CreateMap<Baptism, BaptismDTO>().ReverseMap();
        CreateMap<Baptism, BaptismViewModel>().ReverseMap();

        CreateMap<Cell, CellDTO>().ReverseMap();
        CreateMap<Cell, CellViewModel>().ReverseMap();

        CreateMap<MissionCampaign, MissionCampaignDTO>().ReverseMap();
        CreateMap<MissionCampaign, MissionCampaignViewModel>().ReverseMap();
    }
}
