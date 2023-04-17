using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;
using FS.HISFC.Models.Material;


namespace FS.HISFC.Components.Material.Base
{
    /// <summary>		
    /// ucMaterialQuery��ժҪ˵����<br></br>
    /// [��������: ������Ϣ��ѯ]<br></br>
    /// [�� �� ��: �]<br></br>
    /// [����ʱ��: 2007-03-28<br></br>
    /// </summary>
    public partial class ucMaterialQuery : UserControl
    {
        public ucMaterialQuery()
        {
            InitializeComponent();
        }

        #region ������

        /// <summary>
        /// �����ֵ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem materialManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ���ʹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store matManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ���ʲֿ⡢��Ŀ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset basesetManager = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// ���ʹ�����˾����
        /// </summary>
        private FS.HISFC.BizLogic.Material.ComCompany companyManager = new FS.HISFC.BizLogic.Material.ComCompany();

        /// <summary>
        /// ���ʿ�Ŀ�滻
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemKindObjHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���������滻
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper produceHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������˾�滻
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����

        /// <summary>
        /// XML·��
        /// </summary>
        private string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\MaterialItem.xml";

        /// <summary>
        /// ���ʻ�����Ϣʵ��
        /// </summary>
        private FS.HISFC.Models.Material.MaterialItem materialItem = new FS.HISFC.Models.Material.MaterialItem();

        /// <summary>
        /// ��ѡ���б������DataSet
        /// </summary>
        private DataSet myDataSet;

        /// <summary>
        /// ���ݼ�
        /// </summary>
        protected DataTable myDataTable = new DataTable();

        /// <summary>
        /// ������ͼ
        /// </summary>
        private DataView dv;

        /// <summary>
        /// ������˾ʹ�õ�����
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        /// <summary>
        /// ��������ʹ�õ�����
        /// </summary>
        private ArrayList alFactory = new ArrayList();

        /// <summary>
        /// ���ʿ�Ŀʹ�õ�����
        /// </summary>
        private ArrayList alItemKind = new ArrayList();

        /// <summary>
        /// �����ֵ�����
        /// </summary>
        private List<FS.HISFC.Models.Material.MaterialItem> nowDrugList = new List<FS.HISFC.Models.Material.MaterialItem>();

        /// <summary>
        /// ��С���ô�������
        /// </summary>
        private ArrayList alFeeCode = new ArrayList();

        /// <summary>
        /// ͳ�ƴ�������
        /// </summary>
        private ArrayList alStatCode = new ArrayList();

        /// <summary>
        /// �Ӽ۹�������
        /// </summary>
        private ArrayList alAddRule = new ArrayList();

        /// <summary>
        /// �洢�ֿ�����
        /// </summary>
        private ArrayList alStorCode = new ArrayList();

        /// <summary>
        /// ��ǰ���ʿ�Ŀ����
        /// </summary>
        private string matKind = "";

        /// <summary>
        /// �Ƿ��Ѷ�MyInput�¼����������й�ע��
        /// </summary>
        private bool isEventRegister = false;

        /// <summary>
        /// ֻ������
        /// </summary>
        private bool isEditExpediency = true;

        /// <summary>
        /// ɾ��ʱ �Ƿ���ø���״̬Ϊ'2' �ķ�ʽ (��ʵ��ɾ��)
        /// </summary>
        private bool isDelToUpdateState = false;

        public delegate void SaveInput(FS.HISFC.Models.Pharmacy.Item item);

        public string storageCode;//liuxq add

        /// <summary>
        /// ����ά���ؼ�
        /// </summary>
        private ucMaterialManager MainteranceUC = null;

        /// <summary>
        /// ����ά������
        /// </summary>
        private System.Windows.Forms.Form MainteranceForm = null;

        #endregion

        #region ����

        // <summary>
        /// XML·������
        /// </summary>
        public string FilePath
        {
            set
            {
                try
                {
                    this.filePath = value;
                }
                catch
                {
                    this.filePath = ".\\PharmacyManager.xml";
                }
            }
        }

        /// <summary>
        /// ���ʿ�Ŀ�������
        /// </summary>
        public string MatKind
        {
            get
            {
                return this.matKind;
            }
            set
            {
                this.matKind = value;
            }
        }

