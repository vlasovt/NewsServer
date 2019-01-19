using Microsoft.ML.Runtime.Api;

namespace NewsServer.Classes
{
    public class NewsData
    {
        [Column(ordinal: "0")]
        public string Text;

        [Column(ordinal: "1", name: "Label")]
        public string Label;
    }
}
