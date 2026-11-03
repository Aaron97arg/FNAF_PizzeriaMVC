using Dapper;
using Microsoft.Extensions.Configuration;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;

namespace LandingPizzeria.Persistence.Repo;

public class RepoCliente : RepoBase, IClienteRepo
{
    public RepoCliente(IConfiguration configuration) : base(configuration) { }

    public async Task<int> GuardarAsync(Cliente cliente)
    {
        using var connection = Connection;

        const string sql = @"
            INSERT INTO Cliente (nombre, telefono, direccion)
            VALUES (@Nombre, @Telefono, @Direccion);
            SELECT LAST_INSERT_ID();";

        return await connection.ExecuteScalarAsync<int>(sql, cliente);
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int idCliente)
    {
        using var connection = Connection;
        return await connection.QueryFirstOrDefaultAsync<Cliente>(
            "SELECT * FROM Cliente WHERE idCliente = @Id;", new { Id = idCliente });
    }
}