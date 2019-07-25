using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
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
    public class HelperService : IHelper
    {
        private ApplicationDbContext _context;
        private UserManager<UserProfileModel> _userManager;

        public HelperService(ApplicationDbContext context, UserManager<UserProfileModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddAsync(HelperDTO user)
        {
            try
            {
                HelperModel helper = new HelperModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Surname = user.Surname,
                    Gender = user.Gender,

                    Qualification = user.Qualification,
                    GuarantorId = user.GuarantorId,
                    Religion = user.Religion,
                    State = user.State,

                    DateOfBirth = user.DateOfBirth

                };

                _context.Helper.Add(helper);

                await _context.SaveChangesAsync();

                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }

        public async Task<IEnumerable<HelperDTO>> GetAllAsync()
        {
            var result = await _context.Helper.AsNoTracking().AnyAsync() ? _context.Helper.AsNoTracking().ToListAsync() : null;

            if (result == null) return null;

            var helpers = result.Result.Select(user => new HelperDTO
            {

                DateCreated = user.DateCreated,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                Gender = user.Gender,

                Id = user.Id,
                LastName = user.LastName,
                Qualification = user.Qualification,
                Religion = user.Religion,

                State = user.State,
                Surname = user.Surname,
                Guarantor = string.Concat(user.Guarantor.Surname, " ", user.Guarantor.FirstName, " ", user.Guarantor.LastName)

            });

            _context.Dispose();

            return helpers;
        }

        public async Task<HelperDTO> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            HelperModel user = await _context.Helper.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            HelperDTO result = new HelperDTO
            {
                DateCreated = user.DateCreated,
                Id = user.Id,
                Religion = user.Religion,
                Qualification = user.Qualification,

                Guarantor = string.Concat(user.Guarantor.Surname, " ", user.Guarantor.FirstName, " ", user.Guarantor.LastName),
                FirstName = user.FirstName,
                Gender = user.Gender,
                LastName = user.LastName,

                DateOfBirth = user.DateOfBirth,
                State = user.State,
                Surname = user.Surname  
                
            };

            _context.Dispose();

            return result;
        }

        public async Task<bool> UpdateAsync(HelperDTO user)
        {
            try
            {
                HelperModel helper = new HelperModel()
                {
                    Religion = user.Religion,
                    Qualification = user.Qualification,
                    FirstName = user.FirstName,
                    Gender = user.Gender,

                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    State = user.State,
                    Surname = user.Surname
                };

                using(var context = _context)
                {
                    context.Helper.Attach(helper);

                    await _context.SaveChangesAsync();
                    return true;
                }               
            }
            catch (Exception ex) { }

            return false;
        }
    }
}
