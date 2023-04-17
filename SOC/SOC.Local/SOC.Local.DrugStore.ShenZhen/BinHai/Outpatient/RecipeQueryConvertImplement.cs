using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    public class RecipeQueryConvertImplement:SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRecipeQueryConvert
    {
        #region IRecipeQueryConvert 成员
        FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();

        FS.HISFC.BizProcess.Integrate.Pharmacy pharamacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient.PackageCharge.PackageChargeHelper packageChargeHelper = new FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient.PackageCharge.PackageChargeHelper();

        /// <summary>
        /// 就诊卡号转换
        /// </summary>
        /// <param name="sampleCardNO"></param>
        /// <returns></returns>
        public string ConvertCardNO(string sampleCardNO)
        {
            ArrayList allFeeItemList = null;

            if (feeIntegrate.ValidMarkNO(sampleCardNO, ref accountCardObj) <= 0)
            {
                MessageBox.Show(feeIntegrate.Err);
            }
            sampleCardNO = accountCardObj.Patient.PID.CardNO;

            string errInfo = string.Empty;
            ArrayList allRegister = packageChargeHelper.GetRegisterByCardNO(sampleCardNO);

            if (allRegister == null || allRegister.Count == 0)
            {
                return sampleCardNO;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Registration.Register r in allRegister)
            {
                int returnValue = packageChargeHelper.InsertApplyOut(r,ref errInfo);
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errInfo);
                    return sampleCardNO;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();


            return sampleCardNO;
        }

        public string ConvertClinicNO(string sampleClinicNO)
        {
            return sampleClinicNO;
        }

        public string ConvertInvoiceNO(string sampleInvoiceNO)
        {
            return sampleInvoiceNO.PadLeft(12,'0');
        }

        public string ConvertRecipeNO(string sampleRecipeNO)
        {
            #region 根据cis处方号得出我们的处方号，传过去
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            FS.HISFC.BizLogic.Registration.Register RegisterMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.HISFC.Models.Registration.Register Register = new FS.HISFC.Models.Registration.Register();
            string sampleCardNO = string.Empty;
            sampleCardNO = dbMgr.ExecSqlReturnOne("select a.clinic_code from met_ord_recipedetail a where a.sequence_no=(select b.his_item_code from hl7_item_map b where  b.hl7_item_code2 ='" + sampleRecipeNO + "' and b.hl7_item_code='P' and rownum =1)");
            
            //if (feeIntegrate.ValidMarkNO(sampleCardNO, ref accountCardObj) <= 0)
            //{
            //    MessageBox.Show(feeIntegrate.Err);
            //}
            //sampleCardNO = accountCardObj.Patient.PID.ID;

            sampleRecipeNO = dbMgr.ExecSqlReturnOne("select a.recipe_no from met_ord_recipedetail a where a.sequence_no=(select b.his_item_code from hl7_item_map b where  b.hl7_item_code2 ='" + sampleRecipeNO + "' and b.hl7_item_code='P' and rownum =1)");

            string errInfo = string.Empty;
            //ArrayList allRegister = packageChargeHelper.QueryByRecipe(sampleCardNO);

            //if (allRegister == null || allRegister.Count == 0)
            //{
            //    return sampleRecipeNO;
            //}
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            Register = RegisterMgr.GetByClinic(sampleCardNO);
            if (Register == null)
            {
                MessageBox.Show("查不到病人信息,请核对信息");
            }
            int returnValue = packageChargeHelper.InsertApplyOut(Register, ref errInfo);
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errInfo);
                return sampleCardNO;
            }
  
            FS.FrameWork.Management.PublicTrans.Commit();

       
            #endregion
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

        #endregion
    }
}
