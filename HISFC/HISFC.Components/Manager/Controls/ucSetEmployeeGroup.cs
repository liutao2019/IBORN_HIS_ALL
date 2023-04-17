using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    public partial class ucSetEmployeeGroup : FS.FrameWork.WinForms.Controls.ucBaseControl,
        FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucSetEmployeeGroup()
        {
            InitializeComponent();
            this.spread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.spread1_Sheet1.DataAutoSizeColumns = false;
            this.spread1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.spread1_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.spread1_Sheet1.Columns[2].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.spread1_Sheet1.Columns[3].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.spread1_Sheet1.DataAutoCellTypes = false;

            
        }

        

        #region ��ʼ��

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            FS.FrameWork.WinForms.Forms.ToolBarService toolService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            toolService.AddToolButton("���", "", FS.FrameWork.WinForms.Classes.EnumImageList.T���.GetHashCode(), true, false, null);
            toolService.AddToolButton("�޸�", "", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�.GetHashCode(), true, false, null);
            toolService.AddToolButton("ɾ��", "", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��.GetHashCode(), true, false, null);
            toolService.AddToolButton("��ӡ", "", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��.GetHashCode(), true, false, null);
            this.Init();
            return toolService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "���")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "�޸�")
            {
                this.Modify();
            }
            else if (e.ClickedItem.Text == "ɾ��")
            {
                this.Delete();
            }
            else if (e.ClickedItem.Text == "��ӡ")
            {
                this.Print();
            }
        }
        /// <summary>
        /// ��ʼ��DATASET
        /// </summary>
        private void InitDataSet()
        {
            DataTable table = new DataTable("��Ա");

            DataColumn dataColumn1 = new DataColumn("����");
            dataColumn1.DataType = typeof(System.String);
            table.Columns.Add(dataColumn1);

            DataColumn dataColumn2 = new DataColumn("��Ա����");
            dataColumn2.DataType = typeof(System.String);
            table.Columns.Add(dataColumn2);

            DataColumn dataColumn3 = new DataColumn("��¼��");
            dataColumn3.DataType = typeof(System.String);
            table.Columns.Add(dataColumn3);

            DataColumn dataColumn4 = new DataColumn("����Ա���");
            dataColumn4.DataType = typeof(System.String);
            table.Columns.Add(dataColumn4);

            DataColumn dataColumn5 = new DataColumn("ƴ��");
            dataColumn5.DataType = typeof(System.String);
            table.Columns.Add(dataColumn5);

            DataColumn dataColumn6 = new DataColumn("���");
            dataColumn6.DataType = typeof(System.String);
            table.Columns.Add(dataColumn6);

            ds.Tables.Add(table);
        }
        protected DataSet ds = new DataSet();
        private bool AddDataIntoTable(System.Data.DataTable table, ArrayList list)
        {
            if (table == null)
            {
                return false;
            }
            try
            {
                table.Clear();

                foreach (FS.HISFC.Models.Base.Employee obj in list)
                {
                    table.Rows.Add(new object[] { obj.ID, obj.Name, obj.User01, obj.IsManager, obj.SpellCode, obj.WBCode });
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
            return true;

        }

        FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();
        protected virtual void RefreshUserList()
        {
            //{1D7BC020-92AC-431b-B27B-1BFBEB0E566B} 
            FS.HISFC.Models.Base.Employee person = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            ArrayList al = new ArrayList();
            //�����Ա�б� 
            if (person.IsManager)
            {
                al = userManager.GetPeronList();
            }
            else
            {
                //���ǹ���Ա��ȡ�ò���Ա���ڿ��ҵ���Ա
                al = userManager.GetPeronList(person.Dept.ID);
            }

            if (al == null)
            {
                MessageBox.Show(userManager.Err);
                return;
            }
            AddDataIntoTable(ds.Tables[0], al); //�������
            dv = new DataView(ds.Tables[0]);   //��ʼ�� DataView 
            this.spread1.DataSource = dv;          // ������Դ
            this.spread1_Sheet1.Columns[3].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
        }
        protected DataView dv = null;
        #endregion

        #region ����
        private void ModifyUser()
        {
            try
            {
                FS.HISFC.Models.Base.Employee obj = GetPersonInfo();
                if (obj == null)
                {
                    return;
                }
                Forms.frmSetUserGroup f = new Forms.frmSetUserGroup(obj);
                f.ShowDialog();
                this.RefreshUserList();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private FS.HISFC.Models.Base.Employee GetPersonInfo()
        {
            FS.HISFC.Models.Base.Employee p = new FS.HISFC.Models.Base.Employee();
            try
            {
                p.ID = spread1_Sheet1.Cells[spread1_Sheet1.ActiveRowIndex, 0].Text;
                p.Name = spread1_Sheet1.Cells[spread1_Sheet1.ActiveRowIndex, 1].Text;
                p.User01 = spread1_Sheet1.Cells[spread1_Sheet1.ActiveRowIndex, 2].Text;
                p.IsManager = Convert.ToBoolean(spread1_Sheet1.Cells[spread1_Sheet1.ActiveRowIndex, 3].Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                p = null;
            }
            return p;
        }
        private void AddUser()
        {
            try
            {
                Forms.frmSetUserGroup f = new Forms.frmSetUserGroup(new FS.HISFC.Models.Base.Employee());
                f.CheckLogName+=new FS.HISFC.Components.Manager.Forms.frmSetUserGroup.CheckLogNameHandler(f_CheckLogName);
                f.ShowDialog();
                this.RefreshUserList();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private bool f_CheckLogName(string logName,ref string error)
        {
            if (ds.Tables.Count <= 0)
            {
                error="���ݳ�ʼ������";
                return false;
            }
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["��¼��"].ToString().ToLower() == logName.ToLower())
                {
                    error = "���û�[" + dr["��Ա����"].ToString() + "]�ĵ�½����" + logName + "�Ѵ��ڣ�";
                    return false;
                }
            }
            return true;
        }
        protected override void OnLoad(EventArgs e)
        {
            FS.FrameWork.WinForms.Forms.frmQuery q = this.iMaintenaceForm as FS.FrameWork.WinForms.Forms.frmQuery;
            if (q != null)
                q.ShowQueryButton = false;

            base.OnLoad(e);
        }
        #endregion

        #region IMaintenanceControlable ��Ա
        private FS.FrameWork.WinForms.Forms.IMaintenanceForm iMaintenaceForm = null;

        public int Add()
        {
            this.AddUser();
            return 0;
        }

        public int Delete()
        {
            return 0;   
        }

        public int Export()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "��ѡ�񱣴��·��������";
            sf.CheckFileExists = false;
            sf.CheckPathExists = true;
            sf.DefaultExt = "*.xls";
            sf.Filter = "(*.xls)|*.xls";
            DialogResult result = sf.ShowDialog();
            if (result == DialogResult.Cancel || sf.FileName == string.Empty)
            {
                return -1;
            }
            string filePath = sf.FileName;

            bool resultValue = this.spread1.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            return 0;
            //return this.spread1.Export();
        }

        public bool IsDirty
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsHaveGrid = true;
            p.PrintPage(0, 0, this.panel1);
            return 0;
        }

        public int Query()
        {
            this.RefreshUserList();
            return 0;
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return iMaintenaceForm;
            }
            set
            {
                iMaintenaceForm = value;
                if (iMaintenaceForm == null) return;
                iMaintenaceForm.ShowCopyButton = false;
                iMaintenaceForm.ShowCutButton = false;
                iMaintenaceForm.ShowDeleteButton = false;
                iMaintenaceForm.ShowImportButton = false;
                iMaintenaceForm.ShowNextRowButton = false;
                iMaintenaceForm.ShowPasteButton = false;
                iMaintenaceForm.ShowPreRowButton = false;
                iMaintenaceForm.ShowPrintConfigButton = false;
                iMaintenaceForm.ShowSaveButton = false;
            }
        }
        private void spread1_CellDoubleClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.ModifyUser();
        }
        public int Save()
        {
            return 0;
        }

        #endregion

        #region IMaintenanceControlable ��Ա


        public int Copy()
        {
            return 0;
        }

        public int Cut()
        {
            return 0;
        }

        public int Import()
        {
            return 0;
        }

        public int Init()
        {
            InitDataSet();
            this.RefreshUserList();
            this.spread1_Sheet1.Columns[0].Width = 100;
            this.spread1_Sheet1.Columns[1].Width = 200;
            this.spread1_Sheet1.Columns[2].Width = 200;
            this.spread1_Sheet1.Columns[3].Width = 100;
            this.spread1_Sheet1.Columns[4].Width = 100;
            this.spread1_Sheet1.Columns[5].Width = 100;
            return 0;
        }

        public int Modify()
        {
            ModifyUser();
            return 0;
        }

        public int NextRow()
        {
            return 0;
        }

        public int Paste()
        {
            return 0;
        }

        public int PreRow()
        {
            return 0;
        }

        public int PrintConfig()
        {
            return 0;
        }

        public int PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsHaveGrid = true;
            p.PrintPreview( this.panel1);
            return 0;
        }

        #endregion

        #region �¼�
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string temp = " like  '" + this.txtFilter.Text + "%' ";
                dv.RowFilter = "ƴ��" + temp + " or " + "���" + temp + " or " + "����" + temp + " or " + "��Ա����" + temp + " or " + "��¼��" + temp;
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                txtFilter.Text = "";
            }
        }
        #endregion




    }
}
