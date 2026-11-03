using Microsoft.AspNetCore.Identity;
using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;
using LandingPizzeria.Services.IService;

namespace LandingPizzeria.Services.Service;

public class ServiceUsuario : IUsuarioService
{
    private readonly IUsuarioRepo _usuarioRepo;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public ServiceUsuario(
        IUsuarioRepo usuarioRepo,
        IPasswordHasher<Usuario> passwordHasher)
    {
        _usuarioRepo = usuarioRepo;
        _passwordHasher = passwordHasher;
    }

    public async Task<int> RegistrarUsuarioAsync(Usuario user)
    {
        if (string.IsNullOrWhiteSpace(user.NombreUsuario))
            throw new ArgumentException("El nombre de usuario es obligatorio.");

        if (string.IsNullOrWhiteSpace(user.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(user.Apellido))
            throw new ArgumentException("El apellido es obligatorio.");

        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException("El email es obligatorio.");

        if (string.IsNullOrWhiteSpace(user.PasswordHash))
            throw new ArgumentException("La contraseña es obligatoria.");

        if (string.IsNullOrWhiteSpace(user.Telefono))
            throw new ArgumentException("El teléfono es obligatorio.");

        var usuarioExistente = await _usuarioRepo.ObtenerPorUsuarioAsync(user.NombreUsuario);

        if (usuarioExistente != null)
            throw new ArgumentException("El nombre de usuario ya está registrado.");

        var emailExistente = await _usuarioRepo.ObtenerPorEmailAsync(user.Email);

        if (emailExistente != null)
            throw new ArgumentException("El email ya está registrado.");

        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

        return await _usuarioRepo.CrearUsuarioAsync(user);
    }

    public async Task<Usuario?> ObtenerUsuarioAsync(int id)
    {
        return await _usuarioRepo.ObtenerPorIdAsync(id);
    }

    public async Task<Usuario?> AutenticarAsync(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            return null;

        Usuario? usuario = login.Contains('@')
            ? await _usuarioRepo.ObtenerPorEmailAsync(login)
            : await _usuarioRepo.ObtenerPorUsuarioAsync(login);

        if (usuario == null)
            return null;

        var resultado = _passwordHasher.VerifyHashedPassword(
            usuario,
            usuario.PasswordHash,
            password);

        if (resultado == PasswordVerificationResult.Failed)
            return null;

        return usuario;
    }
}