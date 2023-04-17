using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FS.HISFC.Components.Common.Controls;
using FS.HISFC.Models.HealthRecord.EnumServer;
using FarPoint.Win.Spread;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{

    public partial class ucOutPatientCase : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Terminal.IOutpatientCase
    {

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// 离开窗体时提示是否需要保存
        /// </summary>
        private bool needSave = false;

        /// <summary>
        /// 外部调用是否需要保存//{4270B15D-35E1-4f95-874E-D552E65BBD26}
        /// </summary>
        public bool OutneedSave = true;
        /// <summary>
        /// 最下面诊断列表容器的高度
        /// </summary>
        private int Panel9Height = 272;

        /// <summary>
        /// 诊断fp底部与panle底部的距离
        /// </summary>
        private int FpGap = 100;

        /// <summary>
        /// 预产期
        /// </summary>
        public string expectedtime = "";

        /// <summary>
        /// 诊断类别
        /// </summary>
        private ArrayList diagnoseType = new ArrayList();

        /// <summary>
        /// 诊断列表
        /// </summary>
        public ArrayList alDiag = new ArrayList();

        /// <summary>
        /// 科室常用诊断
        /// </summary>
        ArrayList alDeptDiag = new ArrayList();

        /// <summary>
        /// 全部诊断
        /// </summary>
        ArrayList alAllDiag = new ArrayList();

        /// <summary>
        /// 传染病报告，需要维护提示的诊断
        /// </summary>
        private ArrayList alDir = new ArrayList();

        /// <summary>
        /// 提示多次同步   {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        List<string> CheckSym = new List<string>();

        /// <summary>
        /// 常用文本
        /// </summary>
        private FS.FrameWork.Models.NeuObject userText = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 文本控件
        /// </summary>
        private ucUserText ucUserText = new ucUserText();

        /// <summary>
        /// Update操作前的时间
        /// </summary>
        private string oldOperTime = string.Empty;

        /// <summary>
        /// 需要填写教育评估
        /// </summary>
        private ArrayList writedep = new ArrayList();

        /// <summary>
        /// 操作前的时间
        /// </summary>
        private string OldOperTime
        {
            get
            {
                return oldOperTime;
            }
            set
            {
                oldOperTime = value;
            }
        }


        /// <summary>
        ///Update、Insert操作后时间(用来修改的时候进行判断)
        /// </summary>
        private string newOperTime = string.Empty;

        /// <summary>
        /// 操作后的时间
        /// </summary>
        private string NewOperTime
        {
            get
            {
                return newOperTime;
            }
            set
            {
                newOperTime = value;
            }
        }

        /// <summary>
        /// 用来判断是否需要上传的传染病报卡
        /// </summary>
        private FS.HISFC.BizLogic.HealthRecord.ICDMedicare icdMedicare = new FS.HISFC.BizLogic.HealthRecord.ICDMedicare();

        /// <summary>
        /// 用来判断是修改还是新增加病历
        /// </summary>
        private bool isNew = true;


        /// <summary>
        /// 保存时是否需要验证病历内容是否为空
        /// </summary>
        private bool isChecked = false;

        /// <summary>
        /// 保存时是否需要验证病历内容是否为空,true:需要;false:不需
        /// </summary>
        [Category("控件设置"), Description("保存时是否需要验证病历内容是否为空,true:需要;false:不需")]
        public bool IsChecked
        {
            set { this.isChecked = value; }
            get { return this.isChecked; }
        }

        private int rememberCaseDiagnoseDays = 1000;

        [Category("参数设置"), Description("显示历史诊断的默认天数"), DefaultValue(1000)]
        public int RememberCaseDiagnoseDays
        {
            get { return this.rememberCaseDiagnoseDays; }
            set { this.rememberCaseDiagnoseDays = value; }
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

        //{CE1A8814-003C-47dd-AAE1-F870CBCD6BB9}
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
                this.ucModifyOutPatientHealthInfo1.RememberHelthHistoryDays = value;
            }
        }

        #region 委托事件

        /// <summary>
        /// 诊断列表
        /// </summary>
        public ArrayList listDiagnose = new ArrayList();

        public delegate void SaveClickDelegate(object sender, ArrayList alDiag);

        /// <summary>
        /// 单击"保存"按钮时,这个没办法,因为门诊医生站,先录诊断再开医嘱.
        /// 这个在用的时候注册就行
        /// </summary>
        public event SaveClickDelegate SaveClicked;

        public delegate void TransportAlDiag(ArrayList arrayDiagnoses);

        /// <summary>
        /// 传递诊断
        /// </summary>
        public event TransportAlDiag transportAlDiag;

        #endregion

        #region 管理类

        /// <summary>
        /// 诊断类别
        /// </summary>
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
        /// 模板实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory tempCaseModule = null;

        /// <summary>
        /// 历史病历实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;

        /// <summary>
        /// 数据库管理类
        /// </summary>
        private FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// 门诊诊断证明书和病假条信息管理
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.DiagExtend diagExtMgr = new FS.HISFC.BizLogic.Order.OutPatient.DiagExtend();
        #endregion


        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        private string patientId = "";

        /// <summary>
        /// 病历和模板树右键
        /// </summary>
        private System.Windows.Forms.ContextMenu caseMenu = new ContextMenu();


        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        private string PatientId
        {
            get { return patientId; }

            set
            {
                this.patientId = value;
                if (!string.IsNullOrEmpty(value) && this.regObj != null)
                {
                    FS.HISFC.Models.Registration.Register regTemp = CacheManager.RegMgr.GetByClinic(value);

                    if (regTemp != null && !string.IsNullOrEmpty(regTemp.ID))
                    {
                        this.regObj = regTemp;
                    }

                    ShowPatientInfo();
                }
            }
        }

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
        /// 当前患者信息
        /// </summary>
        FS.HISFC.Models.Registration.Register regObj = null;

        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 当前登录科室
        /// </summary>
        FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();

        /// <summary>
        /// 当前登录人
        /// </summary>
        FS.HISFC.Models.Base.Employee currEmp = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// 患者挂号时实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegObj
        {
            get
            {
                return regObj;
            }
            set
            {
                regObj = value;
                this.PatientId = regObj.ID;
            }
        }

        private bool isShow = false;

        public ucOutPatientCase()
        {
            InitializeComponent();



            // 同步按钮先让顺德不显示   {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);


            currEmp = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

            //if (currDept.HospitalName.Contains("顺德"))
            //{
            //    this.btsynchro.Visible = false;
            //}





            //清屏
            this.Clear();

            #region 翻译

            //隐藏
            this.chkDeptDiag.Visible = false;

            //翻译
            this.tabPage1.Text = Language.Msg(this.tabPage1.Text);  //病历模板
            this.tabPage2.Text = Language.Msg(this.tabPage2.Text);   //历史病历
            this.btnSaveTemplate.Text = Language.Msg(this.btnSaveTemplate.Text);   //存模板
            this.chkIsPerson.Text = Language.Msg(this.chkIsPerson.Text);   //科室模板
            this.btnHistoryDiagnose.Text = Language.Msg(this.btnHistoryDiagnose.Text);   //查看历史诊断
            this.btDeleteDiagnose.Text = Language.Msg(this.btDeleteDiagnose.Text);    //删除诊断
            this.btInvalidDiagnose.Text = Language.Msg(this.btInvalidDiagnose.Text);  //作废诊断
            this.btSave.Text = Language.Msg(this.btSave.Text);    //保存病历
            this.btAddDiagnose.Text = Language.Msg(this.btAddDiagnose.Text);      //增加诊断
            this.btnReturn.Text = "<-" + Language.Msg("返回病历界面");     //返回病历界面
            this.label13.Text = Language.Msg(this.label13.Text);     //双击将该诊断加入到本次就诊诊断中
            this.btPrint.Text = Language.Msg(this.btPrint.Text);     //打印病历
            this.btDiagnosisPrint.Text = Language.Msg(this.btDiagnosisPrint.Text);     //打印病历

            Classes.Function.TranslateUI(this.pnlMain.Controls);

            #region 诊断录入FP

            for (int i = 0; i < this.fpDiagnose_Sheet1.Columns.Count; i++)
            {
                this.fpDiagnose_Sheet1.Columns.Get(i).Label = Language.Msg(this.fpDiagnose_Sheet1.Columns.Get(i).Label);
            }

            #endregion

            #region 历史诊断FP

            for (int k = 0; k < this.fpHistory_Sheet1.Columns.Count; k++)
            {
                this.fpHistory_Sheet1.Columns.Get(k).Label = Language.Msg(this.fpHistory_Sheet1.Columns.Get(k).Label);
            }

            #endregion


            #region 教育评估
            SetEstimate();
            #endregion

            #endregion

            InitEvents();
        }


        /// <summary>
        /// 设置评估记录  {b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
        /// </summary>
        private void SetEstimate()
        {

            ArrayList educationallist = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.EMR_EDUCATIONAL); //教育对象
            ArrayList medicationlist = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.MEDICATIONKNOWLEDGE); //用药知识教育
            ArrayList dietlist = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DIETKNOWLEDGE);  //饮食相关教育
            ArrayList diseaselist = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DISEASEKNOWLEDGE); // 疾病预防知识
            ArrayList educationlist = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.EDUCATIONALEFFECT);  //教育效果

            writedep = CacheManager.GetConList("HEALTHASSESSMENT");  //是否需要填写


            if ((writedep.ToArray().FirstOrDefault() as FS.FrameWork.Models.NeuObject).Name.Contains(this.currDept.ID))
            {
                this.panel12.Height = this.lbmemo2.Height + this.tbTreatment.Height + this.panel16.Height + this.label9.Height;
            }
            else
            {
                this.panel16.Height = 0;
                this.panel12.Height = this.lbmemo2.Height + this.tbTreatment.Height + this.label9.Height;
            }

            this.comeducational.AddItems(educationallist);
            this.commedication.AddItems(medicationlist);
            this.comdiet.AddItems(dietlist);
            this.comdisease.AddItems(diseaselist);
            this.comeducation.AddItems(educationlist);
        }

        #region 事件处理

        void InitEvents()
        {
            this.Load += new EventHandler(ucOutPatientCase_Load);
            this.Leave += new EventHandler(ucOutPatientCase_Leave);

            this.chkDeptDiag.Click += new System.EventHandler(this.chkDeptDiag_Click);

            this.tbChiefComplaint.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbPresentIllness.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbPastHistory.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbAllergicHistory.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbPhysicalExam.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbMemo.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbTreatment.TextChanged += new EventHandler(TextBox_TextChanged);
            this.tbMemo9.TextChanged += new EventHandler(TextBox_TextChanged);

            this.tbChiefComplaint.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbPresentIllness.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbPastHistory.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbAllergicHistory.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbPhysicalExam.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbMemo.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbTreatment.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);
            this.tbMemo9.MouseWheel += new MouseEventHandler(TextBox_MouseWheel);

            this.btAddDiagnose.Click += new EventHandler(btAddDiagnose_Click);
            this.btDeleteDiagnose.Click += new EventHandler(btDeleteDiagnose_Click);
            this.btInvalidDiagnose.Click += new EventHandler(btInvalidDiagnose_Click);
            this.btSave.Click += new EventHandler(btSave_Click);

            this.fpDiagnose.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpDiagnose_CellClick);
            this.fpDiagnose.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpDiagnose_ButtonClicked);
            this.fpDiagnose.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpDiagnose_EditChange);
            this.fpDiagnose.EditModeOn += new EventHandler(fpDiagnose_EditModeOn);
            this.fpDiagnose.DragDrop += new DragEventHandler(fpDiagnose_DragDrop);
            this.fpDiagnose.DragEnter += new DragEventHandler(fpDiagnose_DragEnter);
            this.fpDiagnose.MouseUp += new MouseEventHandler(fpDiagnose_MouseUp);

            this.fpHistory.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpHistory_CellDoubleClick);
            this.btnReturn.Click += new EventHandler(btnReturn_Click);

            this.tvCaseTemplate.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvCaseTemplate_DragDrop);
            this.tvCaseTemplate.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvCaseTemplate_DragEnter);
            this.tvCaseTemplate.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvCaseTemplate_ItemDrag);
            this.tvCaseTemplate.DragOver += new System.Windows.Forms.DragEventHandler(this.tvCaseTemplate_DragOver);
            this.tvCaseTemplate.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(tvCaseTemplate_NodeMouseDoubleClick);

            this.btnSaveTemplate.Click += new EventHandler(btnSaveTemplate_Click);

            this.tvHistoryCase.AfterSelect += new TreeViewEventHandler(tvHistoryCase_AfterSelect);
            this.ucUserText1.OnDoubleClick += new EventHandler(ucUserText1_OnDoubleClick);
            this.btnHistoryDiagnose.Click += new EventHandler(btnHistoryDiagnose_Click);


            this.btsynchro.Click += new EventHandler(btsynchro_Click);
        }

        void btnReturn_Click(object sender, EventArgs e)
        {
            this.pnlTitle.Visible = true;
            this.Panel2.Visible = true;
            this.panel3.Visible = true;
            this.panel4.Visible = true;
            this.panel4.Visible = true;
            this.panel5.Visible = true;
            this.panel6.Visible = true;
            this.panel7.Visible = true;
            this.panel8.Visible = true;
            this.panel9.Visible = true;
            this.panel11.Visible = false;
        }

        void ucOutPatientCase_Load(object sender, EventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载诊断列表,请稍候......");
                //初始化诊断列表
                InitDiagList();
                //初始化诊断录入
                InitList();
                //初始化病历模板的treeview
                InitCaseTemplateTree();
                //初始化历史病历的treeview
                InitTreeCase();
                //历史诊断
                HistoryCase();

                this.fpDiagnose.ShowListWhenOfFocus = true;
                this.ucUserText1.bSetToolTip = false;

                #region

                InputMap im;
                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpDiagnose.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                #endregion

                if (this.alDeptDiag != null && this.alDeptDiag.Count > 0)
                {
                    this.ucDiagnose1.AlDiag = this.alDeptDiag;
                }
                else if (this.alAllDiag != null)
                {
                    this.ucDiagnose1.AlDiag = this.alAllDiag;
                }

                this.ucDiagnose1.InitICDCategory(this.alAllDiag);

                this.chkDeptDiag_Click(null, null);
                this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
                this.ucDiagnose1.Visible = false;
                this.alDir = CacheManager.GetConList("WARNICD10");

                this.Panel9Height = this.panel9.Height;
                this.FpGap = this.pnlMain.Bottom - this.panel9.Bottom;
                if (this.FpGap > 7)
                {
                    this.panel9.Height += this.FpGap - 7;
                    this.fpDiagnose.Height += this.FpGap - 7;
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载诊断列表出错！\r\n" + ex.Message + "\r\n" + ex.StackTrace, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void ucOutPatientCase_Leave(object sender, EventArgs e)
        {
            if (needSave && this.regObj != null && OutneedSave) //{4270B15D-35E1-4f95-874E-D552E65BBD26}
            {
                DialogResult dr = MessageBox.Show(Classes.Function.GetMsg("是否保存当前病历") + "?", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    this.Save();
                }
                this.needSave = false;
            }
        }

        //textBox内容发生改变时，重新设定textBox的高度
        void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.needSave = true;
            TextBox tb = sender as TextBox;
            //int tmp1 = tb.Lines.Count<string>();

            int tmp = LineFeed(tb.Text);// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
            tb.Parent.Height = (tmp > 2 ? tmp : 2) * (tb.Font.Height - 1);

            //{BC3602BF-4098-4725-9846-A5F8BFFAAF2D}

            if (tb.Name == "tbTreatment")
            {
                if ((writedep.ToArray().FirstOrDefault() as FS.FrameWork.Models.NeuObject).Name.Contains(this.currDept.ID))
                {
                    this.panel12.Height = tb.Parent.Height + this.lbmemo2.Height + this.label9.Height + this.panel16.Height;
                }
                else
                {
                    this.panel12.Height = tb.Parent.Height + this.lbmemo2.Height+ this.label9.Height;
                }
            }
         
        }
        /// <summary>
        /// 计算行数// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int LineFeed(string str)
        {
            string[] stringN = str.Split('\n');

            int addRow = 0;
            foreach (string ss in stringN)
            {
                addRow++;
            }
            if (addRow > 1)
            {
                addRow = addRow - 1;
            }
            if (str == null || str.Length == 0) { return 1; }

            #region 计算长度
            int clen = System.Text.Encoding.Default.GetBytes(str).Length;
            #endregion
            int rowNum = 124;
            int colNum = (clen - (clen / rowNum) * rowNum) == 0 ? (clen / rowNum) : (clen / rowNum) + 1 + addRow;
            return colNum;

        }
        //鼠标在textbox上进行滚轮滑动时将消息传递给主panel
        void TextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            SendMessage(this.pnlMain.Handle, 0x020A, e.Delta << 16, 0);
        }

        /// 改变描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDiagnose_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {


            if (this.fpDiagnose_Sheet1.Rows[e.Row].Locked == true)
            {
                return;
            }

            string isLocked = this.fpDiagnose_Sheet1.Cells[e.Row, 1].Value.ToString();
            string isDubdiag = this.fpDiagnose_Sheet1.Cells[e.Row, 4].Value.ToString();

            this.fpDiagnose_Sheet1.Cells[e.Row, 3].Locked = isLocked == "True" ? false : true;
            this.fpDiagnose_Sheet1.Cells[e.Row, 2].Locked = isLocked == "True" ? true : false;

            string diag = this.fpDiagnose_Sheet1.Cells[e.Row, 3].Value.ToString();
            //this.fpDiagnose_Sheet1.Cells[e.Row, 2].Value = "";  疑似诊断后面增加 ？{3da1ce6e-b764-47cd-8490-3f77251fadb7}
            if (isDubdiag == "True")
            {
                this.fpDiagnose_Sheet1.Cells[e.Row, 3].Value = diag + "?";
            }
            else if (diag.Contains("?"))
            {
                this.fpDiagnose_Sheet1.Cells[e.Row, 3].Value = diag.Replace("?", "");
            }

            if (e.Column == 1)
            {
                CloseDiagnoseForm();
            }
        }

        void fpDiagnose_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = this.fpDiagnose_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.HealthRecord.Diagnose;

                if (diag != null && e.Column != 11)
                {
                    MessageBox.Show("该诊断已经保存,不允许修改，只能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.fpDiagnose.EditChange -= new FarPoint.Win.Spread.EditorNotifyEventHandler(fpDiagnose_EditChange);
                    this.fpDiagnose_Sheet1.Cells[e.Row, e.Column].Text = diag.DiagInfo.ICD10.ID;
                    this.fpDiagnose.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpDiagnose_EditChange);
                    return;
                }


                if (e.Column == 2)
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ShowDiagPanel();
                    }

                    SetDiagLocation();

                    string str = fpDiagnose_Sheet1.ActiveCell.Text;
                    string strFilter = str;
                    this.ucDiagnose1.Filter(strFilter, false);
                }
                else
                {
                    CloseDiagnoseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("fpDiagnose_EditChange" + ex.Message);
            }
        }

        void fpDiagnose_EditModeOn(object sender, EventArgs e)
        {
            //SetDiagLocation();
        }

        void fpDiagnose_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //当医生输入诊断的时候，如果如果界面已经出现滚动条，则自动滚动到最下面
            //防止医生看不见保存按钮
            this.pnlMain.VerticalScroll.Value = this.pnlMain.VerticalScroll.Maximum;
            if (e != null && e.Row >= 0)
            {
                //this.fpDiagnose_Sheet1.SetActiveCell(e.Row, 7);
                this.fpDiagnose_Sheet1.Cells[e.Row, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpDiagnose_Sheet1.Cells[e.Row, 11].Locked = false;
                this.fpDiagnose_Sheet1.SetActiveCell(e.Row, 11);
            }
        }

        void fpDiagnose_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.fpDiagnose_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpDiagnose_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            FarPoint.Win.Spread.Model.CellRange c = this.fpDiagnose.GetCellFromPixel(0, 0, e.X, e.Y);

            int activeRow = c.Row;

            if (activeRow < 0)
            {
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenu menu = new ContextMenu();

                MenuItem addMenu = new MenuItem(Classes.Function.GetMsg("添加到用户常用文本"));

                addMenu.Click += new EventHandler(addMenu_Click);

                this.fpDiagnose.ContextMenu = menu;

                if (this.fpDiagnose_Sheet1.Cells[activeRow, 1].Text == "TRUE")
                {
                    this.userText.ID = this.fpDiagnose_Sheet1.ActiveCell.Text;
                    this.userText.Name = this.userText.ID;
                }
                else
                {
                    this.userText.ID = this.fpDiagnose_Sheet1.Cells[activeRow, 2].Text.Trim();
                    this.userText.Name = this.fpDiagnose_Sheet1.Cells[activeRow, 3].Text.Trim();
                }

                menu.MenuItems.Add(addMenu);

                menu.Show(this.fpDiagnose, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 双击加入诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucUserText1_OnDoubleClick(object sender, EventArgs e)
        {
            if (this.fpDiagnose_Sheet1.Rows[fpDiagnose_Sheet1.ActiveRowIndex].Locked)
            {
                MessageBox.Show(Classes.Function.GetMsg("该诊断已经保存") + "," + Classes.Function.GetMsg("不允许修改") + "，" + Classes.Function.GetMsg("只可以作废") + "!", Classes.Function.GetMsg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.ucUserText1.GetSelectedNode() == null || this.ucUserText1.GetSelectedNode().Tag == null)
                return;

            //在诊断列表找不到时，默认为描述诊断
            string filter = "";

            FS.HISFC.Models.Base.UserText userText = this.ucUserText1.GetSelectedNode().Tag as FS.HISFC.Models.Base.UserText;
            if (userText != null)
            {
                filter = userText.Text;
            }
            else
            {
                filter = this.ucUserText1.GetSelectedNode().Tag.ToString();
            }


            if (filter.Length <= 0)
                return;


            this.needSave = true;

            if (!this.ucDiagnose1.hsDiags.Contains(filter))
            {
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 1].Value = true;
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Locked = true;
            }
            else
            {
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 1].Value = false;
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Locked = false;
            }

            if (System.Convert.ToBoolean(this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 1].Value) == true)
            {
                this.fpDiagnose_Sheet1.SetActiveCell(this.fpDiagnose_Sheet1.ActiveRowIndex, 3, false);
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Text = "MS999";
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 3].Text = this.ucUserText1.GetSelectedNode().Text;
            }
            else
            {
                string data = filter;
                this.ucDiagnose1.isDrag = true;
                this.fpDiagnose_Sheet1.SetActiveCell(this.fpDiagnose_Sheet1.ActiveRowIndex, 2, false);
                this.fpDiagnose_Sheet1.SetValue(this.fpDiagnose_Sheet1.ActiveRowIndex, 2, data);
                this.ucDiagnose1.isDrag = false;
                this.ucDiagnose1.Filter(data, true);
                this.GetInfo();
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

        void fpDiagnose_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.Copy;
        }

        void fpDiagnose_DragDrop(object sender, DragEventArgs e)
        {
            if (this.fpDiagnose_Sheet1.Rows[fpDiagnose_Sheet1.ActiveRowIndex].Locked)
            {
                MessageBox.Show("该诊断已经保存,不允许修改，只能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //在诊断列表找不到时，默认为描述诊断
            string filter = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();

            if (!this.ucDiagnose1.hsDiags.Contains(filter))
            {
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 1].Value = true;
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Locked = true;
            }

            if (System.Convert.ToBoolean(this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 1].Value) == true)
            {
                this.fpDiagnose_Sheet1.SetActiveCell(this.fpDiagnose_Sheet1.ActiveRowIndex, 3, false);
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Text = "MS999";
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 3].Text = this.ucUserText1.GetSelectedNode().Text;
            }
            else
            {
                string data = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
                this.ucDiagnose1.isDrag = true;
                this.fpDiagnose_Sheet1.SetActiveCell(this.fpDiagnose_Sheet1.ActiveRowIndex, 2, false);
                this.fpDiagnose_Sheet1.SetValue(this.fpDiagnose_Sheet1.ActiveRowIndex, 2, data);
                this.ucDiagnose1.isDrag = false;
                this.ucDiagnose1.Filter(data, true);
                this.GetInfo();
            }
            e.Data.SetData("");
        }

        /// <summary>
        /// 历史诊断双击复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpHistory_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowIndex = this.fpHistory.ActiveSheet.ActiveRowIndex;
            string diagnoise = this.fpHistory_Sheet1.Cells[rowIndex, 0].Text;

            if (!string.IsNullOrEmpty(diagnoise))
            {
                //{138546BF-3D41-497b-BA7F-9DAF68D2D888}
                string icdcode = this.fpHistory_Sheet1.Cells[rowIndex, 2].Text;
                if (icdcode == "MS999")
                {
                    MessageBox.Show("请选择ICD10诊断，非标准诊断请核对后选择", Classes.Function.GetMsg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                int row = this.fpDiagnose_Sheet1.RowCount;
                if (row == 0 || (!string.IsNullOrEmpty(this.fpDiagnose_Sheet1.Cells[row - 1, 2].Text)))
                {
                    this.fpDiagnose_Sheet1.Rows.Add(row, 1);
                }
                row = this.fpDiagnose_Sheet1.RowCount;
                this.fpDiagnose_Sheet1.Cells[row - 1, 0].Text = this.fpHistory_Sheet1.Cells[rowIndex, 0].Text;
                if (this.fpHistory_Sheet1.Cells[rowIndex, 1].Text == Language.Msg("是"))
                {
                    this.fpDiagnose_Sheet1.Cells[row - 1, 1].Value = true;
                }
                else
                {
                    this.fpDiagnose_Sheet1.Cells[row - 1, 1].Value = false;
                }

                this.fpDiagnose_Sheet1.Cells[row - 1, 2].Text = this.fpHistory_Sheet1.Cells[rowIndex, 2].Text;
                this.fpDiagnose_Sheet1.Cells[row - 1, 3].Text = this.fpHistory_Sheet1.Cells[rowIndex, 3].Text;
                if (this.fpHistory_Sheet1.Cells[rowIndex, 4].Text == Language.Msg("是"))
                {
                    this.fpDiagnose_Sheet1.Cells[row - 1, 4].Value = true;
                }
                else
                {
                    this.fpDiagnose_Sheet1.Cells[row - 1, 4].Value = false;
                }
                if (this.fpHistory_Sheet1.Cells[rowIndex, 5].Text == Language.Msg("是"))
                {
                    this.fpDiagnose_Sheet1.Cells[row - 1, 5].Value = false;
                }
                else
                {
                    this.fpDiagnose_Sheet1.Cells[row - 1, 5].Value = true;
                }
                this.fpDiagnose_Sheet1.Cells[row - 1, 6].Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.fpDiagnose_Sheet1.Cells[row - 1, 7].Text = currEmp.ID;// CacheManager.LogEmpl.ID;
                this.fpDiagnose_Sheet1.Cells[row - 1, 8].Text = CacheManager.LogEmpl.Name;
            }

            this.pnlTitle.Visible = true;
            this.Panel2.Visible = true;
            this.panel3.Visible = true;
            this.panel4.Visible = true;
            this.panel4.Visible = true;
            this.panel5.Visible = true;
            this.panel6.Visible = true;
            this.panel7.Visible = true;
            this.panel8.Visible = true;
            this.panel9.Visible = true;
            this.panel11.Visible = false;

        }

        /// <summary>
        /// 设置诊断输入框位置
        /// </summary>
        private void ShowDiagPanel()
        {
            this.ucDiagnose1.Show();
            this.ucDiagnose1.BringToFront();  // {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
            OpenDiagnoseForm();
        }

        /// <summary>
        /// 设置诊断录入框位置
        /// </summary>
        private void SetDiagLocation()
        {
            Control cell = this.fpDiagnose.EditingControl;
            if (cell == null) return;

            if (this.ucDiagnose1.Height + cell.Bottom > fpDiagnose.Height)
            {
                this.ucDiagnose1.Location = new Point(fpDiagnose.Left + cell.Right, fpDiagnose.Bottom - this.ucDiagnose1.Height);
            }
            else
            {
                this.ucDiagnose1.Location = new Point(fpDiagnose.Left + cell.Right, cell.Bottom);
            }

        }



        void btnHistoryDiagnose_Click(object sender, EventArgs e)
        {
            this.pnlTitle.Visible = false;
            this.Panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            this.panel8.Visible = false;
            this.panel9.Visible = false;
            this.panel11.Visible = true;
            //this.panel16.Visible = false;
            //this.panel16.Height = 0;

        }

        /// <summary>
        /// 增加打印功能{9B722705-CFCC-4195-9436-AA834FDC813A}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPrint_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (currDept.HospitalID == "BELLAIRE")// {E24C3405-0CF7-429b-964E-A8A0230A2110}
            {
                //{A54386E1-DEDB-4fe2-A6D0-292BEBF1FBEE}
                FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase), typeof(FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint), 1) as FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint;
                if (o == null)
                {
                    return;
                }
                else
                {
                    o.RegId = this.regObj.ID;
                    bool isPrint = o.IsPrint;
                    if (isPrint)
                    {
                        o.PrintView();
                    }
                }
            }
            else
            {
                //{A54386E1-DEDB-4fe2-A6D0-292BEBF1FBEE}
                FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase), typeof(FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint), 2) as FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint;
                if (o == null)
                {
                    return;
                }
                else
                {
                    o.RegId = this.regObj.ID;
                    bool isPrint = o.IsPrint;
                    if (isPrint)
                    {
                        o.Print();
                    }
                }

            }

        }

        #endregion

        #region 接口实现
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int InitUC()
        {
            return 1;
        }

        public bool IsBrowse
        {
            set
            {
                this.neuPanel1.Visible = !value;
            }
        }

        /// <summary>
        /// 患者实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            set
            {
                this.regObj = value;
            }
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        public void Show()
        {
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
        }

        /// <summary>
        /// 选择患者赋值
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Clear();

            if (neuObject != null && neuObject.GetType() == typeof(FS.HISFC.Models.Registration.Register))
            {
                this.regObj = neuObject as FS.HISFC.Models.Registration.Register;
                this.PatientId = this.regObj.ID;

                //{CE1A8814-003C-47dd-AAE1-F870CBCD6BB9}
                ucModifyOutPatientHealthInfo1.RegInfo = this.regObj;
                this.ucModifyOutPatientHealthInfo1.RememberHelthHistoryDays = this.rememberHelthHistoryDays;
                ucModifyOutPatientHealthInfo1.GetHealthInfo(ref this.regObj);
            }

            this.needSave = false;

            return 1;
        }

        #endregion


        #region 病历相关
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitCaseTemplateTree()
        {
            caseMenu.MenuItems.Clear();
            MenuItem EditMenu = new MenuItem(Classes.Function.GetMsg("修改名称"));
            MenuItem DelMenu = new MenuItem(Classes.Function.GetMsg("删除模板"));
            EditMenu.Click += new EventHandler(EditMenu_Click);
            DelMenu.Click += new EventHandler(DelMenu_Click);
            caseMenu.MenuItems.Add(EditMenu);
            caseMenu.MenuItems.Add(DelMenu);


            ArrayList al = null;//所有模板

            if (this.tvCaseTemplate.Nodes.Count > 0)
            {
                this.tvCaseTemplate.Nodes.Clear();
            }

            //添加科室模板
            TreeNode deptModule = new TreeNode(Classes.Function.GetMsg("科室模板"));
            deptModule.Tag = "DeptModule";
            deptModule.ImageIndex = 1;
            deptModule.SelectedImageIndex = 1;

            this.tvCaseTemplate.Nodes.Add(deptModule);
            try
            {
                al = CacheManager.OutOrderMgr.QueryAllCaseModule("1", (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Dept.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory module = al[i] as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
                    if (module == null)
                        continue;
                    TreeNode node = new TreeNode(module.Name);
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                    node.Tag = module;
                    node.ContextMenu = this.caseMenu;
                    deptModule.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                this.ShowErr(Classes.Function.GetMsg("获得科室模板出错") + "!" + ex.Message);
                return;
            }

            //个人模板
            TreeNode perModule = new TreeNode(Classes.Function.GetMsg("个人模板"));
            perModule.Tag = "PerModule";
            perModule.ImageIndex = 2;
            perModule.SelectedImageIndex = 2;

            this.tvCaseTemplate.Nodes.Add(perModule);
            try
            {
                al = CacheManager.OutOrderMgr.QueryAllCaseModule("2", CacheManager.OutOrderMgr.Operator.ID);
                for (int i = 0; i < al.Count; i++)
                {

                    FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory module = al[i] as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
                    if (module == null)
                        continue;

                    TreeNode node = new TreeNode(module.Name);
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                    node.Tag = module;
                    node.ContextMenu = this.caseMenu;
                    perModule.Nodes.Add(node);
                }
            }
            catch
            {
                this.ShowErr(Classes.Function.GetMsg("获得个人模板出错") + "!");
                return;
            }

            //展开
            this.tvCaseTemplate.ExpandAll();
        }

        void EditMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = this.tvCaseTemplate.SelectedNode;
            if (node.Tag == null)
            {
                return;
            }
            this.tempCaseModule = node.Tag as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;

            if (this.tempCaseModule == null)
            {
                return;
            }

            HISFC.Components.Order.OutPatient.Forms.frmPopShow frm = new HISFC.Components.Order.OutPatient.Forms.frmPopShow();
            frm.ShowDialog();
            //{CAEC0986-5DE9-4fed-9112-65C7E3B812AE}
            if (frm.IsCancel)
            {
                return;
            }
            string name = frm.ModuleName;
            if (name == null || name == "")
            {
                this.ShowErr(Classes.Function.GetMsg("病历模板名称不能为空") + "," + Classes.Function.GetMsg("请修改") + "！");
                return;
            }
            this.tempCaseModule.Name = name;
            try
            {
                int i = -1;
                i = CacheManager.OutOrderMgr.SetCaseModule(this.tempCaseModule);
                if (i < 0)
                {
                    this.ShowErr(Classes.Function.GetMsg("修改名称失败") + "！" + CacheManager.OutOrderMgr.Err);
                    return;
                }
                else
                {
                    this.ShowErr(Classes.Function.GetMsg("修改名称成功") + "！");
                    this.InitCaseTemplateTree();
                }
            }
            catch
            {
                this.ShowErr(Classes.Function.GetMsg("修改名称失败") + "！" + CacheManager.OutOrderMgr.Err);
                return;
            }
        }

        void DelMenu_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Drsult = MessageBox.Show(Classes.Function.GetMsg("确认删除该模板") + "？", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Drsult == DialogResult.Yes)
                {
                    FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory obj = this.tvCaseTemplate.SelectedNode.Tag as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
                    if (obj == null || string.IsNullOrEmpty(obj.ID))
                    {
                        return;
                    }

                    int result = CacheManager.OutOrderMgr.DeleteCaseModule(obj.ID);
                    if (result < 0)
                    {
                        this.ShowErr(Classes.Function.GetMsg("删除失败") + "！");
                    }
                    else
                    {
                        MessageBox.Show(Classes.Function.GetMsg("删除成功") + "！", Classes.Function.GetMsg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.tvCaseTemplate.Nodes.Remove(this.tvCaseTemplate.SelectedNode);
                    }
                }

            }
            catch (Exception ex)
            {

                this.ShowErr(Classes.Function.GetMsg("删除失败") + "！" + Classes.Function.GetMsg("错误") + "：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化历史病历
        /// </summary>
        public void InitTreeCase()
        {
            ArrayList al = new ArrayList();
            try
            {
                if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
                {
                    return;
                }

                if (this.tvHistoryCase.Nodes.Count > 0)
                {
                    this.tvHistoryCase.Nodes.Clear();
                }

                TreeNode root = new TreeNode();
                root.Text = Language.Msg("历史病历");
                root.ImageIndex = 1;
                root.SelectedImageIndex = 1;
                root.Tag = null;
                this.tvHistoryCase.Nodes.Add(root);
                al = CacheManager.OutOrderMgr.QueryAllCaseHistory(this.regObj.PID.CardNO);
                if (al == null || al.Count < 0)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < al.Count; i++)
                    {

                        FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                        if (obj == null)
                        {
                            continue;
                        }

                        TreeNode node = new TreeNode();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                        node.Tag = obj;
                        if (obj.Memo != null)
                        {
                            node.Text = obj.Name + "[" + FS.FrameWork.Function.NConvert.ToDateTime(obj.User01).ToString() + "]";
                        }
                        else
                        {
                            node.Text = obj.Name + "[" + obj.ID + "]";
                        }
                        root.Nodes.Add(node);
                    }

                    //获取当前id最新的病历
                    this.SetCaseHistory(this.regObj.ID);

                    this.tvHistoryCase.ExpandAll();


                }
                if (OutneedSave == false)//{4270B15D-35E1-4f95-874E-D552E65BBD26}
                {
                    this.btSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "获得历史模板出错！");
                return;
            }
        }

        /// <summary>
        /// 弹出错误
        /// </summary>
        /// <param name="Err"></param>
        private void ShowErr(string Err)
        {
            MessageBox.Show(Err, Classes.Function.GetMsg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region Drop模板TreeView节点
        private void tvCaseTemplate_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tvCaseTemplate_DragOver(object sender, DragEventArgs e)
        {
            Point p = new Point();
            p.X = e.X;
            p.Y = e.Y;
            TreeNode node = this.tvCaseTemplate.GetNodeAt(p);
            this.tvCaseTemplate.SelectedNode = node;
            this.tvCaseTemplate.Focus();
        }

        private void tvCaseTemplate_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //如果是跟接点(科室模板或个人模板)则return
                TreeNode node = (TreeNode)e.Item;
                string tag = node.Tag.ToString().Trim();
                if (tag == " PerModule" || tag == "DeptModule")
                    return;
                //开始进行"Drag"操作
                DoDragDrop((TreeNode)e.Item, DragDropEffects.Move);

            }
        }

        private void tvCaseTemplate_DragDrop(object sender, DragEventArgs e)
        {

            string Mess = null;

            TreeNode temp = new TreeNode();
            //得到要移动的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData(temp.GetType());

            //转换坐标为控件treeview的坐标
            Point position = new Point(0, 0);
            position.X = e.X;
            position.Y = e.Y;
            position = this.tvCaseTemplate.PointToClient(position);

            //得到移动的目的地的节点
            TreeNode aimNode = this.tvCaseTemplate.GetNodeAt(position);
            if (aimNode == null)
                return;
            //得到要将移动节点加如到哪个节点下面
            TreeNode MdNode = GetaimNode(aimNode, moveNode);
            if (MdNode == null)
                return;
            string Modtype = string.Empty;//要移动的节点的模板类型
            string ModNumer = string.Empty;//要移动的节点的模板ID
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory MoveModule = moveNode.Tag as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
            ModNumer = MoveModule.ID;
            //如果是科室模板则改为个人模板
            if (MoveModule.ModuleType == "1")
            {
                Mess = "确认要将科室模板改为个人模板？";
                Modtype = "2";
            }
            else
            {
                Mess = "确认要将个人模板改为科室模板？";
                Modtype = "1";
            }

            DialogResult Result = MessageBox.Show(Mess, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Result == DialogResult.Yes)
            {
                //更新模板类型
                if (CacheManager.OutOrderMgr.UpdateCaseModuleType(Modtype, ModNumer) < 0)
                {
                    this.ShowErr("更新模板类型失败！");
                    return;
                }
                //FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory obj = moveNode.Tag as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
                MoveModule.ModuleType = Modtype;
                moveNode.Tag = MoveModule;
                this.tvCaseTemplate.Nodes.Remove(moveNode);
                MdNode.Nodes.Add(moveNode);
            }
        }

        /// <summary>
        /// 得到目的节点
        /// </summary>
        /// <param name="aimNode">目的节点</param>
        /// <param name="moveNode">要移动的节点</param>
        /// <returns></returns>
        private TreeNode GetaimNode(TreeNode aimNode, TreeNode moveNode)
        {
            try
            {
                //存放移动前节点是个人模板还是科室模板标识
                string tag = moveNode.Parent.Tag.ToString();
                //存放目的节点是个人模板还是科室模板
                string aimtag = string.Empty;
                TreeNode tempNode = null;
                //如果子节点为0则判断是子节点还是跟节点
                if (aimNode.Nodes.Count == 0)
                {
                    if (aimNode.Tag.ToString().Trim() == "DeptModule" || aimNode.Tag.ToString().Trim() == "PerModule")
                    {
                        tempNode = aimNode;
                        aimtag = aimNode.Tag.ToString().Trim();
                    }
                    else
                    {
                        tempNode = aimNode.Parent;
                        aimtag = aimNode.Parent.Tag.ToString().Trim();
                    }
                }
                else
                {
                    tempNode = aimNode;
                    aimtag = aimNode.Tag.ToString().Trim();
                }
                if (aimtag == tag)
                    return null;
                return tempNode;
            }
            catch
            {
                return null;
            }

        }

        private void tvCaseTemplate_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.tvCaseTemplate.SelectedNode == null)
            {
                return;
            }

            if (this.tvCaseTemplate.SelectedNode.Tag.ToString() == "DeptModule" || this.tvCaseTemplate.SelectedNode.Tag.ToString() == "PerModule")
                return;

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory module = this.tvCaseTemplate.SelectedNode.Tag
                        as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
            if (module == null)
            {
                return;
            }

            this.SetCaseHistory(module);
        }
        #endregion

        #region

        private void tvHistoryCase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvHistoryCase.SelectedNode != null && this.tvHistoryCase.SelectedNode.Tag != null)
            {
                FS.FrameWork.Models.NeuObject obj = this.tvHistoryCase.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
                if (obj == null)
                {
                    return;
                }

                this.SetCaseHistory(obj.ID, obj.User01);

                //{CA9AEA4A-B78E-4145-86FF-98E918487029} 历史病历模板修改
                //if (!this.regObj.ID.Equals(obj.ID))
                //{
                //    // 非当前挂号病历信息提示是否提取,否则将修改当前病历信息
                //    // //DialogResult dr = MessageBox.Show(Classes.Function.GetMsg("是否提取病历信息") + "？", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo);
                //    //if (dr == DialogResult.Yes)
                //    // {
                //    //{4FCDEE77-DBEA-4de2-8FDC-09586A791429}
                //    this.isNew = true;
                //    //}
                //}
                //else
                //{
                //    this.isNew = false;
                //}
        #endregion
            }
        }

        private void SetCaseHistory(string regId, string operTime)
        {
            //{CA9AEA4A-B78E-4145-86FF-98E918487029} 历史病历模板修改
            //this.caseHistory 
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistorytmp = CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(regId, operTime);//.SelectCaseHistory(regId);
            if (caseHistorytmp != null)
            {
                this.tbChiefComplaint.Text = caseHistorytmp.CaseMain;//主诉
                this.tbPresentIllness.Text = caseHistorytmp.CaseNow;//现病史
                this.tbPastHistory.Text = caseHistorytmp.CaseOld;//既往史
                this.tbPhysicalExam.Text = caseHistorytmp.CheckBody;//查体
                this.tbMemo.Text = caseHistorytmp.Memo;//备注
                this.tbAllergicHistory.Text = caseHistorytmp.CaseAllery;//过敏史
                this.tbTreatment.Text = caseHistorytmp.User01;//处理
                this.tbMemo9.Text = caseHistorytmp.SupExamination;//辅助检查
                this.tbmemo2.Text = caseHistorytmp.Memo2;// //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
                //this.txtDiagnose.Text = caseHistory.CaseDiag;
                this.OldOperTime = caseHistorytmp.CaseOper.OperTime.ToString();//操作时间
                //this.isNew = false;
                //}
                //else
                //{
                //this.isNew = true;
                //}
            }
            this.tbChiefComplaint.Focus();
            if (this.ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Visible = false;
            }
        }



        void btnSaveTemplate_Click(object sender, EventArgs e)
        {

            try
            {
                FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory module
                        = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
                //如果不为空则代表是Update
                string ID = CacheManager.OutOrderMgr.GetModuleSeq();//编号
                module = this.GetCaseHistory();
                //判断是否存为科室模板
                module.ModuleType = this.chkIsPerson.Checked ? "1" : "2";
                //医生
                module.DoctID = CacheManager.OutOrderMgr.Operator.ID;
                module.ID = ID;
                //科室
                FS.HISFC.Models.Base.Employee p = CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee;
                module.DeptID = p.Dept.ID;

                HISFC.Components.Order.OutPatient.Forms.frmPopShow frm = new HISFC.Components.Order.OutPatient.Forms.frmPopShow();
                frm.ShowDialog();
                if (frm.IsCancel)
                {
                    return;
                }
                if (frm.ModuleName == "")
                {
                    module.Name = "新建病历模板";
                }
                else
                {
                    module.Name = frm.ModuleName;
                }

                try
                {
                    int i = -1;
                    i = CacheManager.OutOrderMgr.SetCaseModule(module);
                    if (i < 0)
                    {
                        this.ShowErr(Classes.Function.GetMsg("保存为病历模板时出现错误") + "！");
                        return;
                    }
                    else
                    {
                        this.ShowErr(Classes.Function.GetMsg("保存病历模板成功") + "！");
                    }
                }
                catch (Exception ex)
                {
                    this.ShowErr(Classes.Function.GetMsg("保存为病历模板时出现错误") + "！" + ex.Message);
                    return;
                }

                this.InitCaseTemplateTree();
            }
            catch (Exception ex)
            {
                this.ShowErr(ex.Message);
            }
        }

        // <summary>
        /// 得到病历信息
        /// </summary>
        /// <returns></returns>

        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory GetCaseHistory()
        {
            this.caseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
            this.caseHistory.CaseMain = this.tbChiefComplaint.Text.Trim();//主诉
            this.caseHistory.CaseNow = this.tbPresentIllness.Text.Trim();//现病史
            this.caseHistory.CaseOld = this.tbPastHistory.Text.Trim();//既往史
            this.caseHistory.CheckBody = this.tbPhysicalExam.Text.Trim();//查体
            this.caseHistory.User01 = this.tbTreatment.Text.Trim();//治疗方案
            this.caseHistory.Memo = this.tbMemo.Text.Trim();//嘱托
            this.caseHistory.CaseAllery = this.tbAllergicHistory.Text.Trim();
            this.caseHistory.CaseOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.NewOperTime);//操作时间
            this.caseHistory.Memo2 = this.tbmemo2.Text.Trim();// //  {4694CFAC-9041-496a-93C1-FAE7863E055E} 备注
            this.caseHistory.SupExamination = this.tbMemo9.Text.Trim();//辅助检查

            this.caseHistory.Emr_Educational = this.comeducational.Text; // 教育对象
            this.caseHistory.MedicationKnowledge = this.commedication.Text;  //用药知识
            this.caseHistory.DiteKnowledge = this.comdiet.Text; //饮食相关
            this.caseHistory.DiseaseKnowledge = this.comdisease.Text; //疾病预防
            this.caseHistory.EducationalEffect = this.comeducation.Text; //教育效果
            this.caseHistory.EducationContent = "详见宣教及注意事项";
            this.caseHistory.PatientDiagnose = "主要诊断名称";

            return this.caseHistory;
        }

        /// <summary>
        /// 根据模板设置
        /// </summary>
        /// <param name="module"></param>
        private void SetCaseHistory(FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory module)
        {
            DialogResult dr = DialogResult.Cancel;
            if (string.IsNullOrEmpty(this.tbChiefComplaint.Text) &&
                string.IsNullOrEmpty(this.tbPresentIllness.Text) &&
                string.IsNullOrEmpty(this.tbPastHistory.Text) &&
                string.IsNullOrEmpty(this.tbPhysicalExam.Text) &&
                string.IsNullOrEmpty(this.tbMemo.Text) &&
                string.IsNullOrEmpty(this.tbAllergicHistory.Text) &&
                string.IsNullOrEmpty(this.tbTreatment.Text))
            {
                this.tbChiefComplaint.Text = module.CaseMain;
                this.tbPresentIllness.Text = module.CaseNow;
                this.tbPastHistory.Text = module.CaseOld;
                this.tbPhysicalExam.Text = module.CheckBody;
                this.tbMemo.Text = module.Memo;
                this.tbAllergicHistory.Text = module.CaseAllery;
                this.tbTreatment.Text = module.User01;// {BAF5F82E-F492-49eb-B445-65EB6B355D66}
            }
            else
            {
                dr = MessageBox.Show(Classes.Function.GetMsg("是否追加模板内容") + "？", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    this.tbChiefComplaint.Text += module.CaseMain;
                    this.tbPresentIllness.Text += module.CaseNow;
                    this.tbPastHistory.Text += module.CaseOld;
                    this.tbPhysicalExam.Text += module.CheckBody;
                    this.tbMemo.Text += module.Memo;
                    this.tbAllergicHistory.Text += module.CaseAllery;
                    this.tbTreatment.Text += module.User01;// {BAF5F82E-F492-49eb-B445-65EB6B355D66}
                }
                else
                {
                    this.tbChiefComplaint.Text = module.CaseMain;
                    this.tbPresentIllness.Text = module.CaseNow;
                    this.tbPastHistory.Text = module.CaseOld;
                    this.tbPhysicalExam.Text = module.CheckBody;
                    this.tbMemo.Text = module.Memo;
                    this.tbAllergicHistory.Text = module.CaseAllery;
                    this.tbTreatment.Text = module.User01;// {BAF5F82E-F492-49eb-B445-65EB6B355D66}
                }
            }

            this.tbChiefComplaint.Focus();
            if (this.ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Visible = false;
            }
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="regId"></param>
        private void SetCaseHistory(string regId)
        {
            this.caseHistory = CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(regId);
            if (this.caseHistory != null)
            {
                this.tbChiefComplaint.Text = caseHistory.CaseMain;//主诉
                this.tbPresentIllness.Text = caseHistory.CaseNow;//现病史
                this.tbPastHistory.Text = caseHistory.CaseOld;//既往史
                this.tbPhysicalExam.Text = caseHistory.CheckBody;//查体
                this.tbMemo.Text = caseHistory.Memo;//嘱托  
                this.tbAllergicHistory.Text = caseHistory.CaseAllery;//过敏史
                this.tbTreatment.Text = caseHistory.User01;// {BAF5F82E-F492-49eb-B445-65EB6B355D66} 治疗方案

                
                this.OldOperTime = caseHistory.CaseOper.OperTime.ToString();//操作时间
                this.tbmemo2.Text = caseHistory.Memo2;// //  {4694CFAC-9041-496a-93C1-FAE7863E055E}  备注

                this.tbMemo9.Text =caseHistory.SupExamination;//辅助检查
                this.comeducational.Text = this.caseHistory.Emr_Educational; // 教育对象
                this.commedication.Text = this.caseHistory.MedicationKnowledge;  //用药知识
                this.comdiet.Text = this.caseHistory.DiteKnowledge; //饮食相关
                this.comdisease.Text = this.caseHistory.DiseaseKnowledge; //疾病预防
                this.comeducation.Text = this.caseHistory.EducationalEffect; //教育效果

                this.isNew = false;
            }
            else
            {
                this.isNew = true;
                this.tbChiefComplaint.Text = "";//主诉
                this.tbPresentIllness.Text = "";//现病史
                this.tbPastHistory.Text = "";//既往史
                this.tbPhysicalExam.Text = "";//查体
                this.tbMemo.Text = "";//嘱托
                this.tbAllergicHistory.Text = "";//过敏史
                this.tbTreatment.Text = "";//治疗方案// {BAF5F82E-F492-49eb-B445-65EB6B355D66}
                this.tbmemo2.Text = "";// 备注

                this.tbMemo9.Text = "";//辅助检查

                this.comeducational.Text = ""; // 教育对象
                this.commedication.Text = "";  //用药知识
                this.comdiet.Text = ""; //饮食相关
                this.comdisease.Text = ""; //疾病预防
                this.comeducation.Text = ""; //教育效果

            }
            this.tbChiefComplaint.Focus();
            if (this.ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Visible = false;
            }
        }
        #endregion

        #region 信息显示
        /// <summary>
        /// 显示
        /// </summary>
        private void ShowPatientInfo()
        {
            this.lblCardNO.Text = this.regObj.PID.CardNO.PadRight(10, '_');
            this.lblName.Text = this.regObj.Name.PadRight(10, '_');
            this.lblSex.Text = Language.Msg(this.regObj.Sex.Name).PadRight(10, '_');
            //this.lblAge.Text = this.getAge(this.regObj.Birthday).PadRight(10, '_');
            this.lblAge.Text = regMgr.GetAge(this.regObj.Birthday);// {A2526EC4-7FD8-4561-9B8A-8D42EE8D72AA} lfhm

            this.lblDept.Text = this.regObj.DoctorInfo.Templet.Dept.Name.PadRight(10, '_');

            try
            {
                this.alDiag = CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获得患者的诊断信息出错！" + ex.Message, "提示");
                return;
            }
            if (this.alDiag == null)
            {
                return;
            }

            //如果为空，重取，否则下面出异常
            if (this.diagnoseTypeHelper.ArrayObject.Count <= 0)
            {
                this.diagnoseTypeHelper.ArrayObject = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            }

            //清空
            if (this.fpDiagnose_Sheet1.Rows.Count > 0)
            {
                this.fpDiagnose_Sheet1.Rows.Remove(0, this.fpDiagnose_Sheet1.Rows.Count);
            }

            //填充
            if (alDiag.Count > 0)
            {
                if (this.transportAlDiag != null)
                {
                    this.transportAlDiag(listDiagnose);
                }
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in this.alDiag)
            {
                this.fpDiagnose_Sheet1.Rows.Add(0, 1);
                if (!diag.IsValid)
                {
                    this.fpDiagnose_Sheet1.Rows[0].ForeColor = Color.Red;
                }

                this.fpDiagnose_Sheet1.Cells[0, 0].Text = this.diagnoseTypeHelper.GetObjectFromID(diag.DiagInfo.DiagType.ID).Name;
                this.fpDiagnose_Sheet1.Cells[0, 1].Value = diag.DiagInfo.IsMain;//是否描述
                this.fpDiagnose_Sheet1.Cells[0, 2].Text = diag.DiagInfo.ICD10.ID;//icd码
                this.fpDiagnose_Sheet1.Cells[0, 3].Text = diag.DiagInfo.ICD10.Name;//icd名称
                if (diag.DiagInfo.ICD10.ID == "MS999")
                {
                    this.fpDiagnose_Sheet1.Cells[0, 3].Locked = false;
                    this.fpDiagnose_Sheet1.Cells[0, 2].Locked = true;
                }
                else
                {
                    this.fpDiagnose_Sheet1.Cells[0, 2].Locked = false;
                    this.fpDiagnose_Sheet1.Cells[0, 3].Locked = true;
                }
                this.fpDiagnose_Sheet1.Cells[0, 4].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.DubDiagFlag);//是否疑诊
                this.fpDiagnose_Sheet1.Cells[0, 5].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.Diagnosis_flag);//是否初诊
                this.fpDiagnose_Sheet1.Cells[0, 6].Text = diag.DiagInfo.DiagDate.Date.ToShortDateString();//日期
                this.fpDiagnose_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpDiagnose_Sheet1.Cells[0, 7].Text = diag.DiagInfo.Doctor.ID;//代码
                this.fpDiagnose_Sheet1.Cells[0, 8].Text = diag.DiagInfo.Doctor.Name;//诊断医生
                this.fpDiagnose_Sheet1.Cells[0, 11].Text = diag.Memo;//备注

                if (this.periodHelper.GetObjectFromID(diag.PeriorCode) != null)
                {
                    this.fpDiagnose_Sheet1.Cells[0, 9].Text = this.periodHelper.GetObjectFromID(diag.PeriorCode).Name;
                }
                if (this.levelHelper.GetObjectFromID(diag.LevelCode) != null)
                {
                    this.fpDiagnose_Sheet1.Cells[0, 10].Text = this.levelHelper.GetObjectFromID(diag.LevelCode).Name;
                }
                this.fpDiagnose_Sheet1.Rows[0].Tag = diag;

                this.fpDiagnose_Sheet1.Rows[0].Locked = true;
                this.fpDiagnose_Sheet1.Cells[0, 3].Locked = true;
            }

            //加载历史病历
            InitTreeCase();
            InitCaseTemplateTree();
            HistoryCase();
            if (this.fpDiagnose_Sheet1.Rows.Count == 0)// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            {
                //this.fpDiagnose_Sheet1.Rows.Count = 1;
                //this.fpDiagnose_Sheet1.Cells[0, 0].Text = "主要诊断";
                this.AddNew(this.fpDiagnose_Sheet1.Rows.Count);
            }
        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        private void InitList()
        {
            try
            {
                this.fpDiagnose.SelectNone = true;
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
                this.fpDiagnose_Sheet1.Columns[0].CellType = type;

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
                this.fpDiagnose_Sheet1.Columns[10].CellType = type1;


                //诊断分期
                FarPoint.Win.Spread.CellType.ComboBoxCellType type2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                ArrayList diagPeriod = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DIAGPERIOD);
                this.periodHelper.ArrayObject = diagPeriod;
                s = new string[diagPeriod.Count];
                for (int i = 0; i < diagPeriod.Count; i++)
                    s[i] = (diagPeriod[i] as FS.FrameWork.Models.NeuObject).Name;
                type2.Items = s;
                this.fpDiagnose_Sheet1.Columns[9].CellType = type2;

                this.fpDiagnose_Sheet1.Rows.Remove(0, this.fpDiagnose_Sheet1.Rows.Count);

                if (this.alDiag.Count <= 0)
                {
                    if (this.fpDiagnose_Sheet1.Rows.Count > 1)
                    {
                        this.fpDiagnose_Sheet1.Rows.Add(0, 1);

                        if (diagnoseType != null && diagnoseType.Count > 0)
                        {
                            this.fpDiagnose_Sheet1.Cells[0, 0].Value = diagnoseType[0];
                        }

                        this.fpDiagnose_Sheet1.Cells[0, 1].Value = false;
                        this.fpDiagnose_Sheet1.Cells[0, 4].Value = true;
                        this.fpDiagnose_Sheet1.Cells[0, 5].Value = true;
                        //诊断日期
                        this.fpDiagnose_Sheet1.Cells[0, 6].Text = System.DateTime.Now.Date.ToShortDateString();
                        //诊断医生代码
                        this.fpDiagnose_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpDiagnose_Sheet1.Cells[0, 7].Text = currEmp.ID; // CacheManager.DiagMgr.Operator.ID;
                        //诊断医生名称
                        this.fpDiagnose_Sheet1.Cells[0, 8].Text = CacheManager.DiagMgr.Operator.Name;

                        this.fpDiagnose_Sheet1.Cells[0, 9].Value = "一期";
                        this.fpDiagnose_Sheet1.Cells[0, 10].Value = "普通";
                        //备注
                        this.fpDiagnose_Sheet1.Cells[0, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                        this.fpDiagnose.Focus();
                        this.fpDiagnose_Sheet1.SetActiveCell(this.fpDiagnose_Sheet1.Rows.Count - 1, 2, false);
                    }
                    else
                    {
                        this.fpDiagnose_Sheet1.Rows.Add(0, 1);
                        this.fpDiagnose_Sheet1.Cells[0, 0].Value = s[0];
                        this.fpDiagnose_Sheet1.Cells[0, 1].Value = false;
                        this.fpDiagnose_Sheet1.Cells[0, 4].Value = true;
                        this.fpDiagnose_Sheet1.Cells[0, 5].Value = true;
                        //诊断日期
                        this.fpDiagnose_Sheet1.Cells[0, 6].Text = System.DateTime.Now.Date.ToShortDateString();
                        //诊断医生代码
                        this.fpDiagnose_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpDiagnose_Sheet1.Cells[0, 7].Text = currEmp.ID;  //CacheManager.DiagMgr.Operator.ID;
                        //诊断医生名称
                        this.fpDiagnose_Sheet1.Cells[0, 8].Text = CacheManager.DiagMgr.Operator.Name;
                        //备注
                        this.fpDiagnose_Sheet1.Cells[0, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpDiagnose.Focus();
                        this.fpDiagnose_Sheet1.SetActiveCell(this.fpDiagnose_Sheet1.Rows.Count - 1, 2, false);
                    }
                    this.fpDiagnose_Sheet1.Cells[0, 1].Value = true;
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
            this.ucUserText = this.ucUserText1;

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

            if (!isChineseMedicalDept || chinaSeeAll)
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

        /// <summary>
        /// 根据生日获取年龄
        /// </summary>
        /// <param name="birthday"></param>
        private string getAge(DateTime birthday)
        {
            string rtn = string.Empty;

            if (birthday == DateTime.MinValue)
            {
                return "";
            }

            DateTime current;
            int year = 0, month = 0, day = 0;

            current = this.dbManager.GetDateTimeFromSysDateTime();
            this.dbManager.GetAge(birthday, current, ref year, ref month, ref day);

            if (year > 1)
            {
                rtn = year.ToString();
                rtn += Language.Msg("岁");
            }
            else if (year == 1)
            {
                if (month >= 0)//一岁
                {
                    rtn = year.ToString();
                    rtn += Language.Msg("岁");
                }
                else
                {
                    rtn = Convert.ToString(12 + month);
                    rtn += Language.Msg("月");
                }
            }
            else if (month > 0)
            {
                rtn = month.ToString();
                rtn += Language.Msg("月");
            }
            else if (day > 0)
            {
                rtn = day.ToString();
                rtn += Language.Msg("日");
            }

            return rtn;
        }

        /// <summary>
        /// 科室常用诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDeptDiag_Click(object sender, EventArgs e)
        {
            if (this.chkDeptDiag.CheckState == CheckState.Checked)
            {
                this.ucDiagnose1.AlDiag = this.alDeptDiag;
            }
            if (this.chkDeptDiag.CheckState == CheckState.Unchecked)
            {
                this.ucDiagnose1.AlDiag = this.alAllDiag;
            }
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
                FS.HISFC.Models.HealthRecord.Diagnose diag = this.fpDiagnose_Sheet1.Rows[this.fpDiagnose_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.HealthRecord.Diagnose;

                if (diag != null)
                {
                    MessageBox.Show(Classes.Function.GetMsg("该诊断已经保存") + "," + Classes.Function.GetMsg("不允许修改") + "，" + Classes.Function.GetMsg("只可以作废") + "!", Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }

                FS.HISFC.Models.HealthRecord.ICD item = null;
                if (this.ucDiagnose1.GetItem(ref item) == -1)
                {
                    //MessageBox.Show("获取项目出错!","提示");
                    CloseDiagnoseForm();
                    return -1;
                }
                if (item == null)
                    return -1;

                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Text = item.ID;
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 3].Text = item.Name;
                this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 3].Locked = true;
                CloseDiagnoseForm();
                this.fpDiagnose.Focus();
                this.fpDiagnose_Sheet1.SetActiveCell(fpDiagnose_Sheet1.ActiveRowIndex, 4);
            }
            catch (Exception ex)
            {
                ucDiagnose1.Visible = false;
                MessageBox.Show("GetInfo" + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 历史诊断
        /// </summary>
        private void HistoryCase()
        {
            DataSet ds = new DataSet();

            if (this.regObj == null)
            {
                return;
            }

            if (this.fpHistory_Sheet1.RowCount > 0)
            {
                this.fpHistory_Sheet1.Rows.Remove(0, this.fpHistory_Sheet1.RowCount);
            }

            ArrayList al = CacheManager.RegMgr.Query(this.regObj.PID.CardNO, DateTime.Now.AddDays(-rememberCaseDiagnoseDays));
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
                this.fpHistory_Sheet1.Cells[i, 1].Text = diagObj.DiagInfo.ICD10.ID == "MS999" ? Language.Msg("是") : Language.Msg("否");
                this.fpHistory_Sheet1.Cells[i, 2].Text = diagObj.DiagInfo.ICD10.ID;
                this.fpHistory_Sheet1.Cells[i, 3].Text = diagObj.DiagInfo.ICD10.Name;
                this.fpHistory_Sheet1.Cells[i, 4].Text = diagObj.DubDiagFlag == "1" ? Language.Msg("是") : Language.Msg("否");
                //this.fpHistory_Sheet1.Cells[i, 5].Text = diagObj.Is30Disease;
                //this.fpHistory_Sheet1.Cells[i, 5].Text = diagObj.IsValid ? "1" : "0";
                this.fpHistory_Sheet1.Cells[i, 5].Text = diagObj.Diagnosis_flag == "1" ? Language.Msg("是") : Language.Msg("否");
                this.fpHistory_Sheet1.Cells[i, 6].Text = diagObj.DiagInfo.Doctor.Name;
                this.fpHistory_Sheet1.Cells[i, 7].Text = diagObj.OperInfo.OperTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (this.levelHelper.GetObjectFromID(diagObj.LevelCode) != null)
                {
                    this.fpHistory_Sheet1.Cells[i, 8].Text = this.levelHelper.GetObjectFromID(diagObj.LevelCode).Name;
                }

                //挂号信息是否为当前登录科室
                bool isCurrentDept = (diagObj.OperInfo.Dept.ID == ((HISFC.Models.Base.Employee)CacheManager.DiagMgr.Operator).Dept.ID ? true : false);


                this.fpHistory_Sheet1.Rows[i].Visible = true;




                //if (diagObj.Is30Disease == "0")
                if (!diagObj.IsValid)
                {
                    this.fpHistory_Sheet1.Rows[i].BackColor = Color.Red;//无效诊断背景色
                }
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            this.regObj = null;
            this.lblCardNO.Text = "";
            this.lblName.Text = "";
            this.lblSex.Text = "";
            this.lblAge.Text = "";
            this.lblDept.Text = "";
            this.tbChiefComplaint.Text = "";
            this.tbPresentIllness.Text = "";
            this.tbPastHistory.Text = "";
            this.tbAllergicHistory.Text = "";
            this.tbPhysicalExam.Text = "";
            this.tbTreatment.Text = "";
            this.tbMemo.Text = "";
            this.fpDiagnose_Sheet1.RowCount = 1;// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            this.tvCaseTemplate.Nodes.Clear();
            this.tvHistoryCase.Nodes.Clear();
            this.tbmemo2.Text = "";
            this.tbMemo9.Text = "";

            this.pnlTitle.Visible = true;
            this.Panel2.Visible = true;
            this.panel3.Visible = true;
            this.panel4.Visible = true;
            this.panel4.Visible = true;
            this.panel5.Visible = true;
            this.panel6.Visible = true;
            this.panel7.Visible = true;
            this.panel8.Visible = true;
            this.panel9.Visible = true;
            this.panel11.Visible = false;
            this.fpHistory_Sheet1.RowCount = 0;
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
                        this.ucDiagnose1.PriorRow();
                        return true;
                    }
                }
                else if (keyData == Keys.Down)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ShowDiagPanel();
                        this.ucDiagnose1.NextRow();
                        return true;
                    }
                }
                else if (keyData == Keys.Escape)
                {
                    CloseDiagnoseForm();
                }
            }
            catch { }

            return base.ProcessCmdKey(ref msg, keyData);
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
                CloseDiagnoseForm();
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
        /// 关闭诊断选择框
        /// </summary>
        private void CloseDiagnoseForm()
        {
            //this.pnlTitle.Visible = true;
            //this.Panel2.Visible = true;
            //this.panel3.Visible = true;
            //this.panel4.Visible = true;
            //this.panel4.Visible = true;
            //this.panel5.Visible = true;
            //this.panel6.Visible = true;
            //this.panel7.Visible = true;
            //this.panel8.Visible = true;
            this.ucDiagnose1.Visible = false;
            //this.panel9.Height = this.Panel9Height;
            //this.fpDiagnose.Height = this.panel9.Height  - this.FpGap;
        }

        /// <summary>
        /// 打开诊断选择框
        /// </summary>
        private void OpenDiagnoseForm()
        {
            //this.pnlTitle.Visible = false;
            //this.Panel2.Visible = false;
            //this.panel3.Visible = false;
            //this.panel4.Visible = false;
            //this.panel4.Visible = false;
            //this.panel5.Visible = false;
            //this.panel6.Visible = false;
            //this.panel7.Visible = false;
            //this.panel8.Visible = false;
            this.ucDiagnose1.Visible = true;
            //this.panel9.Height = this.pnlMain.Height;
            //this.fpDiagnose.Height = this.panel9.Height  - this.FpGap;
        }

        #endregion

        #region 诊断操作

        void btAddDiagnose_Click(object sender, EventArgs e)
        {
            this.AddNew(this.fpDiagnose_Sheet1.Rows.Count);
        }

        void btDeleteDiagnose_Click(object sender, EventArgs e)
        {
            this.CloseDiagnoseForm();

            if (this.fpDiagnose_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpDiagnose_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 2].Text)
                && string.IsNullOrEmpty(this.fpDiagnose_Sheet1.Cells[this.fpDiagnose_Sheet1.ActiveRowIndex, 3].Text)
                )
            {

            }
            else
            {
                DialogResult r = MessageBox.Show(Classes.Function.GetMsg("确定要删除该诊断吗") + "?", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.No)
                {
                    return;
                }
            }
            //数据库里存在的 ！= null
            if (this.fpDiagnose_Sheet1.Rows[this.fpDiagnose_Sheet1.ActiveRowIndex].Tag != null)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = this.fpDiagnose_Sheet1.Rows[this.fpDiagnose_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.HealthRecord.Diagnose;
                if (diag != null)
                {
                    MessageBox.Show(Classes.Function.GetMsg("该诊断已经保存") + "，" + Classes.Function.GetMsg("只可以作废") + "！", Classes.Function.GetMsg("提示"));
                    return;
                }
            }
            this.fpDiagnose_Sheet1.Rows.Remove(this.fpDiagnose_Sheet1.ActiveRowIndex, 1);
        }

        void btInvalidDiagnose_Click(object sender, EventArgs e)
        {
            if (this.fpDiagnose_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpDiagnose_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            DialogResult r = MessageBox.Show(Classes.Function.GetMsg("确实要作废此诊断吗") + "？", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
            {
                return;
            }

            FS.HISFC.Models.HealthRecord.Diagnose diag1 = this.fpDiagnose_Sheet1.ActiveRow.Tag as FS.HISFC.Models.HealthRecord.Diagnose;
            if (diag1 == null)
            {
                MessageBox.Show(Classes.Function.GetMsg("该诊断尚未保存") + "，" + Classes.Function.GetMsg("不需要作废") + "！", Classes.Function.GetMsg("提示"));
                return;
            }
            try
            {
                int flag = CacheManager.DiagMgr.CancelDiagnoseSingleForClinic(this.PatientId, diag1.DiagInfo.ICD10.ID, diag1.DiagInfo.HappenNo.ToString());

                if (flag == 1 && this.listDiagnose.Count > 0 && this.listDiagnose != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in listDiagnose)
                    {
                        if (diag.DiagInfo.HappenNo == diag1.DiagInfo.HappenNo)
                        {
                            this.listDiagnose.Remove(diag);
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CancelDiag" + ex.Message);
            }
            this.ShowPatientInfo();
        }

        //{64FDFB25-6A75-42b4-9E00-80BDEE666706}

        void btSave_Click(object sender, EventArgs e)
        {
            //同步预产期到bd_crm_patient表中  {2DF1211B-9E5F-4b0b-B5CA-8EDBC4FC3B59}
            string exptime = this.ucModifyOutPatientHealthInfo1.GetExpectedTime();

            bool ischeck = this.ucModifyOutPatientHealthInfo1.GetChecked();

            if (string.IsNullOrEmpty(exptime) && !ischeck && currDept.ID == "5011" && Language.Msg(this.regObj.Sex.Name) == "女")
            {
                MessageBox.Show("【预产期】与【未怀孕】需要有一个必选！！！");

                return;
            }

            if (exptime != "1" && !string.IsNullOrEmpty(exptime))   //适配其他科室
            {

                if (DateTime.Now.AddMonths(11) > Convert.ToDateTime(exptime) && DateTime.Now < Convert.ToDateTime(exptime))
                {
                    try
                    {
                        string sql = "select * from fin_opr_register t left join com_patientinfo t1 on t.card_no=t1.card_no where t.clinic_code='" + this.PatientId + "'";
                        DataSet ds = new DataSet();
                        dbManager.ExecQuery(sql, ref ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        if (dt != null)
                        {
                            string crmid = dt.Rows[0]["crmid"].ToString();
                            exptime = Convert.ToDateTime(exptime).ToString("yyyy-MM-dd");
                            string req = "<req><crmid>" + crmid + "</crmid><expectedtime>" + exptime + "</expectedtime></req>";
                            FS.HISFC.BizProcess.Integrate.WSHelper.updatePatientInfoByExceptedTime(req);
                        }
                    }
                    catch (Exception)
                    {


                    }

                }

            }

            this.Save();
        }

        /// <summary>
        /// 同步  {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btsynchro_Click(object sender, EventArgs e)
        {

            if (this.regObj.IsSee && this.regObj.DoctorInfo.SeeDate.AddDays(1) < CacheManager.RegMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,诊断不允许修改!", "提示");
                return;
            }


            if (CheckSym.Contains(this.PatientId))
            {
                DialogResult dr = MessageBox.Show(Classes.Function.GetMsg("信息已同步过，是否再次同步") + "?", Classes.Function.GetMsg("提示"), MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
            }

            //FS.HISFC.Components.OutpatientFee.Class.LogManager.Write("根据挂号ID查询:" + "就诊卡号:" + this.regObj.PID.CardNO.PadRight(10, '_') + ";姓名:" + this.regObj.Name.PadRight(10, '_') + ";挂号ID:" + this.patientId + "   " + DateTime.Now.ToString());

            FS.HISFC.Models.HealthRecord.VisitReord record = CacheManager.VisitReordMgr.QueryObcrecordBySerialno(this.patientId).ToArray().FirstOrDefault() as FS.HISFC.Models.HealthRecord.VisitReord;
            if (record != null)
            {
                if (record.EXPECTEDTIME != "")
                {
                    expectedtime = record.EXPECTEDTIME.ToString();
                }
                this.ucModifyOutPatientHealthInfo1.GetHealthInfo(record.HEIGHT, record.WEIGHT, record.SYSTOLIC, record.DIASTOLIC, record.TEMPERATURE, record.BLOOD, expectedtime);
                SetEMR(record);
                SetDiagInfo(record);

                CheckSym.Add(this.PatientId);

            }
            else
            {
                MessageBox.Show("没有可同步信息");
            }

        }




        /// <summary>
        /// 设置同步诊断信息   {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        /// <param name="record"></param>
        void SetDiagInfo(FS.HISFC.Models.HealthRecord.VisitReord record)
        {
            int rowIndexs = this.fpDiagnose_Sheet1.Rows.Count;

            if (rowIndexs == 1 && string.IsNullOrEmpty(this.fpDiagnose_Sheet1.Cells[0, 2].Text))
            {
                this.fpDiagnose_Sheet1.RemoveRows(0, rowIndexs);
            }


            //新增
            var insertdia = record.Diaglist.Select(a => a.DiagCode).Except(GetICD10()).ToList();
            //作废
            var deldia = GetICD10().Except(record.Diaglist.Select(a => a.DiagCode)).ToList();
            //含相同的诊断
            var jiaoji = record.Diaglist.Select(a => a.DiagCode).Intersect(GetICD10()).ToList();

            foreach (var item in record.Diaglist)
            {
                if (string.IsNullOrEmpty(item.DiagCode))
                {
                    continue;
                }

                if (insertdia.Contains(item.DiagCode))
                {
                    //新增

                    int rowIndex = this.fpDiagnose_Sheet1.Rows.Count;
                    this.fpDiagnose_Sheet1.Rows.Add(rowIndex, 1);
                    this.fpDiagnose_Sheet1.Columns[2].Locked = false;
                    if (this.diagnoseType != null && this.diagnoseType.Count > 1)
                    {
                        //新增的默认为其他诊断

                        if (rowIndex == 0)
                        {
                            this.fpDiagnose_Sheet1.Cells[rowIndex, 0].Value = this.diagnoseType[0];
                        }
                        else
                        {
                            this.fpDiagnose_Sheet1.Cells[rowIndex, 0].Value = this.diagnoseType[1];
                        }


                    }

                    this.fpDiagnose_Sheet1.Cells[rowIndex, 1].Value = false;
                    //{E3B5B7A5-CECE-4b30-948A-3CFBA188A257}禁止使用描述诊断
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 1].Locked = true;
                    //{E3B5B7A5-CECE-4b30-948A-3CFBA188A257}禁止使用描述诊断
                    //this.fpDiagnose_Sheet1.Cells[rowIndex, 2].Locked = true;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 2].Locked = false;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 2].Text = item.DiagCode;
                    //{E3B5B7A5-CECE-4b30-948A-3CFBA188A257}禁止使用描述诊断
                    //this.fpDiagnose_Sheet1.Cells[rowIndex, 3].Locked = false;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 3].Locked = true;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 3].Text = item.DiagName;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 4].Value = false;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 5].Value = true;
                    //this.fpDiagnose_Sheet1.Cells[0, 5].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.Diagnosis_flag);//是否初诊
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 6].Text = this.dbManager.GetDateTimeFromSysDateTime().ToShortDateString();
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 7].Text = currEmp.ID; //CacheManager.DiagMgr.Operator.ID;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 8].Text = CacheManager.DiagMgr.Operator.Name;
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 9].Text = "一期";
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 10].Text = "一级";
                    this.fpDiagnose_Sheet1.Cells[rowIndex, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                    this.fpDiagnose_Sheet1.SetActiveCell(rowIndex, 2, false);

                    this.Focus();
                    this.fpDiagnose.Focus();



                }

                if (jiaoji.Contains(item.DiagCode))
                {
                    //不动
                }
            }



            //作废 //{074d247c-5b09-4c0d-bb5f-215768a871b3}

            //for (int i = 0; i < this.fpDiagnose_Sheet1.Rows.Count; i++)
            //{
            //    FS.HISFC.Models.HealthRecord.Diagnose temp = null;
            //    if (this.fpDiagnose_Sheet1.Rows[i].Tag != null)
            //    {
            //        temp = this.fpDiagnose_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Diagnose;

            //        //作废
            //        if (deldia.Contains(temp.DiagInfo.ICD10.ID))
            //        {
            //            if (!temp.IsValid)
            //            {
            //                continue;
            //            }
            //            int flag = CacheManager.DiagMgr.CancelDiagnoseSingleForClinic(this.PatientId, temp.DiagInfo.ICD10.ID, temp.DiagInfo.HappenNo.ToString());

            //            if (flag == 1)
            //            {
            //                this.fpDiagnose_Sheet1.Rows[i].ForeColor = Color.Red;
            //                temp.IsValid = false;
            //            }
            //            if (flag == 1 && this.listDiagnose.Count > 0 && this.listDiagnose != null)
            //            {

            //                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in listDiagnose)
            //                {
            //                    if (diag.DiagInfo.HappenNo == temp.DiagInfo.HappenNo)
            //                    {

            //                        this.listDiagnose.Remove(diag);
            //                        break;
            //                    }
            //                }

            //            }
            //        }

            //    }

            //}



        }

        //{074d247c-5b09-4c0d-bb5f-215768a871b3}
        List<string> GetICD10()
        {
            List<string> str = new List<string>();

            for (int i = 0; i < this.fpDiagnose_Sheet1.Rows.Count; i++)
            {
                str.Add(this.fpDiagnose_Sheet1.Cells[i, 2].Text);
            }
            return str;
        }


        /// <summary>
        /// 设置同步电子病历信息
        /// </summary>
        /// <param name="record"></param>

        void SetEMR(FS.HISFC.Models.HealthRecord.VisitReord record)
        {
            this.tbChiefComplaint.Text = record.CHIEFCOMPLAINT;  //主诉
            this.tbPresentIllness.Text = record.HPI;//现病史
            this.tbPastHistory.Text = record.PASTHISTORY;//既往史
            this.tbPhysicalExam.Text = record.SIGN;//查体
            this.tbMemo.Text = "";//备注
            this.tbAllergicHistory.Text = record.ALLERGIES;//过敏史
            this.tbTreatment.Text = record.PRESCRIPTION;//处理
        }


        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
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

        /// <summary>
        /// 判断病历是否为空
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            if (string.IsNullOrEmpty(this.tbChiefComplaint.Text) && string.IsNullOrEmpty(this.tbPresentIllness.Text) && string.IsNullOrEmpty(this.tbPastHistory.Text)
                && string.IsNullOrEmpty(this.tbPhysicalExam.Text) && string.IsNullOrEmpty(this.tbAllergicHistory.Text) && string.IsNullOrEmpty(this.tbMemo.Text) && string.IsNullOrEmpty(this.tbTreatment.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证健康评估{b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
        /// </summary>
        /// <returns></returns>
        private bool ValidAssessment()
        {
            if (string.IsNullOrEmpty(this.comeducational.Text) && string.IsNullOrEmpty(this.commedication.Text) && string.IsNullOrEmpty(this.comdiet.Text)
                   && string.IsNullOrEmpty(this.comdisease.Text) && string.IsNullOrEmpty(this.comeducation.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.regObj == null || string.IsNullOrEmpty(regObj.ID))
            {
                MessageBox.Show(Classes.Function.GetMsg("请选择患者") + "！", Classes.Function.GetMsg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (this.regObj.IsSee && this.regObj.DoctorInfo.SeeDate.AddDays(1) < CacheManager.RegMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,病历不允许修改!", "提示");
                return -1;
            }

            if (this.IsChecked)
            {
                if (!Valid())
                {
                    MessageBox.Show("病历信息不能为空，请输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }
            //获得病历信息
            this.newOperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime().ToString();

            //{b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
            if ((writedep.ToArray().FirstOrDefault() as FS.FrameWork.Models.NeuObject).Name.Contains(this.currDept.ID))
            {
                if (!ValidAssessment())
                {
                    MessageBox.Show("教育评估不能为空，请输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            this.GetCaseHistory();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #region 电子病历

            bool isSuccess = true;//是否保存成功

            int caseRtn;

            if (isNew)
            {
                //保存
                caseRtn = CacheManager.OutOrderMgr.InsertCaseHistory(this.regObj, this.caseHistory);
            }
            else
            {
                caseRtn = CacheManager.OutOrderMgr.UpdateCaseHistory(this.regObj, this.caseHistory, this.OldOperTime);
            }

            if (caseRtn == -1)
            {
                isSuccess = false;
                FS.FrameWork.Management.PublicTrans.RollBack();

                if (CacheManager.OutOrderMgr.DBErrCode == 1)
                {
                    MessageBox.Show(Classes.Function.GetMsg("该患者已存在门诊病历") + "," + Classes.Function.GetMsg("不能重复生成") + "!", Classes.Function.GetMsg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }

                MessageBox.Show(Classes.Function.GetMsg("门诊病历保存失败") + CacheManager.OutOrderMgr.Err, Classes.Function.GetMsg("提示"));
                return -1;
            }
            #endregion

            #region 体征信息
            //{CE1A8814-003C-47dd-AAE1-F870CBCD6BB9}
            //保存体重等体征信息
            int rev = this.ucModifyOutPatientHealthInfo1.Save();
            if (rev == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.ucModifyOutPatientHealthInfo1.ErrInfo);
                return -1;
            }
            else if (rev == 0)
            {
                ucModifyOutPatientHealthInfo1.GetHealthInfo(ref this.regObj);
            }

            #endregion

            #region  诊断

            //{32F5AA9B-FAB2-4390-A82C-D34266406FA3}
            /// <summary>
            /// 是否传染病，报卡提示
            /// </summary>
            bool isInfectLog = false;

            string messagelog = null;

            for (int i = 0; i < this.fpDiagnose_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = new FS.HISFC.Models.HealthRecord.Diagnose();
                FS.HISFC.Models.HealthRecord.Diagnose temp = null;
                if (this.fpDiagnose_Sheet1.Rows[i].Tag != null)
                {
                    temp = this.fpDiagnose_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Diagnose;
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
                diag.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(this.fpDiagnose_Sheet1.Cells[i, 0].Text);
                bool isLocked = FS.FrameWork.Function.NConvert.ToBoolean(this.fpDiagnose_Sheet1.GetValue(i, 1));
                diag.DiagInfo.IsMain = isLocked;//借用一下

                //诊断ICD码
                if (isLocked)
                {
                    diag.DiagInfo.ICD10.ID = "MS999";
                }
                else
                {
                    //{32F5AA9B-FAB2-4390-A82C-D34266406FA3}
                    diag.DiagInfo.ICD10.ID = this.fpDiagnose_Sheet1.Cells[i, 2].Text;
                    //首个参数为0时，查询icd10编码  {ff204e15-7044-4176-a7a1-fd52b06129b6}
                    string message = icdMedicare.isICDUpload("0", diag.DiagInfo.ICD10.ID);
                    //{6361a1ed-efff-4fba-9009-4a012a5779d8}
                    if (!string.IsNullOrEmpty(message) && message != "-1")
                    {
                        isInfectLog = true;
                        messagelog += "诊断：" + this.fpDiagnose_Sheet1.Cells[i, 3].Text + "," + message + "\r\n";
                        //MessageBox.Show(Classes.Function.GetMsg("疑似传染病患者，请注意是否需要填写传染病报卡") + "！", Classes.Function.GetMsg("提示"));
                    }
                }
                if (diag.DiagInfo.ICD10.ID == "" && isLocked == false)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Classes.Function.GetMsg("描述诊断请选择描述标志") + "！", Classes.Function.GetMsg("提示"));
                    this.fpDiagnose.Focus();
                    this.fpDiagnose_Sheet1.SetActiveCell(i, 1);
                    return -1;
                }
                if (isLocked == true && this.fpDiagnose_Sheet1.Cells[i, 3].Text.Trim() == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Classes.Function.GetMsg("请填写描述诊断") + "！", Classes.Function.GetMsg("提示"));
                    this.fpDiagnose.Focus();
                    this.fpDiagnose_Sheet1.SetActiveCell(i, 3);
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = this.levelHelper.GetObjectFromName(this.fpDiagnose_Sheet1.Cells[i, 10].Text);
                if (obj != null)
                {
                    diag.LevelCode = obj.ID;
                }
                obj = this.periodHelper.GetObjectFromName(this.fpDiagnose_Sheet1.Cells[i, 9].Text);
                if (obj != null)
                {
                    diag.PeriorCode = obj.ID;
                }

                //诊断名称
                diag.DiagInfo.ICD10.Name = this.fpDiagnose_Sheet1.Cells[i, 3].Text;
                //首个参数为1时，查询诊断名称
                //{32F5AA9B-FAB2-4390-A82C-D34266406FA3}
                //if (icdMedicare.isICDUpload("1", diag.DiagInfo.ICD10.Name) > 0)
                //{
                //    isInfectLog = true;
                //    //MessageBox.Show(Classes.Function.GetMsg("疑似传染病患者，请注意是否需要填写传染病报卡") + "！", Classes.Function.GetMsg("提示"));
                //}

                //是否疑诊
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpDiagnose_Sheet1.GetValue(i, 4)))
                {
                    diag.DubDiagFlag = "1";
                }
                else
                {
                    diag.DubDiagFlag = "0";
                }
                //是否初诊
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpDiagnose_Sheet1.GetValue(i, 5)))
                {
                    diag.Diagnosis_flag = "1";
                }
                else
                {
                    diag.Diagnosis_flag = "0";
                }
                //诊断日期
                diag.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpDiagnose_Sheet1.GetValue(i, 6));//11
                //输入类别
                diag.OperType = "1";
                diag.DiagInfo.Doctor.ID = this.fpDiagnose_Sheet1.Cells[i, 7].Text;
                diag.DiagInfo.Doctor.Name = this.fpDiagnose_Sheet1.Cells[i, 8].Text;
                //备注
                diag.Memo = this.fpDiagnose_Sheet1.Cells[i, 11].Text;

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

            //{32F5AA9B-FAB2-4390-A82C-D34266406FA3}
            //若两种查询到可能为国家规定传染病，提示填写传染病报卡
            if (isInfectLog == true)
            {
                //{ff204e15-7044-4176-a7a1-fd52b06129b6}
                MessageBox.Show(Classes.Function.GetMsg(messagelog + "疑似传染病患者，请注意是否需要填写传染病报卡!!!"), Classes.Function.GetMsg("传染病报卡提示"));
            }

            #endregion

            if (isSuccess == true)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show(Classes.Function.GetMsg("保存成功") + "！");
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Classes.Function.GetMsg("保存失败") + "！");
                isSuccess = false;
                return -1;
            }

            this.needSave = false;
            this.oldOperTime = this.newOperTime;
            this.isNew = false;

            this.ShowPatientInfo();

            //门诊医生站用:返回医生站开立界面
            if (this.SaveClicked != null)
            {
                this.SaveClicked(this, isMustDcpReport ? listDiagnose : null);
            }

            return 1;
        }

        private void AddNew(int rowIndex)
        {
            if (this.regObj == null)
            {
                MessageBox.Show("请选择患者!", "提示");
                return;
            }

            if (this.regObj.IsSee
                && this.regObj.DoctorInfo.SeeDate.AddDays(1) < CacheManager.RegMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,诊断不允许修改!", "提示");
                return;
            }

            this.fpDiagnose_Sheet1.Rows.Add(rowIndex, 1);
            this.fpDiagnose_Sheet1.Columns[2].Locked = false;

            if (this.diagnoseType != null && this.diagnoseType.Count > 1)
            {
                //新增的默认为其他诊断
                this.fpDiagnose_Sheet1.Cells[rowIndex, 0].Value = rowIndex >= 1 ? this.diagnoseType[1] : this.diagnoseType[0];
            }

            this.fpDiagnose_Sheet1.Cells[rowIndex, 1].Value = false;
            //{E3B5B7A5-CECE-4b30-948A-3CFBA188A257}禁止使用描述诊断
            this.fpDiagnose_Sheet1.Cells[rowIndex, 1].Locked = true;
            //{E3B5B7A5-CECE-4b30-948A-3CFBA188A257}禁止使用描述诊断
            //this.fpDiagnose_Sheet1.Cells[rowIndex, 2].Locked = true;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 2].Locked = false;
            //{E3B5B7A5-CECE-4b30-948A-3CFBA188A257}禁止使用描述诊断
            //this.fpDiagnose_Sheet1.Cells[rowIndex, 3].Locked = false;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 3].Locked = true;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 4].Value = false;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 5].Value = true;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 6].Text = this.dbManager.GetDateTimeFromSysDateTime().ToShortDateString();
            this.fpDiagnose_Sheet1.Cells[rowIndex, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpDiagnose_Sheet1.Cells[rowIndex, 7].Text = currEmp.ID;//CacheManager.DiagMgr.Operator.ID;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 8].Text = CacheManager.DiagMgr.Operator.Name;
            this.fpDiagnose_Sheet1.Cells[rowIndex, 9].Text = "一期";
            this.fpDiagnose_Sheet1.Cells[rowIndex, 10].Text = "一级";
            this.fpDiagnose_Sheet1.Cells[rowIndex, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.fpDiagnose_Sheet1.SetActiveCell(rowIndex, 2, false);

            this.Focus();
            this.fpDiagnose.Focus();
        }

        #endregion

        #region 诊断证明和病假建议书打印

        /// <summary>
        /// 诊断证明点击事件
        /// </summary>
        void btDiagnosisPrint_Click(object sender, EventArgs e)
        {

            FS.HISFC.BizProcess.Interface.Order.IDiagnosisProvePrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase), typeof(FS.HISFC.BizProcess.Interface.Order.IDiagnosisProvePrint), 3) as FS.HISFC.BizProcess.Interface.Order.IDiagnosisProvePrint;
            if (o == null)
            {
                return;
            }
            else
            {
                FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
                caseHistory = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.regObj.ID);

                if (caseHistory == null)
                {
                    this.Save();
                }
                if (string.IsNullOrEmpty(caseHistory.User01))
                {
                    MessageBox.Show("请填写治疗方案！");
                    return;
                }

                FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
                diagExtend = this.diagExtMgr.QueryByClinicCodCardNo(this.regObj.ID, this.regObj.PID.CardNO);
                string proveNo = string.Empty;

                string caseMain;
                string caseNow;
                string opinions;

                frmDiagProveInfo frmdiagProve = new frmDiagProveInfo();   
                frmdiagProve.SetInfo(caseHistory, diagExtend);
                frmdiagProve.ShowDialog();
                caseMain = frmdiagProve.CaseMain;
                caseNow = frmdiagProve.CaseNow;
                opinions = frmdiagProve.Opinions;
                if (string.IsNullOrEmpty(opinions))
                {
                    return;
                }

                if (diagExtend == null)
                {
                    
                    string GetProveNoSql = "select SEQ_FIN_DIAGNOSISPROVENO.nextval from dual";
                    DataSet ds = new DataSet();
                    dbManager.ExecQuery(GetProveNoSql, ref ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt != null)
                    {
                        proveNo = dt.Rows[0]["nextval"].ToString();
                    }
                    FS.HISFC.Models.Order.OutPatient.DiagExtend DiagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
                    DiagExtend.ClinicCode = this.regObj.ID;
                    DiagExtend.CardNo = this.regObj.PID.CardNO;
                    DiagExtend.ProveNo = proveNo;
                    DiagExtend.ValidFlag = "1";
                    DiagExtend.ProvePrintDate = this.dbManager.GetSysDateTime();
                    DiagExtend.CaseMain = caseMain;
                    DiagExtend.CaseNow = caseNow;
                    DiagExtend.Opinions = opinions;

                    if (this.diagExtMgr.InsertDiagExtend(DiagExtend) < 0)
                    {
                        MessageBox.Show("保存门诊诊断证明失败！");
                        return;
                    }
                }
                else
                {
                    //有病假建议信息，但无诊断证明记录
                    if (!string.IsNullOrEmpty(diagExtend.ProveNo))
                    {
                        //string GetProveNoSql = "select SEQ_FIN_DIAGNOSISPROVENO.nextval from dual";
                        //DataSet ds = new DataSet();
                        //dbManager.ExecQuery(GetProveNoSql, ref ds);
                        //DataTable dt = new DataTable();
                        //dt = ds.Tables[0];
                        //if (dt != null)
                        //{
                        //    proveNo = dt.Rows[0]["nextval"].ToString();
                        //}
                        FS.HISFC.Models.Order.OutPatient.DiagExtend DiagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
                        DiagExtend.ClinicCode = this.regObj.ID;
                        DiagExtend.CardNo = this.regObj.PID.CardNO;
                        DiagExtend.ProveNo = diagExtend.ProveNo;
                        DiagExtend.LeaveNo = diagExtend.LeaveNo;
                        DiagExtend.LeaveDays = diagExtend.LeaveDays;
                        DiagExtend.LeaveStart = diagExtend.LeaveStart;
                        DiagExtend.LeaveEnd = diagExtend.LeaveEnd;
                        DiagExtend.ValidFlag = "1";
                        DiagExtend.ProvePrintDate = this.dbManager.GetSysDateTime();
                        DiagExtend.LeavePrintDate = diagExtend.LeavePrintDate;
                        DiagExtend.CaseMain = caseMain;
                        DiagExtend.CaseNow = caseNow;
                        DiagExtend.Opinions = opinions;
                        DiagExtend.LeaveType = diagExtend.LeaveType;

                        if (this.diagExtMgr.UpdateDiagExtendByLeaveNo(DiagExtend) < 0)
                        {
                            MessageBox.Show("更新门诊诊断证明失败！");
                            return;
                        }
                    }
                }

                o.RegId = this.regObj.ID;
                bool isPrint = o.IsPrint;
                if (isPrint)
                {
                    o.Print();
                }

            }
        }

        /// <summary>
        /// 病假建议点击事件
        /// </summary>
        void btLeave_Click(object sender, EventArgs e)
        {

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
            caseHistory = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.regObj.ID);

            if (caseHistory == null)
            {
                this.Save();
            }
             
            DateTime leaveStart;
            DateTime leaveEnd;
            string leaveType;
            string leaveNo = string.Empty;

            frmLeavePrint frmleavePrint = new frmLeavePrint();
            frmleavePrint.ShowDialog();
            leaveStart = frmleavePrint.LeaveStart;
            leaveEnd = frmleavePrint.LeaveEnd;
            leaveType = "1";// frmleavePrint.LeaveType;

            if (frmleavePrint.LeaveStart == DateTime.MinValue)
            {
                return;
            }

            FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
            diagExtend = this.diagExtMgr.QueryByClinicCodCardNo(this.regObj.ID, this.regObj.PID.CardNO);

            if (diagExtend != null)
            {
                #region 有诊断证明记录但不存在病假建议信息记录，更新病假信息

                if (string.IsNullOrEmpty(diagExtend.LeaveNo))
                {
                    string GetLeaveNoSql = "select SEQ_FIN_DIAGLEAVENO.nextval from dual";
                    DataSet ds = new DataSet();
                    dbManager.ExecQuery(GetLeaveNoSql, ref ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt != null)
                    {
                        leaveNo = dt.Rows[0]["nextval"].ToString();
                    }

                    diagExtend.ClinicCode = this.regObj.ID;
                    diagExtend.CardNo = this.regObj.PID.CardNO;
                    diagExtend.LeaveNo = leaveNo;
                    diagExtend.LeaveDays = ((leaveEnd - leaveStart).Days + 1).ToString();
                    if (leaveStart.ToShortDateString() == System.DateTime.Now.ToShortDateString())
                    {
                        diagExtend.LeaveDays = ((leaveEnd - leaveStart).Days + 2).ToString();
                    }
                    if (leaveStart.ToLongDateString() == leaveEnd.ToLongDateString())
                    {
                        diagExtend.LeaveDays = "1";
                    }
                   
                    diagExtend.LeaveStart = leaveStart.ToString();
                    diagExtend.LeaveEnd = leaveEnd.ToString();
                    diagExtend.LeavePrintDate = this.dbManager.GetSysDateTime();
                    diagExtend.LeaveType = leaveType;

                    if (this.diagExtMgr.UpdateDiagExtend(diagExtend) < 0)
                    {
                        MessageBox.Show("更新门诊病假建议书信息失败！");
                        return;
                    }
                }
                #endregion

                #region 有诊断证明记录且存在病假建议信息记录，更新旧病假信息为无效状态，同时插入新记录

                else
                {

                    leaveNo = diagExtend.LeaveNo;
                    diagExtend.ClinicCode = this.regObj.ID;
                    diagExtend.CardNo = this.regObj.PID.CardNO;
                    diagExtend.LeaveNo = leaveNo;
                    diagExtend.LeaveDays = diagExtend.LeaveDays;
                    diagExtend.LeaveStart = diagExtend.LeaveStart;
                    diagExtend.LeaveEnd = diagExtend.LeaveEnd;
                    diagExtend.ValidFlag = "0";
                    diagExtend.ProvePrintDate = diagExtend.ProvePrintDate;
                    diagExtend.LeavePrintDate = diagExtend.LeavePrintDate;
                    diagExtend.CaseMain = diagExtend.CaseMain;
                    diagExtend.CaseNow = diagExtend.CaseNow;
                    diagExtend.Opinions = diagExtend.Opinions;
                    diagExtend.LeaveType = diagExtend.LeaveType;

                    if (this.diagExtMgr.UpdateDiagExtend(diagExtend) < 0)
                    {
                        MessageBox.Show("更新门诊病假建议书为无效状态失败！");
                        return;
                    }

                    string GetLeaveNoSql = "select SEQ_FIN_DIAGLEAVENO.nextval from dual";
                    DataSet ds = new DataSet();
                    dbManager.ExecQuery(GetLeaveNoSql, ref ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt != null)
                    {
                        leaveNo = dt.Rows[0]["nextval"].ToString();
                    }

                    FS.HISFC.Models.Order.OutPatient.DiagExtend DiagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
                    DiagExtend.ClinicCode = this.regObj.ID;
                    DiagExtend.CardNo = this.regObj.PID.CardNO;
                    DiagExtend.ProveNo = diagExtend.ProveNo;
                    DiagExtend.LeaveNo = leaveNo;
                    DiagExtend.LeaveDays = ((leaveEnd - leaveStart).Days + 1).ToString();
                    if (leaveStart.ToShortDateString() == System.DateTime.Now.ToShortDateString())
                    {
                        DiagExtend.LeaveDays = ((leaveEnd - leaveStart).Days + 2).ToString();
                    }
                    if (leaveStart.ToLongDateString() == leaveEnd.ToLongDateString())
                    {
                        DiagExtend.LeaveDays = "1";
                    }
                    DiagExtend.LeaveStart = leaveStart.ToString();
                    DiagExtend.LeaveEnd = leaveEnd.ToString();
                    DiagExtend.ValidFlag = "1";
                    DiagExtend.LeavePrintDate = this.dbManager.GetSysDateTime();
                    DiagExtend.ProvePrintDate = diagExtend.ProvePrintDate;
                    DiagExtend.CaseMain = diagExtend.CaseMain;
                    DiagExtend.CaseNow = diagExtend.CaseNow;
                    DiagExtend.Opinions = diagExtend.Opinions;
                    DiagExtend.LeaveType = leaveType;

                    if (this.diagExtMgr.InsertDiagExtend(DiagExtend) < 0)
                    {
                        MessageBox.Show("保存新门诊病假建议书失败！");
                        return;
                    }
                }
                #endregion

            }
            else
            {
                #region 不存在有效的诊断证明和病假建议记录，新插入

                string GetLeaveNoSql = "select SEQ_FIN_DIAGLEAVENO.nextval from dual";
                DataSet ds = new DataSet();
                dbManager.ExecQuery(GetLeaveNoSql, ref ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt != null)
                {
                    leaveNo = dt.Rows[0]["nextval"].ToString();
                }

                FS.HISFC.Models.Order.OutPatient.DiagExtend DiagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
                DiagExtend.ClinicCode = this.regObj.ID;
                DiagExtend.CardNo = this.regObj.PID.CardNO;
                DiagExtend.LeaveNo = leaveNo;
                DiagExtend.LeaveDays = ((leaveEnd - leaveStart).Days + 1).ToString();
                if (leaveStart.ToShortDateString() == System.DateTime.Now.ToShortDateString())
                {
                    DiagExtend.LeaveDays = ((leaveEnd - leaveStart).Days + 2).ToString();
                }
                if (leaveStart.ToLongDateString() == leaveEnd.ToLongDateString())
                {
                    DiagExtend.LeaveDays = "1";
                }
                DiagExtend.LeaveStart = leaveStart.ToString();
                DiagExtend.LeaveEnd = leaveEnd.ToString();
                DiagExtend.ValidFlag = "1";
                DiagExtend.LeavePrintDate = this.dbManager.GetSysDateTime();

                if (this.diagExtMgr.InsertDiagExtend(DiagExtend) < 0)
                {
                    MessageBox.Show("保存门诊病假建议书失败！");
                    return;
                }
                #endregion
            }

            FS.HISFC.BizProcess.Interface.Order.ILeavePrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase), typeof(FS.HISFC.BizProcess.Interface.Order.ILeavePrint), 4) as FS.HISFC.BizProcess.Interface.Order.ILeavePrint;
            if (o == null)
            {
                return;
            }
            else 
            {
                o.RegId = this.regObj.ID;
                bool isPrint = o.IsPrint;
                if (isPrint)
                {
                    o.Print();
                }
            }

        }
        #endregion 

    }

}
