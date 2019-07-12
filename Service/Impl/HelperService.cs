using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.DTO_s;
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
                    MarkAsDeleted = false,
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
            var helpers = await _context.Helper.AnyAsync() ? _context.Helper.ToListAsync() : null;

            List<HelperModel> all = helpers.GetAwaiter().GetResult();
            List<HelperDTO> helpersDTO = null;
            HelperDTO helper = null;

            foreach(HelperModel user in all)
            {
                helper = new HelperDTO {
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
                    Guarantor = string.Concat(user.Guarantor.Surname," ",user.Guarantor.FirstName," ",user.Guarantor.LastName)
                };
                helpersDTO.Add(helper);
            }

            return helpersDTO;
        }

        public async Task<HelperDTO> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            HelperModel user = await _context.Helper.FirstOrDefaultAsync(c => c.Id == id);

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

                _context.Helper.Attach(helper);
                _context.Entry(helper).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
