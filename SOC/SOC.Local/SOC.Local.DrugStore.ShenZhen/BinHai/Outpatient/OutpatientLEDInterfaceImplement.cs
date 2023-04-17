using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    /// <summary>
    /// 香港大学深圳医院叫号本地化实现
    /// </summary>
    public class OutpatientLEDInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientLED
    {
     

        FS.SOC.Local.DrugStore.ShenZhen.Bizlogic.DrugStoreAsign drugStoreAsignMgr = new FS.SOC.Local.DrugStore.ShenZhen.Bizlogic.DrugStoreAsign();

        private FS.FrameWork.Models.NeuObject privDept = null;

        /// <summary>
        /// 将传入drugRecipe实体转换成DrugStoreAsign实体
        /// </summary>
        /// <param name="drugRecipe"></param>
        private FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign changeDrugRecipetoDrugStoreAsign(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign = new FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign();
            drugStoreAsign.recipeNO = drugRecipe.RecipeNO;
            drugStoreAsign.patientId = drugRecipe.ClinicNO;
            drugStoreAsign.cardNO = drugRecipe.CardNO;
            drugStoreAsign.PatientName = drugRecipe.PatientName;
            drugStoreAsign.patientSex = drugRecipe.Sex.ID.ToString();
            drugStoreAsign.deptCode = drugRecipe.DoctDept.ID;
            drugStoreAsign.drugDeptCode = drugRecipe.StockDept.ID;
            drugStoreAsign.sendTerminalCode = drugRecipe.SendTerminal.ID;
            drugStoreAsign.sendTerminalName = SOC.Local.DrugStore.ShenZhen.Common.Function.GetTerminalNameById(drugRecipe.SendTerminal.ID);
            drugStoreAsign.Oper.ID = this.drugStoreAsignMgr.Operator.ID;
            drugStoreAsign.Oper.OperTime = this.drugStoreAsignMgr.GetDateTimeFromSysDateTime();
            return drugStoreAsign;
        }

        #region IOutpatientLED 成员

        public int AutoShowData(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, bool operBusying)
        {
            return 1;
        }

        public int SetLED()
        {
            return 1;
        }

        public int ShowDataAfterSave(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe savedDrugRecipe)
        {
            if (string.IsNullOrEmpty(savedDrugRecipe.RecipeNO) || string.IsNullOrEmpty(savedDrugRecipe.DrugDept.ID))
            {
                return -1;
            }
            FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign = this.changeDrugRecipetoDrugStoreAsign(savedDrugRecipe);          

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                this.drugStoreAsignMgr.DeleteByRecipeNO(drugStoreAsign.recipeNO,drugStoreAsign.drugDeptCode);
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            finally
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 1;
        }

        public int ShowDataAfterSelect(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe)
        {
            if (selectedDrugRecipe == null)
            {
                return -1;
            }
            if (string.IsNullOrEmpty(selectedDrugRecipe.RecipeNO) || string.IsNullOrEmpty(selectedDrugRecipe.DrugDept.ID))
            {
                return -1;
            }

            if (this.privDept == null )
            {
                this.privDept = selectedDrugRecipe.StockDept as FS.FrameWork.Models.NeuObject;  
            }
            FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign = this.changeDrugRecipetoDrugStoreAsign(selectedDrugRecipe);
           
            string strSql = @"select * from PHA_SOC_CallQueue where REICIPE_NO='{0}' and DRUG_DEPT_CODE='{1}'";
            strSql = string.Format(strSql, drugStoreAsign.recipeNO, drugStoreAsign.drugDeptCode);
            string  returnValue = this.drugStoreAsignMgr.ExecSqlReturnOne(strSql);
            if (FS.FrameWork.Function.NConvert.ToDecimal(returnValue) == 1)
            {
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                this.drugStoreAsignMgr.Insert(drugStoreAsign);
                this.drugStoreAsignMgr.UpdateRecipe(drugStoreAsign.drugDeptCode, drugStoreAsign.recipeNO);
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            finally
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }


            return 1;
        }

        //{014680EC-6381-408b-98FB-A549DAA49B82}
        public int OverNO(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe)
        {
            return 1;
        }

        #endregion
    }
}
