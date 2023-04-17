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
    /// �������������Ƿ�ѱ����ĵ��ݴ�ӡʵ��
    /// �˽ӿڷ��䲻��ͨ�������ݶ�ȡ��
    /// ��HISFC.Components�������д����
    /// </summary>
    public partial class ucPatientMoneyAlter : UserControl,FS.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucPatientMoneyAlter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo curPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #region IMoneyAlert ��Ա

        /// <summary>
        /// ������Ϣ
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
        /// ���ý�����Ϣ
        /// </summary>
        public void SetPatientInfo()
        {
            this.nlb����.Text = this.PatientInfo.Name;
            this.nlb�ܷ���.Text = this.PatientInfo.FT.TotCost.ToString();
            this.nlbʣ����.Text = this.PatientInfo.FT.LeftCost.ToString();
            this.nlb��ӡʱ��.Text = this.constantMgr.GetSysDate().ToString();
            #region {86287A69-DAA9-457d-8E7D-236E46BC3EAA}
            this.nlb����.Text = this.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.nlbסԺ��.Text = this.PatientInfo.PID.PatientNO;
            #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
            if (this.PatientInfo.PVisit.AdmittingDoctor.User02 == "2")
            {
                if (string.IsNullOrEmpty(this.PatientInfo.PVisit.AdmittingDoctor.User01))
                {
                    this.nlb�������.Text = "__________";
                }
                else
                {
                    this.nlb�������.Text = this.PatientInfo.PVisit.AdmittingDoctor.User01;
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
                    this.nlb�������.Text = inputValue;
                }
                else
                {
                    this.nlb�������.Text = "__________";
                }
            }
            else
            {
                this.nlb�������.Text = "__________";
            }
            
            #endregion
            #endregion
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            //page.Height = 342;
            //page.Width = 342;
            //page.Name = "PhaInput";
            //p.SetPageSize(page);
            p.PrintPage(0, 0, this);
        }

        #endregion
    }
}
