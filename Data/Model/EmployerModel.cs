﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class EmployerModel : BaseModel
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public string Email { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

        public string UserId { get; set; }
        public UserProfileModel User { get; set; }

        public virtual IEnumerable<HelperModel> Helpers { get; set; }
        
    }
}
