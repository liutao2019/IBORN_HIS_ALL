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
    public partial class ucGeneralQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucGeneralQuery()
        {
            InitializeComponent();
        }


        #region 变量

        /// <summary>
        /// sqlid
        /// </summary>
        private string sqlID = string.Empty;

        /// <summary>
        /// sqlid
        /// </summary>
        [Description("SqlID"), Category("数据")]
        public string SqlID
        {
            get
            {
                return this.sqlID;
            }

            set
            {
                this.sqlID = value;
            }
        }

        /// <summary>
        /// 是否需要合计行：0不需要；1需要
        /// </summary>
        private string isNeedSum = "0";

        /// <summary>
        /// 是否需要合计行：0不需要；1需要
        /// </summary>
        [Category("查询设置"), Description("是否需要合计行：0不需要；1需要")]
        public string IsNeedSum
        {
            get
            {
                return this.isNeedSum;
            }
            set
            {
                this.isNeedSum = value;
            }
        }

        /// <summary>
        /// 第几列需要合计
        /// </summary>
        private string columnNO = "";

        [Category("查询设置"), Description("第几列需要合计")]
        public string ColumnNO
        {
            get
            {
                return this.columnNO;
            }
            set
            {
                this.columnNO = value;
            }
        }

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();

        #endregion

        #region 方法

        protected override int OnQuery(object sender, object neuObject)
        {
            //清空
            this.neuSpread1_Sheet1.Rows.Count = 0;

            DataSet dsResult = new DataSet();

            int i = feeMgr.ExecQuery(this.SqlID, ref dsResult, this.beginDate.Value.ToString(), this.endDate.Value.ToString());

            if (i < 0)
            {
                MessageBox.Show("执行SQL语句:" + this.SqlID + "失败!");
                return -1;
            }

            if (dsResult == null || dsResult.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }

            this.neuSpread1_Sheet1.DataSource = dsResult.Tables[0];

            if (this.IsNeedSum == "1")
            {
                this.AddSum();
            }

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 添加合计行
        /// </summary>
        public void AddSum()
        {
            if (string.IsNullOrEmpty(this.columnNO))
            {
                MessageBox.Show("请维护好需要合计列!");
                return;
            }

            string[] columns = this.columnNO.Split(';');
            decimal[] columnSum = new decimal[columns.Length];

            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    for (int j = 0; j < columns.Length; j++)
                    {
                        columnSum[j] += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, FS.FrameWork.Function.NConvert.ToInt32(columns[j])].Text);
                    }
                }

                int index = this.neuSpread1_Sheet1.Rows.Count;

                //增加一行
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Cells[index, 0].Text = "合计";
                for (int k = 0; k < columnSum.Length; k++)
                {
                    this.neuSpread1_Sheet1.Cells[index, FS.FrameWork.Function.NConvert.ToInt32(columns[k])].Text = columnSum[k].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return base.Export(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("A4", 827, 1170);

            this.neuPanel1.Dock = DockStyle.None;

            print.SetPageSize(ps);
            print.PrintPage(0, 0, this.neuPanel1);

            this.neuPanel1.Dock = DockStyle.Fill;
                        
            return base.OnPrint(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            DateTime dtNow = this.feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            this.endDate.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);

            base.OnLoad(e);
        }

        #endregion


    }
}
