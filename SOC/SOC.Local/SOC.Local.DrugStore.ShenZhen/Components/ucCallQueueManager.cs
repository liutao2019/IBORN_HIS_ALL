using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FS.SOC.Local.DrugStore.ShenZhen.Components
{
    public partial class ucCallQueueManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCallQueueManager()
        {
            InitializeComponent();
            this.Init();
        }



        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.lbShow.Text = "";
            this.lbShow.Text = "空闲";
            DateTime dt = this.drugStoreAsignMgr.GetDateTimeFromSysDateTime();
            this.dtpAutoPrintBegin.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            this.isAutoRefresh.Checked = false;
            this.isCanRefresh = this.isAutoRefresh.Checked;
            FS.HISFC.Models.Base.Employee employee = this.drugStoreAsignMgr.Operator as FS.HISFC.Models.Base.Employee;
            this.curDept  = employee.Dept;
            this.lbDeptName.Text  = this.curDept.Name;
        }


        /// <summary>
        /// 是否正在运行
        /// </summary>
        private bool isStart = false;

        private System.Collections.Queue queue = System.Collections.Queue.Synchronized(new System.Collections.Queue());

        private FS.SOC.Local.DrugStore.ShenZhen.Bizlogic.DrugStoreAsign drugStoreAsignMgr = new FS.SOC.Local.DrugStore.ShenZhen.Bizlogic.DrugStoreAsign();

        FS.SOC.Local.DrugStore.ShenZhen.Common.CallSpeak callSpeak = new FS.SOC.Local.DrugStore.ShenZhen.Common.CallSpeak();

        FS.FrameWork.Models.NeuObject curDept = new FS.FrameWork.Models.NeuObject();

        public bool isAutoRefreshPrint = true;
        [Description("是否自动刷新打印"), Category("设置")]
        public bool IsAutoRefreshPrint
        {
            get
            {
                return isAutoRefreshPrint;
            }
            set
            {
                isAutoRefreshPrint = value;
            }
        }

        /// <summary>
        /// 当前是否可以刷新，如果正在打印则不刷
        /// </summary>
        public  bool isCanRefresh = true;
        public bool IsCanRefresh
        {
            get
            {
                return isCanRefresh;
            }
            set
            {
                isCanRefresh = value;
            }
        }

        /// <summary>
        /// 刷新间隔，默认10秒
        /// </summary>
        private int freshTimes = 5;

        /// <summary>
        /// 刷新间隔，默认10秒
        /// </summary>
        [Description("刷新间隔，单位秒，默认10秒"), Category("设置")]
        public int FreshTimes
        {
            get
            {
                return freshTimes;
            }
            set
            {
                freshTimes = value;
                this.timer1.Interval = value * 1000;
            }
        }




        /// <summary>
        /// 叫分诊
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="noon"></param>
        public void CallAssign(FS.FrameWork.Models.NeuObject priveDept)
        {
            this.curDept = priveDept.Clone();
            //往队列里面加入次数
            object[] o = new object[] { priveDept };
            //进入队列
            queue.Enqueue(o);

            //启动异步调用
            if (!isStart)
            {
                Thread t = new Thread(new ThreadStart(this.AssignCallback));
                t.Start();
            }
        }

        /// <summary>
        /// 异步调用叫号
        /// </summary>
        /// <param name="ar"></param>
        private void AssignCallback()
        {
            this.lbShow.Text = "";
            this.lbShow.Text = "开始叫号";
            try
            {
                isStart = true;

                isCanRefresh = false;

                //只要队列大于零，则开始叫号
                while (queue.Count > 0)
                {
                    Object[] o = queue.Dequeue() as Object[];
  
                    //根据护士站和午别查找所有的叫号申请信息
                    List<FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign> list = drugStoreAsignMgr.Query(this.curDept.ID);
                    if (list != null && list.Count > 0)
                    {
                        foreach (FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign in list)
                        {
                            //开始逐一叫号
                            callSpeak.Speech(drugStoreAsign);
                            //删除叫号信息
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            int retutnValue = this.drugStoreAsignMgr.DeleteByRecipeNO(drugStoreAsign.recipeNO,this.curDept.ID);
                            if (retutnValue == 1)
                            {
                                FS.FrameWork.Management.PublicTrans.Commit();
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                        }
                    }
                }
                isCanRefresh = true;
                this.lbShow.Text = "";
                this.lbShow.Text = "空闲";

            }
            catch (Exception e)
            {
                this.drugStoreAsignMgr.Err = e.Message;
                this.drugStoreAsignMgr.WriteErr();
            }
            finally
            {
                isStart = false;
            }
        }

        /// <summary>
        /// 自动刷新功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (!this.isAutoRefreshPrint)
            {
                return;
            }
            if (!this.isCanRefresh)
            {
                return;
            }
            this.CallAssign(this.curDept);

            
            this.isCanRefresh = true;
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "开始叫号")
            {
                this.CallAssign(this.curDept);
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("开始叫号", "开始叫号", FS.FrameWork.WinForms.Classes.EnumImageList.L累计开始, true, false, null);
            return toolBarService;
        }

        private void isAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isAutoRefresh.Checked)
            {
                this.isCanRefresh = true;
            }
            else
            {
                this.isCanRefresh = false;
            }
        }


    }
}
