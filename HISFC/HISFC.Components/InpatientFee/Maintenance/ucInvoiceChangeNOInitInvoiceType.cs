﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Fee;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    /// <summary>
    /// 跳号成功后查询
    /// </summary>
    public delegate void QueryInvoiceAfterChange();

    /// <summary>
    /// [功能描述: 调整收费员默认发票号]<br></br>
    /// [创建者:   郝武]<br></br>
    /// [创建时间: 2008-11-24]<br></br>
    /// <说明>
    /// </说明>
    /// <修改记录>
    ///     <修改时间>本次修改时间</修改时间>
    ///     <修改内容>
    ///            本次修改内容
    ///     </修改内容>
    /// </修改记录>
    /// </summary>
    public partial class ucInvoiceChangeNOInitInvoiceType : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //跳号后查询
        public QueryInvoiceAfterChange QueryAfterChange;
        FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum manager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// 登陆操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 发票类型
        /// </summary>
        private string invoiceTypeID = string.Empty;

        /// <summary>
        ///  发票类型
        /// </summary>
        public string InvoiceTypeID
        {
            get { return invoiceTypeID; }
            set { invoiceTypeID = value; }
        }

        /// <summary>
        /// 发票类型名称
        /// </summary>
        private string invoiceTypeName = "门诊发票";

        /// <summary>
        /// 发票类型名称
        /// </summary>
        public string InvoiceTypeName
        {
            get { return invoiceTypeName; }
            set
            {
                invoiceTypeName = value;
                this.lbInvoiceTypeName.Text = string.Format("发票类型：{0}", InvoiceTypeName);
            }
        }

        /// <summary>
        /// 是否显示跳号列
        /// </summary>
        public bool IsShowButtonColumn
        {
            set
            {
                this.invoiceSheet.Columns[5].Visible = value;
            }
        }

        public ucInvoiceChangeNOInitInvoiceType()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.oper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

                //ArrayList invoiceList = manager.QueryInvoices(this.oper.ID, this.InvoiceTypeID);

                //InitSheet(invoiceList);
            }
            base.OnLoad(e);
        }

        public int QueryInvoices(string operCode, string invoiceType)
        {
            ArrayList invoiceList = manager.QueryInvoices(operCode, invoiceType);
            InitSheet(invoiceList);
            return 1;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="invoiceList"></param>
        private void InitSheet(ArrayList invoiceList)
        {
            if (this.invoiceSheet.RowCount > 0)
            {
                this.invoiceSheet.Rows.Remove(0, this.invoiceSheet.Rows.Count);
            }

            if (invoiceList != null && invoiceList.Count > 0)
            {
                this.invoiceSheet.RowCount = 0;
                int i = 0;
                foreach (Invoice invoice in invoiceList)
                {
                    //if (invoice.EndNO != invoice.UsedNO && invoice.ValidState != "-1")
                    if (invoice.ValidState != "-1") //把最后发票段使用完但是validState=1的也显示出来，这里手工改动
                    {
                        this.invoiceSheet.Rows.Add(this.invoiceSheet.RowCount, 1);
                        this.invoiceSheet.Cells[i, 0].Text = invoice.AcceptTime.ToString();
                        this.invoiceSheet.Cells[i, 1].Text = invoice.BeginNO.ToString();
                        this.invoiceSheet.Cells[i, 2].Text = invoice.EndNO;
                        this.invoiceSheet.Cells[i, 3].Text = invoice.UsedNO;
                        this.invoiceSheet.Cells[i, 4].Value = invoice.ValidState == "1" ? true : false;
                        this.invoiceSheet.Rows[i].Tag = invoice;
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// 更新发票数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidDefaultState())
            {
                MessageBox.Show("只能有一个默认号段");
                return;
            }

            for (int i = 0, j = this.invoiceSheet.RowCount; i < j; i++)
            {
                if (Boolean.Parse(this.invoiceSheet.Cells[i, 4].Value.ToString()) == true)
                {
                    string endNO = this.invoiceSheet.Cells[i, 2].Text;
                    string usedNO = this.invoiceSheet.Cells[i, 3].Text;
                    if (endNO == usedNO)
                    {
                        MessageBox.Show("该发票段已经用完了!请选择另一段!");
                        return;
                    }
                    break;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            for (int i = 0, j = this.invoiceSheet.RowCount; i < j; i++)
            {
                Invoice invoice = (Invoice)this.invoiceSheet.Rows[i].Tag;
                invoice.ValidState = Boolean.Parse(this.invoiceSheet.Cells[i, 4].Value.ToString()) ? "1" : "2";  //1表示在用；2表示停用

                //如果发票段中使用号和终止号相等，表示该发票段已经用完了。
                if (invoice.UsedNO == invoice.EndNO)
                {
                    invoice.ValidState = "-1";
                }

                if (manager.UpdateInvoiceDefaltState(invoice) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新出差：" + manager.Err);
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.FindForm().Close();
        }



        private void neuFpEnter1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //if (!ValidDefaultState())
            //{
            //    MessageBox.Show("只能有一个默认号段");
            //}
        }

        private bool ValidDefaultState()
        {
            int count = 0;
            for (int i = 0, j = this.invoiceSheet.RowCount; i < j; i++)
            {
                if (Boolean.Parse(this.invoiceSheet.Cells[i, 4].Value.ToString()) == true)
                {
                    count++;

                    if (count > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int SaveResult()
        {
            if (!ValidDefaultState())
            {
                MessageBox.Show("只能有一个默认号段");
                return -1;
            }

            for (int i = 0, j = this.invoiceSheet.RowCount; i < j; i++)
            {
                if (Boolean.Parse(this.invoiceSheet.Cells[i, 4].Value.ToString()) == true)
                {
                    string endNO = this.invoiceSheet.Cells[i, 2].Text;
                    string usedNO = this.invoiceSheet.Cells[i, 3].Text;
                    if (endNO == usedNO)
                    {
                        MessageBox.Show("该发票段已经用完了!请选择另一段!");
                        return -1;
                    }
                    break;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            for (int i = 0, j = this.invoiceSheet.RowCount; i < j; i++)
            {
                Invoice invoice = (Invoice)this.invoiceSheet.Rows[i].Tag;
                invoice.ValidState = Boolean.Parse(this.invoiceSheet.Cells[i, 4].Value.ToString()) ? "1" : "2"; //1表示在用；2表示停用

                //如果发票段中使用号和终止号相等，表示该发票段已经用完了。
                if (invoice.UsedNO == invoice.EndNO)
                {
                    invoice.ValidState = "-1";
                }

                if (manager.UpdateInvoiceDefaltState(invoice) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新出错：" + manager.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("设置成功");

            //this.FindForm().Close();
            return 1;
        }

        private void neuFpEnter1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //FS.HISFC.Models.Fee.Invoice invoice = this.neuFpEnter1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Fee.Invoice;

            //if (invoice == null)
            //{
            //    MessageBox.Show("请选再用发票记录");
            //    return;
            //}

            //FS.HISFC.Components.Common.Controls.ucChangeInvoiceNO ucChange = new FS.HISFC.Components.Common.Controls.ucChangeInvoiceNO();
            //ucChange.Invoice = invoice;
            //DialogResult d = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucChange);
            //if (d == DialogResult.OK)
            //{
            //    DisplayInvoiceInUse(FS.FrameWork.Management.Connection.Operator.ID);
            //}
        }

        private void neuFpEnter1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column != 5 && e.Column != 4) return;

            if (e.Column == 5)
            {
                if (e.Column != 5) return;
                FS.HISFC.Models.Fee.Invoice invoice = this.neuFpEnter1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Fee.Invoice;

                if (invoice == null)
                {
                    MessageBox.Show("请选再用发票记录");
                    return;
                }

                FS.HISFC.Components.Common.Controls.ucChangeInvoiceNO ucChange = new FS.HISFC.Components.Common.Controls.ucChangeInvoiceNO();
                ucChange.Invoice = invoice;
                DialogResult d = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucChange);
                this.QueryAfterChange();
                return;
            }
            else if (e.Column == 4)
            {
                if (e.Column != 4) return;

                for (int i = 0, j = this.invoiceSheet.RowCount; i < j; i++)
                {
                    this.invoiceSheet.Cells[i, 4].Value = false;
                }

                this.invoiceSheet.Cells[e.Row, e.Column].Value = true;
            }
        }

    }
}
