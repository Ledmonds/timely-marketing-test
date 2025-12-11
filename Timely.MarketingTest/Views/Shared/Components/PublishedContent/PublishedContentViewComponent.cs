using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;

namespace Timely.MarketingTest.Views.Shared.Components.PublishedForm;

public class PublishedContentViewComponent : ViewComponent
{
    private readonly IPublishedContentQuery _publishedContentQuery;

    public PublishedContentViewComponent(IPublishedContentQuery publishedContentQuery)
    {
        _publishedContentQuery = publishedContentQuery;
    }

    public IViewComponentResult Invoke(int count)
    {
        return View();
    }
}
