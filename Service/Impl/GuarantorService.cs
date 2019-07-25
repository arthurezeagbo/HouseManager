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
    public class GuarantorService : IGuarantor
    {
        private ApplicationDbContext _context;

        public GuarantorService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<GuarantorDTO>> GetAllAsync()
        {
            List<GuarantorModel> result = null;

            using (var context = _context)
            {
                result = await context.Guarantor.AsNoTracking().ToListAsync();
            }
            
            if (result == null) return null;

            var guarantors = result.Select(g => new GuarantorDTO
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
                DateCreated = g.DateCreated
            });          

            return guarantors;
        }

        public async Task<GuarantorDTO> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return null;

            GuarantorModel user;

            using (var context = _context)
            {
                user = await context.Guarantor.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            }
          
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
              
                await _context.SaveChangesAsync();
                _context.Dispose();

                return Task.CompletedTask.IsCompleted;
            }
            catch (Exception ex) { }

            return Task.CompletedTask.IsCanceled;
        }
    }
}
