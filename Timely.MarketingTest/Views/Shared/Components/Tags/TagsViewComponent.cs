using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Timely.MarketingTest.Views.Shared.Components.PublishedForm;

public class TagsViewComponent : ViewComponent
{
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;

    public TagsViewComponent(IUmbracoContextAccessor umbracoContextAccessor)
    {
        _umbracoContextAccessor = umbracoContextAccessor;
    }

    public IViewComponentResult Invoke(string propertyName)
    {
        var currentPage = GetCurrentPage();

        var tags =
            currentPage?.Value<IEnumerable<string>>(propertyName) ?? Enumerable.Empty<string>();

        return View(tags);
    }

    private IPublishedContent? GetCurrentPage()
    {
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
        {
            throw new Exception("current page could not be located for the given context");
        }

        return umbracoContext.PublishedRequest?.PublishedContent;
    }
}