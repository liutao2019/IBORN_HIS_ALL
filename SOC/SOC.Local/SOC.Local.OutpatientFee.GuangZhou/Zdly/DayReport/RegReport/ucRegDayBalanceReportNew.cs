using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.OutpatientFee.GuangZhou.Zdly.DayReport.RegReport
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

            if (this.neuSpread1_Sheet1.RowCount > 3)
            {
                this.neuSpread1_Sheet1.RemoveRows(3, this.neuSpread1_Sheet1.RowCount-3);
            }
        }
        private DateTime dtBegin = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime DtBegin
        {
            set { dtBegin = value; }
        }
        private DateTime dtEnd = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime DtEnd
        {
            set { dtEnd = value; }
        }
        #endregion

        Neusoft.HISFC.BizLogic.Registration.DayReport dayReport = new Neusoft.HISFC.BizLogic.Registration.DayReport();
        /// <summary>
        /// 将信息填充到farpoint上
        /// </summary>
        /// <param name="dayreport">挂号日结实体</param>
        /// <returns></returns>
        public int setFP(Neusoft.HISFC.Models.Registration.DayReport dayreport)
        {
            int BackCount = 0;//退费张数
            int Disvalid = 0;//作废张数
            if (dayreport.Details.Count <= 0) return -1;

            //单位
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Value = dayReport.Hospital.Name;

            //统计时间
            this.neuSpread1_Sheet1.Cells.Get(2,5).Value = dtBegin.ToString() +" 至 "+dtEnd.ToString();

            //表头
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount-1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Value = "收据起止号码";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).Value = "张数";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 4).Value = "合计金额";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 5).Value = "挂号金额";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 6).Value = "诊金";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).Value = "病历本费";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 8).Value = "备注";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            #region 发票部分

            for (int i = 0; i < dayreport.Details.Count; i++)
            {
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(i+4, 0).ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells.Get(i+4, 0).RowSpan = 1;
                this.neuSpread1_Sheet1.Cells[i + 4, 0].Text = dayreport.Details[i].BeginRecipeNo + "～" + dayreport.Details[i].EndRecipeNo;
                this.neuSpread1_Sheet1.Cells[i + 4, 3].Text = dayreport.Details[i].Count.ToString();
                if (dayreport.Details[i].Status == Neusoft.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    this.neuSpread1_Sheet1.Cells[i + 4, 4].Text = (dayreport.Details[i].RegFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i + 4, 5].Text = (dayreport.Details[i].DigFee - dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i + 4, 6].Text = (dayreport.Details[i].CardFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i + 4, 7].Text = (dayreport.Details[i].CaseFee).ToString();
                    BackCount += dayreport.Details[i].Count;

                }
                else
                {

                    this.neuSpread1_Sheet1.Cells[i + 4, 4].Text = dayreport.Details[i].RegFee.ToString();
                    this.neuSpread1_Sheet1.Cells[i + 4, 5].Text = (dayreport.Details[i].DigFee + dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i + 4, 6].Text = (dayreport.Details[i].CardFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i + 4, 7].Text = (dayreport.Details[i].CaseFee).ToString();

                }

                this.neuSpread1_Sheet1.Cells[i + 4, 8].Text = getStatus(dayreport.Details[i].Status);

                if (dayreport.Details[i].Status == Neusoft.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    Disvalid += dayreport.Details[i].Count;
                }
            }
            //合计
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计";

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = dayreport.SumCount.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = dayreport.SumRegFee.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = (dayreport.SumDigFee + dayreport.SumChkFee).ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = dayreport.SumCardFee.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = dayreport.SumCaseFee.ToString();


            #region 现金金额 、诊疗卡金额
            //现金
            decimal decCAtot = dayReport.QueryPayModeTotal(dayreport.Oper.ID, dtBegin.ToString(), dtEnd.ToString(),"CA");
            //诊疗卡
            decimal decPOtot = dayReport.QueryPayModeTotal(dayreport.Oper.ID, dtBegin.ToString(), dtEnd.ToString(),"PO");

            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "现金";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = decCAtot.ToString("F2");

            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 5).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "诊疗卡账户";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = decPOtot.ToString("F2");


            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 5).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "合计";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = (decCAtot + decPOtot).ToString("F2");
            

            #endregion
            //添加空行

            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 9;

           

            #endregion

            #region 诊疗卡预交
            decimal decValid = 0;//正交易 收取
            decimal decCancle = 0;//负交易 取现
            //根据操作人、开始时间、结束时间 获取诊疗卡预交金记录
            DataSet dsAccountPay = dayReport.QueryAccountPay(dayreport.Oper.ID, dtBegin.ToString(), dtEnd.ToString());
            if (dsAccountPay!=null &&dsAccountPay.Tables.Count>0 && dsAccountPay.Tables[0].Rows.Count>0)
            {
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.RowCount - 1).Height = this.neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.RowCount - 2).Height * 2;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 9;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Text = "诊疗卡预交金收取、取现日结报表";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                string invFirst = string.Empty;
                string invLast = string.Empty;
                for (int i = 0; i <= dsAccountPay.Tables[0].Rows.Count - 1; ++i)
                {
                    string OperType = dsAccountPay.Tables[0].Rows[i]["OPERTYPE"].ToString().Trim();
                    switch (OperType)
                    {
                        case "0"://预交金
                            decValid += Neusoft.FrameWork.Function.NConvert.ToDecimal(dsAccountPay.Tables[0].Rows[i]["MONEY"].ToString().Trim());
                            if (string.IsNullOrEmpty(invFirst))
                            {
                                invFirst= dsAccountPay.Tables[0].Rows[i]["REMARK"].ToString().Trim();
                            }
                            invLast = dsAccountPay.Tables[0].Rows[i]["REMARK"].ToString().Trim();
                            break;
                        case "16"://取消预交金 取现
                            decCancle += (-Neusoft.FrameWork.Function.NConvert.ToDecimal(dsAccountPay.Tables[0].Rows[i]["MONEY"].ToString().Trim()));
                            break;
                        default:
                            break;

                    }
                }
                string invZoon = invFirst + " ~ " + invLast;
                //收取
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "收取";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = decValid.ToString("F2");
                //取现
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "取现";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = decCancle.ToString("F2");
                //合计
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (decValid + decCancle).ToString("F2");
                //现金
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "现金";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (decValid + decCancle).ToString("F2");

                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 9;

                //空行
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 9;
            }

            #endregion


            //实收
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "实收 ";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (dayreport.SumOwnCost+decValid + decCancle).ToString("F2");

            // 收费员
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "收费员 ";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = dayreport.Oper.Name;

            // 财务复核
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "财务复核 ";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            // 打印时间
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "打印时间 ";
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).Text = dayReport.GetDateTimeFromSysDateTime().ToString();

          

            //this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 5, 1, 5);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "退费张数:" + BackCount.ToString();


         
            return 1;
        }
        /// <summary>
        /// 将挂号是否有效转换成汉字
        /// </summary>
        /// <param name="status">挂号状态</param>
        /// <returns></returns>
        private string getStatus(Neusoft.HISFC.Models.Base.EnumRegisterStatus status)
        {
            if (status == Neusoft.HISFC.Models.Base.EnumRegisterStatus.Valid)
            { return "正常"; }
            else if (status == Neusoft.HISFC.Models.Base.EnumRegisterStatus.Back)
            { return "退费"; }
            else if (status == Neusoft.HISFC.Models.Base.EnumRegisterStatus.Cancel)
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
