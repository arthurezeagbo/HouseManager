using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.DTO_s
{
    public class HelperDTO
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Qualification { get; set; }

        public string Religion { get; set; }

        public string State { get; set; }
        public int Id { get; set; }
        public string GuarantorId { get; set; }
        public string Guarantor { get; set; }
        public DateTime DateCreated { get; set; }

    }
}