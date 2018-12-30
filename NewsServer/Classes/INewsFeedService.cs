using System.Collections.Generic;

namespace NewsServer.Classes
{
    public interface INewsFeedService
    {
        List<GeoFeedItem> GetNewsFeed();
    }
}
