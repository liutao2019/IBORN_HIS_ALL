using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOrder
{
    /// <summary>
    /// 收费后执行
    /// </summary>
    public class FeeConfirm : FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder
    {
        FS.HISFC.BizLogic.Registration.Register regsiterMgr = new FS.HISFC.BizLogic.Registration.Register();
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        #region IOrder 成员
        string errinfo = string.Empty;
        public string Err
        {
            get
            {
                return errinfo;
            }
            set
            {
                errinfo = value;
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
            //更新患者信息
            if (regInfo is FS.HISFC.Models.Registration.Register)
            {

                FS.HISFC.Models.Registration.Register register = regInfo as FS.HISFC.Models.Registration.Register;
                int rtn = regsiterMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo, register);

                if (rtn == -1)
                {
                    this.errinfo = regsiterMgr.Err;
                    return -1;
                }

                if (rtn == 0)//没有更新到患者信息，插入
                {
                    FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                    p.PID.CardNO = register.PID.CardNO;
                    p.Name = register.Name;
                    p.Sex.ID = register.Sex.ID;
                    p.Birthday = register.Birthday;
                    p.Pact = register.Pact;
                    p.Pact.PayKind.ID = register.Pact.PayKind.ID;
                    p.SSN = register.SSN;
                    p.PhoneHome = register.PhoneHome;
                    p.AddressHome = register.AddressHome;
                    p.IDCard = register.IDCard;
                    p.Memo = register.CardType.ID;
                    p.NormalName = register.NormalName;
                    p.IsEncrypt = register.IsEncrypt;

                    if (radtIntegrate.RegisterComPatient(p) == -1)
                    {
                        this.errinfo = "更新患者基本信息失败" + radtIntegrate.Err;
                        return -1;
                    }
                }

                return 1;
            }

            return 1;
        }

        #endregion
    }
}
