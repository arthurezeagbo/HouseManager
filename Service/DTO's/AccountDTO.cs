using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO_s
{
    public class AccountDTO
    {

    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }

    public class ForgotPasswordDTO
    {
        public string Email { get; set; }

    }

    public class ChangePasswordDTO

    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
