using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsServer.Classes
{
    public interface IFeedProvider
    {
        FeedType FeedType { get; }

        Task<string> Download(string url);
        IEnumerable<GeoFeedItem> Parse(string feedContent);
    }
}
