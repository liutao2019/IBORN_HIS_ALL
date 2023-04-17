using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbIncomeStatPatient : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbIncomeStatPatient()
        {
            InitializeComponent();
            this.isAcross = true;
            this.isSort = false;
        }
        
        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        private string pactCode = string.Empty;
        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        private string pactName = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        private string department = string.Empty;
        private Department department1 = Department.ȫԺ;
        /// <summary>
        /// ���Ҳ���
        /// </summary>
        private string dept = string.Empty;
        /// <summary>
        /// ͳ�ƴ������
        /// </summary>
        private string reportCode = string.Empty;
        /// <summary>
        /// ͳ�ƴ�������
        /// </summary>
        private string reportName = string.Empty;


        [Description("���ò������ͣ���ΪסԺ�������ȫԺ"), Category("����"), DefaultValue("סԺ")]
        public Department Department1
        {
            get
            {
                return this.department1;
            }
            set
            {
                this.department1 = value;
            }
        }

        public enum Department
        {
            ȫԺ = 0,
            ���� = 1,
            סԺ = 2,
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // ���������б�
            System.Collections.ArrayList list_department = new System.Collections.ArrayList();

            if (department1 == Department.סԺ)
            {
                list_department.Add("סԺ");
            }
            else if (department1 == Department.����)
            {
                list_department.Add("����");
            }
            else if (department1 == Department.ȫԺ)
            {
                list_department.Add("ȫԺ");
                list_department.Add("סԺ");
                list_department.Add("����");
            }

            for (int i = 0; i < list_department.Count;i++ )
            {
                this.ncboDepartment.Items.Add(list_department[i]);
            }

            ncboDepartment.alItems.AddRange(list_department);

            if (ncboDepartment.Items.Count > 0)
            {
                ncboDepartment.SelectedIndex = 0;
                department = "ALL";
            }

            // ͳ�ƴ��������б�
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList list_bigtype = manager.GetConstantList("FEECODESTAT");

            //FS.HISFC.Models.Base.Const top_bigtype = new FS.HISFC.Models.Base.Const();
            //top_bigtype.ID = "ALL";
            //top_bigtype.Name = "ȫ��";

            //this.ncboReportCode.Items.Add(top_bigtype);

            foreach (FS.HISFC.Models.Base.Const var_bigtype in list_bigtype)
            {
                this.ncboReportCode.Items.Add(var_bigtype);
            }

            //this.ncboReportCode.alItems.Add(top_bigtype);
            this.ncboReportCode.alItems.AddRange(list_bigtype);

            if (ncboReportCode.Items.Count > 0)
            {
                ncboReportCode.SelectedIndex = 0;
                reportCode = ((FS.HISFC.Models.Base.Const)ncboReportCode.alItems[this.ncboReportCode.SelectedIndex]).ID; //((FS.HISFC.Models.Base.Const)ncboReportCode.alItems[0]).ID.ToString();
                reportName = ((FS.HISFC.Models.Base.Const)ncboReportCode.alItems[this.ncboReportCode.SelectedIndex]).Name; //((FS.HISFC.Models.Base.Const)ncboReportCode.alItems[0]).Name;
            }

            // ���������б�
            System.Collections.ArrayList list_dept = new System.Collections.ArrayList();

            list_dept.Add("��������");
            list_dept.Add("ִ�п���");
            list_dept.Add("�������ڿ���");

            for (int i = 0; i < list_dept.Count; i++)
            {
                this.ncboDept.Items.Add(list_dept[i]);
            }

            this.ncboDept.alItems.AddRange(list_dept);

            if (ncboDept.Items.Count > 0)
            {
                ncboDept.SelectedIndex = 0;
                dept = ncboDept.alItems[0].ToString();
            }

            // ��ͬ��λ�����б�
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            System.Collections.ArrayList list_pact = pactManager.QueryPactUnitAll();

            FS.HISFC.Models.Base.PactInfo top_pact = new FS.HISFC.Models.Base.PactInfo();
            top_pact.ID = "ALL";
            top_pact.Name = "ȫ��";
            this.ncboPact.Items.Add(top_pact);

            foreach (FS.HISFC.Models.Base.PactInfo var_pact in list_pact)
            {
                this.ncboPact.Items.Add(var_pact);
            }

            this.ncboPact.alItems.Add(top_pact);
            this.ncboPact.alItems.AddRange(list_pact);

            if (ncboPact.Items.Count > 0)
            {
                ncboPact.SelectedIndex = 0;
                pactCode = ((FS.HISFC.Models.Base.PactInfo)ncboPact.alItems[0]).ID;
                pactName = ((FS.HISFC.Models.Base.PactInfo)ncboPact.alItems[0]).Name;
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

            if (this.dept == "ִ�п���")
            {
                dept_exec = this.ncboDept.Items[this.ncboDept.SelectedIndex].ToString();
                dept_inhos = "ALL";
                dept_receipe = "ALL";
            }
            else if (this.dept == "��������")
            {
                dept_receipe = this.ncboDept.Items[this.ncboDept.SelectedIndex].ToString();
                dept_inhos = "ALL";
                dept_exec = "ALL";
            }
            else
            {
                dept_inhos = this.ncboDept.Items[this.ncboDept.SelectedIndex].ToString();
                dept_exec = "ALL";
                dept_receipe = "ALL";
            }

            if (department == "ALL")
            {
                this.dwMain.Modify("title.text='ȫԺ-" + pactName + "��Ŀ����ͳ��'");
            }
            else
            {
                this.dwMain.Modify("title.text='" + department + "-" + pactName + "��Ŀ����ͳ��'");
            }
            return base.OnRetrieve(this.dept, this.department, this.dtpBeginTime.Value, this.dtpEndTime.Value, "ALL", "ALL", "ALL", this.reportCode, this.pactCode);
        }

        /// <summary>
        /// ͳ�ƴ��������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncboReportCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ncboReportCode.SelectedIndex >= 0)
            {
                reportCode = ((FS.HISFC.Models.Base.Const)ncboReportCode.alItems[this.ncboReportCode.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)ncboReportCode.alItems[this.ncboReportCode.SelectedIndex]).Name;
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
            else
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
                this.dept = ncboDept.Items[this.ncboDept.SelectedIndex].ToString();
                //deptName = ((FS.HISFC.Models.Base.Department)ncboDept.Items[this.ncboDept.SelectedIndex]).Name;
            }
        }

        /// <summary>
        /// ��ͬ��λ�����б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncboPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ncboPact.SelectedIndex >= 0)
            {
                pactCode = ((FS.HISFC.Models.Base.PactInfo)ncboPact.Items[this.ncboPact.SelectedIndex]).ID;
                pactName = ((FS.HISFC.Models.Base.PactInfo)ncboPact.Items[this.ncboPact.SelectedIndex]).Name;
            }
        }
    }
}
