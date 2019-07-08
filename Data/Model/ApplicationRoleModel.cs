using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class ApplicationRoleModel : IdentityRole
    {
        public ApplicationRoleModel() : base()
        {

        }

        public ApplicationRoleModel(string roleName) : base(roleName)
        {
            this.Name = this.NormalizedName = roleName;
        }

       
    }


}
