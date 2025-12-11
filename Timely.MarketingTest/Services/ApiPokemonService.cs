using Timely.MarketingTest.Models;

namespace Timely.MarketingTest.Services;

public class ApiPokemonService : IPokemonService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private Uri baseUri = new("https://pokeapi.co/api/v2/pokemon/");

    public ApiPokemonService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async IAsyncEnumerable<PokemonDto> RetrievePokemon(int n)
    {
        var client = _httpClientFactory.CreateClient();
        var nextUrl = baseUri;

        for (int i = 0; i < n; )
        {
            var pokemonChunkStream = await client.GetFromJsonAsync<PokemonChunk>(nextUrl);

            // yup, this will throw if the model does not parse - maybe we want that, maybe not?!?
            await foreach (var pl in pokemonChunkStream!.Results.OfType<PokemonUriLookup>())
            {
                var result = await client.GetFromJsonAsync<PokemonResult>(pl.Url);

                // skip if the result has returned null - empty object in JSON - logging is likely prudent
                if (result is null)
                {
                    continue;
                }

                yield return new PokemonDto()
                {
                    Height = result.Height,
                    Name = result.Name,
                    Weight = result.Weight,
                    Species = result.Species.Name,
                };

                i++;

                // stop if you run out, or if you have reached the max wanted
                if (i >= n)
                {
                    yield break;
                }
            }

            nextUrl = new Uri(pokemonChunkStream.Next);
        }
    }

    private record PokemonChunk(string Next, IAsyncEnumerable<PokemonUriLookup> Results);

    private record PokemonUriLookup(string Name, string Url);

    private record PokemonResult(
        string Name,
        decimal Weight,
        decimal Height,
        PokemonSpecies Species
    );

    private record PokemonSpecies(string Name);
}
