using Timely.MarketingTest.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder().AddBackOffice().AddWebsite().AddComposers().Build();

builder.Services.AddTransient<IPokemonService, ApiPokemonService>();
builder.Services.AddTransient<IPublishedContentService, PublishedContentService>();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
        u.EndpointRouteBuilder.MapControllers();
    });

await app.RunAsync();