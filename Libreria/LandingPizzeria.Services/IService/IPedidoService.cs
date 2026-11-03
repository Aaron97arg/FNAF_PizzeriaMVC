using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Estado;

namespace LandingPizzeria.Services.IService;

public interface IPedidoService
{
    Task<int> CrearPedidoAsync(Pedido pedido);
    Task<Pedido?> ObtenerPedidoAsync(int id);
    Task CambiarEstadoPedidoAsync(int idPedido, EstadoPedido nuevoEstado);
}