using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheaterSettings
{


    public enum TemperatureUnity
    {
        C, // celsius
        F, // fahrenheit
        K, //kebin 
    }
    public class TemperatureSettings
    {

        

        // a wheater reported on a day has
        int minTemperature { get; set; }

        int maxTemperature { get; set; }


        int dewPointTemperature { get; set; }
        
        TemperatureUnity temperatureMeansure { get; set; }
        


        public TemperatureSettings (int min, int max, int dewPoint, TemperatureUnity tempUn)
        {
            minTemperature = min;
            maxTemperature = max;
            dewPointTemperature = dewPoint;
            temperatureMeansure = tempUn;
        }
            
    }
}
