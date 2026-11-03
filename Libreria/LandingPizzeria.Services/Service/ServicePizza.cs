using LandingPizzeria.Core.Entidades;
using LandingPizzeria.Core.Interfaces;
using LandingPizzeria.Services.IService;

namespace LandingPizzeria.Services.Service;

public class ServicePizza : IPizzaService
{
    private readonly IPizzaRepo _pizzaRepo;

    public ServicePizza(IPizzaRepo pizzaRepo)
    {
        _pizzaRepo = pizzaRepo;
    }

    public async Task<IEnumerable<Pizza>> VerDisponiblesAsync()
    {
        return await _pizzaRepo.ObtenerDisponiblesAsync();
    }

    public async Task<Pizza?> ObtenerPizzaAsync(int id)
    {
        return await _pizzaRepo.ObtenerPorIdAsync(id);
    }

    public async Task<int> RegistrarPizzaAsync(Pizza pizza)
    {
        if (string.IsNullOrWhiteSpace(pizza.Nombre))
            throw new ArgumentException("El nombre de la pizza es obligatorio.");

        if (pizza.Precio <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero.");

        return await _pizzaRepo.CrearPizzaAsync(pizza);
    }
}