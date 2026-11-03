using LandingPizzeria.Core.Estado;

namespace LandingPizzeria.Core.Entidades;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdCliente { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public EstadoPedido Estado { get; set; } = EstadoPedido.EnPreparacion;
    public string DireccionEntrega { get; set; } = string.Empty;
    public decimal Total { get; set; }

    public List<DetallePedido> Detalles { get; set; } = new();

    public void AgregarDetalle(DetallePedido detalle)
    {
        ArgumentNullException.ThrowIfNull(detalle);
        Detalles.Add(detalle);
        CalcularTotal();
    }

    public void CalcularTotal() =>
        Total = Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
}