using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheaterSettings
{



    public class CloudSettings
    {

        public enum TypeOfCloud
        {
            Cumulus,
            Cumulonimbus,
            Stratus,
            Stratocumulus,
            Nimbostratus,
            Altostratus,
            Altocumulus,
            Cirrus,
            Cirrocumulos,
            Cirrostratus,
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


    }
}
