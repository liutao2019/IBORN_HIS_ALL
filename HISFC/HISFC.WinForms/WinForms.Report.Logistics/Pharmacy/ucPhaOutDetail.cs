using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.Pharmacy
{
    public partial class ucPhaOutDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        #region ����

        FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizLogic.Pharmacy.Item itemPhaManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        #endregion

        public ucPhaOutDetail()
        {
            InitializeComponent();
        }

        private void ucPhaOutDetail_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���������У����Ժ򡭡�");
            Application.DoEvents();

            this.InitDrugStores();
            cmbDrug.AddItems(new ArrayList(itemPhaManager.QueryItemList()));
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #region ����

        private void InitDrugStores()
        {
            ArrayList al = new ArrayList();
            al = inteManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            this.cmbOutDept.AddItems(al);
        }

        protected override int OnRetrieve(params object[] objects)
        {
            string listCode = "000";
            string outDept = "111";
            string drugID = "222";

            if(this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
                return -1;
            }
            if(this.ckListCode.Checked)
            {
                if(string.IsNullOrEmpty(this.txtListCode.Text.Trim()))
                {
                    MessageBox.Show("�������˻����ݺţ�");
                    return -1;
                }
                listCode = this.txtListCode.Text.Trim();
            }
            if(this.ckOutDept.Checked)
            {
                if (this.cmbOutDept.SelectedItem == null)
                {
                    MessageBox.Show("��ѡ����ҩ���ң�");
                    return -1;
                }
                outDept = this.cmbOutDept.SelectedItem.ID;
            }
            if(this.ckDrugID.Checked)
            {

                if (this.cmbDrug.SelectedItem == null)
                {
                    MessageBox.Show("��ѡ��ҩƷ��");
                    return -1;
                }
                drugID = this.cmbDrug.SelectedItem.ID;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,listCode,outDept,drugID);
        }
        #endregion 

        #region �¼�

        private void ckListCode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckListCode.Checked)
            {
                this.txtListCode.Enabled = true;
            }
            else
            {
                this.txtListCode.Text = "";
                this.txtListCode.Enabled = false;
            }
        }

        private void ckOutDept_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckOutDept.Checked)
            {
                this.cmbOutDept.Enabled = true;
            }
            else
            {
                this.cmbOutDept.Text = "";
                this.cmbOutDept.Tag = null;
                this.cmbOutDept.Enabled = false;
            }
        }

        private void ckDrugID_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckDrugID.Checked)
            {
                this.cmbDrug.Enabled = true;
            }
            else
            {
                this.cmbDrug.Text = "";
                this.cmbDrug.Tag = null;
                this.cmbDrug.Enabled = false;
            }
        }

        #endregion 
    }
}
