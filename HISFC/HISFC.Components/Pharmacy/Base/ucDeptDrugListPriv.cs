using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ����ҩƷ�б���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��
    ///		��鲡��ҽ�����Կ�����ҩƷ�б�
    ///  />
    /// </summary>
    public partial class ucDeptDrugListPriv : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDeptDrugListPriv()
        {
            InitializeComponent();
        }        

        private DataTable dt = null;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDpet = deptManager.GetDeptmentAll();
            this.cmbDept.AddItems(alDpet);

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList alPerson = personManager.GetEmployeeAll();
            this.cmbDoc.AddItems(alPerson);

            this.InitDataTable();

            return 1;
        }

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        /// <returns></returns>
        private void InitDataTable()
        {
            this.dt = new DataTable();

            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {
														new DataColumn("ҩƷ����",    dtStr),														
														new DataColumn("��Ʒ����",    dtStr),
														new DataColumn("���",        dtStr),
														new DataColumn("ƴ����",      dtStr),
														new DataColumn("�����",      dtStr),
				                                        new DataColumn("�Զ�����",    dtStr)
                    								});

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.dt.DefaultView.AllowNew = true;
        }

        /// <summary>
        /// �����ݼ���DataTable
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        private void AddDataToTable(FS.HISFC.Models.Pharmacy.Storage storage)
        {
            DataRow dr = this.dt.NewRow();

            dr["ҩƷ����"] = storage.Item.ID;
            dr["��Ʒ����"] = storage.Item.Name;
            dr["���"] = storage.Item.Specs;
            dr["ƴ����"] = storage.Item.NameCollection.SpellCode;
            dr["�����"] = storage.Item.NameCollection.WBCode;
            dr["�Զ�����"] = storage.Item.NameCollection.UserCode;

            this.dt.Rows.Add(dr);
        }

        /// <summary>
        /// �����ݼ���DataTable
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void AddDataToTable(FS.HISFC.Models.Pharmacy.Item item)
        {
            DataRow dr = this.dt.NewRow();

            dr["ҩƷ����"] = item.ID;
            dr["��Ʒ����"] = item.Name;
            dr["���"] = item.Specs;
            dr["ƴ����"] = item.NameCollection.SpellCode;
            dr["�����"] = item.NameCollection.WBCode;
            dr["�Զ�����"] = item.NameCollection.UserCode;

            this.dt.Rows.Add(dr);
        }

        /// <summary>
        /// ��ҩƷ�б���Ϣ�������ݱ�
        /// </summary>
        /// <param name="alStorage"></param>
        private void AddDataToTable(ArrayList alItem)
        {
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                this.AddDataToTable(item);
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
        }

        /// <summary>
        /// ��ҩƷ�б���Ϣ�������ݱ�
        /// </summary>
        /// <param name="alStorage"></param>
        private void AddDataToTable(List<FS.HISFC.Models.Pharmacy.Storage> alStorage)
        {
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorage)
            {
                this.AddDataToTable(storage);
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
        }

        /// <summary>
        /// ��ҩƷ�б���Ϣ�������ݱ�
        /// </summary>
        /// <param name="alStorage"></param>
        private void AddDataToTable(List<FS.HISFC.Models.Pharmacy.Item> alItem)
        {
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                this.AddDataToTable(item);
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        protected void Query()
        {            
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
            {
                MessageBox.Show(Language.Msg("��ѡ���ѯ�ⷿ"));
                return;
            }
            if (!this.ckIgnoreDoc.Checked)
            {
                if (this.cmbDoc.Tag == null || this.cmbDoc.Tag.ToString() == "")
                {
                    MessageBox.Show(Language.Msg("��ѡ��ҽ��"));
                    return;
                }
            }

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.BizProcess.Integrate.Pharmacy integratePha = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            string deptCode = this.cmbDept.Tag.ToString();
            string docCode = "";
            string docGrade = "";
            if (!this.ckIgnoreDoc.Checked && this.cmbDoc.Tag != null)
            {
                docCode = this.cmbDoc.Tag.ToString();

                FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee person = personManager.GetPersonByID(docCode);

                docGrade = person.Level.ID;
            }

            this.dt.Rows.Clear();

            List<FS.HISFC.Models.Pharmacy.Item> alList = new List<FS.HISFC.Models.Pharmacy.Item>();
            ArrayList alArrList = new ArrayList();
            if (!this.ckIgnoreDoc.Checked)
            {
                alList = integratePha.QueryItemAvailableList(deptCode, docCode, docGrade);

                if (alList == null)
                {
                    MessageBox.Show(integratePha.Err);
                    return;
                }

                this.AddDataToTable(alList);

                MessageBox.Show(Language.Msg("��ѯ���"));
            }
            else
            {
                alArrList = itemManager.QueryItemAvailableList(deptCode);

                if (alArrList == null)
                {
                    MessageBox.Show(itemManager.Err);
                    return;
                }

                this.AddDataToTable(alArrList);

                MessageBox.Show(Language.Msg("��ѯ���"));
            }
        }

        /// <summary>
        /// ���ڹر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void cmbDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDoc.Tag == null)
            {
                return;
            }

            string docCode = this.cmbDoc.Tag.ToString();

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.Models.Base.Employee person = personManager.GetPersonByID(docCode);

            this.lbDocInfo.Text = string.Format("ҽ����Ϣ: {0} ְ�� {1} ְ�� {2}", person.Name, person.Level.ID, person.Duty.ID);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string queryCode = "";
            queryCode = "%" + this.txtFilter.Text.Trim() + "%";
            string filter = "";

            filter = Function.GetFilterStr(this.dt.DefaultView, queryCode);

            this.dt.DefaultView.RowFilter = filter;            
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
