using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;
using LandingPizzeria.Services.IService;

namespace LandingPizzeria.Services.Service;

public class ServiceCliente : IClienteService
{
    private readonly IClienteRepo _clienteRepo;

    public ServiceCliente(IClienteRepo clienteRepo)
    {
        _clienteRepo = clienteRepo;
    }

    public async Task<int> RegistrarClienteAsync(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(cliente.Telefono))
            throw new ArgumentException("El teléfono es obligatorio.");

        if (string.IsNullOrWhiteSpace(cliente.Direccion))
            throw new ArgumentException("La dirección es obligatoria.");

        return await _clienteRepo.GuardarAsync(cliente);
    }
}