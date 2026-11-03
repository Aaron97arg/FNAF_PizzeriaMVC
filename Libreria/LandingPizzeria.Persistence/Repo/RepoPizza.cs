using Dapper;
using Microsoft.Extensions.Configuration;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;

namespace LandingPizzeria.Persistence.Repo;

public class RepoPizza : RepoBase, IPizzaRepo
{
    public RepoPizza(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<Pizza>> ObtenerDisponiblesAsync()
    {
        using var connection = Connection;
        return await connection.QueryAsync<Pizza>(
            "SELECT * FROM Pizza WHERE disponible = TRUE;");
    }

    public async Task<Pizza?> ObtenerPorIdAsync(int id)
    {
        using var connection = Connection;
        return await connection.QueryFirstOrDefaultAsync<Pizza>(
            "SELECT * FROM Pizza WHERE idPizza = @Id;", new { Id = id });
    }

    public async Task<int> CrearPizzaAsync(Pizza pizza)
    {
        using var connection = Connection;

        const string sql = @"
            INSERT INTO Pizza (nombre, descripcion, precio, disponible)
            VALUES (@Nombre, @Descripcion, @Precio, @Disponible);
            SELECT LAST_INSERT_ID();";

        return await connection.ExecuteScalarAsync<int>(sql, pizza);
    }
}