using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    public partial class ucMedicalReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMedicalReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string regId = "";

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string RegId
        {
            get { return regId; }
            set { regId = value; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        private string err = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            get { return err; }
            set { err = value; }
        }
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// 病历实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;

        public int Print()
        {
            if (string.IsNullOrEmpty(this.regId))
            {
                this.err = "请录入门诊流水号！";
                return -1;
            }
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
            //挂号信息
            FS.HISFC.Models.Registration.Register regiter = registerMgr.GetByClinic(this.regId);
            //体征信息
            this.labelSeeDate.Text = regiter.DoctorInfo.SeeDate.ToShortDateString();
            FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister gjMgr
                   = new FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister();
            string Pulse = "";
            string BP = "";
            string Tempitrue = "";
            System.Collections.Hashtable hsNR = gjMgr.QueryGJRegisterInfo(this.regId, "NR");
            FS.FrameWork.Models.NeuObject obj = null;
            if (hsNR.ContainsKey("NRtBPulse"))
            {
                obj = hsNR["NRtBPulse"] as FS.FrameWork.Models.NeuObject;
                Pulse = obj.Memo;
            }
            if (hsNR.ContainsKey("NRtBBP"))
            {
                obj = hsNR["NRtBBP"] as FS.FrameWork.Models.NeuObject;
                BP = obj.Memo;
            }
            if (hsNR.ContainsKey("NRtBTemp"))
            {
                obj = hsNR["NRtBTemp"] as FS.FrameWork.Models.NeuObject;
                Tempitrue = obj.Memo;
            }

            //医嘱信息
            string strOrder = "";
            System.Collections.ArrayList alRecipe = new System.Collections.ArrayList();
            if (!string.IsNullOrEmpty(regiter.Card.ID))
            {
                alRecipe = FS.HISFC.Components.Order.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.Card.ID, this.regId);
            }
            else
            {
                alRecipe = FS.HISFC.Components.Order.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.PID.CardNO, this.regId);
            }
                //(this.regId, regiter.DoctorInfo.SeeNO.ToString());
            string eName = "";
            if (alRecipe.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in alRecipe)
                {
                    if (orderTemp != null && orderTemp.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        eName = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderTemp.Item.ID).NameCollection.EnglishName;
                        if (string.IsNullOrEmpty(eName))
                        {
                            eName = orderTemp.Item.Name;
                        }
                        strOrder = strOrder + "\n\r"
                            + eName //orderTemp.Item.Name 
                            + "  " + orderTemp.Frequency.Name + "  " + orderTemp.Usage.Name;
                    }
                }
            }

            //诊断信息
            System.Collections.ArrayList alDiag
                = FS.HISFC.Components.Order.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.regId,
                FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
            string strDiag = "";
            if (alDiag.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                {
                    strDiag = strDiag + "\n\r" + diag.DiagInfo.ICD10.Name;
                }
            }

            //病历信息
            this.caseHistory = FS.HISFC.Components.Order.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.regId);
            if (this.caseHistory != null)
            {
                this.lbInvestigation.Text = caseHistory.CaseMain;//主诉*
                this.lbPresentMedicalhistory.Text = caseHistory.CaseNow;//现病史*
                this.lbMedicalHistory.Text = caseHistory.CaseOld;//既往史*
                this.lbPhysicalExamination.Text = caseHistory.CheckBody;//查体*
                this.lbRecommendation.Text = caseHistory.Memo;//备注
                //this.tbAllergicHistory.Text = caseHistory.CaseAllery;//过敏史
                this.lbDiagnosis.Text = strDiag;//诊断
                //this.txtDiagnose.Text = caseHistory.CaseDiag;
                //this.OldOperTime = caseHistory.CaseOper.OperTime.ToString();//操作时间
                this.lbReportBy.Text = registerMgr.Operator.Name +"\n"+DateTime.Now.ToString();
                this.lbVitalSigns.Text = string.Format(this.lbVitalSigns.Text, BP, Pulse, Tempitrue);
                this.lbName.Text = regiter.Name;
                this.lbSex.Text = regiter.Sex.ID.ToString();
                this.lblBirth.Text = regiter.Birthday.ToShortDateString();//
                this.lbTreatment.Text = strOrder;
                 
                //this.isNew = false;
            }
            else
            {
                this.err = "没有找到相关记录！";
                return -1;
            }
            this.PrintView();
            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("A4");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A4", 850, 1100);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }



    }
}
