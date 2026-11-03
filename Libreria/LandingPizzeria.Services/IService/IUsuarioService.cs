using LandingPizzeria.Core.Entidades;

namespace LandingPizzeria.Services.IService;

public interface IUsuarioService
{
    Task<int> RegistrarUsuarioAsync(Usuario user);

    Task<Usuario?> ObtenerUsuarioAsync(int id);

    Task<Usuario?> AutenticarAsync(string login, string password);
}