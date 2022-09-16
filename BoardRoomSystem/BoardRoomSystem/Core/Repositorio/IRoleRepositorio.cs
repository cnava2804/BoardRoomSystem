using Microsoft.AspNetCore.Identity;

namespace BoardRoomSystem.Core.Repositories
{
    public interface IRoleRepositorio
    {
        ICollection<IdentityRole> GetRoles();
    }
}
