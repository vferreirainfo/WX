using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WheaterSettings
{
    public enum TypeOfWind
    {
        Variable_Not_Know, //VRB - direcção variavel desconhecida
        Variable,    // 130V160
        HaveGust, //14530G70KT
        HavePLetterAssociated, //190P90KT
    }

    public enum WindReportedOn
    {
        TAF,
        METAR,
    }

    public enum UnityOfWindSpeed
    {
        KT,
        MS,
    }


    /// <summary>
    ///  // what is a wind
    // a wind has two major components

    // 1) direction
    // 2) speed
    // AND
    // 3) Could have gust wind associated, variable winds between two directions, for exemple: 120V160
    // or even direction not know, represented as VRB. 
    // 4) A wind can also have a P letter in a expression like 190P90KT, meaning in this 
    // example wind with 190 degrees and speed above 90 KT (very strong speed)
    /// </summary>
    public class WindSettings
    {

        TypeOfWind type;


        public WindSettings()
        {
        }

        public WindSettings(int direction, int speed)
        {
            directionOfWind = direction;
            windSpeed = speed;

        }

        /// <summary>
        ///  Un constructeur plus complet
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="minimumVariableDirection"></param>
        /// <param name="maximumVariableDirection"></param>
        /// <param name="reference"></param>
        public WindSettings(int direction, int speed, int minimumVariableDirection, int maximumVariableDirection, string reference)
        {
            directionOfWind = direction; // the actual direction
            windSpeed = speed; // the speed
            variableMinimumDirectionOfWind = minimumVariableDirection; // if happen! Minimum direction provided
            variableMaximumDirectionOfWind = maximumVariableDirection; // if happen! Maximum direction provided
            unityOfWindSpeed = reference; // the unity of wind speed
        }

        /// <summary>
        /// If we need to create a new wind with gust speed
        /// A constructor builded also to insert variations in form AAAVBBB like 120V160
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="minimumVariableDirection"></param>
        /// <param name="maximumVariableDirection"></param>
        /// <param name="reference"></param>
        public WindSettings(int direction, int speed, int gustSpeed,int minimumVariableDirection, int maximumVariableDirection, string reference)
        {
            directionOfWind = direction; // the actual direction
            windSpeed = speed; // the speed
            variableMinimumDirectionOfWind = minimumVariableDirection; // if happen! Minimum direction provided
            variableMaximumDirectionOfWind = maximumVariableDirection; // if happen! Maximum direction provided
            unityOfWindSpeed = reference; // the unity of wind speed
            windGustSpeed = gustSpeed;
        }
        /// <summary>
        /// The wind direction in the moment 
        /// </summary>
        public int directionOfWind { get; set; }

        /// <summary>
        /// The wind speed
        /// </summary>
        public int windSpeed { get; set; }


        /// <summary>
        /// Minimum variable direction. In example above should be 120
        /// </summary>
        public int variableMinimumDirectionOfWind { get; set; }


        /// <summary>
        /// Maximum variable direction. In example above should be 160
        /// </summary>
        public int variableMaximumDirectionOfWind { get; set; }


        /// <summary>
        /// The speed unity reference 
        /// </summary>
        public string unityOfWindSpeed { get; set; }

        public int windGustSpeed { get; set; }


        public TypeOfWind KindOfWindReported
        {
            get { return type; }
            set { type = value; }
        }

        #region metodos
        public WindSettings ReconheceInstrucoesDoVento(string expressao)
        {
            string matchValue;
            WindSettings wind = new WindSettings();
            int percorreArray=0;
            string[] separa = new string[2];
            // reconhecer direcao
            string expRegularTemp = "\\s[0-9]{3}";
            

            // separar direccao e velocidade
            separa = Regex.Split(expressao, expRegularTemp, RegexOptions.Singleline); // guarda tudo do vento excepto direccao
            separa[percorreArray] = Regex.Match(expressao, expRegularTemp).ToString(); // guarda direccao
            wind.directionOfWind = Convert.ToInt32(separa[0]);
            percorreArray++;//1

            //encontrar unidade de velocidade no array separa indice 1
            expRegularTemp = "KT";
            matchValue = Regex.Match(separa[percorreArray], expRegularTemp).ToString();
            if (matchValue != string.Empty)
            {
                wind.unityOfWindSpeed = matchValue;
            }


            //reconhecer velocidade
            expRegularTemp = "^[0-9]{2}KT";
            matchValue = Regex.Match(separa[percorreArray], expRegularTemp).ToString();
            if (matchValue != string.Empty)
            {
                matchValue = Regex.Match(separa[percorreArray], "[0-9]{2}").ToString();
                if (matchValue != string.Empty)
                    wind.windSpeed = Convert.ToInt32(matchValue);

            }
            // tentar reconhecer vento com velocidade Gust

            expRegularTemp = "^[0-9]{2}G[0-9]{2}KT";
            matchValue = Regex.Match(separa[percorreArray], expRegularTemp).ToString();
            if (matchValue != string.Empty)
            {

                // reconhecer velocidade inicial (a que vem antes de G)
                expRegularTemp = "[0-9]{2}G";
                matchValue = Regex.Match(matchValue, expRegularTemp).ToString();
                if (matchValue != string.Empty)
                {
                    matchValue = Regex.Match(matchValue, "[0-9]{2}").ToString();
                    wind.windSpeed = Convert.ToInt32(matchValue);
                }


                //reconhecer velocidade de rajada (a que vem entre G e a unidade de vel)
                expRegularTemp = "[0-9]{2}KT";
                matchValue = Regex.Match(expressao, expRegularTemp).ToString();
                if (matchValue != string.Empty)
                {
                    matchValue = Regex.Match(matchValue, "[0-9]{2}").ToString();
                    wind.windGustSpeed = Convert.ToInt32(matchValue);
                }
            }
            return wind;
        }
        #endregion
    }
}
