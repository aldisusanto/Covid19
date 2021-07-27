using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19
{

    public class Country
    {
        public string get { get; set; }
        public object[] parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public string[] response { get; set; }
    }

}
