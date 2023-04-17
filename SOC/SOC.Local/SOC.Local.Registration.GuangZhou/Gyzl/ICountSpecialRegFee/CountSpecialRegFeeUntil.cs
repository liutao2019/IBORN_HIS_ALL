using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.ICountSpecialRegFee
{
    public class CountSpecialRegFeeUntil : FS.HISFC.BizProcess.Interface.Registration.ICountSpecialRegFee
    {
        #region ICountSpecialRegFee 成员
        private CountSpecialRegFeeManager speRegFeeManager = new CountSpecialRegFeeManager();

        public int CountSpecialRegFee(DateTime birthday, string name, string idenno, ref decimal regfee, ref decimal digfee, ref decimal othfee, ref FS.HISFC.Models.Registration.Register regObj)
        {
            //1.老人70岁减免挂号费regfee = 0;
            //2.本院离退休职工减免诊金digfee = 0;
            string isOld = "0";
            string isRetailed = "0";
            if (speRegFeeManager.VailRetailed(name, idenno))
            {
                digfee = 0;
                isRetailed = "1";
            }
            if (birthday.AddYears(70) < speRegFeeManager.GetDateTimeFromSysDateTime())
            {
                regfee = 0;
                isOld = "1";
            }
            if (regObj != null)
            {
                regObj.Mark1 = isOld + "|" + isRetailed;
            }
            return 1;
        }

        #endregion
    }
}
