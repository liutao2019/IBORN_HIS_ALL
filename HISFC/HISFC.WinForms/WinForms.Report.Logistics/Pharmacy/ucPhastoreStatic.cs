using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Logistics.Pharmacy
{
    public partial class ucPhastoreStatic : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhastoreStatic()
        {
            InitializeComponent();
        }

        private string reportCode = string.Empty;
        private string reportName = string.Empty;

        #region ����
        /// <summary>
        /// ���ҹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// ҩƷ������Ϣ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// ҩƷ����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant phaConstManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
        /// <summary>
        /// ���ڴ洢�����б�
        /// </summary>
        private ArrayList deptArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩ������б�
        /// </summary>
        private ArrayList deptYKArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩƷ�����б�
        /// </summary>
        private ArrayList constArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩƷ������Ϣ
        /// </summary>
        private ArrayList drugArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩƷ������Ϣlist
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.Item> itemList = new List<FS.HISFC.Models.Pharmacy.Item>();
        /// <summary>
        /// ���ڴ洢������˾�б�
        /// </summary>
        private ArrayList companyArry = new ArrayList();
        /// <summary>
        /// �����ַ���
        /// </summary>
        private string queryStr = "(drug_quality like '{0}%')";

        private string queryStr2 = "(ҩƷ���� like '{0}%') or (pha_com_storage_ҩƷ���� like '{1}%') or (��Ӧ�� like '{2}%')";    
        #endregion

        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;

            base.OnLoad();


            this.init();

            this.neuTabControl1.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {
            #region ���ؿ���������
            deptArry = new ArrayList();
            deptYKArry = new ArrayList();
            deptArry = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            deptYKArry = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.PI);
            if (deptYKArry != null)
            {
                foreach (FS.HISFC.Models.Base.Department deptObj in deptYKArry)
                {
                    deptArry.Add(deptYKArry);
                }
            }
            this.cmbDept.AddItems(deptArry);
            #endregion

            #region ����ҩƷ����
            constArry = new ArrayList();
            constArry = constManager.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            this.cmbDrugQuality.AddItems(constArry);
            #endregion

            #region ����ҩƷ����
            drugArry = new ArrayList();
            itemList = new List<FS.HISFC.Models.Pharmacy.Item>();
            itemList = itemManager.QueryItemList();
            if (itemList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in itemList)
                {
                    drugArry.Add(itemObj);
                }

                this.cmbDrug.AddItems(drugArry);
            }
            #endregion

            #region ������λ
            companyArry = new ArrayList();
            companyArry = phaConstManager.QueryCompany("1");
            if (constArry != null)
            {
                this.cmbCompany.AddItems(companyArry);
            }
            #endregion

            

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
            
           
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            
            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ����ѯ")
            {
                this.MainDWDataObject = "d_pha_valid_qry";

                //return this.dwNoConfirm.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, this.employee.Dept.ID);
                return base.OnRetrieve(this.employee.Dept.ID);
               
            }
            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ���ͳ��")
            {
                this.MainDWDataObject = "d_pha_storesum_static";

                return base.OnRetrieve(this.employee.Dept.ID);
            }

            return 1;
           
        }
        /// <summary>
        /// text�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_TextChanged(object sender, EventArgs e)
        {
           
            string dept = this.cmbDept.Text.Trim().ToUpper().Replace(@"\","");
            string drug = this.cmbDrug.Text.Trim().ToUpper().Replace(@"\","");
            string company = this.cmbCompany.Text.Trim().ToUpper().Replace(@"\","");
            string drugQuality = this.cmbDrugQuality.Text.Trim().ToUpper().Replace(@"\","");

            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ����ѯ")
            {
                if (!this.chkCompany.Checked &&  !this.chkDrug.Checked && !this.chkDrugQuality.Checked)
                {

                    this.dwNoConfirm.SetFilter("");                   
                    this.dwNoConfirm.Filter();
                  

                    return;
                }
                else if (string.IsNullOrEmpty(drug) && string.IsNullOrEmpty(company) && string.IsNullOrEmpty(drugQuality))
                {
                    this.dwNoConfirm.SetFilter("");
                    this.dwNoConfirm.Filter();

                    return;

                }
                else
                {
                    string str = string.Format(this.queryStr2, drugQuality, drug, company);                    
                    this.dwNoConfirm.SetFilter(str);
                    this.dwNoConfirm.Filter();
                }
            }
            else if (this.neuTabControl1.SelectedTab.Text == "ҩƷ���ͳ��")
            {
                if (!this.chkCompany.Checked &&  !this.chkDrugQuality.Checked)
                {
                    this.dwMain.SetFilter("");
                    this.dwMain.Filter();

                    return;
                }
                else if (string.IsNullOrEmpty(dept)   && string.IsNullOrEmpty(drugQuality))
                {
                    this.dwMain.SetFilter("");
                    this.dwMain.Filter();

                    return;

                }
                else
                {
                    string str = string.Format(this.queryStr, drugQuality);
                    this.dwMain.SetFilter(str);
                    this.dwMain.Filter();
                }

            }
            else
            {
                if (!this.chkCompany.Checked && !this.chkDept.Checked && !this.chkDrug.Checked && !this.chkDrugQuality.Checked)
                {

                    this.dwNoConfirm.SetFilter("");
                    this.dwNoConfirm.Filter();


                    return;
                }
                else if (string.IsNullOrEmpty(dept) && string.IsNullOrEmpty(drug) && string.IsNullOrEmpty(company) && string.IsNullOrEmpty(drugQuality))
                {
                    this.dwNoConfirm.SetFilter("");
                    this.dwNoConfirm.Filter();

                    return;

                }
                else
                {
                    string str = string.Format(this.queryStr, dept, drugQuality, drug, company);
                    this.dwNoConfirm.SetFilter(str);
                    this.dwNoConfirm.Filter();
                }
            }
        }

       

      
    }
}
