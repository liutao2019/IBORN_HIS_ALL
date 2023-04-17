﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Controls;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.HealthRecord.EnumServer;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 门诊诊断录入
    /// </summary>
    public partial class ucCaseInputForClinic : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCaseInputForClinic()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.cbxDeptDiag.Click += new System.EventHandler(this.cbxDeptDiag_Click);
                this.btHistory.Click += new System.EventHandler(this.btHistory_Click);
                this.fpHistory.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpHistory_CellDoubleClick);
                this.fpHistory.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.fpHistory_ColumnWidthChanged);
                this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
                this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
                this.btSave.Click += new System.EventHandler(this.btSave_Click);
                this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
                btInsert.Click += new EventHandler(btInsert_Click);
                this.chkCurrentDeptCase.CheckedChanged += new System.EventHandler(this.chkCurrentDeptCase_CheckedChanged);
                this.Load += new System.EventHandler(this.ucCaseInputForClinic_Load);

                this.fpDiag.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDiag_ButtonClicked);
                this.fpDiag.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDiag_EditChange);
                this.fpDiag.EditModeOn += new EventHandler(fpDiag_EditModeOn);
                this.fpDiag.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpDiag_CellDoubleClick);
                this.fpDiag.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpDiag_CellClick);
                this.fpDiag.DragDrop += new System.Windows.Forms.DragEventHandler(this.fpDiag_DragDrop);
                this.fpDiag.DragEnter += new System.Windows.Forms.DragEventHandler(this.fpDiag_DragEnter);
                this.fpDiag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpDiag_MouseUp);
                this.fpDiag.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.fpDiag_ColumnWidthChanged);
            }
        }

        void fpDiag_EditModeOn(object sender, EventArgs e)
        {
            SetDiagLocation();
        }

        #region 变量
        public delegate void SaveClickDelegate(object sender, ArrayList alDiag);

        /// <summary>
        /// 单击"保存"按钮时,这个没办法,因为门诊医生站,先录诊断再开医嘱.
        /// 这个在用的时候注册就行
        /// </summary>
        public event SaveClickDelegate SaveClicked;

        /// <summary>
        /// 诊断类别
        /// </summary>
        private ArrayList diagnoseType = new ArrayList();

        /// <summary>
        /// 传染病报告，需要维护提示的诊断
        /// </summary>
        private ArrayList alDir = new ArrayList();

        /// <summary>
        /// 诊断列表
        /// </summary>
        public ArrayList alDiag = new ArrayList();

        #region 将诊断传到病历界面
        public delegate void TransportAlDiag(ArrayList arrayDiagnoses);
        public event TransportAlDiag transportAlDiag;
        public ArrayList listDiagnose = new ArrayList();
        #endregion

        /// <summary>
        /// 常用文本
        /// </summary>
        private FS.FrameWork.Models.NeuObject userText = new FS.FrameWork.Models.NeuObject();

        ucUserText ucUserText = new ucUserText();

        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 诊断级别帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper levelHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 诊断分期帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper periodHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 诊断录入配置文件
        /// </summary>
        private string xmlOutCaseInput = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "outcaseinput.xml";

        /// <summary>
        /// 历史诊断配置文件
        /// </summary>
        private string xmlOutCaseHistory = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "outcasehistory.xml";

        /// <summary>
        /// 科室常用诊断
        /// </summary>
        ArrayList alDeptDiag = new ArrayList();

        /// <summary>
        /// 全部诊断
        /// </summary>
        ArrayList alAllDiag = new ArrayList();

        ///<summary>
        /// 当前活动行
        /// </summary>
        public int activeRowNum = 0;//{D6D3096F-80E6-4758-911C-B25D5A9263C0}

        /// <summary>
        /// 中医科名称，中医科使用中医诊断
        /// </summary>
        private string chineseMedicalDept;

        /// <summary>
        /// 中医科名称，中医科使用中医诊断
        /// </summary>
        [Category("参数设置"), Description("中医科名称，中医科使用中医诊断")]
        public string ChineseMedicalDept
        {
            get
            {
                return chineseMedicalDept;
            }
            set
            {
                chineseMedicalDept = value;
            }
        }

        /// <summary>
        /// 中医科是否允许开立ICD诊断
        /// </summary>
        private bool chinaSeeAll = true;

        /// <summary>
        /// 中医科是否允许开立ICD诊断
        /// </summary>
        [Category("参数设置"), Description("中医科是否允许开立ICD诊断")]
        public bool ChinaSeeAll
        {
            get
            {
                return chinaSeeAll;
            }
            set
            {
                chinaSeeAll = value;
            }
        }

        /// <summary>
        /// 使用类别：门诊、住院
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes useType = ServiceTypes.C;

        /// <summary>
        /// 使用类别：门诊、住院
        /// </summary>
        [Category("参数设置"), Description("使用类别：门诊、住院"), DefaultValue(ServiceTypes.C)]
        public FS.HISFC.Models.Base.ServiceTypes UseType
        {
            get
            {
                return useType;
            }
            set
            {
                useType = value;
            }
        }

        /// <summary>
        /// 是否允许自定义诊断
        /// </summary>
        bool isAllowDefineDiag = true;

        /// <summary>
        /// 是否允许自定义诊断
        /// </summary>
        [Category("参数设置"), Description("是否允许自定义诊断"), DefaultValue(true)]
        public bool IsAllowDefineDiag
        {
            get
            {
                return isAllowDefineDiag;
            }
            set
            {
                isAllowDefineDiag = value;
            }
        }

        /// <summary>
        /// 是否显示诊断分类
        /// </summary>
        bool isShowICDCategory = false;

        /// <summary>
        /// 是否显示诊断分类
        /// </summary>
        [Category("参数设置"), Description("是否显示诊断分类"), DefaultValue(ServiceTypes.C)]
        public bool IsShowICDCategory
        {
            set
            {
                isShowICDCategory = value;
                this.ucDiagnose1.IsShowICDCategory = value;
            }
            get
            {
                //return ucDiagnose1.IsShowICDCategory;
                return isShowICDCategory;
            }
        }

        /// <summary>
        /// 是否显示成功提示
        /// </summary>
        private bool isShowSuccessInfo = true;

        /// <summary>
        /// 是否显示成功提示
        /// </summary>
        [Category("参数设置"), Description("是否显示成功提示"), DefaultValue(true)]
        public bool IsShowSuccessInfo
        {
            get
            {
                return isShowSuccessInfo;
            }
            set
            {
                isShowSuccessInfo = value;
            }
        }

        /// <summary>
        /// 是否显示初诊列
        /// </summary>
        private bool isShowDiagnosisFlag = true;

        /// <summary>
        /// 是否显示初诊列
        /// </summary>
        [Category("参数设置"), Description("是否显示初诊列"), DefaultValue(true)]
        public bool IsShowDiagnosisFlag
        {
            get { return isShowDiagnosisFlag; }
            set { isShowDiagnosisFlag = value; }
        }



        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        private string patientId = "";

        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        public string PatientId
        {
            get
            {
                return this.patientId;
            }
            set
            {
                this.patientId = value;
                if (value != "")
                {
                    if (this.regInfo != null)
                    {
                        FS.HISFC.Models.Registration.Register regTemp = CacheManager.RegMgr.GetByClinic(value);

                        if (regTemp != null && !string.IsNullOrEmpty(regTemp.ID))
                        {
                            regInfo = regTemp;
                        }
                    }

                    //显示体重等信息
                    this.ucModifyOutPatientHealthInfo2.RegInfo = regInfo;
                    this.ucModifyOutPatientHealthInfo2.IsVisibleSave = false;
                    this.ucModifyOutPatientHealthInfo2.RememberHelthHistoryDays = this.rememberHelthHistoryDays;

                    this.Display();
                    this.HistoryCase();
                    //录入诊断时，默认新增一空行
                    if (this.fpDiag_Sheet1.RowCount < 1)
                    {
                        this.btAdd_Click(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 患者实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register regInfo = null;

        /// <summary>
        /// 患者挂号时实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegInfo
        {
            get
            {
                return regInfo;
            }
            set
            {
                regInfo = value;
                this.PatientId = regInfo.ID;
            }
        }

        /// <summary>
        /// 是否必须填写传染病报告卡
        /// </summary>
        private bool isMustDcpReport = false;

        /// <summary>
        /// 是否必须填写传染病报告卡
        /// </summary>
        [Category("参数设置"), Description("是否必须填写传染病报告卡"), DefaultValue(false)]
        public bool IsMustDcpReport
        {
            get
            {
                return isMustDcpReport;
            }
            set
            {
                isMustDcpReport = value;
            }
        }


        /// <summary>
        /// 门诊医生站增加诊断时,默认为描述医嘱
        /// </summary>
        private bool isClinicDoctorAdd;

        /// <summary>
        /// 门诊医生站增加诊断时,默认为描述医嘱
        /// </summary>
        public bool IsClinicDoctorAdd
        {
            get
            {
                return this.isClinicDoctorAdd;
            }
            set
            {
                this.isClinicDoctorAdd = value;
            }
        }

        /// <summary>
        /// 是否显示体征录入
        /// </summary>
        private bool isHealthInfoVisible = true;

        /// <summary>
        /// 是否显示体征录入
        /// </summary>
        [Category("参数设置"), Description("是否显示体征录入"), DefaultValue(0)]
        public bool IsHealthInfoVisible
        {
            get
            {
                return this.isHealthInfoVisible;
            }
            set
            {
                this.isHealthInfoVisible = value;
                this.ucModifyOutPatientHealthInfo2.Visible = value;
            }
        }

        /// <summary>
        /// 默认保存体征信息的天数 0 标识不默认
        /// </summary>
        private int rememberHelthHistoryDays = 7;

        /// <summary>
        /// 默认保存体征信息的天数 0 标识不默认
        /// </summary>
        [Category("参数设置"), Description("默认保存体征信息的天数 0 标识不默认 默认:身高、体重"), DefaultValue(0)]
        public int RememberHelthHistoryDays
        {
            get
            {
                return rememberHelthHistoryDays;
            }
            set
            {
                rememberHelthHistoryDays = value;
                this.ucModifyOutPatientHealthInfo2.RememberHelthHistoryDays = value;
            }
        }



        private int rememberCaseDiagnoseDays = 1000;

        [Category("参数设置"), Description("显示历史诊断的默认天数"), DefaultValue(1000)]
        public int RememberCaseDiagnoseDays
        {
            get { return this.rememberCaseDiagnoseDays; }
            set { this.rememberCaseDiagnoseDays = value; }
        }


        private bool isShowCurrentDeptCase = false;


        [Category("参数设置"), Description("是否只显示当前登录科室的历史诊断"), DefaultValue(false)]
        public bool IsShowCurrentDeptCase
        {
            get
            {
                return isShowCurrentDeptCase;
            }
            set
            {
                isShowCurrentDeptCase = value;
                this.chkCurrentDeptCase.Checked = value;
            }
        }





        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitInfo()
        {
            try
            {
                this.ucUserText = this.ucUserText1;

                //初始化表
                this.InitDiagList();

                //设置下拉列表
                this.InitList();

                fpDiag_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;

                this.ucUserText1.OnDoubleClick += new EventHandler(ucUserText1_OnDoubleClick);

                this.ucUserText1.bSetToolTip = false;

                ucDiagnose1.IsShowICDCategory = isShowICDCategory;

                //ucDiagnose1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("InitInfo" + ex.Message);
            }
        }

        /// <summary>
        /// load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCaseInputForClinic_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(this.xmlOutCaseInput))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpDiag.Sheets[0], xmlOutCaseInput);
                }
                if (System.IO.File.Exists(this.xmlOutCaseHistory))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpHistory.Sheets[0], xmlOutCaseHistory);
                }

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载诊断列表,请稍候......");
                Application.DoEvents();

                this.InitInfo();
                fpDiag.ShowListWhenOfFocus = true;

                #region 

                InputMap im;
                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiag.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);


                #endregion
                if (this.ucDiagnose1 == null)
                {
                    this.ucDiagnose1 = new FS.HISFC.Components.Common.Controls.ucDiagnose();
                }
                
                if (this.alDeptDiag != null && this.alDeptDiag.Count > 0)
                {
                    this.ucDiagnose1.AlDiag = this.alDeptDiag;
                }
                else if (this.alAllDiag != null)
                {
                    this.ucDiagnose1.AlDiag = this.alAllDiag;
                }

                this.ucDiagnose1.InitICDCategory(this.alAllDiag);

                cbxDeptDiag_Click(null, null);
                this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
                this.ucDiagnose1.Visible = false;
                this.alDir = CacheManager.GetConList("WARNICD10");

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                this.fpDiag_Sheet1.Columns.Get(5).Visible = this.IsShowDiagnosisFlag;
                this.fpHistory_Sheet1.Columns.Get(5).Visible = this.IsShowDiagnosisFlag;

                if (!isAllowDefineDiag)
                {
                    this.fpDiag_Sheet1.Columns.Get(1).Locked = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("加载诊断列表出错！\r\n" + ex.Message + "\r\n" + ex.StackTrace, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        private void InitList()
        {
            try
            {
                this.fpDiag.SelectNone = true;
                //获取诊断类别
                diagnoseType = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
                diagnoseTypeHelper.ArrayObject = diagnoseType;

                FarPoint.Win.Spread.CellType.ComboBoxCellType type = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                string[] s = new string[diagnoseType.Count];
                for (int i = 0; i < diagnoseType.Count; i++)
                {
                    s[i] = (diagnoseType[i] as FS.FrameWork.Models.NeuObject).Name;
                }

                type.Items = s;
                this.fpDiag_Sheet1.Columns[0].CellType = type;

                //诊断级别
                FarPoint.Win.Spread.CellType.ComboBoxCellType type1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                ArrayList diagLevellist = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DIAGLEVEL);
                this.levelHelper.ArrayObject = diagLevellist;
                type = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                s = new string[diagLevellist.Count];
                for (int i = 0; i < diagLevellist.Count; i++)
                {
                    s[i] = (diagLevellist[i] as FS.FrameWork.Models.NeuObject).Name;
                }

                type1.Items = s;
                this.fpDiag_Sheet1.Columns[10].CellType = type1;


                //诊断分期
                FarPoint.Win.Spread.CellType.ComboBoxCellType type2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                ArrayList diagPeriod = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DIAGPERIOD);
                this.periodHelper.ArrayObject = diagPeriod;
                s = new string[diagPeriod.Count];
                for (int i = 0; i < diagPeriod.Count; i++)
                    s[i] = (diagPeriod[i] as FS.FrameWork.Models.NeuObject).Name;
                type2.Items = s;
                this.fpDiag_Sheet1.Columns[9].CellType = type2;

                this.fpDiag_Sheet1.Rows.Remove(0, this.fpDiag_Sheet1.Rows.Count);

                if (this.alDiag.Count <= 0)
                {
                    if (this.fpDiag_Sheet1.Rows.Count > 1)
                    {
                        this.fpDiag_Sheet1.Rows.Add(0, 1);

                        if (diagnoseType != null && diagnoseType.Count > 0)
                        {
                            this.fpDiag_Sheet1.Cells[0, 0].Value = diagnoseType[0];
                        }

                        this.fpDiag_Sheet1.Cells[0, 1].Value = false;
                        this.fpDiag_Sheet1.Cells[0, 4].Value = true;
                        this.fpDiag_Sheet1.Cells[0, 5].Value = true;
                        //诊断日期
                        this.fpDiag_Sheet1.Cells[0, 6].Text = System.DateTime.Now.Date.ToShortDateString();
                        //诊断医生代码
                        this.fpDiag_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpDiag_Sheet1.Cells[0, 7].Text = CacheManager.DiagMgr.Operator.ID;
                        //诊断医生名称
                        this.fpDiag_Sheet1.Cells[0, 8].Text = CacheManager.DiagMgr.Operator.Name;

                        this.fpDiag_Sheet1.Cells[0, 9].Value = "一期";
                        this.fpDiag_Sheet1.Cells[0, 10].Value = "普通";

                        this.fpDiag.Focus();
                        this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.Rows.Count - 1, 2, false);
                    }
                    else
                    {
                        this.fpDiag_Sheet1.Rows.Add(0, 1);
                        this.fpDiag_Sheet1.Cells[0, 0].Value = s[0];
                        this.fpDiag_Sheet1.Cells[0, 1].Value = false;
                        this.fpDiag_Sheet1.Cells[0, 4].Value = true;
                        this.fpDiag_Sheet1.Cells[0, 5].Value = true;
                        //诊断日期
                        this.fpDiag_Sheet1.Cells[0, 6].Text = System.DateTime.Now.Date.ToShortDateString();
                        //诊断医生代码
                        this.fpDiag_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpDiag_Sheet1.Cells[0, 7].Text = CacheManager.DiagMgr.Operator.ID;
                        //诊断医生名称
                        this.fpDiag_Sheet1.Cells[0, 8].Text = CacheManager.DiagMgr.Operator.Name;

                        this.fpDiag.Focus();
                        this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.Rows.Count - 1, 2, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("InitList" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化表
        /// </summary>
        private void InitDiagList()
        {
            alDeptDiag = CacheManager.HealthIntegrate.QueryDeptDiag(CacheManager.LogEmpl.Dept.ID);
            if (alDeptDiag == null)
            {
                MessageBox.Show("获取科室常用诊断出错：" + CacheManager.HealthIntegrate.Err);
                return;
            }

            bool isChineseMedicalDept = false;
            if (!string.IsNullOrEmpty(chineseMedicalDept))
            {
                if (chineseMedicalDept.Contains(CacheManager.LogEmpl.Dept.Name)
                    || CacheManager.LogEmpl.Dept.Name.Contains(chineseMedicalDept))
                {
                    isChineseMedicalDept = true;
                }
            }

            alAllDiag = CacheManager.HealthIntegrate.ICDQueryNew(ICDTypes.ICD10, QueryTypes.Valid, CacheManager.LogEmpl.Dept.ID);
            if (alAllDiag == null)
            {
                MessageBox.Show("获取全部诊断出错：" + CacheManager.HealthIntegrate.Err);
                return;
            }

            if (!isChineseMedicalDept
                && chinaSeeAll)
            {
                //过滤诊断类型
                if (!string.IsNullOrEmpty(chineseMedicalDept))
                {
                    ArrayList alTemp = new ArrayList();
                    foreach (FS.HISFC.Models.HealthRecord.ICD icd in alAllDiag)
                    {
                        if (isChineseMedicalDept)
                        {
                            if (icd.TraditionalDiag)
                            {
                                alTemp.Add(icd);
                            }
                        }
                        else
                        {
                            if (!icd.TraditionalDiag)
                            {
                                alTemp.Add(icd);
                            }
                        }
                    }

                    alAllDiag = alTemp;
                }
            }
        }

        #endregion

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        /// <summary>
        /// 处理诊断上下键响应 xiaohf 2012年7月2日16:13:50
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Up)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ShowDiagPanel();
                        // this.ucDiagnose1.SetFocus();
                        this.ucDiagnose1.PriorRow();
                        return true;
                    }
                }
                else if (keyData == Keys.Down)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ShowDiagPanel();
                        // this.ucDiagnose1.SetFocus();
                        this.ucDiagnose1.NextRow();
                        return true;
                    }
                }
                else if (keyData == Keys.Escape)
                {
                    this.ucDiagnose1.Visible = false;
                }
            }
            catch { }

            return base.ProcessCmdKey(ref msg, keyData);
            //    return true;
        }

        /// <summary>
        /// 设置诊断输入框位置
        /// </summary>
        private void ShowDiagPanel()
        {
            this.ucDiagnose1.Show();
        }

        /// <summary>
        /// 设置诊断录入框位置
        /// </summary>
        private void SetDiagLocation()
        {
            Control cell = this.fpDiag.EditingControl;
            if (cell == null) return;

            int y = fpDiag.Top + cell.Top + cell.Height + this.ucDiagnose1.Height + 7;
            if (y <= this.Height)
            {
                if (fpDiag.Left + cell.Left + this.ucDiagnose1.Width + 20 <= this.Width)
                {
                    this.ucDiagnose1.Location = new Point(fpDiag.Left + cell.Left + 20, y - this.ucDiagnose1.Height);
                }
                else
                {
                    this.ucDiagnose1.Location = new Point(this.Width - this.ucDiagnose1.Width - 10, y - this.ucDiagnose1.Height);
                }
            }
            else
            {
                if (fpDiag.Left + cell.Left + this.ucDiagnose1.Width + 20 <= this.Width)
                {
                    this.ucDiagnose1.Location = new Point(fpDiag.Left + cell.Left + 20, fpDiag.Top + cell.Top - this.ucDiagnose1.Height - 7);
                }
                else
                {
                    this.ucDiagnose1.Location = new Point(this.Width - this.ucDiagnose1.Width - 10, fpDiag.Top + cell.Top - this.ucDiagnose1.Height - 7);
                }
            }
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.ucDiagnose1.Visible = false;
            }
            else if (keyData == Keys.Up)
            {
                if (this.ucDiagnose1.Visible == true)
                {
                    this.ShowDiagPanel();
                    this.ucDiagnose1.SetFocus();
                    this.ucDiagnose1.PriorRow();
                    return true;
                }
            }
            else if (keyData == Keys.Down)
            {
                if (this.ucDiagnose1.Visible == true)
                {
                    this.ucDiagnose1.Focus();
                    this.ucDiagnose1.SetFocus();
                    this.ucDiagnose1.NextRow();
                    return true;
                }
            }
            else if (keyData == Keys.Enter)
            {
                if (this.ucDiagnose1.Visible)
                {
                    this.ucDiagnose1_SelectItem(keyData);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 选择诊断项目
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ucDiagnose1_SelectItem(Keys key)
        {
            GetInfo();
            return 0;
        }

        /// <summary>
        /// 得到诊断
        /// </summary>
        /// <returns></returns>
        private int GetInfo()
        {
            try
            {
                FS.HISFC.Models.HealthRecord.ICD item = null;
                if (this.ucDiagnose1.GetItem(ref item) == -1)
                {
                    //MessageBox.Show("获取项目出错!","提示");
                    ucDiagnose1.Visible = false;
                    return -1;
                }
                if (item == null)
                    return -1;

                //ICD诊断名称
                fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Text = item.ID;
                //ICD诊断编码
                fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 3].Text = item.Name;
                //fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex,2].Locked = true;
                //ICD诊断编码
                fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 3].Locked = true;
                ucDiagnose1.Visible = false;
                fpDiag.Focus();
                fpDiag_Sheet1.SetActiveCell(fpDiag_Sheet1.ActiveRowIndex, 4);
            }
            catch (Exception ex)
            {
                ucDiagnose1.Visible = false;
                MessageBox.Show("GetInfo" + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = this.fpDiag_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.HealthRecord.Diagnose;

                if (diag != null)
                {
                    MessageBox.Show("该诊断已经保存,不允许修改，只能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (e.Column == 2)
                    {
                        this.fpDiag_Sheet1.Cells[e.Row, e.Column].Text = diag.DiagInfo.ICD10.ID;
                    }
                    return;
                    //if (!diag.DiagInfo.IsValid)
                    //{
                    //MessageBox.Show("该诊断已经作废,不允许修改!", "提示");
                    //return;
                    //}
                }
                if (e.Column == 2)
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ShowDiagPanel();
                    }
                    else
                    {
                        SetDiagLocation();
                    }
                    string str = fpDiag_Sheet1.ActiveCell.Text;

                    string strFilter = str;
                    //char split = '%';
                    //for (int i = 0; i < str.Length; i++)
                    //{
                    //    strFilter += str[i] + split.ToString();
                    //}
                    //strFilter = strFilter.TrimEnd(split).TrimStart(split);
                    this.ucDiagnose1.Filter(strFilter, false);
                }
                else
                {
                    this.ucDiagnose1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("fpDiag_EditChange" + ex.Message);
            }
        }

        /// <summary>
        /// 选择患者赋值
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e.Tag == null)
            {
                this.lblRegInfo.Text = "";
                this.fpHistory_Sheet1.RowCount = 0;
                this.fpDiag_Sheet1.RowCount = 0;
                return -1;
            }
            if (e.Tag is FS.HISFC.Models.Registration.Register)
            {
                this.regInfo = e.Tag as FS.HISFC.Models.Registration.Register;
                this.PatientId = regInfo.ID;
            }
            return 1;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, System.EventArgs e)
        {
            AddNew(fpDiag_Sheet1.Rows.Count);
        }

        private void AddNew(int rowIndex)
        {
            if (this.regInfo.IsSee
                && this.regInfo.DoctorInfo.SeeDate.AddDays(1) < CacheManager.RegMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,诊断不允许修改!", "提示");
                return;
            }

            this.fpDiag_Sheet1.Rows.Add(rowIndex, 1);

            this.fpDiag_Sheet1.Columns[2].Locked = false;


            if (this.diagnoseType != null && this.diagnoseType.Count > 1)
            {
                if (rowIndex == 1)
                {
                    //新增的默认为其他诊断
                    this.fpDiag_Sheet1.Cells[rowIndex, 0].Value = this.diagnoseType[1];
                }
                else
                {
                    this.fpDiag_Sheet1.Cells[rowIndex, 0].Value = this.diagnoseType[0];
                }
            }

            if (this.isClinicDoctorAdd)
            {
                this.fpDiag_Sheet1.Cells[rowIndex, 1].Value = true;
            }
            else
            {
                this.fpDiag_Sheet1.Cells[rowIndex, 1].Value = false;
            }

            if (System.Convert.ToBoolean(this.fpDiag_Sheet1.Cells[rowIndex, 1].Value) == false)
            {
                this.fpDiag_Sheet1.Cells[rowIndex, 3].Locked = true;
            }
            else
            {
                this.fpDiag_Sheet1.Cells[rowIndex, 3].Locked = false;
            }

            //this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.Rows.Count-1,4].Value = true;去掉拟诊的默认打钩
            this.fpDiag_Sheet1.Cells[rowIndex, 4].Value = false;
            this.fpDiag_Sheet1.Cells[rowIndex, 5].Value = true;
            //诊断日期
            this.fpDiag_Sheet1.Cells[rowIndex, 6].Text = System.DateTime.Now.Date.ToShortDateString();
            //诊断医生代码
            this.fpDiag_Sheet1.Cells[rowIndex, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpDiag_Sheet1.Cells[rowIndex, 7].Text = CacheManager.DiagMgr.Operator.ID;
            //诊断医生名称
            this.fpDiag_Sheet1.Cells[rowIndex, 8].Text = CacheManager.DiagMgr.Operator.Name;
            this.fpDiag_Sheet1.Cells[rowIndex, 9].Text = "一期";
            this.fpDiag_Sheet1.Cells[rowIndex, 10].Text = "一级";

            this.fpDiag_Sheet1.SetActiveCell(rowIndex, 2, false);
            this.Focus();
            this.fpDiag.Focus();
        }

        void btInsert_Click(object sender, EventArgs e)
        {
            AddNew(this.fpDiag_Sheet1.ActiveRowIndex + 1);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelete_Click(object sender, System.EventArgs e)
        {
            this.CloseUcDiagnoseForm();
            this.Delete();
        }

        private void Delete()
        {
            if (this.fpDiag_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpDiag_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Text)
                && string.IsNullOrEmpty(this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 3].Text)
                )
            {

            }
            else
            {
                DialogResult r = MessageBox.Show("确定要删除该诊断吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.No)
                {
                    return;
                }
            }
            //数据库里存在的 ！= null
            if (this.fpDiag_Sheet1.Rows[this.fpDiag_Sheet1.ActiveRowIndex].Tag != null)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = this.fpDiag_Sheet1.Rows[this.fpDiag_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.HealthRecord.Diagnose;
                if (diag != null)
                {
                    MessageBox.Show("该诊断已经保存，只可以作废！", "提示");
                    return;
                }
            }
            this.fpDiag_Sheet1.Rows.Remove(this.fpDiag_Sheet1.ActiveRowIndex, 1);
        }


        /// <summary>
        /// 作废诊断
        /// </summary>
        public void CancelDiag()
        {
            if (this.fpDiag_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpDiag_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            DialogResult r = MessageBox.Show("确实要作废此诊断吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
            {
                return;
            }
            //{D6D3096F-80E6-4758-911C-B25D5A9263C0}
            //FS.HISFC.Models.HealthRecord.Diagnose diag1 = this.fpDiag_Sheet1.Rows[this.fpDiag_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.HealthRecord.Diagnose;
            FS.HISFC.Models.HealthRecord.Diagnose diag1 = this.fpDiag_Sheet1.Rows[this.activeRowNum].Tag as FS.HISFC.Models.HealthRecord.Diagnose;
            if (diag1 == null)
            {
                MessageBox.Show("该诊断尚未保存，不需要作废！", "提示");
                return;
            }
            try
            {
                int flag = CacheManager.DiagMgr.CancelDiagnoseSingleForClinic(this.PatientId, diag1.DiagInfo.ICD10.ID, diag1.DiagInfo.HappenNo.ToString());
                if (flag == 1 && this.listDiagnose.Count > 0 && listDiagnose != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in listDiagnose)
                    {
                        if (diag.DiagInfo.HappenNo == diag1.DiagInfo.HappenNo)
                        {
                            listDiagnose.Remove(diag);
                            this.activeRowNum = 0;//{D6D3096F-80E6-4758-911C-B25D5A9263C0}
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CancelDiag" + ex.Message);
            }
            this.Display();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        /// <summary>
        /// 验证诊断
        /// </summary>
        /// <param name="tem"></param>
        /// <returns></returns>
        private int DiagCheck(FS.HISFC.Models.HealthRecord.Diagnose tem)
        {
            if (tem == null)
            {
                MessageBox.Show("诊断实体转换出错！");
                return -1;
            }
            if (tem.DiagInfo.DiagType.ID == "")
            {
                MessageBox.Show("诊断类别代码不能为空！");
                return -1;
            }
            if (tem.DiagInfo.Patient.ID == "")
            {
                MessageBox.Show("患者卡号不能为空！");
                return -1;
            }
            if (tem.DiagInfo.ICD10.ID == "")
            {
                MessageBox.Show("诊断ICD10代码不能为空！");
                return -1;
            }
            if (tem.DiagInfo.ICD10.Name == "")
            {
                MessageBox.Show("诊断ICD10名称不能为空！");
                return -1;
            }
            if (tem.DiagInfo.Doctor.ID == "")
            {
                MessageBox.Show("诊断医生代码不能为空！");
                return -1;
            }
            if (tem.DiagInfo.Doctor.Name == "")
            {
                MessageBox.Show("诊断医生姓名不能为空！");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public int Save()
        {
            if (this.regInfo == null
                || string.IsNullOrEmpty(regInfo.ID))
            {
                MessageBox.Show("请选择患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            if (this.alDiag.Count > 0)
            {
                this.alDiag.Clear();
            }
            if (this.listDiagnose.Count > 0)
            {
                this.listDiagnose.Clear();
            }
            if (this.regInfo.IsSee && this.regInfo.DoctorInfo.SeeDate.AddDays(1) < CacheManager.RegMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,诊断不允许修改!", "提示");
                return -1;
            }

            //DialogResult dr = MessageBox.Show("保存后不可修改,是否确认保存？", "提示", MessageBoxButtons.OKCancel);
            //if (dr != DialogResult.OK)
            //{
            //    return -1;
            //}

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            bool isSuccess = true;//是否保存成功
            for (int i = 0; i < this.fpDiag_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = new FS.HISFC.Models.HealthRecord.Diagnose();
                FS.HISFC.Models.HealthRecord.Diagnose temp = null;
                if (this.fpDiag_Sheet1.Rows[i].Tag != null)
                {
                    temp = this.fpDiag_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Diagnose;
                    //if (temp.Is30Disease == "0")
                    if (!temp.IsValid)
                    {
                        alDiag.Remove(temp);
                        continue;
                    }
                    if (temp != null)
                    {
                        diag.DiagInfo.HappenNo = temp.DiagInfo.HappenNo;
                    }
                    else
                    {
                        diag.DiagInfo.HappenNo = -1;
                    }
                }
                else
                {
                    diag.DiagInfo.HappenNo = -1;
                }
                diag.DiagInfo.Patient.ID = this.patientId;
                //诊断类别
                diag.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(this.fpDiag_Sheet1.Cells[i, 0].Text);
                bool isLocked = FS.FrameWork.Function.NConvert.ToBoolean(this.fpDiag_Sheet1.GetValue(i, 1));
                diag.DiagInfo.IsMain = isLocked;//借用一下

                //诊断ICD码
                if (isLocked)
                {
                    diag.DiagInfo.ICD10.ID = "MS999";
                }
                else
                    diag.DiagInfo.ICD10.ID = this.fpDiag_Sheet1.Cells[i, 2].Text;
                if (diag.DiagInfo.ICD10.ID == "" && isLocked == false)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("描述诊断请选择描述标志！", "提示");
                    this.fpDiag.Focus();
                    this.fpDiag_Sheet1.SetActiveCell(i, 1);
                    return -1;
                }
                if (isLocked == true && this.fpDiag_Sheet1.Cells[i, 3].Text.Trim() == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("请填写描述诊断！", "提示");
                    this.fpDiag.Focus();
                    this.fpDiag_Sheet1.SetActiveCell(i, 3);
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = this.levelHelper.GetObjectFromName(this.fpDiag_Sheet1.Cells[i, 10].Text);
                if (obj != null)
                {
                    diag.LevelCode = obj.ID;
                }
                obj = this.periodHelper.GetObjectFromName(this.fpDiag_Sheet1.Cells[i, 9].Text);
                if (obj != null)
                {
                    diag.PeriorCode = obj.ID;
                }

                //诊断名称
                diag.DiagInfo.ICD10.Name = this.fpDiag_Sheet1.Cells[i, 3].Text;
                //是否疑诊
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpDiag_Sheet1.GetValue(i, 4)))
                {
                    diag.DubDiagFlag = "1";
                }
                else
                {
                    diag.DubDiagFlag = "0";
                }
                //是否初诊
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpDiag_Sheet1.GetValue(i, 5)))
                {
                    diag.Diagnosis_flag = "1";
                }
                else
                {
                    diag.Diagnosis_flag = "0";
                }
                //诊断日期
                diag.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpDiag_Sheet1.GetValue(i, 6));//11
                //输入类别
                diag.OperType = "1";
                diag.DiagInfo.Doctor.ID = this.fpDiag_Sheet1.Cells[i, 7].Text;
                diag.DiagInfo.Doctor.Name = this.fpDiag_Sheet1.Cells[i, 8].Text;
                if (this.DiagCheck(diag) < 0)
                {
                    return -1;
                }

                if (temp != null)
                {
                    diag.IsValid = temp.IsValid;
                    //diag.Is30Disease = temp.Is30Disease;//后增[2008/01/25]
                }
                else
                {
                    diag.IsValid = true;
                    //diag.Is30Disease = "1";//默认都是有效的呗[2008/01/25]
                }

                diag.PerssonType = FS.HISFC.Models.Base.ServiceTypes.C;

                int j = CacheManager.DiagMgr.UpdateDiagnoseForClinic(diag);
                if (j == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "保存失败\r\n" + CacheManager.DiagMgr.Err, "提示");
                    isSuccess = false;
                    return -1;
                }
                else if (j == 0)
                {
                    if (CacheManager.DiagMgr.InsertDiagnose(diag) > 0)
                    {
                        this.Warning(diag);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "保存失败\r\n" + CacheManager.DiagMgr.Err, "提示");
                        isSuccess = false;
                        return -1;
                    }
                }
                this.listDiagnose.Add(diag);
                this.alDiag.Add(diag);
            }

            //保存体重等体征信息
            int rev = this.ucModifyOutPatientHealthInfo2.Save();
            if (rev == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.ucModifyOutPatientHealthInfo2.ErrInfo);
                return -1;
            }
            else if (rev == 0)
            {
                ucModifyOutPatientHealthInfo2.GetHealthInfo(ref this.regInfo);
            }


            if (isSuccess == true)
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                if (isShowSuccessInfo)
                {
                    MessageBox.Show("诊断保存成功", "提示");
                }
                else
                {
                    Components.Order.Classes.Function.ShowBalloonTip(4, "提示", "\r\n诊断保存成功！\r\n\r\n\r\n", ToolTipIcon.Info);
                }
            }

            Display();

            //门诊医生站用的[2008/8/27]
            if (this.SaveClicked != null)
            {
                this.SaveClicked(this, isMustDcpReport ? listDiagnose : null);
            }
            //END[2008/8/27]

            return 1;
        }

        /// <summary>
        /// 显示
        /// </summary>
        private void Display()
        {
            if (alDiag.Count > 0)
            {
                if (this.transportAlDiag != null)
                {
                    this.transportAlDiag(listDiagnose);
                }
            }
            if (regInfo.IsAccount)
            {
                this.lblRegInfo.Text = "卡号: " + this.regInfo.PID.CardNO + " 姓名: " + this.regInfo.Name + " 性别: " + this.regInfo.Sex.Name + " 挂号时间: " + this.regInfo.DoctorInfo.SeeDate.ToString() + " 挂号科室: " + this.regInfo.DoctorInfo.Templet.Dept.Name;
            }
            else
            {
                this.lblRegInfo.Text = "卡号: " + this.regInfo.PID.CardNO + " 姓名: " + this.regInfo.Name + " 性别: " + this.regInfo.Sex.Name + " 挂号时间: " + this.regInfo.DoctorInfo.SeeDate.ToString() + " 挂号科室: " + this.regInfo.DoctorInfo.Templet.Dept.Name;
            }
            ArrayList al = null;
            try
            {
                al = CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获得患者的诊断信息出错！" + ex.Message, "提示");
                return;
            }
            if (al == null)
            {
                return;
            }

            //付给当前数组
            this.alDiag = al;
            //如果为空，重取，否则下面出异常
            if (this.diagnoseTypeHelper.ArrayObject.Count <= 0)
            {
                this.diagnoseTypeHelper.ArrayObject = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            }
            //清空
            if (this.fpDiag_Sheet1.Rows.Count > 0)
            {
                this.fpDiag_Sheet1.Rows.Remove(0, this.fpDiag_Sheet1.Rows.Count);
            }
            //填充
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                this.fpDiag_Sheet1.Rows.Add(0, 1);

                //if (diag.Is30Disease == "0")//借用这个字段,代表是否有效诊断
                if (!diag.IsValid)//借用这个字段,代表是否有效诊断
                {
                    this.fpDiag_Sheet1.Rows[0].BackColor = Color.Red;//无效诊断背景色
                }

                this.fpDiag_Sheet1.Cells[0, 0].Text = this.diagnoseTypeHelper.GetObjectFromID(diag.DiagInfo.DiagType.ID).Name;
                this.fpDiag_Sheet1.Cells[0, 1].Value = diag.DiagInfo.IsMain;//是否描述
                this.fpDiag_Sheet1.Cells[0, 2].Text = diag.DiagInfo.ICD10.ID;//icd码
                this.fpDiag_Sheet1.Cells[0, 3].Text = diag.DiagInfo.ICD10.Name;//icd名称
                if (diag.DiagInfo.ICD10.ID == "MS999")
                {
                    this.fpDiag_Sheet1.Cells[0, 3].Locked = false;
                    this.fpDiag_Sheet1.Cells[0, 2].Locked = true;
                }
                else
                {
                    this.fpDiag_Sheet1.Cells[0, 2].Locked = false;
                    this.fpDiag_Sheet1.Cells[0, 3].Locked = true;
                }
                this.fpDiag_Sheet1.Cells[0, 4].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.DubDiagFlag);//是否疑诊
                this.fpDiag_Sheet1.Cells[0, 5].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.Diagnosis_flag);//是否初诊
                this.fpDiag_Sheet1.Cells[0, 6].Text = diag.DiagInfo.DiagDate.Date.ToShortDateString();//日期
                this.fpDiag_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpDiag_Sheet1.Cells[0, 7].Text = diag.DiagInfo.Doctor.ID;//代码
                this.fpDiag_Sheet1.Cells[0, 8].Text = diag.DiagInfo.Doctor.Name;//诊断医生
                if (this.periodHelper.GetObjectFromID(diag.PeriorCode) != null)
                {
                    this.fpDiag_Sheet1.Cells[0, 9].Text = this.periodHelper.GetObjectFromID(diag.PeriorCode).Name;
                }
                if (this.levelHelper.GetObjectFromID(diag.LevelCode) != null)
                {
                    this.fpDiag_Sheet1.Cells[0, 10].Text = this.levelHelper.GetObjectFromID(diag.LevelCode).Name;
                }
                this.fpDiag_Sheet1.Rows[0].Tag = diag;

                this.fpDiag_Sheet1.Rows[0].Locked = true;
                this.fpDiag_Sheet1.Cells[0, 3].Locked = true;
            }
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public void SetFocus()
        {
            this.Focus();

            this.fpDiag.Focus();

            if (this.fpDiag_Sheet1.ActiveRowIndex >= 0)
            {
                this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.ActiveRowIndex, 2, false);
            }
        }

        /// <summary>
        /// 设置文字模板
        /// </summary>
        /// <param name="text"></param>
        public void SetControl(ucUserText text)
        {
            this.ucUserText = text;
        }

        /// <summary>
        /// 历史诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btHistory_Click(object sender, System.EventArgs e)
        {
            this.HistoryCase();
        }

        /// <summary>
        /// 历史诊断
        /// </summary>
        private void HistoryCase()
        {
            DataSet ds = new DataSet();

            if (this.regInfo == null)
            {
                MessageBox.Show("查询患者信息出错!" + CacheManager.RegMgr.Err);
                return;
            }

            if (this.fpHistory_Sheet1.RowCount > 0)
            {
                this.fpHistory_Sheet1.Rows.Remove(0, this.fpHistory_Sheet1.RowCount);
            }

            ArrayList al = CacheManager.RegMgr.Query(this.regInfo.PID.CardNO, DateTime.Now.AddDays(-rememberCaseDiagnoseDays));
            if (al == null)
            {
                MessageBox.Show("查询诊断信息出错！\r\n" + CacheManager.RegMgr.Err, "错误", MessageBoxButtons.OK);
                return;
            }

            ArrayList alDiag = new ArrayList();
            ArrayList alTemp = null;
            FS.HISFC.Models.Registration.Register rr = null;
            for (int i = al.Count - 1; i >= 0; i--)
            {
                rr = al[i] as FS.HISFC.Models.Registration.Register;

                alTemp = new ArrayList();
                alTemp = CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(rr.ID, frmTypes.DOC, false);
                if (alTemp == null)
                {
                    MessageBox.Show(CacheManager.DiagMgr.Err);
                }
                else
                {

                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alTemp)
                    {
                        diag.OperInfo.Dept.ID = rr.SeeDoct.Dept.ID;
                    }
                    alDiag.AddRange(alTemp);
                }

            }
            this.fpHistory_Sheet1.RowCount = alDiag.Count;
            FS.HISFC.Models.HealthRecord.Diagnose diagObj = null;
            for (int i = 0; i < alDiag.Count; i++)
            {
                diagObj = alDiag[i] as FS.HISFC.Models.HealthRecord.Diagnose;
                this.fpHistory_Sheet1.Cells[i, 0].Text = diagnoseTypeHelper.GetName(diagObj.DiagInfo.DiagType.ID);
                this.fpHistory_Sheet1.Cells[i, 1].Text = diagObj.ID == "MS999" ? "是" : "否";
                this.fpHistory_Sheet1.Cells[i, 2].Text = diagObj.DiagInfo.ICD10.ID;
                this.fpHistory_Sheet1.Cells[i, 3].Text = diagObj.DiagInfo.ICD10.Name;
                this.fpHistory_Sheet1.Cells[i, 4].Text = diagObj.DubDiagFlag == "1" ? "是" : "否";
                //this.fpHistory_Sheet1.Cells[i, 5].Text = diagObj.Is30Disease;
                //this.fpHistory_Sheet1.Cells[i, 5].Text = diagObj.IsValid ? "1" : "0";
                this.fpHistory_Sheet1.Cells[i, 5].Text = diagObj.Diagnosis_flag == "1" ? "是" : "否";
                this.fpHistory_Sheet1.Cells[i, 6].Text = diagObj.DiagInfo.Doctor.Name;
                this.fpHistory_Sheet1.Cells[i, 7].Text = diagObj.OperInfo.OperTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (this.levelHelper.GetObjectFromID(diagObj.LevelCode) != null)
                {
                    this.fpHistory_Sheet1.Cells[i, 8].Text = this.levelHelper.GetObjectFromID(diagObj.LevelCode).Name;
                }

                //挂号信息是否为当前登录科室
                bool isCurrentDept = (diagObj.OperInfo.Dept.ID == ((HISFC.Models.Base.Employee)CacheManager.DiagMgr.Operator).Dept.ID ? true : false);


                if (isShowCurrentDeptCase)
                {
                    this.fpHistory_Sheet1.Rows[i].Visible = isCurrentDept;
                }
                else
                {
                    this.fpHistory_Sheet1.Rows[i].Visible = true;
                }




                //if (diagObj.Is30Disease == "0")
                if (!diagObj.IsValid)
                {
                    this.fpHistory_Sheet1.Rows[i].BackColor = Color.Red;//无效诊断背景色
                }
            }

            //历史诊断按诊断日期降序排序           
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ds.Tables[0].DefaultView.Sort = "诊断日期 desc";
            //    DataSet dsNew = new DataSet();
            //    DataTable dt = ds.Tables[0].DefaultView.ToTable();
            //    dsNew.Tables.Add(dt);
            //    if (dsNew != null && dsNew.Tables.Count > 0 && dsNew.Tables[0].Rows.Count > 0)
            //    {
            //        this.fpHistory_Sheet1.DataSource = dsNew;
            //    }
            //}
        }

        /// <summary>
        /// 作废诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, System.EventArgs e)
        {
            this.CancelDiag();
        }

        /// <summary>
        /// 科室常用诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDeptDiag_Click(object sender, EventArgs e)
        {
            if (this.cbxDeptDiag.CheckState == CheckState.Checked)
            {
                this.ucDiagnose1.AlDiag = this.alDeptDiag;
            }
            if (this.cbxDeptDiag.CheckState == CheckState.Unchecked)
            {
                this.ucDiagnose1.AlDiag = this.alAllDiag;
            }
        }

        /// <summary>
        /// 隐藏诊断输入窗口
        /// </summary>
        private void CloseUcDiagnoseForm()
        {
            if (this.ucDiagnose1.Visible == true)
            {
                this.ucDiagnose1.Hide();
            }
        }


        /// <summary>
        /// 诊断录入判断算法，需添加详细内容
        /// </summary>
        /// <param name="diag"></param>
        private void Warning(FS.HISFC.Models.HealthRecord.Diagnose diag)
        {
            //如果是传染病诊断，提示填写传染病报告卡
            if (CacheManager.DiagMgr.IsInfect(diag.DiagInfo.ICD10.ID) == "1")
            {
                //如果是需要提示的项目，提示维护的内容
                foreach (FS.FrameWork.Models.NeuObject obj in alDir)
                {
                    if (obj.ID == diag.DiagInfo.ICD10.ID)
                        MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "提示：" + obj.Name, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #region 诊断模板操作

        /// <summary>
        /// 双击加入诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucUserText1_OnDoubleClick(object sender, EventArgs e)
        {
            if (this.fpDiag_Sheet1.Rows[fpDiag_Sheet1.ActiveRowIndex].Locked)
            {
                MessageBox.Show("该诊断已经保存,不允许修改，只能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.ucUserText1.GetSelectedNode() == null || this.ucUserText1.GetSelectedNode().Tag == null)
                return;

            //在诊断列表找不到时，默认为描述诊断
            string filter = "";

            FS.HISFC.Models.Base.UserText userText=this.ucUserText1.GetSelectedNode().Tag as FS.HISFC.Models.Base.UserText;
            if (userText != null)
            {
                filter = userText.Text;
            }
            else 
            {
                filter=this.ucUserText1.GetSelectedNode().Tag.ToString();
            }
            

            if (filter.Length <= 0)
                return;

            if (!this.ucDiagnose1.hsDiags.Contains(filter))
            {
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 1].Value = true;
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Locked = true;
            }
            else
            {
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 1].Value = false;
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Locked = false;
            }

            if (System.Convert.ToBoolean(this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 1].Value) == true)
            {
                this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.ActiveRowIndex, 3, false);
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Text = "MS999";
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 3].Text = this.ucUserText1.GetSelectedNode().Text;
            }
            else
            {
                string data = filter;
                this.ucDiagnose1.isDrag = true;
                this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.ActiveRowIndex, 2, false);
                this.fpDiag_Sheet1.SetValue(this.fpDiag_Sheet1.ActiveRowIndex, 2, data);
                this.ucDiagnose1.isDrag = false;
                this.ucDiagnose1.Filter(data, true);
                this.GetInfo();
            }
        }

        /// <summary>
        /// 选择诊断医师？ 怎么能随便选择医师呢？？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.activeRowNum = e.Row;//{D6D3096F-80E6-4758-911C-B25D5A9263C0}
            FS.HISFC.Models.HealthRecord.Diagnose diag = this.fpDiag_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.HealthRecord.Diagnose;

            if (diag != null)
            {
                //MessageBox.Show("该诊断已经保存,不允许修改，只能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.fpDiag_Sheet1.Rows[e.Row].Locked = true;
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 3].Locked = true;
                //return;                
            }

            return;
            if (this.fpDiag_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpDiag_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            if (e.Column != 8)
                return;

            //frm.ShowDialog();
            //this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 8].Text = frm.Object.Name;
            //this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 7].Text = frm.Object.ID;
        }


        /// <summary>
        /// 双击删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.ColumnHeader && !e.RowHeader)
            {
                this.Delete();
            }
        }


        /// <summary>
        /// 拖放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (this.fpDiag_Sheet1.Rows[fpDiag_Sheet1.ActiveRowIndex].Locked)
            {
                MessageBox.Show("该诊断已经保存,不允许修改，只能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //在诊断列表找不到时，默认为描述诊断
            string filter = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();

            if (!this.ucDiagnose1.hsDiags.Contains(filter))
            {
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 1].Value = true;
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Locked = true;
            }

            if (System.Convert.ToBoolean(this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 1].Value) == true)
            {
                this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.ActiveRowIndex, 3, false);
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 2].Text = "MS999";
                this.fpDiag_Sheet1.Cells[this.fpDiag_Sheet1.ActiveRowIndex, 3].Text = this.ucUserText1.GetSelectedNode().Text;
            }
            else
            {
                string data = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
                this.ucDiagnose1.isDrag = true;
                this.fpDiag_Sheet1.SetActiveCell(this.fpDiag_Sheet1.ActiveRowIndex, 2, false);
                this.fpDiag_Sheet1.SetValue(this.fpDiag_Sheet1.ActiveRowIndex, 2, data);
                this.ucDiagnose1.isDrag = false;
                this.ucDiagnose1.Filter(data, true);
                this.GetInfo();
            }
            e.Data.SetData("");
        }


        /// <summary>
        /// 右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.fpDiag_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpDiag_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            FarPoint.Win.Spread.Model.CellRange c = this.fpDiag.GetCellFromPixel(0, 0, e.X, e.Y);

            int activeRow = c.Row;

            if (activeRow < 0)
            {
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenu menu = new ContextMenu();

                MenuItem addMenu = new MenuItem("添加到用户常用文本");

                addMenu.Click += new EventHandler(addMenu_Click);

                this.fpDiag.ContextMenu = menu;

                if (this.fpDiag_Sheet1.Cells[activeRow, 1].Text == "TRUE")
                {
                    this.userText.ID = this.fpDiag_Sheet1.ActiveCell.Text;
                    this.userText.Name = this.userText.ID;
                }
                else
                {
                    this.userText.ID = this.fpDiag_Sheet1.Cells[activeRow, 2].Text.Trim();
                    this.userText.Name = this.fpDiag_Sheet1.Cells[activeRow, 3].Text.Trim();
                }

                menu.MenuItems.Add(addMenu);

                menu.Show(this.fpDiag, new Point(e.X, e.Y));
            }
        }


        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMenu_Click(object sender, EventArgs e)
        {
            ucUserTextControl u = new ucUserTextControl();
            FS.HISFC.Models.Base.UserText uT = new FS.HISFC.Models.Base.UserText();
            uT.Text = this.userText.ID;
            if (string.IsNullOrEmpty(uT.Text.ToString()))
            {
                uT.Text = "MS999";
            }
            uT.Name = this.userText.Name;
            uT.RichText = "";
            u.UserText = uT;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);
            this.ucUserText.RefreshList();
        }


        /// <summary>
        /// 拖入区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.Copy;
        }

        /// <summary>
        /// 改变描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiag_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.fpDiag_Sheet1.Rows[e.Row].Locked == true)
            {
                return;
            }

            string isLocked = this.fpDiag_Sheet1.Cells[e.Row, 1].Value.ToString();

            if (isLocked == "True")
            {
                this.fpDiag_Sheet1.Cells[e.Row, 3].Locked = false;
                this.fpDiag_Sheet1.Cells[e.Row, 2].Locked = true;
            }
            else //锁定不让修改
            {
                this.fpDiag_Sheet1.Cells[e.Row, 3].Locked = true;
                this.fpDiag_Sheet1.Cells[e.Row, 2].Locked = false;
            }
            if (e.Column == 1)
            {
                ucDiagnose1.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// 历史诊断双击复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpHistory_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowIndex = this.fpHistory.ActiveSheet.ActiveRowIndex;
            string diagnoise = this.fpHistory_Sheet1.Cells[rowIndex, 0].Text;
            if (string.IsNullOrEmpty(diagnoise))
            {
                return;
            }
            else
            {
                int row = this.fpDiag_Sheet1.RowCount;
                if (row == 0 || (!string.IsNullOrEmpty(this.fpDiag_Sheet1.Cells[row - 1, 2].Text)))
                {
                    this.fpDiag_Sheet1.Rows.Add(row, 1);
                }
                row = this.fpDiag_Sheet1.RowCount;
                this.fpDiag_Sheet1.Cells[row - 1, 0].Text = this.fpHistory_Sheet1.Cells[rowIndex, 0].Text;
                if (this.fpHistory_Sheet1.Cells[rowIndex, 1].Text == "是")
                {
                    this.fpDiag_Sheet1.Cells[row - 1, 1].Value = true;
                }
                else
                {
                    this.fpDiag_Sheet1.Cells[row - 1, 1].Value = false;
                }

                this.fpDiag_Sheet1.Cells[row - 1, 2].Text = this.fpHistory_Sheet1.Cells[rowIndex, 2].Text;
                this.fpDiag_Sheet1.Cells[row - 1, 3].Text = this.fpHistory_Sheet1.Cells[rowIndex, 3].Text;
                if (this.fpHistory_Sheet1.Cells[rowIndex, 4].Text == "是")
                {
                    this.fpDiag_Sheet1.Cells[row - 1, 4].Value = true;
                }
                else
                {
                    this.fpDiag_Sheet1.Cells[row - 1, 4].Value = false;
                }
                if (this.fpHistory_Sheet1.Cells[rowIndex, 5].Text == "是")
                {
                    this.fpDiag_Sheet1.Cells[row - 1, 5].Value = false;
                }
                else
                {
                    this.fpDiag_Sheet1.Cells[row - 1, 5].Value = true;
                }
                this.fpDiag_Sheet1.Cells[row - 1, 6].Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.fpDiag_Sheet1.Cells[row - 1, 7].Text = CacheManager.LogEmpl.ID;
                this.fpDiag_Sheet1.Cells[row - 1, 8].Text = CacheManager.LogEmpl.Name;
            }
        }

        /// <summary>
        /// 是否只显示当前科室的历史诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCurrentDeptCase_CheckedChanged(object sender, EventArgs e)
        {
            if (this.regInfo == null)
            {
                return;
            }
            if (this.chkCurrentDeptCase.Checked)
            {
                isShowCurrentDeptCase = true;
            }
            else
            {
                isShowCurrentDeptCase = false;
            }
            this.HistoryCase();
        }

        #region 保存列宽

        private void fpDiag_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpDiag.Sheets[0], this.xmlOutCaseInput);
        }

        private void fpHistory_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpHistory.Sheets[0], this.xmlOutCaseHistory);
        }
        #endregion

    }
}
