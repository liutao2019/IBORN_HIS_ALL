using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinReg
{
    /// <summary>
    /// 挂号日结报表-新
    /// </summary>
    public partial class ucRegDayBalanceReportNew : UserControl
    {
        public ucRegDayBalanceReportNew()
        {
            InitializeComponent();
            this.InitUC();
        }
        #region
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitUC()
        {
            //设置医院名称 

            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
        }
        #endregion
        /// <summary>
        /// 将信息填充到farpoint上
        /// </summary>
        /// <param name="dayreport">挂号日结实体</param>
        /// <returns></returns>
        public int setFP(FS.HISFC.Models.Registration.DayReport dayreport)
        {
            int BackCount = 0;//退费张数
            int Disvalid = 0;//作废张数
            if (dayreport.Details.Count <= 0) return -1;
            for (int i = 0; i < dayreport.Details.Count; i++)
            {
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);

                this.neuSpread1_Sheet1.Cells[i, 0].Text = dayreport.Details[i].BeginRecipeNo + "～" + dayreport.Details[i].EndRecipeNo;
                this.neuSpread1_Sheet1.Cells[i, 1].Text = dayreport.Details[i].Count.ToString();
                if (dayreport.Details[i].Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = (dayreport.Details[i].OwnCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = (dayreport.Details[i].PayCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = (dayreport.Details[i].RegFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = (dayreport.Details[i].DigFee - dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = (dayreport.Details[i].CardFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 7].Text = (dayreport.Details[i].CaseFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 8].Text = (dayreport.Details[i].OthFee).ToString();
                    BackCount += dayreport.Details[i].Count;

                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = dayreport.Details[i].OwnCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = (dayreport.Details[i].PayCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = dayreport.Details[i].RegFee.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = (dayreport.Details[i].DigFee + dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = (dayreport.Details[i].CardFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 7].Text = (dayreport.Details[i].CaseFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 8].Text = dayreport.Details[i].OthFee.ToString();
                }

                this.neuSpread1_Sheet1.Cells[i, 9].Text = getStatus(dayreport.Details[i].Status);

                if (dayreport.Details[i].Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    Disvalid += dayreport.Details[i].Count;
                }
            }
            //合计
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = dayreport.SumCount.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = dayreport.SumOwnCost.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = dayreport.SumPayCost.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = dayreport.SumRegFee.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = (dayreport.SumDigFee + dayreport.SumChkFee).ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = dayreport.SumCardFee.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = dayreport.SumCaseFee.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = dayreport.SumOthFee.ToString();
            //大写金额
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 10);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "实收金额(大写): " + FS.FrameWork.Function.NConvert.ToCapital(dayreport.SumOwnCost);


            // 操作员信息
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 2);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "缴款人: " + dayreport.Oper.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Row.Height = 25F; 

            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 2, 1, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "收款员: " + dayreport.Oper.ID;


            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 5, 1, 5);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "作废张数: " + Disvalid.ToString();


            //
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 2);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "填表人: ";//2011-10-31 修改显示名称
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "核收人:";            
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Row.Height = 25F;

            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 2, 1, 3);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "出纳员: ";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "票据审核人:";

            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 5, 1, 5);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "退费张数:" + BackCount.ToString();


            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 10);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "统计时间: " + dayreport.BeginDate.ToString() + " 至 " + dayreport.EndDate.ToString();
            return 1;
        }
        /// <summary>
        /// 将挂号是否有效转换成汉字
        /// </summary>
        /// <param name="status">挂号状态</param>
        /// <returns></returns>
        private string getStatus(FS.HISFC.Models.Base.EnumRegisterStatus status)
        {
            if (status == FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
            { return "正常"; }
            else if (status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            { return "退费"; }
            else if (status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            { return "作废"; }
            else
            { return "错误"; }
        }
        private void FPClear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
        }

        #region add by lijp 2012-08-31  {86CDDBDA-E97A-4668-A9F9-0D7B36189D38}

        /// <summary>
        /// 修改报表头的列标题
        /// 传入参数规则：以分号";"分隔各列参数，各列参数用逗号"."分隔，前面是列序，后面是标题字体。
        /// </summary>
        /// <param name="headerStringArr"></param>
        public void ModifyReportHeaderLabel(string headerStringArr)
        {
            if (string.IsNullOrEmpty(headerStringArr))
            {
                return;
            }
            string[] colArr = headerStringArr.Trim().Split(';');

            for ( int i=0; i<colArr.Length; i++ )
            {
                string[] nameAndLabel = colArr[i].Trim().Split(',');

                if (nameAndLabel == null)
                {
                    continue;
                }

                if (nameAndLabel.Length != 2)
                {
                    continue;
                }

                int colIndex = -1;
                try
                {
                    colIndex = Convert.ToInt32(nameAndLabel[0].Trim());
                }
                catch
                {
                    continue;
                }
                if (colIndex >= this.neuSpread1_Sheet1.ColumnCount)
                {
                    continue;
                }

                this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, colIndex).Value = nameAndLabel[1].Trim();
            }

        }

        #endregion
    }
}
