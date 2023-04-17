using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Neusoft.UFC.DCP.DiseaseReport.UC
{
    /// <summary>
    /// [功能描述： 基类附卡uc]
    /// [创 建 者： ZJ]
    /// [创建时间： 2008-08-25]
    /// </summary>
    public partial class ucBaseAddition : Neusoft.FrameWork.WinForms.Controls.ucBaseControl,Neusoft.HISFC.DCP.Interface.IAddition
    {
        public ucBaseAddition()
        {
            InitializeComponent();
            InitDTAdditionReport();
        }

        #region 域变量

        /// <summary>
        /// 报告主卡
        /// </summary>
        private Neusoft.HISFC.DCP.Object.CommonReport report;

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
        private Neusoft.HISFC.DCP.BizLogic.DiseaseReport diseaseReport = new Neusoft.HISFC.DCP.BizLogic.DiseaseReport();

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
            if (c is Neusoft.FrameWork.WinForms.Controls.NeuRadioButton)
            {
                dr["Value"] = Neusoft.FrameWork.Function.NConvert.ToInt32(((Neusoft.FrameWork.WinForms.Controls.NeuRadioButton)c).Checked).ToString();
            }
            if (c is Neusoft.FrameWork.WinForms.Controls.NeuCheckBox)
            {
                dr["Value"] = Neusoft.FrameWork.Function.NConvert.ToInt32(((Neusoft.FrameWork.WinForms.Controls.NeuCheckBox)c).Checked).ToString();
            }
            if (c is Neusoft.FrameWork.WinForms.Controls.NeuComboBox)
            {
                dr["Value"] = ((Neusoft.FrameWork.WinForms.Controls.NeuComboBox)c).Tag.ToString();
            }
            if (c is Neusoft.FrameWork.WinForms.Controls.NeuTextBox)
            {
                dr["Value"] =((Neusoft.FrameWork.WinForms.Controls.NeuTextBox)c).Text;
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
        public void SetValue(DataRow dr,Control container)
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
                    if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuRadioButton))
                    {
                        ((Neusoft.FrameWork.WinForms.Controls.NeuRadioButton)c).Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(dr["Value"].ToString());
                    }
                    if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuCheckBox))
                    {
                        ((Neusoft.FrameWork.WinForms.Controls.NeuCheckBox)c).Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(dr["Value"].ToString());
                    }
                    if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuComboBox))
                    {
                        ((Neusoft.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedValue = Neusoft.FrameWork.Function.NConvert.ToBoolean(dr["Value"].ToString());
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
                if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuCheckBox))
                {
                    listControl.Add(c);
                }
                else if (c.Controls.Count > 0)
                {
                    if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuComboBox) )
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
        public Neusoft.HISFC.DCP.Object.CommonReport Report
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
        public void SetAdditionInfo(Neusoft.HISFC.DCP.Object.AdditionReport additionReport,Control container)
        {
            StringReader stringReader = new StringReader(additionReport.ReportXML);
            this.dsAdditionReportXML.ReadXml(stringReader);
            foreach (DataTable dt in this.dsAdditionReportXML.Tables)
            {
                this.dtAdditionReportXML = dt;
                foreach (DataRow dr in this.dtAdditionReportXML.Rows)
                {
                    this.SetValue(dr,container);
                }
            }
        }

        /// <summary>
        /// 读取附卡信息
        /// </summary>
        /// <returns></returns>
        public Neusoft.HISFC.DCP.Object.AdditionReport GetAdditionInfo(Control container)
        {
            this.dtAdditionReportXML.Clear();
            Neusoft.HISFC.DCP.Object.AdditionReport additionReport = new Neusoft.HISFC.DCP.Object.AdditionReport();
            foreach (Control c in this.GetAllControls(container))
            {
                if (c.GetType() ==typeof(Neusoft.FrameWork.WinForms.Controls.NeuLabel)||c.GetType()==typeof(Label))
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
        public int IsValid(ref string msg)
        {
            return 1;
        }

        /// <summary>
        /// 修改附卡信息
        /// </summary>
        /// <returns></returns>
        public int UpdateAdditionInfo(Neusoft.HISFC.DCP.Object.AdditionReport addition)
        {
            if (addition != null && addition is Neusoft.HISFC.DCP.Object.AdditionReport)
            {
                Neusoft.HISFC.DCP.Object.AdditionReport additionReport = addition as Neusoft.HISFC.DCP.Object.AdditionReport;
                additionReport.Report = this.Report;
                int state = diseaseReport.UpdateAdditionReport(additionReport);
                if (state <=1)
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
        public int InsertAdditionInfo(Neusoft.HISFC.DCP.Object.AdditionReport addition)
        {
            if (addition != null && addition is Neusoft.HISFC.DCP.Object.AdditionReport)
            {
                Neusoft.HISFC.DCP.Object.AdditionReport additionReport = addition as Neusoft.HISFC.DCP.Object.AdditionReport;
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
        public Neusoft.HISFC.DCP.Object.AdditionReport GetAdditionInfo(string reportNO)
        {
            return diseaseReport.GetAdditionReport(reportNO);
        }

    #endregion

    }
}