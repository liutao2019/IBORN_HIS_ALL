using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.DiagnoseProve
{
    public partial class ucDiagnoseProve : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Order.IDiagnosisProvePrint
    {
        public ucDiagnoseProve()
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
            set
            {
                regId = value;
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
        private bool isPrint = true;
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint
        {
            get { return isPrint; }
            set { isPrint = value; }
        }

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 门诊诊断证明书和病假条信息管理
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.DiagExtend diagExtMgr = new FS.HISFC.BizLogic.Order.OutPatient.DiagExtend();

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            lblNO.Text = "";
            lblName.Text = "";
            lblSex.Text = "";
            lblAge.Text = "";
            lblPatientID.Text = "";
            lblDept.Text = "";
            lblDiagnoses.Text = "";
            lblSeeDate.Text = "";
            lblOpinions.Text = "";
            lblPrintDate.Text = "";
            //lblCaseMain.Text = "";
            //lblCaseNow.Text = "";

        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <returns></returns>
        public int SetInfo()
        {
            this.Clear();

            if (string.IsNullOrEmpty(this.regId))
            {
                this.err = "请录入门诊流水号！";
                return -1;
            }
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
          
            //挂号信息
            FS.HISFC.Models.Registration.Register register = registerMgr.GetByClinic(this.regId);
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
            caseHistory = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.regId);

            FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
            diagExtend = this.diagExtMgr.QueryByClinicCodCardNo(this.regId, register.PID.CardNO);

            this.lblNO.Text = diagExtend.ProveNo;
            this.lblPatientID.Text = register.PID.CardNO;
            this.lblDept.Text = register.DoctorInfo.Templet.Dept.Name;
            this.lblName.Text = register.Name;
            this.lblSex.Text = register.Sex.Name;
            this.lblAge.Text = this.outOrderMgr.GetAge(register.Birthday, false);
            this.lblPrintDate.Text = Convert.ToDateTime(registerMgr.GetSysDateTime()).GetDateTimeFormats('f')[0].ToString();
            this.lblSeeDate.Text = register.DoctorInfo.SeeDate.ToString();
            this.lblDept2.Text = register.DoctorInfo.Templet.Dept.Name;
            this.lblDoct.Text = register.DoctorInfo.Templet.Doct.Name;

            //诊断
            System.Collections.ArrayList alDiag
                = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.regId,
                FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, true);
            string strDiag = "";
            int i = 1;
            if (alDiag.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                {
                    if (string.IsNullOrEmpty(diag.Memo))
                    {
                        strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + " ";
                    }
                    else
                    {
                        strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "（" + diag.Memo + "） ";
                    }
                    i++;
                }
            }

            ////主诉
            //this.lblCaseMain.Text = diagExtend.CaseMain;
           
            ////现病史
            //this.lblCaseNow.Text = diagExtend.CaseNow; 

            this.lblDiagnoses.Text = strDiag;

            if (!string.IsNullOrEmpty(diagExtend.Opinions))
            {
                this.lblOpinions.Text = diagExtend.Opinions; ;//治疗意见
            }
            else
            {
                this.lblOpinions.Text = caseHistory.CaseMain + "；" + caseHistory.CaseNow + "；" + caseHistory.User01; ;//治疗意见
            }

            return 1;

        }

        /// <summary>
        /// 预览
        /// </summary>
        public void PrintView()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pageSize = new FS.HISFC.Models.Base.PageSize();
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("A5");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 550, 850);
                }
            }
            print.SetPageSize(pageSize);
            print.IsDataAutoExtend = false;
            print.IsLandScape = true;
            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
                print.PrintPreview(0, 0, this);
            //}
            //else
            //{
            //    print.PrintPage(0, 0, this);
            //}

        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pageSize = new FS.HISFC.Models.Base.PageSize();

            pageSize = pageSizeMgr.GetPageSize("A5");
            if (pageSize != null && pageSize.Printer.ToLower() == "default")
            {
                pageSize.Printer = "";
            }
            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize("A5", 550, 850);
            }

            print.SetPageSize(pageSize);
            print.IsDataAutoExtend = false;
            print.IsLandScape = true;
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
                print.PrintPreview(0, 0, this);
            //}
            //else
            //{
            //    print.PrintPage(0, 0, this);
            //}

        }
    }

}
