using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsServer.Classes
{
    public class FeedClassifier: IFeedClassifier
    {
        private enum Categories
        {
            mil_crisis = 1,
            pol_crisis,
            dip_crisis,
            econ_crisis,
            terror,
            nat_desaster,
            accident,
            rights,
            elections,
            protests,
            spy,
            social,
            diplomacy,
            health_crisis,
            military,
            environment,
            corruption,
            econ_develop,
            human_crisis,
            justice,
            unrecognized
        };

        private string _modelPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Assets/news-model.txt");

        public void ClassifyNewsItem(GeoFeedItem newsItem)
        {
            PredictionModel<NewsData, NewsPrediction> model = null;
            if (File.Exists(_modelPath))
            {
                model = PredictionModel.ReadAsync<NewsData, NewsPrediction>(_modelPath).Result;
            }

            if (model == null)
            {
                return;
            }

            var textToClassify = newsItem.Title + " " + newsItem.Description;

            var prediction = model.Predict(new NewsData { Text = textToClassify });

            Console.WriteLine("Prediction result:");

            var results = new Dictionary<string, float>();

            for (var i = 1; i <= prediction.Score.Count(); i++)
            {
                results.Add(Enum.GetName(typeof(Categories), i), prediction.Score[i - 1]);
            }

            newsItem.Category = results.OrderByDescending(s => s.Value).First().Key;
        }
    }
}
