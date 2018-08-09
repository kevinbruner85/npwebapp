using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }

        public int FiveDayForecastValue { get; set; }

        public int LowTemp { get; set; }
        
        public int HighTemp { get; set; }

        public string Forecast { get; set; }
        public string GetForecastMessage()
        {
            Dictionary<string, string> forecastMessage = new Dictionary<string, string>()
            {
                {"snow", "Be sure to pack snowshoes!" },
                {"rain", "Pack raingear and wear waterproof shoes!" },
                {"thunderstorms", "Seek shelter and avoid hiking on exposed ridges." },
                {"sunny", "Pretty much everywhere, it's gonna be hot." },
                {"partly cloudy", "It's gonna be partly cloudy." },
                {"cloudy", "It's gonna be full cloudy." }
            };
            string result = forecastMessage[Forecast];
            return result;
        }

        public string GetTemperatureMessage()
        {
            string result = "";
            if(HighTemp > 75)
            {
                result = "Bring an extra gallon of water.";
            }
            else if((HighTemp - LowTemp) > 20)
            {
                result = "Wear breathable layers.";
            }
            else if(HighTemp < 20)
            {
                result = "Beware the dangers of exposure to frigid temperatures.";
            }
            return result;
        }
        public string GetTemperatureMessageC()
        {
            string result = "";
            if (HighTemp > 23)
            {
                result = "Bring an extra gallon of water.";
            }
            else if ((HighTemp * 1.8 + 32) - (LowTemp * 1.8 + 32) > 20)
            {
                result = "Wear breathable layers.";
            }
            else if (HighTemp < (-6))
            {
                result = "Beware the dangers of exposure to frigid temperatures.";
            }
            return result;
        }
        public List<string> Scales()
        {
            List<string> result = new List<string>();
            result.Add("Farenheit");
            result.Add("Celsius");
            return result;
        }

       
    }
}