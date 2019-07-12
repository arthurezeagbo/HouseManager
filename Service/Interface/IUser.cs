using Service.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUser
    {
        Task<string> CreateUserAsync(CreateUserDTO user);
        Task<UpdateUserDTO> UpdateUserAsync(string userId);
        Task<bool> UpdateUserAsync(UpdateUserDTO user);
        Task<bool> MarkUserAsDeletedAsync(string userId);
        Task<bool> DisableUserAsync(string userId);
        Task<bool> ActivateUserAsync(string userId);
        Task<bool> SendEmailToUserAsync(string address, string subject, object data);
    }
}
