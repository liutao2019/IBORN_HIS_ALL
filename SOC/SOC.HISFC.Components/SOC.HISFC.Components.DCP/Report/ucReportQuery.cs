using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP.Report
{
    public partial class ucReportQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucReportQuery()
        {
            InitializeComponent();
        }


        #region 属性变量

        //FS.HISFC.Management.DCP.DiseaseReport myReport = new FS.HISFC.Management.DCP.DiseaseReport();
        FS.SOC.HISFC.BizLogic.DCP.DiseaseReport myReportMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
        //FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper employerHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper provinceHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper cityHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper coutyHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper townHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper proHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper caseHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper specialInfectForLogin = new FS.FrameWork.Public.ObjectHelper();

        System.Collections.Hashtable hsReport = new Hashtable();
        ArrayList alNotice = new ArrayList();
        #endregion

        #region 自动刷新，包括属性变量都在此，改动前三思

        /// <summary>
        /// 自动刷新启动延迟时间-秒
        /// </summary>
        private uint dueTime = 3;

        /// <summary>
        /// 自动刷新启动延迟时间-秒
        /// </summary>
        [Description("自动刷新启动延迟时间-秒"), Category("设置"), Browsable(true)]
        public uint DueTime
        {
            get { return dueTime; }
            set { dueTime = value; }
        }

        /// <summary>
        /// 终端设置的刷新间隔
        /// </summary>
        private uint refreshInterval = 10;

        [Description("自动刷新间隔-秒"), Category("设置"), Browsable(true)]
        public uint RefreshInterval
        {
            get { return refreshInterval; }
            set { refreshInterval = value; this.nTxtRefreshSpan.Text = this.refreshInterval.ToString(); }
        }

        private bool isShowMessageForm = true;

        [Description("自动刷新时是否用消息窗口提示"), Category("设置"), Browsable(true)]
        public bool IsShowMessageForm
        {
            get { return isShowMessageForm; }
            set { isShowMessageForm = value; }
        }

        private System.Threading.Timer autoRefreshTimer = null;
        private System.Threading.TimerCallback autoRefreshCallBack = null;
        private delegate void autoRefreshHandler();
        private autoRefreshHandler autoRefreshEven;

        private void BeginAutoRefresh()
        {
            this.ncbAutoQuery.Visible = false;
            this.nTxtRefreshSpan.Visible = false;
            this.nlbRefreshUnit.Visible = false;
            if (this.RefreshInterval <= 0)
            {
                return;
            }
            if (this.autoRefreshCallBack == null)
            {
                this.autoRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefreshTimerCallback);
            }
            this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, dueTime * 1000, this.refreshInterval * 1000);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="param">参数（没有使用）</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            if (!this.ncbAutoQuery.Checked)
            {
                return;
            }

            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.AutoQuery);
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.Invoke(this.autoRefreshEven);
            }

        }

        #endregion


        #region 方法

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //初始化时间
            System.DateTime dt = new DateTime();
            dt = this.myReportMgr.GetDateTimeFromSysDateTime();
            this.dtEnd.Value = dt.AddDays(1);
            this.dtBegin.Value = dt.AddDays(-10);
            //初始化报告科室
            this.initReportDept();
            //初始化疾病
            this.initDisease();
            //初始化时间类型
            this.initDateType();
            //初始化常量 便于快速查找省市县等
            this.initConstant();
            //初始化fp
            this.setFp();
            //显示初始化条件下的报告
            //this.QueryReport();

            this.hsReport.Clear();

            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);

            this.BeginAutoRefresh();

        }

        private void initReportDept()
        {
            try
            {
                
                ArrayList al = new ArrayList();
                //FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();


                FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

                System.Collections.ArrayList alPriv = new ArrayList(privManager.QueryUserPriv(privManager.Operator.ID, "8001"));

                if (alPriv != null && alPriv.Count > 0)
                {
                    //报告科室默认全部 即请选择
                    FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

                    al.AddRange(deptMgr.GetDeptmentAll());
                    ob.ID = "AAA";
                    ob.Name = "全部";
                    al.Insert(0, ob);
                }
                else
                {
                    return;
                }
                this.ncmbReportDept.AddItems(al);
                this.ncmbReportDept.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化科室失败" + ex.Message);
            }
        }
        private void initDateType()
        {
            try
            {
                /*报告日期 发病日期 诊断日期 死亡日期 审核日期*/
                ArrayList al = new ArrayList();

                FS.FrameWork.Models.NeuObject obre = new FS.FrameWork.Models.NeuObject();
                obre.ID = "REPORT_DATE";
                obre.Name = "填卡日期";
                al.Add(obre);

                FS.FrameWork.Models.NeuObject obin = new FS.FrameWork.Models.NeuObject();
                obin.ID = "INFECT_DATE";
                obin.Name = "发病日期";
                al.Add(obin);

                FS.FrameWork.Models.NeuObject obdi = new FS.FrameWork.Models.NeuObject();
                obdi.ID = "DIAGNOSIS_DATE";
                obdi.Name = "诊断日期";
                al.Add(obdi);

                FS.FrameWork.Models.NeuObject obde = new FS.FrameWork.Models.NeuObject();
                obde.ID = "DEAD_DATE";
                obde.Name = "死亡日期";
                al.Add(obde);

                FS.FrameWork.Models.NeuObject obap = new FS.FrameWork.Models.NeuObject();
                obap.ID = "APPROVE_DATE";
                obap.Name = "审核日期";
                al.Add(obap);

                this.cmbDateType.DisplayMember = "Name";
                this.cmbDateType.ValueMember = "ID";
                this.cmbDateType.DataSource = al;

                this.cmbDateType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化日期失败！" + ex.Message);
            }
        }
        private void initDisease()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                
                //传染病的类型
                ArrayList alInfectClass = new ArrayList();
                ArrayList alinfection = new ArrayList();
                alInfectClass.AddRange(conMgr.GetList("INFECTCLASS"));
                if (alInfectClass.Count < 1)
                {
                    return;
                }

                //根据类型获取传染病
                FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "AAA";
                ob.Name = "全部";
                alinfection.Add(ob);
                foreach (FS.FrameWork.Models.NeuObject infectclass in alInfectClass)
                {
                    alinfection.AddRange(conMgr.GetList(infectclass.ID));
                }
                this.ncmbDisease.AddItems(alinfection);
                this.ncmbDisease.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化疾病失败" + ex.Message);
            }
        }

        private void initConstant()
        {
            //常量初始化 在显示报告简单信息时快速查找用
            FS.HISFC.BizLogic.Manager.Department dpt = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            
            //报告科室
            this.deptHelper.ArrayObject = dpt.GetDeptAllUserStopDisuse();
            //员工
            this.employerHelper.ArrayObject = person.GetEmployeeAll();
            //职业
            this.proHelper.ArrayObject = con.GetList("PATIENTJOB");
            //省市县镇
            this.provinceHelper.ArrayObject = con.GetList("PROVINCE");
            this.coutyHelper.ArrayObject = con.GetList("COUNTY");
            this.cityHelper.ArrayObject = con.GetList("CITY");
            this.townHelper.ArrayObject = con.GetList("TOWN");
            //病例分类
            this.caseHelper.ArrayObject = con.GetList("CASECLASS");
            //登录提示疾病
            this.specialInfectForLogin.ArrayObject = con.GetAllList("SPECIALINFECT");
        }
        #endregion

        #region FarPoint设置

      
        private string settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPDCPReportManagerSetting.xml";
        private enum fpColSet
        {
            /// <summary>
            /// 选中
            /// </summary>
            ColChecked,

            /// <summary>
            /// 存放在扩展信息中的报告卡虚拟编号
            /// </summary>
            ColReportExtendID,

            /// <summary>
            /// 患者的住院号或门诊病历号
            /// </summary>
            ColPatientNO,

            /// <summary>
            /// 患者姓名
            /// </summary>
            ColPatientName,

            /// <summary>
            /// 患儿家长
            /// </summary>
            ColPatienParents,

            /// <summary>
            /// 患者身份证号
            /// </summary>
            ColPatientID,

            /// <summary>
            /// 性别
            /// </summary>
            ColSex,

            /// <summary>
            /// 出生日期
            /// </summary>
            ColBirthday,

            /// <summary>
            /// 年龄
            /// </summary>
            ColAge,

            /// <summary>
            /// 职业
            /// </summary>
            ColPatientProfession,

            /// <summary>
            /// 工作单位
            /// </summary>
            ColWorkPlace,

            /// <summary>
            /// 联系电话
            /// </summary>
            ColTelephone,

            /// <summary>
            /// 来源
            /// </summary>
            ColHomeArea,

            /// <summary>
            /// 住址
            /// </summary>
            ColHomePlace,

            /// <summary>
            /// 科室
            /// </summary>
            ColPatientDept,

            /// <summary>
            /// 疾病名称
            /// </summary>
            ColDiseaseName,

            /// <summary>
            /// 发病日期
            /// </summary>
            ColInfectDate,

            /// <summary>
            /// 诊断日期
            /// </summary>
            ColDiagDate,

            /// <summary>
            /// 死亡日期
            /// </summary>
            ColDeadDate,

            /// <summary>
            /// 病例分类
            /// </summary>
            ColCaseClassOne,

            /// <summary>
            /// 病例分类
            /// </summary>
            ColCaseCLassTwo,

            /// <summary>
            /// 接触
            /// </summary>
            ColInfectOther,

            /// <summary>
            /// 是否附卡
            /// </summary>
            ColAddtion,

            /// <summary>
            /// 报告人
            /// </summary>
            ColReportDoctor,

            /// <summary>
            /// 报告科室
            /// </summary>
            ColReportDept,

            /// <summary>
            /// 报告时间
            /// </summary>
            ColReportTime,

            /// <summary>
            /// 作废人
            /// </summary>
            ColCancelOper,

            /// <summary>
            /// 作废时间
            /// </summary>
            ColCancelTime,

            /// <summary>
            /// 修改人
            /// </summary>
            ColModifyOper,

            /// <summary>
            /// 修改时间
            /// </summary>
            ColModifyTime,

            /// <summary>
            /// 审核人
            /// </summary>
            ColApproveOper,

            /// <summary>
            /// 审核时间
            /// </summary>
            ColApproveTime,

            /// <summary>
            /// 操作人
            /// </summary>
            ColOperCode,

            /// <summary>
            /// 操作人科室
            /// </summary>
            ColOperDept,

            /// <summary>
            /// 操作时间
            /// </summary>
            ColOperTime,

            /// <summary>
            /// 状态
            /// </summary>
            ColState,
            /// <summary>
            /// 列数
            /// </summary>
            ColNum
        }
        private void setFp()
        {
            try
            {
                this.fpSpread1_Sheet1.Protect = false;
                if (System.IO.File.Exists(settingFileName))
                {
                    this.fpSpread1.ReadSchema(settingFileName);
                }
                else
                {
                    this.fpSpread1_Sheet1.ColumnCount = (int)fpColSet.ColNum;

                    FarPoint.Win.Spread.CellType.CheckBoxCellType cellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColChecked).CellType = cellType;
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColChecked).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColChecked).Label = "选中";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColChecked).Width = 30F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportExtendID).Label = "报告卡编号";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportExtendID).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientNO).Label = "病历号/住院号";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientNO).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientName).Label = "患者姓名";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientName).Width = 70F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatienParents).Label = "家长姓名";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatienParents).Width = 70;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientID).Label = "身份证号码";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientID).Width = 130F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColSex).Label = "性别";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColSex).Width = 30F;
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColSex).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColBirthday).Label = "出生日期";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColBirthday).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColAge).Label = "年龄";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColAge).Width = 40F;
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColAge).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientProfession).Label = "职业";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientProfession).Width = 100F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColWorkPlace).Label = "工作单位";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColWorkPlace).Width = 160F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColTelephone).Label = "联系电话";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColTelephone).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColHomeArea).Label = "患者来源";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColHomeArea).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColHomePlace).Label = "详细住址";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColHomePlace).Width = 290F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientDept).Label = "就诊科室";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColPatientDept).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColDiseaseName).Width = 100F;
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColDiseaseName).Label = "疾病诊断";

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColInfectDate).Width = 90F;
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColInfectDate).Label = "发病日期";

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColDiagDate).Label = "诊断日期";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColDiagDate).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColDeadDate).Label = "死亡日期";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColDeadDate).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCaseClassOne).Label = "病例分类";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCaseClassOne).Width = 100F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCaseCLassTwo).Label = "病例分类";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCaseCLassTwo).Width = 60F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColInfectOther).Label = "接触者";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColInfectOther).Width = 45F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColAddtion).Label = "附卡";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColAddtion).Width = 30F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportDoctor).Label = "报卡医生";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportDoctor).Width = 60F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportDept).Label = "报卡科室";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportDept).Width = 90F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportTime).Label = "报卡时间";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColReportTime).Width = 120F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCancelOper).Label = "作废者";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCancelOper).Width = 50F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCancelTime).Label = "作废时间";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColCancelTime).Width = 120F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColModifyOper).Label = "修改者";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColModifyOper).Width = 50F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColModifyTime).Label = "修改时间";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColModifyTime).Width = 120F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColApproveOper).Label = "审核者";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColApproveOper).Width = 50F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColApproveTime).Label = "审核时间";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColApproveTime).Width = 120F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColOperCode).Label = "操作者";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColOperCode).Width = 50F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColOperDept).Label = "操作科室";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColOperDept).Width = 60F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColOperTime).Label = "操作时间";
                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColOperTime).Width = 120F;

                    this.fpSpread1_Sheet1.Columns.Get((int)fpColSet.ColState).Visible = false;
                }
                this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
                this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            }
            catch (Exception ex)
            {
                MessageBox.Show("fp设置失败" + ex.Message);
            }
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpSpread1.SaveSchema(this.settingFileName);
        }

        private void colorFp()
        {
            //按状态显示颜色 仅设置行头 RowHeader背景色
            try
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    switch (this.fpSpread1_Sheet1.Cells[i, (int)fpColSet.ColState].Text.Trim())
                    {
                        case "0"://新加
                            this.fpSpread1_Sheet1.RowHeader.Cells.Get(i, 0).BackColor = System.Drawing.Color.Black;
                            break;
                        case "1"://合格
                            this.fpSpread1_Sheet1.RowHeader.Cells.Get(i, 0).BackColor = System.Drawing.Color.Green;
                            break;
                        case "2"://不合格
                            this.fpSpread1_Sheet1.RowHeader.Cells.Get(i, 0).BackColor = System.Drawing.Color.Red;
                            break;
                        case "3"://报告人作废
                            this.fpSpread1_Sheet1.RowHeader.Cells.Get(i, 0).BackColor = System.Drawing.Color.LightBlue;
                            break;
                        case "4"://保健科作废
                            this.fpSpread1_Sheet1.RowHeader.Cells.Get(i, 0).BackColor = System.Drawing.Color.Blue;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("着色发生错误" + ex.Message);
            }
        }
        #endregion

        #region 报告查询

        public void AutoQuery()
        {
            try
            {
                this.dtEnd.Value.AddDays(1);
                string deptcode = "AAA";
                string datetype = "REPORT_DATE";
                string begindate = this.dtBegin.Value.ToShortDateString();
                string enddate = this.myReportMgr.GetDateTimeFromSysDateTime().ToString();
                string state = "0";

                ArrayList al = new ArrayList();
                ArrayList alNeedShow = new ArrayList();
                //查询
                al = this.myReportMgr.GetCommonReportListByMore(datetype, begindate, enddate, state, deptcode);

                foreach (FS.HISFC.DCP.Object.CommonReport r in al)
                {

                    if (this.hsReport.Contains(r.ID))
                    {
                        continue;
                    }
                    this.hsReport.Add(r.ID, null);
                    alNeedShow.Add(r);
                    if (this.IsShowMessageForm)
                    {
                        DCP.Notice.Notice notice = new DCP.Notice.Notice();
                        alNotice.Add(notice);
                        notice.ShowNotice(this.deptHelper.GetName(r.DoctorDept.ID) + this.employerHelper.GetName(r.ReportDoctor.ID) + "新报了卡，请审核！");
                    }
                }
                if (alNeedShow == null || alNeedShow.Count == 0)
                {
                    return;
                }
                //显示查询结果
                this.myShowReportInfo(alNeedShow, false);
                //着色
                this.colorFp();
            }
            catch//(Exception ex)
            {
            }
        }

        public void QueryReport()
        {
            //界面多条件查询 有默认值
            //报卡科室 按什么时间 起止时间 状态 疾病
            try
            {
                this.dtEnd.Value.AddDays(1);
                string deptcode = "AAA";
                if (this.ncmbReportDept.Tag != null)
                {
                    deptcode = this.ncmbReportDept.Tag.ToString();
                }
                //报告状态[0新填 1合格 2不合格 3报告人作废 4 疾病预防科作废]
                string datetype = this.cmbDateType.SelectedValue.ToString();
                string begindate = this.dtBegin.Value.ToShortDateString();
                string enddate = this.dtEnd.Value.ToString();
                string state = "0','1','2','3','4";
                if (this.radUnapprove.Checked)
                    state = "0','2";
                else if (this.radEligible.Checked)
                    state = "1";
                else if (this.radUneligible.Checked)
                    state = "2";
                else if (this.radCanel.Checked)
                    state = "3','4";
                else if (this.radAll.Checked)
                    state = "0','1','2','3','4";

                string disease = "AAA";
                if (this.ncmbDisease.Tag != null)
                {
                    disease = this.ncmbDisease.Tag.ToString();
                }

                ArrayList al = new ArrayList();
                ArrayList alNeedShow = new ArrayList();
                //查询
                al = this.myReportMgr.GetCommonReportListByMore(datetype, begindate, enddate, state, deptcode, disease);

                if (al == null)
                {
                    MessageBox.Show("查询失败>>" + this.myReportMgr.Err);
                    return;
                }
                this.hsReport.Clear();

                foreach (FS.HISFC.DCP.Object.CommonReport r in al)
                {
                    if (this.hsReport.Contains(r.ID))
                    {
                        continue;
                    }
                    this.hsReport.Add(r.ID, null);
                    alNeedShow.Add(r);
                }
                if (alNeedShow == null || alNeedShow.Count == 0)
                {
                    return;
                }
                //显示查询结果
                this.myShowReportInfo(al, true);
                //着色
                this.colorFp();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败>>" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 疑似病例诊断超过20天未订正警告
        /// </summary>
        public void QueryReportForUnclearCase()
        {
            ArrayList al = new ArrayList();
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("检测正在进行....请稍候");
                
                Application.DoEvents();
                DateTime dt = this.myReportMgr.GetDateTimeFromSysDateTime();
                //al = this.myReport.QueryCommonReportListByMore("AAAA", "DIAGNOSIS", "0001-1-1", dt.AddDays(-20).ToString(), "'0','1','2','3','4'", "AAAA");
                if (al.Count < 1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("暂时没有符合条件的报告");
                    return;
                }
                ArrayList altemp = new ArrayList();
                foreach (FS.HISFC.DCP.Object.CommonReport report in al)
                {
                    if (report.CaseClass1.ID == "0001" && report.CorrectReportNO == "")
                    {
                        altemp.Add(report);
                    }
                }
                if (altemp.Count < 1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("暂时没有符合条件的报告");
                    return;
                }
                //显示查询结果
                this.myShowReportInfo(altemp, true);
                //着色
                this.colorFp();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("疑似病例警告处理错误：>>" + ex.Message);
            }
        }
        #endregion

        #region 报告浏览
        /// <summary>
        /// 报告浏览
        /// </summary>
        //public void BrowserReport()
        //{
        //    //调用打印函数
        //    if (this.fpSpread1_Sheet1.RowCount <= 0) return;
        //    if (this.fpSpread1_Sheet1.ActiveRowIndex < 0) return;
        //    DCP.DiseaseReport.ucReportRegister uc = new DCP.DiseaseReport.ucReportRegister();
        //    uc.init();
        //    //uc.Show();
        //    for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
        //    {
        //        if (this.fpSpread1_Sheet1.Cells[row, 0].Text == "True")
        //        {
        //            uc.ShowReportData(this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.DCP.Object.CommonReport);
        //            uc.print();
        //        }
        //    }
        //}
        #endregion

        #region 报告审核
        /// <summary>
        /// 获取有序的报告卡
        /// </summary>
        /// <returns></returns>
        private ArrayList GetReport()
        {
            //同状态的报告在一起 便于审核窗口跳出列表有序
            ArrayList al = new ArrayList();
            try
            {
                ArrayList alone = new ArrayList();//新加
                ArrayList altwo = new ArrayList();//合格
                ArrayList althree = new ArrayList();//不合格
                ArrayList alfour = new ArrayList();//报告人作废
                ArrayList alfive = new ArrayList();//保健科作废
                for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                {
                    //Farpoint的ChechBox类型Cell在未选中的情况下可能为空
                    if (this.fpSpread1_Sheet1.Cells[row, 0].Text == "True")
                    {

                        switch (this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColState].Text)
                        {
                            case "0":
                                alone.Add(this.fpSpread1_Sheet1.Rows[row].Tag);
                                break;
                            case "1":
                                altwo.Add(this.fpSpread1_Sheet1.Rows[row].Tag);
                                break;
                            case "2":
                                althree.Add(this.fpSpread1_Sheet1.Rows[row].Tag);
                                break;
                            case "3":
                                alfour.Add(this.fpSpread1_Sheet1.Rows[row].Tag);
                                break;
                            case "4":
                                alfive.Add(this.fpSpread1_Sheet1.Rows[row].Tag);
                                break;
                        }
                    }
                }
                if (alone.Count > 0)
                    al.AddRange(alone);
                if (altwo.Count > 0)
                    al.AddRange(altwo);
                if (althree.Count > 0)
                    al.AddRange(althree);
                if (alfour.Count > 0)
                    al.AddRange(alfour);
                if (alfive.Count > 0)
                    al.AddRange(alfive);
            }
            catch (Exception ex)
            {
                MessageBox.Show("从fp获取报告卡失败" + ex.Message);
                return null;
            }
            return al;
        }

        /// <summary>
        /// 审核
        /// </summary>
        public void ApproveReport()
        {
            ArrayList al = new ArrayList();
            al = this.GetReport();
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("请选择数据！");
                return;
            }

            foreach (DCP.Notice.Notice n in alNotice)
            {
                try
                {
                    n.TerminateNotice();
                }
                catch 
                { 

                }
            }
            DCP.Controls.ucDiseaseReport uc = new FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseReport(true, al);
            FS.FrameWork.WinForms.Forms.frmBaseForm f = new FS.FrameWork.WinForms.Forms.frmBaseForm(uc);
            f.SetFormID("DCP_Q");
            f.WindowState = FormWindowState.Maximized;

            f.ShowDialog();
            this.QueryReport();
            
        }
        #endregion

        #region 数据加载

        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="al"></param>
        private void myShowReportInfo(ArrayList al, bool isNeedClear)
        {
            try
            {
                if (isNeedClear)
                {
                    this.fpSpread1_Sheet1.RowCount = 0;
                }
                this.fpSpread1_Sheet1.ColumnCount = (int)fpColSet.ColNum;
                foreach (FS.HISFC.DCP.Object.CommonReport report in al)
                {

                    this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                    int row = this.fpSpread1_Sheet1.RowCount - 1;
                    //"编号"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColReportExtendID].Value = report.ReportNO;
                    //"患者号
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColPatientNO].Value = report.Patient.PID.CardNO;
                    //"姓名"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColPatientName].Value = report.Patient.Name;
                    //家长
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColPatienParents].Value = report.PatientParents;
                    //"身份证号"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColPatientID].Value = report.Patient.IDCard;
                    //"性别"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColSex].Value = report.Patient.Sex.Name;
                    //"出生日期"                
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColBirthday].Value = report.Patient.Birthday.ToString("yyyy-MM-dd");
                    //"年龄"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColAge].Text = report.Patient.Age + report.AgeUnit;
                    //"职业"
                    if (report.Patient.Profession.ID != null && report.Patient.Profession.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColPatientProfession].Value =
                            proHelper.GetObjectFromID(report.Patient.Profession.ID).Name;
                    }
                    //"工作单位"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColWorkPlace].Value = report.Patient.CompanyName;
                    //"联系电话"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColTelephone].Value = report.Patient.PhoneHome;
                    //"患者来源"
                    string homearea = "";
                    switch (report.HomeArea)
                    {
                        case "0":
                            homearea = "本县区";
                            break;
                        case "1":
                            homearea = "本市其它县区";
                            break;
                        case "2":
                            homearea = "本省其它地市";
                            break;
                        case "3":
                            homearea = "外省";
                            break;
                        case "4":
                            homearea = "港澳台";
                            break;
                        case "5":
                            homearea = "外籍";
                            break;
                        default:
                            break;
                    }
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColHomeArea].Value = homearea;
                    //"详细住址"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColHomePlace].Value = report.Patient.AddressHome;
                    //"患者科室"
                    if (report.PatientDept.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColPatientDept].Value =
                        deptHelper.GetObjectFromID(report.PatientDept.ID).Name;
                    }
                    //"疾病名称"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColDiseaseName].Value = report.Disease.Name;
                    //"发病日期"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColInfectDate].Value = report.InfectDate.ToString("yyyy-MM-dd");
                    //"诊断日期"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColDiagDate].Value = report.DiagnosisTime.ToString("yyyy-MM-dd HH");
                    //"死亡日期"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColDeadDate].Value = report.DeadDate.ToString("yyyy-MM-dd");
                    //"病例分类"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColCaseClassOne].Value =
                        caseHelper.GetObjectFromID(report.CaseClass1.ID).Name;
                    //"病例分类"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColCaseCLassTwo].Value =
                        report.CaseClass2 == "0" ? "急性" : (report.CaseClass2 == "1" ? "慢性" : (report.CaseClass2 == "2" ? "未分型" : ""));
                    //"接触"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColInfectOther].Value = report.InfectOtherFlag == "1" ? "有" : "无";
                    //"附卡"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColAddtion].Value = report.AddtionFlag == "1" ? "有" : "无";
                    //"报卡人"
                    if (report.ReportDoctor.ID != null && report.ReportDoctor.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColReportDoctor].Value = this.employerHelper.GetObjectFromID(report.ReportDoctor.ID).Name;//report.ReportDoctor.ID;
                    }
                    //"报卡科室"
                    if (report.DoctorDept.ID != null && report.DoctorDept.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColReportDept].Value =
                            deptHelper.GetObjectFromID(report.DoctorDept.ID).Name;
                    }
                    //"报卡时间"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColReportTime].Value = report.ReportTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //"作废人"
                    if (report.CancelOper.ID != null && report.CancelOper.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColCancelOper].Value = this.employerHelper.GetObjectFromID(report.CancelOper.ID).Name;
                    }
                    //"作废时间"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColCancelTime].Value = report.CancelTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //修改人
                    if (report.ModifyOper.ID != null && report.ModifyOper.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColModifyOper].Value = this.employerHelper.GetObjectFromID(report.ModifyOper.ID).Name;
                    }
                    //修改时间
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColModifyTime].Value = report.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //"审核人"
                    if (report.ApproveOper.ID != null && report.ApproveOper.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColApproveOper].Value = this.employerHelper.GetObjectFromID(report.ApproveOper.ID).Name;
                    }
                    //审核时间
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColApproveTime].Value = report.ApproveTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //"操作人"
                    if (report.Oper.ID != null && report.Oper.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColOperCode].Value = this.employerHelper.GetObjectFromID(report.Oper.ID).Name;
                    }
                    //"操作科室"
                    if (report.OperDept.ID != null && report.OperDept.ID != "")
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColOperDept].Value = this.deptHelper.GetObjectFromID(report.OperDept.ID).Name;
                    }
                    //"操作时间"
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColOperTime].Value = report.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //状态					
                    this.fpSpread1_Sheet1.Cells[row, (int)fpColSet.ColState].Text = report.State;
                    //登录提示疾病
                    if (this.specialInfectForLogin.GetObjectFromID(report.Disease.ID) != null)
                    {
                        this.fpSpread1_Sheet1.Rows[row].Label = "重";
                    }
                    //实体邦定
                    this.fpSpread1_Sheet1.Rows[row].Tag = report;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("fp赋值失败" + ex.Message);
            }
        }
        #endregion

        #region 数据导出

        /// <summary>
        /// 导出
        /// </summary>
        public void Export()
        {
            try
            {
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.Columns.Count; colIndex++)
                {
                    this.fpSpread1_Sheet1.Columns[colIndex].Visible = true;
                }
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.setFp();
            }
        }
        #endregion

        #endregion

        #region 事件

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //选中的处理
            if (this.fpSpread1_Sheet1.RowCount < 1)
            {
                return;
            }
            if (e.Column == 0)
            {
                if (this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text == "True")
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text = "False";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text = "True";
                }
            }
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            report = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.DCP.Object.CommonReport;//.InfectiousDisease;
            try
            {
                this.lbReportInfo.Text = "<<" + report.ReportNO + ">>"
                    + report.Disease.Name
                    + "  " + deptHelper.GetObjectFromID(report.DoctorDept.ID).Name + "  "
                    + employerHelper.GetObjectFromID(report.ReportDoctor.ID).Name
                    + "  " + report.DiagnosisTime.ToString("yyyy年MM月dd日 HH:MM") + "诊断  "
                    + report.ReportTime.ToString("yyyy年MM月dd日 HH:MM") + "报告";
            }
            catch
            { }
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            //全选的处理
            for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
            {
                this.fpSpread1_Sheet1.Cells[row, 0].Text = "True";
            }
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            //全不选的处理
            for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
            {
                this.fpSpread1_Sheet1.Cells[row, 0].Text = "False";
            }
        }

        private void txtCarNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //			只会根据当前回车的TextBox的Text查询
            this.fpSpread1_Sheet1.RowCount = 0;
            if (e.KeyChar == (char)13)
            {
                ArrayList al = new ArrayList();
                //查询
                FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
                report = this.myReportMgr.GetCommonReportByNO(((System.Windows.Forms.TextBox)sender).Text.Trim());
                //显示查询结果
                if (report == null)
                {
                    return;
                }
                al.Add(report);
                this.myShowReportInfo(al, true);
                //着色
                this.colorFp();
            }
        }

        private void txtInPatienNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            if (e.KeyChar == (char)13)
            {
                ArrayList al = new ArrayList();
                //查询
                al = this.myReportMgr.GetCommonReportListByPatientNO(((System.Windows.Forms.TextBox)sender).Text.Trim());
                //显示查询结果
                this.myShowReportInfo(al, true);
                //着色
                this.colorFp();
            }
        }

        private void txtName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            if (e.KeyChar == (char)13)
            {
                ArrayList al = new ArrayList();
                //查询
                if (this.txtName.Text == "")
                {
                    MessageBox.Show("请录入姓名！");
                    return;
                }
                al = this.myReportMgr.GetReportListByPatientName(((System.Windows.Forms.TextBox)sender).Text.Trim());
                //显示查询结果
                this.myShowReportInfo(al, true);
                //着色
                this.colorFp();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        //		/// <summary>
        //		/// 病历号住院号、姓名回车查询
        //		/// </summary>
        //		/// <param name="sender">相应TextBox</param>
        //		/// <param name="e">按键</param>
        //		private void txtInPatienNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //		{
        //			//只会根据当前回车的TextBox的Text查询
        //			if(e.KeyChar == (char)13)
        //			{
        //				ArrayList al = new ArrayList();
        //				//查询
        //				al = this.myReport.GetReportListByPatientNO(((System.Windows.Forms.TextBox)sender).Text.Trim());
        //				//显示查询结果
        //				this.myShowReportInfo(al);
        //				//着色
        //				this.colorFp();
        //			}
        //		}
        #endregion

        #region 工具栏

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return base.Export(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryReport();
            return base.OnQuery(sender, neuObject);
        }

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("浏览", "浏览选中的报告卡", FS.FrameWork.WinForms.Classes.EnumImageList.L浏览, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "浏览")
            {
                this.ApproveReport();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

    }
}
