using BoardRoomSystem.Areas.Identity.Data;

namespace BoardRoomSystem.Core.Repositories
{
    public interface IUsuarioRepositorio
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);
    }
}
