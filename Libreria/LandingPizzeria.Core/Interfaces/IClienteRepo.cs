using LandingPizzeria.Core.Entidades;

namespace LandingPizzeria.Core.Interfaces;

public interface IClienteRepo
{
    Task<int> GuardarAsync(Cliente cliente);
    Task<Cliente?> ObtenerPorIdAsync(int idCliente);
}