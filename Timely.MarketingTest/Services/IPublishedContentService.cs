namespace Timely.MarketingTest.Services;

public interface IPublishedContentService
{
    public Task<int> GetPublishedContentCount();
}
