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
        int directionOfWind { get; set; }

        /// <summary>
        /// The wind speed
        /// </summary>
        int windSpeed { get; set; }


        /// <summary>
        /// Minimum variable direction. In example above should be 120
        /// </summary>
        int variableMinimumDirectionOfWind { get; set; }


        /// <summary>
        /// Maximum variable direction. In example above should be 160
        /// </summary>
        int variableMaximumDirectionOfWind { get; set; }


        /// <summary>
        /// The speed unity reference 
        /// </summary>
        string unityOfWindSpeed { get; set; }

        int windGustSpeed { get; set; }


        TypeOfWind KindOfWindReported
        {
            get { return type; }
            set { type = value; }
        }

        #region metodos
        public bool ReconheceInstrucoesDoVento(string expressao)
        {
            bool returnResult = true;
            //Reconhecer direcção inicial 
            string expRegular = "[0-3]{1}[0-9]{1}[0-9]{1} | VRB[0-9]{2}KT";
            string tempVal = Regex.Match(expressao, expRegular).ToString();

            if (tempVal != "") // se reconhecemos algo
            {

                // se direccao está entre 0 e 360
                if (Convert.ToInt32(tempVal) >= 0 && Convert.ToInt32(tempVal) <= 360)
                {
                    // set the wind direction
                    this.directionOfWind = Convert.ToInt32(tempVal);
                }

                // caso contrario se VRB
                if (tempVal == "VRB")
                {
                    type = TypeOfWind.Variable_Not_Know;
                }

            }
            else if (tempVal == "")
            {
                // se nao existir vento retoma falso, pois nao adicionamos nada ao 
                //objeto windssettings
                returnResult = false;
            }


            // direccao variavel em intervalo! nem sempre poderá ocorrer

            string expReg = "(\\s[0-3]{1}[0-9]{1}[0-9]{1}V[0-3]{1}[0-9]{1}[0-9]{1})?";
            tempVal = Regex.Match(expressao, expReg).ToString();
            if (tempVal != "") // temos ventos variaveis do genero AAAVBBB
            {
                // match the first and match the second one
                string expRegMinDir = "\\s[0-9]{3}V";
                string expRegMaxDir = "V[0-9]{3}\\s";

                string valRegMin, valRegMax;
                valRegMin = Regex.Match(expressao, expRegMinDir).ToString();
                if (valRegMin != "")
                {
                    //esta direcao encontra-se entre 0 e 360
                    if (Convert.ToInt32(valRegMin) >= 0 && Convert.ToInt32(valRegMin) <= 360)
                    {
                        this.variableMinimumDirectionOfWind = Convert.ToInt32(valRegMin);
                    }

                }

                valRegMax = Regex.Match(expressao, expRegMaxDir).ToString();
                if (valRegMax != "")
                {
                    // esta direccao do vento variavel encontra-se entre 0 e 360
                    if (Convert.ToInt32(valRegMax) >= 0 && Convert.ToInt32(valRegMax) <= 360)
                    {
                        this.variableMaximumDirectionOfWind = Convert.ToInt32(valRegMax);
                    }

                }
                
            }
            return returnResult;
        }
        #endregion
    }
}
