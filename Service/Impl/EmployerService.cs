using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Service.DTO_s;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
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
            if (!await _context.Guarantor.AnyAsync()) return null;

            List<EmployerModel> result = await _context.Employer.ToListAsync();
            List<EmployerDTO> employers = null;
            EmployerDTO output = null;

            foreach (EmployerModel g in result)
            {
                output = new EmployerDTO
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
                };
                employers.Add(output);
            }

            return employers;
        }

        public async Task<EmployerDTO> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            EmployerModel user = await _context.Employer.FirstOrDefaultAsync(c => c.Id == id);

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

                _context.Employer.Attach(employer);
                _context.Entry(employer).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
