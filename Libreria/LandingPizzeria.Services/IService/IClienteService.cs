using LandingPizzeria.Core.Entidades;

namespace LandingPizzeria.Services.IService;

public interface IClienteService
{
    Task<int> RegistrarClienteAsync(Cliente cliente);
}