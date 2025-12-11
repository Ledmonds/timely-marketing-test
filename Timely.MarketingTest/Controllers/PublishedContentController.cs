using Microsoft.AspNetCore.Mvc;
using Timely.MarketingTest.Models;
using Timely.MarketingTest.Services;

namespace Timely.MarketingTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishedContentController : ControllerBase
{
    private readonly IPublishedContentService _publishedContentService;

    public PublishedContentController(IPublishedContentService publishedContentService)
    {
        _publishedContentService = publishedContentService;
    }

    [HttpPost]
    public async Task<JsonResult> GetPublihsedContentCount()
    {
        var publishedContentCount = await _publishedContentService.GetPublishedContentCount();
        var dto = new PublishedContentDto(publishedContentCount);

        return new JsonResult(dto);
    }
}