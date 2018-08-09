using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DetailView
    {
        public List<Weather> weather { get; set; }
        public Park park { get; set; }
        public List<Weather> ConvertedTemps()
        {
            List<Weather> result = new List<Weather>();
            foreach(var item in weather)
            {
                result.Add(item);
            }
            for(int i = 0; i > result.Count; i++)
            {
                result[i].HighTemp = ((result[i].HighTemp - 32) * 5 / 9);
                result[i].LowTemp = ((result[i].LowTemp - 32) * 5 / 9);
            }
            return result;

        }
    }
}