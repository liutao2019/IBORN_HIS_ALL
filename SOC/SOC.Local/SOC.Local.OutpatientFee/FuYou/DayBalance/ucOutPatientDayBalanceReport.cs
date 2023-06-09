using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.FuYou.DayBalance
{
    /// <summary>
    /// 门诊收费日结报表打印窗体
    /// </summary>
    public partial class ucOutPatientDayBalanceReport : UserControl
    {
        public ucOutPatientDayBalanceReport()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 是否汇总数据
        /// </summary>
        private bool isCollectData = false;

        /// <summary>
        /// 是否汇总数据
        /// </summary>
        public bool IsCollectData
        {
            set
            {
                isCollectData = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitUC(string title)
        {
            this.lbltitle.Text = title;
        }

        /// <summary>
        /// 清空显示
        /// </summary>
        public void Clear(string beginDate, string endDate)
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr=new FS.HISFC.BizLogic.Manager.Department();

            //显示报表日结时间和制表员
            string strSpace = "               ";
            string strInfo = "制表员：" + deptMgr.Operator.Name + strSpace + "起始时间：" +
                            beginDate + strSpace + "截止时间：" + endDate;
            this.lblReportInfo.Text = strInfo;
        }

        #region 设置Farpoint格式

        /// <summary>
        /// 设置显示格式
        /// </summary>
        /// <param name="sheet">SheetView</param>
        protected virtual void SetFpStyle(FarPoint.Win.Spread.SheetView sheet)
        {      

            try
            {
                if (sheet.Rows.Count > 4)
                {
                    sheet.Rows.Count = 4;
                }
                //上缴现金
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "上缴现金";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A001;

                //有效票据
                sheet.Rows.Count += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "有效数：";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A002;
                //作废数
                sheet.Cells[sheet.RowCount - 1, 2].Text = "作废数：";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A003;
                //作废金额
                sheet.Cells[sheet.RowCount - 1, 4].Text = "作废金额：";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A004;

                sheet.RowCount += 1;
                //使用票据号
                sheet.Cells[sheet.RowCount - 1, 0].Text = "使用票据号：";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 7);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A005;
                sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
                sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;

                sheet.RowCount += 1;
                //作废票据号
                sheet.Cells[sheet.RowCount - 1, 0].Text = "作废票据号：";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 7);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A006;
                sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
                sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;

                sheet.RowCount += 1;
                //职工医疗
                sheet.Cells[sheet.RowCount - 1, 0].Text = "职工医疗：";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A007;
                //居民医疗
                sheet.Cells[sheet.RowCount - 1, 2].Text = "居民医疗：";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A008;
                //特约单位
                sheet.Cells[sheet.RowCount - 1, 4].Text = "特约单位：";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A009;
                //医疗优惠
                sheet.Cells[sheet.RowCount - 1, 6].Text = "医疗优惠：";
                sheet.Cells[sheet.RowCount - 1, 7].Tag = EnumCellName.A010;
                

                sheet.RowCount += 1;
                //特定门诊
                sheet.Cells[sheet.RowCount - 1, 0].Text = "特定门诊：";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A011;
                //刷卡
                sheet.Cells[sheet.RowCount - 1, 2].Text = "刷卡结算：";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A012;
                //现金
                sheet.Cells[sheet.RowCount - 1, 4].Text = "现金结算：";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A013;

                #region 制表人等
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "制表人：";
                //sheet.Cells[sheet.RowCount - 1, 2].Text = "收款员签名：";
                sheet.Cells[sheet.RowCount - 1, 4].Text = "审核人：";

                #endregion

                return;

                #region 作废先

                #region 发票格式

                if (!isCollectData)
                {
                    //起止票据号
                    sheet.Rows.Count += 1;
                    sheet.Cells[sheet.RowCount - 1, 0].Text = "使用票据号：";//luoff
                    sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);//luoff
                    sheet.Cells[sheet.RowCount - 1, 1].Tag = "A00101";
                }
                //票据总数
                sheet.Rows.Count += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "票据总数：";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = "A002";

                //退费票据
                sheet.Cells[sheet.RowCount - 1, 2].Text = "退费票据数：";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = "A00401";
                //退费票据号: 在做日结和查询时显示，在汇总时不显示
                if (!this.isCollectData)
                {
                    //退费票据号
                    sheet.Rows.Count += 1;
                    sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);
                    sheet.Cells[sheet.RowCount - 1, 1].Tag = "A00402";
                    sheet.Cells[sheet.RowCount - 1, 0].Text = "退费票据号：";
                    sheet.Rows[sheet.RowCount - 1].Height = 50;
                }
                //作废票据
                if (!this.isCollectData)
                {
                    //作废票据号
                    sheet.Rows.Count += 1;

                    sheet.Cells[sheet.RowCount - 1, 4].Text = "作废票据数：";
                    sheet.Cells[sheet.RowCount - 1, 5].Tag = "A00501";
                    sheet.Models.Span.Add(sheet.RowCount - 1 + 1, 1, 1, 5);
                    sheet.Rows[sheet.RowCount - 1].Height = 50;
                    sheet.Cells[sheet.RowCount - 1, 1].Tag = "A00502";
                    sheet.Cells[sheet.RowCount - 1, 0].Text = "作废票据号：";
                }
                #endregion

                int rowCount = sheet.Rows.Count;
                sheet.Rows.Count += 9;
                sheet.Cells[rowCount, 0].Text = "退费金额";
                sheet.Cells[rowCount, 1].Tag = "A006";
                sheet.Cells[rowCount, 2].Text = "作废金额";
                sheet.Cells[rowCount, 3].Tag = "A007";
                sheet.Cells[rowCount, 4].Text = "押金金额";
                sheet.Cells[rowCount, 5].Tag = "A008";
                sheet.Cells[rowCount + 1, 0].Text = "退押金额";
                sheet.Cells[rowCount + 1, 1].Tag = "A009";
                sheet.Cells[rowCount + 1, 2].Text = "减免金额";
                sheet.Cells[rowCount + 1, 3].Tag = "A010";
                sheet.Cells[rowCount + 1, 4].Text = "四舍五入";
                sheet.Cells[rowCount + 1, 5].Tag = "A011";

                sheet.Cells[rowCount + 2, 0].Text = "公费医疗";
                sheet.Cells[rowCount + 2, 1].Tag = "A012";
                sheet.Cells[rowCount + 2, 2].Text = "公费自付";
                sheet.Cells[rowCount + 2, 3].Tag = "A013";
                sheet.Cells[rowCount + 2, 4].Text = "公费账户";
                sheet.Cells[rowCount + 2, 5].Tag = "A026";

                sheet.Cells[rowCount + 3, 0].Text = "市保自付";
                sheet.Cells[rowCount + 3, 1].Tag = "A014";
                sheet.Cells[rowCount + 3, 2].Text = "市保账户";
                sheet.Cells[rowCount + 3, 3].Tag = "A015";
                sheet.Cells[rowCount + 3, 4].Text = "市保统筹";
                sheet.Cells[rowCount + 3, 5].Tag = "A016";
                sheet.Cells[rowCount + 4, 0].Text = "市保大额";
                sheet.Cells[rowCount + 4, 1].Tag = "A017";

                sheet.Cells[rowCount + 5, 0].Text = "省保自付";
                sheet.Cells[rowCount + 5, 1].Tag = "A018";
                sheet.Cells[rowCount + 5, 2].Text = "省保账户";
                sheet.Cells[rowCount + 5, 3].Tag = "A019";
                sheet.Cells[rowCount + 5, 4].Text = "省保统筹";
                sheet.Cells[rowCount + 5, 5].Tag = "A020";
                sheet.Cells[rowCount + 6, 0].Text = "省保大额";
                sheet.Cells[rowCount + 6, 1].Tag = "A021";
                sheet.Cells[rowCount + 6, 2].Text = "省公务员";
                sheet.Cells[rowCount + 6, 3].Tag = "A022";

                sheet.Cells[rowCount + 7, 0].Text = "上缴现金额";
                sheet.Cells[rowCount + 7, 1].Tag = "A023";
                sheet.Cells[rowCount + 7, 2].Text = "上缴支票额";
                sheet.Cells[rowCount + 7, 3].Tag = "A024";
                sheet.Cells[rowCount + 7, 4].Text = "上缴账户额";
                sheet.Cells[rowCount + 7, 5].Tag = "A025";
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 设置FarPoint显示
        /// </summary>
        /// <param name="sheet"></param>
        public virtual void SetFarPoint()
        {
            FarPoint.Win.Spread.SheetView sheet = this.neuSpread1_Sheet1;

            SetFpStyle(sheet);
        }

        public void SetDetailName()
        {
           
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "西药费";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "化验费";
            this.neuSpread1_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "输血费";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "床位费";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = "中成药";
            this.neuSpread1_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).Value = "放射费";
            this.neuSpread1_Sheet1.Cells.Get(1, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 3).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 4).Value = "手术费";
            this.neuSpread1_Sheet1.Cells.Get(1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 5).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).Value = "护理费";
            this.neuSpread1_Sheet1.Cells.Get(1, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).Value = "中草药";
            this.neuSpread1_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 2).Value = "其他诊查费";
            this.neuSpread1_Sheet1.Cells.Get(2, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 3).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 4).Value = "其他治疗费";
            this.neuSpread1_Sheet1.Cells.Get(2, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 5).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 6).Value = "其他费用";
            this.neuSpread1_Sheet1.Cells.Get(2, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 7).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = "合计：";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).TabStop = true;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).Value = "大写:";
            this.neuSpread1_Sheet1.Cells.Get(3, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 3).ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells.Get(3, 3).Value = "";
        }

        #endregion

        #endregion

    }

    /// <summary>
    /// 显示的数据名
    /// </summary>
    public enum EnumCellName
    {
        /// <summary>
        /// 上缴现金（数字）
        /// </summary>
        A001,

        /// <summary>
        /// 有效发票数
        /// </summary>
        A002,

        /// <summary>
        /// 作废发票数
        /// </summary>
        A003,

        /// <summary>
        /// 作废金额
        /// </summary>
        A004,

        /// <summary>
        /// 使用票据号
        /// </summary>
        A005,

        /// <summary>
        /// 作废票据号
        /// </summary>
        A006,

        /// <summary>
        /// 职工医疗金额
        /// </summary>
        A007,

        /// <summary>
        /// 居民医疗金额
        /// </summary>
        A008,

        /// <summary>
        /// 特约单位金额
        /// </summary>
        A009,

        /// <summary>
        /// 医疗优惠金额
        /// </summary>
        A010,

        /// <summary>
        /// 特定门诊金额
        /// </summary>
        A011,

        /// <summary>
        /// 刷卡结算金额
        /// </summary>
        A012,

        /// <summary>
        /// 现金结算金额
        /// </summary>
        A013
    }
}
