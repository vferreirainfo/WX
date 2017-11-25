using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WheaterSettings
{


    public enum TemperatureUnity
    {
        C, // celsius
        F, // fahrenheit
        K, //kevin 
    }

    public enum TemperatureReportedOn
    {
        METAR,
        TAF,
    }

    public class TemperatureSettings
    {



        DateTime reportedOn;


        public TemperatureSettings() { }

        public TemperatureSettings(int min, int max, int dewPoint, TemperatureUnity tempUn)
        {
            minTemperature = min;
            maxTemperature = max;
            dewPointTemperature = dewPoint;
            temperatureMeansure = tempUn;
        }


        // a wheater reported on a day has
        public int minTemperature { get; set; }

        public int maxTemperature { get; set; }


        public int dewPointTemperature { get; set; }

        public TemperatureUnity temperatureMeansure { get; set; }

        public TemperatureReportedOn TypeReport {get;set;}


        #region METODOS
        public TemperatureSettings ReconheceCodificacaoTemperaturaMinMax(string exp)
        {
            string[] splitMinMax = new string[2];
            TemperatureSettings temp = new TemperatureSettings();
            string  keptValue, regExp;
            keptValue = exp;

            //M - minus: Representação de temperaturas negativas
            //TX - Designacao opcional para representar temperatura máxima
            //TN - Designacao opcional para representar temperatura minima

            regExp = "[M]?[0-9]{2}/[M]?[0-9]{2}|[TX]?[M]?[0-9]{2}|[TN]?[M]?[0-9]{2}";
            //matchValue = Regex.Match(exp, regExp).ToString();
            if (Regex.IsMatch(exp, regExp))
            {
                
                //testar qual das ocorrencias de temperatura temos 
                if(Regex.Match(exp, "[M]?[0-9]{2}/[M]?[0-9]{2}").ToString() != string.Empty)
                {
                    splitMinMax = Regex.Split(exp, "/", RegexOptions.Singleline);
                    // reconhecer tempMax e tempMin negativa (T<0)
                    if((Regex.Match(splitMinMax[0], "^[M][0-9]{2}").ToString() != null)&& (Regex.Match(splitMinMax[1], "^[M][0-9]{2}").ToString() != null))
                    {
                        // temos temperatura maxima inferior a 0ºC
                        string t = Regex.Replace(splitMinMax[0], "M", ""); // substituimos M por ""
                        temp.maxTemperature = -Convert.ToInt32(t);
                        t = Regex.Replace(splitMinMax[1], "M", ""); // substituimos M por ""
                        temp.minTemperature = -Convert.ToInt32(t);

                    }
                    //reconhecer temperatura maxima e minima positiva (T>0)
                    else if ((Regex.Match(splitMinMax[0], "^[0-9]{2}").ToString() != null) && (Regex.Match(splitMinMax[1], "^[0-9]{2}").ToString() != null))
                    {
                        // temos temperatura maxima inferior a 0ºC
                        temp.maxTemperature = -Convert.ToInt32(splitMinMax[0]);
                        temp.minTemperature = -Convert.ToInt32(splitMinMax[1]);

                    }
                    
                    
                }
            }
            return temp;
        }

        #endregion
    }
}
