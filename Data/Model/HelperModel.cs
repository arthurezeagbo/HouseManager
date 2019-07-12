using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Model
{
    public class HelperModel : BaseModel
    {
        public HelperModel() : base()
        {

        }
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Qualification { get; set; }

        public string Religion { get; set; }

        public string State { get; set; }

        public string GuarantorId { get; set; }
        public virtual GuarantorModel Guarantor { get; set; }
        public int? EmployerId { get; set; }
        public virtual EmployerModel Employer { get; set; }


    }
}
