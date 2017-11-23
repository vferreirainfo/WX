using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WheaterSettings
{
    public class WheaterReport
    {
        List<WheaterSettings> currentDayWheaterReports = new List<WheaterSettings>();

        public WheaterReport(){

        }





        #region metodos

        public static bool ReconheceReporteMetereologicoAeronautico (string [] arrayMet)
        {
            bool result = true;
            WheaterSettings wx = new WheaterSettings();
            string reconheceCodInternacional;
            string reconheceInstrucaoMETAR; // passo a passo
            string reconheceConjuntoTAF; // passo a passo
            string valTemp;

            for (int i=0; i<arrayMet.Length; i++)
            {
                // se encontrarmos posicoes vazias no array ... "" ... break
                if (arrayMet[i] == "")
                    break;
                else if(arrayMet[i] != "")//caso contrario
                {
                    // passo 1 -  reconhecer aeroporto
                    // O codigo internacional do aeroporto esta no formato AAAA

                    reconheceCodInternacional = "[A-Z]{4}";
                    valTemp = Regex.Match(arrayMet[i], reconheceCodInternacional).ToString();
                    if(valTemp != "")// vamos assinar atributo wx com este valor
                    {
                        wx.CodigoICAO = valTemp;
                    }

                    // Reconhece data e hora 
                    reconheceInstrucaoMETAR = "\\s[0-9]{6}Z";
                    valTemp = Regex.Match(arrayMet[i], reconheceInstrucaoMETAR).ToString();
                    if(valTemp != "") //Temos data
                    {
                        DateTime dtReport= WheaterReport.ReconheceDataHoraZulu(valTemp);
                        wx.dtReported = dtReport;
                    }



                    //Reconhecer instrucao de ventos variaveis e nao variaveis
                    reconheceInstrucaoMETAR = "\\s[0-9]{4,6}KT ([0-9]{3}V[0-9]{3})? |\\sVRB[0-9]{2}KT ||\\s[0-9]{5}G[0-9]{2}KT";
                    valTemp = Regex.Match(arrayMet[i], reconheceInstrucaoMETAR).ToString();
                    if(valTemp != "") //Temos dados
                    {
                        //WindSettings.ReconheceDefinicoesDeVento(x,y)
                    }




                }
                
            
            }
            return result;  
        }


        public static DateTime ReconheceDataHoraZulu (string expressao)
        {
            string val;
            DateTime dt = DateTime.Today;
            TimeSpan time;
            string expRegHora = "[0-2][0-9]{2}"; // formato de 24h
            string expRegMinuto = "[0-5][0-9]{2}";
            string expRegularSeg = "[0-5][0-9]{2}";

            val = Regex.Match(expressao, expRegHora).ToString();

       
            
            if (val != "") // hora reconhecida
            {
                dt.AddHours(Convert.ToDouble(val));
            }

            
            val = Regex.Match(expressao, expRegMinuto).ToString();
            
            if(val != "")
            {
                dt.AddMinutes(Convert.ToDouble(val));
            }

            val = Regex.Match(expressao, expRegularSeg).ToString();

            if (val != "")
            {
                dt.AddSeconds(Convert.ToDouble(val));
            }


            return dt;
        }
        #endregion

    }
}