        /// <summary>
        /// �Ƿ�ӵ���޸�Ȩ�� 1 ���޸�Ȩ�� 0 ���޸�Ȩ��
        /// </summary>
        public bool EditExpediency
        {
            set
            {
                this.isEditExpediency = value;
            }
        }

        /// <summary>
        /// DataView
        /// </summary>
        public DataView DefaultDataView
        {
            get { return dv; }
        }

        #endregion

        #region ά����������

        /// <summary>
        /// ά���������� ��̳���Material.Base.ucMaterialManager
        /// </summary>
        public Material.Base.ucMaterialManager MaintenancePopUC
        {
            set
            {
                if (value != null && value as Material.Base.ucMaterialManager == null)
                {
                    System.Windows.Forms.MessageBox.Show("��ά���ؼ���̳���Material.Base.ucMaterialManager");
                }
                else
                {
                    this.MainteranceUC = value as Material.Base.ucMaterialManager;

                    this.MainteranceUC.MyInput -= new ucMaterialManager.SaveInput(ucMaterialManager_MyInput);
                    this.MainteranceUC.MyInput += new ucMaterialManager.SaveInput(ucMaterialManager_MyInput);
                }
            }
        }

        /// <summary>
        /// ����ά������
        /// </summary>
        private void InitMaintenanceForm()
        {
            if (this.MainteranceUC == null)
            {
                this.MainteranceUC = new Material.Base.ucMaterialManager();
                this.MainteranceUC.MyInput -= new Material.Base.ucMaterialManager.SaveInput(ucMaterialManager_MyInput);
                this.MainteranceUC.MyInput += new Material.Base.ucMaterialManager.SaveInput(ucMaterialManager_MyInput);
            }
            if (this.MainteranceForm == null)
            {
                this.MainteranceForm = new Form();
                this.MainteranceForm.Width = this.MainteranceUC.Width + 10;
                this.MainteranceForm.Height = this.MainteranceUC.Height + 25;
                this.MainteranceForm.Text = "��Ʒ��ϸ��Ϣά��";
                this.MainteranceForm.StartPosition = FormStartPosition.CenterScreen;
                this.MainteranceForm.ShowInTaskbar = false;
                this.MainteranceForm.HelpButton = false;
                this.MainteranceForm.MaximizeBox = false;
                this.MainteranceForm.MinimizeBox = false;
                this.MainteranceForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            }


            this.MainteranceUC.Dock = DockStyle.Fill;
            this.MainteranceForm.Controls.Add(this.MainteranceUC);
        }

        /// <summary>
        /// ά��������ʾ
        /// </summary>
        private void ShowMaintenanceForm(string inputType, FS.HISFC.Models.Material.MaterialItem item, bool isShow)
        {
            if (this.MainteranceForm == null || this.MainteranceUC == null)
                this.InitMaintenanceForm();

            this.MainteranceUC.InputType = inputType;
            this.MainteranceUC.Item = item;
            this.MainteranceUC.MatKind = this.MatKind;
            this.MainteranceUC.storageCode = storageCode;
            this.MainteranceUC.ReadOnly = !this.isEditExpediency;

            if (isShow)
            {
                this.MainteranceForm.ShowDialog();
            }
        }

        #endregion

        #region ����

        #region ��ʼ������

