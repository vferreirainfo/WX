using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WheaterSettings
{



    public class CloudSettings
    {

        public enum TypeOfCloud
        {
            Sem_Nuvens, //NSC - Sem Nuvens Significativas
            Poucas_Nuvens, //FEW 
            Nuvens_Esparsas, //SCT
            Nublado, //BKN
            Ceu_Encoberto, //OVC
            Poucas_Nuvens_Cumulonimbus, //FEW000CB
            Poucas_Nuvens_Cumulus_congestus, //FEW000TCU
            Nuvens_Esparsas_Cumulonimbus, //SCT000CB
            Nublado_Nuvens_Cumulonimbus, //BKN000CB

        }

        public enum ClassOfCloud
        {
            nuvens_baixas,
            nuvens_medias,
            nuvens_altas,

        }


        int topLevel { get; set; }

        int level { get; set; }

        TypeOfCloud kindOfCloud {get; set;}

        ClassOfCloud cloudClass { get; set;  }


        public CloudSettings (int lvlCloud, int topLvl, TypeOfCloud typeCloud, ClassOfCloud classCloud)
        {
            level = lvlCloud;
            topLevel = topLvl;
            kindOfCloud = typeCloud;
            cloudClass = classCloud;

        }

        public CloudSettings()
        {
        }

        public CloudSettings  ReconheceInstrucaoCloud (string expressao)
        {
            bool isMatch;
            CloudSettings cloud = new CloudSettings();
            string expReg, keepValue, matchValue;
            keepValue = expressao;


            //expressão regular geral 
            expReg = "(FEW[0-9]{3}|BKN[0-9]{3}|SCT[0-9]{3}|OVC[0-9]{3}|BKN[0-9]{3}TCU|FEW[0-9]{3}TCU)";
            isMatch = Regex.IsMatch(expressao, expReg);
            if(isMatch == true) // temos nuvens! necessitamos de determinar o tipo
            {
                expReg = "FEW|BKN|SCT|OVC|FEW|[A-Z]{3}\\d{3}[A-Z]{2,3}";

                matchValue = Regex.Match(expressao, expReg).ToString();
                if (matchValue == "FEW")
                {
                    cloud.kindOfCloud = TypeOfCloud.Poucas_Nuvens;
                }
                else if (matchValue == "BKN")
                {
                    cloud.kindOfCloud = TypeOfCloud.Nublado;
                }
                else if (matchValue == "OVC")
                {
                    cloud.kindOfCloud = TypeOfCloud.Ceu_Encoberto;
                }
                else if (matchValue == "SCT")
                {
                    cloud.kindOfCloud = TypeOfCloud.Nuvens_Esparsas;
                }
                else
                {
                    expReg = "[A-Z]{3}\\d{3}[A-Z]{2,3}";

                    //tentar encontrar nuvens "compostas"
                    if (Regex.IsMatch(expressao, expReg))
                    {

                        matchValue = Regex.Replace(expressao, "\\d{3}", "");
                        if(matchValue=="BKNCB")
                        {
                            cloud.kindOfCloud = TypeOfCloud.Nublado_Nuvens_Cumulonimbus;
                        }
                    }
                }
            }

            return cloud;
        }


    }
}
