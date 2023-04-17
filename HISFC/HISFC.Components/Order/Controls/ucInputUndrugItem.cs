using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    ///  ִ�е����� ����Ŀά��
    /// </summary>
    public partial class ucInputUndrugItem : FS.HISFC.Components.Common.Controls.ucInputItem
    {
        public ucInputUndrugItem()
        {
            InitializeComponent();
        }

        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler OrderSelectedItem;

        private ArrayList alOrderType;
        public ArrayList AlOrderType
        {
            get
            {
                return this.alOrderType;
            }
            set
            {
                this.alOrderType = value;
            }
        }

        //ҽ������
        private string myOrderType;
        public string MyOrderType
        {
            get
            {
                return this.myOrderType;
            }
            set
            {
                this.myOrderType = value;
            }
        }

        //ҽ��������Ŀ

        private string mySysClass = "";
        public string MySysClass
        {
            get
            {
                return this.mySysClass;
            }
            set
            {
                if (this.mySysClass == value && this.cmbCategory.SelectedItem != null && this.cmbCategory.SelectedItem.ID == value)
                {
                    return;
                }
                else
                {
                    this.mySysClass = value;
                    for (int i = 0; i < this.cmbCategory.alItems.Count; i++)
                    {
                        FS.FrameWork.Models.NeuObject obj = this.cmbCategory.alItems[i] as FS.FrameWork.Models.NeuObject;
                        if (obj.ID == value)
                        {
                            this.cmbCategory.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private string nurseID;
        public String NurseID
        {
            get
            {
                return this.nurseID;
            }
            set
            {
                this.nurseID = value;
            }
        }

        private DataSet dsUndrugItem;
        public DataSet DsUndrugItem
        {
            get
            {
                return this.dsUndrugItem;
            }
            set
            {
                this.dsUndrugItem = value;
            }
        }

        public new int Init()
        {
            if (this.GetUndrugDS() == -1)
            {
                return -1;
            }

            try
            {
                if (this.fpItemList.Sheets.Count <= 0)
                    this.fpItemList.Sheets.Add(new FarPoint.Win.Spread.SheetView());
                AddCategory();//������
                this.InputType = 0;//Ĭ��ƴ��
                #region {46983F5B-E184-4b8b-B819-AA1C34993F1B}
                this.initFPdetail(); 
                #endregion
                fpItemList.Sheets[0].DataAutoCellTypes = false;
                fpItemList.Sheets[0].DataAutoSizeColumns = false;
                frmShowItem.AddControl(fpItemList);
                frmShowItem.Owner = this.FindForm();
                frmShowItem.Size = new Size(0, 0);
                frmShowItem.Show();
                frmShowItem.Hide();
                fpItemList.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                frmShowItem.Closing += new CancelEventHandler(frmItemList_Closing);
                fpItemList.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
                fpItemList.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpItemList_CellDoubleClick);
                this.txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
                this.txtItemCode.Enter += new EventHandler(txtItemCode_Enter);
                this.txtItemCode.Leave += new EventHandler(txtItemCode_Leave);
                //this.InputType = FS.FrameWork.WinForms.Classes.Function.GetInputType();

                //this.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputUndrugItem_SelectedItem);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        //void ucInputUndrugItem_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        //{
        //    frmShowItem.Hide();
        //    if (this.SelectedItem != null)
        //        this.SelectedItem(sender);
        //}


        //��ȡ��ҩƷ��Ŀ

        public int GetUndrugDS()
        {
            if (this.alOrderType == null || this.alOrderType.Count == 0)
            {
                MessageBox.Show("��ȡҽ��������");
                return -1;
            }

            this.dsUndrugItem = new DataSet();

            foreach (FS.HISFC.Models.Order.OrderType orderType in this.alOrderType)
            {
                DataSet ds = new DataSet();
                if (CacheManager.FeeIntegrate.QueryItemOutExecBill(this.nurseID, orderType.ID, "1", ref ds) == -1)
                {
                    MessageBox.Show("��ȡ��ҩƷ��Ŀ����!");
                    return -1;
                }

                if (ds == null || ds.Tables.Count == 0)
                {
                    MessageBox.Show("��ȡ��ҩƷ��Ŀ����!");
                    return -1;
                }
                else
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.TableName = orderType.ID;
                    this.dsUndrugItem.Tables.Add(dt);
                }
            }
            return 0;
        }

        protected override int AddCategory()
        {
            //�ɴ˻�ȡ��Ŀ��� ��ҩ��������������ҽ����
            ArrayList arrItemTypes = FS.HISFC.Models.Base.SysClassEnumService.List();

            if (arrItemTypes != null)
            {
                FS.FrameWork.Models.NeuObject o = new FS.FrameWork.Models.NeuObject();
                o.Name = "ȫ��";
                arrItemTypes.Insert(0, o);
                this.cmbCategory.AddItems(arrItemTypes);
            }
            else
            {
                MessageBox.Show("������ʧ�ܣ������²�����");
                return -1;
            }
            this.cmbCategory.Text = "ȫ��";
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            return 0;
        }

        protected override void  ChangeItem()
        {
            try
            {
                this.myShowList(); //��ʾ�б�

                string sCategory = " and ������ = '" + this.cmbCategory.Tag + "'";
                if (this.cmbCategory.Text == "ȫ��")
                {
                    //��Ϊȫ�������������ܲ�ͬ�����Բ�������д,by huangxw
                    //sCategory =string.Empty;
                    sCategory = string.Empty;
                    foreach (FS.FrameWork.Models.NeuObject obj in this.cmbCategory.alItems)
                    {
                        if (obj.Name != "ȫ��")
                            sCategory = sCategory + " or ������ = '" + obj.ID + "'";
                    }
                    if (sCategory != string.Empty)
                    {
                        sCategory = sCategory.Substring(3);//ȥ����һ��or
                        sCategory = " and (" + sCategory + ")";
                    }
                }
                string sInput = string.Empty;
                //ȡ������
                string[] spChar = new string[] { "@", "#", "$", "%", "^", "&", "[", "]", "|" };
                string queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtItemCode.Text.Trim(), spChar);
                queryCode = queryCode.Replace("*", "[*]");

                //�����Ƿ�ȷ���ң������Ƿ����ģ����ѯ

                if (this.frmShowItem.IsReal == false)
                {
                    queryCode = '%' + FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode) + '%';
                }
                else
                {
                    queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode) + '%';
                }
                if (queryCode == "%%")
                {
                    queryCode = "%";
                }
                //
                sInput = "(ƴ���� LIKE '{0}' or " + "ͨ����ƴ���� LIKE '{0}' or ����� LIKE '{0}' or " + "ͨ��������� LIKE '{0}' or �Զ����� LIKE '{0}' or " + "ͨ�����Զ����� LIKE '{0}' or " + "Ӣ����Ʒ�� LIKE '{0}' or " + "���� LIKE '{0}')";
                //
                sInput = string.Format(sInput, queryCode);

                sInput = sInput + sCategory;
                //����
                this.MyDataView.RowFilter = sInput;

                if (this.IsListShowAlways)
                {
                    fpItemList.Sheets[0].DataSource = MyDataView;
                }
                fpItemList.Sheets[0].ActiveRowIndex = 0;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        protected override void txtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (fpItemList.Sheets[0].ActiveRowIndex > 9)
                {
                    fpItemList.SetViewportTopRow(0, fpItemList.Sheets[0].ActiveRowIndex - 9);
                }

                if (e.KeyCode == Keys.Up)
                {
                    fpItemList.Sheets[0].ActiveRowIndex--;
                    fpItemList.Sheets[0].AddSelection(fpItemList.Sheets[0].ActiveRowIndex, 0, 1, 1);
                    fpItemList.Focus();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    fpItemList.Sheets[0].ActiveRowIndex++;
                    fpItemList.Sheets[0].AddSelection(fpItemList.Sheets[0].ActiveRowIndex, 0, 1, 1);
                    fpItemList.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (
                        //fpItemList.Sheets[0].Rows.Count > 0 
                        //&& fpItemList.Sheets[0].ActiveRowIndex >= 0
                        //&& 
                        this.fpItemList.Visible
                        )
                    {
                        mySelectedItem();
                    }
                }
                else if (e.KeyCode == Keys.F3)//��ʾѡ����Ŀ����
                {
                    if (this.bIsListShowAlways == false)
                    {
                        if (this.frmShowItem != null) this.frmShowItem.Hide();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    frmShowItem.Hide();
                }
            }
            catch { }
        }

        protected new void mySelectedItem()
        {
            if (this.bIsListShowAlways == false)
            {
                if (this.frmShowItem != null)
                {
                    this.frmShowItem.Hide();
                }
            }

            int columnIndex = 0;
            for (int j = 0; j < this.fpItemList.Sheets[0].ColumnCount; j++)
            {
                if (this.fpItemList.Sheets[0].ColumnHeader.Columns[j].Label == "ִ�п���")
                {
                    columnIndex = j;
                    break;
                }

            }

            FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
            item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
            item.ID = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 0].Text;
            item.Name = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 1].Text;
            item.Specs = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 3].Text;
            item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 4].Text);
            item.PriceUnit = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 5].Text;
            item.SysClass.ID = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 8].Text;
            item.SpellCode = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 9].Text;
            item.WBCode = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 10].Text;
            item.UserCode = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 11].Text;

            this.myFeeItem = item;

            this.txtItemName.Text = this.myFeeItem.Name;
            this.txtItemCode.Text = string.Empty;
            frmShowItem.Hide();
            if (OrderSelectedItem != null)
                OrderSelectedItem(this.FeeItem);
            return;
        }

        public override void Refresh()
        {
            this.AddItem();
        }

        public override void AddItem()
        {
            // [2007/02/08 ��ΰ��]
            // �ؼ���ͬ������,��������
            try
            {
                System.Threading.Monitor.Enter(this);
                if (this.bIsListShowAlways == false)
                {
                    this.txtItemCode.Enabled = false;
                }
                RefreshFP();
                this.txtItemCode.Text = "";
                this.txtItemCode.Enabled = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                System.Threading.Monitor.Exit(this);
            }
        }

        public override void RefreshFP()
        {
            if (this.MyOrderType == null)
            {
                MessageBox.Show("��ѡ��ҽ�����ͣ�");
                return;
            }

            if (this.dsUndrugItem == null || this.dsUndrugItem.Tables.Count == 0)
            {
                MessageBox.Show("��ȡ��ҩƷ��Ŀ����");
                return;
            }

            try
            {
                MyDataView = new DataView(this.dsUndrugItem.Tables[MyOrderType]);
                fpItemList.Sheets[0].DataSource = MyDataView;
                if (this.IsListShowAlways == false)
                {
                    frmShowItem.DataView = MyDataView;
                    frmShowItem.RefreshFP();
                }
                this.fpItemList.Sheets[0].Columns[0].Visible = false;
                this.fpItemList.Sheets[0].Columns[2].Width = 50;
                this.fpItemList.Sheets[0].Columns[3].Width = 90;
                this.fpItemList.Sheets[0].Columns[4].Width = 40;
                this.fpItemList.Sheets[0].Columns[5].Width = 30;
                this.fpItemList.Sheets[0].Columns[7].Visible = false;
                this.fpItemList.Sheets[0].Columns[8].Visible = false;
                this.fpItemList.Sheets[0].Columns[9].Visible = false;
                this.fpItemList.Sheets[0].Columns[10].Visible = false;
                this.fpItemList.Sheets[0].Columns[13].Visible = false;
                this.fpItemList.Sheets[0].Columns[14].Visible = false;
                this.fpItemList.Sheets[0].Columns[15].Visible = false;
                this.fpItemList.Sheets[0].Columns[16].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("��ȡ��ҩƷ��Ŀ����:" + err.ToString());
                return;
            }
        }

        protected new void fpItemList_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e != null)
            {
                mySelectedItem();
            }
        }
    }
}
