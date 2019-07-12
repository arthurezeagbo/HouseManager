using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.DTO_s
{
    public class GuarantorDTO
    {
        public int Id { get; set; }
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public string Email { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
