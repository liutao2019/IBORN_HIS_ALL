using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.Query
{
    public partial class ucFeedetailPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFeedetailPrint()
        {
            InitializeComponent();
        }

        #region 变量

        int queryType = 0;

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        #endregion


        #region 方法

        /// <summary>
        /// 获取对应的发票号
        /// </summary>
        /// <returns></returns>
        public void GetInvoiceNo()
        {
            #region 是否是有效的输入字符

            string strValid = this.txtInvoiceNo.Text.Trim();
            if (strValid.Length <= 0)
            {
                if (this.queryType == 1)
                {
                    MessageBox.Show("请输入发票号!");
                    return;
                }
                else if (this.queryType == 2)
                {
                    MessageBox.Show("请输入病历号!");
                    return;
                }
                else
                {
                    MessageBox.Show("请输入患者姓名!");
                    return;
                }
            }

            #endregion

            //刷卡功能
            if (this.queryType == 2)
            {
                string cardNo = this.txtInvoiceNo.Text.Trim();
                FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
                if (this.feeIntegrate.ValidMarkNO(cardNo, ref accountObj) == -1)
                {
                    MessageBox.Show(feeIntegrate.Err);
                    this.txtInvoiceNo.Select();
                    this.txtInvoiceNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(accountObj.Patient.PID.CardNO))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("您输入的就诊卡号不存在"), "提示");
                    this.txtInvoiceNo.Select();
                    this.txtInvoiceNo.Focus();
                    return;
                }

                this.txtInvoiceNo.Text = accountObj.Patient.PID.CardNO;

            }


            frmInvoiceList frm = new frmInvoiceList();
            frm.SelectInvoiceNO += new frmInvoiceList.SelectInvoice(SelectItem);
            frm.Location = new Point(this.Parent.Location.X + 100, this.Parent.Location.Y + 200);

            if (this.neuCheckBox1.Checked)
            {
                frm.Query(this.queryType.ToString(), this.txtInvoiceNo.Text.Trim(), this.beginDate.Value.ToString(), this.endDate.Value.ToString());
            }
            else
            {
                frm.Query(this.queryType.ToString(), this.txtInvoiceNo.Text.Trim(), DateTime.MinValue.AddDays(1).ToString(), DateTime.MaxValue.AddDays(-1).ToString());

            }
        }

        /// <summary>
        /// 选择发票处理事件方法
        /// </summary>
        /// <param name="invoiceNo"></param>
        public void SelectItem(string invoiceNo)
        {
            DataSet dsResult = new DataSet();

            if (this.feeMgr.QueryInvoiceFeedetail(invoiceNo, ref dsResult) == -1)
            {
                MessageBox.Show("查询发票明细失败!");
                return;
            }

            if (dsResult == null || dsResult.Tables[0].Rows.Count <= 0)
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;
                return;
            }

            DataTable dt = dsResult.Tables[0];

            this.neuSpread1_Sheet1.Rows.Count = 0;
            int index = this.neuSpread1_Sheet1.Rows.Count;
            decimal totCost = 0m;

            foreach (DataRow dr in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);

                FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();

                this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtType;
                this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString();

                this.neuSpread1_Sheet1.Cells[index, 1].CellType = txtType;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();

                this.neuSpread1_Sheet1.Cells[index, 2].CellType = txtType;
                this.neuSpread1_Sheet1.Cells[index, 2].Text = dr[2].ToString();

                this.neuSpread1_Sheet1.Cells[index, 3].CellType = numType;
                this.neuSpread1_Sheet1.Cells[index, 3].Text = dr[3].ToString();

                this.neuSpread1_Sheet1.Cells[index, 4].CellType = txtType;
                this.neuSpread1_Sheet1.Cells[index, 4].Text = dr[4].ToString();

                this.neuSpread1_Sheet1.Cells[index, 5].CellType = numType;
                this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[5].ToString();

                this.neuSpread1_Sheet1.Cells[index, 6].CellType = numType;
                this.neuSpread1_Sheet1.Cells[index, 6].Text = dr[6].ToString();
                totCost += FS.FrameWork.Function.NConvert.ToDecimal(dr[6].ToString());

                index++;

            }

            DataRow drTemp = dt.Rows[0];
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 0].Text = "发票号：" + drTemp[11].ToString() + "; 自费金额：" + drTemp[13].ToString() + "; 公费金额：" + drTemp[14].ToString()+"; 合同单位：" + drTemp[15].ToString();
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 0].Text = "姓名：" + drTemp[8].ToString();
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 2].Text = "日期：" + drTemp[7].ToString();

            index = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(index, 1);
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[index, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells[index, 0].Text = "合计：";
            this.neuSpread1_Sheet1.Cells[index, 6].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuSpread1_Sheet1.Cells[index, 6].Text = totCost.ToString();

            index = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(index, 1);
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[index, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].Text = "祝君健康";
            this.neuSpread1_Sheet1.Cells[index, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells[index, 1].Text = "收费员工号：" + drTemp[9].ToString();
            this.neuSpread1_Sheet1.Cells[index, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[index, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 3].ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells[index, 3].Text = "打印日期：" + drTemp[10].ToString();

            index = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(index, 1);
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[index, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 7;
            this.neuSpread1_Sheet1.Cells[index, 0].Text = "医生工号：" + drTemp[12].ToString();

        }

        /// <summary>
        /// 设置FP宽度
        /// </summary>
        public void SetFP()
        {
            this.neuSpread1_Sheet1.Columns[0].Width = 80;
            this.neuSpread1_Sheet1.Columns[1].Width = 170;
            this.neuSpread1_Sheet1.Columns[2].Width = 130;
            this.neuSpread1_Sheet1.Columns[3].Width = 80;
            this.neuSpread1_Sheet1.Columns[4].Width = 80;
            this.neuSpread1_Sheet1.Columns[5].Width = 100;
            this.neuSpread1_Sheet1.Columns[6].Width = 100;
        }


        #endregion


        #region 事件

        protected override int  OnQuery(object sender, object neuObject)
        {
            this.GetInvoiceNo();
 	         return base.OnQuery(sender, neuObject);
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.GetInvoiceNo();
            }
        }

        private void cbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.queryType = this.cbQueryType.SelectedIndex + 1;
        }

        private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.neuCheckBox1.Checked)
            {
                this.beginDate.Enabled = true;
                this.endDate.Enabled = true;
            }
            else
            {
                this.beginDate.Enabled = false;
                this.endDate.Enabled = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            DateTime now = this.feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.endDate.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            if (this.neuCheckBox1.Checked)
            {
                this.beginDate.Enabled = true;
                this.endDate.Enabled = true;
            }
            else
            {
                this.beginDate.Enabled = false;
                this.endDate.Enabled = false;
            }

            this.cbQueryType.SelectedIndex = 1;  //默认为病历号
            this.queryType = 2;

            this.SetFP();

            //设置焦点
            this.txtInvoiceNo.Select();
            this.txtInvoiceNo.Focus();

            base.OnLoad(e);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return 1;
            }

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize ps = null;
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            ps = pgMgr.GetPageSize("ClinicFeedetail");

            if (ps == null)
            {
                //默认为A4纸张
                ps = new FS.HISFC.Models.Base.PageSize("A4", 827, 1169);
            }

            print.SetPageSize(ps);

            print.PrintPage(ps.Left , ps.Top, this.neuPanel1);

            return 1;

            //return base.OnPrint(sender, neuObject);
        }


        
        #endregion  

        //private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        //{
            
        //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, Application.StartupPath+"/profile/testgmz.xml");

        //}

        

    }
}
