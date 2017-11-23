using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WheaterSettings
{
    public enum VisibilityUnityExpressions
    {
        SM,
        CAVOK,

    }

    public class VisibilitySettings:IVisibilitySettings
    {
        
        int visibility;
        string unity;

        public VisibilitySettings() { }

        public int visibilityLength {

            get { return visibility; }
            set { visibility = value; }
        }

        public string visibilityUnity {
            get { return unity; }
            set {

                if (Enum.IsDefined(typeof(VisibilityUnityExpressions),unity))
                    unity = value;
            }
        }

        public VisibilitySettings ReconhoceCondicoesVisibilidade(string expressao)
        {

            string expReg, matchValue, keepValue;
            VisibilitySettings vis = new VisibilitySettings();

            //expressao regular geral para visibilidade


            //investigar expressões
            expReg = "[0-9]{4}|[0-9]{2}SM|CAVOK|1/2SM|3/4SM|P[0-9]{1}SM";
            matchValue = Regex.Match(expressao, expReg).ToString();
            if (matchValue != string.Empty) // temos condicoes de visibilidade
            {
                if (Regex.IsMatch(expressao, "[0-9]{4}"))
                {
                    vis.visibilityLength = Convert.ToInt32(expressao);
                }
            }
            return vis;
        }

    }

    public interface IVisibilitySettings
    {
        int visibilityLength { get; set; }

        string visibilityUnity { get; set; }
    }

    #region metodos
   
    
    
    
    #endregion
}
