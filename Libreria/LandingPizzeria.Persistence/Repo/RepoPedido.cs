using Dapper;
using Microsoft.Extensions.Configuration;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Estado;
using LandingPizzeria.Core.Interfaces;

namespace LandingPizzeria.Persistence.Repo;

public class RepoPedido : RepoBase, IPedidoRepo
{
    public RepoPedido(IConfiguration configuration) : base(configuration) { }

    public async Task<int> CrearPedidoAsync(Pedido pedido)
    {
        using var connection = Connection;
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            const string sqlPedido = @"
                INSERT INTO Pedido
                (idCliente, fechaHora, estado, direccionEntrega, total)
                VALUES
                (@IdCliente, @FechaHora, @Estado, @DireccionEntrega, @Total);
                SELECT LAST_INSERT_ID();";

            var idPedido = await connection.ExecuteScalarAsync<int>(sqlPedido, new
            {
                pedido.IdCliente,
                pedido.FechaHora,
                Estado = (int)pedido.Estado,
                pedido.DireccionEntrega,
                pedido.Total
            }, transaction);

            const string sqlDetalle = @"
                INSERT INTO DetallePedido
                (idPedido, idPizza, cantidad, precioUnitario)
                VALUES
                (@IdPedido, @IdPizza, @Cantidad, @PrecioUnitario);";

            foreach (var d in pedido.Detalles)
            {
                await connection.ExecuteAsync(sqlDetalle, new
                {
                    IdPedido = idPedido,
                    d.IdPizza,
                    d.Cantidad,
                    d.PrecioUnitario
                }, transaction);
            }

            await transaction.CommitAsync();
            return idPedido;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Pedido?> ObtenerPorIdAsync(int id)
    {
        using var connection = Connection;

        var pedido = await connection.QueryFirstOrDefaultAsync<Pedido>(
            "SELECT * FROM Pedido WHERE idPedido = @Id;", new { Id = id });

        if (pedido == null)
            return null;

        var detalles = await connection.QueryAsync<DetallePedido>(
            "SELECT * FROM DetallePedido WHERE idPedido = @Id;", new { Id = id });

        foreach (var d in detalles)
            pedido.AgregarDetalle(d);

        return pedido;
    }

    public async Task ActualizarEstadoAsync(int idPedido, EstadoPedido nuevoEstado)
    {
        using var connection = Connection;

        await connection.ExecuteAsync(
            "UPDATE Pedido SET estado = @Estado WHERE idPedido = @IdPedido;",
            new { IdPedido = idPedido, Estado = (int)nuevoEstado });
    }
}