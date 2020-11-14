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
            CreateMap<Advertisement, AdvertisementDTO>();
            CreateMap<Article, ArticleDTO>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<Employer, EmployerDTO>();
            CreateMap<News, NewsDTO>();
            CreateMap<Position, PositionDTO>();
            CreateMap<Student, StudentDTO>();
        }
    }
}
