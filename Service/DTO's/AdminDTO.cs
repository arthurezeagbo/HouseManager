using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO_s
{
    public class AdminDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
       
    }
}
