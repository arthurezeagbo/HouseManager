using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Service.DTO_s;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class EmployerService : IEmployer
    {
        private ApplicationDbContext _context;

        public EmployerService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<EmployerDTO>> GetAllAsync()
        {
            List<EmployerModel> result = null ;

            using (var context = _context)
            {
                result = await context.Employer.AsNoTracking().ToListAsync();
            }  
            
            var employers = result.Select(g => new EmployerDTO
            {
                Address = g.Address,
                Email = g.Email,
                FirstName = g.FirstName,
                Gender = g.Gender,

                LastName = g.LastName,
                PhoneNumber1 = g.PhoneNumber1,
                PhoneNumber2 = g.PhoneNumber2,
                State = g.State,

                Surname = g.Surname,
                Id = g.Id,
                DateCreated = g.DateCreated,
            });             

            return employers;
        }
        
        public async Task<EmployerDTO> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            EmployerModel user = null;

            using (var context = _context)
            {
                user = await _context.Employer.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            }
           

            EmployerDTO result = new EmployerDTO
            {
                DateCreated = user.DateCreated,
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,

                FirstName = user.FirstName,
                Gender = user.Gender,
                LastName = user.LastName,
                PhoneNumber1 = user.PhoneNumber1,

                PhoneNumber2 = user.PhoneNumber2,
                State = user.State,
                Surname = user.Surname,
            };

            return result;
        }

        public async Task<bool> UpdateAsync(EmployerDTO user)
        {
            if (user == null) return false;

            try
            {
                EmployerModel employer = new EmployerModel()
                {
                    Address = user.Address,
                    Email = user.Email,
                    Gender = user.Gender,
                    PhoneNumber1 = user.PhoneNumber1,

                    PhoneNumber2 = user.PhoneNumber2,
                    State = user.State,
                    Surname = user.Surname,
                    LastName = user.LastName
                };

                using (var context = _context)
                {
                    _context.Attach(employer);

                    await _context.SaveChangesAsync();
                    return Task.CompletedTask.IsCompleted;
                }                
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
