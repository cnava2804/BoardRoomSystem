using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BoardRoomSystem.Repositories
{
    public class RoleRepositorio : IRoleRepositorio
    {
        private readonly ApplicationDbContext _context;

        public RoleRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
