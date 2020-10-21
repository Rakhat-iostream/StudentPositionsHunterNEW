using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories;
using ISPH.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ISPH.API
{
    public static class ServicesDependencies
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Student>, StudentRepository>();
            services.AddScoped<IEntityRepository<News>, NewsRepository>();
            services.AddScoped<IEntityRepository<Advertisement>, AdvertisementsRepository>();
            services.AddScoped<ICompanyRepository, CompaniesRepository>();
            services.AddScoped<IEntityRepository<Employer>, EmployersRepository>();
            services.AddScoped<IEntityRepository<Article>, ArticlesRepository>();
            services.AddScoped<IEntityRepository<Company>, CompaniesRepository>();
            services.AddScoped<IEntityRepository<Position>, PositionsRepository>();
            services.AddScoped<IPositionsRepository, PositionsRepository>();
            services.AddScoped<IEntityRepository<Resume>, ResumesRepository>();
            services.AddScoped<IUserAuthRepository<Student>, StudentRepository>();
            services.AddScoped<IUserAuthRepository<Employer>, EmployersRepository>();
        }
    }
}
