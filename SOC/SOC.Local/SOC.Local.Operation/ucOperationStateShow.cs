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
    public partial class ucOperationStateShow : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 手术室状态管理
        /// addby cao-lin
        /// </summary>
        public ucOperationStateShow()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            frmOpsLEDShow = new frmOpsLEDShow();
            frmOpsLEDShow.CancelButtonClickEvent += new frmOpsLEDShow.CancelButtonClickHandler(this.CloseScreen);
            frmOpsLEDShow.TimeInterval = TimeInterval;
            frmOpsLEDShow.TimeInterval1 = TimeInterval1;
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
        frmOpsLEDShow frmOpsLEDShow = null;

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
            if (tableManager.Sql.GetSql("Operator.Operator.LED.PatientShow", ref strSql) == -1)
            {
                MessageBox.Show("没有找到SQL语句：");
                return;
            }
            strSql = string.Format(strSql, DateTime.Now, ((FS.HISFC.Models.Base.Employee)tableManager.Operator).Dept.ID);
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
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 154F;
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 228;
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 85;
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 126F;
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 88F;
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 173F;
            this.neuOperationSpread_汇总.Columns.Get(6).Width = 199F;
            if (this.neuOperationSpread_汇总.Rows.Count > 0)
            {
                for (int index = 0; index < this.neuOperationSpread_汇总.Rows.Count; index++)
                {
                    this.neuOperationSpread_汇总.Rows[index].Height = 30F;
                    switch (this.neuOperationSpread_汇总.Cells[index, 6].Text)
                    {
                        case "术中":
                            this.neuOperationSpread_汇总.Rows[index].BackColor = System.Drawing.Color.Blue;
                            break;
                        default:
                            break;
                    }
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
            this.frmOpsLEDShow.Close();
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
                this.frmOpsLEDShow.Close();
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
                frmOpsLEDShow.TimeInterval = TimeInterval;
                frmOpsLEDShow.TimeInterval1 = TimeInterval1;
                this.frmOpsLEDShow.Show();
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
