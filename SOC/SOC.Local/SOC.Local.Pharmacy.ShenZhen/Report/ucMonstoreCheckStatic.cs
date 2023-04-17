using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Report
{
    public partial class ucMonstoreCheckStatic : FS.SOC.Local.Pharmacy.ShenZhen.Report.ucPrivePowerReport
    {
        public ucMonstoreCheckStatic()
        {
            InitializeComponent();
        }

        public void LoadInit()
        {
            this.OperationEndHandler += new DelegateOperateionEnd(this.operationEnd);
            this.DetailDoubleClickEnd += new DelegateDoubleClickEnd(this.doubleClick);
            //默认设置
            this.SetDefaultSetting();

            //需要附加标题

            this.IsNeedAdditionTitle = true;

            //二级权限为查询权限

            this.PriClassTwos = new string[] { "0330" };

            //初始化的时候查询数据

            this.QueryDataWhenInit = true;

            //标题
            this.Titles = new string[] { "盘点统计表", "日期：", "", "" };

            //汇总sql
            string[] sqlTots = { "Report.Pharmacy.MonthStore.CheckStatic" };
            this.SQLIndexs = sqlTots;

            //需要明细查询

            //SetDefaultSetting()调用后[默认是不查询明细的]
            this.IsNeedDetailData = true;

            //查询方式
            //O表示和汇总同步查询数据且使用相同条件
            //C表示双击时使用固定条件查询

            //R表示双击时使用farPoint中的活动行取条件
            this.DetailDataQueryType = "R";
            this.QueryConditionColIndexs = new int[] { 0, 1 };

            //明细sql
            this.DetailSQLIndexs = new string[] { "Report.Pharmacy.MonthStore.CheckStaticDetail" };

            //汇总的合计列，可以超过实际数据的总列数

            this.SumColIndexs = new int[] { 3, 4, 5 };
            this.SumDetailColIndexs = new int[] { 11, 12, 13, 14 };

            this.panelDept.Visible = true;
            this.panelTime.Visible = true;
            //按照以上条件初始化

            this.Init();

            //过滤方式为汇总和明细同时过滤
            //Tot汇总过滤

            //Det明细过滤
            //SetDefaultSetting()调用后[默认是不过滤的]
            this.FilerType = "Det";

            //汇总过滤字段

            //this.Filters = new string[] { "住院号", "科室名", "药名", "姓名", "性质", "拼音码" };

            //明细过滤字段
            this.DetailFilters = new string[] { "单号", "名称", "拼音码" };
        }
        private void operationEnd(string type)
        {
            if (type == "query")
            {
                try
                {
                    this.fpSpread1_Sheet1.Columns[2].Width = 120F;
                    this.fpSpread1_Sheet1.Columns[1].Visible = false;
                    this.fpSpread1_Sheet1.Columns[3].Width = 120F;
                    this.fpSpread1_Sheet1.Columns[4].Width = 120F;
                    this.fpSpread1_Sheet1.Columns[5].Width = 160F;
                    this.fpSpread1_Sheet1.Columns[6].Width = 160F;
                    FarPoint.Win.Spread.CellType.DateTimeCellType d = new FarPoint.Win.Spread.CellType.DateTimeCellType();
                    d.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.LongDateWithTime;

                    this.fpSpread1_Sheet1.Columns[5].CellType = d;
                    this.fpSpread1_Sheet1.Columns[6].CellType = d;
                    //this.fpSpread1_Sheet2.Columns[3].Width = 120F;
                    //this.fpSpread1_Sheet2.Columns[4].Width = 120F;
                }
                catch { }
                this.lbMainTitle.Text = this.cmbDept.Text + "盘点统计表";
                this.lbAdditionTitleLeft.Text = "日期："
                    + this.dtStart.Value.ToString("yyyy年MM月dd日 HH:mm:ss")
               + " - " + this.dtEnd.Value.ToString("yyyy年MM月dd日 HH:mm:ss");
            }
        }
        private void doubleClick()
        {
            try
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                for (int i = 0; i < this.fpSpread1_Sheet2.ColumnCount; i++)
                {
                    this.fpSpread1_Sheet2.Columns[i].Width = 60F;

                }
                this.fpSpread1_Sheet2.Columns[this.fpSpread1_Sheet2.ColumnCount - 1].Width = 120F;
                this.fpSpread1_Sheet2.Columns[this.fpSpread1_Sheet2.ColumnCount - 2].Width = 120F;
                this.fpSpread1_Sheet2.Columns[this.fpSpread1_Sheet2.ColumnCount - 3].Width = 120F;
            }
            catch { }

        }
    }
}
