using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.HISFC.Object.Pharmacy;
using System.Collections;
using Neusoft.NFC.Function;

namespace UFC.Preparation
{
    public partial class frmPrescription : Form
    {
        public frmPrescription()
        {
            InitializeComponent();
        }

        #region ˽�б���
        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private Neusoft.HISFC.Management.Pharmacy.Preparation preparationMgr = new Neusoft.HISFC.Management.Pharmacy.Preparation();
        /// <summary>
        /// ҩƷ������
        /// </summary>
        private Neusoft.HISFC.Management.Pharmacy.Item itemMgr = new Neusoft.HISFC.Management.Pharmacy.Item();
        /// <summary>
        /// ���ƴ������ݼ�
        /// </summary>
        private DataSet dsPrescription;
        /// <summary>
        /// ���ƴ���DataView
        /// </summary>
        private DataView dvPrescription;
        /// <summary>
        /// �����ַ���
        /// </summary>
        private string filterStr = "";
        /// <summary>
        /// ��ά���ĳ�Ʒ��Ϣ
        /// </summary>
        private ArrayList alDrug;

        private List<Neusoft.HISFC.Object.Preparation.Prescription> alPrescription;
        /// <summary>
        /// ��ǰ��ʾ�����ƴ����ĳ�Ʒ����
        /// </summary>
        private string nowDrugPrescription = "";
        /// <summary>
        /// �Ƿ�Գ�Ʒ��ִ�й���
        /// </summary>
        private bool isFilter = true;
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ��ػ������� ���Ժ�...");
            Application.DoEvents();

            this.ucQueryItem1.Init(false,"E");
            this.ucQueryItem1.SelectItem += new EventHandler(ucQueryItem1_SelectItem);
            this.ucQueryItem1.TextKeyDown += new KeyEventHandler(ucQueryItem1_TextKeyDown);
            this.ucQueryItem1.TextChanged += new EventHandler(ucQueryItem1_TextChanged);

