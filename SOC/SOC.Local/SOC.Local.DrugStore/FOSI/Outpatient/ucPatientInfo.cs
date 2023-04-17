using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.FOSI.Outpatient
{
    /// <summary>
    /// [功能描述: 佛四本地化门诊配发药患者信息]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、这个采用接口形式
    /// </summary>
    public partial class ucPatientInfo : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow
    {
        public ucPatientInfo()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();


        public void ShowInfo(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            if (drugRecipe == null)
            {
                return;
            }
            this.nlbInvoiceNO.Text = "电话：" +GetTelephone(drugRecipe.ClinicNO);
            this.nlbRecipeNO.Text = "处方号：" + drugRecipe.RecipeNO;
            this.nlbRecipeDept.Text = "开方：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.nlbRecipDoctor.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.nlbRegDate.Text = "挂号：" + drugRecipe.RegTime.ToString();
            this.nlbCardNO.Text = "病例号：" + drugRecipe.CardNO;
            this.nlbPatientName.Text = "姓名：" + drugRecipe.PatientName + " "+drugRecipe.Sex.Name + " " + this.GetAge(drugRecipe.Age).Trim();
            this.nlbFeeDate.Text = "收费：" + drugRecipe.FeeOper.OperTime.ToString();

            this.nlbDiagnose.Text = "诊断：" + this.GetDiagnose(drugRecipe.ClinicNO);
        }

        public void Clear()
        {
            this.nlbInvoiceNO.Text = "电话：";
            this.nlbRecipeNO.Text = "处方号：";
            this.nlbRecipeDept.Text = "开方：";
            this.nlbRecipDoctor.Text = "医生：";
            this.nlbRegDate.Text = "挂号：";
            this.nlbCardNO.Text = "病例号：";
            this.nlbPatientName.Text = "姓名：";
            this.nlbFeeDate.Text = "收费：";
            this.nlbDiagnose.Text = "诊断：";
        }

        private string GetAge(DateTime birthday)
        {
            if (birthday == null)
            {
                return "";
            }
            return deptMgr.GetAge(birthday);
        }

        /// <summary>
        /// 显示诊断信息
        /// </summary>
        private string GetDiagnose(string clinicNO)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = diagnoseMgr.QueryMainDiagnose(clinicNO, true, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return  "查询诊断出错：";
            }
            if (al.Count == 0)
            {
                al = diagnoseMgr.QueryMainDiagnose(clinicNO, false, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                if (al == null)
                {
                    return  "查询诊断出错：";
                }
                if (al.Count == 0)
                {
                    return "无";
                }
                else
                {
                    FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                    return diagnose.DiagInfo.ICD10.Name;
                }
            }
            else
            {
                FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                return diagnose.DiagInfo.ICD10.Name;
            }
            return "";
        }

        private string GetTelephone(string clinicNo)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

            FS.HISFC.Models.Registration.Register  regObj = regMgr.GetByClinic(clinicNo);
            if (regObj == null)
            {
                return "查询电话出错：";
            }
            return regObj.PhoneHome;
        }

        #region IOutpatientShow 成员

        #endregion
    }
}
