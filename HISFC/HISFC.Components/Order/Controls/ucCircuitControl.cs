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
    /// [功能描述: 贴瓶单控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///  />
    /// </summary>
    public partial class ucCircuitControl : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCircuitControl()
        {
            InitializeComponent();
        }

        #region 变量
        FS.HISFC.BizLogic.Order.TransFusion manager = new FS.HISFC.BizLogic.Order.TransFusion();
        FS.HISFC.BizProcess.Interface.IPrintTransFusion IPrintTransFusion = null;//当前接口

        bool bPrint = true;

        /// <summary>
        /// 是否分用法 默认分用法
        /// </summary>
        private bool isSeprateUse = true;

        #endregion

        #region 属性

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
        /// 是否按天打印（按天打印的话，时间点就只能选择一天）
        /// </summary>
        private bool isShowByDay = false;

        /// <summary>
        /// 是否按天打印（按天打印的话，时间点就只能选择一天）
        /// </summary>
        [Category("查询设置"), Description("是否按天打印（按天打印的话，时间点就只能选择一天）")]
        public bool IsShowByDay
        {
            get
            {
                return isShowByDay;
            }
            set
            {
                isShowByDay = value;
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

        protected List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;

        /// <summary>
        /// 是否分用法显示
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
                if (value == null)
                    return;
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

        #region 方法

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            TreeView tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false)
            {
                tv.CheckBoxes = true;
            }
            DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

            DateTime dt1 = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            DateTime dt2 = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 00, 00);
            if (!string.IsNullOrEmpty(beginTime))
            {
                dt1 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                dt2 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
            }

            this.dateTimePicker1.Value = dt1;
            this.dateTimePicker2.Value = dt2;
            if (isShowByDay)
            {
                //lblDateAnd.Visible = false;
                //dateTimePicker2.Visible = false;

                //dateTimePicker2.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
                //dateTimePicker1.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
                //dateTimePicker1.CustomFormat = "yyyy年MM月dd日";
            }
            else
            {
                lblDateAnd.Visible = true;
                dateTimePicker2.Visible = true;
            }
            ResetPanel();

            this.cbxFirstOrder.Visible = this.isShowFirstDay;

            return null;
        }


        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        public void ResetPanel()
        {
            ArrayList alUsage = null;
            FS.FrameWork.Public.ObjectHelper helper = null;
            try
            {
                //系统用法
                alUsage = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
                helper = new FS.FrameWork.Public.ObjectHelper(alUsage);
            }
            catch
            {
                MessageBox.Show("获得用法出错！");
                return;
            }

            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            ArrayList al = manager.QueryTransFusion(empl.Nurse.ID);
            if (al == null)
            {
                MessageBox.Show("获得输液卡设置出错！");
                return;
            }
            else if (al.Count == 0)
            {
                return;
            }

            this.neuTabControl1.TabPages.Clear();

            //用法分组
            if (this.isSeprateUse)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    TabPage tp = new TabPage(helper.GetName(al[i].ToString()));
                    tp.Tag = helper.GetObjectFromID(al[i].ToString());
                    Panel p = new Panel();
                    p.AutoScroll = true;
                    p.Dock = DockStyle.Fill;
                    p.BackColor = Color.White;
                    tp.Controls.Add(p);
                    this.neuTabControl1.TabPages.Add(tp);
                }
            }
            else
            {
                TabPage tp = new TabPage("全部输液卡");
                string useCode = "";
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                int i = 0;
                for (i = 0; i < al.Count - 1; i++)
                {
                    obj = helper.GetObjectFromID(al[i].ToString());
                    if (obj != null)
                    {
                        useCode += obj.ID + "','";
                    }
                }
                obj = helper.GetObjectFromID(al[i].ToString());
                useCode += obj.ID;

                tp.Tag = useCode;
                Panel p = new Panel();
                p.AutoScroll = true;
                p.Dock = DockStyle.Fill;
                p.BackColor = Color.White;
                tp.Controls.Add(p);
                this.neuTabControl1.TabPages.Add(tp);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            // TODO:  添加 ucDrugCardPanel.Retrieve 实现
            if (this.neuTabControl1.TabPages.Count <= 0)
                return 0;

            string CardCode = "";
            if (this.isSeprateUse)
            {
                CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
            }
            else
            {
                CardCode = this.neuTabControl1.SelectedTab.Tag.ToString();
            }

            this.Query(CardCode);

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
            if (IPrintTransFusion != null)
                IPrintTransFusion.Print();
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
            if (IPrintTransFusion != null)
                IPrintTransFusion.PrintSet();
            return 0;
        }

        private void Query(string usageCode)
        {

            if (this.neuTabControl1.TabPages.Count <= 0 || this.myPatients == null || this.myPatients.Count == 0)
            {
                return;
            }

            bPrint = this.chkRePrint.Checked;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询输液卡信息...");
            if (this.neuTabControl1.SelectedTab.Controls[0].Controls.Count == 0)
            {
                //当前Tab页里面还没有输液卡
                object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucCircuitControl), typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion));
                if (o == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("请维护HISFC.Components.Order.Controls.ucDrugCardControl里面接口FS.HISFC.BizProcess.Integrate.IPrintTransFusion的实例对照！");
                    return;
                }
                IPrintTransFusion = o as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
                ((Control)o).Visible = true;
                ((Control)o).Dock = DockStyle.Fill;
                this.neuTabControl1.SelectedTab.Controls[0].Controls.Add((Control)o);

            }
            else
            {
                IPrintTransFusion = this.neuTabControl1.SelectedTab.Controls[0].Controls[0] as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
            }
            if (IPrintTransFusion == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("维护的实例不具备FS.HISFC.BizProcess.Integrate.IPrintTransFusion接口");
                return;
            }

            try
            {
                //user01为是否首日量 1为首日医嘱；0否
                //user02为是医嘱类型：all为全部；1为长嘱；0为临嘱
                bool isFirst = this.cbxFirstOrder.Checked;
                string orderType = string.Empty;
                if (this.rbtShort.Checked)
                {
                    orderType = "0";
                }
                else if (this.rbtLong.Checked)
                {
                    orderType = "1";
                }
                else
                {
                    orderType = "ALL";
                }

                IPrintTransFusion.DCIsPrint = this.dcIsPrint;
                IPrintTransFusion.NoFeeIsPrint = this.noFeeIsPrint;
                IPrintTransFusion.QuitFeeIsPrint = this.quitFeeIsprint;
                IPrintTransFusion.SetSpeOrderType(this.speOrderType);

                IPrintTransFusion.Query(this.myPatients, usageCode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, bPrint, orderType, isFirst);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucCircuitControl.Query" + ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

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

        #endregion

        #region 事件

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == null) return;
            string CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
            this.Query(CardCode);
        }

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forms.frmDrugCardSet f = new HISFC.Components.Order.Forms.frmDrugCardSet();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.ResetPanel();
            }
        }
        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion);
                return type;
            }
        }

        #endregion
    }
}
