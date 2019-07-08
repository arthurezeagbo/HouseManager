using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> AddAsync(EmployerModel user)
        {
            try
            {
                _context.Employer.Add(user);

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }

        public async Task<IEnumerable<EmployerModel>> GetAllAsync()
        {
            if (!await _context.Employer.AnyAsync()) return null;

            return await _context.Employer.ToListAsync();
        }

        public async Task<EmployerModel> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            return await _context.Employer.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateAsync(EmployerModel user)
        {
            try
            {
                _context.Employer.Attach(user);
                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
