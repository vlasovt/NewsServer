using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsServer.Classes
{
    public class NewsFeedService: INewsFeedService
    {
        private readonly IFeedDownloader _feedDownloader;
        private readonly IFeedClassifier _feedClassifier;
        private readonly IHostingEnvironment environment;
        private readonly IEqualityComparer<double[]> _arrayComparer;

        public NewsFeedService(IFeedDownloader feedDownloader, 
                                IHostingEnvironment environment,
                                IEqualityComparer<double[]> arrayComparer,
                                IFeedClassifier feedClassifier)
        {
            _feedDownloader = feedDownloader;
            this.environment = environment;
            _arrayComparer = arrayComparer;
            _feedClassifier = feedClassifier;
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

            var groups = geoFeeds.GroupBy(g => g.Coordinates, _arrayComparer);

            foreach (var group in groups.Where(g => g.Count() > 1))
            {
                var counter = 0;

                foreach (var item in group)
                {
                    item.Coordinates[1] += counter == 0
                                                ? counter
                                                : (double)counter / 10;
                    counter++;
                }
            }

            // select only the last 24hs news
            var latestNews = geoFeeds.Where(i => i.Date > DateTime.Now.AddDays(-1)).OrderByDescending(d => d.Date).ToList();

            foreach (var item in latestNews)
            {
                _feedClassifier.ClassifyNewsItem(item);
            }

            return latestNews;
        }
    }
}
