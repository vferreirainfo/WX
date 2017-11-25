using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Text.RegularExpressions;
using WheaterSettings;

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
            string[] arrayCodMet = new string[30];
            arrayCodMet = AnalisaEPurificaFicheiro.AnalisaFicheiroTexto(text, out estado);
            AnalisaEPurificaFicheiro.RetomaObjetoWheater(arrayCodMet);
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

            public static Wheater RetomaObjetoWheater(string[] array)
            {
                //variaveis -->
                //Existe a necessidade de dividir o metar após o vento
                string[] divideCodigo = new string[2];

                CloudSettings cloud = new CloudSettings();
                WindSettings wind = new WindSettings();
                int contaTime = 0;
                Wheater wx = new Wheater();
                string expReg, matchValue, keepValue; // expressao regular variavel, resultado match e valor original da expressao
                DateTime dt = new DateTime();
                TimeSpan t;
                VisibilitySettings visibility = new VisibilitySettings();
                TemperatureSettings temp = new TemperatureSettings();
                List<string> conjuntoCodigosTAF = new List<string>();
                string[] time = new string[2]; // guarda hora e minuto em 2 indices
                string[] arrayMETAR = new string[15];
                string[] arrayTemp = new string[15];
                bool result;


                //-->Método

                //percorrer array 
                #region separaMETAR_E_TAF
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                        break;
                    if (i == 1) // temos um METAR
                    {
                        //percorrer linha 1 e separar para novo array cada instrucao

                        arrayMETAR = Regex.Split(array[i], "\\s", RegexOptions.Singleline);
                    }
                    if (i > 1)
                    {
                        arrayTemp = Regex.Split(array[i], "\\s", RegexOptions.Singleline);
                        foreach (string s in arrayTemp)
                        {
                            conjuntoCodigosTAF.Add(s);
                        }
                        i++;
                    }
                }
                #endregion

                //METAR
                #region decifraMETAR
                for (int j = 0; j < arrayMETAR.Length; j++)
                {
                    expReg = "[A-Z]{4}";
                    //1º instrucao: aeroporto
                    if (Regex.Match(arrayMETAR[j], expReg).ToString() != string.Empty)
                    {
                        wx.CodigoICAO = Regex.Match(arrayMETAR[j], expReg).ToString();
                    }
                    else
                    {
                        // Data e hora
                        dt = DateTime.Today;

                        //reconhecer hora e minutos
                        expReg = "[0-9]{4}Z";
                        matchValue = Regex.Match(array[j], expReg).ToString();
                        if (matchValue != string.Empty && j==1) // data está na posicao 1 do array
                        {
                            //substituir Z por ""
                            matchValue = Regex.Replace(matchValue, "Z", "");

                            //guardar hora/minuto numa segunda string
                            keepValue = matchValue;

                            //reconhecer hora
                            expReg = "^[0-9]{4}";
                            matchValue = Regex.Match(matchValue, expReg).ToString();
                            if (matchValue != string.Empty)
                            {
                                time[contaTime] = matchValue[contaTime] + matchValue[contaTime + 1].ToString();
                                contaTime += 2;
                                time[contaTime - 1] = matchValue[contaTime] + matchValue[contaTime + 1].ToString();
                                t = new TimeSpan(Convert.ToInt32(time[contaTime - 2]), Convert.ToInt32(time[contaTime - 1]), 0);
                                dt = dt + t;

                            }
                        }

                        //reconhecer vento
                        expReg = "[0-9]{4,6}KT|sVRB[0-9]{2}KT|[0-9]{5}G[0-9]{2}KT";
                        string variableWindRegex = "([0-9]{3}V[0-9]{3})?";
                        matchValue = Regex.Match(arrayMETAR[j], expReg).ToString();
                        string matchVarWinds = Regex.Match(arrayMETAR[j + 1], variableWindRegex).ToString();


                        
                        if (matchValue != string.Empty && matchVarWinds != string.Empty)
                        {
                           
                            wind = wind.ReconheceInstrucoesDoVento(matchValue, matchVarWinds);
                            
                            //reconhecer vento variavel com variancia conhecida (120V160)
                            //Tal expressao pode ocorrer (ou não) quer em METAR quer em TAF
                            wx.Wind.Add(wind);
                        }
                        else if(matchValue != string.Empty && matchVarWinds == string.Empty)
                        {

                            wind = wind.ReconheceInstrucoesDoVento(matchValue, "");

                            wx.Wind.Add(wind);

                        }




                        //reconhecer visibilidade
                        expReg = "^[0-9]{4}|^[0-9]{2}SM|CAVOK|1/2SM|3/4SM|P[0-9]{1}SM";
                        matchValue = Regex.Match(arrayMETAR[j], expReg).ToString();
                        if (matchValue != string.Empty && j==3)
                        {
                            visibility = visibility.ReconhoceCondicoesVisibilidade(matchValue);
                            //Adicionar visibilidade a lista
                            wx.Visibility.Add(visibility);

                        }

                        // reconhecer nuvens
                        expReg = "FEW[0-9]{3}(CB|TCU)?|BKN[0-9]{3}(CB|TCU)?|SCT[0-9]{3}(CB|TCU)?|OVC[0-9]{3}(CB|TCU)?";
                        if (Regex.Match(arrayMETAR[j], expReg).ToString() != string.Empty)
                        {
                            cloud = cloud.ReconheceInstrucaoCloud(Regex.Match(arrayMETAR[j], expReg).ToString());
                            wx.Cloud.Add(cloud);
                        }

                        //reconhece temperatura
                        string matchValueII; // nas nuvens apanha-se numeros de altitude com tamanho 3!
                        expReg = "^[M]?[0-9]{2}/[M]?[0-9]{2}|^[TX]?[M]?[0-9]{2}|^[TN]?[M]?[0-9]{2}";
                        matchValueII = Regex.Match(arrayMETAR[j], expReg).ToString();
                        if (matchValueII != matchValue && j>4)
                        {
                            temp.TypeReport = TemperatureReportedOn.METAR;
                            temp = temp.ReconheceCodificacaoTemperaturaMinMax(matchValueII);
                            wx.Temperature.Add(temp);
                        }


                        //reconhece altimetro
                        expReg = "A[0-9]{4}|Q[0-9]{4}";
                        matchValue = Regex.Match(arrayMETAR[j], expReg).ToString();
                        if(matchValue != string.Empty)
                        {
                            keepValue = matchValue;
                            //Testar se é A ou Q que faz match
                            matchValue = Regex.Match(matchValue, "A|Q").ToString();
                            if(matchValue == "A")
                            {
                                wx.AltimeterSettingUnits = AltimeterUnits.inHg;
                                //fazer match aos quatro numeros após o A
                                expReg = "[^A][0-9]{4}";
                                matchValue = Regex.Match(keepValue, expReg).ToString();
                                if (matchValue != string.Empty)
                                    wx.Barometer = Convert.ToInt32(matchValue);
                            }
                            else if(matchValue == "Q")
                            {
                                wx.AltimeterSettingUnits = AltimeterUnits.hPa;

                                //fazer match aos quatro numeros após o A
                                expReg = "[^Q][0-9]{4}";
                                matchValue = Regex.Match(keepValue, expReg).ToString();
                                if (matchValue != string.Empty)
                                    wx.Barometer = Convert.ToInt32(matchValue);

                            }
                        }
                        
                   }
                   
                }
                #endregion

                //TAF
                #region decifraTAF
                //Para cada string na lista verificar ... se:
                foreach (string s in conjuntoCodigosTAF)
                {
                    //hora temporaria

                    //detectar dia e hora de alerta ... primeiros dois digitos
                    expReg = "[0-9]{6}";
                    matchValue = Regex.Match(s, expReg).ToString();
                    if(matchValue != string.Empty)
                    {
                        expReg = "[0-9]{2}";
                        matchValue = Regex.Match(s, expReg).ToString();
                        if(matchValue!=string.Empty)
                        {
                            //guardar dia (considerando o mês atual)
                            dt = dt.AddDays(Convert.ToInt32(matchValue));
                            matchValue = matchValue + "/" + dt.Month + "/" + dt.Year;
                            dt = DateTime.Parse(matchValue);
                        }

                        //apanhar hora
                        expReg = "[0-9]{4}Z";
                        matchValue = Regex.Match(s, expReg).ToString();
                        if (matchValue != string.Empty)
                        {
                            matchValue = Regex.Replace(matchValue, "Z", "");
                            //reconhecer hora
                            expReg = "^[0-9]{4}";
                            matchValue = Regex.Match(matchValue, expReg).ToString();
                            if (matchValue != string.Empty)
                            {
                                contaTime = 0;
                                time[contaTime] = matchValue[contaTime] + matchValue[contaTime + 1].ToString();
                                contaTime += 2;
                                time[contaTime - 1] = matchValue[contaTime] + matchValue[contaTime + 1].ToString();
                                t = new TimeSpan(Convert.ToInt32(time[contaTime - 2]), Convert.ToInt32(time[contaTime - 1]), 0);
                                dt = dt + t;
                                
                            }    
                        }

                        // apanhar vento
                        expReg = "[0-9]{4,6}KT|sVRB[0-9]{2}KT|[0-9]{5}G[0-9]{2}KT";
                        matchValue = Regex.Match(s, expReg).ToString();
                        if(matchValue != string.Empty)
                        {

                        }

                    }


                }
                #endregion

                return wx;
            }

            
        }
    }
}
