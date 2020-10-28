using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ISPH.Core.Interfaces.Authentification;

namespace ISPH.API
{
    public static class ServicesDependencies
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudentsRepository, StudentRepository>();
            services.AddScoped<IUserAuthRepository<Student>, StudentRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAdvertisementsRepository, AdvertisementsRepository>();
            services.AddScoped<ICompanyRepository, CompaniesRepository>();
            services.AddScoped<IEmployersRepository, EmployersRepository>();
            services.AddScoped<IUserAuthRepository<Employer>, EmployersRepository>();
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<ICompanyRepository, CompaniesRepository>();
            services.AddScoped<IPositionsRepository, PositionsRepository>();
            services.AddScoped<IResumesRepository, ResumesRepository>();
        }
    }
}
