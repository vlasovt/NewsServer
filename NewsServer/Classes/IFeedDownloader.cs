using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsServer.Classes
{
    public interface IFeedDownloader
    {
        Task<IEnumerable<GeoFeedItem>> GetFeedItems(string url, FeedType feedType);
    }
}
