using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.QiaoTou
{
    public partial class ucQueryItemReportForOutPatient : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryItemReportForOutPatient()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 报表业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();

        /// <summary>
        /// 项目标志：0非药品；1药品
        /// </summary>
        private string itemFlag = "0";

        /// <summary>
        /// 项目标志：0非药品；1药品
        /// </summary>
        [Category("查询设置"), Description("项目标志：0非药品；1药品")]
        public string ItemFlag
        {
            get
            {
                return this.itemFlag;
            }
            set
            {
                this.itemFlag = value;
            }
        }

        /// <summary>
        /// 报表标题
        /// </summary>
        private string titleName = "";

        /// <summary>
        /// 报表标题
        /// </summary>
        [Category("查询设置"), Description("报表标题")]
        public string TitleName
        {
            get
            {
                return this.titleName;
            }
            set
            {
                this.titleName = value;
            }
        }

        /// <summary>
        /// 使用该报表的人员
        /// </summary>
        private string operCode = "";

        /// <summary>
        /// 使用该报表人员
        /// </summary>
        [Category("查询设置"),Description("使用该报表人员:空值为任何人都可以查询;")]
        public string OperCode
        {
            get
            {
                return this.operCode;
            }
            set
            {
                this.operCode = value;
            }
        }

        /// <summary>
        /// 每页打印行数
        /// </summary>
        private int lineCount = 32;

        /// <summary>
        /// 每页打印行数
        /// </summary>
        [Description("每页打印行数"), Category("打印设置")]
        public int LineCount
        {
            get
            {
                return this.lineCount;
            }
            set
            {
                this.lineCount = value;
            }
        }


        #endregion

        #region 方法

        /// <summary>
        /// 项目加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            //初始化时间
            DateTime dtNow = feeMgr.GetDateTimeFromSysDateTime();
            //this.beginDate.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            this.beginDate.Value = new DateTime(dtNow.Year, dtNow.Month, 1, 0, 0, 0);
            this.endDate.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);

            //初始化项目列表
            if (this.ItemFlag == "0")
            {
                FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                ArrayList listItem = itemMgr.QueryValidItems();
                if (listItem == null)
                {
                    MessageBox.Show("初始化项目失败!");
                    return;
                }
                this.cmbQueryItem.AddItems(listItem);
            }
            else if (this.itemFlag == "1")
            {
                FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
                List<FS.HISFC.Models.Pharmacy.Item> listItem = itemMgr.QueryItemAvailableList();

                if (listItem == null)
                {
                    MessageBox.Show("初始化项目失败!");
                    return;
                }

                ArrayList listItemTemp = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Item item in listItem)
                {
                    listItemTemp.Add(item);
                }
                
                this.cmbQueryItem.AddItems(listItemTemp);
            }

            this.InitFP();

            //如果不符合查询条件的人，不让进这个界面
            if (this.Valid() == -1)
            {
                MessageBox.Show("你没有权限使用此菜单!", "警告");
                this.neuPrint.Visible = false;
                this.neuGroupBox1.Visible = false;
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化FP列宽
        /// </summary>
        private void InitFP()
        {
            this.neuSpread1_Sheet1.Columns[0].Width = 100;
            this.neuSpread1_Sheet1.Columns[1].Width = 75;
            this.neuSpread1_Sheet1.Columns[2].Width = 95;
            this.neuSpread1_Sheet1.Columns[3].Width = 75;
            this.neuSpread1_Sheet1.Columns[4].Width = 100;
            this.neuSpread1_Sheet1.Columns[5].Width = 100;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (string.IsNullOrEmpty(this.cmbQueryItem.Tag.ToString()))
            {
                MessageBox.Show("请选择要查询的项目!");
                return -1;
            }

            if (this.Valid() == -1)
            {
                MessageBox.Show("你没有权限查询该报表!");
                return -1;
            }

            this.QueryReport();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 查询报表
        /// </summary>
        /// <returns></returns>
        private int QueryReport()
        {
            //清空
            this.Clear();

            string begin = this.beginDate.Value.ToString();
            string end = this.endDate.Value.ToString();
            string itemCode = this.cmbQueryItem.Tag.ToString();
            DataSet dsResult = new DataSet();

            if (this.ItemFlag == "0")
            {
                if (this.feeMgr.QueryItemReportForOutPatient(begin, end, itemCode, "0", ref dsResult) == -1)
                {
                    MessageBox.Show(this.feeMgr.Err);
                    return -1;
                }

                this.ShowData(dsResult, itemCode);
            }
            else if (this.ItemFlag == "1")
            {
                if (this.feeMgr.QueryItemReportForOutPatient(begin, end, itemCode, "1", ref dsResult) == -1)
                {
                    MessageBox.Show(this.feeMgr.Err);
                    return -1;
                }

                this.ShowData(dsResult, itemCode);
            }

            return 1;
        }

        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        private void ShowData(DataSet dsResult, string itemCode)
        {
            //设置报表显示内容
            this.neuSpread1_Sheet1.Rows.Count = 0;
            int index = this.neuSpread1_Sheet1.Rows.Count;
            DataTable dt = dsResult.Tables[0];
            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();

            decimal amount = 0m;
            decimal totCost = 0m;

            foreach (DataRow dr in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString();
                this.neuSpread1_Sheet1.Cells[index, 1].CellType = txtType;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();
                this.neuSpread1_Sheet1.Cells[index, 2].Text = dr[2].ToString();
                this.neuSpread1_Sheet1.Cells[index, 3].Text = dr[3].ToString();
                this.neuSpread1_Sheet1.Cells[index, 4].Text = dr[4].ToString();
                this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[5].ToString();

                amount += FS.FrameWork.Function.NConvert.ToDecimal(dr[4].ToString());
                totCost += FS.FrameWork.Function.NConvert.ToDecimal(dr[5].ToString());

                index++;
            }
            //>>{D3D3905D-0338-4617-BEE0-04C278F2B481}屏蔽合计行，sql实现合计20120724kjl
            //index = this.neuSpread1_Sheet1.Rows.Count;
            //this.neuSpread1_Sheet1.Rows.Add(index, 1);
            ////this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 2;
            //this.neuSpread1_Sheet1.Cells[index, 0].Text = "合计";
            //this.neuSpread1_Sheet1.Cells[index, 4].Text = amount.ToString();
            //this.neuSpread1_Sheet1.Cells[index, 5].Text = totCost.ToString();
            //<<
            //for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            //{
            //    this.neuSpread1_Sheet1.Columns[i].Width = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
            //}

            this.InitFP();

            //标题
            this.neuLblTitle.Text = this.TitleName;

            //设置标题位置
            float spreadWidth = 0;
            foreach (FarPoint.Win.Spread.Column fpColumn in this.neuSpread1_Sheet1.Columns)
            {
                spreadWidth += fpColumn.Width;
            }

            if (spreadWidth > this.neuPrint.Width)
            {
                spreadWidth = this.neuPrint.Width;
            }

            spreadWidth = spreadWidth - this.neuLblTitle.Width;
            int titleX = FS.FrameWork.Function.NConvert.ToInt32( (spreadWidth / 2) );
            if (titleX <= 0)
            {
                titleX = 1;
            }

            this.neuLblTitle.Location = new Point(titleX, this.neuLblTitle.Location.Y);

            this.neuDate.Text = "统计日期：" + this.beginDate.Value.ToString().Substring(0, 10) + " 至 " + this.endDate.Value.ToString().Substring(0, 10) ;//+ "   " + this.feeMgr.Operator.Name;

            if (this.ItemFlag == "0")
            {
                FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                FS.HISFC.Models.Fee.Item.Undrug undrug = itemMgr.GetUndrugByCode(itemCode);
                if (undrug == null)
                {
                    MessageBox.Show("查询项目编码为: " + itemCode + " 失败!");
                    return;
                }
                this.neuMemo.Text = "收费编码：" + undrug.UserCode + "     收费名称：" + undrug.Name;
            }
            else if (this.ItemFlag == "1")
            {
                FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
                FS.HISFC.Models.Pharmacy.Item pharmacy = itemMgr.GetItem(itemCode);
                if (pharmacy == null)
                {
                    MessageBox.Show("查询项目编码为：" + itemCode + " 失败!");
                    return;
                }
                this.neuMemo.Text = "收费编码：" + pharmacy.UserCode + "   项目名称：" + pharmacy.Name;
            }

        }

        /// <summary>
        /// 是否符合查询条件
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (string.IsNullOrEmpty(this.OperCode))
            {
                return 1;
            }
            else
            {
                if (this.operCode.Contains(this.feeMgr.Operator.ID))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.neuLblTitle.Text = "标题";
            this.neuDate.Text = "统计信息";
            this.neuMemo.Text = "查询项目";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        private void ExportInfo()
        {
            
            bool tr = false;
            string fileName = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "excel|*.xls";
            saveFile.Title = "导出到Excel";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFile.FileName))
                {
                    fileName = saveFile.FileName;
                    tr = this.neuSpread1.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders, new FarPoint.Excel.ExcelWarningList());
                }
                else
                {
                    MessageBox.Show("文件名字不能为空!");
                    return;
                }

                if (tr)
                {
                    MessageBox.Show("导出成功!");
                }
                else
                {
                    MessageBox.Show("导出失败!");
                }

            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return 1;
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize ps = null;
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            //ps = pgMgr.GetPageSize("ClinicFeedetail");

            if (ps == null)
            {
                //默认为A4纸张
                ps = new FS.HISFC.Models.Base.PageSize("A4", 827, 1169);
            }

            print.SetPageSize(ps);

            int fromPage = 1;
            int toPage = (System.Int32)Math.Ceiling((double)this.neuSpread1_Sheet1.Rows.Count / lineCount);

            //打印的时候，先将属性改为none;
            this.neuPrint.Dock = DockStyle.None;
            for (int i = fromPage; i <= toPage; i++)
            {
                for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
                {
                    if (j >= (i - 1) * this.lineCount && (j + 1) <= i * this.lineCount)
                    {
                        this.neuSpread1_Sheet1.Rows[j].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[j].Visible = false;
                    }
                }

                print.PrintPage(50, 0, this.neuPrint);
            }

            //打印完之后修改回来
            this.neuPrint.Dock = DockStyle.Fill;

            //打印完之后全部显示
            for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
            {
                this.neuSpread1_Sheet1.Rows[k].Visible = true;
            }


            return base.OnPrint(sender, neuObject);
        }

        #endregion
    }
}
