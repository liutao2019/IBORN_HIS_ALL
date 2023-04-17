using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.InpatientFee
{
    /// <summary>
    /// 病房管理里面的欠费报警的单据打印实现
    /// 此接口反射不是通过表数据读取的
    /// 是HISFC.Components里面代码写死的
    /// </summary>
    public partial class ucPatientMoneyAlterZDLY : UserControl,FS.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ucPatientMoneyAlterZDLY()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前患者信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo curPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #region IMoneyAlert 成员

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return curPatientInfo ;
            }
            set
            {
                this.curPatientInfo = value;
            }
        }

        /// <summary>
        /// 设置界面信息
        /// </summary>
        public void SetPatientInfo()
        {
            this.neuLabel17.Text = this.PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.neuLabel15.Text = this.PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.nlb姓名.Text = this.PatientInfo.Name;
            this.neuLabel11.Text = this.PatientInfo.Name;
            this.nlb总费用.Text = this.PatientInfo.FT.TotCost.ToString();
            this.nlb剩余金额.Text = this.PatientInfo.FT.LeftCost.ToString();
            this.nlb自付费用.Text = (this.PatientInfo.FT.PayCost + this.PatientInfo.FT.OwnCost).ToString();
            this.nbl预交金额.Text = this.PatientInfo.FT.PrepayCost.ToString();
            this.nbl科室.Text = this.PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.nlb打印时间.Text = this.constantMgr.GetSysDate().ToString();
            #region {86287A69-DAA9-457d-8E7D-236E46BC3EAA}
            this.nlb床号.Text = this.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.nlb住院号.Text = this.PatientInfo.PID.PatientNO;
            #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
            if (this.PatientInfo.PVisit.AdmittingDoctor.User02 == "2")
            {
                if (string.IsNullOrEmpty(this.PatientInfo.PVisit.AdmittingDoctor.User01))
                {
                    this.nlb补交金额.Text = "__________";
                }
                else
                {
                    this.nlb补交金额.Text = this.PatientInfo.PVisit.AdmittingDoctor.User01;
                }
            }
            else if (this.PatientInfo.PVisit.AdmittingDoctor.User02 == "1")
            {
                ucInputPrepayNum uc = new ucInputPrepayNum();
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = this.PatientInfo.Name;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                string inputValue = uc.InputValue;

                if (FS.FrameWork.Function.NConvert.ToDecimal(inputValue) > 0)
                {
                    this.nlb补交金额.Text = inputValue;
                }
                else
                {
                    this.nlb补交金额.Text = "__________";
                }
            }
            else
            {
                this.nlb补交金额.Text = "__________";
            }
            
            #endregion
            #endregion
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            page.Height = this.Height + 1;
            page.Width = this.Width + 1;
            //page.Name = "PhaInput";
            p.SetPageSize(page);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                p.PrintPreview(0, 0, this);
            }
            else
            {
                p.PrintPage(0, 0, this);
            }
        }

        #endregion

   
     

    
      
    }
}
