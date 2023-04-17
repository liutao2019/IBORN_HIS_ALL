using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.QiaoTou.Outpatient
{
    public class RecipeQueryConvertImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRecipeQueryConvert
    {

        #region IRecipeQueryConvert 成员

        public string ConvertCardNO(string sampleCardNO)
        {
            //根据患者主索引接口获取门诊病历号
            FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();

            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            if (feeIntegrate.ValidMarkNO(sampleCardNO, ref accountCardObj) <= 0)
            {
                MessageBox.Show(feeIntegrate.Err);
                return "";
            }
            return accountCardObj.Patient.PID.CardNO;
        }

        public string ConvertClinicNO(string sampleClinicNO)
        {
            return sampleClinicNO;
        }

        public string ConvertInvoiceNO(string sampleInvoiceNO)
        {
            return sampleInvoiceNO.PadLeft(12, '0');
        }

        public string ConvertRecipeNO(string sampleRecipeNO)
        {
            return sampleRecipeNO;
        }

        public string ConvertToClinicNO(string curDayNO)
        {
            return curDayNO;
        }

        public string ConvertToCurDayNO(string clinicNO)
        {
            return clinicNO;
        }


        //public string ConvertMarkNO(string sampleMarkNO)
        //{
        //    //根据患者主索引接口获取门诊病历号
        //    FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();

        //    FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        //    if (feeIntegrate.ValidMarkNO(sampleMarkNO, ref accountCardObj) <= 0)
        //    {
        //        MessageBox.Show(feeIntegrate.Err);
        //        return "";
        //    }
        //    return accountCardObj.Patient.PID.CardNO;

        //}

        #endregion
    }
}
