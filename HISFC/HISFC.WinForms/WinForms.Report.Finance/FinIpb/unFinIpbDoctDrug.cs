using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinReg
{
    public partial class unFinIpbDoctDrug : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public unFinIpbDoctDrug()
        {
            InitializeComponent();
        }

        private DeptZone deptZone1 = DeptZone.ALL;

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

        protected override int OnRetrieve(params object[] objects)
        {

            if (this.dtpBeginTime.Value>this.dtpEndTime.Value)
            {
                MessageBox.Show("����ʱ�䲻��С�ڿ�ʼʱ��");
            }


            if (this.cmbDeptZone.Items[cmbDeptZone.SelectedIndex].ToString() == "��ҽ�����ڿ���ͳ��")
            {
                this.MainDWDataObject = "d_fin_ipb_doctdrugfee_recipedept";
                dwMain.DataWindowObject = "d_fin_ipb_doctdrugfee_recipedept";
            }
            else if (this.cmbDeptZone.Items[cmbDeptZone.SelectedIndex].ToString() == "���������ڿ���ͳ��")
            {
                this.MainDWDataObject = "d_fin_ipb_doctdrugfee_regdept";
                dwMain.DataWindowObject = "d_fin_ipb_doctdrugfee_regdept";
            }
         

            this.MainDWLabrary = "Report\\finipb.pbd;Report\\finipb.pbd";
            dwMain.LibraryList = "Report\\finipb.pbd;Report\\finipb.pbd";


            string strFeelan = "ȫԺ";

            if (!string.IsNullOrEmpty(cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString()))
            {
                strFeelan = cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString();
            }



            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, strFeelan);
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            cmbFeelan.Items.Clear();
            if (this.deptZone1 == DeptZone.ALL)
            {
                this.cmbFeelan.Items.Add("����");
                this.cmbFeelan.Items.Add("סԺ");
                this.cmbFeelan.Items.Add("ȫԺ");

                this.cmbFeelan.SelectedIndex = 0;
            }
            if (this.deptZone1 == DeptZone.MZ)
            {
                this.cmbFeelan.Items.Add("����");

                this.cmbFeelan.SelectedIndex = 0;

            }
            if (this.deptZone1 == DeptZone.ZY)
            {
                this.cmbFeelan.Items.Add("סԺ");

                this.cmbFeelan.SelectedIndex = 0;
            }

            this.cmbDeptZone.Items.Clear();
            this.cmbDeptZone.Items.Add("���������ڿ���ͳ��");
            this.cmbDeptZone.Items.Add("��ҽ�����ڿ���ͳ��");
            
            this.cmbDeptZone.SelectedIndex = 0;

            this.isAcross = true;
            this.isSort = false;
        }

        /// <summary>
        /// ö��
        /// </summary>
        public enum DeptZone
        {
            //����
            MZ = 0,
            //סԺ
            ZY = 1,
            //ȫԺ
            ALL = 2,
        }
    }
}
