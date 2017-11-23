using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheaterSettings
{

    public enum TypeOfPrecipitation
    {
        Rain,
        Snow,
    }


    public enum RateOfPrecipitation
    {
        VeryLow,
        Low,
        Normal,
        Moderate,
        Fast,
        VeryFast,
    }

    public class Precipitation
    {

        int temperatureOfPrecipitation { get; set; }

        int phOfPrecipitation { get; set; }

        TypeOfPrecipitation typePrec { get; set; }

        RateOfPrecipitation ratePrec { get; set; }


        public Precipitation (int tempOfPrec, int phOfPrec, TypeOfPrecipitation type, RateOfPrecipitation rate)
        {
            temperatureOfPrecipitation = tempOfPrec;
            phOfPrecipitation = phOfPrec;
            typePrec = type;
            ratePrec = rate;
        }
    }
}
