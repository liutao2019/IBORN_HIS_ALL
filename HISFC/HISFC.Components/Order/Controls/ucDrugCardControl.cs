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
    /// [功能描述: 输液卡控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人='冯超'
    ///		修改时间='2010-10-12'
    ///		修改目的='实现不分用法显示输液卡'
    ///		修改描述='加isSeprateUse属性判断是否分用法'
    ///  />
    /// </summary>
    public partial class ucDrugCardControl : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucDrugCardControl()
        {
            InitializeComponent();
        }

        #region 变量
        FS.HISFC.BizLogic.Order.TransFusion manager = new FS.HISFC.BizLogic.Order.TransFusion();

        /// <summary>
        /// 当前接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPrintTransFusion ip = null;

        /// <summary>
        /// 是否重打
        /// </summary>
        bool isRePrint = true;

        /// <summary>
        /// 是否首日量
        /// </summary>
        bool isFirst = false;

        /// <summary>
        /// 用于存储用法
        /// </summary>
        ArrayList useList = null;

        /// <summary>
        /// 选中的患者列表
        /// </summary>
        protected List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;

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
        /// 设置护士站看到医嘱的类型,逗号隔开 会诊:CONS 科室:DEPTXXX 医技:DEPTXXX 其他:OTHER"
        /// </summary>
        private string speOrderType = "";

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
        /// 是否分用法 默认分用法
        /// </summary>
        private bool isSeprateUse = true;

        /// <summary>
        /// 是否分用法显示输液卡
        /// </summary>
        [Category("控件设置"), Description("是否分用法显示输液卡 true 是 ，false 不是")]
        public bool IsSeprateUse
        {
            get
            {
                return this.isSeprateUse;
            }
            set
            {
                this.isSeprateUse = value;
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

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            if (tv != null && tv.CheckBoxes == false)
            {
                tv.CheckBoxes = true;
            }

            try
            {
                DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                DateTime dt1 = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

                DateTime dt2 = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 00, 00);
                try
                {
                    if (!string.IsNullOrEmpty(beginTime))
                    {
                        dt1 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
                    }
                    if (!string.IsNullOrEmpty(endTime))
                    {
                        dt2 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
                    }
                }
                catch
                { }

                this.dateTimePicker1.Value = dt1;
                this.dateTimePicker2.Value = dt2;
            }
            catch { }
            ResetPanel();

            this.chkFirst.Visible = this.isShowFirstDay;

            return null;
        }

        /// <summary>
        /// 重新设置
        /// </summary>
        public void ResetPanel()
        {
            ArrayList al = manager.QueryTransFusion(CacheManager.LogEmpl.Nurse.ID);
            if (al == null)
            {
                MessageBox.Show("获得输液卡设置出错！");
                return;
            }
            this.neuTabControl1.TabPages.Clear();
            //{D5058DCE-E168-4732-B5F7-1541A87C2D82}按照用法产生TAB页
            if (this.isSeprateUse)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    TabPage tp = new TabPage(SOC.HISFC.BizProcess.Cache.Common.GetUsageName(al[i].ToString()));
                    tp.Tag = SOC.HISFC.BizProcess.Cache.Common.GetUsage(al[i].ToString());
                    Panel p = new Panel();
                    p.AutoScroll = true;
                    p.Dock = DockStyle.Fill;
                    p.BackColor = Color.White;
                    tp.Controls.Add(p);
                    this.neuTabControl1.TabPages.Add(tp);
                }
            }
            else//{D5058DCE-E168-4732-B5F7-1541A87C2D82}不分用法的话不产生TAB页
            {
                TabPage tp = new TabPage("输液卡");
                tp.Tag = null;
                Panel p = new Panel();
                p.AutoScroll = true;
                p.Dock = DockStyle.Fill;
                p.BackColor = Color.White;
                tp.Controls.Add(p);
                this.neuTabControl1.TabPages.Add(tp);
                this.useList = new ArrayList(al);
            }
        }
       
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            //{D5058DCE-E168-4732-B5F7-1541A87C2D82}
            if (this.isSeprateUse)
            {
                if (this.neuTabControl1.TabPages.Count <= 0) return 0;
                string CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
                this.Query(CardCode);
            }
            else
            {
                string useFlag = string.Empty;
                if (this.useList != null)
                {
                    for (int i = 0; i < this.useList.Count; i++)
                    {
                        useFlag += "'" + this.useList[i].ToString() + "'" + ",";
                    }
                }
                if (useFlag.Length > 0)
                {
                    useFlag = useFlag.Substring(0, useFlag.Length - 1);
                    this.Query(useFlag);
                }
            }
            return 0;
        }

        private void Query(string usageCode)
        {
            if (this.neuTabControl1.TabPages.Count <= 0 || this.myPatients == null)
            {
                return;
            }

            isRePrint = this.chkRePrint.Checked;
            isFirst = this.chkFirst.Checked;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询输液卡信息...");
            Application.DoEvents();

            if (this.neuTabControl1.SelectedTab.Controls[0].Controls.Count == 0)
            {
                //当前Tab页里面还没有输液卡
                object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucDrugCardControl), typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion));
                if (o == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("请维护HISFC.Components.Order.Controls.ucDrugCardControl里面接口FS.HISFC.BizProcess.Interface.IPrintTransFusion的实例对照！");
                    return;
                }
                this.ip = o as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
                ((Control)o).Visible = true;
                ((Control)o).Dock = DockStyle.Fill;
                this.neuTabControl1.SelectedTab.Controls[0].Controls.Add((Control)o);
            }

            ip = this.neuTabControl1.SelectedTab.Controls[0].Controls[0] as FS.HISFC.BizProcess.Interface.IPrintTransFusion;

            if (ip == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("维护的实例不具备FS.HISFC.BizProcess.Integrate.IPrintTransFusion接口");
                return;
            }

            try
            {
                string orderType = "ALL";

                if (this.rbtAll.Checked)
                {
                    orderType = "ALL";
                }

                if (this.rbtLong.Checked)
                {
                    orderType = "1";
                }

                if (this.rbtShort.Checked)
                {
                    orderType = "0";
                }

                ip.DCIsPrint = this.dcIsPrint;
                ip.NoFeeIsPrint = this.noFeeIsPrint;
                ip.QuitFeeIsPrint = this.quitFeeIsprint;
                ip.SetSpeOrderType(this.speOrderType);

                ip.Query(this.myPatients, usageCode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, isRePrint, orderType, isFirst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == null) return;
            string CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
            this.Query(CardCode);
        }

        #region 重写
       

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alValues)
            {
                myPatients.Add(p);
            }

            return 0;
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.GetPatientList() == -1)
            {
                return -1;
            }
            return this.Retrieve();
        }

        private int GetPatientList()
        {
            try
            {
                ArrayList al = this.GetSelectedTreeNodes();
                if (al != null)
                {
                    this.myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                        {
                            myPatients.Add((FS.HISFC.Models.RADT.PatientInfo)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
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
        #endregion

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forms.frmDrugCardSet f = new HISFC.Components.Order.Forms.frmDrugCardSet();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.ResetPanel();
            }
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Retrieve();
        }
    }
}
