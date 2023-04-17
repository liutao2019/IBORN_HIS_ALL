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
    /// ִ�е����� ����Ŀά��
    /// </summary>
    public partial class ucUndrugItemSelect : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUndrugItemSelect()
        {
            InitializeComponent();
        }

        public delegate int AllItemHandle(string orderType, string sysClass, ArrayList alItems);
        public event AllItemHandle ItemAllUpdate;
        public delegate int OtherItemHandle(ArrayList alItems);
        public event ItemHandle ItemOtherInsert;
        public delegate int ItemHandle(ArrayList alItems);
        public event ItemHandle ItemInsert;

        private FS.FrameWork.Public.ObjectHelper orderTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ʿվ����
        /// </summary>
        private string nurseID;

        /// <summary>
        /// ��ʿվ����
        /// </summary>
        public string NurseID
        {
            set
            {
                this.nurseID = value;
                this.ucInputUndrug.NurseID = value;
            }
        }

        private string billNO;
        public string BillNO
        {
            set
            {
                this.billNO = value;
            }
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        private string myOrderType;

        /// <summary>
        /// ҽ������
        /// </summary>
        public string MyOrderType
        {
            get
            {
                return this.myOrderType;
            }
            set
            {
                if (this.myOrderType == value)
                {
                    return;
                }
                else
                {
                    this.myOrderType = value;
                    this.ucInputUndrug.MyOrderType = value;
                    for (int i = 0; i < this.cmbOrderType.alItems.Count; i++)
                    {
                        FS.HISFC.Models.Order.OrderType orderType = this.cmbOrderType.alItems[i] as FS.HISFC.Models.Order.OrderType;
                        if (orderType.ID == value)
                        {
                            this.cmbOrderType.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ҽ��������Ŀ���
        /// </summary>
        private string mySysClass;

        /// <summary>
        /// ҽ��������Ŀ���
        /// </summary>
        public string MySysClass
        {
            get
            {
                return this.mySysClass;
            }
            set
            {
                if (this.mySysClass == value)
                {
                    return;
                }
                else
                {
                    this.mySysClass = value;
                    this.ucInputUndrug.MySysClass = value;
                }
            }
        }

        public int Init()
        {
            if (this.GetOrderType() == -1)
            {
                return -1;
            }

            this.ucInputUndrug.AlOrderType = this.cmbOrderType.alItems;
            this.ucInputUndrug.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputUndrug_CatagoryChanged);
            if (this.ucInputUndrug.Init() == -1)
            {
                return -1;
            }
            this.ucInputUndrug.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputUndrug_SelectedItem);
            this.ucInputUndrug.OrderSelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputUndrug_SelectedItem);
            return 0;
        }

        void ucInputUndrug_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            FS.FrameWork.Models.NeuObject obj = sender as FS.FrameWork.Models.NeuObject;
            this.mySysClass = obj.ID;
        }

        void ucInputUndrug_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            FS.HISFC.Models.Base.Item item = sender as FS.HISFC.Models.Base.Item;
            this.AddItem(item);
        }

        private int GetOrderType()
        {
            this.cmbOrderType.AddItems(SOC.HISFC.BizProcess.Cache.Order.GetOrderType());
            this.orderTypeHelper.ArrayObject = SOC.HISFC.BizProcess.Cache.Order.GetOrderType();
            return 0;
        }

        protected void AddItem(FS.HISFC.Models.Base.Item item)
        {
            if (item == null)
            {
                return;
            }

            if (this.rdbSingle.Checked)
            {
                if (this.lvUndrugItems.Items.Count > 0)
                {
                    this.lvUndrugItems.Items.Clear();
                }
            }

            foreach (ListViewItem lstItem in this.lvUndrugItems.Items)
            {
                FS.HISFC.Models.Base.Item inItem = lstItem.Tag as FS.HISFC.Models.Base.Item;
                if (item.ID == inItem.ID)
                {
                    MessageBox.Show("����Ŀ�����:" + item.Name);
                    return;
                }
            }

            ListViewItem lvItem = new ListViewItem(item.Name);
            lvItem.Tag = item;
            lvItem.Checked = true;
            this.lvUndrugItems.Items.Add(lvItem);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.ucInputUndrug.Refresh();
            base.OnLoad(e);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.lvUndrugItems.Items.Count > 0)
            {
                this.lvUndrugItems.Items.Clear();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Check() == -1)
            {
                MessageBox.Show("����ʧ�ܣ�");
                this.FindForm().Hide();
                return;
            }

            if (this.rdbAll.Checked)//ȫ��
            {
                if (this.SaveAllItems() == -1)
                {
                    MessageBox.Show("����ʧ�ܣ�");
                }
                else
                {
                    MessageBox.Show("����ɹ���");
                }
            }
            else if (this.rdbOther.Checked)//ʣ��ȫ��
            {
                if (this.SaveOtherItems() == -1)
                {
                    MessageBox.Show("����ʧ�ܣ�");
                }
                else
                {
                    MessageBox.Show("����ɹ���");
                }
            }
            else
            {
                if (this.SaveItems() == -1)
                {
                    MessageBox.Show("����ʧ�ܣ�");
                }
                else
                {
                    MessageBox.Show("����ɹ���");

                }
            }
            if (this.lvUndrugItems.Items.Count > 0)
            {
                this.lvUndrugItems.Items.Clear();
            }
            this.FindForm().Hide();
            return;
        }

        /// <summary>
        /// ѡ��ȫ������
        /// </summary>
        /// <returns></returns>
        private int SaveAllItems()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //ִ�е������
            FS.HISFC.BizLogic.Order.ExecBill manager = new FS.HISFC.BizLogic.Order.ExecBill();
            if (manager.UpdateExecBillAllItem(this.nurseID, this.billNO, this.myOrderType, this.mySysClass) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            //DataSet����
            DataTable dt = this.ucInputUndrug.DsUndrugItem.Tables[this.myOrderType];
            if (dt == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            ArrayList alItems = new ArrayList();
            string filter = "������='" + this.mySysClass + "'";
            foreach (DataRow row in dt.Select(filter))
            {
                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                order.ID = this.billNO;
                order.Memo = "2";
                order.OrderType.ID = this.myOrderType;
                order.OrderType.Name = this.orderTypeHelper.GetName(this.myOrderType);
                order.Item.SysClass.ID = this.mySysClass;
                order.Item.ID = row["����"].ToString();
                order.Item.Name = row["����"].ToString();
                alItems.Add(order);
                row.Delete();
            }

            //farpoint����
            if (this.ItemAllUpdate != null)
            {
                if (this.ItemAllUpdate(this.myOrderType, this.mySysClass, alItems) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }

        private int SaveOtherItems()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //ִ�е������
            FS.HISFC.BizLogic.Order.ExecBill manager = new FS.HISFC.BizLogic.Order.ExecBill();
            if (manager.InsertExecBillOtherItem(this.nurseID, this.billNO, this.myOrderType, this.mySysClass) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            //DataSet����
            DataTable dt = this.ucInputUndrug.DsUndrugItem.Tables[this.myOrderType];
            if (dt == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            ArrayList alItems = new ArrayList();
            string filter = "������='" + this.mySysClass + "'";
            foreach (DataRow row in dt.Select(filter))
            {
                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                order.ID = this.billNO;
                order.Memo = "2";
                order.OrderType.ID = this.myOrderType;
                order.OrderType.Name = this.orderTypeHelper.GetName(this.myOrderType);
                order.Item.SysClass.ID = this.mySysClass;
                order.Item.ID = row["����"].ToString();
                order.Item.Name = row["����"].ToString();
                alItems.Add(order);
                row.Delete();
            }

            //farpoint����
            if (this.ItemOtherInsert != null)
            {
                if (this.ItemOtherInsert(alItems) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }

        /// <summary>
        /// ѡ��ѡ���ѡ�ı���
        /// </summary>
        /// <returns></returns>
        private int SaveItems()
        {
            ArrayList alItem = this.GetItems();
            if (alItem == null)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //ִ�е������
            FS.HISFC.BizLogic.Order.ExecBill manager = new FS.HISFC.BizLogic.Order.ExecBill();
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alItem)
            {
                if (manager.UpdateExecBillItem(this.nurseID, this.billNO, this.myOrderType, this.mySysClass, order.Item.ID, order.Item.Name) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            DataTable dt = this.ucInputUndrug.DsUndrugItem.Tables[this.myOrderType];
            if (dt == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            //datase����
            string filter = "������='" + this.mySysClass + "' and ���� in (";
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alItem)
            {
                filter += "'" + order.Item.ID + "',";
            }
            filter = filter.Substring(0, filter.Length - 1);
            filter += ")";
            foreach (DataRow row in dt.Select(filter))
            {
                row.Delete();
            }

            //farpoint����
            if (this.ItemInsert != null)
            {
                if (this.ItemInsert(alItem) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.lvUndrugItems.Items.Count > 0)
            {
                this.lvUndrugItems.Items.Clear();
            }
            this.FindForm().Hide();
        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbAll.Checked || this.rdbOther.Checked)
            {
                this.ucInputUndrug.txtItemCode.Enabled = false;
                if (this.lvUndrugItems.Items.Count > 0)
                {
                    this.lvUndrugItems.Items.Clear();
                }
            }
            else
            {
                this.ucInputUndrug.txtItemCode.Enabled = true;
            }
        }

        private void rdbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbAll.Checked || this.rdbOther.Checked)
            {
                this.ucInputUndrug.txtItemCode.Enabled = false;
                if (this.lvUndrugItems.Items.Count > 0)
                {
                    this.lvUndrugItems.Items.Clear();
                }
            }
            else
            {
                this.ucInputUndrug.txtItemCode.Enabled = true;
            }
        }

        private int Check()
        {
            if (this.nurseID == null || this.nurseID == string.Empty)
            {
                return -1;
            }

            if (this.billNO == null || this.billNO == string.Empty)
            {
                return -1;
            }

            if (this.myOrderType == null || this.myOrderType == string.Empty)
            {
                return -1;
            }

            if (this.myOrderType == null || this.mySysClass == string.Empty)
            {
                return -1;
            }

            return 0;
        }

        private ArrayList GetItems()
        {
            if (this.cmbOrderType.SelectedItem == null)
            {
                return null;
            }

            if (this.mySysClass == null || this.mySysClass == string.Empty)
            {
                return null;
            }

            if (this.lvUndrugItems.CheckedItems.Count > 0)
            {
                ArrayList alItem = new ArrayList();
                foreach (ListViewItem item in this.lvUndrugItems.CheckedItems)
                {
                    FS.FrameWork.Models.NeuObject obj = item.Tag as FS.FrameWork.Models.NeuObject;
                    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                    order.ID = this.billNO;
                    order.Memo = "2";
                    order.OrderType.ID = this.myOrderType;
                    order.OrderType.Name = this.orderTypeHelper.GetName(this.myOrderType);
                    order.Item.SysClass.ID = this.mySysClass;
                    order.Item.ID = obj.ID;
                    order.Item.Name = obj.Name;
                    alItem.Add(order);
                }
                return alItem;
            }
            else
            {
                return null;
            }
        }
    }
}
