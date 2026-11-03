using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Estado;

namespace LandingPizzeria.Core.Interfaces;

public interface IPedidoRepo
{
    Task<int> CrearPedidoAsync(Pedido pedido);
    Task<Pedido?> ObtenerPorIdAsync(int id);
    Task ActualizarEstadoAsync(int idPedido, EstadoPedido nuevoEstado);
}