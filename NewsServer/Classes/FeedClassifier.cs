using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsServer.Classes
{
    public class FeedClassifier: IFeedClassifier
    {
        private const string ACCEPT_HEADER_NAME = "Accept";
    }
}
