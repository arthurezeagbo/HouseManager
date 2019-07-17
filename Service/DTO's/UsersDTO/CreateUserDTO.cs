using Settings.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.DTO_s
{
    public class CreateUserDTO
    {
        public string Id { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

        public string UserType { get; set; }

    }

    public class UpdateUserDTO
    {
        public string UserId { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

    }
}
