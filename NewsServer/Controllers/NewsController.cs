using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsServer.Classes;

namespace NewsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsFeedService _newsService;

        public NewsController(INewsFeedService newsService)
        {
            _newsService = newsService;
        }

        // GET api/news
        [HttpGet]
        public ActionResult<IEnumerable<GeoFeedItem>> Get()
        {
            return _newsService.GetNewsFeed();
        }
    }
}
