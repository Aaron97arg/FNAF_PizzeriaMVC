using LandingPizzeria.Core.Entidades;

namespace LandingPizzeria.Services.IService;

public interface IPizzaService
{
    Task<IEnumerable<Pizza>> VerDisponiblesAsync();
    Task<Pizza?> ObtenerPizzaAsync(int id);
    Task<int> RegistrarPizzaAsync(Pizza pizza);
}