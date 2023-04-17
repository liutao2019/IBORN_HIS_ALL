using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace SOC.Local.Operation
{
    public partial class ucOperationStateShowByDoc : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 手术室状态管理
        /// addby cao-lin
        /// </summary>
        public ucOperationStateShowByDoc()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            frmOpsLEDShowByDoc = new frmOpsLEDShowByDoc();
            frmOpsLEDShowByDoc.CancelButtonClickEvent += new frmOpsLEDShowByDoc.CancelButtonClickHandler(this.CloseScreen);
            frmOpsLEDShowByDoc.TimeInterval = TimeInterval;
            frmOpsLEDShowByDoc.TimeInterval1 = TimeInterval1;
        }


        void timer1_Tick(object sender, EventArgs e)
        {
            this.QueryOnLoad();
        }

        #region 属性及变量
        private int timeInterval = 30000;
        [Description("外屏显示刷新间隔时间"),Category("设置"),Browsable(true)]
        public int TimeInterval
        {
            get { return timeInterval; }
            set { timeInterval = value; }
        }

        private int timeInterval1 = 15000;
        [Description("外屏显示刷新间隔时间1"), Category("设置"), Browsable(true)]
        public int TimeInterval1
        {
            get { return timeInterval1; }
            set { timeInterval1 = value; }
        }
        #endregion


        /// <summary>
        /// 手术管理类
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Operation.Operation operationManager = new FS.HISFC.BizProcess.Integrate.Operation.Operation();

        /// <summary>
        /// 手术台手术间管理类
        /// </summary>
        public static FS.HISFC.BizLogic.Operation.OpsTableManage tableManager = new FS.HISFC.BizLogic.Operation.OpsTableManage();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 手术台序管理类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper OperationOrderHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 手术大屏幕显示
        /// </summary>
        frmOpsLEDShowByDoc frmOpsLEDShowByDoc = null;

        #region 方法
        protected override void OnLoad(EventArgs e)
        {
          
            //初始化设置
            this.Init();

            //查询数据
            this.QueryOnLoad();
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        private void QueryOnLoad()
        {
            this.Clear();
            string strSql = string.Empty;
            if (tableManager.Sql.GetSql("Operator.Operator.LED.DocShow", ref strSql) == -1)
            {
                MessageBox.Show("没有找到SQL语句：");
                return;
            }
            strSql = string.Format(strSql, ((FS.HISFC.Models.Base.Employee)tableManager.Operator).Dept.ID, this.neuOperationDate.Value.ToShortDateString(), (this.neuOperationDate.Value.AddDays(1)).ToShortDateString());
            DataSet ds = new DataSet();
            tableManager.ExecQuery(strSql, ref ds);
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 37F;//手术间
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 34F;//手术台
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 128F;//科室
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 35F;//床号
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 67F;//姓名
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 34F;//性别
            this.neuOperationSpread_汇总.Columns.Get(6).Width = 67F;//住院号
            this.neuOperationSpread_汇总.Columns.Get(7).Width = 53F;//年龄
            this.neuOperationSpread_汇总.Columns.Get(8).Width = 160;//术前诊断
            this.neuOperationSpread_汇总.Columns.Get(9).Width = 160F;//手术名称
            this.neuOperationSpread_汇总.Columns.Get(10).Width = 39F;//感染类型
            this.neuOperationSpread_汇总.Columns.Get(11).Width = 50F;//主刀医生
            this.neuOperationSpread_汇总.Columns.Get(12).Width = 0F;//一助
            this.neuOperationSpread_汇总.Columns.Get(13).Width = 50F;//麻醉医生
            this.neuOperationSpread_汇总.Columns.Get(14).Width = 0F;//麻醉一助
            this.neuOperationSpread_汇总.Columns.Get(15).Width = 50F;//麻醉方式
            this.neuOperationSpread_汇总.Columns.Get(16).Width = 50F;//洗手护士
            this.neuOperationSpread_汇总.Columns.Get(17).Width = 50F;//巡回护士
            this.neuOperationSpread_汇总.Columns.Get(18).Width = 140F;//特殊说明

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
                    this.neuOperationSpread_汇总.Rows[rowIndex].Height = 50F;
                }
            }

        }


        /// <summary>
        /// 初始化设置
        /// </summary>
        private void Init()
        {
            //初始化日期
            this.InitDateTime();

            //加载常数字典
            this.InitCons();
   
        }

        /// <summary>
        /// 加载常数字典
        /// </summary>
        private void InitCons()
        {
            ArrayList alOperatoinOrder = consMgr.GetAllList("OperatoinOrder");
            OperationOrderHelper.ArrayObject = alOperatoinOrder;
        }


        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("屏显（开）", "屏显（开）", 0, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.ToString())
            {
                case "屏显（开）":
                    this.SetScreen(false);
                    break;
                case "屏显（关）"://{E1BE1043-5BDA-4c1d-94F0-8C2D5FF84F9F}
                    this.SetScreen(true);
                    break;
            }

        }

        private void CloseScreen()
        {
            ToolStripButton tsb = this.toolBarService.GetToolButton("屏显（开）");
            if (tsb == null)
            {
                tsb = this.toolBarService.GetToolButton("屏显（关）");
            }

            if (tsb == null)
            {
                return;
            }

            tsb.Text = "屏显（开）";
            this.frmOpsLEDShowByDoc.Close();
        }

        private void SetScreen(bool isClose)
        {
            ToolStripButton tsb = this.toolBarService.GetToolButton("屏显（开）");
            if (tsb == null)
            {
                tsb = this.toolBarService.GetToolButton("屏显（关）");
            }

            if (tsb == null)
            {
                return;
            }

            if (isClose)
            {
                tsb.Text = "屏显（开）";
                this.frmOpsLEDShowByDoc.Close();
            }
            else
            {
                if (Screen.AllScreens.Length <= 1)
                {
                    if (MessageBox.Show("您的电脑只连接了一个屏幕，是否确认显示手术患者外屏？\r\n\r\n显示手术患者外屏可能会影响您的正常操作！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                tsb.Text = "屏显（关）";
                frmOpsLEDShowByDoc.TimeInterval = TimeInterval;
                frmOpsLEDShowByDoc.TimeInterval1 = TimeInterval1;
                this.frmOpsLEDShowByDoc.Show();
            }
        }

        /// <summary>
        /// 初始化日期
        /// </summary>
        private void InitDateTime()
        {
            this.neuOperationDate.Value = DateTime.Now.AddDays(0);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryOnLoad();
            return 1;
        }
        #endregion
    }
}
