using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Logistics.DrugStore
{
    public partial class ucStoSendDrug : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoSendDrug()
        {
            InitializeComponent();
        }

        DeptZone deptZone1 = DeptZone.ALL;

        #region ö��DeptZone
        public enum DeptZone
        {
            //����
            MZ = 0,
            //סԺ
            ZY = 1,
            //ȫԺ
            ALL = 2,
        }
        #endregion

        #region ����
        [Category("��������"), Description("��ѯ��Χ��MZ:���ZY:סԺ��ALL:ȫԺ")]
        public DeptZone DeptZone1
        {
            get
            {
                return deptZone1;
            }
            set
            {
                deptZone1 = value;
            }
        }
        #endregion

        #region ��ʼ��

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);

            cmbFeelan.ClearItems();
            if (deptZone1 == DeptZone.ALL)
            {
                cmbFeelan.Items.Add("ȫԺ");
                cmbFeelan.Items.Add("����");
                cmbFeelan.Items.Add("סԺ");
                cmbFeelan.SelectedIndex = 0;
            }
            if (deptZone1 == DeptZone.MZ)
            {
                cmbFeelan.Items.Add("����");
                cmbFeelan.SelectedIndex = 0;
                cmbFeelan.Visible = false;
            }
            if (deptZone1 == DeptZone.ZY)
            {
                cmbFeelan.Items.Add("סԺ");
                cmbFeelan.SelectedIndex = 0;
                cmbFeelan.Visible = false;
            }
        }

      

  
        #endregion

        #region ��ѯ

        protected override int OnRetrieve(params object[] objects)
        {
            if (GetQueryTime() == -1)
            {
                return -1;
            }
            FS.HISFC.Models.Base.Employee employee = null;
            employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
            string  strFeelan = "ȫԺ";
            if (!string.IsNullOrEmpty(strFeelan = cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString()))
            {
                strFeelan = cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString();
            }
            
            return base.OnRetrieve(this.beginTime,this.endTime,employee.Dept.ID.ToString(),strFeelan);
        }

        #endregion
    }
}