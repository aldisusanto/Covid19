using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19
{

    public class Rootobject
    {
        public string get { get; set; }
        public Parameters parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public Response[] response { get; set; }
    }

    public class Parameters
    {
        public string country { get; set; }
    }

    public class Response
    {
        public string country { get; set; }
        public Cases cases { get; set; }
        public Deaths deaths { get; set; }
        public Tests tests { get; set; }
        public string day { get; set; }
        public DateTime time { get; set; }
    }

    public class Cases
    {
        public string New { get; set; }
        public string active { get; set; }
        public string critical { get; set; }
        public string recovered { get; set; }
        public string total { get; set; }
    }

    public class Deaths
    {
        public string New { get; set; }
        public string total { get; set; }
    }

    public class Tests
    {
        public string total { get; set; }
    }

}
