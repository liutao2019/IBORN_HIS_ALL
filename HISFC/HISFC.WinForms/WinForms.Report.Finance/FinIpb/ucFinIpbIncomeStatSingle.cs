using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbIncomeStatSingle : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbIncomeStatSingle()
        {
            InitializeComponent();
        }

        private string department="סԺ";
        private string deptCode=string.Empty;
        private string deptName = string.Empty;
        private string reportCode = string.Empty;
        private string reportName = string.Empty;
        private string title_report = string.Empty;
        private string title_program = string.Empty;

        private string deptType = "��������";

        [Description("���ÿ������ͣ���Ϊ�������ң�ִ�п��Һͻ������ڿ���"),Category("����"),DefaultValue("��������")]
        public string DeptType
        {
            get
            {
                return this.deptType;
            }
            set
            {
                this.deptType = value;
            }
        }

        [Description("���ò������ͣ���ΪסԺ�������ȫԺ"), Category("����"), DefaultValue("סԺ")]
        public string Department
        {
            get
            {
                return this.department;
            }
            set
            {
                this.department = value;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // ���������б�
            System.Collections.ArrayList list_department = new System.Collections.ArrayList();

            if (this.Department == "סԺ")
            {
                list_department.Add("סԺ");
            }
            else if (this.Department == "����")
            {
                list_department.Add("����");
            }
            else if (this.Department == "ȫԺ")
            {
                list_department.Add("ȫԺ");
                list_department.Add("����");
                list_department.Add("סԺ");
            }

            foreach (string str in list_department)
            {
                ncboDepartment.Items.Add(str);
            }

            ncboDepartment.alItems.AddRange(list_department);

            if (ncboDepartment.Items.Count > 0)
            {
                ncboDepartment.SelectedIndex = 0;
                department = ncboDepartment.Items[this.ncboDepartment.SelectedIndex].ToString();
            }

            // ͳ�ƴ��������б�
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList list_bigtype = manager.GetConstantList("FEECODESTAT");

            FS.HISFC.Models.Base.Const top_bigtype = new FS.HISFC.Models.Base.Const();
            top_bigtype.ID = "ALL";
            top_bigtype.Name = "ȫ��";

            // �����б���ص�һ��ѡ�ȫ����
            this.ncboReportcode.Items.Add(top_bigtype);

            foreach (FS.HISFC.Models.Base.Const con in list_bigtype)
            {
                ncboReportcode.Items.Add(con);
            }

            // �����б����ѡ�����ص�һ��ѡ�ȫ�����Լ������б�
            this.ncboReportcode.alItems.Add(top_bigtype);
            this.ncboReportcode.alItems.AddRange(list_bigtype);

            if (ncboReportcode.Items.Count > 0)
            {
                ncboReportcode.SelectedIndex = 0;
                reportCode = ((FS.HISFC.Models.Base.Const)ncboReportcode.Items[0]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)ncboReportcode.Items[0]).Name;
            }

            // ���������б�
            //FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList list_dept = new System.Collections.ArrayList();

            FS.HISFC.Models.Base.Department top_dept = new FS.HISFC.Models.Base.Department();
            top_dept.ID = "ALL";
            top_dept.Name = "ȫ��";

            this.ncboDept.Items.Add(top_dept);

            if (ncboDepartment.Items[ncboDepartment.SelectedIndex].ToString() == "סԺ")
            {
                list_dept = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            }
            else if (ncboDepartment.Items[ncboDepartment.SelectedIndex].ToString() == "����")
            {
                list_dept = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
            }
            else
            {
                System.Collections.ArrayList list_inhos = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                System.Collections.ArrayList list_clinic = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
                
                foreach (FS.HISFC.Models.Base.Department var_inhos in list_inhos)
                {
                    list_dept.Add(var_inhos);
                }

                foreach (FS.HISFC.Models.Base.Department var_clinic in list_clinic)
                {
                    list_dept.Add(var_clinic);
                }
            }

            foreach (FS.HISFC.Models.Base.Department var_dept in list_dept)
            {
                this.ncboDept.Items.Add(var_dept);
            }

            this.ncboDept.alItems.Add(top_dept);
            this.ncboDept.alItems.AddRange(list_dept);

            if (ncboDept.Items.Count > 0)
            {
                ncboDept.SelectedIndex = 0;
                deptCode = ((FS.HISFC.Models.Base.Department)ncboDept.Items[0]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)ncboDept.Items[0]).Name;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            string dept_receipe;
            string dept_inhos;
            string dept_exec;

            if (this.DeptType == "ִ�п���")
            {
                dept_exec = this.ncboDept.SelectedItem.ID;
                dept_inhos = "ALL";
                dept_receipe = "ALL";
            }
            else if (this.DeptType == "��������")
            {
                dept_receipe = this.ncboDept.SelectedItem.ID;
                dept_inhos = "ALL";
                dept_exec = "ALL";
            }
            else
            {
                dept_inhos = this.ncboDept.SelectedItem.ID;
                dept_exec = "ALL";
                dept_receipe = "ALL";
            }

            //this.dwMain.Modify("title.text=" + "aaaa");
            //this.dwMain.Modify("title.text='" + this.ncboDepartment.Items[ncboDepartment.SelectedIndex].ToString() + "-" + this.ncboReportcode.Items[ncboReportcode.SelectedIndex].ToString() + "��Ŀ����ͳ��'");
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,this.ncboDepartment.Items[ncboDepartment.SelectedIndex].ToString(),dept_receipe,dept_exec,dept_inhos,this.reportCode,this.DeptType);
        }

        /// <summary>
        /// ͳ�ƴ��������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncboReportcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ncboReportcode.SelectedIndex >= 0)
            {
                reportCode = ((FS.HISFC.Models.Base.Const)ncboReportcode.Items[this.ncboReportcode.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)ncboReportcode.Items[this.ncboReportcode.SelectedIndex]).Name;
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ncboDepartment.SelectedIndex == 0)
            {
                department = "ALL";
            }
            else if(ncboDepartment.SelectedIndex > 0)
            {
                department = ncboDepartment.Items[this.ncboDepartment.SelectedIndex].ToString();
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ncboDept.SelectedIndex >= 0)
            {
                deptCode = ((FS.HISFC.Models.Base.Department)ncboDept.Items[this.ncboDept.SelectedIndex]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)ncboDept.Items[this.ncboDept.SelectedIndex]).Name;
            }
        }


        }
    
}
