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
    public partial class ucRegDayBalanceReportReg : UserControl
    {
        public ucRegDayBalanceReportReg()
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
            string strNegative = string.Empty;
            if (dayreport.Details.Count <= 0) return -1;
            for (int i = 0; i < dayreport.Details.Count; i++)
            {
                if (dayreport.Details[i].Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    if (dayreport.Details[i].BeginRecipeNo == dayreport.Details[i].EndRecipeNo)
                    {
                        if (string.IsNullOrEmpty(strNegative))
                        {
                            strNegative += dayreport.Details[i].BeginRecipeNo;
                        }
                        else
                        {
                            strNegative += "、" + dayreport.Details[i].BeginRecipeNo;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strNegative))
                        {
                            strNegative += dayreport.Details[i].BeginRecipeNo + "～" + dayreport.Details[i].EndRecipeNo;
                        }
                        else
                        {
                            strNegative += "、" + dayreport.Details[i].BeginRecipeNo + "～" + dayreport.Details[i].EndRecipeNo;
                        }
                    }

                    BackCount += dayreport.Details[i].Count;
                }
                else
                {
                    this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = dayreport.Details[i].BeginRecipeNo + "～" + dayreport.Details[i].EndRecipeNo;
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = dayreport.Details[i].Count.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = dayreport.Details[i].OwnCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = (dayreport.Details[i].PayCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = dayreport.Details[i].RegFee.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = (dayreport.Details[i].DigFee + dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = (dayreport.Details[i].CardFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 7].Text = (dayreport.Details[i].CaseFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 8].Text = dayreport.Details[i].OthFee.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 9].Text = (dayreport.Details[i].TotalFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 10].Text = getStatus(dayreport.Details[i].Status);
                }

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
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = dayreport.SumTotal.ToString();
            //大写金额
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 11);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "实收金额(大写): " + FS.FrameWork.Function.NConvert.ToCapital(dayreport.SumOwnCost);


            // 操作员信息
            //this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            //this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 2);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "缴款人: " + dayreport.Oper.Name;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Row.Height = 25F; 

            //this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 2, 1, 3);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "收款员: " + dayreport.Oper.ID;


            //this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 5, 1, 6);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "作废张数: " + Disvalid.ToString();


            //
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 11);
            //设置单元格属性用于换行
            FarPoint.Win.Spread.CellType.TextCellType TextCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            TextCellType.WordWrap = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].CellType = TextCellType;
            
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "有效发票共" + dayreport.SumCount.ToString() + "张" + "退费张数:" + (BackCount + Disvalid).ToString() + "张:" + strNegative;


            //this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            //this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 11);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "统计时间: " + dayreport.BeginDate.ToString() + " 至 " + dayreport.EndDate.ToString();
            this.lblDate.Text = dayreport.BeginDate.ToString() + "至" + dayreport.EndDate.ToString();
            this.lblRegName.Text = dayreport.Oper.Name;
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

        /// <summary>
        /// 清除FarPoint数据
        /// </summary>
        private void FPClear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SetFPByDept(DataSet dsRegInfoDept, List<int> HideList)
        {
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 11);
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "科室";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "发票张数";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "实收金额";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "帐户金额";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "挂号费";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "诊金";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = "卡工本费";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = "病历本费";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = "其他金额";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = "金额合计";

            for (int i = 0; i <dsRegInfoDept.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dsRegInfoDept.Tables[0].Rows[i];
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = dr[0].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = dr[1].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = dr[2].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = dr[3].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = dr[4].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = dr[5].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = dr[6].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = dr[7].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = dr[8].ToString(); 
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = dr[9].ToString(); 
            }
            foreach (int Column in HideList)
            {
                this.neuSpread1_Sheet1.Columns[Column].Visible = false;
            
            }
            return 1;
        }




    }
}
