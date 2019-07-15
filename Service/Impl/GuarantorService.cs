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
    public class GuarantorService : IGuarantor
    {
        private ApplicationDbContext _context;

        public GuarantorService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<GuarantorDTO>> GetAllAsync()
        {
            if (!await _context.Guarantor.AnyAsync()) return null;

            List<GuarantorModel> result = await _context.Guarantor.ToListAsync();
            List<GuarantorDTO> guarantors = null;
            GuarantorDTO output = null;

            foreach (GuarantorModel g in result)
            {
                output = new GuarantorDTO {
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
                    DateCreated = g.DateCreated
                };
                guarantors.Add(output);
            }

            return guarantors;
        }

        public async Task<GuarantorDTO> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            GuarantorModel user = await _context.Guarantor.FirstOrDefaultAsync(c => c.Id == id);

            GuarantorDTO result = new GuarantorDTO {
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
                 Surname = user.Surname
            };

            return result ;
        }

        public async Task<bool> UpdateAsync(GuarantorDTO user)
        {
            try
            {
                GuarantorModel guarantor = new GuarantorModel()
                {
                    Address = user.Address,
                    Email = user.Email,
                    Gender = user.Gender,
                    PhoneNumber1 = user.PhoneNumber1,
                    PhoneNumber2 = user.PhoneNumber2,
                    State = user.State,
                    Surname = user.Surname,
                    LastName = user.LastName,
                };

                _context.Guarantor.Attach(guarantor);
                _context.Entry(guarantor).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
