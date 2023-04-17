using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Forms
{
    public partial class frmAddGroup : Form
    {
        public frmAddGroup()
        {
            InitializeComponent();
        }

        protected FS.HISFC.Models.Admin.SysGroup sysgroup = null;
 
        /// <summary>
        /// OK�¼�
        /// </summary>
        public event FS.FrameWork.WinForms.Forms.OKHandler OkEvent;

        /// <summary>
        /// ϵͳ��
        /// </summary>
        public FS.HISFC.Models.Admin.SysGroup SysGroup
        {
            set
            {
                
                this.txtParentGroupCode.Text = value.ID;
                this.txtParentGroupName.Text = value.Name;
                this.sysgroup = new FS.HISFC.Models.Admin.SysGroup();
                this.sysgroup.ParentGroup.ID = value.ID;
                this.sysgroup.ParentGroup.Name = value.Name;
                try
                {
                    int i = this.sysgroup.SortID + 1;
                    this.txtSortID.Text = i.ToString();
                }
                catch { }
            }
        }
        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidateValue()
        {
            if (this.txtGroupCode.Text.Trim() == "")
            {
                MessageBox.Show("�����벻��Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (this.txtGroupName.Text.Trim() == "")
            {
                MessageBox.Show("������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            this.sysgroup.ID = this.txtGroupCode.Text;
            this.sysgroup.Name = this.txtGroupName.Text;
            this.sysgroup.ParentGroup.ID = this.txtParentGroupCode.Text;
            this.sysgroup.ParentGroup.Name = this.txtParentGroupName.Text;
            this.sysgroup.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.txtSortID.Text);
            //
            //�ж������Ƿ��ظ���
            //
            if (manager.Get(this.sysgroup) != null)
            {
                MessageBox.Show("�Ѿ�������ͬ��ID�飬��ѡ������ID!");
                return false;
            }
            return true;
        }

        FS.HISFC.BizLogic.Manager.SysGroup manager = new FS.HISFC.BizLogic.Manager.SysGroup();
        private bool Save()
        {
            if (ValidateValue())
            {
                if (manager.Insert(this.sysgroup) != -1)
                {
                    return true;
                }
                else
                {
                    if (manager.DBErrCode == 1 )//.Err.LastIndexOf("ORA-00001")
                    {
                        MessageBox.Show("�˱���Ѿ�����");
                    }
                    else
                    {
                        MessageBox.Show("���ݱ���ʧ�ܣ�" + manager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return false;
                }
            }

            return false;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Save())
                {
                    this.FindForm().DialogResult = DialogResult.OK;
                    
                    if(OkEvent !=null)    OkEvent(sender,this.sysgroup);
                    
                    this.FindForm().Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.FindForm().DialogResult = DialogResult.Cancel;
                this.FindForm().Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}