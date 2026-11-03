using LandingPizzeria.Core.Entidades;

namespace LandingPizzeria.Core.Interfaces;

public interface IUsuarioRepo
{
    Task<int> CrearUsuarioAsync(Usuario user);
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<Usuario?> ObtenerPorEmailAsync(string email);
    Task<Usuario?> ObtenerPorUsuarioAsync(string usuario);
}