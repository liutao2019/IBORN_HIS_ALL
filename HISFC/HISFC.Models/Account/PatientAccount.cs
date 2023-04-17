using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class PatientAccount : FS.HISFC.Models.RADT.PatientInfo
    {

        #region 变量
        FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion 

        #region 属性
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            set
            {
                oper = value;
            }
            get
            {
                return oper;
            }
        }
        #endregion

        #region 方法
        public new PatientAccount Clone()
        {
            PatientAccount patient = base.Clone() as PatientAccount;
            patient.Oper = this.Oper.Clone();
            return patient;
        }

        #endregion
    }
}
