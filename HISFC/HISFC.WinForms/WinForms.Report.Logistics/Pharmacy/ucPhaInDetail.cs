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
    public partial class ucPhaInDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        #region ����

        FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Pharmacy intePharmacy = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        FS.HISFC.BizLogic.Pharmacy.Item itemPhaManager = new FS.HISFC.BizLogic.Pharmacy.Item();
       
        #endregion

        public ucPhaInDetail()
        {
            InitializeComponent();
        }

        private void ucPhaInDetail_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���������У����Ժ򡭡�");
            Application.DoEvents();
            this.cmbQuality.AddItems(inteManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY));
            this.cmbInDept.AddItems(inteManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P));
            this.cmbOper.AddItems(inteManager.QueryEmployeeByDeptID(empl.Dept.ID));
            this.cmbDrug.AddItems(new ArrayList(itemPhaManager.QueryItemList()));
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #region ����

        protected override int OnRetrieve(params object[] objects)
        {
            string oper = "000";
            string indept = "111";
            string listcode = "222";
            string qualityID = "333";
            string drugID = "444";

            if(this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
                return -1;
            }
            if(ckInDept.Checked)
            {
                if(cmbInDept.SelectedItem == null)
                {
                    MessageBox.Show("��ѡ����ҩ���ң�");
                    return -1;
                }
                indept = cmbInDept.SelectedItem.ID;
            }
            if(ckListCode.Checked)
            {
                if(string.IsNullOrEmpty(txtListCode.Text.Trim()))
                {
                    MessageBox.Show("��������ҩ���ţ�");
                    return -1;
                }
                listcode = txtListCode.Text.Trim();
            }
            if (ckQuality.Checked)
            {
                if (cmbQuality.SelectedItem == null)
                {
                    MessageBox.Show("��ѡ��ҩƷ���ʣ�");
                    return -1;
                }
                qualityID = cmbQuality.SelectedItem.ID;
            }
            if (ckOper.Checked)
            {
                if (cmbOper.SelectedItem == null)
                {
                    MessageBox.Show("��ѡ�񾭰��ˣ�");
                    return -1;
                }
                oper = cmbOper.SelectedItem.ID;
            }
            if(ckDrug.Checked)
            {
                if (cmbDrug.SelectedItem == null)
                {
                    MessageBox.Show("��ѡ��ҩƷ��");
                    return -1;
                }
                drugID = cmbDrug.SelectedItem.ID;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,oper,indept,listcode,qualityID,drugID);
        }
        #endregion

        #region �¼�

        private void ckInDept_CheckedChanged(object sender, EventArgs e)
        {
            if (ckInDept.Checked)
            {
                this.cmbInDept.Enabled = true;
            }
            else
            {
                cmbInDept.Tag = null;
                cmbInDept.Text = "";
                cmbInDept.Enabled = false;
            }
        }

        private void ckListCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ckListCode.Checked)
            {
                txtListCode.Enabled = true;
            }
            else 
            {
                txtListCode.Text = "";
                txtListCode.Enabled = false;
            }
        }

        private void ckQuality_CheckedChanged(object sender, EventArgs e)
        {
            if (ckQuality.Checked)
            {
                cmbQuality.Enabled = true;
            }
            else
            {
                cmbQuality.Tag = null;
                cmbQuality.Text = "";
                cmbQuality.Enabled = false;
            }
        }

        private void ckOper_CheckedChanged(object sender, EventArgs e)
        {
            if (ckOper.Checked)
            {
                cmbOper.Enabled = true;
            }
            else
            {
                cmbOper.Tag = null;
                cmbOper.Text = "";
                cmbOper.Enabled = false;
            }
        }

        private void ckDrug_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDrug.Checked)
            {
                cmbDrug.Enabled = true;
            }
            else
            {
                cmbDrug.Tag = null;
                cmbDrug.Text = "";
                cmbDrug.Enabled = false;
            }
        }
        #endregion 
    }
}
