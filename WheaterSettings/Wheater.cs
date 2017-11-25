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

    public enum AltimeterUnits
    {
        hPa, //Q
        inHg, //A
    }

    public class Wheater
    {

        AltimeterUnits altUnits;
        KindOfWhaterReported kindReport;
        DateTime dtRepoted; // the metar
        List<DateTime> tafReports = new List<DateTime>(); //  Cada alerta taf tem uma validade temporaria!
        int humidityPercentage;
        int barometer;
        List<CloudSettings> cloudsGroup = new List<CloudSettings>();
        List<WindSettings> windsGroup = new List<WindSettings>();
        List<VisibilitySettings> visibilyGroup = new List<VisibilitySettings>();
        List<TemperatureSettings> tempSettings = new List<TemperatureSettings>();
        int hpaPressure;
        string airpotICAOCode;

        public Wheater() { }


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


        public int Barometer //if (barometer > 29.92)
        {
            get { return barometer; }
            set { barometer = value; } // converter para HpA para saber a unidade !!
        }

        public List<WindSettings> Wind
        {
            get { return windsGroup; }
            set { windsGroup = value; }
        }

        public List<CloudSettings> Cloud
        {
            get { return cloudsGroup; }
            set { cloudsGroup = value; }
        }

        public List<VisibilitySettings> Visibility
        {
            get { return visibilyGroup; }
            set { visibilyGroup = value; }
        }

        public List<TemperatureSettings> Temperature
        {
            get { return tempSettings; }
            set { tempSettings = value; }
        }

        public List<DateTime> TAFAlertsReportedOn
        {
            get { return tafReports; }
            set { tafReports = value; }
        }
        public AltimeterUnits AltimeterSettingUnits
        {
            get {  return altUnits; }
            set { altUnits = value; }
        }

        public int convertBaromterPressureToHPA(double barPressure)
        {
            int value = 0;
            return value;
        }

        

    }
}
