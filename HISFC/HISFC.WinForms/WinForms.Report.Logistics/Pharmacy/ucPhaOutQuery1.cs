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
    public partial class ucPhaOutQuery1 : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        #region ����
        /// <summary>
        /// �ۺ�ҵ���ʵ��
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();

        private string oper = FS.FrameWork.Management.Connection.Operator.Name;
        
        private string deptID;
        private string deptName;

        #endregion 

        public ucPhaOutQuery1()
        {
            InitializeComponent();
        }

        private void ucPhaOutQuery1_Load(object sender, EventArgs e)
        {
            this.isAcross = true;
            this.isSort = false;

            this.InitData();
        }

        #region ����

        private void InitData()
        {
            ArrayList al = this.integrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI);
            this.cmbPharmacy.AddItems(al);
        }

        #endregion 

        #region �¼�
        protected override int OnRetrieve(params object[] objects)
        {
            if(this.cmbPharmacy.Tag == null || string.IsNullOrEmpty(this.cmbPharmacy.Text.Trim()))
            {
                MessageBox.Show("��ѡ����Ҫͳ�Ƶ�ҩ�⣡");
                return -1;
            }
            if(this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڲ�ѯ����ʱ�䣡");
                return -1;
            }
            return base.OnRetrieve(this.deptID,this.oper,this.dtpBeginTime.Value,this.dtpEndTime.Value,this.deptName);
        }

        private void cmbPharmacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            deptID = this.cmbPharmacy.SelectedItem.ID;
            deptName = this.cmbPharmacy.SelectedItem.Name;
        }
        #endregion 
    }
}
