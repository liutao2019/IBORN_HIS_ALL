using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucReportTop<br></br>
    /// [功能描述: 报卡头部uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucReportTop : ucBaseMainReport
    {

        public ucReportTop()
            : this(false)
        {
        }

        public ucReportTop(bool isAddition)
        {
            InitializeComponent();
            if (isAddition)
            {
                this.label69.Text += "(附卡)";
            } 
        }

        #region 属性

        /// <summary>
        /// 报卡类型
        /// </summary>
        public string ReportType
        {
            get
            {
                return this.lbState.Text;
            }
            set
            {
                if (value != null)
                {
                    this.lbState.Text = value;
                }
                
            }
        }

        /// <summary>
        /// 报卡编号
        /// </summary>
        public string ReportNO
        {
            get
            {
                return this.lbID.Text;
            }
            set
            {
                if (value != null)
                {
                    this.lbID.Text = value;
                }
            }
        }

        /// <summary>
        /// 患者NO
        /// </summary>
        public string PatientNO
        {
            get
            {
                return this.lbPID.Text;
            }
            set
            {
                if (value != null)
                {
                    this.lbPID.Text = value;
                }
            }
        }

        #endregion

        #region 方法

        public override int SetValue(FS.HISFC.DCP.Object.CommonReport report)
        {
            if (report.CorrectFlag == "1")
            {
                this.lbState.Text = "订正报卡";
            }
            else
            {
                this.lbState.Text = "初次报卡";
            }

            this.lbID.Text = report.ReportNO;

            if (this.SetValue(report.Patient, (FS.SOC.HISFC.DCP.Enum.PatientType)System.Enum.Parse(typeof(FS.SOC.HISFC.DCP.Enum.PatientType), report.PatientType)) == -1)
            {
                return -1;
            }

            return base.SetValue(report);
        }

        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            if (!string.IsNullOrEmpty(this.lbID.Text))
            {
                report.ReportNO = this.lbID.Text;
            }

            return base.GetValue(ref report);
        }

        public override int SetValue(FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                this.lbPID.Text = patient.PID.CardNO;
                this.labelClinic.Text = "门诊卡号：";
            }
            else if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                this.lbPID.Text = patient.PID.PatientNO;
                this.labelClinic.Text = "住 院 号：";
            }
            else if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)
            {
                this.lbPID.Text = patient.PID.CardNO;
                this.labelClinic.Text = "病 历 号：";
            }
            return base.SetValue(patient,patientType);
        }

        public override void Clear()
        {
            FS.HISFC.DCP.Object.CommonReport commonReport = new FS.HISFC.DCP.Object.CommonReport();
            commonReport.CorrectFlag = "0";
            commonReport.PatientType = "C";

            this.SetValue(commonReport);
            base.Clear();
        }

        public override void PrePrint()
        {
            this.gbReportTop.BackColor = Color.White;
            this.BackColor = Color.White;
            base.PrePrint();
        }

        public override void Printed()
        {
            this.gbReportTop.BackColor = System.Drawing.Color.FromArgb(240,240,240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            base.Printed();
        }

        #endregion
    }
}
