using Microsoft.AspNetCore.Mvc;
using Timely.MarketingTest.Services;

namespace Timely.MarketingTest.Views.Shared.Components.Pokemon;

public class PokemonViewComponent : ViewComponent
{
    private readonly IPokemonService _pokemonService;

    public PokemonViewComponent(IPokemonService pokemonRepository)
    {
        _pokemonService = pokemonRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(int count)
    {
        // ideally I would like to stream the IAsyncEnumerable to the FE, however components do not appear to support this
        var pokemon = await _pokemonService.RetrievePokemon(count).ToListAsync();

        return View(pokemon);
    }
}