            this.ucQueryItem2.Init(false,"E1");
            this.ucQueryItem2.isCheck = true;
            this.ucQueryItem2.isShow = false;
            this.ucQueryItem2.SelectItem += new EventHandler(ucQueryItem2_SelectItem);
            this.ucQueryItem2.TextKeyDown += new KeyEventHandler(ucQueryItem1_TextKeyDown);
            this.ucQueryItem2.TextChanged += new EventHandler(ucQueryItem1_TextChanged);
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// ���ݼ���ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitDataSet()
        {
            try
            {
                this.dsPrescription = new DataSet();
                Type dtStr = System.Type.GetType("System.String");
                Type dtDec = typeof(System.Decimal);
                Type dtInt = typeof(System.Int32);
                Type dtBol = typeof(System.Boolean);
                Type dtDate = typeof(System.DateTime);

                DataTable dt = new DataTable("Table");
                dt.Columns.AddRange(new DataColumn[] {
															new DataColumn("ҩƷ����",dtStr),			//ҩƷ����
															new DataColumn("ҩƷ����",dtStr),			//ҩƷ����
															new DataColumn("ҩƷͨ����",dtStr),			//ҩƷͨ����
															new DataColumn("Ӣ����Ʒ��",dtStr),			//Ӣ����Ʒ��
															new DataColumn("���",dtStr),				//���
															new DataColumn("��װ����",dtDec),			//��װ����
															new DataColumn("��װ��λ",dtStr),			//��װ��λ
															new DataColumn("��С��λ",dtStr),			//��С��λ
															new DataColumn("���ۼ�",dtDec),				//���ۼ�
															new DataColumn("ҩƷ���",dtStr),			//ҩƷ���
															new DataColumn("ҩƷ����",dtStr),			//ҩƷ����
															new DataColumn("ϵͳ���",dtStr),			//ϵͳ���
															new DataColumn("�Ƿ�ͣ��",dtBol),			//�Ƿ�ͣ��
															new DataColumn("ƴ����",dtStr),				//ƴ����
															new DataColumn("�����",dtStr),				//�����
														    new DataColumn("�Զ�����",dtStr),			//�Զ�����
															new DataColumn("ͨ����ƴ����",dtStr),			//ͨ����ƴ����
															new DataColumn("ͨ���������",dtStr),		//ͨ���������
															new DataColumn("ͨ�����Զ�����",dtStr)		//ͨ�����Զ�����															
														});
                DataColumn[] keys = new DataColumn[] { dt.Columns["ҩƷ����"] };
                dt.PrimaryKey = keys;
                this.dsPrescription.Tables.Add(dt);

                this.filterStr = "(ҩƷ���� like '{0}') or (ƴ���� like '{1}') or (����� like '{2}') or (�Զ����� like '{3}')";
                //��ʽ��FarPoint
                this.SetFormat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ݼ���ʼ����������" + ex.Message);
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1_Sheet1.Columns.Count = this.dsPrescription.Tables[0].Columns.Count;

            this.neuSpread1_Sheet1.Columns[0].Label = "ҩƷ����";
            this.neuSpread1_Sheet1.Columns[0].Visible = false;

            this.neuSpread1_Sheet1.Columns[1].Label = "��Ʒ��";
            this.neuSpread1_Sheet1.Columns[1].Visible = true;
            this.neuSpread1_Sheet1.Columns[1].Width = 180F;

            this.neuSpread1_Sheet1.Columns[2].Label = "ͨ����";
            this.neuSpread1_Sheet1.Columns[2].Visible = false;
            this.neuSpread1_Sheet1.Columns[3].Label = "Ӣ����";
            this.neuSpread1_Sheet1.Columns[3].Visible = false;

            this.neuSpread1_Sheet1.Columns[4].Label = "���";
            this.neuSpread1_Sheet1.Columns[4].Width = 80F;
            this.neuSpread1_Sheet1.Columns[5].Label = "��װ����";
            this.neuSpread1_Sheet1.Columns[6].Label = "��װ��λ";
            this.neuSpread1_Sheet1.Columns[7].Label = "��С��λ";

            this.neuSpread1_Sheet1.Columns[8].Label = "���ۼ�";
            this.neuSpread1_Sheet1.Columns[8].Visible = false;
            this.neuSpread1_Sheet1.Columns[9].Label = "ҩƷ���";
            this.neuSpread1_Sheet1.Columns[9].Visible = false;
            this.neuSpread1_Sheet1.Columns[10].Label = "ҩƷ����";
            this.neuSpread1_Sheet1.Columns[10].Visible = false;
            this.neuSpread1_Sheet1.Columns[11].Label = "ϵͳ���";
            this.neuSpread1_Sheet1.Columns[11].Visible = false;
            this.neuSpread1_Sheet1.Columns[12].Label = "�Ƿ�ͣ��";
            this.neuSpread1_Sheet1.Columns[12].Visible = false;
            this.neuSpread1_Sheet1.Columns[13].Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns[13].Visible = false;
            this.neuSpread1_Sheet1.Columns[14].Label = "�����";
            this.neuSpread1_Sheet1.Columns[14].Visible = false;
            this.neuSpread1_Sheet1.Columns[15].Label = "�Զ�����";
            this.neuSpread1_Sheet1.Columns[15].Visible = false;
            this.neuSpread1_Sheet1.Columns[16].Label = "ͨ����ƴ����";
            this.neuSpread1_Sheet1.Columns[16].Visible = false;
            this.neuSpread1_Sheet1.Columns[17].Label = "ͨ���������";
            this.neuSpread1_Sheet1.Columns[17].Visible = false;
            this.neuSpread1_Sheet1.Columns[18].Label = "ͨ�����Զ�����";
            this.neuSpread1_Sheet1.Columns[18].Visible = false;
        }

        #endregion

        /// <summary>
        /// ����ʵ���ȡ��Ӧ��DataRow
        /// </summary>
        /// <param name="drug">ҩƷ������Ϣʵ��</param>
        /// <returns>�ɹ����������DataRow ʧ�ܷ���null</returns>
        private DataRow GetRow(Item drug)
        {
            try
            {
                DataRow dr = this.dsPrescription.Tables[0].NewRow();

                #region DataRow���
                dr["ҩƷ����"] = drug.ID;
                dr["ҩƷ����"] = drug.Name;
                dr["ҩƷͨ����"] = drug.NameCollection.RegularName;
                dr["Ӣ����Ʒ��"] = drug.NameCollection.EnglishName;		//Ӣ����
                dr["���"] = drug.Specs;					//���
                dr["��װ����"] = drug.PackQty;
                dr["��װ��λ"] = drug.PackUnit;
                dr["��С��λ"] = drug.MinUnit;
                dr["���ۼ�"] = drug.PriceCollection.RetailPrice;
                dr["ҩƷ���"] = drug.Type.ID;
                dr["ҩƷ����"] = drug.Quality.ID;
                dr["ϵͳ���"] = drug.SysClass.ID;
                dr["�Ƿ�ͣ��"] = drug.IsStop;
                dr["ƴ����"] = drug.SpellCode;
                dr["�����"] = drug.WBCode;
                dr["�Զ�����"] = drug.UserCode;
                dr["ͨ����ƴ����"] = drug.NameCollection.SpellCode;
                dr["ͨ���������"] = drug.NameCollection.WBCode;
                dr["ͨ�����Զ�����"] = drug.NameCollection.UserCode;
                #endregion

                return dr;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʵ�����DataRowʱ�������� \n" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// �ɳ�Ʒ�����ڻ�ȡ��Ʒ��Ϣ
        /// </summary>
        /// <param name="drugCode">��Ʒ����</param>
        /// <returns>�ɹ�����Itemʵ�� ʧ�ܷ���null</returns>
        private Item GetItem(string drugCode)
        {
            foreach (Item info in this.alDrug)
            {
                if (info.ID == drugCode)
                    return info;
            }
            return null;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isClearDrug">�Ƿ������Ƽ���Ʒ��ʾ</param>
        public void Clear(bool isClearDrug)
        {
            //this.neuSpread2_Sheet1.Rows.Count = 0;
            //this.neuSpread1_Sheet1.Rows.Count = 0;
            //if (this.dsPrescription != null && this.dsPrescription.Tables[0].Rows.Count > 0)
            //{
            //    this.dsPrescription.Tables[0].Clear();
            //}
        }

        /// <summary>
        /// ����³�Ʒ
        /// </summary>
        /// <param name="item"></param>
        protected void AddItem(Item item)
        {
            if (item == null || item.ID == "")
                return;

            if (this.dsPrescription == null)
            {
                this.dsPrescription = new DataSet();
            }
            bool isNew = this.dsPrescription.Tables[0].Rows.Count == 0 ? true : false;

            DataRow findRow = this.dsPrescription.Tables[0].Rows.Find(item.ID);
            if (findRow != null)
            {
                MessageBox.Show(item.Name + " ��ά���ô��� �����ظ�ά��");
                return;
            }

            DataRow dr = this.GetRow(item);
            this.dsPrescription.Tables[0].Rows.Add(dr);
            if (isNew)
            {
                this.dvPrescription = new DataView(this.dsPrescription.Tables[0]);
                this.neuSpread1_Sheet1.DataSource = this.dvPrescription;
            }

            if (this.alDrug == null)
                this.alDrug = new ArrayList();
            this.alDrug.Add(item);
            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
        }

        public void AddItemDetail(Item item)
        {
            int i = this.neuSpread2_Sheet1.Rows.Count;
            this.neuSpread2_Sheet1.Rows.Add(i, 1);
            this.neuSpread2_Sheet1.Cells[i, 0].Text = item.Name;
            this.neuSpread2_Sheet1.Cells[i, 1].Text = item.Specs;
            this.neuSpread2_Sheet1.Cells[i, 3].Text = item.MinUnit;
            this.neuSpread2_Sheet1.Rows[i].Tag = item;
        }

        /// <summary>
        /// ��ȡ��Ʒ�б�
        /// </summary>
        public void Query()
        {
            this.GetPrescription();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            return this.SavePrescription();
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int Del()
        {
            try
            {
                return this.DelPrescription();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// ����һ����ϸ
        /// </summary>
        protected void Add()
        {
            this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.Rows.Count, 1);
            this.neuSpread2_Sheet1.ActiveColumnIndex = 0;
        }

        #region ���ƴ���
        /// <summary>
        /// ��ȡ��Ʒ���ƴ�����Ϣ
        /// </summary>
        /// <returns></returns>
        public int GetPrescription()
        {
            //this.Clear(true);
            this.dsPrescription.Tables[0].Clear();
            this.neuSpread2_Sheet1.Rows.Count = 0;
            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.alPrescription = this.preparationMgr.QueryPrescription();
            if (this.alPrescription == null)
            {
                MessageBox.Show("δ��ȷ��ȡ��Ʒ���ƴ�����Ϣ \n" + this.preparationMgr.Err);
                return -1;
            }
            DataRow dr;
            string privDrugCode = "";					//��һ����¼��Ʒ����
            ArrayList alTemp = new ArrayList();
            foreach (Neusoft.HISFC.Object.Preparation.Prescription info in this.alPrescription)
            {
                if (info.Drug.ID != privDrugCode)
                {
                    #region ��ȡ��Ʒ������Ϣ
                    info.Drug = this.itemMgr.GetItem(info.Drug.ID);
                    if (info.Drug == null)
                    {
                        MessageBox.Show("�� " + info.Drug.Name + " ��ȡҩƷ������Ϣʧ��\n" + this.itemMgr.Err);
                        return -1;
                    }
                    if (this.alDrug == null)
                    {
                        this.alDrug = new ArrayList();
                    }
                    this.alDrug.Add(info.Drug);
                    #endregion

                    dr = this.GetRow(info.Drug);
                    if (dr == null)
                        return -1;
                    this.dsPrescription.Tables[0].Rows.Add(dr);
                    privDrugCode = info.Drug.ID;
                }
            }
            if (this.dsPrescription.Tables[0].Rows.Count > 0)
            {
                this.dvPrescription = new DataView(this.dsPrescription.Tables[0]);
                this.neuSpread1_Sheet1.DataSource = this.dvPrescription;
            }
            return 1;
        }

        /// <summary>
        /// ���ƴ�����Ϣ ����ʾ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowPrescription()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return -1;

            string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            this.neuSpread2_Sheet1.Rows.Count = 0;
            //this.Clear(false);

            this.lbPrescription.Text = string.Format("{0}  ��Ʒ�������ݣ���׼�������Գ�Ʒ��1000Ϊ��׼����", this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text);

            List<Neusoft.HISFC.Object.Preparation.Prescription> al = this.preparationMgr.QueryPrescription(drugCode);
            if (al == null)
            {
                MessageBox.Show("��ȡ��ǰѡ���Ʒ�����ƴ�����Ϣ����\n" + drugCode);
                return -1;
            }
            foreach (Neusoft.HISFC.Object.Preparation.Prescription info in al)
            {
                int i = this.neuSpread2_Sheet1.Rows.Count;

                this.neuSpread2_Sheet1.Rows.Add(i, 1);
                this.neuSpread2_Sheet1.Cells[i, 0].Text = info.Material.Name;
                this.neuSpread2_Sheet1.Cells[i, 1].Text = info.Specs;
                this.neuSpread2_Sheet1.Cells[i, 2].Text = info.NormativeQty.ToString();
                this.neuSpread2_Sheet1.Cells[i, 3].Text = info.NormativeUnit;

                this.neuSpread2_Sheet1.Rows[i].Tag = info.Material;


            }
            return 1;
        }

        /// <summary>
        /// �������ƴ�����Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SavePrescription()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return 1;
            if (this.neuSpread2_Sheet1.Rows.Count <= 0)
                return 1;

            string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            DateTime sysTime = this.preparationMgr.GetDateTimeFromSysDateTime();

            Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction(Neusoft.NFC.Management.Connection.Instance);
            t.BeginTransaction();
            this.preparationMgr.SetTrans(t.Trans);

            try
            {
                if (this.preparationMgr.DelPrescription(drugCode) == -1)
                {
                    t.RollBack();
                    MessageBox.Show("ɾ�����ƴ�����Ϣ����" + this.preparationMgr.Err);
                    return -1;
                }
                Neusoft.HISFC.Object.Preparation.Prescription info = new Neusoft.HISFC.Object.Preparation.Prescription();
                Neusoft.HISFC.Object.Pharmacy.Item tempItem;
                for (int i = 0; i < this.neuSpread2_Sheet1.Rows.Count; i++)
                {
                    if (this.neuSpread2_Sheet1.Cells[i, 0].Text == "")
                        continue;

                    info = new Neusoft.HISFC.Object.Preparation.Prescription();
                    info.Drug = this.GetItem(drugCode);
                    tempItem = this.neuSpread2_Sheet1.Rows[i].Tag as Neusoft.HISFC.Object.Pharmacy.Item;
                    if (tempItem == null)
                    {
                        t.RollBack();
                        MessageBox.Show("����ת������");
                        return -1;
                    }
                    info.Material = tempItem;
                    info.NormativeQty = NConvert.ToDecimal(this.neuSpread2_Sheet1.Cells[i, 2].Text);
                    info.NormativeUnit = this.neuSpread2_Sheet1.Cells[i, 3].Text;
                    info.OperEnv.ID = this.preparationMgr.Operator.ID;
                    info.OperEnv.OperTime = sysTime;

                    if (this.preparationMgr.SetPrescription(info) == -1)
                    {
                        t.RollBack();
                        if (this.preparationMgr.DBErrCode == 1)
                            MessageBox.Show(info.Material.Name + "�����ظ����");
                        else
                            MessageBox.Show("����" + info.Drug.Name + "���ƴ�����Ϣʧ��" + this.preparationMgr.Err);
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                t.RollBack();
                MessageBox.Show(ex.Message);
            }
            t.Commit();
            MessageBox.Show("����ɹ�");
            return 1;
        }

        /// <summary>
        /// ɾ�����ƴ�����Ϣ
        /// </summary>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���-1</returns>
        public int DelPrescription()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return 1;

            string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            if (this.neuSpread1.ContainsFocus)
            {
                DialogResult rs = MessageBox.Show("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��\n ����ɾ��������ɾ���ó�Ʒ�������ƴ�����Ϣ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return 1;
                if (this.preparationMgr.DelPrescription(drugCode) == -1)
                {
                    MessageBox.Show("�Ե�ǰѡ���Ʒִ��ɾ������ʧ��\n" + this.preparationMgr.Err);
                    return -1;
                }
                DataRow dr = this.dsPrescription.Tables[0].Rows.Find(drugCode);
                if (dr != null)
                {
                    this.dsPrescription.Tables[0].Rows.Remove(dr);
                }
                this.ShowPrescription();
            }
            else if (this.neuSpread2.ContainsFocus)
            {
                DialogResult rs = MessageBox.Show("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return 1;

                if (this.neuSpread2_Sheet1.Rows.Count <= 0)
                    return 1;
                int iIndex = this.neuSpread2_Sheet1.ActiveRowIndex;
                Neusoft.HISFC.Object.Pharmacy.Item item = this.neuSpread2_Sheet1.Rows[iIndex].Tag as Neusoft.HISFC.Object.Pharmacy.Item;
                if (item == null)
                    return 1;
                if (this.preparationMgr.DelPrescription(drugCode, item.ID) == -1)
                {
                    MessageBox.Show("�Ե�ǰѡ�񴦷���¼����ɾ������ʧ��\n" + this.preparationMgr.Err);
                    return -1;
                }
                this.neuSpread2_Sheet1.Rows.Remove(iIndex, 1);
            }
            if (this.neuSpread2_Sheet1.Rows.Count <= 0)
            {
                DataRow dr = this.dsPrescription.Tables[0].Rows.Find(drugCode);
                if (dr != null)
                {
                    this.dsPrescription.Tables[0].Rows.Remove(dr);
                }
            }
            return 1;
        }
        #endregion

        #region �¼�
        private void neuToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == this.tbExit)
            {
                this.Close();
                return;
            }
            if (e.Button == this.tbSave)
            {
                this.Save();
                return;
            }
            if (e.Button == this.tbQuery)
            {
                this.Query();
                return;
            }
            if (e.Button == this.tbDel)
            {
                this.Del();
                return;
            }
            if (e.Button == this.tbAdd)
            {
                this.Add();
            }
        }

        private void ucQueryItem1_SelectItem(object sender, EventArgs e)
        {
            Item item = new Item();
            item = sender as Item;

            this.AddItem(item);

            this.lbPrescription.Text = string.Format("{0}  ��Ʒ�������ݣ���׼�������Գ�Ʒ��1000Ϊ��׼����", item.Name);

            this.Clear(false);
        }

        private void ucQueryItem2_SelectItem(object sender, EventArgs e)
        {
            Item item = new Item();
            item = sender as Item;

            this.AddItemDetail(item);
            this.Clear(false);
        }

        private void ucQueryItem1_TextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                //���ϸ��д���� ����ʵ�����¼�����ѡ�� ����һ������SelectChanged�¼�
                //				this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex,0,1,this.neuSpread1_Sheet1.Columns.Count);
                this.isFilter = false;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                //				this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex,0,1,this.neuSpread1_Sheet1.Columns.Count);
                this.isFilter = false;
                e.Handled = true;
            }
            else
            {
                this.isFilter = true;
            }
        }

        private void ucQueryItem1_TextChanged(object sender, EventArgs e)
        {
            if (!this.isFilter)
                return;

            if (this.dvPrescription != null && this.dvPrescription.Table.Rows.Count > 0)
            {
                string str = "%" + this.ucQueryItem1.TxtStr + "%";
                this.dvPrescription.RowFilter = string.Format(this.filterStr, str, str, str, str);

                this.neuSpread1_Sheet1.DataSource = this.dvPrescription;

                this.ShowPrescription();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //switch (keyData)
            //{
            //    case Keys.Enter:

            //        #region �С�����ת
            //        if (this.fpItem1.ContainsFocus)
            //        {
            //            if (this.neuSpread2_Sheet1.ActiveColumnIndex == 0)
            //                this.fpItem1.JumpColumn(2, false);
            //            else
            //                this.fpItem1.JumpColumn(0, true);
            //        }
            //        #endregion

            //        break;
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtRegulation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.nowDrugPrescription == this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text)
            {
                return;
            }
            else
            {
                this.nowDrugPrescription = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            }
            this.ShowPrescription();
        }

        private void fpItem1_SelectItem(object sender, EventArgs e)
        {
            //int i = this.neuSpread2_Sheet1.ActiveRowIndex;
            //Item item = sender as Item;
            //this.neuSpread2_Sheet1.Cells[i, 0].Text = item.Name;
            //this.neuSpread2_Sheet1.Cells[i, 1].Text = item.Specs;
            //this.neuSpread2_Sheet1.Cells[i, 2].Text = "1000";
            //this.neuSpread2_Sheet1.Cells[i, 3].Text = item.MinUnit;
            //this.neuSpread2_Sheet1.Rows[i].Tag = sender;
        }

        #endregion

        private void frmPPRManager_Load(object sender, EventArgs e)
        {
            string strWaitMessage = "";
            this.Init();
            this.InitDataSet();

            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm(strWaitMessage);
            Application.DoEvents();
            this.Query();
            Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
        }

       
    }
}