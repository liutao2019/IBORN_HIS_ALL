using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Account;
using System.Collections.Generic;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 退号/注销
    /// </summary>
    public partial class ucCancelRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucCancelRegister()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown  += new KeyEventHandler(fpSpread1_KeyDown);
            this.txtInvoice.KeyDown += new KeyEventHandler(txtInvoice_KeyDown);
            this.txtIDCard.KeyDown += new KeyEventHandler(txtIDCard_KeyDown);
            this.txtCardNo.KeyDown  += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread1_ButtonClicked);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.neuSpread2.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread2_ButtonClicked);

            this.Init();
        }

        #region 域
        private AppointmentService NetService = new AppointmentService();
        /// <summary>
        /// 预约订单管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Appointment appointmentMgr = new FS.HISFC.BizLogic.Registration.Appointment();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 帐户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 分诊管理
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        /// <summary>
        /// 控制管理类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// 系统参数控制
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 费用
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 支付方式
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper payWayHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 可退号天数
        /// </summary>
        private int PermitDays = 0;

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        private bool isQuitAccount = false;

        /// <summary>
        /// 是否打印退号票
        /// {B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// </summary>
        private bool isPrintBackBill = false;


        private bool isCallYBInterface = true;


        private bool isatm=false;

      
        #endregion

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        #region 属性

        private bool isCardFeeCanReturn = false;

        /// <summary>
        /// 是否打印退号票
        /// </summary>
        [Category("控件设置"), Description("工本费是否可以退费,默认false"), DefaultValue(false)]
        public bool IsCardFeeCanReturn
        {
            set
            {
                this.isCardFeeCanReturn = value;
            }
            get
            {
                return this.isCardFeeCanReturn;
            }
        }

        /// <summary>
        /// 是否打印退号票
        /// </summary>
        [Category("控件设置"), Description("是否打印退号票(未实现)"), DefaultValue(false)]
        public bool IsPrintBackBill
        {
            set
            {
                this.isPrintBackBill = value;
            }
            get
            {
                return this.isPrintBackBill;
            }
        }
        /// <summary>
        /// 是否是网上预约退号
        /// </summary>
        [Category("控件设置"), Description("是否是网上预约退号"), DefaultValue(false)]
        public bool IsNetCancle { get; set; }

        /// <summary>
        /// 是否判断权限
        /// </summary>
        [Category("控件设置"), Description("是否判断权限"), DefaultValue(true)]
        public bool IsJudePrivileged { get { return isJudePrivileged; } set { this.isJudePrivileged = value; } }

        private bool isJudePrivileged = true;
        /// <summary>
        /// 是否是网上预约退号
        /// </summary>
        [Category("控件设置"), Description("是否退网上预约挂号费"), DefaultValue(false)]
        public bool IsReturnNetFee { get; set; }
        /// <summary>
        /// 是否打印退号票
        /// </summary>
        [Category("控件设置"), Description("是否检测发票是否可以退费/废号"), DefaultValue(false)]
        public bool IsCheckInvoice
        {
            set
            {
                this.isCheckInvoic = value;
            }
            get
            {
                return this.isCheckInvoic;
            }
        }
        private bool isCheckInvoic = true;
        /// <summary>
        /// //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        [Category("控件设置"), Description("是否专窗退号"), DefaultValue(false)]
        public bool IsATM
        {
            get { return isatm; }
            set { isatm = value; }
        }

        /// <summary>
        /// //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        [Category("控件设置"), Description("帐户患者是否退帐户"), DefaultValue(false)]
        public bool IsQuitAccount
        {
            get { return isQuitAccount; }
            set { isQuitAccount = value; }
        }

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        private bool isSeeedCanCancelRegInfo = false;

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        [Category("控件设置"), Description("已看诊挂号记录是否能退号？"), DefaultValue(false)]
        public bool IsSeeedCanCancelRegInfo
        {
            get { return isSeeedCanCancelRegInfo; }
            set { isSeeedCanCancelRegInfo = value; }
        }

        private bool isMustAllReturnFee = true;
        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        [Category("控件设置"), Description("退号是否必须全退所有费用：true:必须全退？"), DefaultValue(true)]
        public bool IsMustAllReturnFee
        {
            get { return isMustAllReturnFee; }
            set { isMustAllReturnFee = value; }
        }

        /// <summary>
        /// 是否作废操作
        /// </summary>
        private bool isUseLogout = false;

        [Category("控件设置"), Description("退号是否启用作废功能：True = 是；False = 否"), DefaultValue(false)]
        public bool IsUseLogout
        {
            get
            {
                return isUseLogout;
            }
            set
            {
                isUseLogout = value;
            }
        }
        /// <summary>
        /// 挂号状态枚举与数据库定义是否一致
        /// </summary>
        private bool isEnumEqualDataBase = true;
        /// <summary>
        /// 挂号状态枚举与数据库定义是否一致
        /// </summary>
        [Category("控件设置"), Description("挂号状态枚举定义与数据库定义是否一致，默认true"), DefaultValue(true)]
        public bool IsEnumEqualDataBase
        {
            get
            {
                return this.isEnumEqualDataBase;
            }
            set
            {
                this.isEnumEqualDataBase = value;
            }
        }
        #endregion

        #region 医保接口

        /// <summary>
        /// 医保接口代理服务器
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 门诊挂号－挂号费中otherfee的意义 0:床费(广医专用) 1：病历本费 2：其他费
        /// </summary>
        string otherFeeType = string.Empty;

        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            //支付方式
            ArrayList al = constantManager.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (al == null)
            {
                MessageBox.Show("获取支付方式失败!");
            }
            else
            {
                payWayHelper.ArrayObject = al;
            }

            //门诊退号－允许退号天数
            string Days = this.ctlMgr.QueryControlerInfo("400006");

            if (Days == null || Days == "" || Days == "-1")
            {
                this.PermitDays = 1;
            }
            else
            {
                this.PermitDays = int.Parse(Days);
            }

            //门诊挂号－挂号费中otherfee的意义 0:床费(广医专用) 1：病历本费 2：其他费
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            Days = this.ctlMgr.QueryControlerInfo("400027");

            if (string.IsNullOrEmpty(Days))
            {
                Days = "2"; //默认其他费
            }

            this.isCallYBInterface = this.controlParma.GetControlParam<bool>("MZ9931",false,true);

            this.otherFeeType = Days;

            this.txtCardNo.Focus();

            return 0;
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        private void SetNameFocus()
        {
            this.txtName.SelectAll();
            this.txtName.Focus();
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        private void SetCardNoFocus()
        {
            this.txtCardNo.SelectAll();
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        private void SetInvoiceFocus()
        {
            this.txtInvoice.SelectAll();
            this.txtInvoice.Focus();
        }
        /// <summary>
        /// 清空显示信息
        /// </summary>
        private void Clear()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;

            this.lbTot.Text = "";
            this.lbReturn.Text = "";
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void ClearAll()
        {
            Clear();
            this.txtName.Text = "";
            this.txtCardNo.Text = "";
            this.txtInvoice.Text = "";
            this.txtIDCard.Text = "";
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// 根据病历号查询卡信息
        /// </summary>
        /// <param name="IDCard"></param>
        private void QueryRegisterByIDCard(string IDCard)
        {
            this.Clear();

            //检索患者有效号
            ArrayList tarlRegInfo = this.regMgr.GetByIDCard(IDCard);
            if (tarlRegInfo == null || tarlRegInfo.Count < 1)
            {
                MessageBox.Show("未检索到该身份证的有效挂号信息", "提示", MessageBoxButtons.OK);
                return;
            }

            ArrayList arlRegInfo = new ArrayList();
            if (isatm)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (tinfo.IsAccount)
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else if (IsNetCancle)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            } else
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (!tinfo.IsAccount && !isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }

            if (arlRegInfo == null || arlRegInfo.Count == 0)
            {
                MessageBox.Show("未检索到相关患者挂号信息" + this.regMgr.Err, "提示");
                return;
            }

            foreach (FS.HISFC.Models.Registration.Register r in arlRegInfo)
            {
                List<AccountCardFee> lstCardFee = null;

                FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
                if (this.feeMgr.ValidMarkNO(r.PID.CardNO, ref accountObj) == -1)
                {
                    MessageBox.Show(feeMgr.Err);
                    SetCardNoFocus();
                    return;
                }

                int iRes = this.accMgr.QueryAccCardFeeDirectory(r.PID.CardNO, out lstCardFee);

                if (lstCardFee != null && lstCardFee.Count > 0)
                {
                    for (int idx = 0; idx < lstCardFee.Count; idx++)
                    {
                        lstCardFee[idx].Patient = accountObj.Patient;
                    }
                    AddCardFeeNoRegister(lstCardFee);
                }
            }



            if (arlRegInfo != null && arlRegInfo.Count > 0)
            {

                //只找到一条挂号记录
                if (arlRegInfo.Count == 1)
                {
                    this.addRegister(arlRegInfo);
                }
                else
                {
                    //多条挂号记录，让收费员自己去选择
                    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    ucShow.SetRegisterInfo(arlRegInfo);
                    Form fpShow = new Form();
                    fpShow.Width = 600;
                    fpShow.Height = 300;
                    fpShow.Controls.Add(ucShow);
                    fpShow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 根据姓名查询挂号记录
        /// </summary>
        /// <param name="name"></param>
        private void QueryRegisterByName(string name)
        {
            this.Clear();
            FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();


            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);

            //检索患者有效号
            ArrayList tarlRegInfo = this.regMgr.QueryName(name, permitDate);
            ArrayList arlRegInfo = new ArrayList();
            if (isatm)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (tinfo.IsAccount)
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else if (IsNetCancle)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (!tinfo.IsAccount && !isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }

            if (arlRegInfo == null || arlRegInfo.Count == 0)
            {
                MessageBox.Show("未检索到相关患者挂号信息" + this.regMgr.Err, "提示");
                return;
            }
            //List<AccountCardFee> lstCardFee = null;
            //int iRes = this.accMgr.QueryAccCardFeeDirectory(cardNo, out lstCardFee);

            //if (lstCardFee != null && lstCardFee.Count > 0)
            //{
            //    for (int idx = 0; idx < lstCardFee.Count; idx++)
            //    {
            //        lstCardFee[idx].Patient = accountObj.Patient;
            //    }
            //    AddCardFeeNoRegister(lstCardFee);
            //}

            if (arlRegInfo != null && arlRegInfo.Count > 0)
            {

                //只找到一条挂号记录
                if (arlRegInfo.Count == 1)
                {
                    this.addRegister(arlRegInfo);
                }
                else
                {
                    //多条挂号记录，让收费员自己去选择
                    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    ucShow.SetRegisterInfo(arlRegInfo);
                    Form fpShow = new Form();
                    fpShow.Width = 600;
                    fpShow.Height = 300;
                    fpShow.Controls.Add(ucShow);
                    fpShow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 根据病历号查询卡信息
        /// </summary>
        /// <param name="cardNo"></param>
        private void QueryRegisterByCardNO(string cardNo)
        {
            this.Clear();
            FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
            if (this.feeMgr.ValidMarkNO(cardNo, ref accountObj) == -1)
            {
                MessageBox.Show(feeMgr.Err);
                SetCardNoFocus();
                return;
            }

            if (string.IsNullOrEmpty(accountObj.Patient.PID.CardNO))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("您输入的就诊卡号不存在"), "提示");
                SetCardNoFocus();
                return;
            }

            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);
            cardNo = accountObj.Patient.PID.CardNO;

            //检索患者有效号
            ArrayList tarlRegInfo = this.regMgr.Query(cardNo, permitDate);
             ArrayList arlRegInfo =new ArrayList();
            if(isatm)
            {
                foreach(Register tinfo in tarlRegInfo)
                {
                   if(tinfo.IsAccount)
                   {
                       arlRegInfo.Add(tinfo);
                   }
                }
            }
            else if (IsNetCancle)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else
            {
                foreach(Register tinfo in tarlRegInfo)
                {
                   if(!tinfo.IsAccount&&!isNetInvoice(tinfo.InvoiceNO))
                   {
                       arlRegInfo.Add(tinfo);
                   }
                }               
            }

            //if (arlRegInfo == null || arlRegInfo.Count == 0)
            //{
            //    MessageBox.Show("未检索到相关患者挂号信息" + this.regMgr.Err, "提示");
            //    return;
            //}
            List<AccountCardFee> lstCardFee = null;
            int iRes = this.accMgr.QueryAccCardFeeDirectory(cardNo, out lstCardFee);

            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                for (int idx = 0; idx < lstCardFee.Count; idx++)
                {
                    lstCardFee[idx].Patient = accountObj.Patient;
                }
                AddCardFeeNoRegister(lstCardFee);
            }

            if (arlRegInfo != null && arlRegInfo.Count > 0)
            {
               
                //只找到一条挂号记录
                if (arlRegInfo.Count == 1)
                {
                    this.addRegister(arlRegInfo);
                }
                else
                {
                    //多条挂号记录，让收费员自己去选择
                    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    ucShow.SetRegisterInfo(arlRegInfo);
                    Form fpShow = new Form();
                    fpShow.Width = 600;
                    fpShow.Height = 300;
                    fpShow.Controls.Add(ucShow);
                    fpShow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 根据发票号查询卡信息
        /// </summary>
        /// <param name="invoiceNO"></param>
        private void QueryRegisterByInvoiceNO(string invoiceNo)
        {
            this.Clear();
            invoiceNo = invoiceNo.PadLeft(12, '0');
            txtInvoice.Text = invoiceNo;
            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);

            //先根据发票号查找到Clinic_Code
            //然后通过Clinic_Code找到对应的记录信息
            List<AccountCardFee> lstCardFee = null;
            int iRes = this.accMgr.QueryAccountCardFeeByInvoiceNO(invoiceNo, out lstCardFee);
            if (lstCardFee == null || lstCardFee.Count == 0)
            {
                MessageBox.Show("未检索到患者相关发票信息" + this.accMgr.Err, "提示");
                return;
            }

            string clinicCode = lstCardFee[0].ClinicNO;

            //检索患者有效号
            if (string.IsNullOrEmpty(clinicCode)==false)
            {
                Register arlRegInfo = this.regMgr.GetByClinic(clinicCode);
                this.addRegister(arlRegInfo);
            }
            else
            {
                //查找患者信息
                string cardNO = lstCardFee[0].CardNo;
                FS.HISFC.Models.RADT.PatientInfo p= radtMgr.QueryComPatientInfo(cardNO);
                for (int idx = 0; idx < lstCardFee.Count; idx++)
                {
                    lstCardFee[idx].Patient = p;
                }
                AddCardFeeNoRegister(lstCardFee);
                //跳转到收费界面
                this.neuTabControl1.SelectedTab = this.tabPageDir;
            }

        }

        /// <summary>
        /// 添加患者挂号明细
        /// </summary>
        /// <param name="registers"></param>
        private void addRegister(ArrayList registers)
        {
            this.fpSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            try
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                FS.HISFC.Models.Registration.Register obj;

                for (int i = registers.Count - 1; i >= 0; i--)
                {
                    obj = (FS.HISFC.Models.Registration.Register)registers[i];
                    this.addRegister(obj);
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(objEx.Message), "提示");
            }
            finally
            {
                this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);
            }
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.SetActiveCell(0, 1);
                this.fpSpread1_Sheet1.Cells[0, 0].Value = true;
                this.fpSpread1_SelectionChanged(null, null);

                SetReturnCost(this.neuSpread1_Sheet1);
            }

            //调整最适合列宽
            for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
            {
                this.fpSpread1_Sheet1.Columns[i].Width = this.fpSpread1_Sheet1.Columns[i].GetPreferredWidth();
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Width = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
            }
            for (int i = 0; i < this.neuSpread2_Sheet1.Columns.Count; i++)
            {
                this.neuSpread2_Sheet1.Columns[i].Width = this.neuSpread2_Sheet1.Columns[i].GetPreferredWidth();
            }
        }
        /// <summary>
        /// 不允许使用直接收费生成的号再进行挂号
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("此号段为直接收费使用，不可以退号"), FS.FrameWork.Management.Language.Msg("提示"));
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// 显示挂号信息
        /// </summary>
        /// <param name="reg"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            int cnt = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(cnt, 1, reg.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 2, reg.Sex.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 3, reg.DoctorInfo.SeeDate.ToString(), false);
            this.fpSpread1_Sheet1.SetValue(cnt, 4, reg.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 5, reg.DoctorInfo.Templet.RegLevel.Name, false);
            //新增标记：是否已看诊
            this.fpSpread1_Sheet1.SetValue(cnt, 6, reg.IsSee, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 7, reg.DoctorInfo.Templet.Doct.Name, false);
            
            this.fpSpread1_Sheet1.SetValue(cnt, 8, reg.RegLvlFee.RegFee + reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
            this.fpSpread1_Sheet1.Rows[cnt].Tag = reg;

            if (reg.IsSee)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.LightCyan;
            }
            if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back||
                reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.MistyRose;
            }


            this.fpSpread1_Sheet1.SetActiveCell(cnt, 1);
            this.fpSpread1_Sheet1.Cells[cnt, 0].Value = true;
            this.fpSpread1_SelectionChanged(null, null);
        }
        /// <summary>
        /// 处理卫生局预约退号
        /// </summary>
        /// <returns></returns>
        private int ReturnNetRegister()
        {

            // 所选挂号信息
            FS.HISFC.Models.Registration.Register regSelect = null;

            #region 获取退费数据
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (this.fpSpread1_Sheet1.Rows.Count >= 2)
                {
                    MessageBox.Show("不能同时退两条或者以上的挂号记录!");
                    return -1;
                }
                if (fpSpread1_Sheet1.ActiveRow == null)
                {
                    MessageBox.Show("没有找到需要退费信息!");
                    return -1;
                }
                regSelect = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Registration.Register;
                bool blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value);
                if (blnCheck)
                {
                    if (regSelect.IsSee && !this.isSeeedCanCancelRegInfo)
                    {
                        MessageBox.Show("该号已经看诊，不允许退号！", "提示");
                        return -1;
                    }
                }

            }
            #endregion
            if (!isNetInvoice(regSelect.InvoiceNO))
            {
                MessageBox.Show("此功能只能为网上预约退号,你没有权利进行其它退号/废号");
                return -1;
            }

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            #region 更新卫生局预约信息

            FS.HISFC.Models.Registration.AppointmentOrder app = appointmentMgr.QueryAppointmentOrderByClinicNO(regSelect.ID);
            if (app == null)
            {
                MessageBox.Show("没有找到该预约信息");
                return -1;
            }
            if (app != null && (app.OrderState == "2" || app.OrderState == "5"))
            {
                MessageBox.Show("该号已退费,不能再退!");
                return -1;
            }

            if (MessageBox.Show("是否取消[" + regSelect.Name + "]预约的专家[" + regSelect.DoctorInfo.Templet.Doct.Name +
                "]在[" + regSelect.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd") + "]的挂号信息?", "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                return -1;

            try
            {
                FS.HISFC.Components.Registration.AppointmentService.InvokeResult result =
                    NetService.Invoke_Sync(AppointmentService.funs.refundPay,
                                 app.OrderID,
                                 "2",
                                 current.ToString(),
                                 (app.RegFee + app.TreatFee).ToString()
                                 );

                if (result.ResultCode == "0")
                    MessageBox.Show("通知卫生局取消挂号成功,挂号费已退回网银,不需要退现金！", "提示");
                else
                {
                    MessageBox.Show("通知卫生局取消挂号失败,原因: " + result.ResultDesc, "错误");
                    return -1;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("通知卫生局取消挂号失败,原因: " + ex.Message, "错误");
                return -1;
            }
            #endregion
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("退号成功"), "提示");


            this.ClearAll();

            return 0;

        }

        private int DualAccountCardFee(ref List<AccountCardFee> lstAccFee)
        {
            FS.HISFC.Components.Registration.Forms.frmAccountCardFeePayTypeInput frmPayType = new FS.HISFC.Components.Registration.Forms.frmAccountCardFeePayTypeInput();
            frmPayType.AccountCardFeeList = lstAccFee;
            DialogResult dr = frmPayType.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lstAccFee = frmPayType.AccountCardFeeList;
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int save()
        {
           
            // true=通过挂号产生费用，false=直接收到费用
            bool blnReturnRegFee = true;
            // 所选挂号信息
            FS.HISFC.Models.Registration.Register regSelect = null;

            // 退号信息, true=退号
            bool blnRegReturn = false;

            decimal returndecimal=0;


            // 退费信息
            List<AccountCardFee> lstReturnFee = new List<AccountCardFee>();
            // 不退费信息
            List<AccountCardFee> lstUnRetFee = new List<AccountCardFee>();

            #region 获取退费数据
            AccountCardFee cardFee = null;
            bool blnCheck = false;
            if (this.neuTabControl1.SelectedIndex == 0)
            {

                //判断是否能够退号
                //if (this.fpSpread1_Sheet1.Rows.Count <= 0)
                //{
                //    MessageBox.Show("没有可以退费的记录!");
                //    return -1;
                //}

                if (this.fpSpread1_Sheet1.Rows.Count >= 2)
                {
                    MessageBox.Show("不能同时退两条或者以上的挂号记录!");
                    return -1;
                }

                if (this.fpSpread1_Sheet1.Rows.Count == 0)
                {
                    MessageBox.Show("没有选择退费数据!");
                    return -1;
                }

                blnReturnRegFee = true;
                regSelect = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Registration.Register;
                blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value);
                if (blnCheck)
                {
                    FS.HISFC.Models.Base.Employee myemployee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                    if (myemployee.ID.Equals(regSelect.InputOper.ID) == false)
                    {
                        //判断权限,是否有退其他挂号员操作的权限
                        if (!CommonController.CreateInstance().JugePrive("0811", "02"))
                        {
                            CommonController.CreateInstance().MessageBox("没有退其他操作员挂号记录的权限，操作员：" + CommonController.CreateInstance().GetEmployeeName(regSelect.InputOper.ID), MessageBoxIcon.Warning);
                            return -1;
                        }
                    }



                    if (regSelect.IsSee && !this.isSeeedCanCancelRegInfo)
                    {
                        MessageBox.Show("该号已经看诊，不允许退号！", "提示");
                        return -1;
                    }
                    else if (regSelect.PVisit.InState.ID.ToString() == "I" || regSelect.PVisit.InState.ID.ToString() == "R")
                    {
                        MessageBox.Show("留观患者不允许退号", "提示");
                        return -1;
                    }

                    blnRegReturn = true;
                }

                for (int idx = 0; idx < this.neuSpread1_Sheet1.RowCount; idx++)
                {
                    cardFee = this.neuSpread1_Sheet1.Rows[idx].Tag as AccountCardFee;
                    blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[idx, 0].Value);
                    if (blnCheck)
                    {
                        lstReturnFee.Add(cardFee);
                    }
                    else
                    {
                        lstUnRetFee.Add(cardFee);
                    }
                }

                if (blnRegReturn && IsMustAllReturnFee)
                {
                    if (lstUnRetFee.Count > 0)
                    {
                        MessageBox.Show("退号的情况下，必须全退!");
                        return -1;
                    }
                }
            }
            else
            {
                // 退直接收取费用 -- 只能一项一项退，因收取时也是只能一项一项的收取
                // 此时挂号信息为空

                blnReturnRegFee = false;
                for (int idx = 0; idx < this.neuSpread2_Sheet1.RowCount; idx++)
                {
                    cardFee = this.neuSpread2_Sheet1.Rows[idx].Tag as AccountCardFee;
                    blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread2_Sheet1.Cells[idx, 0].Value);
                    if (blnCheck)
                    {
                        lstReturnFee.Add(cardFee);
                    }
                }
            }

            #endregion

            if (!IsNetCancle && regSelect != null && isNetInvoice(regSelect.InvoiceNO))
            {
                MessageBox.Show("这条挂号记录为网上预约,你没有权利进行退号/废号");
                return -1;
            }
            if (IsNetCancle && !isNetInvoice(regSelect.InvoiceNO))
            {
                MessageBox.Show("此窗口只能为网上预约退号,你没有权利进行其它退号/废号");
                return -1;
            }

            if (blnReturnRegFee)
            {
                if (lstReturnFee == null || lstReturnFee.Count <= 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择需要退费的记录！"), "提示");
                    return -1;
                }
            }
            else
            {
                if (lstReturnFee == null || lstReturnFee.Count != 1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择需要退费的记录！且只能单项退费！"), "提示");
                    return -1;
                }
            }

            //if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("是否确认此操作") + "?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //{
            //    return -1;
            //}
            #region 选择支付方式退费

            if (this.DualAccountCardFee(ref lstReturnFee) < 0)
            {
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.appointmentMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int rtn;
            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;
            FS.HISFC.Models.Base.EnumRegisterStatus registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Back;

            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (this.isUseLogout && regSelect.InputOper.ID.Equals(regMgr.Operator.ID) && regSelect.BalanceOperStat.IsCheck == false)
                {
                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Bad;
                    registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Bad;
                }
                if (IsATM)
                {
                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Bad;
                    registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Bad;
                }
            }
            else
            {
                if (this.isUseLogout && lstReturnFee.Count > 0 && lstReturnFee[0].Oper.ID.Equals(regMgr.Operator.ID) && lstReturnFee[0].IsBalance==false)
                {
                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Bad;
                    registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Bad;
                }
            }

            try
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

                if (blnReturnRegFee)
                {
                    //重新获取患者实体,防止并发
                    regSelect = this.regMgr.GetByClinic(regSelect.ID);
                    if (this.ValidCardNO(regSelect.PID.CardNO) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    //出错
                    if (regSelect == null || string.IsNullOrEmpty(regSelect.ID))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regMgr.Err, "提示");
                        return -1;
                    }
                    //医疗保险已交过费的，不能进行退号 
                    if (regSelect.IsFee && feeMgr.QueryFeeItemListsByClinicNO(regSelect.ID).Count > 0)
                    {
                        if (regSelect.Pact.ID == "2")
                        {
                           
                                MessageBox.Show("该号为医疗保险号已经有交费记录,不能退费!");
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return -1;
                        }
                        DialogResult dir = MessageBox.Show("该号已经有交费记录,是否继续退号!", "提示", MessageBoxButtons.YesNo);
                        if (dir == DialogResult.No)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }

                    //使用,不能作废
                    if (regSelect.IsSee && !this.isSeeedCanCancelRegInfo)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("该号已经看诊,不能作废"), "提示");
                        return -1;
                    }

                    //是否已经退号
                    if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经退号，不能再次退号"), "提示");
                        return -1;
                    }

                    //是否已经作废
                    if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel||regSelect.Status== FS.HISFC.Models.Base.EnumRegisterStatus.Bad)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经作废，不能进行退号"), "提示");
                        return -1;
                    }
                }

                FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                // 是否需要获取发票号
                bool blnGetInvoice = false;
                if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                {
                    blnGetInvoice = true;
                }

                string strNewInvoiceNo = string.Empty;
                string strNewPrintInvoiceNo = string.Empty;
                int iRes = 0;
                string strErrText = "";

                string strInvoiceType = "R";
                if (blnGetInvoice)
                {
                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strNewInvoiceNo, ref strNewPrintInvoiceNo, ref strErrText);

                    if (iRes == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strErrText);
                        return -1;
                    }
                }

                #region 更改原记录状态

                string oldInvoiceNo = "";
                if (blnReturnRegFee)
                {
                    oldInvoiceNo = regSelect.InvoiceNO;
                }
                else
                {
                    oldInvoiceNo = lstReturnFee[0].InvoiceNo;
                }

                if (string.IsNullOrEmpty(oldInvoiceNo) == false)
                {
                    //数据库记录中状态定义与枚举定义是否一致

                    //挂号操作枚举：FS.HISFC.BizLogic.Registration.EnumUpdateStatus
                    //        Return = 0,
                    //        ChangeDept = 1,
                    //        Cancel = 2,
                    //        PatientInfo = 3,
                    //        Uncancel = 4,
                    //        Bad = 5,		
                    //fin_opr_register VALID_FLAG：0退费,1有效,2作废

                    //挂号状态枚举：FS.HISFC.Models.Base.EnumRegisterStatus
                    //        退费
                    //        Back = 0,
                    //        有效
                    //        Valid = 1,
                    //        作废
                    //        Cancel = 2,
                    //        Bad = 3,
                    //fin_opb_accountcardfee CANCEL_FLAG：‘0’ 无效 ‘1’ 有效,2退费
					//退费定义不一致
                    if (!this.IsEnumEqualDataBase&&registerStatus==FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                    {
                        iRes = this.accMgr.CancelAccountCardFeeByInvoice(oldInvoiceNo, 2);
                    }
                    else
                    {
                        iRes = this.accMgr.CancelAccountCardFeeByInvoice(oldInvoiceNo, (int)registerStatus);
                    }
                    if (iRes <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                        return -1;
                    }
                }

                #endregion

                #region 插入负记录

                cardFee = null;
                if (lstReturnFee != null && lstReturnFee.Count > 0)
                {
                    for (int idx = 0; idx < lstReturnFee.Count; idx++)
                    {
                        cardFee = lstReturnFee[idx].Clone();
                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        cardFee.Oper.ID = employee.ID;
                        cardFee.Oper.Name = employee.Name;
                        cardFee.Oper.OperTime = current;
                        cardFee.Tot_cost = -cardFee.Tot_cost;
                        cardFee.Own_cost = -cardFee.Own_cost;
                        cardFee.Pub_cost = -cardFee.Pub_cost;
                        cardFee.Pay_cost = -cardFee.Pay_cost;
                        returndecimal=returndecimal+cardFee.Tot_cost;
                        //退费定义不一致
                        if (!this.IsEnumEqualDataBase && registerStatus == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                        {
                            cardFee.IStatus = 2;
                        }
                        else
                        {
                            cardFee.IStatus = (int)registerStatus;
                        }

                        iRes = this.accMgr.InsertAccountCardFee(cardFee);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                            return -1;
                        }
                    }
                }

                cardFee = null;
                if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                {
                    for (int idx = 0; idx < lstUnRetFee.Count; idx++)
                    {
                        cardFee = lstUnRetFee[idx].Clone();
                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        cardFee.Oper.ID = employee.ID;
                        cardFee.Oper.Name = employee.Name;
                        cardFee.Oper.OperTime = current;
                        cardFee.Tot_cost = -cardFee.Tot_cost;
                        cardFee.Own_cost = -cardFee.Own_cost;
                        cardFee.Pub_cost = -cardFee.Pub_cost;
                        cardFee.Pay_cost = -cardFee.Pay_cost;

                        cardFee.IStatus = (int)registerStatus;

                        iRes = this.accMgr.InsertAccountCardFee(cardFee);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                            return -1;
                        }
                    }
                }


                #endregion

                #region 插入正记录

                cardFee = null;
                if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                {
                    for (int idx = 0; idx < lstUnRetFee.Count; idx++)
                    {
                       
                        cardFee = lstUnRetFee[idx];

                        if (blnRegReturn) cardFee.ClinicNO = "";

                        cardFee.InvoiceNo = strNewInvoiceNo;
                        cardFee.Print_InvoiceNo = strNewPrintInvoiceNo;

                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;

                        cardFee.FeeOper.ID = employee.ID;
                        cardFee.FeeOper.Name = employee.Name;
                        cardFee.FeeOper.OperTime = current;
                        cardFee.Oper.ID = employee.ID;
                        cardFee.Oper.Name = employee.Name;
                        cardFee.Oper.OperTime = current;

                        cardFee.IStatus = (int)(FS.HISFC.Models.Base.EnumRegisterStatus.Valid);

                        iRes = this.accMgr.InsertAccountCardFee(cardFee);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                            return -1;
                        }

                        lstUnRetFee[idx] = cardFee;
                    }
                }

                #endregion

                #region 处理挂号记录

                if (blnReturnRegFee)
                {
                    decimal decRegFee = 0;
                    decimal decChkFee = 0;
                    decimal decDigFee = 0;
                    decimal decOthFee = 0;
                    decimal ownCost = 0;
                    decimal pubCost = 0;
                    decimal payCost = 0;

                    if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                    {
                        for (int idx = 0; idx < lstUnRetFee.Count; idx++)
                        {
                            cardFee = lstUnRetFee[idx];
                            switch (cardFee.FeeType)
                            {
                                case AccCardFeeType.RegFee:
                                    decRegFee += cardFee.Tot_cost;
                                    break;
                                case AccCardFeeType.ChkFee:
                                    decChkFee += cardFee.Tot_cost;
                                    break;
                                case AccCardFeeType.DiaFee:
                                    decDigFee += cardFee.Tot_cost;
                                    break;
                                case AccCardFeeType.OthFee:
                                case AccCardFeeType.CaseFee:
                                case AccCardFeeType.CardFee:
                                case AccCardFeeType.AirConFee:
                                    decOthFee += cardFee.Tot_cost;
                                    break;
                            }

                            ownCost += cardFee.Own_cost;
                            pubCost += cardFee.Pub_cost;
                            payCost += cardFee.Pay_cost;
                        }
                    }

                    FS.HISFC.Models.Registration.Register objReturn = regSelect.Clone();

                    if (!blnRegReturn)
                    {
                        // 只退费，更新挂号记录
                        if (blnGetInvoice)
                        {
                            objReturn.InvoiceNO = strNewInvoiceNo;
                        }
                        objReturn.RegLvlFee.ChkFee = decChkFee;
                        objReturn.RegLvlFee.OwnDigFee = decDigFee;
                        objReturn.RegLvlFee.OthFee = decOthFee;
                        objReturn.RegLvlFee.RegFee = decRegFee;

                        objReturn.OwnCost = ownCost;
                        objReturn.PubCost = pubCost;
                        objReturn.PayCost = payCost;

                        iRes = this.regMgr.UpdateRegFeeCost(objReturn);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号信息状态已经变更,请重新检索数据"), "提示");
                            return -1;
                        }
                    }
                    else
                    {
                        #region 作废挂号信息

                        //发票号取回旧的发票号
                        //if (blnGetInvoice)
                        //{
                        //    objReturn.InvoiceNO = strNewInvoiceNo;
                        //}

                        objReturn.RegLvlFee.ChkFee = -objReturn.RegLvlFee.ChkFee;// +decChkFee;
                        objReturn.RegLvlFee.OwnDigFee = -objReturn.RegLvlFee.OwnDigFee;// +decDigFee;
                        objReturn.RegLvlFee.OthFee = -objReturn.RegLvlFee.OthFee;// +decOthFee;
                        objReturn.RegLvlFee.RegFee = -objReturn.RegLvlFee.RegFee;// +decRegFee;

                        objReturn.OwnCost = -objReturn.OwnCost;// +ownCost;
                        objReturn.PubCost = -objReturn.PubCost;// +pubCost;
                        objReturn.PayCost = -objReturn.PayCost;// +payCost;

                        objReturn.BalanceOperStat.IsCheck = false;
                        objReturn.BalanceOperStat.ID = "";
                        objReturn.BalanceOperStat.Oper.ID = "";
                        objReturn.Status = registerStatus;
                        
                        objReturn.InputOper.OperTime = current;//操作时间
                        objReturn.InputOper.ID = employee.ID;//操作人
                        objReturn.CancelOper.ID = employee.ID;//退号人
                        objReturn.CancelOper.OperTime = current;//退号时间

                        objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                        if (this.regMgr.Insert(objReturn) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.regMgr.Err, "提示");
                            return -1;
                        }

                        #endregion

                        #region 更新原来项目为作废

                        regSelect.CancelOper.ID = regMgr.Operator.ID;
                        regSelect.CancelOper.OperTime = current;

                        //更新原来项目为作废
                        rtn = this.regMgr.Update(flag, regSelect);
                        if (rtn == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.regMgr.Err, "提示");
                            return -1;
                        }
                        if (rtn == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号信息状态已经变更,请重新检索数据"), "提示");
                            return -1;
                        }

                        #endregion

                        #region 恢复限额
                        //恢复原来排班限额
                        //如果原来更新限额,那么恢复限额
                        if (regSelect.DoctorInfo.Templet.ID != null && regSelect.DoctorInfo.Templet.ID != "")
                        {
                            //现场号、预约号、特诊号

                            bool IsReged = false, IsTeled = false, IsSped = false, IsTeling = false;

                            if (regSelect.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                            {
                                IsTeled = true; //预约号
                                IsTeling = true;
                            }
                            else if (regSelect.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                            {
                                IsReged = true;//现场号
                                if (!regSelect.DoctorInfo.SeeDate.ToString("yyyy-MM-dd").Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                                {
                                    IsTeled = true; //提前挂号
                                    IsTeling = true;

                                    IsReged = false;
                                }
                            }
                            else
                            {
                                IsSped = true;//特诊号
                            }

                            rtn = this.schMgr.Reduce(regSelect.DoctorInfo.Templet.ID, IsReged, IsTeling, IsTeled, IsSped);
                            if (rtn == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(this.schMgr.Err, "提示");
                                return -1;
                            }

                            if (rtn == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg("已无排班信息,无法恢复限额"), "提示");
                                return -1;
                            }
                        }
                        #endregion

                        //医保处理
                        if (isCallYBInterface)
                        {
                            #region 医保处理

                            if (regSelect.Pact.PayKind.ID == "02" && DialogResult.Yes == MessageBox.Show("是否选择医保登记患者？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                long returnValue = 0;
                                FS.HISFC.Models.Registration.Register myYBregObject = regSelect.Clone();
                                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                this.medcareInterfaceProxy.SetPactCode(regSelect.Pact.ID);
                                //初始化医保dll
                                returnValue = this.medcareInterfaceProxy.Connect();
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("初始化失败") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                //读卡取患者信息
                                returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(myYBregObject);
                                if (returnValue == -1)
                                {

                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("读取患者信息失败") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                //医保信息赋值
                                regSelect.SIMainInfo = myYBregObject.SIMainInfo;
                                //退号
                                regSelect.User01 = "-1";//退号借用
                                //错误的调用了挂号方法{719DEE22-E3E3-4d3c-8711-829391BEA73C} by GengXiaoLei
                                //returnValue = this.medcareInterfaceProxy.UploadRegInfoOutpatient(reg);
                                regSelect.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                                returnValue = this.medcareInterfaceProxy.CancelRegInfoOutpatient(regSelect);
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者退号失败") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                returnValue = this.medcareInterfaceProxy.Commit();
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者退号提交失败") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                this.medcareInterfaceProxy.Disconnect();
                            }
                            #endregion
                        }
                    }

                }
                #endregion

                #region 发票走号

                if (blnGetInvoice)
                {
                    string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                    if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strNewInvoiceNo, ref strNewPrintInvoiceNo, ref strErrText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err);
                        return -1;
                    }

                    if (this.feeMgr.InsertInvoiceExtend(strNewInvoiceNo, strInvoiceType, strNewPrintInvoiceNo, "00") < 1)
                    {
                        // 发票头暂时先保存00
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err);
                        return -1;
                    }
                }

                #endregion

                #region 退号才发送消息

                //退号才发送消息
                if (blnRegReturn)
                {
                    if (InterfaceManager.GetIADT() != null)
                    {
                        if (InterfaceManager.GetIADT().Register(regSelect, false) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this, "退号失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return -1;

                        }
                    }
                }
                
               
                #endregion

                #region 更新分诊状态
                if (blnRegReturn)
                {
                    int updateAssignResult = assignMgr.UpdateAssignFlag(regSelect.ID, employee.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Cancel);
                    if (updateAssignResult < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.assignMgr.Err), "提示");
                        return -1;
                    }
                }
                #endregion

                if (isatm && regSelect.IsAccount)
                {
                    if (returndecimal > 0)
                    {
                        returndecimal = returndecimal * -1;
                    }
                    if (!checkatm(regSelect.InvoiceNO))
                    {
                        //MessageBox.Show(FS.FrameWork.Management.Language.Msg("发票号不能为空"), "提示");
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.txtInvoice.Focus();
                        return -1;
                    }
                   
                  if (feeMgr.AccountCancelPay(regSelect, returndecimal, regSelect.InvoiceNO, (feeMgr.Operator as Employee).Dept.ID, "R") < 0)
                  {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                       MessageBox.Show("账户退费入户失败！退号失败！" + feeMgr.Err);
                       return -1;
                   }
                  else
                  {
                       FS.FrameWork.Management.PublicTrans.Commit();
                       MessageBox.Show("已退费入个人账户中，不需要退现金！" + feeMgr.Err);
                  }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("退号出错!" + e.Message, "提示");
                return -1;
            }

            //如果已经打印发票,提示收回发票
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("退号成功"), "提示");


            #region 打印操作

            if (lstUnRetFee != null && lstUnRetFee.Count > 0)
            {
                cardFee = lstUnRetFee[0];
                if (regSelect != null)
                {
                    string name = regSelect.Name;
                    if (string.IsNullOrEmpty(cardFee.Patient.Name))
                    {
                        cardFee.Patient.Name = name;
                    }
                    if (string.IsNullOrEmpty(cardFee.Patient.Sex.Name))
                    {
                        cardFee.Patient.Sex = regSelect.Sex;
                    }
                    if (DateTime.Compare(cardFee.Patient.Birthday, new DateTime(1900, 1, 1)) <= 0)
                    {
                        cardFee.Patient.Birthday = regSelect.Birthday;
                    }
                    regSelect = new Register();
                    regSelect.PID.CardNO = cardFee.Patient.PID.CardNO;
                    
                    regSelect.Name = cardFee.Patient.Name;
                    regSelect.Sex = cardFee.Patient.Sex;
                    regSelect.Pact = cardFee.Patient.Pact;
                    regSelect.Birthday = cardFee.Patient.Birthday;

                    //如果有有挂号记录的情况下，去查询挂号记录表
                    if (!string.IsNullOrEmpty(cardFee.ClinicNO))
                    {
                        ArrayList listTemp = this.regMgr.QueryPatient(cardFee.ClinicNO);

                        if (listTemp != null && listTemp.Count == 1)
                        {
                            Register regTemp = listTemp[0] as FS.HISFC.Models.Registration.Register;

                            if (regTemp != null && !string.IsNullOrEmpty(regTemp.ID))
                            {
                                regSelect = regTemp;
                            }
                        }
                        
                    }

                    regSelect.InvoiceNO = cardFee.InvoiceNo;//重新设置发票号
                }

                regSelect.LstCardFee = lstUnRetFee;

                //打印退号票
                this.Print(regSelect);
            }
            #endregion

            this.ClearAll();

            return 0;
        }

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {

            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;

            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("打印票据失败,请在报表维护中维护退号票"));
            }
            else
            {

                if (regObj.IsEncrypt)
                {
                    regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(regObj.NormalName);
                }

                regprint.SetPrintValue(regObj);
                regprint.Print();
            }



        }

        #endregion

        #region 事件
        /// <summary>
        /// 处理快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.F12)
            //{
            //    this.save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.Escape)
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.F8)
            //{
            //    this.Clear();

            //    return true;
            //}

            if (keyData == Keys.F1)
            {
                this.txtName.Focus();
            }
            if (keyData == Keys.F2)
            {
                this.txtCardNo.Focus();
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// fp回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.save();
            //}
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0 || this.fpSpread1_Sheet1.ActiveRowIndex > this.fpSpread1_Sheet1.RowCount)
                return;

            FS.HISFC.Models.Registration.Register obj = fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
            if (obj == null)
                return;

            if (this.regMgr.Operator.ID.Equals(obj.InputOper.ID) == false && IsJudePrivileged)
            {
                //判断权限,是否有退其他挂号员操作的权限
                if (!CommonController.CreateInstance().JugePrive("0811", "02"))
                {
                    CommonController.CreateInstance().MessageBox("没有退其他操作员挂号记录的权限，操作员：" + CommonController.CreateInstance().GetEmployeeName(obj.InputOper.ID), MessageBoxIcon.Warning);
                    return;
                }
            }

            List<AccountCardFee> lstCardFee = null;
            int iRes = this.accMgr.QueryAccCardFeeByClinic(obj.PID.CardNO, obj.ID, out lstCardFee);
            if (iRes < 0 )
            {
                MessageBox.Show("未检索到挂号相关费用信息!" + this.accMgr.Err, "提示");
                return;
            }
            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                AddCardFeeByRegister(lstCardFee, FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value));
            }

            //this.SetReturnFee(e.Range.Row);
        }

        void fpSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                bool bln = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Value);

                SetClinicFeeChecked(bln);
            }
        }

        private void SetClinicFeeChecked(bool blnChecked)
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }

            int value = 0;
            if (blnChecked)
            {
                value = 1;
            }

            AccountCardFee cardFee = null;
            for (int idx = 0; idx < this.neuSpread1_Sheet1.RowCount; idx++)
            {
                cardFee = this.neuSpread1_Sheet1.Rows[idx].Tag as AccountCardFee;
                if (cardFee == null)
                    continue;

                switch (cardFee.FeeType)
                {
                    case AccCardFeeType.RegFee:
                    case AccCardFeeType.DiaFee:
                    case AccCardFeeType.ChkFee:
                        this.neuSpread1_Sheet1.Cells[idx, 0].Value = value;
                        break;
                }
            }
        }

        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column != 0)
            {
                return;
            }

            bool boolChecked = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[e.Row,e.Column].Value);
            int n = 0;
            if(boolChecked)
            {
                n = 1;
            }

            // 选择诊金及挂号费自动选上挂号记录
            AccountCardFee cardFee = null;
            cardFee = this.neuSpread1_Sheet1.Rows[e.Row].Tag as AccountCardFee;
            switch (cardFee.FeeType)
            {
                case AccCardFeeType.RegFee:
                case AccCardFeeType.DiaFee:
                case AccCardFeeType.ChkFee:
                    this.fpSpread1_Sheet1.Cells[0, 0].Value = n;
                    break;
            }

            SetReturnCost(this.neuSpread1_Sheet1);
        }

        void neuSpread2_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column != 0)
            {
                return;
            }

            SetReturnCost(this.neuSpread2_Sheet1);
        }
        /// <summary>
        /// 计算并设置金额
        /// </summary>
        /// <param name="sheet"></param>
        private void SetReturnCost(FarPoint.Win.Spread.SheetView sheet)
        {
            this.lbTot.Text = "";
            this.lbReturn.Text = "";

            if (sheet == null)
                return;

            decimal totCost = 0;
            decimal retCost = 0;

            AccountCardFee cardFee = null;
            for (int idx = 0; idx < sheet.RowCount; idx++)
            {
                if (sheet.Rows[idx].Tag == null)
                    continue;

                cardFee = sheet.Rows[idx].Tag as AccountCardFee;
                if (cardFee == null)
                    continue;

                totCost += cardFee.Tot_cost;

                bool bln = FS.FrameWork.Function.NConvert.ToBoolean(sheet.Cells[idx, 0].Value);
                if (bln)
                {
                    retCost += cardFee.Own_cost + cardFee.Pay_cost;
                }
            }

            this.lbTot.Text = totCost.ToString();
            this.lbReturn.Text = retCost.ToString();
        }

        /// <summary>
        /// 根据姓名检索患者卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Clear();

                string name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'", "[", "]");

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("就诊卡号不能为空"), "提示");
                    SetNameFocus();
                    return;
                }

                this.QueryRegisterByName(name);
            }
        }

        /// <summary>
        /// 根据病历号检索患者挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();

                Clear();

                if (string.IsNullOrEmpty(cardNo))
                {
                    SetCardNoFocus();
                    return;
                }

                cardNo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtCardNo.Text.Trim(), "'", "[", "]");
                if(string.IsNullOrEmpty(cardNo))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("就诊卡号不能为空"), "提示");
                    SetCardNoFocus();
                    return;
                }

                this.QueryRegisterByCardNO(cardNo);

            }
        }
        /// <summary>
        /// 根据身份证检索挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIDCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //{4661623D-235A-4380-A7E0-476C977650CD}
                string IDCard = this.txtIDCard.Text.Trim();
                if (string.IsNullOrEmpty(IDCard))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("身份证不能为空"), "提示");
                    this.txtIDCard.Focus();
                    return;
                }

                this.QueryRegisterByIDCard(IDCard);
            }
        }
        /// <summary>
        /// 挂号信息选择事件
        /// </summary>
        /// <param name="reg"></param>
        public void ucShow_SelectedRegister(FS.HISFC.Models.Registration.Register reg)
        {
            ArrayList listReg = new ArrayList();
            listReg.Add(reg);
            this.addRegister(listReg);
        }

        private void AddCardFeeNoRegister(List<AccountCardFee> lstCardFee)
        {
            this.neuSpread2_Sheet1.RowCount = 0;
            if (lstCardFee == null || lstCardFee.Count <= 0)
                return;

            int idx = 0;
            string strTypeName = "";
           // decimal decTotCost = 0;
            foreach (AccountCardFee cardFee in lstCardFee)
            {
                if (cardFee.IStatus != 1)
                {
                    continue;
                }
                if (isCardFeeCanReturn == false && cardFee.FeeType == AccCardFeeType.CardFee)
                {
                    continue;
                }
                this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.RowCount, 1);
                idx = this.neuSpread2_Sheet1.RowCount - 1;

                this.neuSpread2_Sheet1.SetValue(idx, 0, true, false);
                this.neuSpread2_Sheet1.SetValue(idx, 1, cardFee.Patient.Name, false);
                this.neuSpread2_Sheet1.SetValue(idx, 2, cardFee.Patient.Sex.Name, false);
                this.neuSpread2_Sheet1.SetValue(idx, 3, cardFee.Print_InvoiceNo, false);

                switch (cardFee.FeeType)
                {
                    case AccCardFeeType.AirConFee:
                        strTypeName = "空调费";
                        break;
                    case AccCardFeeType.CardFee:
                        strTypeName = "卡费用";
                        break;
                    case AccCardFeeType.CaseFee:
                        strTypeName = "病历本费";
                        break;

                    case AccCardFeeType.ChkFee:
                        strTypeName = "检查费";
                        break;

                    case AccCardFeeType.DiaFee:
                        strTypeName = "诊金";
                        break;

                    case AccCardFeeType.OthFee:
                        strTypeName = "其他费用";
                        break;
                    case AccCardFeeType.RegFee:
                        strTypeName = "挂号费";
                        break;

                    default:
                        strTypeName = "其他费用";
                        break;
                }

                this.neuSpread2_Sheet1.SetValue(idx, 4, strTypeName, false);


                this.neuSpread2_Sheet1.SetValue(idx, 5, cardFee.Own_cost.ToString(), false);
                this.neuSpread2_Sheet1.SetValue(idx, 6, cardFee.Pub_cost.ToString(), false);
                this.neuSpread2_Sheet1.SetValue(idx, 7, cardFee.Pay_cost.ToString(), false);

                this.neuSpread2_Sheet1.SetValue(idx, 8, cardFee.FeeOper.Name, false);
                this.neuSpread2_Sheet1.SetValue(idx, 9, cardFee.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"), false);

                this.neuSpread2_Sheet1.SetValue(idx, 10, cardFee.MarkNO, false);
                this.neuSpread2_Sheet1.SetValue(idx, 11, cardFee.MarkType.Name, false);

                this.neuSpread2_Sheet1.Rows[idx].Tag = cardFee;
                //decTotCost +=cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
            }
           // this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.Rows.Count - 1, 8, decTotCost.ToString(), false);
        }

        private void AddCardFeeByRegister(List<AccountCardFee> lstCardFee, bool isSelect)
        {
            decimal decTotCost = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            if (lstCardFee == null || lstCardFee.Count <= 0)
                return;

            int idx = 0;
            string strTypeName = "";
            foreach (AccountCardFee cardFee in lstCardFee)
            {
                if (cardFee.IStatus != 1)
                    continue;

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                idx = this.neuSpread1_Sheet1.RowCount - 1;

                switch (cardFee.FeeType)
                {
                    case AccCardFeeType.AirConFee:
                        strTypeName = "空调费";
                        break;
                    case AccCardFeeType.CardFee:
                        strTypeName = "卡费用";
                        break;
                    case AccCardFeeType.CaseFee:
                        strTypeName = "病历本费";
                        break;

                    case AccCardFeeType.ChkFee:
                        strTypeName = "检查费";
                        break;

                    case AccCardFeeType.DiaFee:
                        strTypeName = "诊金";
                        break;

                    case AccCardFeeType.OthFee:
                        strTypeName = "其他费用";
                        break;
                    case AccCardFeeType.RegFee:
                        strTypeName = "挂号费";
                        break;

                    default:
                        strTypeName = "其他费用";
                        break;
                }

                this.neuSpread1_Sheet1.SetValue(idx, 0, isSelect, false);
                this.neuSpread1_Sheet1.SetValue(idx, 1, strTypeName, false);
                this.neuSpread1_Sheet1.SetValue(idx, 2, cardFee.Own_cost.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 3, cardFee.Pub_cost.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 4, cardFee.Pay_cost.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 5, this.payWayHelper.GetName(cardFee.PayType.ID), false);
                this.neuSpread1_Sheet1.SetValue(idx, 6, cardFee.FeeOper.Name, false);
                this.neuSpread1_Sheet1.SetValue(idx, 7, cardFee.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"), false);
                this.neuSpread1_Sheet1.SetValue(idx, 8, cardFee.MarkNO, false);
                this.neuSpread1_Sheet1.SetValue(idx, 9, cardFee.MarkType.Name, false);

                this.neuSpread1_Sheet1.Rows[idx].Tag = cardFee;

                decTotCost += cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
            }
            this.fpSpread1_Sheet1.SetValue(this.fpSpread1_Sheet1.Rows.Count - 1, 8, decTotCost.ToString(), false);
        }
        private bool isNetInvoice(string invoiceno)
        {
            if (invoiceno.StartsWith("W"))
                return true;

            return false;
        }
        private bool checkatm(string invoiceno)
        {
            if (invoiceno.StartsWith("R"))
           {
               MessageBox.Show("请先去专窗打印发票。");
               return false;
           }
           FS.HISFC.Models.Registration.Register tregObj=null;
           try
           {
               if(this.fpSpread1_Sheet1.Rows.Count<0)
               {
                  MessageBox.Show("没有找到挂号记录");
                  return false;   
               }
               tregObj=(FS.HISFC.Models.Registration.Register)this.fpSpread1_Sheet1.Rows[0].Tag;
               if(tregObj==null)
               {
                   MessageBox.Show("没有找到挂号记录");
                   return false;   
               }
           
           }
           catch
           {
               MessageBox.Show("没有找到挂号记录");
               return false;   
           }
           if(isatm)
           {
               if(!tregObj.IsAccount)
               {
                     MessageBox.Show("非自助机挂号数据，请去普通窗口办理");
                     return false;
               }
           }
           else
           {
                if(tregObj.IsAccount)
               {
                     MessageBox.Show("自助机挂号数据，请去专窗办理");
                     return false;
               }
           }
           return true; 
        }

        /// <summary>
        /// 根据处方号检索挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //{4661623D-235A-4380-A7E0-476C977650CD}
                string invoiceNo = this.txtInvoice.Text.Trim();

                if (invoiceNo == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("发票号不能为空"), "提示");
                    this.txtInvoice.Focus();
                    return;
                }
                if (IsNetCancle && !isNetInvoice(invoiceNo))
                {
                    MessageBox.Show("此发票不是卫生局预约发票,不允许退费", "提示");
                    return;
                }
                if (!IsNetCancle && isNetInvoice(invoiceNo))
                {
                    MessageBox.Show("此发票为卫生局预约发票,不允许退费", "提示");
                    return;
                }
                this.QueryRegisterByInvoiceNO(invoiceNo);
                if (!checkatm(invoiceNo))
                {
                    //MessageBox.Show(FS.FrameWork.Management.Language.Msg("发票号不能为空"), "提示");
                    this.Clear();
                    this.txtInvoice.Focus();
                    return;
                }
            }
        }

        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("退费", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("废号", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("清屏", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolbarService.AddToolButton("刷卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            toolbarService.AddToolButton("身份证", "身份证", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);
            toolbarService.AddToolButton("测试网络", "测试卫生局预约网络是否通畅", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设置, true, false, null);
            toolbarService.AddToolButton("网络退号", "网络预约号退号/退费", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            return toolbarService;
        }        

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "测试网络":
                    break; 
                case "网络退号":
                    ReturnNetRegister();
                    break; 
                case "退费":
                    //if (txtCardNo.Text == null || txtCardNo.Text.Trim() == "")
                    //{
                    //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入病历号"), FS.FrameWork.Management.Language.Msg("提示"));
                    //    return;
                    //}
                    e.ClickedItem.Enabled = false;
                    if (this.save() == -1)
                    {
                        e.ClickedItem.Enabled = true;

                        return;
                    }
                    e.ClickedItem.Enabled = true;

                    break;
                case "废号":
                    e.ClickedItem.Enabled = false;
                    if (this.save() == -1)
                    {
                        e.ClickedItem.Enabled = true;
                        return;
                    }
                    e.ClickedItem.Enabled = true;

                    break;
                case "身份证":
                    {
                        if (InterfaceManager.GetIReadIDCard() != null)
                        {
                            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
                            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
                            string photoFileName = "";
                            int rtn = InterfaceManager.GetIReadIDCard().GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
                            if (rtn == -1)
                            {
                                CommonController.Instance.MessageBox(this, "读卡失败，" + message, MessageBoxIcon.Asterisk);
                            }
                            else if (rtn == 0)
                            {
                                CommonController.Instance.MessageBox(this, "读卡失败，" + message, MessageBoxIcon.Asterisk);
                            }
                            else if (!string.IsNullOrEmpty(code))
                            {
                                this.txtIDCard.Text = code;
                                this.txtIDCard_KeyDown(txtIDCard, new KeyEventArgs(Keys.Enter));
                            }
                        }
                        break;
                    }
                case "刷卡":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperMCard(ref cardNo, ref error) == -1)
                        {
                            CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                            return;
                        }

                        txtCardNo.Text = cardNo;
                        txtCardNo.Focus();
                        this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        break;
                    }
                case "清屏":

                    this.ClearAll();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #region IInterfaceContainer 成员
        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        public Type[] InterfaceTypes
        {

            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);

                return type;
            }
        }

        #endregion
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        private void chbQuitFeeBookFee_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                SetReturnCost(this.neuSpread1_Sheet1);
            }
            else
            {
                SetReturnCost(this.neuSpread2_Sheet1);
            }
        }

        #region IPreArrange 成员

        public int PreArrange()
        {
            //判断退号权限
            if (!CommonController.CreateInstance().JugePrive("0811", "01"))
            {
                CommonController.CreateInstance().MessageBox("没有退员的权限，操作员：" + CommonController.CreateInstance().GetEmployeeName(regMgr.Operator.ID), MessageBoxIcon.Warning);
                return -1;
            }
            return 1;
        }

        #endregion
    }
}
