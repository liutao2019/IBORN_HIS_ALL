using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.OutpatientFee.FoSan.Interface
{
    /// <summary>
    /// 收费后操作类
    /// </summary>
    public class FeeOrder : Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder
    {
        private string err = string.Empty;
        #region IOrder 成员

        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        public int SendDrugInfo(object patientInfo, System.Collections.ArrayList alFeeInfo, bool isPositive)
        {
            return 1;
        }

        public int SendFeeInfo(object regInfo, System.Collections.ArrayList alObj, bool isPositive, string flag)
        {
            return 1;
        }

        public int SendFeeInfo(object regInfo, System.Collections.ArrayList alObj, bool isPositive)
        {
            if (regInfo is Neusoft.HISFC.Models.Registration.Register)
            {
                Neusoft.HISFC.Models.Registration.Register register=regInfo as Neusoft.HISFC.Models.Registration.Register;
                //收费
                if (isPositive)
                {
                    //更新挂号表姓名
                    Neusoft.HISFC.BizLogic.Registration.Register registerMgr = new Neusoft.HISFC.BizLogic.Registration.Register();
                    if (registerMgr.Update(Neusoft.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo, register) <= 0)
                    {
                        this.err = registerMgr.Err;
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion
    }
}
