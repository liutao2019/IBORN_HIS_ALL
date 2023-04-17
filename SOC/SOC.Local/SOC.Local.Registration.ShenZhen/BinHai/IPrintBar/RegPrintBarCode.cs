using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.Local.Registration.ShenZhen.BinHai.IRegPrint;

namespace FS.SOC.Local.Registration.ShenZhen.BinHai.IPrintBar
{
    public class RegPrintBarCode : FS.HISFC.BizProcess.Interface.Registration.IPrintBar
    {
        #region IPrintBar 成员

        public int printBar(FS.HISFC.Models.RADT.Patient p, ref string errText)
        {
            if (p is FS.HISFC.Models.Registration.Register)
            {
                FS.HISFC.Models.Registration.Register register=p as FS.HISFC.Models.Registration.Register;
                //PrintInvoiceCnt=2为补打条码
                //PrintInvoiceCnt=0为挂号时打印
                //IsFirst为第一次就诊
                if (register.PrintInvoiceCnt == 2 || (register.PrintInvoiceCnt == 0 && register.IsFirst))
                {
                    ucMedicalRecord ucMedicalRecord = new ucMedicalRecord();
                    ucMedicalRecord.SetPrintValue(p, 1);
                }
            }
            return 1;
        }

        #endregion
    }
}
