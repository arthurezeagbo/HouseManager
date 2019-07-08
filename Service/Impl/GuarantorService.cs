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
    public class GuarantorService : IGuarantor
    {
        private ApplicationDbContext _context;

        public GuarantorService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<bool> AddAsync(GuarantorModel user)
        {
            try
            {
                _context.Guarantor.Add(user);

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }

        public async Task<IEnumerable<GuarantorModel>> GetAllAsync()
        {
            if (!await _context.Guarantor.AnyAsync()) return null;

            return await _context.Guarantor.ToListAsync();
        }

        public async Task<GuarantorModel> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            return await _context.Guarantor.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateAsync(GuarantorModel user)
        {
            try
            {
                _context.Guarantor.Attach(user);
                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
