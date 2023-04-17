using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 执行单打印]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucExecBill : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucExecBill()
        {
            InitializeComponent();
        }

        #region 变量
        private FS.HISFC.BizLogic.Order.ExecBill Bill = new FS.HISFC.BizLogic.Order.ExecBill();
        
        /// <summary>
        /// 是否补打
        /// </summary>
        bool IsRePrint = false;

        /// <summary>
        /// 是否首日量
        /// </summary>
        bool IsFirst = false; 

        /// <summary>
        /// 执行单执行时间
        /// </summary>
        string Memo = "";

        /// <summary>
        /// 设置护士站看到医嘱的类型,逗号隔开 会诊:CONS 科室:DEPTXXX 医技:DEPTXXX 其他:OTHER"
        /// </summary>
        string speOrderType = "";

        protected List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;

        /// <summary>
        /// 当前接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPrintTransFusion ip = null;

        /// <summary>
        /// 是否显示首日量打印
        /// </summary>
        private bool isShowFirstDay = false;

        /// <summary>
        /// 是否显示首日量打印
        /// </summary>
        [Category("打印设置"), Description("是否显示首日量打印")]
        public bool IsShowFirstDay
        {
            get
            {
                return isShowFirstDay;
            }
            set
            {
                isShowFirstDay = value;
            }
        }

        /// <summary>
        /// 停止的医嘱是否打印（停止时间之前的）
        /// </summary>
        private bool dcIsPrint = true;

        /// <summary>
        /// 停止的医嘱是否打印（停止时间之前的）
        /// </summary>
        [Category("打印设置"), Description("停止的医嘱是否打印（停止时间之前的）")]
        public bool DCIsPrint
        {
            get
            {
                return dcIsPrint;
            }
            set
            {
                dcIsPrint = value;
            }
        }

        /// <summary>
        /// 未收费是否打印
        /// </summary>
        private bool noFeeIsPrint = true;

        /// <summary>
        /// 未收费是否打印
        /// </summary>
        [Category("打印设置"), Description("未收费是否打印（默认打印，显示未收费！）")]
        public bool NoFeeIsPrint
        {
            get
            {
                return noFeeIsPrint;
            }
            set
            {
                noFeeIsPrint = value;
            }
        }

        /// <summary>
        /// 退费是否打印
        /// </summary>
        private bool quitFeeIsprint = true;

        /// <summary>
        /// 退费是否打印
        /// </summary>
        [Category("打印设置"), Description("退费是否打印")]
        public bool QuitFeeIsPrint
        {
            get
            {
                return quitFeeIsprint;
            }
            set
            {
                quitFeeIsprint = value;
            }
        }

        /// <summary>
        /// 设置护士站看到医嘱的类型,逗号隔开
        /// </summary>
        [Category("控件设置"), Description("设置护士站看到医嘱的类型,逗号隔开 会诊:CONS 科室:DEPTXXX 医技:DEPTXXX 其他:OTHER")]
        public string SpeOrderType
        {
            set
            {
                this.speOrderType = value;
            }
            get
            {
                return this.speOrderType;
            }
        }

        /// <summary>
        /// 默认的执行结束时间
        /// </summary>
        private string endTime = "12:01:00";

        /// <summary>
        /// 查询截止时间
        /// </summary>
        [Category("查询设置"), Description("默认的查询结束时间，如 12:01:00")]
        public string EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        /// <summary>
        /// 开始时间距今的间隔天数
        /// </summary>
        private int beginDateSpanDay = 0;

        /// <summary>
        /// 开始时间距今的间隔天数
        /// </summary>
        [Category("查询设置"), Description("默认的查询开始时间距今的间隔天数")]
        public int BeginDateSpanDay
        {
            get
            {
                return beginDateSpanDay;
            }
            set
            {
                beginDateSpanDay = value;
            }
        }

        /// <summary>
        /// 默认的查询结束时间距今的间隔天数
        /// </summary>
        private int endDateSpanDay = 1;

        /// <summary>
        /// 默认的查询结束时间距今的间隔天数
        /// </summary>
        [Category("查询设置"), Description("默认的查询结束时间距今的间隔天数")]
        public int EndDateSpanDay
        {
            get
            {
                return endDateSpanDay;
            }
            set
            {
                endDateSpanDay = value;
            }
        }

        /// <summary>
        /// 查询开始时间
        /// </summary>
        string beginTime = "12:01:00";

        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Category("查询设置"), Description("默认的查询结束时间,如：12:01:00")]
        public string BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Init();

        }


        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            ResetPanel();

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            this.dateTimePicker1.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 00, 00, 00);
            this.dateTimePicker2.Value = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 01, 00);
            try
            {
                if (!string.IsNullOrEmpty(beginTime))
                {
                    DateTime dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
                    this.dateTimePicker1.Value = dtBegin;
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
                    this.dateTimePicker2.Value = dtEnd;
                }
            }
            catch
            { }
        }

        public int Retrieve()
        {
            // TODO:  添加 ucDrugCardPanel.Retrieve 实现
            if (this.tabControl1.TabPages.Count <= 0)
                return 0;
            FS.FrameWork.Models.NeuObject obj = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag);
            string BillNo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).ID;
            //this.IsRePrint = false;
            this.Memo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).User01;
            this.Query(BillNo);
            return 0;
        }

        private void Query(string billNo)
        {
            if (this.tabControl1.TabPages.Count <= 0)
            {
                MessageBox.Show("请维护接口");
                return;
            }
            if (this.myPatients == null || this.myPatients.Count == 0)
            {
                MessageBox.Show("没有选择患者信息。");
                return;
            }

            IsRePrint = this.chkRePrint.Checked;
            IsFirst = this.chkFirst.Checked;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询执行单信息...");
            Application.DoEvents();

            if (this.tabControl1.SelectedTab.Controls[0].Controls.Count == 0)
            {
                //当前Tab页里面还没有输液卡
                object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucExecBill), typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion));
                if (o == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("请维护HISFC.Components.Order.Controls.ucExecBill里面接口FS.HISFC.BizProcess.Integrate.IPrintTransFusion的实例对照！");
                    return;
                }

                ip = o as FS.HISFC.BizProcess.Interface.IPrintTransFusion;

                ((Control)o).Tag = tabControl1.SelectedTab.Text;
                ((Control)o).Visible = true;
                ((Control)o).Dock = DockStyle.Fill;
                this.tabControl1.SelectedTab.Controls[0].Controls.Add((Control)o);
            }
            else
            {
                ip = this.tabControl1.SelectedTab.Controls[0].Controls[0] as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
            }

            if (ip == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("维护的实例不具备FS.HISFC.BizProcess.Integrate.IPrintTransFusion接口");
                return;
            }

            try
            {
                //将备注放入userCode中
                this.myPatients[0].UserCode = this.Memo;

                string orderType = "ALL";

                if (this.rbtAll.Checked)
                    orderType = "ALL";

                if (this.rbtLong.Checked)
                    orderType = "1";

                if (this.rbtShort.Checked)
                    orderType = "0";

                ip.DCIsPrint = this.dcIsPrint;
                ip.NoFeeIsPrint = this.noFeeIsPrint;
                ip.QuitFeeIsPrint = this.quitFeeIsprint;
                ip.SetSpeOrderType(this.speOrderType);

                ip.Query(this.myPatients, billNo, this.dateTimePicker1.Value, this.dateTimePicker2.Value, this.IsRePrint, orderType, IsFirst);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        public void ResetPanel()
        {
            ArrayList alBill = new ArrayList();

            try
            {
                //获得执行单分类
                alBill = Bill.QueryExecBill(CacheManager.LogEmpl.Nurse.ID);
            }
            catch { MessageBox.Show("获得执行单分类出错！"); }

            if (alBill == null)
            {
                MessageBox.Show("获得执行单设置出错！");
                return;
            }
            this.tabControl1.TabPages.Clear();

            for (int i = 0; i < alBill.Count; i++)
            {
                TabPage t = new TabPage();
                t.Text = ((FS.FrameWork.Models.NeuObject)alBill[i]).Name;
                
                t.Tag = alBill[i];
                Panel p = new Panel();
                p.AutoScroll = true;
                p.Dock = DockStyle.Fill;
                p.BackColor = Color.White;

                t.Controls.Add(p);

                this.tabControl1.TabPages.Add(t);
            }


        }

        /// <summary>
        /// 设置执行单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //frmSetExecBill f = new frmSetExecBill(FS.Common.Class.Main.var);
            //f.ShowDialog();
            this.ResetPanel();
        }

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.myPatients != null && this.myPatients.Count > 0 && this.tabControl1.TabPages.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示执行单信息，请稍候........");
                Application.DoEvents();
                string BillNo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).ID;
                this.IsRePrint = false;

                this.Memo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).User01;
                this.Query(BillNo);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// 补打变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.Retrieve();
            }
            catch
            {
                MessageBox.Show("请先点查询按钮进行查询！");
            }
        }

        #region 防止执行单的复选框在和医嘱查询来回点击后显示消失掉 20100916

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService  OnInit(object sender, object neuObject, object param)
        {
            TreeView tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false) tv.CheckBoxes = true;
            this.ResetPanel();

            this.chkFirst.Visible = this.isShowFirstDay;

            return null;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        #endregion

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alValues)
            {
                myPatients.Add(p);
            }
            this.Retrieve();
            return 0;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
           // return this.Retrieve();
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            if (ip != null)
                ip.Print();
            return 0;
        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            if (ip != null)
                ip.PrintSet();
            return 0;
        }

         #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get {
                 Type[]  type = new Type[1];
                 type[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion);
                return type;
            }
        }

        #endregion

    }
}
