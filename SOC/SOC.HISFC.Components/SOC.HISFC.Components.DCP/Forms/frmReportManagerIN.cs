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
    /// [��������: ����frm]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-8-27]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='' 
    ///		�޸�Ŀ��='��������'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmReportManagerIN : FS.FrameWork.WinForms.Forms.BaseForm
    {
        #region ���캯��
        public frmReportManagerIN()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmReportManager_Load);
        }       
        #endregion

        #region �����ؼ�
        /// <summary>
        /// ��Ⱦ��UC
        /// </summary>
        //private FS.SOC.HISFC.Components.DCP.DiseaseReport.ucDiseaseReportRegister ucDiseaseReportRegister = new FS.SOC.HISFC.Components.DCP.DiseaseReport.ucDiseaseReportRegister();
        private FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseReport ucDiseaseReportRegister = new FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseReport();
        #endregion

        #region ����
        /// <summary>
        /// �����������
        /// </summary>
        FS.SOC.HISFC.DCP.Enum.ReportOperResult reportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.Other;
       
        #endregion

        #region ����

        /// <summary>
        /// �����������
        /// </summary>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult ReportOperResult
        {
            get
            {
                return ucDiseaseReportRegister.ReportOperResult;
            }
            set 
            {
                reportOperResult = value;
            }
        }

        #endregion        

        #region �¼�

        private void frmReportManager_Load(object sender, EventArgs e)
        {
            this.ucDiseaseReportRegister.PatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            AddControl();
            AddToolStripItem();
            this.toolStrip1.ItemClicked -= new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
            //xiwx
            this.WindowState = FormWindowState.Maximized;            
        }

        private void toolStrip1_ItemClicked(object sender,ToolStripItemClickedEventArgs e)
        {
            this.ClickControlToolStripItem(e.ClickedItem.Text);
            if (e.ClickedItem.Text == tbExit.Text)
            {
                this.ucDiseaseReportRegister.ReportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.Cancel;
                this.Close();
            }
        }
       
        #endregion

        #region ����

        /// <summary>
        /// ��Ӱ�ť
        /// </summary>
        private void AddToolStripItem()
        {
            this.toolStrip1.Items.Clear();

            this.toolStrip1.Items.Add("�½�", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X�½�));
            this.toolStrip1.Items.Add("����", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X�½�));
            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add("��ѯ", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );          

            this.toolStrip1.Items.Add("����", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add("����", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add( "����", FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ�� ) );

            //ɾ�����濨������Report_NO��ȡ���� �˴�ֻ��������
            //this.toolStrip1.Items.Add("ɾ��",FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Aɾ��));

            this.toolStrip1.Items.Add( new ToolStripSeparator() );

            this.toolStrip1.Items.Add("�˳�", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�));

            foreach (ToolStripItem t in this.toolStrip1.Items)
            {
                t.TextImageRelation = TextImageRelation.ImageAboveText;
            }
        }

        /// <summary>
        /// ��ӱ����ؼ�
        /// </summary>
        private void AddControl()
        {
            this.panelFill.Controls.Add(this.ucDiseaseReportRegister);
            ucDiseaseReportRegister.Dock = DockStyle.Fill;           
        }

        /// <summary>
        /// ���ÿؼ���ť�¼���������
        /// </summary>
        /// <param name="clickedItemText">��ťText</param>
        private void ClickControlToolStripItem(string clickedItemText)
        {
            this.ucDiseaseReportRegister.ToolStrip_ItemClicked(clickedItemText);
        }

        /// <summary>
        /// ���ÿؼ�����
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="patientType">��������</param>
        /// <param name="diseaseCode">��������</param>
        public void SetControlProperty(FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType, string diseaseCode)
        {
            this.ucDiseaseReportRegister.Patient = patient;
            this.ucDiseaseReportRegister.PatientType = patientType;
            this.ucDiseaseReportRegister.InfectCode = diseaseCode;
        }
        
        #endregion

    }
}

