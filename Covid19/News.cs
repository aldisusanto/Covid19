using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19
{
    class Covid
    {
        public News[] news { get; set; }
    }

    public class News
    {
        public string news_id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string urlToImage { get; set; }
        public string pubDate { get; set; }
        public string content { get; set; }
        public string reference { get; set; }

    }
}
