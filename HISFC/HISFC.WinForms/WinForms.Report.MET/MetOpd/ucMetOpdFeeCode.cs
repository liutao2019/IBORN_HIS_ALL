using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.MET.MetOpd
{
    public partial class ucMetOpdFeeCode : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetOpdFeeCode()
        {
            InitializeComponent();
        }
        DeptZone deptZone1 = DeptZone.ALL;


        public enum DeptZone
        {
            MZ = 0,
            ZY = 1,
            ALL = 2,
        }

        [Category("��������"), Description("��ѯ��Χ��ALL��ȫԺ��MZ�����ZY��סԺ")]
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

        protected override void OnLoad(EventArgs e)
        {
            this.isAcross = true;
            this.isSort = false;
            base.OnLoad(e);

            cmbDept.ClearItems();

            if (deptZone1 == DeptZone.ALL)
            {
                cmbDept.Items.Add("ȫԺ");
                cmbDept.Items.Add("����");
                cmbDept.Items.Add("סԺ");

            }
            if (deptZone1 == DeptZone.MZ)
            {
                cmbDept.Items.Add("����");
                cmbDept.Enabled = false;
            }
            if (deptZone1 == DeptZone.ZY)
            {
                cmbDept.Items.Add("סԺ");
                cmbDept.Enabled = false;
            }

            cmbDept.SelectedIndex = 0;
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }


            string strFeelan = "ȫԺ";
            //List<string> alType = new List<string>();


            if (!string.IsNullOrEmpty(cmbDept.Items[cmbDept.SelectedIndex].ToString()))
            {
                strFeelan = cmbDept.Items[cmbDept.SelectedIndex].ToString();
            }


            return base.OnRetrieve(this.beginTime, this.endTime, strFeelan);
        }

    }
}
