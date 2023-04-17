using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.Operation
{
    /// <summary>
    /// beijiao LED
    /// </summary>
    public partial class frmOpsLEDShowByDoc : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmOpsLEDShowByDoc()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            button1.Click +=new EventHandler(button1_Click);
        }

        int curPageNo = 0;

        int rowCount = 8;

        void timer2_Tick(object sender, EventArgs e)
        {
            if (this.neuOperationSpread_汇总.Rows.Count > 0)
            {
                for (int rowIndex = 0; rowIndex < this.neuOperationSpread_汇总.RowCount; rowIndex++)
                {
                    if (rowIndex >= curPageNo * rowCount
                        && rowIndex < (curPageNo + 1) * rowCount)
                    {
                        neuOperationSpread_汇总.Rows[rowIndex].Visible = true;
                    }
                    else
                    {
                        neuOperationSpread_汇总.Rows[rowIndex].Visible = false;
                    }
                }

                curPageNo += 1;

                if (curPageNo >= Math.Ceiling((decimal)neuOperationSpread_汇总.RowCount / rowCount))
                {
                    curPageNo = 0;
                }
            }
        }

        public delegate void CancelButtonClickHandler();
        public CancelButtonClickHandler CancelButtonClickEvent;

        private int timeInterval = 30000;
        public int TimeInterval
        {
            get { return timeInterval; }
            set 
            { 
                timeInterval = value;
                timer1.Interval = value;
            }
        }

        private int timeInterval1 = 15000;
        public int TimeInterval1
        {
            get { return timeInterval1; }
            set
            {
                timeInterval1 = value;
                timer2.Interval = value;
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            QueryData();
        }

        private FS.HISFC.BizLogic.Operation.OpsTableManage opsTableMgr = new FS.HISFC.BizLogic.Operation.OpsTableManage();

        public int Show()
        {
            this.QueryData();
            this.FindForm().Show();
            this.FindForm().TopMost = false;
            return 1;
        }

        private void QueryData()
        {
            this.Clear();
            string strSql = string.Empty;
            if (this.opsTableMgr.Sql.GetSql("Operator.Operator.LED.DocShow", ref strSql) == -1)
            {
                MessageBox.Show("没有找到SQL语句：");
                return;
            }
            strSql = string.Format(strSql, ((FS.HISFC.Models.Base.Employee)this.opsTableMgr.Operator).Dept.ID, DateTime.Now.ToShortDateString(), (DateTime.Now.AddDays(1)).ToShortDateString());
            DataSet ds = new DataSet();
            this.opsTableMgr.ExecQuery(strSql, ref ds);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                this.neuOperationSpread_汇总.DataSource = dt;
            }
            this.SetFarpointFormat();
        }

        private void Clear()
        {
            this.neuOperationSpread_汇总.Rows.Count = 0;
        }

        private void SetFarpointFormat()
        {
            this.nlbDate.Text = "日期：" + DateTime.Now.ToShortDateString();
            this.lblWindow.Location = new Point((1600 - this.lblWindow.Width) / 2, this.lblWindow.Location.Y);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            textCellType1.WordWrap = true;
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 62F;//手术间
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 35F;//手术台
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 153F;//科室
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 50F;//床号
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 115F;//姓名
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 32F;//性别
            this.neuOperationSpread_汇总.Columns.Get(6).Width = 116F;//住院号
            this.neuOperationSpread_汇总.Columns.Get(7).Width = 77F;//年龄
            this.neuOperationSpread_汇总.Columns.Get(8).Width = 171F;//术前诊断
            this.neuOperationSpread_汇总.Columns.Get(9).Width = 211F;//手术名称
            this.neuOperationSpread_汇总.Columns.Get(10).Width = 0F;//感染类型
            this.neuOperationSpread_汇总.Columns.Get(11).Width = 107F;//主刀医生
            this.neuOperationSpread_汇总.Columns.Get(12).Width = 0F;//一助
            this.neuOperationSpread_汇总.Columns.Get(13).Width = 107F;//麻醉医生
            this.neuOperationSpread_汇总.Columns.Get(14).Width = 0F;//麻醉一助
            this.neuOperationSpread_汇总.Columns.Get(15).Width = 107F;//麻醉方式
            this.neuOperationSpread_汇总.Columns.Get(16).Width = 107F;//洗手护士
            this.neuOperationSpread_汇总.Columns.Get(17).Width = 107F;//巡回护士
            this.neuOperationSpread_汇总.Columns.Get(18).Width = 0F;//特殊说明

            this.neuOperationSpread_汇总.Columns.Get(1).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(2).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(3).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(4).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(5).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(6).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(7).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(8).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(9).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(10).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(11).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(12).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(13).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(14).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(15).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(16).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(17).CellType = textCellType1;
            this.neuOperationSpread_汇总.Columns.Get(18).CellType = textCellType1;
            if (this.neuOperationSpread_汇总.Rows.Count > 0)
            {
                for (int rowIndex = 0; rowIndex < this.neuOperationSpread_汇总.Rows.Count; rowIndex++)
                {
                    if (rowIndex >= rowCount)
                    {
                        this.neuOperationSpread_汇总.Rows[rowIndex].Visible = false;
                    }
                    this.neuOperationSpread_汇总.Rows[rowIndex].Height = 110F;
                }
            }
        }

        public int Close()
        {
            this.FindForm().Hide();
            return 1;
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            #region 设定显示大小

            if (Screen.AllScreens.Length > 1)
            {
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            else
            {
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            #endregion
        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CancelButtonClickEvent();
        }
    }
}
