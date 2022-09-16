using BoardRoomSystem.Core.Repositories;

namespace BoardRoomSystem.Core.Repositories
{
    public interface IUnidadDeTrabajo
    {
        IUsuarioRepositorio User { get; }

        IRoleRepositorio Role { get; }
    }
}
