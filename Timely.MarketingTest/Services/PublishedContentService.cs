using Umbraco.Cms.Core;

namespace Timely.MarketingTest.Services;

public class PublishedContentService : IPublishedContentService
{
    private readonly IPublishedContentQuery _publishedContentQuery;

    public PublishedContentService(IPublishedContentQuery publishedContentQuery)
    {
        _publishedContentQuery = publishedContentQuery;
    }

    public Task<int> GetPublishedContentCount()
    {
        var result = _publishedContentQuery
            .ContentAtRoot()
            .Select(r => r.DescendantOrSelf())
            .Count();

        return Task.FromResult(result);
        throw new NotImplementedException();
    }
}
