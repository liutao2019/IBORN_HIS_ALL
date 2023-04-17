using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// 附卡基类
    /// </summary>
    public partial class ucBaseAddition : ucBaseMainReport, FS.SOC.HISFC.BizProcess.DCPInterface.IAddition
    {
        public ucBaseAddition()
        {
            InitializeComponent();
           this.InitDTAdditionReport();
        }

        #region 域变量

        /// <summary>
        /// 报告主卡
        /// </summary>
        private FS.HISFC.DCP.Object.CommonReport report=null;

        /// <summary>
        /// 附卡xml数据表
        /// </summary>
        private DataTable dtAdditionReportXML;

        /// <summary>
        /// 附卡xml数据集
        /// </summary>
        private DataSet dsAdditionReportXML;

        /// <summary>
        /// 传染病管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseReport = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

        /// <summary>
        /// 患者编号
        /// </summary>
        private string patientNO;

        /// <summary>
        /// 患者名称
        /// </summary>
        private string patientName;

        #endregion

        #region 方法

        /// <summary>
        /// 初始化附卡数据表
        /// </summary>
        public void InitDTAdditionReport()
        {
            this.dsAdditionReportXML = new DataSet();
            this.dtAdditionReportXML = new DataTable("Addition");
            this.dtAdditionReportXML.Columns.Add("Type", typeof(string));
            this.dtAdditionReportXML.Columns.Add("Name", typeof(string));
            this.dtAdditionReportXML.Columns.Add("Text", typeof(string));
            this.dtAdditionReportXML.Columns.Add("Tag", typeof(string));
            this.dtAdditionReportXML.Columns.Add("Value", typeof(string));
            this.dsAdditionReportXML.Tables.Add(this.dtAdditionReportXML);
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public DataRow GetValue(Control c)
        {
            if (c is Label)
            {
                return null;
            }
            DataRow dr = this.dtAdditionReportXML.NewRow();

            dr["Type"] = c.GetType().FullName.ToString();
            dr["Name"] = c.Name.ToString();
            dr["Text"] = c.Text.ToString();
            if (c is FS.FrameWork.WinForms.Controls.NeuRadioButton)
            {
                dr["Value"] = FS.FrameWork.Function.NConvert.ToInt32(((FS.FrameWork.WinForms.Controls.NeuRadioButton)c).Checked).ToString();
            }
            if (c is FS.FrameWork.WinForms.Controls.NeuCheckBox)
            {
                dr["Value"] = FS.FrameWork.Function.NConvert.ToInt32(((FS.FrameWork.WinForms.Controls.NeuCheckBox)c).Checked).ToString();
            }
            if (c is FS.FrameWork.WinForms.Controls.NeuComboBox)
            {
                dr["Value"] = ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).Tag.ToString();
            }
            if (c is FS.FrameWork.WinForms.Controls.NeuTextBox)
            {
                dr["Value"] = ((FS.FrameWork.WinForms.Controls.NeuTextBox)c).Text;
            }
            if (c.Tag != null)
            {
                dr["Tag"] = c.Tag.ToString();
            }
            return dr;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="dr"></param>
        public void SetValue(DataRow dr, Control container)
        {
            if (dr == null)
            {
                return;
            }
            foreach (Control c in this.GetAllControls(container))
            {
                if (c.GetType().FullName == dr["Type"].ToString() && c.Name == dr["Name"].ToString())
                {
                    c.Text = dr["Text"].ToString();
                    c.Tag = dr["Tag"];
                    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuRadioButton))
                    {
                        ((FS.FrameWork.WinForms.Controls.NeuRadioButton)c).Checked = FS.FrameWork.Function.NConvert.ToBoolean(dr["Value"].ToString());
                    }
                    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                    {
                        ((FS.FrameWork.WinForms.Controls.NeuCheckBox)c).Checked = FS.FrameWork.Function.NConvert.ToBoolean(dr["Value"].ToString());
                    }
                    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                    {
                        ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedValue = FS.FrameWork.Function.NConvert.ToBoolean(dr["Value"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有的控件
        /// </summary>
        /// <returns></returns>
        public List<Control> GetAllControls(Control container)
        {
            List<Control> listControl = new List<Control>();
            foreach (Control c in container.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                {
                    listControl.Add(c);
                }
                else if (c.Controls.Count > 0)
                {
                    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                    {
                        listControl.Add(c);
                        continue;
                    }
                    List<Control> list = this.GetAllControls(c);
                    if (list != null)
                    {
                        listControl.AddRange(list);
                    }
                }
                else
                {
                    listControl.Add(c);
                }

            }
            return listControl;
        }

        public virtual void Clear()
        {
            this.Clear(this);
        }

        private void Clear(Control mainControl)
        {
            foreach (Control c in mainControl.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                {
                    ((FS.FrameWork.WinForms.Controls.NeuCheckBox)c).Checked=false;
                }
                else if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox))
                {
                    c.Text = "";
                }
                else if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                {
                    c.Text = "";
                    c.Tag = "";
                }
                else if (c.Controls.Count > 0)
                {
                    this.Clear(c);
                }
            }
        }

        #endregion

        #region IAddition成员

        #region 属性

        /// <summary>
        /// 患者编号
        /// </summary>
        public string PatientNO
        {
            get
            {
                return patientNO;
            }
            set
            {
                patientNO = value;
            }
        }

        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName
        {
            get
            {
                return patientName;
            }
            set
            {
                patientName = value;
            }
        }

        /// <summary>
        /// 报告主卡
        /// </summary>
        public FS.HISFC.DCP.Object.CommonReport Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }

        #endregion

        /// <summary>
        /// 写入附卡信息
        /// </summary>
        /// <param name="additionReport"></param>
        public void SetAdditionInfo(FS.HISFC.DCP.Object.AdditionReport additionReport, Control container)
        {
            if (string.IsNullOrEmpty(additionReport.ReportXML))
            {
                MessageBox.Show("注意：附卡没有填写，当前界面显示为默认值。原因可能是在报卡时还不需要填写附卡！");
                return;
            }
            StringReader stringReader = new StringReader(additionReport.ReportXML);
            this.dsAdditionReportXML.ReadXml(stringReader);
            foreach (DataTable dt in this.dsAdditionReportXML.Tables)
            {
                this.dtAdditionReportXML = dt;
                foreach (DataRow dr in this.dtAdditionReportXML.Rows)
                {
                    this.SetValue(dr, container);
                }
            }
        }

        /// <summary>
        /// 读取附卡信息
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.AdditionReport GetAdditionInfo(Control container)
        {
            this.dtAdditionReportXML.Clear();
            FS.HISFC.DCP.Object.AdditionReport additionReport = new FS.HISFC.DCP.Object.AdditionReport();
            foreach (Control c in this.GetAllControls(container))
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuLabel) || c.GetType() == typeof(Label))
                {
                    continue;
                }
                DataRow dr = this.GetValue(c);
                this.dtAdditionReportXML.Rows.Add(dr);
            }
            additionReport.ReportXML = this.dsAdditionReportXML.GetXml();
            return additionReport;
        }

        /// <summary>
        /// 验证附卡信息是否完整
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual int IsValid(ref string msg)
        {
            return 1;
        }

        /// <summary>
        /// 修改附卡信息
        /// </summary>
        /// <returns></returns>
        public int UpdateAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition)
        {
            if (addition != null && addition is FS.HISFC.DCP.Object.AdditionReport)
            {
                FS.HISFC.DCP.Object.AdditionReport additionReport = addition as FS.HISFC.DCP.Object.AdditionReport;
                additionReport.Report = this.Report;
                int state = diseaseReport.UpdateAdditionReport(additionReport);
                if (state <= 1)
                {
                    return diseaseReport.InsertAdditionReport(additionReport);
                }
                else
                {
                    return state;
                }
            }
            return 1;
        }

        /// <summary>
        /// 增加附卡信息
        /// </summary>
        /// <param name="addition"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int InsertAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition)
        {
            if (addition != null && addition is FS.HISFC.DCP.Object.AdditionReport)
            {
                FS.HISFC.DCP.Object.AdditionReport additionReport = addition as FS.HISFC.DCP.Object.AdditionReport;
                additionReport.Report = this.Report;
                additionReport.PatientNO = this.Report.Patient.ID;
                additionReport.PatientName = this.Report.Patient.Name;
                return diseaseReport.InsertAdditionReport(additionReport);
            }
            return 1;
        }

        /// <summary>
        /// 删除附卡信息
        /// </summary>
        /// <param name="addition"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int DeleteAdditionInfo()
        {
            return diseaseReport.DeleteAdditionReport(this.Report.ReportNO);
        }

        /// <summary>
        /// 获取附卡信息
        /// </summary>
        /// <param name="reportNO"></param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.AdditionReport GetAdditionInfo(string reportNO)
        {
            return diseaseReport.GetAdditionReport(reportNO);
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
