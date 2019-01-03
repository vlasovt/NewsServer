using System;
using System.Collections.Generic;

namespace NewsServer.Classes
{
    public class GeoFeedItem
    {
        public string ChannelTitle { get; set; }
        public string ChannelLink { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public double [] Coordinates { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
