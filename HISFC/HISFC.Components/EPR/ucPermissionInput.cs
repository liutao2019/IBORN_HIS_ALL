using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{
    /// <summary>
    /// Ȩ������
    /// </summary>
    public partial class ucPermissionInput : UserControl
    {
        public ucPermissionInput(object permission)
        {
            InitializeComponent();
            //this.curPermission = permission as FS.HISFC.Models.Medical.Permission;
            this.initControl();
        }

        //private FS.HISFC.Models.Medical.Permission curPermission = null;//��ǰȨ��

        private FS.FrameWork.WinForms.Forms.frmEasyChoose frmShowPerson = null;//ѡ����Ա
        private void ucPermissionInput_Load(object sender, EventArgs e)
        {
            try
            {
                
                frmShowPerson = new FS.FrameWork.WinForms.Forms.frmEasyChoose(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.QueryEmployeeAll());
                frmShowPerson.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(frmShowPerson_SelectedItem);
            }
            catch { }
        }

         //��ʾ��Ա�¼�
        void frmShowPerson_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            //curPermission.Person = sender.Clone() as FS.HISFC.Models.Base.Employee;
            //this.neuDateTimePicker2.Value = new DateTime(2010, 01, 01, 0, 0, 0);
            //this.txtLoginName.Text = curPermission.Person.ID;
            //this.txtName.Text = curPermission.Person.Name;
        }

       
        
        private void initControl()
        {
            //this.txtLoginName.Text = this.curPermission.Person.ID;
            //this.txtName.Text = this.curPermission.Person.Name;
            //this.initPermission(this.tabPage1, this.curPermission.OrderPermission);
            //this.initPermission(this.tabPage2, this.curPermission.EMRPermission);
            //this.initPermission(this.tabPage3, this.curPermission.QCPermission);
        }
        /// <summary>
        /// ��ʾȨ��
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="permission"></param>
        //private void initPermission(Control panel, FS.HISFC.Models.Medical.CPermission permission)
        //{
        //    foreach (Control c in panel.Controls)
        //    {
        //        if (c.GetType().IsSubclassOf(typeof(CheckBox) ))
        //        {
        //            ((CheckBox)c).Checked = permission.GetOnePermission(c.Tag);
        //        }
        //        if (c.GetType()==typeof(ComboBox))
        //        {
        //            int temp = permission.GetPermission(c.Tag);
        //            if (temp == -1) return;
        //            ((ComboBox)c).SelectedIndex = temp;
        //        }
        //    }
        //}
       

        /// <summary>
        /// �������Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            frmShowPerson.ShowDialog(this.FindForm());
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, System.EventArgs e)
        {
           // this._getPermission();
           // this.curPermission.DateBeginOrderPermission = this.neuDateTimePicker1.Value;
           // this.curPermission.DateEndOrderPermission = this.neuDateTimePicker2.Value;
           //this.curPermission.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
           // FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
            
           // if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SetPermission(this.curPermission) == -1)
           // {
           //     FS.HISFC.BizProcess.Factory.Function.RollBack();
           //     MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
           //     return;
           // }
           // FS.HISFC.BizProcess.Factory.Function.Commit();
           // MessageBox.Show("����ɹ���");
           // this.FindForm().DialogResult = DialogResult.OK;
           // this.FindForm().Close();
        }
        protected void _getPermission()
        {
            //this._getCPermission(this.tabPage1, this.curPermission.OrderPermission);
            //this._getCPermission(this.tabPage2, this.curPermission.EMRPermission);
            //this._getCPermission(this.tabPage3, this.curPermission.QCPermission);
        }
        //protected void _getCPermission(Control panel, FS.HISFC.Models.Medical.CPermission permission)
        //{
        //    string strPermission = "0000000000000";
        //    foreach (Control c in panel.Controls)
        //    {
        //        if (c.GetType().IsSubclassOf(typeof(CheckBox) ))
        //        {
        //            strPermission = strPermission.Insert(FS.FrameWork.Function.NConvert.ToInt32(c.Tag), FS.FrameWork.Function.NConvert.ToInt32(((CheckBox)c).Checked).ToString());
        //            strPermission = strPermission.Remove(FS.FrameWork.Function.NConvert.ToInt32(c.Tag) + 1, 1);
        //        }
        //        if (c.GetType()==(typeof(ComboBox)))
        //        {
        //            strPermission = strPermission.Insert(FS.FrameWork.Function.NConvert.ToInt32(c.Tag), ((ComboBox)c).SelectedIndex.ToString ());
        //            strPermission = strPermission.Remove(FS.FrameWork.Function.NConvert.ToInt32(c.Tag) + 1, 1);
                
        //        }
        //    }
        //    permission.Permission = strPermission;
        //}

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

   
    }
}
