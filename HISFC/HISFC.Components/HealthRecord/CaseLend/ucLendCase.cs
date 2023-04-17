using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    /// <summary>
    /// 病案借阅 2011-8-4
    /// </summary>
    public partial class ucLendCase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 病案借阅构造函数
        /// </summary>
        public ucLendCase()
        {
            InitializeComponent();
        }
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();   //部门业务类
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();//常数业务类
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();//人员业务类
        FS.HISFC.BizProcess.Integrate.RADT radtMana = new FS.HISFC.BizProcess.Integrate.RADT();//住院患者业务类
        FS.HISFC.BizLogic.HealthRecord.CaseCard cardMgr = new FS.HISFC.BizLogic.HealthRecord.CaseCard();//借阅业务类
  

        FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe caseStoreMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe();//病案库房业务类
     
        List<FS.HISFC.Models.HealthRecord.Lend> LendList = null;//借出数组 用于打印赋值
        private bool isUserInterFace = false;//是否使用广东省病案接口  2011-6-22
        private bool isComPaitentinfo = false;// com_patientinfo 中获取的数据
        private ArrayList Caselist = null;
        /// <summary>
        /// 是否使用广东省病案接口
        /// </summary>
        [Category("是否使用广东省病案接口"), Description("从广东省病案系统获取数据")]
        public bool IsUserInterFace
        {
            get { return this.isUserInterFace; }
            set { this.isUserInterFace = value; }
        }


        private bool isUserCaseStore = false;

        /// <summary>
        /// 是否使用库房管理
        /// </summary>
        [Category("是否使用库房管理"), Description("是借出默认出库，归还默认入库")]
        public bool IsUserCaseStore
        {
            get { return this.isUserCaseStore; }
            set { this.isUserCaseStore = value; }
        }

        /// <summary>
        /// 是否电子病历借阅 医生使用的申请功能 2011-8-10 by chengym
        /// </summary>
        private bool isElectronCase = false;

        /// <summary>
        /// 是否电子病历借阅属性
        /// </summary>
        [Category("是否电子病历借阅申请"), Description("处理电子病历借阅申请")]
        public bool IsElectronCase
        {
            get { return this.isElectronCase; }
            set { this.isElectronCase = value; }
        }
        /// <summary>
        /// 是否需要医务科审核 医生使用的申请功能 2011-8-10 by chengym
        /// </summary>
        private bool isVerify = false;

        /// <summary>
        /// 是否需要医务科审核属性
        /// </summary>
        [Category("是否需要医务科审核"), Description("处理电子病历借阅申请")]
        public bool IsVerify
        {
            get { return this.isVerify; }
            set { this.isVerify = value; }
        }

        /// <summary>
        /// load 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.init();
            base.OnLoad(e);
        }
        /// <summary>
        ///  初始化 
        /// </summary>
        private void init()
        {
            //科室
            ArrayList deptAl = this.deptMgr.GetDeptmentAll();
            this.cmbDept.AddItems(deptAl);
            //人员
            ArrayList personAl = this.personMgr.GetUserEmployeeAll();
            this.cmbPerson.AddItems(personAl);
            this.cmbOper.AddItems(personAl);
            //借阅用途
            ArrayList LendTypeAl = this.conMgr.GetAllList("CASE_LEND_TYPE");
            this.cmbLendType.AppendItems(LendTypeAl);

            this.cmbOper.Tag = this.deptMgr.Operator.ID;
            this.dtLendDate.Value = this.deptMgr.GetDateTimeFromSysDateTime();
            this.caseDetail.RowCount = 0;
            this.txtLendTot.Text = "0";

            //电子病历借阅申请
            if (this.isElectronCase)
            {
                this.linkLabel1.Visible = false;
                this.txtCaseNO.Visible = false;
                label12.Visible = false;
                this.txtMemo.Visible = false;
                this.caseDetail.Columns.Get(13).Label = "住院流水号";
                this.cmbLendType.SelectedIndex = 0;
                this.cmbPerson.Tag=this.deptMgr.Operator.ID;
            }
            else
            {
                this.caseDetail.Columns[3].Visible = false;
                this.caseDetail.Columns[4].Visible = false;
                this.caseDetail.Columns[5].Visible = false;
                this.caseDetail.Columns[6].Visible = false;
                this.ucQueryInpatientNo1.Visible = false;
            }

        }
        /// <summary>
        /// 清空控件
        /// </summary>
        private void Clear()
        {
            this.cmbPerson.Tag = "";
            this.cmbPerson.Text = "";
            this.cmbDept.Tag = "";
            this.cmbDept.Text = "";
            this.cmbLendType.Tag = "";
            this.cmbLendType.Text = "";
            this.txtLendTot.Text = "";
            this.txtNeedBack.Text = "0";
            this.cmbOper.Tag = this.deptMgr.Operator.ID;
            this.txtName.Text = "";
            this.txtSex.Text = "";
            this.txtIntimes.Text = "";
            this.caseDetail.RowCount = 0;

        }

        private void txtCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.linkLabel1.Text == "病案号")
                {
                    string paitenNO = string.Empty;//住院号
                    int inTimes = 1;//住院次数

                    string caseNo = ""; //输入值
                    caseNo = this.txtCaseNO.Text;
                    //add by chengym 东莞特殊处理
                    if (caseNo.IndexOf('A') >= 0 || caseNo.IndexOf('B') >= 0 || caseNo.IndexOf('C') >= 0 || caseNo.IndexOf('D') >= 0 || caseNo.IndexOf('E') >= 0)
                    {
                        caseNo = caseNo.Replace('A', '0');
                        caseNo = caseNo.Replace('B', '0');
                        caseNo = caseNo.Replace('C', '0');
                        caseNo = caseNo.Replace('D', '0');
                        caseNo = caseNo.Replace('E', '0');
                        caseNo = caseNo.TrimStart('0').PadLeft(6, '0') + "-" + "1";
                    }
                    //end
                    caseNo = caseNo.Replace('—', '-');
                    if (caseNo.IndexOf('-') > 0)
                    {
                        string[] CaseNO = caseNo.Split('-');
                        this.txtCaseNO.Text = CaseNO[0].ToString().TrimStart('0').PadLeft(6, '0');
                        this.txtIntimes.Text = CaseNO[1].ToString().Trim();

                        paitenNO = CaseNO[0].ToString().TrimStart('0').PadLeft(6, '0');
                        inTimes = FS.FrameWork.Function.NConvert.ToInt32(CaseNO[1].ToString().Trim());
                    }
                    else
                    {
                        paitenNO = caseNo;//不带-次数情况
                    }
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    if (this.isUserInterFace == true)
                    {
                        FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMgr = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();//广东省病案接口
                        
                        patientInfo = uploadMgr.GetPatientByIdAndTimes(paitenNO, inTimes);
                        if (patientInfo == null || patientInfo.ID == "")
                        {
                            MessageBox.Show("广东省病案接口中，未找到患者信息!", "提示");
                            this.txtMemo.Text = "";
                            this.txtCaseNO.Text = "";
                            this.txtCaseNO.Focus();
                            return;
                        }
                        this.txtName.Text = patientInfo.Name;
                    }
                    else
                    {

                        paitenNO = "T" + paitenNO.TrimStart('0').PadLeft(9, '0');  //卡号：“T+九位”情况
                        patientInfo = this.radtMana.QueryComPatientInfo(paitenNO);//查com_patientinfo（卡号：T + 住院号（用0补齐9位）） 

                        if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                        {
                            paitenNO = paitenNO.Replace('T', '0').PadLeft(10, '0');
                            patientInfo = this.radtMana.QueryComPatientInfo(paitenNO);//查com_patientinfo 卡号：住院号（用0补齐10位）
                            if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                            {
                                FS.HISFC.BizLogic.HealthRecord.Case.CaseLend caseLendLogic = new FS.HISFC.BizLogic.HealthRecord.Case.CaseLend();
                                patientInfo = caseLendLogic.QueryComPatientInfo(paitenNO);
                                if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                                {
                                    MessageBox.Show("com_patientinfo未找到患者信息!" + radtMana.Err);
                                    this.txtMemo.Text = "";
                                    this.txtCaseNO.Text = "";
                                    this.txtCaseNO.Focus();
                                    return;
                                }
                            }
                        }
                        if (patientInfo.InTimes == 0)
                        {
                            patientInfo.InTimes = inTimes;
                            this.txtIntimes.Text = inTimes.ToString();
                        }
                        this.txtName.Text = patientInfo.Name;
                        this.isComPaitentinfo = true;
                       
                    }

                    this.SetFarpiont(patientInfo);
                }
                else//姓名
                {

                }
                this.label13.Text = this.txtCaseNO.Text;
                this.txtMemo.Text = "";
                this.txtCaseNO.Text = "";
                this.txtCaseNO.Focus();
            }
        }
        /// <summary>
        /// 显示借阅数据
        /// </summary>
        /// <param name="patientInfo"></param>
        private void SetFarpiont(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (this.isElectronCase)//医生电子病历借阅申请
            {
                for (int i = 0; i < this.caseDetail.RowCount; i++) //仅仅判断借出是否重复 不需要判断是否借出 
                {
                    if (this.caseDetail.Cells[i, 13].Text == patientInfo.ID)
                    {
                        int j = i + 1;
                        MessageBox.Show("第" + j.ToString() + "已经存在相同借阅记录", "提示！");
                        this.txtCaseNO.SelectAll();
                        this.txtCaseNO.Focus();
                        return;
                    }
                }
                this.caseDetail.Rows.Add(this.caseDetail.RowCount, 1);
                int row = this.caseDetail.RowCount - 1;

                this.caseDetail.Cells[row, 0].Text = patientInfo.PID.PatientNO;
                this.caseDetail.Cells[row, 1].Text = patientInfo.InTimes.ToString();
                this.caseDetail.Cells[row, 2].Text = patientInfo.Name;

                this.caseDetail.Cells[row, 3].Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                this.caseDetail.Cells[row, 4].Text = patientInfo.PVisit.InTime.ToString();
                this.caseDetail.Cells[row, 5].Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                this.caseDetail.Cells[row, 6].Text = patientInfo.PVisit.OutTime.ToString();
                if (patientInfo.Sex.ID.ToString() == "M" || patientInfo.Sex.ID.ToString() == "1")
                {
                    this.caseDetail.Cells[row, 7].Text = "男";
                }
                else
                {
                    this.caseDetail.Cells[row, 7].Text = "女";
                }
                this.caseDetail.Cells[row, 8].Text = this.conMgr.GetAge(patientInfo.Birthday, patientInfo.PVisit.InTime);
                this.caseDetail.Cells[row, 9].Text = this.cmbLendType.Tag.ToString();//借阅用途
                this.caseDetail.Cells[row, 10].Text = this.cmbLendType.Text.ToString();//借阅用途名称
                this.caseDetail.Cells[row, 11].Text = patientInfo.PVisit.PatientLocation.Dept.ID;//出院科室代码  
                this.caseDetail.Cells[row, 12].Text = patientInfo.PVisit.PatientLocation.Dept.ID;//入院科室代码
                this.caseDetail.Cells[row, 13].Text = patientInfo.ID;//住院流水号
                this.caseDetail.Rows[row].Tag = patientInfo;
                this.txtLendTot.Text = this.caseDetail.RowCount.ToString();
            }
            else
            {
                if (this.isComPaitentinfo)//com_patientinfo的数据
                {
                    #region patientinfo类型
                    string patientNo = patientInfo.PID.CardNO.Replace('T', '0');//去掉 卡号中的“T”
                    for (int i = 0; i < this.caseDetail.RowCount; i++) //仅仅判断借出是否重复
                    {
                        if (this.caseDetail.Cells[i, 0].Text == patientNo)
                        {
                            if (this.caseDetail.Cells[i, 1].Text == patientInfo.InTimes.ToString())
                            {
                                int j = i + 1;
                                MessageBox.Show("第" + j.ToString() + "已经存在相同借阅记录", "提示！");
                                this.txtCaseNO.Focus();
                                return;
                            }
                        }
                    }
                    this.caseDetail.Rows.Add(this.caseDetail.RowCount, 1);
                    int row = this.caseDetail.RowCount - 1;

                    this.caseDetail.Cells[row, 0].Text = patientNo;//住院号
                    this.caseDetail.Cells[row, 1].Text = patientInfo.InTimes.ToString();//住院次数
                    this.caseDetail.Cells[row, 2].Text = patientInfo.Name;//患者姓名
                    this.caseDetail.Cells[row, 3].Text = "";//入院科室
                    this.caseDetail.Cells[row, 4].Text = "";//入院日期
                    this.caseDetail.Cells[row, 5].Text = "";//出院科室
                    this.caseDetail.Cells[row, 6].Text = "";//出院日期

                    if (patientInfo.Sex.ID.ToString() == "M" || patientInfo.Sex.ID.ToString() == "1")
                    {
                        this.caseDetail.Cells[row, 7].Text = "男";
                    }
                    else
                    {
                        this.caseDetail.Cells[row, 7].Text = "女";
                    }
                    this.caseDetail.Cells[row, 8].Text = this.conMgr.GetAge(patientInfo.Birthday);//出生日期
                    this.caseDetail.Cells[row, 9].Text = this.cmbLendType.Tag.ToString();//借阅用途
                    this.caseDetail.Cells[row, 10].Text = this.cmbLendType.Text.ToString();//借阅用途名称
                    this.caseDetail.Cells[row, 11].Text = patientInfo.ID; //住院流水号
                    this.caseDetail.Cells[row, 12].Text = "";//入院科室代码

                    this.caseDetail.Rows[row].Tag = patientInfo;
                    this.caseDetail.Rows[row].BackColor = System.Drawing.Color.PeachPuff;
                    this.txtLendTot.Text = this.caseDetail.RowCount.ToString();
                    #endregion
                }
                else
                {
                    for (int i = 0; i < this.caseDetail.RowCount; i++) //仅仅判断借出是否重复 不需要判断是否借出 
                    {
                        if (this.caseDetail.Cells[i, 0].Text == patientInfo.ID && this.caseDetail.Cells[i, 1].Text == patientInfo.InTimes.ToString())
                        {
                            int j = i + 1;
                            MessageBox.Show("第" + j.ToString() + "已经存在相同借阅记录", "提示！");
                            this.txtCaseNO.SelectAll();
                            this.txtCaseNO.Focus();
                            return;
                        }
                    }
                    this.caseDetail.Rows.Add(this.caseDetail.RowCount, 1);
                    int row = this.caseDetail.RowCount - 1;

                    this.caseDetail.Cells[row, 0].Text = patientInfo.PID.PatientNO;
                    this.caseDetail.Cells[row, 1].Text = patientInfo.InTimes.ToString();
                    this.caseDetail.Cells[row, 2].Text = patientInfo.Name;

                    this.caseDetail.Cells[row, 3].Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                    this.caseDetail.Cells[row, 4].Text = patientInfo.PVisit.InTime.ToString();
                    this.caseDetail.Cells[row, 5].Text = patientInfo.PVisit.PatientLocation.Name;
                    this.caseDetail.Cells[row, 6].Text = patientInfo.PVisit.OutTime.ToString();

                    this.caseDetail.Cells[row, 7].Text = patientInfo.Sex.Name;

                    this.caseDetail.Cells[row, 8].Text = this.conMgr.GetAge(patientInfo.Birthday, patientInfo.PVisit.InTime);
                    this.caseDetail.Cells[row, 9].Text = this.cmbLendType.Tag.ToString();//借阅用途
                    this.caseDetail.Cells[row, 10].Text = this.cmbLendType.Text.ToString();//借阅用途名称
                    this.caseDetail.Cells[row, 11].Text = patientInfo.PVisit.PatientLocation.ID;//出院科室代码--注意：广东省的字典库代码  应该用不到的
                    this.caseDetail.Cells[row, 12].Text = patientInfo.PVisit.PatientLocation.Dept.ID;//入院科室代码--注意：广东省的字典库代码  应该用不到的
                    this.caseDetail.Cells[row, 13].Text = this.txtMemo.Text.Trim();//份数备注
                    this.caseDetail.Rows[row].Tag = patientInfo;
                    this.txtLendTot.Text = this.caseDetail.RowCount.ToString();
                }
                this.txtCaseNO.SelectAll();
                this.txtCaseNO.Focus();
                this.isComPaitentinfo = false;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        //private FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
        private List<FS.HISFC.Models.HealthRecord.Lend> GetLendInfo()
        {
            List<FS.HISFC.Models.HealthRecord.Lend> list = new List<FS.HISFC.Models.HealthRecord.Lend>();
            if (this.caseDetail.Rows.Count == 0)
            {
                MessageBox.Show("请输入借阅的病案明细信息");
                return list;
            }
            string CaseNum = this.cardMgr.GetSequence("Case.CaseCard.LendCase.Seq");//先获取一个序列,记录同一次的借阅记录

            if (CaseNum == null || CaseNum == "")
            {
                MessageBox.Show("获取序号失败");
                return null;
            }
            if (this.isElectronCase)
            {
                #region 医生电子病历借阅申请
                for (int i = 0; i < this.caseDetail.Rows.Count; i++)
                {
                    FS.HISFC.Models.HealthRecord.Lend Saveinfo = null;

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.caseDetail.Rows[i].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    Saveinfo = new FS.HISFC.Models.HealthRecord.Lend();
                    Saveinfo.SeqNO = this.cardMgr.GetSequence("Case.CaseCard.LendCase.Seq");//主键序列
                    if (Saveinfo.SeqNO == null || Saveinfo.SeqNO == "")
                    {
                        MessageBox.Show("获取序号失败");
                        return null;
                    }
                    // 住院流水号 
                    Saveinfo.CaseBase.PatientInfo.ID = patientInfo.ID;
                    //病人住院号 
                    Saveinfo.CaseBase.CaseNO = patientInfo.PID.PatientNO;
                    Saveinfo.CaseBase.PatientInfo.Name = patientInfo.Name; //病人姓名
                    if (patientInfo.Sex.Name == "男")
                    {
                        Saveinfo.CaseBase.PatientInfo.Sex.ID = "M";//性别
                    }
                    else
                    {
                        Saveinfo.CaseBase.PatientInfo.Sex.ID = "F";//性别
                    }

                    Saveinfo.CaseBase.PatientInfo.Birthday = patientInfo.Birthday;//出生日期
                    Saveinfo.CaseBase.PatientInfo.PVisit.InTime = patientInfo.PVisit.InTime;//入院日期
                    Saveinfo.CaseBase.PatientInfo.PVisit.OutTime = patientInfo.PVisit.OutTime;//出院日期
                    Saveinfo.CaseBase.InDept.ID = patientInfo.PVisit.PatientLocation.Dept.ID; //入院科室代码
                    Saveinfo.CaseBase.InDept.Name = patientInfo.PVisit.PatientLocation.Dept.Name; //入院科室名称
                    Saveinfo.CaseBase.OutDept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;  //出院科室代码
                    Saveinfo.CaseBase.OutDept.Name = patientInfo.PVisit.PatientLocation.Dept.Name; //出院科室名称
                    Saveinfo.EmployeeInfo.ID = this.cmbPerson.Tag.ToString();//借阅人代号
                    Saveinfo.EmployeeInfo.Name = this.cmbPerson.Text.Trim();//借阅人姓名
                    Saveinfo.EmployeeDept.ID = this.cmbDept.Tag.ToString(); //借阅人所在科室代码
                    Saveinfo.EmployeeDept.Name = this.cmbDept.Text.Trim(); //借阅人所在科室名称
                    Saveinfo.LendDate = this.dtLendDate.Value; //借阅日期
                    string d = this.conMgr.GetConstant("CASE_LEND_TYPE", this.cmbLendType.Tag.ToString()).Memo;//根据常数获取“借阅期限”
                    int days = FS.FrameWork.Function.NConvert.ToInt32(d);
                    Saveinfo.PrerDate = this.dtLendDate.Value.AddDays(days); //预定还期
                    Saveinfo.LendKind = this.cmbLendType.Tag.ToString(); //借阅用途
                    //需要判断是否需要医务科审核  根据设定条件（暂时不清楚，只是听说过有这样的需求，先保留）

                    if (this.isVerify)
                    {
                        int lendStus = (int)FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.MedicalMattersVerify;
                        Saveinfo.LendStus = lendStus.ToString(); ;//病历状态 1借出/2返还/3医生电子病历借阅申请医务科审核/4医生电子病历借阅申请病案室审核
                    }
                    else
                    {
                        int lendStus = (int)FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.HealthRecordVerify;
                        Saveinfo.LendStus = lendStus.ToString(); ;//病历状态 1借出/2返还/3医生电子病历借阅申请医务科审核/4医生电子病历借阅申请病案室审核
          
                    }
                    Saveinfo.ID = cardMgr.Operator.ID; //操作员代号
                    Saveinfo.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(cardMgr.GetSysDate()); //操作时间
                    Saveinfo.ReturnOperInfo.ID = "";   //归还操作员代号
                    Saveinfo.ReturnDate = FS.FrameWork.Function.NConvert.ToDateTime("1900-1-1");   //实际归还日期 默认一个不达到日期

                    Saveinfo.CardNO = CaseNum;//借阅卡号  存某次的标记号
                    Saveinfo.Memo = "完好";//返还情况
                    Saveinfo.LendNum = "1";//份数
                    Saveinfo.CaseBase.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.caseDetail.Cells[i, 1].Text.Trim());//住院次数  
                    list.Add(Saveinfo);
                }
                #endregion
            }
            else
            {
                #region 住院病案借阅
                for (int i = 0; i < this.caseDetail.Rows.Count; i++)
                {
                    FS.HISFC.Models.HealthRecord.Lend Saveinfo = null;

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.caseDetail.Rows[i].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    FS.HISFC.Models.HealthRecord.Base objBase = this.ChangeBase(patientInfo);
                    //FS.HISFC.Models.HealthRecord.Base objBase1 = (FS.HISFC.Models.HealthRecord.Base)this.caseDetail.Rows[i].Tag; ;
                    //ArrayList tempList = this.baseDml.QueryCaseBaseInfoByCaseNO(objBase.CaseNO);
                    //foreach (FS.HISFC.Models.HealthRecord.Base tempObj in tempList)
                    //{

                        Saveinfo = new FS.HISFC.Models.HealthRecord.Lend();
                        Saveinfo.SeqNO = this.cardMgr.GetSequence("Case.CaseCard.LendCase.Seq");//主键序列
                        if (Saveinfo.SeqNO == null || Saveinfo.SeqNO == "")
                        {
                            MessageBox.Show("获取序号失败");
                            return null;
                        }
                        // 住院流水号 com_patientinfo 数据存卡号
                        if (patientInfo.ID == "" || patientInfo.ID == null)
                        {
                            Saveinfo.CaseBase.PatientInfo.ID = patientInfo.PID.CardNO;
                        }
                        else
                        {
                            Saveinfo.CaseBase.PatientInfo.ID = patientInfo.ID;
                        }
                        //病人住院号 
                        if (patientInfo.PID.PatientNO == "" || patientInfo.PID.PatientNO == null)
                        {
                            Saveinfo.CaseBase.CaseNO = this.caseDetail.Cells[i, 0].Text.Trim();
                        }
                        else
                        {
                            Saveinfo.CaseBase.CaseNO = patientInfo.PID.PatientNO;
                        }
                        //Saveinfo.CaseBase.CaseNO = tempObj.CaseNO;
                        //Saveinfo.CaseBase.PatientInfo.ID = tempObj.PatientInfo.ID;//住院流水号
                        //Saveinfo.CaseBase.CaseNO = tempObj.CaseNO;//病人住院号 
                        Saveinfo.CaseBase.PatientInfo.Name = patientInfo.Name; //病人姓名
                        if (patientInfo.Sex.Name == "男")
                        {
                            Saveinfo.CaseBase.PatientInfo.Sex.ID = "M";//性别
                        }
                        else
                        {
                            Saveinfo.CaseBase.PatientInfo.Sex.ID = "F";//性别
                        }
                        Saveinfo.CaseBase.PatientInfo.Sex.ID = patientInfo.Sex.ID;// tempObj.PatientInfo.Sex.ID;//性别
                        Saveinfo.CaseBase.PatientInfo.Birthday = patientInfo.Birthday;//出生日期
                        Saveinfo.CaseBase.PatientInfo.PVisit.InTime = patientInfo.PVisit.InTime;//入院日期
                        Saveinfo.CaseBase.PatientInfo.PVisit.OutTime = patientInfo.PVisit.OutTime;//出院日期
                        Saveinfo.CaseBase.InDept.ID = patientInfo.PVisit.PatientLocation.Dept.ID; //入院科室代码
                        Saveinfo.CaseBase.InDept.Name = patientInfo.PVisit.PatientLocation.Dept.Name; //入院科室名称
                        Saveinfo.CaseBase.OutDept.ID = patientInfo.PVisit.PatientLocation.ID;  //出院科室代码
                        Saveinfo.CaseBase.OutDept.Name =patientInfo.PVisit.PatientLocation.Name; //出院科室名称
                        Saveinfo.EmployeeInfo.ID = this.cmbPerson.Tag.ToString();//借阅人代号
                        Saveinfo.EmployeeInfo.Name = this.cmbPerson.Text.Trim();//借阅人姓名
                        Saveinfo.EmployeeDept.ID = this.cmbDept.Tag.ToString(); //借阅人所在科室代码
                        Saveinfo.EmployeeDept.Name = this.cmbDept.Text.Trim(); //借阅人所在科室名称
                        Saveinfo.LendDate = this.dtLendDate.Value; //借阅日期
                        string d = this.conMgr.GetConstant("CASE_LEND_TYPE", this.cmbLendType.Tag.ToString()).Memo;//根据常数获取“借阅期限”
                        int days = FS.FrameWork.Function.NConvert.ToInt32(d);
                        Saveinfo.PrerDate = this.dtLendDate.Value.AddDays(days); //预定还期
                        Saveinfo.LendKind = this.cmbLendType.Tag.ToString(); //借阅用途
                        Saveinfo.LendStus = "1"; ;//病历状态 1借出/2返还
                        Saveinfo.ID = cardMgr.Operator.ID; //操作员代号
                        Saveinfo.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(cardMgr.GetSysDate()); //操作时间
                        Saveinfo.ReturnOperInfo.ID = "";   //归还操作员代号
                        Saveinfo.ReturnDate = FS.FrameWork.Function.NConvert.ToDateTime("1900-1-1");   //实际归还日期 默认一个不达到日期

                        Saveinfo.CardNO = CaseNum;//借阅卡号  存某次的标记号
                        Saveinfo.Memo = "完好";//返还情况

                        Saveinfo.LendNum = this.caseDetail.Cells[i, 13].Text.Trim();//份数
                        if (Saveinfo.LendNum == "")
                        {
                            Saveinfo.LendNum = "1";
                        }
                        Saveinfo.CaseBase.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.caseDetail.Cells[i, 1].Text.Trim());//住院次数  

                        list.Add(Saveinfo);
       
                }  
                #endregion
            }
            return list;
        }
        private void Save()
        {
            if (this.Vaild() == false)
            {
                return;
            }
            List<FS.HISFC.Models.HealthRecord.Lend> list = this.GetLendInfo();
            if (list == null || list.Count == 0)
            {
                return;
            }
            this.LendList = list;//用于打印赋值；
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            cardMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            caseStoreMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            foreach (FS.HISFC.Models.HealthRecord.Lend obj in list)
            {
                if (obj == null)
                {
                    return;
                }
                if (cardMgr.LendCase(obj) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入借阅记录失败: " + cardMgr.Err);
                    return;
                }
                #region 若使用库房管理  借阅时同时做出库操作
                if (this.isUserCaseStore && this.isElectronCase==false)
                {
                    FS.HISFC.Models.HealthRecord.Case.CaseStore info = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                    info.PatientInfo.PID.PatientNO = obj.CaseBase.CaseNO;//住院号
                    info.PatientInfo.Name = obj.CaseBase.PatientInfo.Name;
                    info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(obj.CaseBase.PatientInfo.PID.CardNO);//次数
                    FS.HISFC.Components.HealthRecord.Case.functionStore fun = new FS.HISFC.Components.HealthRecord.Case.functionStore();
                    info.Store.ID = fun.GetCaseStore(info.PatientInfo.PID.PatientNO);
                    info.Cabinet.ID = fun.GetCabinet(info.PatientInfo.PID.PatientNO);
                    info.Grid.ID = "";
                    info.CaseState = "5";
                    info.IsVaild = true;
                    info.CaseMemo = "";
                    info.OperEnv.ID = obj.ID;
                    info.OperEnv.OperTime = obj.OperInfo.OperTime;
                    info.Extend1 = "";
                    info.Extend2 = "";
                    info.Extend3 = "";
                    info.Extend4 = "";
                    if (this.caseStoreMgr.InsertCaseStore(info) < 0)
                    {
                        if (this.caseStoreMgr.UpdateCaseStore(info) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存病案库房信息失败！", "提示");
                            return;
                        }
                    }
                    string CaseNum = this.caseStoreMgr.GetSequence("HealthReacord.Case.CaseStore.Seq");
                    if (this.caseStoreMgr.InsertCaseStoreDetail(info, CaseNum) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存病案库房明细信息失败！", "提示");
                        return;
                    }
                }
                #endregion
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            if (this.isElectronCase)
            {
                MessageBox.Show("借阅申请成功！");
            }
            else
            {
                MessageBox.Show("借阅成功");
                this.print();
            }
            this.Clear();
        }
        /// <summary>
        /// 保存获取实体数据前 判断必填项是否空缺
        /// </summary>
        /// <returns></returns>
        private bool Vaild()
        {
            if (this.cmbPerson.Tag == null || this.cmbPerson.Tag.ToString() == "")
            {
                MessageBox.Show("请选择借阅人");
                this.cmbPerson.Focus();
                return false;
            }
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
            {
                MessageBox.Show("请选择借阅人所在科室");
                this.cmbDept.Focus();
                return false;
            }
            if (this.cmbLendType.Tag == null || this.cmbLendType.Tag.ToString() == "")
            {
                MessageBox.Show("请选择借阅用途");
                this.cmbLendType.Focus();
                return false;
            }
            if (this.dtLendDate.Value < this.cardMgr.GetDateTimeFromSysDateTime().AddDays(-1))
            {
                MessageBox.Show("借阅日期非当前时间？");
                this.dtLendDate.Focus();
                return false;
            }
            return true;
        }
    
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //this.print();
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// 打印借阅卡
        /// </summary>
        private void print()
        {
            FS.HISFC.Components.HealthRecord.CaseLend.ucLendCasePrint ucPrint = new ucLendCasePrint();
            ucPrint.LeadCaseList = this.LendList;
            ucPrint.Print();
            this.LendList = null;
            this.Clear();
        }

        /// <summary>
        /// 选择借阅用途跳转到借阅人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbLendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbPerson.Focus();
        }
        /// <summary>
        /// 选择借阅人 带出借阅人所在科室和跳转到病案号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPerson.Tag != null && cmbPerson.Tag.ToString() != "")
            {
                this.cmbDept.Tag = "";
                this.cmbDept.Text = "";
                FS.HISFC.Models.Base.Employee personObj = new FS.HISFC.Models.Base.Employee();
                personObj = this.personMgr.GetPersonByID(this.cmbPerson.Tag.ToString());


                if (personObj.Dept.ID != "")
                {
                    this.cmbDept.Tag = personObj.Dept.ID;
                }
                else if (personObj.Nurse.ID != "")
                {
                    this.cmbDept.Tag = personObj.Nurse.ID;
                }
                string whereSql = " WHERE EMPL_CODE='" + this.cmbPerson.Tag.ToString() + "'  AND LEN_STUS='1'";

                ArrayList LendTotAl = this.cardMgr.QueryLendInfoSetWhere(whereSql);//未来可以做一个可以查询明细的按钮
                if (LendTotAl == null)
                {
                    this.txtNeedBack.Text = "0";
                }
                else
                {
                    this.txtNeedBack.Text = LendTotAl.Count.ToString();
                }
            }
            this.txtCaseNO.Text = "";
            this.txtCaseNO.Focus();
        }
        /// <summary>
        /// 备注借阅份数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMemo_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.caseDetail.RowCount; i++)
            {
                if (this.caseDetail.Cells[i, 0].Text.PadLeft(10, '0') == this.label13.Text.PadLeft(10, '0'))
                {
                    if (this.caseDetail.Cells[i, 1].Text.Trim() == this.txtIntimes.Text.Trim())
                    {
                        this.caseDetail.Cells[i, 13].Text = this.txtMemo.Text.Trim();
                    }
                }
            }
        }
        /// <summary>
        /// 加载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            try
            {
                toolBarService.AddToolButton("清空", "清空", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
                toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误信息");
            }
            return toolBarService;
        }
        /// <summary>
        /// 工具栏单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清空":
                    this.Clear();
                    break;
                case "删除":
                    if (this.caseDetail.Cells[this.caseDetail.ActiveRowIndex, 0].Text != "")
                    {
                        this.caseDetail.ActiveRow.Remove();
                    }
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    this.ucQueryInpatientNo1.Err = "此患者不在院";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            patientInfo = this.radtMana.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            this.txtIntimes.Text = patientInfo.InTimes.ToString();
            this.SetFarpiont(patientInfo);
        }



        private FS.HISFC.Models.HealthRecord.Base ChangeBase(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 实体转化（病人FS.HISFC.Models.RADT.PatientInfo－->病案FS.HISFC.Models.HealthRecord.Base）
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            info.PatientInfo.PID.PatientNO = patientInfo.PID.PatientNO;
            info.PatientInfo.InTimes = patientInfo.InTimes;
            info.PatientInfo.Name = patientInfo.Name;

            info.PatientInfo.PVisit.PatientLocation.Dept.Name = patientInfo.PVisit.PatientLocation.Dept.Name;
            info.PatientInfo.PVisit.InTime = patientInfo.PVisit.InTime;
            info.PatientInfo.PVisit.PatientLocation.Name = patientInfo.PVisit.PatientLocation.Name;
            info.PatientInfo.PVisit.OutTime = patientInfo.PVisit.OutTime;

            info.PatientInfo.Sex.Name = patientInfo.Sex.Name;

            info.PatientInfo.Birthday = patientInfo.Birthday;
            info.PatientInfo.PVisit.PatientLocation.ID = patientInfo.PVisit.PatientLocation.ID;//出院科室代码--注意：广东省的字典库代码  应该用不到的
            info.PatientInfo.PVisit.PatientLocation.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;//入院科室代码--注意：广东省的字典库代码  应该用不到的
    
            return info;
            #endregion
        }


 
    }
}