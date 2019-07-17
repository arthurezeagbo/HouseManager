using Service.DTO_s;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISuperAdmin
    {
        Task<IEnumerable<CreateUserDTO>> GetAllAdmin();

        Task<bool> EnableAnyUserAsync(string id);

        Task<bool> DisableAnyUserAsync(string id);

    }
}
