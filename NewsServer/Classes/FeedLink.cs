using System;

namespace NewsServer.Classes
{
    public class FeedLink
    {
        public string Url { get; set; }

        public string Type { get; set; }

        public FeedType FeedType
        {
            get
            {
                if(string.IsNullOrEmpty(Type))
                {
                    return FeedType.Undefined;
                }

                return (FeedType)Enum.Parse(typeof(FeedType), Type, true);
            }
        }
    }
}
