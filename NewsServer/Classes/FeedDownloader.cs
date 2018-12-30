using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServer.Classes
{
    public class FeedDownloader: IFeedDownloader
    {
        private IEnumerable<IFeedProvider> _feedProviders;

        public FeedDownloader(IEnumerable<IFeedProvider> feedProviders)
        {
            _feedProviders = feedProviders;
        }

        public async Task<IEnumerable<GeoFeedItem>> GetFeedItems(string url, FeedType feedType)
        {
            var feedProvider = _feedProviders.FirstOrDefault(p => p.FeedType == feedType);

            if(feedProvider == null)
            {
                return new List<GeoFeedItem>();
            }

            var feed = await feedProvider.Download(url);

            return feedProvider.Parse(feed);
        }
    }
}
