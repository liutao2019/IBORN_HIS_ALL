using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.UFC.Order.OutPatient.Controls
{
    /// <summary>
    /// [��������: ��ҩҽ������]<br></br>
    /// [�� �� ��: dorian]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// <�޸ļ�¼
    ///  />
    /// </summary>
    public partial class ucHerbalOrder : UserControl
    {
        public ucHerbalOrder()
        {
            InitializeComponent();
        }

        public ucHerbalOrder(bool isClinic, Neusoft.HISFC.Object.Order.EnumType orderType, string deptCode)
            : this()
        {
            this.isClinic = isClinic;
            this.DeptCode = deptCode;
            this.OrderType = orderType;
            this.fpEnter1_Sheet1.Rows.Count = 0;
            this.fpEnter1_Sheet1.Rows.Add(0, 1);

            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnDel.Click += new EventHandler(btnDel_Click);
            this.Load += new EventHandler(ucHerbalOrder_Load);
        }

        #region ����
        /// <summary>
        /// �Ƿ�����ʹ��
        /// </summary>
        private bool isClinic = false;

        /// <summary>
        /// �Ƿ�����ʹ��
        /// </summary>
        public bool IsClinic
        {
            set
            {
                this.isClinic = value;
            }
        }

        /// <summary>
        /// ҽ����� 0 ���� 1 ����
        /// </summary>
        private Neusoft.HISFC.Object.Order.EnumType orderType;

        /// <summary>
        /// ҽ����� 0 ���� 1 ����
        /// </summary>
        public Neusoft.HISFC.Object.Order.EnumType OrderType
        {
            set
            {
                this.orderType = value;
                if (this.alLong == null || this.alShort == null)
                {
                    try
                    {
                        this.DataInit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (value == Neusoft.HISFC.Object.Order.EnumType.LONG)
                {
                    this.cmbOrderType.DataSource = alLong;
                    this.cmbOrderType.DisplayMember = "Name";
                    this.cmbOrderType.ValueMember = "ID";
                    this.orderTypeHelper.ArrayObject = this.alLong;
                }
                else
                {
                    this.cmbOrderType.DataSource = alShort;
                    this.cmbOrderType.DisplayMember = "Name";
                    this.cmbOrderType.ValueMember = "ID";
                    this.orderTypeHelper.ArrayObject = this.alShort;
                }
            }
        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        private string deptCode = "";

        /// <summary>
        /// ����ҽ�����ڿ���
        /// </summary>
        public string DeptCode
        {
            set
            {
                this.deptCode = value;
            }
        }

        /// <summary>
        /// ������Ĳ�ҩҽ����Ϣ
        /// </summary>
        ArrayList alOrder = new ArrayList();

        /// <summary>
        /// ������Ĳ�ҩҽ����Ϣ
        /// </summary>
        public ArrayList AlOrder
        {
            get
            {
                if (this.alOrder == null)
                    this.alOrder = new ArrayList();
                return this.alOrder;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.RADT.PatientInfo patient = null;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.RADT.PatientInfo Patient
        {
            set
            {
                if (value == null)
                {
                    MessageBox.Show("������Ϣ��ֵ����");
                    return;
                }
                this.patient = value;
            }
        }
        #endregion

        #region �����
        /// <summary>
        /// ����ҽ������
        /// </summary>
        ArrayList alLong = null;

        /// <summary>
        /// ��ʱҽ������
        /// </summary>
        ArrayList alShort = null;

        Neusoft.NFC.Public.ObjectHelper orderTypeHelper = new Neusoft.NFC.Public.ObjectHelper();

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        ArrayList alItem = null;

        /// <summary>
        /// �÷���Ϣ
        /// </summary>
        ArrayList alUsage = null;

        /// <summary>
        /// ����Ƶ��
        /// </summary>
        Neusoft.NFC.Public.ObjectHelper frequencyHelper = new Neusoft.NFC.Public.ObjectHelper();

        /// <summary>
        /// �����ҩ��ʽ
        /// </summary>
        Neusoft.HISFC.Integrate.Pharmacy itemManager = new Neusoft.HISFC.Integrate.Pharmacy();
        #endregion

        /// <summary>
        /// ���ݼ��س�ʼ��
        /// </summary>
        protected void DataInit()
        {
            #region ҽ��������
            Neusoft.HISFC.Integrate.Manager integrateManager = new Neusoft.HISFC.Integrate.Manager();

            ArrayList alOrderType = (integrateManager.QueryOrderTypeList());//ҽ������
            foreach (Neusoft.HISFC.Object.Order.OrderType obj in alOrderType)
            {
                if (obj.IsDecompose)
                {
                    if (alLong == null)
                        alLong = new ArrayList();
                    alLong.Add(obj);
                }
                else
                {
                    if (alShort == null)
                        alShort = new ArrayList();
                    alShort.Add(obj);
                }
            }
            #endregion

            #region Ƶ�μ���
            ArrayList List = integrateManager.QuereyFrequencyList();
            this.cmbFrequency.DataSource = List;
            this.cmbFrequency.DisplayMember = "ID";
            this.cmbFrequency.ValueMember = "ID";
            this.frequencyHelper.ArrayObject = List;
            #endregion

            #region ��ҩ��ʽ
            ArrayList memoAl = new ArrayList();
            Neusoft.NFC.Object.NeuObject obj1 = new Neusoft.NFC.Object.NeuObject();
            obj1.ID = "0";
            obj1.Name = "�Լ�";
            memoAl.Add(obj1);
            obj1 = new Neusoft.NFC.Object.NeuObject();
            obj1.ID = "1";
            obj1.Name = "����";
            memoAl.Add(obj1);
            obj1 = new Neusoft.NFC.Object.NeuObject();
            obj1.ID = "2";
            obj1.Name = "����";
            memoAl.Add(obj1);
            obj1 = new Neusoft.NFC.Object.NeuObject();
            obj1.ID = "3";
            obj1.Name = "������";
            memoAl.Add(obj1);
            this.cmbMemo.DataSource = memoAl;
            this.cmbMemo.DisplayMember = "Name";
            this.cmbMemo.ValueMember = "ID";
            #endregion

            #region ��ҩ��Ŀ
            if (this.alItem == null)
                this.alItem = new ArrayList();
            this.alItem = this.itemManager.QueryItemAvailableList(this.deptCode, "C");
            if (this.alItem == null)
            {
                MessageBox.Show("��ȡ��ҩ��Ŀ�б����");
                return;
            }
            #endregion

            #region �÷�
            this.alUsage = Neusoft.UFC.Order.Classes.Function.HelperUsage.ArrayObject;
            if (this.alUsage == null)
            {
                MessageBox.Show("��ȡ�÷��б����!");
                return;
            }
            #endregion

            this.fpEnter1.SetWidthAndHeight(150, 100);
            this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)ColumnSet.ColTradeName, false);
            this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)ColumnSet.ColTradeName, this.alItem);

            this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)ColumnSet.ColUsage, this.alUsage);

            this.fpEnter1.ShowListWhenOfFocus = true;
            this.fpEnter1.SetItem += new Neusoft.NFC.Interface.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            this.fpEnter1.KeyEnter += new Neusoft.NFC.Interface.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);

            FarPoint.Win.Spread.InputMap im;

            im = this.fpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// ���б��ȡ��ѡ����Ŀ
        /// </summary>
        /// <returns>�ɹ�����1 �����أ�1</returns>
        protected int GetSelectItem()
        {
            int currentRow = this.fpEnter1_Sheet1.ActiveRowIndex;
            if (currentRow < 0) return 0;
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
            {
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)ColumnSet.ColTradeName);
                Neusoft.NFC.Object.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                this.SetSelectItem(item);
                return 0;
            }
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColUsage)
            {
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)ColumnSet.ColUsage);
                Neusoft.NFC.Object.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                this.SetSelectItem(item);
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// �������б�����ѡ�����Ŀ
        /// </summary>
        /// <param name="obj">�ɵ����б�����ѡ�����Ŀ</param>
        /// <returns>�ɹ�����1 �����أ�1</returns>
        protected int SetSelectItem(Neusoft.NFC.Object.NeuObject obj)
        {
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
            {
                Neusoft.HISFC.Object.Pharmacy.Item item = this.itemManager.GetItem(obj.ID);
                if (item == null)
                {
                    MessageBox.Show("��ȡҩƷ��Ϣʧ��!" + this.itemManager.Err);
                    return -1;
                }
                item.User02 = obj.User02;		//ȡҩҩ������
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColTradeName].Text = obj.Name;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColSpecs].Text = item.Specs;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString();
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUnit].Text = item.MinUnit;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Text = item.Usage.Name;
                this.fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].Tag = item;

                this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColNum;
                return 1;
            }
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColUsage)
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Text = obj.Name;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Tag = obj;

                if (this.fpEnter1_Sheet1.ActiveRowIndex == this.fpEnter1_Sheet1.Rows.Count - 1)
                {
                    this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.Rows.Count, 1);
                    this.fpEnter1_Sheet1.ActiveRowIndex = this.fpEnter1_Sheet1.Rows.Count - 1;
                }
                else
                {
                    this.fpEnter1_Sheet1.ActiveRowIndex = this.fpEnter1_Sheet1.ActiveRowIndex + 1;
                }
                this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColTradeName;
                return 1;
            }
            return 1;
        }
        /// <summary>
        /// ���������ʾ
        /// </summary>
        public void Clear()
        {
            this.alOrder = new ArrayList();
            this.fpEnter1_Sheet1.Rows.Count = 0;
            this.fpEnter1_Sheet1.Rows.Count = 1;
            this.txtNum.Text = "";
            this.dtEnd.Checked = false;
        }

        /// <summary>
        /// ҽ����Ч�Լ��
        /// </summary>
        /// <returns>�޴��󷵻�1 �����أ�1</returns>
        protected int Valid()
        {
            if (this.patient == null)
            {
                MessageBox.Show("������Ϣδ��ȷ��ֵ");
                return -1;
            }
            if (this.cmbOrderType.Text == "")
            {
                MessageBox.Show("��ѡ��ҽ������");
                this.cmbOrderType.Select();
                this.cmbOrderType.Focus();
                return -1;
            }
            if (this.cmbFrequency.Text == "")
            {
                MessageBox.Show("��ѡ�񱾼���ҩ��ҩƵ��");
                this.cmbFrequency.Select();
                this.cmbFrequency.Focus();
                return -1;
            }
            if (this.txtNum.Text == "")
            {
                MessageBox.Show("��ѡ���ҩ����");
                this.txtNum.Select();
                this.txtNum.Focus();
                return -1;
            }
            if (Neusoft.NFC.Function.NConvert.ToInt32(this.txtNum.Text) == 0)
            {
                MessageBox.Show("����ֻ��Ϊ����0������");
                return -1;
            }
            if (this.cmbMemo.Text == "")
            {
                MessageBox.Show("��ѡ�񱾼�ҩ�ü�ҩ��ʽ");
                this.cmbMemo.Select();
                this.cmbMemo.Focus();
                return -1;
            }
            if (this.dtEnd.Checked)
            {
                if (Neusoft.NFC.Function.NConvert.ToDateTime(this.dtBegin.Text) >= Neusoft.NFC.Function.NConvert.ToDateTime(this.dtEnd.Text))
                {
                    MessageBox.Show("ҽ��ֹͣʱ�䲻�ܴ��ڵ���ҽ����ʼʱ��");
                    return -1;
                }
            }
            for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            {
                if (this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColTradeName].Text == "")
                    continue;
                if (this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text == "")
                {
                    MessageBox.Show("�������" + (i + 1).ToString() + "�в�ҩÿ����");
                    this.fpEnter1_Sheet1.ActiveRowIndex = i;
                    return -1;
                }
                if (this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text == "")
                {
                    MessageBox.Show("�������" + (i + 1).ToString() + "�в�ҩ�÷�");
                    this.fpEnter1_Sheet1.ActiveRowIndex = i;
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// ҽ������
        /// </summary>
        protected int Save()
        {
            if (this.Valid() == -1)
                return -1;
            Neusoft.HISFC.Management.Order.Order orderManager = new Neusoft.HISFC.Management.Order.Order();
            Neusoft.HISFC.Object.Order.Inpatient.Order order;
            string comboID = "";
            try
            {
                comboID = orderManager.GetNewOrderComboID();//�����Ϻ�;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ȡҽ����Ϻų���" + ex.Message);
                return -1;
            }
            Neusoft.NFC.Object.NeuObject usageObj = null;
            for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            {
                order = new Neusoft.HISFC.Object.Order.Inpatient.Order();
                order.Item = this.fpEnter1_Sheet1.Rows[i].Tag as Neusoft.HISFC.Object.Pharmacy.Item;
                if (order.Item == null)
                    continue;
                //������Ϣ
                order.Patient = this.patient;
                //ҽ����Ϻ�
                order.Combo.ID = comboID;
                //ҽ������
                order.OrderType = this.orderTypeHelper.GetObjectFromID(this.cmbOrderType.SelectedValue.ToString()) as Neusoft.HISFC.Object.Order.OrderType;
                //�÷�
                usageObj = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Tag as Neusoft.NFC.Object.NeuObject;
                order.Usage.ID = usageObj.ID;
                order.Usage.Name = usageObj.Name;

                //����
                order.HerbalQty = Neusoft.NFC.Function.NConvert.ToInt32(this.txtNum.Text);
                //��ҩ��ʽ
                order.Memo = this.cmbMemo.Text;
                //Ƶ��
                order.Frequency = this.frequencyHelper.GetObjectFromID(this.cmbFrequency.SelectedValue.ToString()) as Neusoft.HISFC.Object.Order.Frequency;
                //ÿ����
                if (this.orderType == Neusoft.HISFC.Object.Order.EnumType.LONG)
                {
                    order.DoseOnce = Neusoft.NFC.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text);
                }
                else
                {
                    order.Qty = Neusoft.NFC.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text);
                }
                order.BeginTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.dtBegin.Text);
                if (this.dtEnd.Checked)
                    order.EndTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.dtEnd.Text);
                //ȡҩҩ��
                order.StockDept.ID = order.Item.User02;

                this.alOrder.Add(order);
            }
            return 1;
        }


        #region �¼�
        private void ucHerbalOrder_Load(object sender, EventArgs e)
        {
            this.fpEnter1.Select();
            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColTradeName;
        }


        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
                {
                    if (this.GetSelectItem() == -1)
                    {
                        MessageBox.Show("���б��ȡ��ѡ����Ŀ����");
                        return -1;
                    }
                    return 1;
                }
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColNum)
                {
                    this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColUsage;
                    return 1;
                }
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColUsage)
                {
                    if (this.GetSelectItem() == -1)
                    {
                        MessageBox.Show("���б��ȡ��ѡ����Ŀ����");
                        return -1;
                    }
                }
            }
            return 0;
        }

        private int fpEnter1_SetItem(Neusoft.NFC.Object.NeuObject obj)
        {
            if (this.SetSelectItem(obj) == -1)
            {
                MessageBox.Show("������ѡ����Ŀʧ��");
            }
            return 0;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save() == 1)
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnDel_Click(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.RemoveRows(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
                this.fpEnter1.SetAllListBoxUnvisible();
            }
        }
        #endregion

        #region ������
        private enum ColumnSet
        {
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// �۸�
            /// </summary>
            ColPrice,
            /// <summary>
            /// ����
            /// </summary>
            ColNum,
            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// �÷�
            /// </summary>
            ColUsage
        }
        #endregion

    }
}
