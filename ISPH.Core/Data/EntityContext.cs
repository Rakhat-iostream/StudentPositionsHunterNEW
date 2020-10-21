using ISPH.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Core.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
