using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsServer.Classes
{
    public class NewsFeedService: INewsFeedService
    {
        private IFeedDownloader _feedDownloader;
        private readonly IHostingEnvironment environment;

        public NewsFeedService(IFeedDownloader feedDownloader, 
                                IHostingEnvironment environment)
        {
            _feedDownloader = feedDownloader;
            this.environment = environment;
        }

        public List<GeoFeedItem> GetNewsFeed()
        {
            var geoFeeds = new List<GeoFeedItem>();
            var feedLinks = new List<FeedLink>();
            var filePath = Path.Combine(environment.ContentRootPath, "Assets/feeds.json");

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                feedLinks = JsonConvert.DeserializeObject<List<FeedLink>>(json);
            }

            if (feedLinks == null)
            {
                return geoFeeds;
            }

            var feedReader = new FeedDownloader(new List<IFeedProvider> { new GeoRssFeedProvider() });

            foreach (var link in feedLinks)
            {
                var geoFeedItems = feedReader.GetFeedItems(link.Url, link.FeedType).Result;
                geoFeeds.AddRange(geoFeedItems);
            }

            return geoFeeds.OrderByDescending(d => d.Date).ToList();
        }
    }
}
