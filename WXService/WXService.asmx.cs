using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using WheaterSettings;

namespace WXService
{
    /// <summary>
    /// Description résumée de WXService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class WXService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        bool returnResult;
        string valorTemp;
        string[] arrayCodMETAR = new string[30];



        [WebMethod(Description = "Metodo para chamar o processo python e invocar o ficheiro py, para executar o programa")]
        public bool ChamaProcessoPY()
        {
            bool returnRes = false;
            ProcessStartInfo pythonInfo = new ProcessStartInfo();
            Process python;
            pythonInfo.FileName = @"C:\Windows\py.exe";
            pythonInfo.Arguments = @"H:\ISI_WX\Crawler\crawler.py";
            pythonInfo.CreateNoWindow = false;
            pythonInfo.UseShellExecute = true;



            python = Process.Start(pythonInfo);
            if (python.Start())
                returnRes = true;
            python.WaitForExit();
            python.Close();

            return returnRes;
        }



        [WebMethod(Description = "Permite ler o ficheiro de texto após descarregar o ficheiro METAR")]
        public bool LerFicheiroTexto(string path)
        {
            //@"../../../../../output_metar.txt"
            string text = System.IO.File.ReadAllText(path);
            if (text != "") // temos conteudo para analisar
                returnResult = true;
            else if (text == "")
                returnResult = false;

            return returnResult;
        }


       

        [WebMethod(Description = "Este metodo pega num ficheiro TXT e aproveita para um array a informação util")]
        public object[] AnalisaFicheiroTexto(string texto)
        {


            object[] arrayResultado = new object[2];
            bool devolveResultadoFinal = true;
            string valorTemp = " ";
            // [A-Z]{4} ex: LPPR EDDF EHAM KSFO
            string[] splitFicheiro = new string[30]; // array para ser usado sempre que for necessário dividir
                                                     //o texto em duas partes

            string matchConditionData = "<!--\\sD(.)+"; // Expressao regular para apanhar 
                                                        //tudo o que vem a frente de <!--Data ends here--> 







            //resultado toma valor de match(ruleTestMetar) ou resultado toma valor de match(ruleTestTAF)
            string resultado;


            string ruleTestMetar;



            string[] returnResult = new string[30];


            //retirar todo o texto que se encontra a frente da tag
            //<!-- Data ends here -->


            // se encontramos <--Data ends here --- ....
            if (Regex.Match(texto, matchConditionData).ToString() != " ")
            {
                //Dividir e guardar o que está atrás de <--Data ...
                splitFicheiro = Regex.Split(texto, matchConditionData, RegexOptions.ExplicitCapture);

                // Aproveitar a parte que contem o cod. METAR e TAF
                valorTemp = splitFicheiro[0];
            }


            // Encontrar um "\\n" para posteriormente dividir
            resultado = Regex.Match(valorTemp, "\\n").ToString();



            if (resultado == "") // se resultado é " "
            {
                // Faz-se um Regex.split(), obdecendo a esta E.R
                splitFicheiro = Regex.Split(valorTemp, @"\\n");

                // Verifica-se se splitFicheiro[0] contem um codigo que faz match com a E.R abaixo
                //definida
                string matchICAO = "[A-Z]{4}";
                if (Regex.Match(splitFicheiro[0], matchICAO).ToString() != " ")
                {
                    //Guardar código internacional
                    returnResult[0] = splitFicheiro[0];
                    valorTemp = splitFicheiro[1]; // METAR e TAF ... 
                }


            }
            try
            {
                ruleTestMetar = "<br \\/>";

                resultado = Regex.Match(valorTemp, ruleTestMetar).ToString();
                if (resultado == "<br />")
                {
                    splitFicheiro = Regex.Split(valorTemp, "<br \\/>");
                    returnResult[1] = splitFicheiro[0];
                    valorTemp = splitFicheiro[1];

                }

                //Fazer match a tudo o que está entre as TAGS <code> e </code>
                string firstMatchTAF = "<code>(.)+<\\/code>";
                resultado = Regex.Match(valorTemp, firstMatchTAF).ToString();
                if (resultado != " ")
                {
                    //procurar novamente por <br/> e dividir 
                    splitFicheiro = Regex.Split(valorTemp, "<br\\/>");

                    //guardar em return result posicao 2 esubstituir <code> por " "
                    returnResult[2] = splitFicheiro[1];
                    returnResult[2] = Regex.Replace(returnResult[2], "<code>", "");
                    Console.WriteLine("\n {0}", returnResult[2]);
                    valorTemp = splitFicheiro[2];

                }

                //procurar pela proxima instrucao TAF e guardar no array
                //existem 2 &nbsp;&nbsp; a cada instrucao TAF
                string secondMatchTAF = "[&nbsp;]{12}";
                resultado = Regex.Match(valorTemp, secondMatchTAF).ToString();
                if (resultado != " ")
                {
                    //substituir os dois &nbsp; por ""
                    valorTemp = Regex.Replace(valorTemp, secondMatchTAF, "");
                    returnResult[3] = valorTemp;

                }



                //procurar por <br/>&nbsp;&nbsp;
                valorTemp = splitFicheiro[3];
                string thirdMatchTAF = "[&nbsp;]{12}";
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();
                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[4] = valorTemp;

                }

                valorTemp = splitFicheiro[4];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();
                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[5] = valorTemp;

                }

                valorTemp = splitFicheiro[5];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();

                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[6] = valorTemp;

                }

                valorTemp = splitFicheiro[6];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();
                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[7] = valorTemp;

                }
                valorTemp = splitFicheiro[7];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();

                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[8] = valorTemp;

                }

                valorTemp = splitFicheiro[8];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();

                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[9] = valorTemp;

                }
                valorTemp = splitFicheiro[9];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();


                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[10] = valorTemp;

                }
                valorTemp = splitFicheiro[10];
                resultado = Regex.Match(valorTemp, thirdMatchTAF).ToString();

                if (resultado != " ")
                {
                    valorTemp = Regex.Replace(valorTemp, thirdMatchTAF, "");
                    returnResult[11] = valorTemp;

                }
            }
            catch (IndexOutOfRangeException ex)
            {
                devolveResultadoFinal = false;
            }

            //encontrar cada \n ou cada <br />

            arrayResultado[0] = returnResult;
            arrayResultado[1] = devolveResultadoFinal;

            return arrayResultado;
        }
    }
}
