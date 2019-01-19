namespace NewsServer.Classes
{
    public interface IFeedClassifier
    {
        void ClassifyNewsItem(GeoFeedItem newsItem);
    }
}