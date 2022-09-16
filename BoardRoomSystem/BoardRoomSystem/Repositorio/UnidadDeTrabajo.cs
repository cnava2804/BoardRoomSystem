using BoardRoomSystem.Core.Repositories;

namespace BoardRoomSystem.Repositories
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public IUsuarioRepositorio User { get; }
        public IRoleRepositorio Role { get; }

        public UnidadDeTrabajo(IUsuarioRepositorio user, IRoleRepositorio role)
        {
            User = user;
            Role = role;
        }
    }
}
