using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common
{
        /// <summary>
        /// [功能描述: 门诊配发药患者信息]<br></br>
        /// [创 建 者: cube]<br></br>
        /// [创建时间: 2010-12]<br></br>
        /// 说明：
        /// 1、这个采用接口形式，程序检测到本地化后不再用这个显示
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
                this.nlbInvoiceNO.Text = "发票：" + drugRecipe.InvoiceNO;
                this.nlbRecipeNO.Text = "处方号：" + drugRecipe.RecipeNO;
                this.nlbRecipeDept.Text = "开方：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
                this.nlbRecipDoctor.Text = "医生：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
                this.nlbRegDate.Text = "挂号：" + drugRecipe.RegTime.ToString();
                this.nlbCardNO.Text = "病例号：" + drugRecipe.CardNO;
                this.nlbPatientName.Text = "姓名：" + drugRecipe.PatientName + " " + drugRecipe.Sex.Name + " " + this.GetAge(drugRecipe.Age).Trim();
                this.nlbFeeDate.Text = "收费：" + drugRecipe.FeeOper.OperTime.ToString();

                this.nlbDiagnose.Text = "诊断：" + this.GetDiagnose(drugRecipe.ClinicNO);
            }

            public void Clear()
            {
                this.nlbInvoiceNO.Text = "发票：";
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
                    return "查询诊断出错：";
                }
                if (al.Count == 0)
                {
                    al = diagnoseMgr.QueryCaseDiagnoseForClinicByState(clinicNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, true);
                    if (al == null)
                    {
                        return "查询诊断出错：";
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
            }

            #region IOutpatientShow 成员

            #endregion
        }
    }

