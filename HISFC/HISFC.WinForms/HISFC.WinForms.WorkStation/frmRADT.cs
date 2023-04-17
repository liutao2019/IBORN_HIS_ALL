using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.SOC.Local.RADT.GuangZhou.ZDLY.Register;

namespace FS.HISFC.WinForms.WorkStation
{
    /// <summary>
    /// �������ת������
    /// </summary>
    public partial class frmRADT : FS.FrameWork.WinForms.Forms.frmBaseForm
    {
        public frmRADT()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�������,���Ժ�...");
            Application.DoEvents();
            InitializeComponent();
            this.tvNursePatientList1.Refresh();
            this.isOneControl = true;//ֻ��һ���ؼ�
            this.SetTree(this.tvNursePatientList1);
            this.tbPrintBracelet.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C�ش�);
            this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            this.tbPackage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�ײ�);
            AddControl(this.ucRADT1, this.panelMain);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        protected override void OnLoad(EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            string tmp = this.Tag.ToString();
            //����tag=null,��ʾά����λ���棬������ʾά����λ����
            if (tmp == null || tmp.Trim() == "")
            {
                this.tbBed.Visible = true;
            }
            else
            {
                this.tbBed.Visible = false;
            }
            tbRefresh.Text = "ˢ��";

            this.tbPrintBracelet.Visible = false;

            base.OnLoad(e);
           
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tbBed)
            {
                this.ucRADT1.AddTabpage(new FS.HISFC.Components.RADT.Controls.ucBedManager(), "��λά��", null);
            }
            else if (e.ClickedItem == this.tbFee)
            {
                this.ucRADT1.AddTabpage(new FS.HISFC.Components.RADT.Controls.ucAlert(), "Ƿ�ѱ���", null);
            }
            else if (e.ClickedItem == this.tbPrintBracelet)
            {
                PatientInfo patientInfo = this.tvNursePatientList1.SelectedNode.Tag as PatientInfo;
                if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
                {
                    MessageBox.Show("������ϢΪ�գ���ȷ���Ƿ�ѡ�����������ߣ�", "", MessageBoxButtons.OK);
                    return;
                }
                if (!patientInfo.IsBaby)
                {
                    MessageBox.Show("�����������ߣ�����סԺ������", "", MessageBoxButtons.OK);
                    return;
                }
                //ucBabyBracelet babyBracelet = new ucBabyBracelet();
                //babyBracelet.myPatientInfo = patientInfo;
                //babyBracelet.Print();
            }
            else if (e.ClickedItem == this.tbExit)
            {
                this.Close();
            }
            else if (e.ClickedItem == this.tbRefresh)
            {
                this.tvNursePatientList1.Refresh();
                this.ucRADT1.ic_RefreshTree(null, null);//{997A8EEC-A27E-492f-941A-CDEAA3CC4AE7}
            }
            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            else if (e.ClickedItem == this.tbPackage)
            {
                if (this.tvNursePatientList1.SelectedNode.Tag is FS.HISFC.Models.RADT.PatientInfo)
                {
                    FS.HISFC.Models.RADT.PatientInfo tmpPatient = this.tvNursePatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                    if (tmpPatient == null || string.IsNullOrEmpty(tmpPatient.PID.CardNO))
                    {
                        MessageBox.Show("���ȼ������ߣ�");
                        return;
                    }
                    FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                    FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                    frmpackage.DetailVisible = true;//{63d5c888-89bc-44a0-87eb-f560ce0c8ac5}
                    frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(tmpPatient.PID.CardNO);
                    frmpackage.ShowDialog();
                }
            }
        }
        protected override void iControlable_RefreshTree(object sender, EventArgs e)
        {
            this.tvNursePatientList1.Refresh();
        }
    }
}