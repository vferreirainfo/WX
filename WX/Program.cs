using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Text.RegularExpressions;

namespace WX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Read METAR/TAF file");
            string text = System.IO.File.ReadAllText(@"H:/../../../../../output_metar.txt");


            //string metarDevolvido;
            //bool method = checkStringForUpperCaseOrNumber(text, out metarDevolvido);

            //Console.WriteLine("Metar {0} ", metarDevolvido);

            //TransformaMetarEmXML(metarDevolvido);
            bool estado;
            //string estado;
            AnalisaEPurificaFicheiro.AnalisaFicheiroTexto(text, out estado);
        }








        public static class AnalisaEPurificaFicheiro
        {

            static Regex rule;
            static string resultado;



            public static string[] AnalisaFicheiroTexto(string texto, out bool estado)
            {

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
                        valorTemp = Regex.Replace(valorTemp, "<\\/code>", "");
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

                estado = devolveResultadoFinal;
                return returnResult;
            }
        }
    }
}
