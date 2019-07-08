using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Data.Model
{
    public class UserProfileModel : IdentityUser
    {
        public UserProfileModel() : base()
        {
            DateCreated = DateUpdated = DateTime.Now;
            
        }

        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsActive { get; set; }

    }
}
