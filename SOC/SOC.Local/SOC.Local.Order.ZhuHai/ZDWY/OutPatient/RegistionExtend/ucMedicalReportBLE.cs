using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RegistionExtend
{
    public partial class ucMedicalReportBLE : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMedicalReportBLE()
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
            set { regId = value;
            this.SetInfo();
            }
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


        /// <summary>
        /// 是否打印
        /// </summary>
        private bool isPrint = false;
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint
        {
            get { return isPrint; }
            set { isPrint = value; }
        }
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        public void Clear()
        {
            this.lbPresentMedicalhistory.Text = "";
            this.lbMedicalHistory.Text = "";
            this.lbDiagnosis.Text = "";
            this.lbInvestigation.Text = "";
            this.lblBirth.Text = "";
            this.lbName.Text = "";
            this.lbPhysicalExamination.Text = "";
            this.lbRecommendation.Text = "";
            this.lbReportBy.Text = "";
            this.lbSex.Text = "";
            this.lbTreatment.Text = "";
            //this.lbVitalSigns.Text = "";
            this.labelSeeDate.Text = "";
            this.label30.Text = "";
            
        }
        /// <summary>
        /// 病历实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;

        public int SetInfo()
        {

            if (string.IsNullOrEmpty(this.regId))
            {
                this.err = "请录入门诊流水号！";
                return -1;
            }
            this.Clear();
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
            //挂号信息
            FS.HISFC.Models.Registration.Register regiter = registerMgr.GetByClinic(this.regId);
            if (regiter == null)
            {
                this.err = "请先保存病历！";
                return -1;
            }
            //体征信息
            this.labelSeeDate.Text = regiter.DoctorInfo.SeeDate.ToShortDateString();
            FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister gjMgr
                   = new FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister();
            string Pulse = "";
            string BP = "";
            string Tempitrue = "";
            System.Collections.Hashtable hsNR = gjMgr.QueryGJRegisterInfo(this.regId, "NR");
            FS.FrameWork.Models.NeuObject obj = null;
            if (hsNR != null)
            {
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
            }
            //医嘱信息
            string strOrder = "";
            System.Collections.ArrayList alRecipe = new System.Collections.ArrayList();
            if (!string.IsNullOrEmpty(regiter.Card.ID))
            {
                //{C958610E-C301-4871-A72E-FE0E9FB4A6F4}
                alRecipe = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.Card.ID, this.regId);
            }
            else
            {
                //{C958610E-C301-4871-A72E-FE0E9FB4A6F4}
                alRecipe = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.PID.CardNO, this.regId);
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
                        //orderTemp.Item.Name 
                        strOrder +=  eName + "  " + orderTemp.Frequency.Name + "  " + orderTemp.Usage.Name +  "\n\r";
                    }
                }
                if (alRecipe.Count > 12)//{E4DA74DC-1BFF-4591-A0B5-3A9184728F8F} 修改显示，超过12行，缩小字体
                {
                    this.lbTreatment.Font = new System.Drawing.Font("Times New Roman", 9F);
                }
            }

            //诊断信息
            //{C958610E-C301-4871-A72E-FE0E9FB4A6F4}
            System.Collections.ArrayList alDiag
                = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.regId,
                FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
            string strDiag = "";
            int i = 1;
            if (alDiag.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                {
                    strDiag += i.ToString() + "、 " + diag.DiagInfo.ICD10.Name + "  ";
                    i++;
                }
            }

            //病历信息
            this.caseHistory = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.regId);
            if (this.caseHistory != null)
            {
                if (string.IsNullOrEmpty(caseHistory.CaseMain) && string.IsNullOrEmpty(caseHistory.CaseNow)
                       && string.IsNullOrEmpty(caseHistory.CaseOld) && string.IsNullOrEmpty(caseHistory.CheckBody)
                       && string.IsNullOrEmpty(caseHistory.Memo))
                {
                    isPrint = false;
                }
                else
                {
                    isPrint = true;
                }
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
                this.label30.Text = caseHistory.User01;//处理 // {BAF5F82E-F492-49eb-B445-65EB6B355D66}
                this.lbTreatment.Text = strOrder;
                 
                //this.isNew = false;
            }
            else
            {
                this.err = "没有找到相关记录！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrintView()
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
