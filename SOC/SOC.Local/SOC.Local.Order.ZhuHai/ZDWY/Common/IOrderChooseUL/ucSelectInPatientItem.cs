using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IOrderChooseUL;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IOrderChooseUL
{
    public partial class ucSelectInPatientItem : UserControl
    {
        FS.HISFC.BizLogic.Manager.UndrugztManager undrugztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

        private List<FS.HISFC.Models.Fee.Item.Undrug> lstUndrugzt = new List<FS.HISFC.Models.Fee.Item.Undrug>();

        private ArrayList alSysClass = new ArrayList();

        public ucSelectInPatientItem()
        {
            InitializeComponent();
            this.init();
        }

        private void init()
        {
            undrugztManager.QueryAllValidItemzt(ref lstUndrugzt);
            this.tcItemPanel.TabPages.Clear();
            this.alSysClass.Clear();
            this.lstbox.Items.Clear();
            foreach (FS.HISFC.Models.Fee.Item.Undrug undrugzt in lstUndrugzt)
            {
                if (!"1".Equals(undrugzt.ValidState) || string.IsNullOrEmpty(undrugzt.Memo) || "0".Equals(undrugzt.Memo) || !"UL".Equals(undrugzt.SysClass.ID))
                {
                    continue;
                }
                if (!alSysClass.Contains(undrugzt.Memo))
                {
                    this.tcItemPanel.TabPages.Add(undrugzt.Memo, undrugzt.Memo);
                    alSysClass.Add(undrugzt.Memo);
                    FS.FrameWork.WinForms.Controls.NeuTreeView tv = new FS.FrameWork.WinForms.Controls.NeuTreeView();
                    tv.AfterCheck += new TreeViewEventHandler(checkedChildNode);
                    tv.CheckBoxes = true;
                    tv.Dock = DockStyle.Fill;
                    this.tcItemPanel.Font = new Font("宋体", 12);
                    this.tcItemPanel.TabPages[undrugzt.Memo].Controls.Add(tv);
                }
                FS.FrameWork.WinForms.Controls.NeuTreeNode tn = new FS.FrameWork.WinForms.Controls.NeuTreeNode();
                tn.Text = undrugzt.Name;
                tn.Tag = undrugzt;
                tn.Nodes.Clear();
                ((FS.FrameWork.WinForms.Controls.NeuTreeView)this.tcItemPanel.TabPages[undrugzt.Memo].Controls[0]).Nodes.Add(tn);
            }
        }

        private void checkedChildNode(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && !string.IsNullOrEmpty(e.Node.Text))
            {
                if (e.Node.Checked)
                {
                    this.lstbox.Items.Add(e.Node.Tag);
                }
                else
                {
                    this.lstbox.Items.Remove(e.Node.Tag);
                }
            }
        }

        public ArrayList Orders
        {
            get
            {
                ArrayList alOrders = new ArrayList();
                string combID = string.Empty;
                FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
                FS.HISFC.BizLogic.Manager.OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();
                foreach (FS.HISFC.Models.Fee.Item.Undrug item in lstbox.Items)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                    order.ID = string.Empty;
                    order.Qty = item.Qty;
                    order.Unit = item.PriceUnit;
                    order.ExeDept.ID = item.ExecDept;
                    order.Name = item.Name;
                    order.Item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                    //order.ApplyNo = item.Package.ID;
                    //order.User02 = item.Package.Name;
                    order.Unit = item.PriceUnit;
                    order.HerbalQty = 1;
                    order.Sample.Name = item.CheckBody;
                    order.OrderType.IsDecompose = true;
                    order.OrderType.isCharge = true;
                    order.OrderType.isConfirm = false;
                    alOrders.Add(order);
                }
                string packageID = string.Empty;
                string newComboID = string.Empty;
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    if (string.IsNullOrEmpty(order.ApplyNo))
                    {
                        //复合项目号为空,新组合号
                        newComboID = orderManager.GetNewOrderComboID();
                        order.Combo.ID = newComboID;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(packageID))
                        {
                            //前一次复合项目为空
                            newComboID = orderManager.GetNewOrderComboID();
                            order.Combo.ID = newComboID;
                            packageID = order.ApplyNo;
                        }
                        else
                        {
                            if (packageID.Equals(order.ApplyNo))
                            {
                                //和前一次相等,为同一组
                                order.Combo.ID = newComboID;
                            }
                            else
                            {
                                newComboID = orderManager.GetNewOrderComboID();
                                order.Combo.ID = newComboID;
                                packageID = order.ApplyNo;
                            }
                        }
                    }
                }
                return alOrders;
            }
        }

        public void Clear()
        {
            this.lstbox.Items.Clear();
            foreach (TabPage tp in this.tcItemPanel.TabPages)
            {
                FS.FrameWork.WinForms.Controls.NeuTreeView tv = (FS.FrameWork.WinForms.Controls.NeuTreeView)tp.Controls[0];
                foreach (FS.FrameWork.WinForms.Controls.NeuTreeNode tn in tv.Nodes)
                {
                    tn.Checked = false;
                }
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.OK;
            this.ParentForm.Hide();
        }
    }
}
