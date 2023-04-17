using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP
{
    /// <summary>
    /// frmReportManager<br></br>
    /// [功能描述: 报卡frm]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-8-27]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的='代码整理'
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class frmReportManager : FS.FrameWork.WinForms.Forms.BaseForm
    {
        #region 构造函数
        public frmReportManager()
        {
            InitializeComponent();
            this.FormClosing -= new FormClosingEventHandler(frmReportManager_FormClosing);
            this.FormClosing += new FormClosingEventHandler(frmReportManager_FormClosing);
        }
        #endregion

        #region 报卡控件
        /// <summary>
        /// 传染病UC
        /// </summary>
        //private FS.SOC.HISFC.Components.DCP.DiseaseReport.ucDiseaseReportRegister ucDiseaseReportRegister = new FS.SOC.HISFC.Components.DCP.DiseaseReport.ucDiseaseReportRegister();
        private FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseReport ucDiseaseReportRegister = null;
        #endregion

        #region 属性

        /// <summary>
        /// 报卡操作结果
        /// </summary>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult ReportOperResult
        {
            get
            {
                return ucDiseaseReportRegister.ReportOperResult;
            }
            set 
            {
                ucDiseaseReportRegister.ReportOperResult = value;
            }
        }

        #endregion        

        #region 事件

        public void Init(FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据，请稍后...");
            Application.DoEvents();
            if (ucDiseaseReportRegister == null)
            {
                ucDiseaseReportRegister=new FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseReport(true,patientType);
                //ucDiseaseReportRegister.Init();
            }

            AddControl();
            AddToolStripItem();
            this.toolStrip1.ItemClicked -= new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
            this.WindowState = FormWindowState.Maximized;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void toolStrip1_ItemClicked(object sender,ToolStripItemClickedEventArgs e)
        {
            this.ClickControlToolStripItem(e.ClickedItem.Text);
            if (e.ClickedItem.Text == "退出")
            {
                if (this.ReportOperResult == FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK)
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "请上报传染病报告", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void frmReportManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.ReportOperResult == FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK)
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "请上报传染病报告", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                e.Cancel = true;
            }
        }       
       
        #endregion

        #region 函数

        /// <summary>
        /// 添加按钮
        /// </summary>
        private void AddToolStripItem()
        {
            this.toolStrip1.Items.Clear();

            this.toolStrip1.Items.Add("新建", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X新建));
            this.toolStrip1.Items.Add("续填", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X新建));
            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add("查询", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查询));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );          

            this.toolStrip1.Items.Add("保存", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B保存));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add("订正", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X信息));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add( "作废", FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.Q取消 ) );

            //删除报告卡后会造成Report_NO获取错误 此处只允许作废
            //this.toolStrip1.Items.Add("删除",FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.A删除));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );
            this.toolStrip1.Items.Add("打印", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印));

            this.toolStrip1.Items.Add(new ToolStripSeparator());

            this.toolStrip1.Items.Add("退出", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出));
            //add by chegnym 2012-11-1 传染病知识帮助
            this.toolStrip1.Items.Add("传染病知识", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B帮助));

            foreach (ToolStripItem t in this.toolStrip1.Items)
            {
                t.TextImageRelation = TextImageRelation.ImageAboveText;
            }
        }

        /// <summary>
        /// 添加报卡控件
        /// </summary>
        private void AddControl()
        {
            this.panelFill.Controls.Add(this.ucDiseaseReportRegister);
            ucDiseaseReportRegister.Dock = DockStyle.Fill;           
        }

        /// <summary>
        /// 调用控件按钮事件处理流程
        /// </summary>
        /// <param name="clickedItemText">按钮Text</param>
        private void ClickControlToolStripItem(string clickedItemText)
        {
            this.ucDiseaseReportRegister.ToolStrip_ItemClicked(clickedItemText);
        }

        /// <summary>
        /// 设置控件属性
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="patientType">患者类型</param>
        /// <param name="diseaseCode">疾病编码</param>
        public void SetControlProperty(FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType, string diseaseCode)
        {
            this.ucDiseaseReportRegister.Patient = patient;
            this.ucDiseaseReportRegister.PatientType = patientType;
            this.ucDiseaseReportRegister.InfectCode = diseaseCode;
        }

        public void QueryDoctReportInfo()
        {
            this.ucDiseaseReportRegister.QueryByDoctorNO(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
        }

        public void QueryDeptReportInfo()
        {
            this.ucDiseaseReportRegister.QueryByDeptNO(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
        }
        #endregion

    }
}

