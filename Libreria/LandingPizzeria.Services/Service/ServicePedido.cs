using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Estado;
using LandingPizzeria.Core.Interfaces;
using LandingPizzeria.Services.IService;

namespace LandingPizzeria.Services.Service;

public class ServicePedido : IPedidoService
{
    private readonly IPedidoRepo _pedidoRepo;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ServicePedido> _logger;

    public ServicePedido(
        IPedidoRepo pedidoRepo,
        IServiceScopeFactory scopeFactory,
        ILogger<ServicePedido> logger)
    {
        _pedidoRepo = pedidoRepo;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task<int> CrearPedidoAsync(Pedido pedido)
    {
        if (pedido.IdCliente <= 0)
            throw new ArgumentException("El cliente del pedido es obligatorio.");

        if (pedido.Detalles == null || !pedido.Detalles.Any())
            throw new ArgumentException("El pedido debe contener al menos una pizza.");

        foreach (var detalle in pedido.Detalles)
        {
            if (detalle.Cantidad <= 0)
                throw new ArgumentException("La cantidad de pizzas debe ser mayor a cero.");
        }

        if (string.IsNullOrWhiteSpace(pedido.DireccionEntrega))
            throw new ArgumentException("La dirección de entrega es obligatoria.");

        pedido.CalcularTotal();

        if (pedido.Total <= 0)
            throw new ArgumentException("El total del pedido debe ser mayor a cero.");

        var idGenerado = await _pedidoRepo.CrearPedidoAsync(pedido);

        _logger.LogInformation("[NUEVO] Pedido {Id} registrado con éxito.", idGenerado);

        _ = Task.Run(async () => await CicloEstadosPedidoAsync(idGenerado));

        return idGenerado;
    }

    public async Task<Pedido?> ObtenerPedidoAsync(int id) =>
        await _pedidoRepo.ObtenerPorIdAsync(id);

    public async Task CambiarEstadoPedidoAsync(int idPedido, EstadoPedido nuevoEstado)
    {
        var pedido = await _pedidoRepo.ObtenerPorIdAsync(idPedido);

        if (pedido == null)
            throw new KeyNotFoundException($"El pedido {idPedido} no existe.");

        await _pedidoRepo.ActualizarEstadoAsync(idPedido, nuevoEstado);
    }

    private async Task CicloEstadosPedidoAsync(int idPedido)
    {
        using var scope = _scopeFactory.CreateScope();

        var repo = scope.ServiceProvider.GetRequiredService<IPedidoRepo>();

        try
        {
            // El pedido nace en EnPreparacion (estado inicial de la entidad)
            await Task.Delay(10000);

            await repo.ActualizarEstadoAsync(idPedido, EstadoPedido.Listo);
            _logger.LogInformation("[SIMULACIÓN] Pedido {Id} LISTO.", idPedido);

            await Task.Delay(5000);

            await repo.ActualizarEstadoAsync(idPedido, EstadoPedido.EnViaje);
            _logger.LogInformation("[SIMULACIÓN] Pedido {Id} EN VIAJE.", idPedido);

            await Task.Delay(12000);

            await repo.ActualizarEstadoAsync(idPedido, EstadoPedido.Entregado);
            _logger.LogInformation("[SIMULACIÓN] Pedido {Id} ENTREGADO.", idPedido);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[ERROR] Falló la simulación del pedido {Id}", idPedido);
        }
    }
}