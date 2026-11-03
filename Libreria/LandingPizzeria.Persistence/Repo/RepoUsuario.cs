using Dapper;
using Microsoft.Extensions.Configuration;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;

namespace LandingPizzeria.Persistence.Repo;

public class RepoUsuario : RepoBase, IUsuarioRepo
{
    public RepoUsuario(IConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<int> CrearUsuarioAsync(Usuario usuario)
    {
        using var db = Connection;

        const string sql = @"
            INSERT INTO Usuario
                (usuario, nombre, apellido, email, passwordHash, telefono, direccion)
            VALUES
                (@NombreUsuario, @Nombre, @Apellido, @Email, @PasswordHash, @Telefono, @Direccion);

            SELECT LAST_INSERT_ID();";

        return await db.ExecuteScalarAsync<int>(sql, usuario);
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        using var db = Connection;

        const string sql = @"
            SELECT
                idUsuario,
                usuario AS NombreUsuario,
                nombre,
                apellido,
                email,
                passwordHash,
                telefono,
                direccion
            FROM Usuario
            WHERE idUsuario = @Id;";

        return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
    }

    public async Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        using var db = Connection;

        const string sql = @"
            SELECT
                idUsuario,
                usuario AS NombreUsuario,
                nombre,
                apellido,
                email,
                passwordHash,
                telefono,
                direccion
            FROM Usuario
            WHERE email = @Email;";

        return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });
    }

    public async Task<Usuario?> ObtenerPorUsuarioAsync(string usuario)
    {
        using var db = Connection;

        const string sql = @"
            SELECT
                idUsuario,
                usuario AS NombreUsuario,
                nombre,
                apellido,
                email,
                passwordHash,
                telefono,
                direccion
            FROM Usuario
            WHERE usuario = @Usuario;";

        return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Usuario = usuario });
    }
}