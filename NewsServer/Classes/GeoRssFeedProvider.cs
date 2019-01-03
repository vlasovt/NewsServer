using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsServer.Classes
{
    public class GeoRssFeedProvider: IFeedProvider
    {
        private const string ACCEPT_HEADER_NAME = "Accept";
        private const string ACCEPT_HEADER_VALUE = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        private const string USER_AGENT_NAME = "User-Agent";
        private const string USER_AGENT_VALUE = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
        private const string GEORSS_CONVERTER = "http://api.geonames.org/rssToGeoRSS?feedUrl={0}&username=ascold";

        private const string DATE_PARSE_FORMAT = "ddd, dd MMM yyyy HH:mm";

        private HttpClient _httpClient;

        public FeedType FeedType => FeedType.Rss;

        public GeoRssFeedProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> Download(string url)
        {
            url = string.Format(GEORSS_CONVERTER, url); //System.Net.WebUtility.UrlDecode(url);
            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.TryAddWithoutValidation(ACCEPT_HEADER_NAME, ACCEPT_HEADER_VALUE);
                request.Headers.TryAddWithoutValidation(USER_AGENT_NAME, USER_AGENT_VALUE);

                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            }

            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            var statusCode = (int)response.StatusCode;
            var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            return Encoding.UTF8.GetString(content);
        }

        public IEnumerable<GeoFeedItem> Parse(string feedContent)
        {
            if (string.IsNullOrEmpty(feedContent))
            {
                return new List<GeoFeedItem>();
            }

            try
            {
                feedContent = RemoveInvalidCharacters(feedContent);

                var feedDoc = XDocument.Parse(feedContent);
                var docRoot = feedDoc.Root;
                var channel = docRoot.Descendants("channel").FirstOrDefault();
                var channelTitle = channel.Element("title")?.Value;
                var channelLink = channel.Element("link")?.Value;
                var items = docRoot.Descendants("item");

                var feedItems = new List<GeoFeedItem>();

                foreach (var item in items)
                {
                    var coordinates = GetCoordinates(item);

                    if (coordinates == null)
                    {
                        continue;
                    }

                    var description = item.Element("description")?.Value;

                    if(!string.IsNullOrEmpty(description) 
                        && description.IndexOf('<') > 0)
                    {
                        description = description.Substring(0,description.IndexOf('<'));
                    }

                    var dateString = item.Element("pubDate")?.Value;
                    DateTime? pubDate = null;

                    if (!string.IsNullOrEmpty(dateString))
                    {
                        pubDate = DateTime.Parse(dateString, CultureInfo.CurrentCulture);
                    }

                    var geoFeedItem = new GeoFeedItem
                    {
                        ChannelTitle = channelTitle,
                        ChannelLink = channelLink,
                        Date = pubDate.HasValue? pubDate: null,
                        Title = item.Element("title")?.Value,
                        Description = description,
                        Link = item.Element("link")?.Value,
                        Coordinates = coordinates
                    };

                    feedItems.Add(geoFeedItem);
                };

                return feedItems;
            }
            catch (Exception)
            {
                return new List<GeoFeedItem>();
            }
        }

        /// <summary>
        /// Removes invalid characters from an xml document
        /// </summary>
        /// <param name="feedContent"></param>
        /// <returns></returns>
        private string RemoveInvalidCharacters(string feedContent)
        {
            feedContent = feedContent.Replace(((char)0x1C).ToString(), string.Empty); // replaces special char 0x1C, fixes issues with at least one feed
            feedContent = feedContent.Replace(((char)65279).ToString(), string.Empty); // replaces special char, fixes issues with at least one feed

            return feedContent;
        }

        /// <summary>
        /// Gets Coordinates
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private double[] GetCoordinates(XElement item)
        {
            double[] coordinates;

            try
            {
                XNamespace geo = "http://www.w3.org/2003/01/geo/wgs84_pos#";
                var latitude = item.Element(geo + "lat")?.Value;
                var longitude = item.Element(geo + "long")?.Value;

                if (string.IsNullOrEmpty(latitude)
                    || string.IsNullOrEmpty(longitude))
                {
                    return null;
                }
                coordinates = new double[] { double.Parse(latitude), double.Parse(longitude) };
            }
            catch (Exception)
            {
                return null;
            }

            return coordinates;
        }
    }
}
