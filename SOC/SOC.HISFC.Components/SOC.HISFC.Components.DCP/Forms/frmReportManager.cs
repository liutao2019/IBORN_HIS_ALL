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
    public partial class frmReportManager : FS.FrameWork.WinForms.Forms.BaseForm
    {
        #region ���캯��
        public frmReportManager()
        {
            InitializeComponent();
            this.FormClosing -= new FormClosingEventHandler(frmReportManager_FormClosing);
            this.FormClosing += new FormClosingEventHandler(frmReportManager_FormClosing);
        }
        #endregion

        #region �����ؼ�
        /// <summary>
        /// ��Ⱦ��UC
        /// </summary>
        //private FS.SOC.HISFC.Components.DCP.DiseaseReport.ucDiseaseReportRegister ucDiseaseReportRegister = new FS.SOC.HISFC.Components.DCP.DiseaseReport.ucDiseaseReportRegister();
        private FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseReport ucDiseaseReportRegister = null;
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
                ucDiseaseReportRegister.ReportOperResult = value;
            }
        }

        #endregion        

        #region �¼�

        public void Init(FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�...");
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
            if (e.ClickedItem.Text == "�˳�")
            {
                if (this.ReportOperResult == FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK)
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "���ϱ���Ⱦ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(this, "���ϱ���Ⱦ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                e.Cancel = true;
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
            this.toolStrip1.Items.Add("��ӡ", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ));

            this.toolStrip1.Items.Add(new ToolStripSeparator());

            this.toolStrip1.Items.Add("�˳�", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�));
            //add by chegnym 2012-11-1 ��Ⱦ��֪ʶ����
            this.toolStrip1.Items.Add("��Ⱦ��֪ʶ", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����));

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

