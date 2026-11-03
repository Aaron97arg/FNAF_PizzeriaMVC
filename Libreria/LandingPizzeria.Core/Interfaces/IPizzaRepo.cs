using LandingPizzeria.Core.Entidades;

namespace LandingPizzeria.Core.Interfaces;

public interface IPizzaRepo
{
    Task<IEnumerable<Pizza>> ObtenerDisponiblesAsync();
    Task<Pizza?> ObtenerPorIdAsync(int id);
    Task<int> CrearPizzaAsync(Pizza pizza);
}