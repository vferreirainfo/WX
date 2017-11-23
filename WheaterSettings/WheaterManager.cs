using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheaterSettings
{
    public static class WheaterManager
    {
        static List<WheaterSettings> wheaterRreportsCollection = new List<WheaterSettings>();


        static bool pendentResult = false; // add a wheater report, imply a call to a method and that method need to return a result

        public static bool AddNewWheaterReport(WheaterSettings wxSettings, string typeWx, out string errorMsg)
        {
            //?? try-catch

            string exceptionMessage=string.Empty;
            try
            { 
                if(typeWx == KindOfWhaterReported.Metar.ToString())
                {


                    // add the metar wheater report to db
                }
                else if(typeWx == KindOfWhaterReported.Normal.ToString())
                {
                    // add normal wheater report to db
                }
            } catch(ArgumentNullException ex)
            {
                exceptionMessage = ex.Message;
            }
            errorMsg = exceptionMessage;
            return false;
        }

        public static List<WheaterSettings> WheaterReportCollection
        {
            get { return wheaterRreportsCollection; }
            private set { wheaterRreportsCollection = value; }
        }
    }
}
