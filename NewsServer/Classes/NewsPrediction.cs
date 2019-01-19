using Microsoft.ML.Runtime.Api;

namespace NewsServer.Classes
{
    public class NewsPrediction
    {
        [ColumnName("Score")]
        public float[] Score;
    }
}
