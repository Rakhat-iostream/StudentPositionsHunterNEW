using AutoMapper;
using ISPH.Core.DTO;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISPH.Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Advertisement, AdvertisementDto>();
            CreateMap<Article, ArticleDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<Employer, EmployerDto>();
            CreateMap<News, NewsDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Student, StudentDto>();
        }
    }
}
