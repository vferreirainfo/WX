using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheaterSettings
{
    public enum KindOfWhaterReported
    {
        Metar,
        TAF,
        Normal,
    }


    public class WheaterSettings
    {


        KindOfWhaterReported kindReport;
        DateTime dtRepoted;
        int humidityPercentage;
        double barometer;
        List<CloudSettings> cloudsGroup = new List<CloudSettings>();
        List<WindSettings> windsGroup = new List<WindSettings>();
        List<VisibilitySettings> visibilyGroup = new List<VisibilitySettings>();
        int hpaPressure;
        string airpotICAOCode;

        public WheaterSettings() { }

        
        public KindOfWhaterReported TypeOfReport
        {
            get { return kindReport; }
            set { kindReport = value; }
        }


        public DateTime dtReported
        {
            get { return dtReported; }
            set { dtReported = value; }
        }


        public int Humidity
        {
            get { return humidityPercentage; }
            set { humidityPercentage = value; }
        }

        public string CodigoICAO
        {
            get { return airpotICAOCode; }
            set { airpotICAOCode = value; }
        }


        public double Barometer
        {
            get { return barometer; }
            set { if(barometer>29.92) barometer = value; } // converter para HpA para saber a unidade !!
        }


        public int convertBaromterPressureToHPA(double barPressure)
        {
            int value = 0;
            return value;
        }

        


    }
}
