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

    class VisibilitySettings:IVisibilitySettings
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

    }

    public interface IVisibilitySettings
    {
        int visibilityLength { get; set; }

        string visibilityUnity { get; set; }
    }

    #region metodos
   

    #endregion
}
