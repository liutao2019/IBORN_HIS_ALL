using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinDeptFeeDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinDeptFeeDetail()
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


        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            base.OnLoad();

            // ���������б�
            System.Collections.ArrayList list_department = new System.Collections.ArrayList();


            this.MainDWLabrary = "Report\\finipb.pbd;Report\\finipb.pbd";
            dwMain.LibraryList = "Report\\finipb.pbd;Report\\finipb.pbd";

            if (deptZone1 == DeptZone.ZY)
            {
                this.ncboDepartment.Items.Add("סԺ");
                this.mainDWDataObject = "d_fin_deptfeeincome";
                dwMain.DataWindowObject = "d_fin_deptfeeincome";
                this.neuComboBox1.Enabled = true;
            }
            else if (deptZone1 == DeptZone.MZ)
            {
                this.ncboDepartment.Items.Add("����");
                this.mainDWDataObject = "d_fin_deptfeeincome2";
                dwMain.DataWindowObject = "d_fin_deptfeeincome2";
                this.neuComboBox1.Enabled = false;
            }
            else if (deptZone1 == DeptZone.ALL)
            {
                this.ncboDepartment.Items.Add("ȫԺ");
                this.ncboDepartment.Items.Add("סԺ");
                this.ncboDepartment.Items.Add("����");
                this.mainDWDataObject = "d_fin_deptfeeincome3";
                dwMain.DataWindowObject = "d_fin_deptfeeincome3";
                this.neuComboBox1.Enabled = true;
            }


            // ͳ�ƴ��������б�
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList list_bigtype = manager.GetConstantList("FEECODESTAT");

            foreach (FS.HISFC.Models.Base.Const con in list_bigtype)
            {
                this.cmbReportType.Items.Add(con);
            }

            this.cmbReportType.alItems.AddRange(list_bigtype);
            this.cmbReportType.SelectedIndex = 0;

            //������������б�
            this.neuComboBox1.SelectedIndex = 1;

            //ͳ�Ʒ�ʽ�����б�
            this.cmbType.SelectedIndex = 0;

            this.ncboDepartment.SelectedIndex = 0;

        }


        protected override int OnRetrieve(params object[] objects)
        {

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            string selectType = this.cmbType.Text;
            string reportType = this.cmbReportType.SelectedItem.ID;
            string inState = this.neuComboBox1.Text;

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,reportType,selectType, inState);
        }

        private void ncboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(this.ncboDepartment.Text=="ȫԺ")
            {
                this.mainDWDataObject = "d_fin_deptfeeincome3";
                dwMain.DataWindowObject = "d_fin_deptfeeincome3";
                this.neuComboBox1.Enabled = true;
            }
            else if(this.ncboDepartment.Text=="סԺ")
            {
                this.mainDWDataObject = "d_fin_deptfeeincome";
                dwMain.DataWindowObject = "d_fin_deptfeeincome";
                this.neuComboBox1.Enabled = true;
            }
            else if (this.ncboDepartment.Text == "����")
            {
                this.mainDWDataObject = "d_fin_deptfeeincome2";
                dwMain.DataWindowObject = "d_fin_deptfeeincome2";
                this.neuComboBox1.Enabled = false;
            }
        }

    }
}