using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.SubFeeSet
{
    public partial class frmChoseSublItem : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmChoseSublItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 附材医嘱列表
        /// </summary>
        ArrayList alSublOrders = new ArrayList();

        /// <summary>
        /// 附材医嘱列表
        /// </summary>
        public ArrayList AlSublOrders
        {
            get
            {
                return alSublOrders;
            }
            set
            {
                if (value != null && value.Count > 0)
                {
                    this.AddItem(value);
                }
            }
        }

        /// <summary>
        /// 类别：门诊、住院
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes serverType = FS.HISFC.Models.Base.ServiceTypes.C;

        /// <summary>
        /// 类别：门诊、住院
        /// </summary>
        public FS.HISFC.Models.Base.ServiceTypes ServerType
        {
            get
            {
                return serverType;
            }
            set
            {
                serverType = value;
            }
        }

        private void AddItem(ArrayList al)
        {
            this.fpSublItem_Sheet1.RowCount = 0;

            if (serverType == FS.HISFC.Models.Base.ServiceTypes.C)
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in al)
                {
                    this.fpSublItem_Sheet1.Rows.Add(0, 1);
                    this.fpSublItem_Sheet1.Columns[3].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpSublItem_Sheet1.Cells[0, 0].Value = 0;
                    this.fpSublItem_Sheet1.Cells[0, 0].Locked = false;
                    this.fpSublItem_Sheet1.Cells[0, 1].Text = outOrder.Item.Name;
                    this.fpSublItem_Sheet1.Cells[0, 1].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 2].Text = outOrder.Item.UserCode;
                    this.fpSublItem_Sheet1.Cells[0, 2].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 3].Text = outOrder.Item.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpSublItem_Sheet1.Cells[0, 3].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 4].Text = outOrder.Item.Specs;
                    this.fpSublItem_Sheet1.Cells[0, 4].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 5].Text = outOrder.Item.SysClass.Name;
                    this.fpSublItem_Sheet1.Cells[0, 5].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 6].Text = outOrder.Item.PriceUnit;
                    this.fpSublItem_Sheet1.Cells[0, 6].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 7].Text = outOrder.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpSublItem_Sheet1.Cells[0, 7].Locked = true;

                    this.fpSublItem_Sheet1.Rows[0].Tag = outOrder;
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.Item item in al)
                {
                    this.fpSublItem_Sheet1.Rows.Add(0, 1);
                    this.fpSublItem_Sheet1.Columns[3].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpSublItem_Sheet1.Cells[0, 0].Value = 0;
                    this.fpSublItem_Sheet1.Cells[0, 0].Locked = false;
                    this.fpSublItem_Sheet1.Cells[0, 1].Text = item.Name;
                    this.fpSublItem_Sheet1.Cells[0, 1].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 2].Text = item.UserCode;
                    this.fpSublItem_Sheet1.Cells[0, 2].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 3].Text = item.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpSublItem_Sheet1.Cells[0, 3].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 4].Text = item.Specs;
                    this.fpSublItem_Sheet1.Cells[0, 4].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 5].Text = item.SysClass.Name;
                    this.fpSublItem_Sheet1.Cells[0, 5].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 6].Text = item.PriceUnit;
                    this.fpSublItem_Sheet1.Cells[0, 6].Locked = true;
                    this.fpSublItem_Sheet1.Cells[0, 7].Text = item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.fpSublItem_Sheet1.Cells[0, 7].Locked = true;

                    this.fpSublItem_Sheet1.Rows[0].Tag = item;
                }
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.alSublOrders = new ArrayList();
            for (int i = 0; i < fpSublItem_Sheet1.RowCount; i++)
            {
                if (fpSublItem_Sheet1.Cells[i, 0].Text == "True")
                {
                    if (fpSublItem_Sheet1.Rows[i].Tag != null)
                    {
                        alSublOrders.Add(fpSublItem_Sheet1.Rows[i].Tag);
                    }
                }
            }
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmChoseSublItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                this.alSublOrders = new ArrayList();
                for (int i = 0; i < fpSublItem_Sheet1.RowCount; i++)
                {
                    if (fpSublItem_Sheet1.Cells[i, 0].Text == "True")
                    {
                        if (fpSublItem_Sheet1.Rows[i].Tag != null)
                        {
                            alSublOrders.Add(fpSublItem_Sheet1.Rows[i].Tag);
                        }
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void fpSublItem_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
        }
    }
}
