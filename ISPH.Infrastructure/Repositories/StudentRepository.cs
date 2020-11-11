using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPH.Infrastructure.Data;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using ISPH.Core.Interfaces.Authentification;

namespace ISPH.Infrastructure.Repositories
{
    public class StudentRepository : EntityRepository<Student>, IUserAuthRepository<Student>, IStudentsRepository
    {
        private readonly DataHashService<Student> hashService = new StudentsHashService();
        public StudentRepository(EntityContext context) : base(context)
        {
        }
        public async Task<Student> GetByEmail(string email)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(student => student.Email == email);
        }
        public override async Task<bool> HasEntity(Student entity)
        {
            return await _context.Students.AnyAsync(Student => Student.Email == entity.Email);
        }
        public override async Task<IList<Student>> GetAll()
        {
           return await _context.Students.AsQueryable().Include(student => student.Resume).
                OrderBy(st => st.StudentId).ToListAsync();
        }

        public override async Task<Student> GetById(int id)
        {
            return await _context.Students.AsNoTracking().Include(st => st.Resume).FirstOrDefaultAsync(st => st.StudentId == id);
        }

        public async Task<bool> UpdatePassword(Student student, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] saltPass);
            student.HashedPassword = hashedPass;
            student.SaltPassword = saltPass;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync() > 0;
        }
        //Auth

        public async Task<Student> Register(Student user, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = SaltPass;
            _context.Students.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Student> Login(string email, string password)
        {
            var user = await _context.Students.FirstOrDefaultAsync(st => st.Email == email);
            if (user != null) {
                if (hashService.CheckHashedPassword(user, password)) return user;
            }
            return null;
        }

        public async Task<bool> UserExists(Student user)
        {
            return await _context.Students.AnyAsync(st => st.Email == user.Email);
        }

    }
}