        /// <summary>
        /// ����DataTable�е���
        /// �������Ŀ¼��������е������ļ����򰴴��ļ���ʾ�У������趨DataTable�ĳ�ʼ��
        /// </summary>
        /// <param name="table"></param>
        private void SetColumn(DataTable table)
        {
            if (System.IO.File.Exists(this.filePath))
            {
                #region ��Xml�����ļ��ڶ�ȡ������
                XmlDocument doc = new XmlDocument();
                try
                {
                    StreamReader sr = new StreamReader(this.filePath, System.Text.Encoding.Default);
                    string cleandown = sr.ReadToEnd();
                    sr.Close();
                    doc.LoadXml(cleandown);
                }
                catch { return; }

                XmlNodeList nodes = doc.SelectNodes("//Column");

                string tempString = "";

                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["type"].Value == "TextCellType")
                    {
                        tempString = "System.String";
                    }
                    else if (node.Attributes["type"].Value == "CheckBoxCellType")
                    {
                        tempString = "System.Boolean";
                    }

                    table.Columns.Add(new DataColumn(node.Attributes["displayname"].Value,
                        System.Type.GetType(tempString)));
                }
                #endregion
            }
            else
            {
                #region ����Ĭ��DataTable���� ��ʾ
                //��������
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtDec = System.Type.GetType("System.Decimal");
                System.Type dtDTime = System.Type.GetType("System.DateTime");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                table.Columns.AddRange(new DataColumn[]{
															new DataColumn("��Ʒ����", dtStr),															   
															new DataColumn("��Ʒ��Ŀ", dtStr),  
															new DataColumn("��Ʒ����", dtStr),                                     
															new DataColumn("ƴ����", dtStr),     
															new DataColumn("�����", dtStr),   
															new DataColumn("�Զ�����", dtStr),   
															new DataColumn("���ұ���", dtStr),   
															new DataColumn("���", dtStr),   
															new DataColumn("��λ", dtStr),   
															new DataColumn("����", dtStr), 
															new DataColumn("�����շѱ�־",dtStr),
															new DataColumn("������Ϣ", dtStr),   
															new DataColumn("ҽ����Ŀ����", dtStr),       
															new DataColumn("ҽ����Ŀ����", dtStr),   
															new DataColumn("��ҩƷ����", dtStr),   
															new DataColumn("��ҩƷ����", dtStr),     
															new DataColumn("ͣ�ñ��", dtStr),     
															new DataColumn("�����־", dtStr), 
															new DataColumn("��������", dtStr),       
															new DataColumn("������˾", dtStr),       
															new DataColumn("���ô���", dtStr),       
															new DataColumn("ͳ�ƴ���", dtStr),
                                                            new DataColumn("��Ŀ����",dtStr),
															new DataColumn("��װ��λ",dtStr),
															new DataColumn("��װ����",dtStr),
															new DataColumn("��װ�۸�",dtStr),
															new DataColumn("�Ӽ۹���",dtStr),
															new DataColumn("�ֿ�����",dtStr),
															new DataColumn("��Դ",dtStr),
															new DataColumn("��;",dtStr)
														});

                this.fpMaterialQuery_Sheet1.DataSource = table;
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMaterialQuery_Sheet1, this.filePath);
                #endregion
            }

            DataColumn[] keys = new DataColumn[1];
            keys[0] = table.Columns["��Ʒ����"];
            table.PrimaryKey = keys;

            for (int i = 0; i < this.fpMaterialQuery_Sheet1.Columns.Count; i++)
            {
                this.fpMaterialQuery_Sheet1.Columns[i].Locked = true;
            }
        }


        /// <summary>
        /// ������������е����ݱ�����myDataSet��
        /// </summary>
        /// <param name="al">�����ֵ�����</param>
        public int InitDataSet(List<FS.HISFC.Models.Material.MaterialItem> al)
        {
            alItemKind = this.basesetManager.QueryKind();

            alFactory = this.companyManager.QueryCompany("0", "A");

            alCompany = this.companyManager.QueryCompany("1", "A");

            nowDrugList = al;

            itemKindObjHelper.ArrayObject = this.alItemKind;
            produceHelper.ArrayObject = this.alFactory;
            produceHelper.ArrayObject = this.alCompany;

            myDataSet = new DataSet();

            myDataSet.EnforceConstraints = true;//�Ƿ���ѭԼ������

            myDataSet.Tables.Clear();

            //��������ӵ�myDataSet��
            DataTable myDataTable = myDataSet.Tables.Add("MaterialItem");

            //��XML�ж�ȡ��˳��,���ȵ�����
            this.SetColumn(myDataTable);

            DataRow newRow;

            FS.HISFC.Models.Material.MaterialItem myItem;

            //ѭ��������Ʒ������Ϣ
            for (int i = 0; i < al.Count; i++)
            {
                myItem = (FS.HISFC.Models.Material.MaterialItem)al[i];

                newRow = myDataTable.NewRow();

                try
                {
                    //�����ݲ��뵽DataTable��
                    this.SetRow(newRow, myItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n��ɾ������Ŀ¼�µ��ļ�:" + this.filePath, "������ʾ");
                    return -1;
                }

                myDataTable.Rows.Add(newRow);

            }

            dv = new DataView(myDataSet.Tables[0]);

            this.fpMaterialQuery.DataSource = dv;

            for (int i = 0; i < this.fpMaterialQuery_Sheet1.Columns.Count; i++)
            {
                this.fpMaterialQuery_Sheet1.Columns[i].Locked = true;
            }

            this.SetColor();

            return 1;
        }

        /// <summary>
        /// ��ͣ�õ�������Ŀ��Ϊ��ɫ
        /// </summary>
        private void SetColor()
        {
            for (int i = 0; i < this.fpMaterialQuery_Sheet1.Rows.Count; i++)
            {
                if (this.fpMaterialQuery_Sheet1.Cells[i, 16].Text == "ͣ��")
                {
                    this.fpMaterialQuery_Sheet1.Rows[i].ForeColor = Color.Red;
                }
                else
                {
                    this.fpMaterialQuery_Sheet1.Rows[i].ForeColor = Color.Black;
                }
            }
        }


        /// <summary>
        /// ��DataSet�в�������
        /// </summary>
        /// <param name="row"></param>
        /// <param name="myItem"></param>
        /// <returns></returns>
        private DataRow SetRow(DataRow row, FS.HISFC.Models.Material.MaterialItem myItem)
        {
            row["��Ʒ����"] = myItem.ID;
            row["��Ʒ��Ŀ"] = this.itemKindObjHelper.GetName(myItem.MaterialKind.ID.ToString());
            row["��Ʒ����"] = myItem.Name;
            row["ƴ����"] = myItem.SpellCode;
            row["�����"] = myItem.WBCode;
            row["�Զ�����"] = myItem.UserCode;
            row["���ұ���"] = myItem.GbCode;
            row["���"] = myItem.Specs;
            row["��λ"] = myItem.MinUnit;
            row["����"] = myItem.UnitPrice.ToString();
            if (myItem.FinanceState)
            {
                row["�����շѱ�־"] = "��";
            }
            else
            {
                row["�����շѱ�־"] = "��";
            }
            row["������Ϣ"] = myItem.ApproveInfo;
            row["ҽ����Ŀ����"] = myItem.Compare.ID;
            row["ҽ����Ŀ����"] = myItem.Compare.Name;
            row["��ҩƷ����"] = myItem.UndrugInfo.ID;
            row["��ҩƷ����"] = myItem.UndrugInfo.Name;
            if (myItem.ValidState)
            {
                row["ͣ�ñ��"] = "ʹ��";
            }
            else
            {
                row["ͣ�ñ��"] = "ͣ��";
            }
            row["�����־"] = myItem.SpecialFlag;
            row["��������"] = this.produceHelper.GetName(myItem.Factory.ID.ToString());
            row["������˾"] = this.produceHelper.GetName(myItem.Company.ID.ToString());
            row["���ô���"] = myItem.MinFee.ID;
            row["ͳ�ƴ���"] = myItem.StatInfo.ID;
            row["��Ŀ����"] = myItem.MaterialKind.ID;
            row["��װ��λ"] = myItem.PackUnit;
            row["��װ����"] = myItem.PackQty;
            row["��װ�۸�"] = myItem.PackPrice;
            row["�Ӽ۹���"] = myItem.AddRule;
            row["�ֿ�����"] = itemKindObjHelper.GetName(myItem.StorageInfo.ID);
            row["��Դ"] = myItem.InSource;
            row["��;"] = myItem.Usage;
            return row;

        }


        #endregion

        /// <summary>
        /// ��յ�ǰ����
        /// </summary>
        public void Clear()
        {
            this.fpMaterialQuery_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        public void Modify()
        {
            if (this.fpMaterialQuery_Sheet1.Rows.Count == 0)
                return;

            DataRow findRow;

            MaterialItem myItem = null;
            myItem = this.materialManager.GetMetItemByMetID(this.fpMaterialQuery_Sheet1.Cells[this.fpMaterialQuery_Sheet1.ActiveRowIndex, this.dv.Table.Columns.IndexOf("��Ʒ����")].Value.ToString());

            this.ShowMaintenanceForm("U", myItem, true);

            findRow = dv.Table.Rows.Find(myItem.ID.ToString());

            if (findRow != null)
            {
                //���ݱ���ȡȫ����Ϣ����ʾ���б���
                myItem = materialManager.GetMetItemByMetID(myItem.ID.ToString());
                this.SetRow(findRow, myItem);
            }
            this.SetColor();
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void Copy()
        {
            if (this.fpMaterialQuery_Sheet1.Rows.Count == 0)
                return;

            MaterialItem myItem = null;
            myItem = materialManager.GetMetItemByMetID(this.fpMaterialQuery_Sheet1.Cells[this.fpMaterialQuery_Sheet1.ActiveRowIndex, this.dv.Table.Columns.IndexOf("��Ʒ����")].Value.ToString());

            myItem.ID = "";

            this.ShowMaintenanceForm("I", myItem, true);
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Add()
        {
            MaterialItem myItem = null;
            myItem = new MaterialItem();
            myItem.StorageInfo.ID = storageCode;
            myItem.MaterialKind.ID = this.MatKind;

            this.ShowMaintenanceForm("I", myItem, true);

            this.SetColor();
        }

        /// <summary>
        /// �ؼ���������ʾһ������
        /// </summary>
        /// <param name="obj"></param>
        public void AddNewRow(FS.HISFC.Models.Material.MaterialItem obj)
        {
            DataRow newRow = dv.Table.NewRow();

            this.SetRow(newRow, obj);

            dv.Table.Rows.Add(newRow);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        public void Delete()
        {
            if (this.fpMaterialQuery_Sheet1.Rows.Count == 0)
                return;

            int parm;
            string itemID = "";

            itemID = this.fpMaterialQuery_Sheet1.Cells[this.fpMaterialQuery_Sheet1.ActiveRowIndex, this.dv.Table.Columns.IndexOf("��Ʒ����")].Value.ToString();

            int count = this.matManager.GetMatStorageRowNum(itemID);

            if (count > 0)
            {
                MessageBox.Show("����Ʒ�ڿ�����Ѵ���,������ɾ��!", "ɾ����ʾ");
                return;
            }

            if (count < 0)
            {
                MessageBox.Show("��ȡ����Ʒ�ڿ���е�����������");
                return;
            }

            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.Show("�����ȷ���Ƿ�ɾ������Ʒ?", "��ʾ!", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }

            if (this.isDelToUpdateState)
            {
                #region

                FS.HISFC.Models.Material.MaterialItem itemTemp = this.materialManager.GetMetItemByMetID(itemID);
                if (itemTemp != null)
                {

                }

                #endregion
            }
            else
            {
                #region ����ɾ��

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();

                materialManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //ɾ��
                parm = materialManager.DeleteMetItem(itemID);

                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.materialManager.Err);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("ɾ���ɹ���");
                }

                //��DataTable�в��Ҵ���Ʒ
                DataRow findRow;

                Object[] obj = new object[1];

                obj[0] = itemID.ToString();

                findRow = dv.Table.Rows.Find(obj);

                //��DataTable��ɾ������Ʒ
                if (findRow != null)
                {
                    dv.Table.Rows.Remove(findRow);
                }

                #endregion
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Export()
        {

        }

        /// <summary>
        /// ���ݴ���Ĺ�������,��������
        /// </summary>
        /// <param name="filter">��������</param>
        public void SetFilter(string filter)
        {
            //��������
            this.dv.RowFilter = filter;

            //������������ļ�������������ļ��е���ʽ������ΪDataSet��Ĭ����ʽ
            //if(System.IO.File.Exists(this.filePath))
            //	FS.FrameWork.WinForms.CustomerFp.ReadColumnProperty( this.fpPharmacyQuery_Sheet1,this.filePath);
            this.SetColor();
        }

        #endregion

        #region �¼�

        private void fpMaterialQuery_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.isEditExpediency)	//ӵ���޸�Ȩ��
            {
                this.Modify();
            }
        }


        private void ucMaterialQuery_Load(object sender, System.EventArgs e)
        {

        }


        private void ucMaterialManager_MyInput(FS.HISFC.Models.Material.MaterialItem item)
        {
            this.AddNewRow(item);

        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Control.GetHashCode() + Keys.C.GetHashCode())
            {
                this.Copy();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }


        private void fpMaterialQuery_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void fpMaterialQuery_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMaterialQuery_Sheet1, this.filePath);
        }

        #endregion
    }
}
