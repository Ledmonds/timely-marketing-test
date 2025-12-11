using Timely.MarketingTest.Models;

namespace Timely.MarketingTest.Services;

public interface IPokemonService
{
    IAsyncEnumerable<PokemonDto> RetrievePokemon(int n);
}
