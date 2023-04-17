using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.DrugStore
{
    public partial class ucPhaMetDrugSorted : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaMetDrugSorted()
        {
            InitializeComponent();
        }
        #region ����
      
        /// <summary>
        /// ���Ź�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// ҩƷ����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant phaConstManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
        /// <summary>
        /// ҩƷ�����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item phaItemManager =new FS.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// ���ڴ洢ҩ������б�
        /// </summary>
        private ArrayList deptArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩƷ������Ϣlist
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.Item> drugList = new List<FS.HISFC.Models.Pharmacy.Item>();
        /// <summary>
        /// ���ڴ洢ҩƷ������Ϣ
        /// </summary>
        private ArrayList drugArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩƷ�����б�
        /// </summary>
        private ArrayList drugQulityArry = new ArrayList();
        /// <summary>
        /// ���ڴ洢��Ӧ���б�
        /// </summary>
        private ArrayList companyArry = new ArrayList();

        private string strQuery = "(��Ʒ��ƴ���� like '{0}%') or(��Ʒ������� like '{1}%') or (ҩƷ���� like '{2}%')";
        #endregion


        #region ��ѯ
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string sortStr = string.Empty;
            string deptStr = "ALL";
            string deptNameStr = string.Empty;
            string companyStr = "ALL";
            string drugQualityStr = "ALL";            
            string drugStr = "ALL";
            int sortCount = 100;


            if (this.cmbSorted.Items[this.cmbSorted.SelectedIndex].ToString() != null)
            {
                sortStr = this.cmbSorted.Items[this.cmbSorted.SelectedIndex].ToString();
            }
            if (this.cmbDept.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDept.SelectedItem.Name))
                {
                    deptStr = this.cmbDept.SelectedItem.ID;
                    deptNameStr = this.cmbDept.SelectedItem.Name;
                }
            }
            if (this.cmbCompany.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbCompany.SelectedItem.ID))
                {
                    companyStr = this.cmbCompany.SelectedItem.ID;
                }
            }
            if (this.cmbDrug.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDrug.SelectedItem.ID))
                {
                    drugStr = this.cmbDrug.SelectedItem.ID;
                }
            }
            if (this.cmbDrugQulity.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDrugQulity.SelectedItem.ID))
                {
                    drugQualityStr = this.cmbDrugQulity.SelectedItem.ID;
                }
            }
            if(!string.IsNullOrEmpty(this.txtSortNum.Text))
            {
                sortCount = FS.FrameWork.Function.NConvert.ToInt32(this.txtSortNum.Text);

            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptStr, drugStr, companyStr, drugQualityStr,  sortCount,sortStr, this.employee.Name, deptNameStr);
            
        }

        #endregion

        #region �¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.init();

            this.isAcross = true;
            this.isSort = false;
        }

        /// <summary>
        /// �ı��ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDrug_TextChanged(object sender, EventArgs e)
        {
            string drugStr = this.txtDrug.Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();
            string drugSpellStr = this.txtDrug.Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();
            string drugWBStr = this.txtDrug.Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();

            DataView dv = this.dwMain.Dv;
            if (dv == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.txtDrug.Text))
            {
                //this.dwMain.SetFilter("");
                //this.dwMain.Filter();
                dv.RowFilter = "";
                return;

            }
            else
            {
                string str = string.Format(strQuery, drugSpellStr, drugWBStr, drugStr);
                //this.dwMain.SetFilter(str);
                //this.dwMain.Filter();
                dv.RowFilter = str;
               // return;
            }

        }
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {
            #region ���ؿ���
            this.cmbDept.Items.Clear();
            this.deptArry = new ArrayList();
            deptArry = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.PI);//�������е�ҩ�����
            if (deptArry != null)
            {
                this.cmbDept.AddItems(deptArry);
            }
            #endregion

            #region ���ع�����
            this.cmbCompany.Items.Clear();
            this.companyArry = new ArrayList();
            companyArry = phaConstManager.QueryCompany("1");//�������й�����˾
            if (companyArry != null)
            {
                this.cmbCompany.AddItems(companyArry);
            }
            #endregion

            #region ����ҩƷ����
            this.cmbDrugQulity.Items.Clear();
            this.drugQulityArry = new ArrayList();
            drugQulityArry = constManager.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            if (drugQulityArry != null)
            {
                this.cmbDrugQulity.AddItems(drugQulityArry);
            }
            #endregion

            #region ������������
            this.cmbSorted.Items.Clear();
            //this.cmbSorted.Items.Add("����");
            //this.cmbSorted.Items.Add("������");
            //this.cmbSorted.Items.Add("���۽��");
            ArrayList al = new ArrayList();
            al.Add(new FS.FrameWork.Models.NeuObject("0","����","������������"));
            al.Add(new FS.FrameWork.Models.NeuObject("1", "������", "���ݹ���������"));
            al.Add(new FS.FrameWork.Models.NeuObject("2", "���۽��", "�������۽������"));
            this.cmbSorted.AddItems(al);

            this.cmbSorted.SelectedIndex = 0;
            #endregion

            #region ����ҩƷ����
            this.cmbDrug.Items.Clear();
            this.drugList.Clear();
            this.drugArry = new ArrayList();
            this.drugList = phaItemManager.QueryItemList();
            if (drugList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in drugList)
                {
                    drugArry.Add(itemObj);
                }

                this.cmbDrug.AddItems(drugArry);
            }
            #endregion
        }


        #endregion

        private void cmbSorted_TextChanged(object sender, EventArgs e)
        {
            string filter = "{0} desc";

            DataView dv = this.dwMain.Dv;
            if (dv == null)
            {
                return;
            }
            
            
            if(!string.IsNullOrEmpty(this.cmbSorted.Items[this.cmbSorted.SelectedIndex].ToString()))            
            {
                string str = string.Format(filter, this.cmbSorted.Items[this.cmbSorted.SelectedIndex].ToString());



                dv.Sort = str;
                
                //dwMain.SetSort(str);
                //dwMain.Sort();


            }
           
        }

        



    }
}
