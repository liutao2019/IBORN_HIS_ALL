using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.EMR.ZDLY.Class
{
   public class CurrentOperator : Neusoft.FrameWork.Management.Database
    {
        private   Neusoft.HISFC.Models.Base.Employee currentuser= null;
        public    Neusoft.HISFC.Models.Base.Employee CurrentUser
        {
            get
            {
               return currentuser = ((Neusoft.HISFC.Models.Base.Employee)(((Neusoft.FrameWork.Management.Database)(this)).Operator));
            }
        }

    }
}
