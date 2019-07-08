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
    public class HelperService : IHelper
    {
        private ApplicationDbContext _context;

        public HelperService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(HelperModel user)
        {
            try
            {
                _context.Helper.Add(user);

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }

        public async Task<IEnumerable<HelperModel>> GetAllAsync()
        {
            if (!await _context.Helper.AnyAsync()) return null;

            return await _context.Helper.ToListAsync();
        }

        public async Task<HelperModel> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            return await _context.Helper.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateAsync(HelperModel user)
        {
            try
            {
                _context.Helper.Attach(user);
                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
