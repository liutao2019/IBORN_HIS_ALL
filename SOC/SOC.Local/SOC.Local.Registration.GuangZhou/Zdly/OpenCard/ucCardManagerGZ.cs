using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Account;
using Neusoft.FrameWork.Models;
using Neusoft.HISFC.BizProcess.Interface.Account;
using Neusoft.FrameWork.Management;

namespace Neusoft.SOC.Local.Registration.GuangZhou.Zdly.OpenCard
{
    /// <summary>
    /// 就诊卡发放
    /// </summary>
    public partial class ucCardManagerGZ : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCardManagerGZ()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// Manager业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 门诊费用业务类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
        
        /// <summary>
        /// Acount业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.Account accountManager = new Neusoft.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 费用管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeManage = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        
        /// <summary>
        /// 卡实体
        /// </summary>
        private Neusoft.HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// 卡操作实体
        /// </summary>
        private Neusoft.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
        
        /// <summary>
        /// 工具栏
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBar = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 卡类型帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper markTypeHelp = new Neusoft.FrameWork.Public.ObjectHelper();

        private Neusoft.HISFC.BizLogic.Manager.Constant constManager = new Neusoft.HISFC.BizLogic.Manager.Constant();
        
        /// <summary>
        /// 在输入过程中是否动态查找患者信息
        /// </summary>
        private bool isSelectPatientByEnter = false;
        /// <summary>
        /// 数据是否只在本地处理，不往数据中心发送
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        private bool isLocalOperation = true;
        /// <summary>
        /// 办卡时是否实时判断，是否享受相应的合同单位
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        private bool isJudgePact = false;
        /// <summary>
        /// 新建卡是否收取卡成本费，0=不收取，1=收取，2=按卡类别收取
        /// </summary>
        private string IsAcceptCardFee = "0";
        /// <summary>
        /// 补卡是否收取卡成本费 0=不收取，1=收取，2=按卡类别收取
        /// </summary>
        private string IsAcceptChangeCardFee = "0";
        /// <summary>
        /// 退卡时退费 0=不退，1=退费
        /// </summary>
        private string ReturnCardReturnFee = "0";
        /// <summary>
        /// 是否自动打印
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// 是否显示物理卡号
        /// </summary>
        private bool isShowMarkNo = false;
        /// <summary>
        /// 自动打印时那些卡类型自动打印，以“;”结尾
        /// </summary>
        private string printCardType = "";

        private bool blnIDCardNoOnly = false;

        /// <summary>
        /// 是否根据身份证号判断是否办过卡
        /// </summary>
        private bool isJudgeByIDCard = false;

        /// <summary>
        /// 是否读身份证号判断检索患者信息
        /// </summary>
        private bool isQueryPatientInfoByReadIDCard = false;

        /// <summary>
        /// 允许卡号为空的保存的卡类型,以“;”结尾"
        /// </summary>
        private string allowNoCardSaveType = "";

        private NeuObject cardTypeObj = new NeuObject();

        /// <summary>
        /// 是否小孩联系人必须要填写
        /// </summary>
        private bool iSChildlinker = false;


        /// <summary>
        /// 是否一个身份证只能办一张卡
        /// </summary>
        private bool isonly = false;

        /// <summary>
        /// 办卡标签打印接口
        /// </summary>
        IPrintLable iPrintLable = null;

        /// <summary>
        /// 诊疗收费凭证打印接口
        /// </summary>
        IPrintCardFee iPrint = null;

        /// <summary>
        /// 诊疗返还凭证打印接口
        /// </summary>
        IPrintReturnCardFee iPrintReturn = null;

        #endregion

        #region 属性

        /// <summary>
        /// 卡类型是否可以修改
        /// </summary>
        private bool isCanChangeCardType = false;

        /// <summary>
        /// 是否根据身份证号判断是否办过卡
        /// </summary>
        [Category("控件设置"), Description("是否根据身份证号判断是否办过卡")]
        public bool IsJudgeByIDCard
        {
            get
            {
                return isJudgeByIDCard;
            }
            set
            {
                isJudgeByIDCard = value;
            }
        }

        /// <summary>
        /// 是否根据身份证号判断是否办过卡
        /// </summary>
        [Category("控件设置"), Description("是否小孩联系人必须要填写")]
        public bool ISChildlinker
        {
            get
            {
                return iSChildlinker;
            }
            set
            {
                iSChildlinker = value;
            }
        }


        /// <summary>
        /// 是否读身份证号判断检索患者信息
        /// </summary>
        [Category("控件设置"), Description("是否读身份证号判断检索患者信息")]
        public bool IsQueryPatientInfoByReadIDCard
        {
            get
            {
                return isQueryPatientInfoByReadIDCard;
            }
            set
            {
                isQueryPatientInfoByReadIDCard = value;
            }
        }


        private bool isJumpHomePhone = false;
        [Category("控件设置"), Description("是否输入家庭地址后直接跳到电话字段")]
        public bool IsJumpHomePhone
        {
            get { return this.ucRegPatientInfo1.IsJumpHomePhone; }
            set { this.ucRegPatientInfo1.IsJumpHomePhone = value; }
        }

        /// <summary>
        /// 卡类型是否可以修改
        /// </summary>
        [Category("控件设置"), Description("卡类型是否可以修改")]
        public bool IsCanChangeCardType
        {
            get
            {
                return isCanChangeCardType;
            }
            set
            {
                isCanChangeCardType = value;
                this.cmbMarkType.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示物理卡号
        /// </summary>
        [Category("控件设置"), Description("是否显示物理卡号")]
        public bool IsShowMarkNo
        {
            get
            {
                return isShowMarkNo;
            }
            set
            {
                isShowMarkNo = value;
            }
        }

        #region 输入控制属性
        [Category("输入设置"), Description("姓名是否必须输入！")]
        public bool IsInputName
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputName;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputName = value;
            }
        }

        [Category("修改控制"), Description("是否判断本院职工")]
        public string IsValidHospitalStaff
        {
            get
            {
                return this.ucRegPatientInfo1.IsValidHospitalStaff;
            }
            set
            {
                this.ucRegPatientInfo1.IsValidHospitalStaff = value;
            }
        }

        [Category("输入设置"), Description("性别是否必须输入！")]
        public bool IsInputSex
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputSex;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputSex = value;
            }
        }

        [Category("输入设置"), Description("结算类别是否必须输入！")]
        public bool IsInputPact
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputPact;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputPact = value;
            }
        }

        [Category("输入设置"), Description("医保证号是否必须输入！")]
        public bool IsInputSiNo
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputSiNo; 
            }
            set 
            {
                this.ucRegPatientInfo1.IsInputSiNo = value;
            }
        }

        [Category("输入设置"), Description("出生日期是否必须输入！")]
        public bool IsInputBirthDay
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputBirthDay; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputBirthDay = value;
            }
        }

        [Category("输入设置"), Description("证件类型是否必须输入！")]
        public bool IsInputIDEType
        {
            get 
            { 
                return this.ucRegPatientInfo1.IsInputIDEType; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputIDEType = value;
            }
        }

        [Category("输入设置"), Description("证件号是否必须输入！")]
        public bool IsInputIDENO
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputIDENO; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputIDENO = value;
            }
        }

        /// <summary>
        /// 不能同时为空项目  0 = 不控制 1 = 控制 身份证与电话号码
        /// </summary>
        [Category("修改控制"), Description("不能同时为空项目  0 = 不控制 1 = 控制 身份证与电话号码")]
        public int IMustInpubByOne
        {
            get { return this.ucRegPatientInfo1.IMustInpubByOne; }
            set { this.ucRegPatientInfo1.IMustInpubByOne = value; }

        }

        #endregion

        [Category("控件设置"), Description("是否按照必录项跳转输入焦点 True:是 False:否")]
        public bool IsMustInputTabIndex
        {
            get
            {
                return this.ucRegPatientInfo1.IsMustInputTabIndex;
            }
            set
            {
                this.ucRegPatientInfo1.IsMustInputTabIndex = value;
            }
        }

        [Category("控件设置"),Description("在输入过程中是否动态查找患者信息 True:是 False:否")]
        public bool IsSelectPatientByEnter
        {
            get 
            {
                isSelectPatientByEnter = this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter;
                return isSelectPatientByEnter;
            }
            set 
            { 
                isSelectPatientByEnter = value;
                this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter = isSelectPatientByEnter;
            }
        }

        /// <summary>
        /// 数据是否只在本地处理，不往数据中心发送
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        [Category("控件设置"), Description("数据是否只在本地处理，不往数据中心发送 True:是 False:否")]
        public bool IsLocalOperation
        {
            get
            {
                return isLocalOperation;
            }
            set
            {
                isLocalOperation = value;
            }
        }

        [Category("控件设置"), Description("false:就诊卡号做物理卡号 true:就诊卡号与物理卡号不同")]
        public bool CardWay
        {
            get
            {
                return this.ucRegPatientInfo1.CardWay;
            }
            set
            {
                this.ucRegPatientInfo1.CardWay = value;
            }
        }
        /// <summary>
        /// 证件号唯一性的提示
        /// </summary>
        [Category("控件设置"), Description("证件号唯一性的提示")]
        public bool BlnIDCardNoOnly
        {
            get { return blnIDCardNoOnly; }
            set { blnIDCardNoOnly = value; }
        }

        [Category("参数设置"), Description("一卡多合同单位")]
        public bool IsMutilPactInfo
        {
            get
            {
                return this.ucRegPatientInfo1.IsMutilPactInfo;
            }
            set
            {
                this.ucRegPatientInfo1.IsMutilPactInfo = value;
            }
        }

        [Description("是否自动打印标签卡"), Category("设置")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        [Description("自动打印时那些卡类型自动打印，以“;”结尾"), Category("设置")]
        public string PrintCardType
        {
            get
            {
                return printCardType;
            }
            set
            {
                this.printCardType = value;
            }
        }

        /// <summary>
        /// 允许卡号为空的保存的卡类型，以“;”结尾
        /// </summary>
        [Description("允许卡号为空的保存的卡类型，以“;”结尾"), Category("设置")]
        public string AllowNoCardSaveType
        {
            get
            {
                return allowNoCardSaveType;
            }
            set
            {
                this.allowNoCardSaveType = value;
            }
        }

        [Description("是否显示患者当日办卡列表"), Category("设置")]
        public bool IsShowPatientIndaySheet
        {
            get
            {
                return this.spPatientInDay.Visible;
            }
            set
            {
                this.spPatientInDay.Visible = value;
            }
        }

        private bool isNewAccount = false;
        [Description("是否显示患者当日办卡列表"), Category("设置")]
        public bool IsNewAccount
        {
            get
            {
                return isNewAccount;
            }
            set
            {
                isNewAccount = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="consList">提示信息集合</param>
        private void DealConstantList(ArrayList consList)
        {
            if (consList == null || consList.Count <= 0)
            {
                return;
            }

            this.spInfo.RowCount = 0;
            this.spInfo.RowCount = (consList.Count / 3) + (consList.Count % 3 == 0 ? 0 : 1);

            int row = 0;
            int col = 0;

            foreach (Neusoft.FrameWork.Models.NeuObject obj in consList)
            {
                if (col >= 5)
                {
                    col = 0;
                    row++;
                }

                this.spInfo.SetValue(row, col, obj.ID);
                this.spInfo.SetValue(row, col + 1, obj.Name);

                col = col + 2;
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns>true:成功　false失败</returns>
        private bool Valid()
        {
            FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
          
            if (string.IsNullOrEmpty(MarkType.ID))
            {
                MessageBox.Show("请选择卡类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbMarkType.Focus();
                return false;
            }
            if (MarkType.Memo == "01")
            {
                MessageBox.Show("请将一张新的诊疗卡放置在读卡器上正确位置，然后按确定按钮继续，在系统没有提示成功前，请勿动卡。");
                IOperCard opercard = Neusoft.FrameWork.WinForms.Classes.
                   UtilInterface.CreateObject(this.GetType(), typeof(IOperCard)) as IOperCard;
                if (opercard == null)
                {
                    MessageBox.Show("没有维护读卡接口！");
                    return false;
                }
                string McardNo = "";
                string error = "";

                int result = opercard.ReadMCardNO(ref McardNo, ref  error);
                if (result != -1)
                {
                    txtMarkNo.Text = McardNo;
                }
                else
                {
                    MessageBox.Show("读卡失败，请确认是否正确放置诊疗卡！\n" + error);
                    return false;
                }
                if (!McardNo.StartsWith(MarkType.Memo))
                {
                    MessageBox.Show("您选择的是" + MarkType.Name + ",卡号是" + McardNo + "与约定的卡规则不一致。请核对！\n" + error);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtMarkNo.Text))
                {
                    MessageBox.Show("请填写！\n" );
                    return false;
                }
    
            }

            this.IsInputIDEType = false;
            this.IsInputIDENO = false;
            AccountCard card = this.accountManager.GetAccountCard(txtMarkNo.Text.Trim(), this.cmbMarkType.Tag.ToString());
            if (card != null)
            {
                MessageBox.Show("该卡已被其他患者使用，请换卡！", "错误");
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }
            if (isJudgeByIDCard)
            {
                Neusoft.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
                if (!string.IsNullOrEmpty(patient.IDCard) && patient.PID.CardNO == "")
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
                    int i = this.accountManager.GetPatientInfoByIDCard(patient.IDCard);
                    if (i >0)
                    {
                        DialogResult digRreslut = MessageBox.Show("该患者身份证号已办过卡，是否继续?", "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        {
                            if (digRreslut == DialogResult.No)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 作废正在使用的就诊卡
        /// </summary>
        /// <returns>1作废成功　0不进行作废操作　-1失败</returns>
        private int StopPatientCard()
        {
            string tempCardNO = this.ucRegPatientInfo1.CardNO;
            //当tempCardNO为空的时候是正常发卡操作，当不为空的时候是补发新卡
            //当正常发卡操作的时候回新建立CardNO形成新的患者信息
            //在补发的时候只更新患者信息
            if (string.IsNullOrEmpty(tempCardNO)) return 0;
            //查找患者正在使用的卡的集合
            //List<AccountCard> list = accountManager.GetMarkList(tempCardNO, this.cardTypeObj.ID, "1");

            //原来方法查询卡类型有问题，暂时传入全部卡类型
            List<AccountCard> list = accountManager.GetMarkList(tempCardNO,"ALL", "1");
            if (list.Count == 0) return 0;
            DialogResult digRreslut = MessageBox.Show("是否停用正在使用的就诊卡？", "提示", MessageBoxButtons.OKCancel);
            if (digRreslut == DialogResult.Cancel) return 0;   
            ucCancelMark uc = new ucCancelMark(list);

            uc.IsAcceptCardFee = this.IsAcceptCardFee;
            uc.IsAcceptChangeCardFee = this.IsAcceptChangeCardFee;
            uc.ReturnCardReturnFee = this.ReturnCardReturnFee;

            uc.StopCardEvent+=new ucCancelMark.EventStopCard(uc_StopCardEvent);
            Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            if (uc.FindForm().DialogResult == DialogResult.No) return 0;
            if (uc.FindForm().DialogResult == DialogResult.Cancel) return -1;
            return 1;
            
        }

        /// <summary>
        /// 停用就诊卡
        /// </summary>
        /// <param name="markList">卡集合</param>
        /// <param name="lstCardFee">卡费用</param>
        /// <returns></returns>
        private bool uc_StopCardEvent(List<AccountCard> markList)
        {
            int resultValue = 0;

            AccountCardRecord tempCardRecord = null;
            
            foreach (AccountCard tempAccountCard in markList)
            {
                //修改卡状态
                if (tempAccountCard.MarkStatus == MarkOperateTypes.Stop)
                {
                    resultValue = accountManager.StopAccountCard(tempAccountCard);
                }
                else if (tempAccountCard.MarkStatus == MarkOperateTypes.Cancel)
                {
                    resultValue = accountManager.BackAccountCard(tempAccountCard);
                }
                else
                {
                    resultValue = accountManager.UpdateAccountCardState(tempAccountCard.MarkNO, tempAccountCard.MarkType, false);
                }
                if (resultValue < 0)
                {
                    MessageBox.Show("作废患者就诊卡失败！" + accountManager.Err, "提示");
                    return false;
                }

                #region 形成卡操作记录
                tempCardRecord = new AccountCardRecord();
                tempCardRecord.CardNO = tempAccountCard.Patient.PID.CardNO;//门诊卡号
                tempCardRecord.MarkNO = tempAccountCard.MarkNO;//就诊卡号
                tempCardRecord.MarkType = tempAccountCard.MarkType; //卡类型
                tempCardRecord.OperateTypes.ID = (int)MarkOperateTypes.Cancel; //操作类型
                tempCardRecord.Oper.ID = accountManager.Operator.ID; //操作人
                #endregion
                //形成操作记录
                resultValue = accountManager.InsertAccountCardRecord(tempCardRecord);
                if (resultValue < 0)
                {
                    MessageBox.Show("插入卡操作记录失败！" + accountManager.Err);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        protected virtual void Save()
        {
            if (!this.Valid())
            {
                //this.ClearData();
                return;
            }

            //普通患者就诊卡发放
            if (!this.ucRegPatientInfo1.IsTreatment)
            {
                if (!this.ucRegPatientInfo1.InputValid())
                {
                    //this.ClearData();
                    return;
                }

            }

            this.ucRegPatientInfo1.IsAutoBuildCardNo = false;
            //如果允许没有卡号保存就先取Card_No,以card_no作为mark_no
            FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
          
            string strMsg = string.Empty;

            if (blnIDCardNoOnly)
            {
                string strIDCardType = string.Empty;
                string strIDCardNo = string.Empty;
                string strCardNo = string.Empty;
                this.ucRegPatientInfo1.GetIdCardInfo(out strIDCardType, out strIDCardNo, out strCardNo);

                if (!string.IsNullOrEmpty(strIDCardType) && !string.IsNullOrEmpty(strIDCardNo))
                {
                    //查找患者信息
                    List<AccountCard> list = accountManager.GetAccountCard("", "", "", "", strIDCardType, strIDCardNo, "");

                    if (list != null && !string.IsNullOrEmpty(strCardNo))
                    {
                        for (int idx = 0; idx < list.Count; idx++)
                        {
                            if (list[idx].Patient.PID.CardNO == strCardNo)
                            {
                                list.RemoveAt(idx);
                                idx--;
                            }
                        }
                    }
                    string tempc = "";
                    if (list != null && list.Count > 0)
                    {
                        strMsg = Language.Msg("存在相同 【证件号】 的办卡记录是否继续？");
                        if (MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            return;
                        }
                        tempc = list[0].Patient.PID.CardNO;
                    }
                    if (string.IsNullOrEmpty(this.ucRegPatientInfo1.CardNO) && tempc!="" )
                    {
                        this.ucRegPatientInfo1.CardNO = list[0].Patient.PID.CardNO;
                    }
                    else
                    {
                        if (this.ucRegPatientInfo1.CardNO != tempc)
                        {
                            MessageBox.Show("输入的病历号与证件号存在的病历号不符，请检查是否曾经有多个病历号", "系统提示");
                            return;
                        }
                    }
                }
            }

            // 历史卡记录
            List<AccountCard> lstHistoryCard = null;

            // 需在停卡退卡列表
            List<AccountCard> lstStopBackCard = null;

            // 记录卡费用
            List<AccountCardFee> lstCardFee = null;

            // 当前操作员
            Neusoft.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as Neusoft.HISFC.Models.Base.Employee;
            DateTime nowTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(accountManager.GetSysDateTime());

            #region 处理停卡退卡操作
            string tempCardNO = this.ucRegPatientInfo1.CardNO;
            //当tempCardNO为空的时候是正常发卡操作，当不为空的时候是补发新卡
            //当正常发卡操作的时候回新建立CardNO形成新的患者信息
            //在补发的时候只更新患者信息
            if (!string.IsNullOrEmpty(tempCardNO))
            {
                //原来方法查询卡类型有问题，暂时传入全部卡类型
                lstHistoryCard = accountManager.GetMarkList(tempCardNO, "ALL", "1");
                if (lstHistoryCard != null && lstHistoryCard.Count > 0)
                {
                    DialogResult digRreslut = MessageBox.Show("是否停用正在使用的就诊卡？", "提示", MessageBoxButtons.OKCancel);
                    if (digRreslut == DialogResult.OK)
                    {
                        ucCancelMark uc = new ucCancelMark(lstHistoryCard);
                        Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                        if (uc.FindForm().DialogResult == DialogResult.OK)
                        {
                            lstStopBackCard = uc.lstCard;
                        }
                        else if (uc.FindForm().DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    else if (digRreslut == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            #endregion

            #region 发卡实体处理

            accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
            accountCard.MarkNO = this.txtMarkNo.Text.Trim();
            accountCard.MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
            accountCard.MarkStatus = MarkOperateTypes.Begin;

            if (lstHistoryCard != null && lstHistoryCard.Count > 0)
            {
                accountCard.ReFlag = "1";

                if(this.IsAcceptCardFee == "2")
                {
                    // 先当作新发卡，找到相同类型的卡变为补发卡
                    accountCard.ReFlag = "0"; 
                    foreach(AccountCard temp in lstHistoryCard)
                    {
                        if(temp.MarkType.ID == accountCard.MarkType.ID)
                        {
                            accountCard.ReFlag = "1";
                            break;
                        }
                    }
                }                
            }
            else
            {
                accountCard.ReFlag = "0";
            }
            accountCard.CreateOper.Oper.ID = currentOperator.ID;
            accountCard.CreateOper.OperTime = nowTime;

            #endregion

            lstCardFee = new List<AccountCardFee>();

            #region 发卡计费实体处理

            if ((accountCard.ReFlag == "0" && this.IsAcceptCardFee != "0") || (accountCard.ReFlag == "1" && this.IsAcceptChangeCardFee != "0" ))
            {
                AccountCardFee cardFee = new AccountCardFee();
                cardFee.InvoiceNo = "";
                cardFee.Print_InvoiceNo = "";
                cardFee.CardNo = "";
                cardFee.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;
                cardFee.MarkNO = accountCard.MarkNO;
                cardFee.MarkType = accountCard.MarkType;

                cardFee.Tot_cost = Neusoft.FrameWork.Function.NConvert.ToDecimal(controlParamIntegrate.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.AccountConstant.AcceptCardFee, false));

                cardFee.Own_cost = cardFee.Tot_cost;

                cardFee.FeeOper.Oper.ID = currentOperator.ID;
                cardFee.FeeOper.OperTime = nowTime;

                cardFee.Oper.Oper.ID = currentOperator.ID;
                cardFee.Oper.OperTime = nowTime;

                cardFee.IsBalance = false;
                cardFee.BalanceNo = "";
                cardFee.BalnaceOper.Oper.ID = "";

                cardFee.FeeType = AccCardFeeType.CardFee;

                cardFee.IStatus = 1;

                if (cardFee.Tot_cost > 0)
                {
                    lstCardFee.Add(cardFee);
                }
            }
            #endregion

            strMsg = string.Empty;
            decimal decMoney = 0;
            decimal decTotalMoney = 0;
            int iRes = 0;

            #region 计算退卡退费问题

            if (ReturnCardReturnFee == "1" && lstStopBackCard != null && lstStopBackCard.Count > 0)
            {
                List<AccountCardFee> lstTempCardFee = null;
                AccountCardFee tempCardFee = null;
                foreach (AccountCard backCard in lstStopBackCard)
                {
                    tempCardFee = null;
                    decMoney = 0;

                    if (backCard.MarkStatus == MarkOperateTypes.Cancel)
                    {
                        strMsg += "\t【" + backCard.MarkType.Name + "：" + backCard.MarkNO + "】\r\n";

                        iRes = accountManager.QueryAccountCardFee(backCard.Patient.PID.CardNO, backCard.MarkNO, backCard.MarkType.ID, out lstTempCardFee);
                        if (iRes > 0 && lstTempCardFee != null && lstTempCardFee.Count > 0)
                        {
                            for (int idx = 0; idx < lstTempCardFee.Count; idx++)
                            {
                                if (lstTempCardFee[idx].IStatus != 1)
                                    continue;

                                decMoney = lstTempCardFee[idx].Tot_cost;
                                if (decMoney <= 0)
                                    continue;

                                tempCardFee = new AccountCardFee();
                                tempCardFee.InvoiceNo = lstTempCardFee[idx].InvoiceNo;
                                tempCardFee.Print_InvoiceNo = lstTempCardFee[idx].Print_InvoiceNo;
                                tempCardFee.Patient = backCard.Patient;

                                tempCardFee.ClinicNO = lstTempCardFee[idx].ClinicNO;

                                tempCardFee.MarkNO = backCard.MarkNO;
                                tempCardFee.MarkType = backCard.MarkType.Clone();
                                tempCardFee.TransType = Neusoft.HISFC.Models.Base.TransTypes.Negative;
                                tempCardFee.Tot_cost = -decMoney;

                                tempCardFee.Own_cost = -lstTempCardFee[idx].Own_cost;
                                tempCardFee.Pay_cost = -lstTempCardFee[idx].Pay_cost;
                                tempCardFee.Pub_cost = -lstTempCardFee[idx].Pub_cost;


                                tempCardFee.FeeOper = lstTempCardFee[idx].FeeOper;

                                tempCardFee.Oper.Oper.ID = currentOperator.ID;
                                tempCardFee.Oper.Oper.Name = currentOperator.Name;
                                tempCardFee.Oper.OperTime = nowTime;

                                tempCardFee.IsBalance = false;
                                tempCardFee.BalanceNo = "";
                                tempCardFee.IStatus = 1;

                                tempCardFee.FeeType = AccCardFeeType.CardFee;

                                lstCardFee.Add(tempCardFee);
                            }
                        }
                    }

                    decTotalMoney += decMoney;
                }
            }

            if (!string.IsNullOrEmpty(strMsg))
            {
                if (MessageBox.Show("请收回以下卡：\r\n" + strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
            }

            #endregion

            #region 设置事物
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.accountManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            #endregion

            #region  保存患者信息
            int resultValue = 0;
            this.ucRegPatientInfo1.McardNO = txtMarkNo.Text;          
            resultValue = this.ucRegPatientInfo1.Save();
            if (resultValue <= 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            #endregion

            //#region 作废正在使用的就诊卡
            //if (StopPatientCard() < 0)
            //{
            //    Neusoft.FrameWork.Management.PublicTrans.RollBack();
            //    return;
            //}
            //#endregion

            #region 发卡

            #region 卡实体补充

            accountCard.Patient = this.ucRegPatientInfo1.GetPatientInfomation();
            
            #endregion
            //处理发卡操作
            string error = string.Empty;
            resultValue = this.BulidCard(accountCard);
            if (resultValue == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region 停卡退卡操作

            if (lstStopBackCard != null && lstStopBackCard.Count > 0)
            {
                for (int idx = 0; idx < lstStopBackCard.Count; idx++)
                {
                    if (lstStopBackCard[idx].MarkStatus == MarkOperateTypes.Stop)
                    {
                        lstStopBackCard[idx].StopOper.Oper.ID = currentOperator.ID;
                        lstStopBackCard[idx].StopOper.Oper.Name = currentOperator.Name;
                        lstStopBackCard[idx].StopOper.OperTime = nowTime;

                        resultValue = accountManager.StopBackAccountCard(lstStopBackCard[idx]);
                    }
                    else if (lstStopBackCard[idx].MarkStatus == MarkOperateTypes.Cancel)
                    {
                        lstStopBackCard[idx].BackOper.Oper.ID = currentOperator.ID;
                        lstStopBackCard[idx].BackOper.Oper.Name = currentOperator.Name;
                        lstStopBackCard[idx].BackOper.OperTime = nowTime;

                        resultValue = accountManager.StopBackAccountCard(lstStopBackCard[idx]);
                    }

                    if (resultValue == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(accountManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            #endregion

            #region 卡费用操作

            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                AccountCardFee cardFee = null;

                for (int idx = 0; idx < lstCardFee.Count; idx++)
                {
                    cardFee = lstCardFee[idx];

                    cardFee.Patient = accountCard.Patient;

                    resultValue = this.feeManage.SaveAccountCardFee(ref cardFee);

                    if (resultValue == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(feeManage.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    lstCardFee[idx] = cardFee;
                }
            }

            #endregion
         
            IOperCard opercard = Neusoft.FrameWork.WinForms.Classes.
                   UtilInterface.CreateObject(this.GetType(), typeof(IOperCard)) as IOperCard;
            if (opercard == null)
            {
                MessageBox.Show("没有维护读卡接口！");
                return ;
            }
            string McardNo = "";

            int result = opercard.WriterCardNO(this.ucRegPatientInfo1.CardNO, ref error);
            if (result == -1)
            {
                MessageBox.Show("写卡失败，在提示成功前勿动卡！\n" + error);
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
          
         

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            //打印标签
            if (this.isAutoPrint)
            {
                if (printCardType.Contains(accountCard.MarkType.ID))
                {
                    PrintLable();
                }
                
            }
            
            // 打印卡费用凭证
            PrintCardFee(lstCardFee);

            if (this.isNewAccount)
            {
                if (this.NewAccount() == 0)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("保存数据成功！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");
                if (register == "0")
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("保存数据成功！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.save();
                }
            }

            this.ClearData();
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        private void PrintLable()
        {
            if (iPrintLable != null)
            {
                if (accountCard == null)
                {
                    return;
                }
                iPrintLable.PrintLable(accountCard);
            }
        }

        /// <summary>
        /// 打印卡费用凭证
        /// </summary>
        /// <param name="lstCardFee"></param>
        private void PrintCardFee(List<AccountCardFee> lstCardFee)
        {
            if (lstCardFee == null || lstCardFee.Count <= 0)
                return;

            foreach (AccountCardFee cardFee in lstCardFee)
            {
                if (cardFee.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive)
                {
                    if (iPrint == null)
                        continue;

                    iPrint.SetValue(cardFee);
                    iPrint.Print();
                }
                else
                {
                    if (iPrintReturn == null)
                        continue;

                    iPrintReturn.SetValue(cardFee);
                    iPrintReturn.Print();
                }
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearData()
        {
            this.ucRegPatientInfo1.Clear(true);
            this.txtMarkNo.Text = string.Empty;
            this.txtCardNO.Text = string.Empty;
            this.txtMedicalCardNo.Text = string.Empty;
            this.cmbMarkType.SelectedIndex = 0;
            this.ucRegPatientInfo1.Focus();
            accountCard = null;
            this.ckIsTreatment.Checked = false;
            this.spPatient.RowCount = 0;
            //外接屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            {
                //this.ucRegPatientInfo1.iMultiScreen.ListInfo = null;
                Neusoft.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as Neusoft.HISFC.Models.Base.Employee;
                //显示初始化界面                    
                System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                lo.Add("");//患者信息
                lo.Add("");//卡号
                lo.Add(currentOperator.ID);//收费员工号
                lo.Add(currentOperator.Name);//收费员姓名
                this.ucRegPatientInfo1.iMultiScreen.ListInfo = lo;
            }
        }
               
        /// <summary>
        /// 建立新的病理卡号
        /// </summary>
        private int BulidCard(AccountCard tempAccountCard)
        {
            try
            {
                if (accountManager.InsertAccountCard(tempAccountCard) == -1)
                {
                    MessageBox.Show("保存卡记录失败！" + accountManager.Err, "错误");
                    return -1;
                }
                accountCardRecord = new Neusoft.HISFC.Models.Account.AccountCardRecord();
                //插入卡的操作记录
                accountCardRecord.MarkNO = tempAccountCard.MarkNO;
                accountCardRecord.MarkType.ID = tempAccountCard.MarkType.ID;
                accountCardRecord.CardNO = tempAccountCard.Patient.PID.CardNO;
                accountCardRecord.OperateTypes.ID = (int)Neusoft.HISFC.Models.Account.MarkOperateTypes.Begin;
                accountCardRecord.Oper.ID = (this.accountManager.Operator as Neusoft.HISFC.Models.Base.Employee).ID;
                //是否收取卡成本费
                accountCardRecord.CardMoney = 0;

                if (accountManager.InsertAccountCardRecord(accountCardRecord) == -1)
                {
                    MessageBox.Show("保存卡操作记录失败！"+ accountManager.Err);
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！" + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        protected virtual int QueryPatientInfo()
        {
            Neusoft.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();

            // 性别与合同单位不做为查询条件
            return this.QueryPatientInfo(patient.Name,
                                         patient.Sex.ID.ToString(),
                                         patient.Pact.ID.ToString(),
                                         patient.PID.CaseNO,
                                         patient.IDCardType.ID,
                                         patient.IDCard,
                                         patient.SSN,
                                         patient.PhoneHome);
        }

        /// <summary>
        /// 查询一天内某患者办卡信息
        /// </summary>
        /// <returns></returns>
        protected virtual int QueryPatientInfoInDay()
        {
            string operCode = this.accountManager.Operator.ID.ToString();
            string days = "1";
            if (!string.IsNullOrEmpty(operCode) && !string.IsNullOrEmpty(days))
            {
                return this.QueryPatientInfoInDay(operCode, days);
            }
            return -1;
        }

        protected virtual int QueryPatientInfoInDay(string operCode, string days)
        {
            if (string.IsNullOrEmpty(operCode) || string.IsNullOrEmpty(days))
            {
                return -1;
            }
            List<AccountCard> list = accountManager.GetAccountCardInDays(operCode, days);
            if (list == null)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(accountManager.Err);
                return -1;  
            }
            try
            {
                if (this.spPatientInDay.RowCount > 0)
                {
                    this.spPatientInDay.Rows.Remove(0, this.spPatientInDay.RowCount);
                }

                this.spPatientInDay.Rows.Count = list.Count;
                int count = 0, beginIndex = 0, rangCount = 1;
                count = list.Count;

                for (int i = 0; i < count; i++)
                {
                    AccountCard tempCard = list[i];
                    this.spPatientInDay.Cells[i, 0].Text = tempCard.Patient.Name;
                    //性别
                    this.spPatientInDay.Cells[i, 1].Text = tempCard.Patient.Sex.Name;
                    //生日
                    this.spPatientInDay.Cells[i, 2].Text = this.accountManager.GetAge(tempCard.Patient.Birthday);
                    //民族
                    this.spPatientInDay.Cells[i, 3].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.Nationality.ID, 0);
                    //合同单位
                    this.spPatientInDay.Cells[i, 4].Text = tempCard.Patient.Pact.Name;
                    //证件类型
                    this.spPatientInDay.Cells[i, 5].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.IDCardType.ID, 1);
                    //证件号
                    this.spPatientInDay.Cells[i, 6].Text = tempCard.Patient.IDCard;
                    string telephone = "";
                    if (tempCard.Patient.PhoneHome != null && tempCard.Patient.PhoneHome != "")
                    {
                        telephone = tempCard.Patient.PhoneHome;
                    }
                    else if (tempCard.Patient.Kin.RelationPhone != null && tempCard.Patient.Kin.RelationPhone != "")
                    {
                        telephone = tempCard.Patient.Kin.RelationPhone;
                    }
                    else
                    {
                        telephone = "";
                    }
                    this.spPatientInDay.Cells[i, 7].Text = telephone;
                    this.spPatientInDay.Cells[i, 8].Text = tempCard.Patient.AddressHome;
                    this.spPatientInDay.Cells[i, 9].Text = tempCard.MarkNO;
                    if (isShowMarkNo)
                    {
                        this.spPatientInDay.Columns.Get(9).Visible = true;
                    }
                    this.spPatientInDay.Cells[i, 10].Text = markTypeHelp.GetName(tempCard.MarkType.ID);
                    this.spPatientInDay.Rows[i].Tag = tempCard;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            return 1;
        }

        protected int QueryPatientInfoByEnter()
        {
            Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = this.ucRegPatientInfo1.patientInfo;
            Neusoft.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            this.ucRegPatientInfo1.patientInfo = patientTemp;

            // 性别与合同单位不做为查询条件
            return this.QueryPatientInfo(patient.Name,
                                         "",
                                         "",
                                         patient.PID.CaseNO,
                                         patient.IDCardType.ID,
                                         patient.IDCard,
                                         patient.SSN,
                                         patient.PhoneHome);
        }

        protected virtual int QueryPatientInfo(string patientName, string sexId, string pactId, string CassNo, string idCardType, string idCard, string SSN,string phoneNumber)
        {
            if (string.IsNullOrEmpty(patientName)  && string.IsNullOrEmpty(idCard)&&string.IsNullOrEmpty(phoneNumber))
            {
                return -1;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查找患者信息，请稍后...");
            Application.DoEvents();
            //查找患者信息

            //如果证件号码为空，证件类型为空
            if (string.IsNullOrEmpty(idCard))
            {
                idCardType = "";
            }
            List<AccountCard> list = accountManager.GetAccountCard(patientName, sexId, pactId, CassNo, idCardType, idCard, SSN,phoneNumber);
                                                                    
            if (list == null)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(accountManager.Err);
                return -1;
            }
            try
            {
                if (this.spPatient.Rows.Count > 0)
                {
                    this.spPatient.Rows.Remove(0, this.spPatient.Rows.Count);
                }
                this.spPatient.Rows.Count = list.Count;
                int count = 0, beginIndex = 0, rangCount = 1;
                count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    AccountCard tempCard = list[i];
                    //姓名
                    this.spPatient.Cells[i, 0].Text = tempCard.Patient.Name;
                    //性别
                    this.spPatient.Cells[i, 1].Text = tempCard.Patient.Sex.Name;
                    //生日
                    this.spPatient.Cells[i, 2].Text = this.accountManager.GetAge(tempCard.Patient.Birthday);
                    //民族
                    this.spPatient.Cells[i, 3].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.Nationality.ID, 0);
                    //合同单位
                    this.spPatient.Cells[i, 4].Text = tempCard.Patient.Pact.Name;
                    //证件类型
                    this.spPatient.Cells[i, 5].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.IDCardType.ID, 1);
                    //证件号
                    this.spPatient.Cells[i, 6].Text = tempCard.Patient.IDCard;
                    string telephone="";
                    if (tempCard.Patient.PhoneHome != null && tempCard.Patient.PhoneHome != "")
                    {
                        telephone = tempCard.Patient.PhoneHome;
                    }
                    else if (tempCard.Patient.Kin.RelationPhone != null && tempCard.Patient.Kin.RelationPhone != "")
                    {
                        telephone = tempCard.Patient.Kin.RelationPhone;
                    }
                    else
                    {
                        telephone = "";
                    }
                    this.spPatient.Cells[i, 7].Text = telephone;
                    this.spPatient.Cells[i, 8].Text = tempCard.Patient.AddressHome;
                    this.spPatient.Cells[i, 9].Text = tempCard.MarkNO;
                    if (isShowMarkNo)
                    {
                        this.spPatient.Columns.Get(9).Visible = true;
                    }
                    this.spPatient.Cells[i, 10].Text = markTypeHelp.GetName(tempCard.MarkType.ID);
                    this.spPatient.Rows[i].Tag = tempCard;
                    //计算合并单元格
                    if (i < count - 1)
                    {
                        if (tempCard.Patient.PID.CardNO == list[i + 1].Patient.PID.CardNO)
                        {
                            rangCount += 1;
                            if (i == count - 2)
                            {
                                if (rangCount > 1)
                                {
                                    RangFpCell(beginIndex, rangCount);
                                }
                            }
                        }
                        else
                        {
                            if (rangCount > 1)
                            {
                                RangFpCell(beginIndex, rangCount);
                            }
                            beginIndex = i + 1;
                            rangCount = 1;
                        }
                    }

                }
                this.neuSpread1.ActiveSheet = spPatient;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        private void RangFpCell(int begin, int count)
        {
            for (int col = 0; col < this.spPatient.Columns.Count - 2; col++)
            {
                this.spPatient.Models.Span.Add(begin, col, count, 1);
            }
        }

        /// <summary>
        /// 跳转焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegPatientInfo1_OnFoucsOver(object sender, EventArgs e)
        {
            this.txtMarkNo.Focus();
            this.neuSpread1.ActiveSheet = this.spPatient;
        }

        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegPatientInfo1_OnEnterSelectPatient(object sender, EventArgs e)
        {
            if (this.IsSelectPatientByEnter)
            {
                this.QueryPatientInfoByEnter();
            }
        }

        /// <summary>
        /// ucRegPatientInfo控件cmb得到焦点时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegPatientInfo1_CmbFoucs(object sender, EventArgs e)
        {
            if (sender is Neusoft.FrameWork.WinForms.Controls.NeuComboBox)
            {
                FrameWork.WinForms.Controls.NeuComboBox cmb = sender as FrameWork.WinForms.Controls.NeuComboBox;
                ArrayList al = cmb.alItems;
                DealConstantList(al);
                this.neuSpread1.ActiveSheet = this.spInfo;
            }
            else
            {
                this.neuSpread1.ActiveSheet = this.spPatient;
            }
        }

        /// <summary>
        /// 新建账户
        /// </summary>
        /// <returns></returns>
        private int NewAccount()
        {
            if (accountCard == null)
            {
                return 0;
            }
            Neusoft.HISFC.Models.Account.Account account = this.accountManager.GetAccountByCardNo(accountCard.Patient.PID.CardNO);
            if (account == null || string.IsNullOrEmpty(account.ID))//账户为空，继续
            {
                string  JudgeCredentialCreating = controlParamIntegrate.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.AccountConstant.JudgeCredentialCreating, false);
                if ("0".Equals(JudgeCredentialCreating))
                {
                    //判断证件号是否存在账户
                    ArrayList accountList = accountManager.GetAccountByIdNO(accountCard.Patient.IDCard, accountCard.Patient.IDCardType.ID);
                    if (accountList == null)
                    {
                        MessageBox.Show("建立账户失败，查找患者账户信息失败！");
                        return -1;
                    }
                    //根据证件号查找患者账户信息
                    if (accountList.Count > 0)
                    {
                        return 0;
                    }
                }

                //账户信息
                account = new Neusoft.HISFC.Models.Account.Account();
                account.ID = accountManager.GetAccountNO();
                account.AccountCard = accountCard;
                //是否取默认密码，系统设置，上线初期一般默认密码。
                //账户密码
                bool IsDefaultPassword= controlParamIntegrate.GetControlParam<bool>("S00033", false);
                if (!IsDefaultPassword)
                {
                    ucEditPassWord uc = new ucEditPassWord(false);
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                    if (uc.FindForm().DialogResult != DialogResult.OK) return -1;
                    //加密密码
                    account.PassWord = uc.PwStr;
                }
                else
                {
                    account.PassWord = "000000";
                }

                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                accountManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                //插入账户表
                if (accountManager.InsertAccount(account) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("建立账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                //生成账户流水信息
                Neusoft.HISFC.Models.Account.AccountRecord accountRecord =  new Neusoft.HISFC.Models.Account.AccountRecord();
                accountRecord.AccountNO = account.ID;//帐号
                accountRecord.Patient = accountCard.Patient;//门诊卡号
                accountRecord.FeeDept.ID = (accountManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID;//科室编码
                accountRecord.Oper.ID = accountManager.Operator.ID;//操作员
                accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//操作时间
                accountRecord.IsValid = true;//是否有效
                if (accountRecord != null)
                {
                    accountRecord.OperType.ID = (int)Neusoft.HISFC.Models.Account.OperTypes.NewAccount;
                    if (accountManager.InsertAccountRecord(accountRecord) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("建立账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                else
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("建立账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("保存并建立账户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 1;
            }
            return 0;
        }
        #endregion

        #region 事件

        private void ucCardManager_Load(object sender, EventArgs e)
        {

            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            if (al == null)
            {
                MessageBox.Show("查找就诊卡类型失败");
                return;
            }
            this.cmbMarkType.AddItems(al);
            this.cmbMarkType.SelectedIndex = 0;
            markTypeHelp.ArrayObject = al;
            foreach (NeuObject conObj in al)
            {
                if (conObj.Name.Contains("门诊"))
                {
                    this.cardTypeObj = conObj;
                    break;
                }
            }
            this.QueryPatientInfoInDay();

            this.IsAcceptCardFee = controlParamIntegrate.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.AccountConstant.IsAcceptCardFee, false);
            this.IsAcceptChangeCardFee = controlParamIntegrate.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.AccountConstant.IsAcceptChangeCardFee, false);
            this.ReturnCardReturnFee = controlParamIntegrate.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.AccountConstant.ReturnCardReturnFee, false);

            this.ucRegPatientInfo1.CmbFoucs += new HandledEventHandler(ucRegPatientInfo1_CmbFoucs);
            this.ucRegPatientInfo1.OnFoucsOver += new HandledEventHandler(ucRegPatientInfo1_OnFoucsOver);
            this.ucRegPatientInfo1.OnEnterSelectPatient += new HandledEventHandler(ucRegPatientInfo1_OnEnterSelectPatient);
            this.ucRegPatientInfo1.IsLocalOperation = this.isLocalOperation;

            iPrintLable = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintLable)) as IPrintLable;

            iPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintCardFee)) as IPrintCardFee;

            iPrintReturn = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintReturnCardFee)) as IPrintReturnCardFee;

            // {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
            this.isJudgePact = controlParamIntegrate.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.AccountConstant.BuildCardIsJudgePact, false);
            this.ucRegPatientInfo1.IsJudgePact = this.isJudgePact;
            this.ucRegPatientInfo1.Focus();
            //外接屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            {
                this.FindForm().Deactivate += new EventHandler(ucCardManager_Deactivate);
                this.FindForm().Activated += new EventHandler(ucCardManager_Activated);
            }

            this.Enter += new EventHandler(ucCardManager_Enter);

            this.Leave += new EventHandler(ucCardManager_Leave);

            //判断是否允许挂号
            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");
            if (register.Equals("1"))
            {
                System.Windows.Forms.MenuItem menuItem3 = new MenuItem("打印挂号票");
                this.menu.MenuItems.Add(menuItem3);
                menuItem3.Click += new EventHandler(menuItem3_Click);
            }
        }

        #region 外接屏相关{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        public void ucCardManager_Activated(object sender, EventArgs e)
        {
            this.ucRegPatientInfo1.iMultiScreen.ShowScreen();
        }

        public void ucCardManager_Deactivate(object sender, EventArgs e)
        {
            this.ucRegPatientInfo1.iMultiScreen.CloseScreen();
        }
          #endregion
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBar.AddToolButton("患者信息查询", "患者信息查询", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBar.AddToolButton("清屏", "清屏", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBar.AddToolButton("身份证", "身份证", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);
            return toolBar;
        }
        
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "患者信息查询":
                    {
                        QueryPatientInfo();
                        break;
                    }
                case "清屏":
                    {
                        this.ClearData();
                        break;
                    }
                case "身份证"://读取二代身份证
                    {

                        //{877A4CAD-6F3A-4c8a-8B8B-949E9160404C}
                        this.ucRegPatientInfo1.Clear(true);
                        int i= this.ucRegPatientInfo1.ReadIDCard();
                        if (i == 1)
                        {
                            this.ucRegPatientInfo1_OnEnterSelectPatient(null, null);

                            if (this.isQueryPatientInfoByReadIDCard)
                            {
                                this.QueryPatientInfo();
                            }
                            if (this.ucRegPatientInfo1.IsJudgePactByIdno)
                            {
                                this.ucRegPatientInfo1.JudgePactByIdno();
                            }
                        }
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintLable();
            this.ClearData();
            return base.OnPrint(sender, neuObject);
        }

        private void txtMarkNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //外接屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add(this.ucRegPatientInfo1 .GetPatientInfomation());
                    lo.Add(this.txtMarkNo.Text .ToString ());
                    lo.Add("");
                    lo.Add("");
                    this.ucRegPatientInfo1.iMultiScreen.ListInfo = lo;
                }
                string strMarkNo = this.txtMarkNo.Text.Trim();
                FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;

                if (MarkType == null)
                {
                    MessageBox.Show("当前未选择卡类型！");
                    this.cmbMarkType.Focus();
                    return;
                }

                if (AllowNoCardSaveType.Contains(MarkType.ID) && string.IsNullOrEmpty(strMarkNo))
                {
                }
                else
                {
                    accountCard = new AccountCard();
                    //标识是办卡
                    accountCard.Memo = "2";

                    int resultValue = accountManager.GetCardByRule(this.txtMarkNo.Text.Trim(), ref accountCard);
                    if (resultValue <= 0)
                    {

                        MessageBox.Show(accountManager.Err);
                        this.txtMarkNo.Focus();
                        this.txtMarkNo.SelectAll();
                        //this.cmbMarkType.Tag = string.Empty;
                        this.cmbMarkType.SelectedIndex = 0;
                        return;
                    }

                    if (resultValue == 1)
                    {
                        MessageBox.Show("该卡已被使用，请换卡！");
                        this.txtMarkNo.Focus();
                        this.txtMarkNo.SelectAll();
                        //this.cmbMarkType.Tag = string.Empty;

                        this.cmbMarkType.SelectedIndex = 0;
                        return;
                    }
                    if (!string.IsNullOrEmpty(accountCard.MarkNO))
                    {
                        this.txtMarkNo.Text = accountCard.MarkNO;
                    }
                    if (!string.IsNullOrEmpty(accountCard.MarkType.ID))
                    {
                        this.cmbMarkType.Tag = accountCard.MarkType.ID;
                    }     
                }
                if (MessageBox.Show("是否保存数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Save();
                }
            }
        }



        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            if (this.neuSpread1.ActiveSheet != spPatient && this.neuSpread1.ActiveSheet != spPatientInDay)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow.Tag == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow.Tag == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet != spPatient)
            {
                this.menuItem1.Enabled = false;
            }

            AccountCard tempAccountCard = new AccountCard();
            if (this.neuSpread1.ActiveSheet == spPatient)
            {
                tempAccountCard = this.spPatient.ActiveRow.Tag as AccountCard;
            }
            else
            {
                tempAccountCard = this.spPatientInDay.ActiveRow.Tag as AccountCard;
            }
            if (printCardType.Contains(tempAccountCard.MarkType.ID))
            {
                this.menuItem2.Enabled = true;
            }
            else
            {
                this.menuItem2.Enabled = false;
            }

            this.menu.Show(neuSpread1 as Control, new Point(e.X, e.Y));
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient) return;
            if (this.spPatient.ActiveRow.Tag == null) return;
            AccountCard tempCard = this.spPatient.ActiveRow.Tag as AccountCard;
            if (tempCard.Patient == null)
            {
                MessageBox.Show("查询患者信息失败！");
                return;
            }
            this.ucRegPatientInfo1.CardNO = tempCard.Patient.PID.CardNO;
            this.txtMarkNo.Focus();
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient && this.neuSpread1.ActiveSheet != spPatientInDay) 
                return;
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow.Tag == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow.Tag == null)
            {
                return;
            }
            //if (this.spPatient.ActiveRow.Tag == null && this.spPatientInDay.ActiveRow.Tag == null) 
            //    return;

            AccountCard tempaccontCard = new AccountCard();
            //调用接口处理打印标签
            if (this.neuSpread1.ActiveSheet == this.spPatient)
            {
                tempaccontCard = this.spPatient.ActiveRow.Tag as AccountCard;
            }
            else
            {
                tempaccontCard = this.spPatientInDay.ActiveRow.Tag as AccountCard;
            }

            if (iPrintLable != null)
            {
                iPrintLable.PrintLable(tempaccontCard);
            }
            else
            {
                MessageBox.Show("未能打印，请联系信息科维护标签打印接口[IPrintLable]！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void menuItem3_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient) return;
            if (this.spPatient.ActiveRow.Tag == null) return;
            AccountCard tempCard = this.spPatient.ActiveRow.Tag as AccountCard;
            if (tempCard.Patient == null)
            {
                MessageBox.Show("查询患者信息失败！");
                return;
            }
            this.ucRegPatientInfo1.CardNO = tempCard.Patient.PID.CardNO;
            this.save();
        }

        private void ckIsTreatment_CheckedChanged(object sender, EventArgs e)
        {
            bool bl = ckIsTreatment.Checked;
            this.ucRegPatientInfo1.IsTreatment = bl;
            if (bl)
            {
                this.ucRegPatientInfo1.Clear(true);
                this.txtMarkNo.Focus();
            }
            else
            {
                this.ucRegPatientInfo1.Focus();
            }

        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {

            get
            {
                Type[] vtype = new Type[2];
                vtype[0] = typeof(IPrintLable);
               
                return vtype;
            }
        }

        #endregion

        private void txtMarkNo_Enter(object sender, EventArgs e)
        {
            try
            {
                foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
                {
                    if (input.LayoutName == "美式键盘" || input.LayoutName == "中文(简体) - 美式键盘")
                    {
                        InputLanguage.CurrentInputLanguage = input;
                    }
                }
            }
            catch
            { }
        }

        private void txtMedicalCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                QueryPaitentInfo();
            }
            //外接屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            { 
                Neusoft.HISFC.Models.RADT.PatientInfo showPatientInfo = this.ucRegPatientInfo1 .GetPatientInfomation ();
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add(showPatientInfo);//患者信息
                    lo.Add(this.txtMedicalCardNo .Text );//卡号
                    lo.Add("");//收费员id
                    lo.Add("");//收费员姓名
                    this.ucRegPatientInfo1 .iMultiScreen .ListInfo =lo;                 
            }
        }

        /// <summary>
        /// 查找患者信息
        /// </summary>
        protected virtual void QueryPaitentInfo()
        {
            string medicalCardNo = this.txtMedicalCardNo.Text.Trim();
            if (medicalCardNo == string.Empty)
            {
                return;
            } 
            accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
            int resultValue = accountManager.GetCardByRule(medicalCardNo, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                this.txtMedicalCardNo.Focus();
                this.txtMedicalCardNo.SelectAll();
                return;
            }

            this.txtMedicalCardNo.Text = accountCard.MarkNO;
            this.ucRegPatientInfo1.CardNO = this.accountCard.Patient.PID.CardNO;
        }

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Registration.Register regMgr = new Neusoft.HISFC.BizLogic.Registration.Register();

        private int GetRecipeType = 2;
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            //if (this.valid() == -1)
            //    return 2;

            // 费用明细
            List<AccountCardFee> lstAccFee = null;

            if (this.getValue(out lstAccFee) == -1)
                return 2;

            //if (this.ValidCardNO(this.regObj.PID.CardNO) < 0)
            //{
            //    return -1;
            //}

            //if (this.IsPrompt)
            //{
            //    //确认提示
            //    if (MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("请确认录入数据是否正确"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button1) == DialogResult.No)
            //    {
            //        this.cmbRegLevel.Focus();
            //        return -1;
            //    }
            //}


            int rtn;
            string Err = "";
            ////接口实现{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            //if (this.iProcessRegiter != null)
            //{
            //    rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

            //    if (rtn < 0)
            //    {
            //        MessageBox.Show(Err);
            //        return -1;
            //    }
            //}

            //this.MedcareInterfaceProxy.SetPactCode(this.regObj.Pact.ID);

            #region 账户新增
            //bool isAccountFee = false;
            //decimal vacancy = 0;
            //int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
            //if (result > 0)
            //{
            //    if (!feeMgr.CheckAccountPassWord(this.regObj))
            //        return -1;
            //    if (vacancy > 0)
            //    {
            //        isAccountFee = true;
            //    }

            //}
            #endregion

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(this.regMgr.con);
            //t.BeginTransaction();

            //this.regMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.bookingMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.SchemaMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.patientMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.feeMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //this.SIMgr.SetTrans(t);
            //this.InterfaceMgr.SetTrans(t.Trans);


            //事务开始
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            // 卡费用
            // {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
            AccountCardFee cardFee = null;

            #region 卡费用特殊处理

            //if (chbCardFee.Visible && chbCardFee.Checked)
            //{
            //    AccountCard accountCard = this.txtCardNo.Tag as AccountCard;
            //    if (accountCard != null)
            //    {
            //        cardFee = new AccountCardFee();
            //        cardFee.FeeType = AccCardFeeType.CardFee;
            //        cardFee.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;
            //        cardFee.MarkNO = accountCard.MarkNO;
            //        cardFee.MarkType = accountCard.MarkType;

            //        Neusoft.HISFC.Models.Base.Const obj = cardFee.MarkType as Neusoft.HISFC.Models.Base.Const;
            //        if (obj != null)
            //        {
            //            cardFee.Tot_cost = Neusoft.FrameWork.Function.NConvert.ToDecimal(obj.UserCode);
            //        }
            //        cardFee.Own_cost = cardFee.Tot_cost;

            //        cardFee.IsBalance = false;
            //        cardFee.BalanceNo = "";
            //        cardFee.BalnaceOper.Oper.ID = "";
            //        cardFee.IStatus = 1;

            //    }
            //}

            if (lstAccFee == null)
            {
                lstAccFee = new List<AccountCardFee>();
            }
            if (cardFee != null)
            {
                lstAccFee.Add(cardFee);

                // 处理挂号记录， 卡费用归到挂号表其他费用中
                this.regObj.RegLvlFee.OthFee += cardFee.Tot_cost;
                this.regObj.OwnCost += cardFee.Own_cost;
                this.regObj.PubCost += cardFee.Pub_cost;
                this.regObj.PayCost += cardFee.Pay_cost;
            }

            //如果打印发票则lstAccFee.count > 0；不打印发票lstAccFee.count = 0
            //如果要打印发票的情况，费用必须要有挂号费信息，否则不让挂号
            if (lstAccFee.Count > 0)
            {
                bool isExistRegFee = false;
                foreach (Neusoft.HISFC.Models.Account.AccountCardFee tempCardFee in lstAccFee)
                {
                    if (tempCardFee.FeeType == AccCardFeeType.RegFee)
                    {
                        isExistRegFee = true;
                        break;
                    }
                }

                if (!isExistRegFee)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show("挂号必须要有挂号费信息!", "警告");
                    return -1;
                }

            }

            #endregion

            #region 取发票号

            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";

            Neusoft.HISFC.Models.Base.Employee employee = this.regMgr.Operator as Neusoft.HISFC.Models.Base.Employee;

            //有费用信息的时候才打发票
            if (lstAccFee.Count > 0)
            {

                if (this.GetRecipeType == 1)
                {
                    strInvioceNO = this.regObj.RecipeNO.ToString().PadLeft(12, '0');
                    strRealInvoiceNO = "";
                }
                else
                {
                    if (this.GetRecipeType == 2)
                    {
                        strInvoiceType = "R";
                    }
                    else if (this.GetRecipeType == 3)
                    {
                        // 取门诊收据
                        strInvoiceType = "C";
                    }

                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

                    if (iRes == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strErrText);
                        return -1;
                    }
                }
            }

            this.regObj.InvoiceNO = strInvioceNO;

            #endregion



            #region 处理费用明细信息

            //有费用信息的时候才处理
            if (lstAccFee.Count > 0)
            {

                foreach (AccountCardFee accFee in lstAccFee)
                {
                    accFee.InvoiceNo = strInvioceNO;
                    accFee.Print_InvoiceNo = strRealInvoiceNO;
                    accFee.ClinicNO = this.regObj.ID;

                    accFee.Patient.PID.CardNO = this.regObj.PID.CardNO;
                    accFee.Patient.Name = this.regObj.Name;

                    accFee.IStatus = 1;

                    accFee.FeeOper.Oper.ID = employee.ID;
                    accFee.FeeOper.Oper.Name = employee.Name;
                    accFee.FeeOper.OperTime = current;

                    accFee.Oper.Oper.ID = employee.ID;
                    accFee.Oper.Oper.Name = employee.Name;
                    accFee.Oper.OperTime = current;

                    accFee.IsBalance = false;
                    accFee.BalanceNo = "";

                }

            }

            #endregion

            decimal OwnCostTot = this.regObj.OwnCost;

            #region 账户新增
            //if (isAccountFee)
            //{
            //    decimal cost = 0m;

            //    if (vacancy < OwnCostTot)
            //    {
            //        cost = vacancy;
            //        this.regObj.PayCost = vacancy;
            //        this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
            //    }
            //    else
            //    {
            //        cost = OwnCostTot;
            //        this.regObj.PayCost = this.regObj.OwnCost;
            //        this.regObj.OwnCost = 0;
            //    }
            //    if (this.feeMgr.AccountPay(this.regObj, cost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R") < 0)
            //    {
            //        Neusoft.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(this.feeMgr.Err);
            //        return -1;
            //    }
            //    this.regObj.IsAccount = true;
            //}
            #endregion

            try
            {
                #region 更新看诊序号
                int orderNo = 0;

                //2看诊序号		
                if (this.UpdateSeeID(this.regObj.DoctorInfo.Templet.Dept.ID, this.regObj.DoctorInfo.Templet.Doct.ID,
                    this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                regObj.DoctorInfo.SeeNO = orderNo;

                //专家、专科、特诊、预约号更新排班限额
                #region schema
                //if (this.UpdateSchema(this.SchemaMgr, this.regObj.RegType, ref orderNo, ref Err) == -1)
                //{
                //    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                //    if (Err != "")
                //        MessageBox.Show(Err, "提示");
                //    return -1;
                //}

                //regObj.DoctorInfo.SeeNO = orderNo;
                #endregion

                //1全院流水号			
                if (this.Update(this.regMgr, current, ref orderNo, ref Err) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                regObj.OrderNO = orderNo;
                #endregion

                //预约号更新已看诊标志
                #region booking
                //if (this.regObj.RegType == Neusoft.HISFC.Models.Base.EnumRegType.Pre)
                //{
                //    //更新看诊限额
                //    rtn = this.bookingMgr.Update((this.txtOrder.Tag as Neusoft.HISFC.Models.Registration.Booking).ID,
                //                true, regMgr.Operator.ID, current);
                //    if (rtn == -1)
                //    {
                //        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("更新预约看诊信息出错!" + this.bookingMgr.Err, "提示");
                //        return -1;
                //    }
                //    if (rtn == 0)
                //    {
                //        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("预约挂号信息状态已经变更,请重新检索"), "提示");
                //        return -1;
                //    }
                //}
                #endregion

                #region 待遇接口实现
                //this.MedcareInterfaceProxy.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.Connect();

                //this.MedcareInterfaceProxy.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.BeginTranscation();

                //this.regObj.SIMainInfo.InvoiceNo = this.regObj.InvoiceNO;
                //int returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(this.regObj);
                //if (returnValue == -1)
                //{
                //    this.MedcareInterfaceProxy.Rollback();
                //    Neusoft.FrameWork.Management.PublicTrans.RollBack()
                //        ;
                //    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("上传挂号信息失败!") + this.MedcareInterfaceProxy.ErrMsg);

                //    return -1;
                //}
                //////医保患者登记医保信息
                ////if (this.regObj.Pact.PayKind.ID == "02")
                ////{
                //this.regObj.OwnCost = this.regObj.SIMainInfo.OwnCost;  //自费金额
                //this.regObj.PubCost = this.regObj.SIMainInfo.PubCost;  //统筹金额
                //this.regObj.PayCost = this.regObj.SIMainInfo.PayCost;  //帐户金额
                ////}
                #endregion

                #region addby xuewj 2010-3-15

                //if (this.adt == null)
                //{
                //    this.adt = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IHE.IADT)) as Neusoft.HISFC.BizProcess.Interface.IHE.IADT;
                //}
                //if (this.adt != null)
                //{
                //    this.adt.RegOutPatient(this.regObj);
                //}

                #endregion

                //登记挂号信息
                if (this.regMgr.Insert(this.regObj) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                #region 保存费用明细信息

                if (lstAccFee != null && lstAccFee.Count > 0)
                {
                    if (this.feeMgr.SaveAccountCardFee(lstAccFee) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        //this.MedcareInterfaceProxy.Rollback();
                        MessageBox.Show(this.feeMgr.Err, "提示");
                        return -1;
                    }
                }

                #endregion


                ////更新患者基本信息
                //if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                //{
                //    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                //    this.MedcareInterfaceProxy.Rollback();
                //    MessageBox.Show(Err, "提示");
                //    return -1;
                //}
                ////接口实现{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                //if (this.iProcessRegiter != null)
                //{
                //    //{4F5BD7B2-27AF-490b-9F09-9DB107EA7AA0}
                //    //rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

                //    rtn = this.iProcessRegiter.SaveEnd(ref regObj, ref Err);
                //    if (rtn < 0)
                //    {
                //        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                //        this.MedcareInterfaceProxy.Rollback();
                //        MessageBox.Show(Err);
                //        return -1;
                //    }
                //}

                //处理医保患者
                //this.MedcareInterfaceProxy.UploadRegInfoOutpatient

                #region 发票走号

                //有费用信息的时候，才处理发票
                if (lstAccFee.Count > 0)
                {

                    if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                    {
                        string invoiceStytle = controlParma.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                        if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText) < 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }

                        if (this.feeMgr.InsertInvoiceExtend(strInvioceNO, strInvoiceType, strRealInvoiceNO, "00") < 1)
                        {
                            // 发票头暂时先保存00
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }
                    }

                }

                #endregion

                Neusoft.FrameWork.Management.PublicTrans.Commit();
                //this.MedcareInterfaceProxy.Commit();
                //this.MedcareInterfaceProxy.Disconnect();

                //最后更新处方号,加 1,防止中途返回跳号
                //this.UpdateRecipeNo(1);

                //this.QueryRegLevl();
            }
            catch (Exception e)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            #region 找零放在发票打印后面【废弃】

            ////找零{F0661633-4754-4758-B683-CB0DC983922B}
            //if (this.isShowChangeCostForm)
            //{
            //    rtn = this.ShowChangeForm(this.regObj);
            //    {
            //        if (rtn < 0)
            //        {
            //            return -1;
            //        }
            //    }
            //}

            #endregion

            // 记录发票费用的打印信息
            this.regObj.LstCardFee = lstAccFee;

            //有费用信息的时候，才打印发票
            if (lstAccFee.Count > 0)
            {

                //if (this.isAutoPrint)
                //{
                    this.PrintReg(this.regObj, this.regMgr);
                //}
                //else
                //{
                //    DialogResult rs = MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("请选择是否打印挂号票"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //    if (rs == DialogResult.Yes)
                //    {
                //        this.Print(this.regObj, this.regMgr);
                //    }
                //}

            }
            else if (lstAccFee.Count == 0)
            {
                MessageBox.Show("挂号成功! 不打印发票!", "提示");
            }


            //this.addRegister(this.regObj);

            //this.clear();
            //ChangeInvoiceNOMessage();
            return 0;


        }

        /// <summary>
        /// 根据病历号获得患者挂号信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.Registration.Register getRegInfo(string CardNo)
        {
            if (string.IsNullOrEmpty(CardNo))
            {
                return null;
            }

            Neusoft.HISFC.Models.Registration.Register ObjReg = new Neusoft.HISFC.Models.Registration.Register();
            Neusoft.HISFC.BizProcess.Integrate.RADT radt = new Neusoft.HISFC.BizProcess.Integrate.RADT();
            Neusoft.HISFC.Models.RADT.PatientInfo p;
            int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);

            if (regCount == 1)
            {
                ObjReg.IsFirst = false;
            }
            else
            {
                if (regCount == 0)
                {
                    ObjReg.IsFirst = true;

                }
                else
                {
                    return null;
                }
            }
            //先检索患者基本信息表,看是否存在该患者信息
            p = radt.QueryComPatientInfo(CardNo);

            if (p == null || p.Name == "")
            {
                //不存在基本信息
                ObjReg.PID.CardNO = CardNo;
                //ObjReg.IsFirst = true;
                ObjReg.Sex.ID = "M";
                //ObjReg.Pact.ID = this.DefaultPactID;
            }
            else
            {
                //存在患者基本信息,取基本信息

                ObjReg.PID.CardNO = CardNo;
                ObjReg.Name = p.Name;
                ObjReg.Sex.ID = p.Sex.ID;
                ObjReg.Birthday = p.Birthday;
                ObjReg.Pact.ID = p.Pact.ID;
                ObjReg.Pact.PayKind.ID = p.Pact.PayKind.ID;
                ObjReg.SSN = p.SSN;
                ObjReg.PhoneHome = p.PhoneHome;
                ObjReg.AddressHome = p.AddressHome;
                ObjReg.IDCard = p.IDCard;
                ObjReg.NormalName = p.NormalName;
                ObjReg.IsEncrypt = p.IsEncrypt;
                //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
                ObjReg.IDCard = p.IDCard;

                if (p.IsEncrypt == true)
                {
                    ObjReg.Name = Neusoft.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                }
                //this.chbEncrpt.Checked = p.IsEncrypt;
                ////ObjReg.IsFirst = false;

                //if (this.validCardType(p.IDCardType.ID))//借用Memo存储证件类别
                //{
                //    ObjReg.CardType.ID = p.IDCardType.ID;

                //}
            }

            return ObjReg;
        }

        /// <summary>
        /// 常数管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager conMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <returns></returns>
        private int getValue(out List<AccountCardFee> lstAccFee)
        {
          
            Neusoft.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.patientInfo;
            regObj = getRegInfo(patient.PID.CardNO);
            lstAccFee = null;
            //门诊号
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = Neusoft.HISFC.Models.Base.TransTypes.Positive;//正交易

            this.regObj.DoctorInfo.Templet.RegLevel.ID = RegisterLevel;//this.cmbRegLevel.Tag.ToString();
            //this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
            //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
            this.regObj.DoctorInfo.Templet.RegLevel.IsEmergency = false;

            this.regObj.DoctorInfo.Templet.Dept.ID = registerDept;
            //this.regObj.DoctorInfo.Templet.Dept.Name = this.cmbDept.Text;

            //this.regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            //this.regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.Text;

            //{0BA561B1-376F-4412-AAD0-F19A0C532A03}
            this.regObj.Name = patient.Name;//Neusoft.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'");//患者姓名
            this.regObj.Sex.ID = patient.Sex.ID;// this.cmbSex.Tag.ToString();//性别

            this.regObj.Birthday = patient.Birthday;//this.dtBirthday.Value;//出生日期			

            //Neusoft.HISFC.Models.Registration.RegLevel level = (Neusoft.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            //this.regObj.RegType = Neusoft.HISFC.Models.Base.EnumRegType.Reg;
            ////不为空说明是预约号
            //if (this.txtOrder.Tag != null)
            //{
            //    this.regObj.RegType = Neusoft.HISFC.Models.Base.EnumRegType.Pre;
            //}
            //else if (level.IsSpecial)
            //{
            //    this.regObj.RegType = Neusoft.HISFC.Models.Base.EnumRegType.Spe;
            //}

            //Neusoft.HISFC.Models.Registration.Schema schema = null;

            ////只有专家、专科、特诊需要输入看诊时间段、更新限额
            //if (this.regObj.RegType != Neusoft.HISFC.Models.Base.EnumRegType.Pre
            //            && (level.IsSpecial || level.IsFaculty || level.IsExpert))
            //{
            //    schema = this.GetValidSchema(level);
            //    if (schema == null)
            //    {
            //        MessageBox.Show("预约时间指定错误,没有符合条件的排班信息!", "提示");
            //        this.dtBookingDate.Focus();
            //        return -1;
            //    }
            //    this.SetBookingTag(schema);
            //}


            //if (level.IsExpert && this.regObj.RegType != Neusoft.HISFC.Models.Base.EnumRegType.Pre)
            //{
            //    if (this.VerifyIsProfessor(level, schema) == false)
            //    {
            //        this.cmbRegLevel.Focus();
            //        return -1;
            //    }
            //}


            #region 结算类别
            this.regObj.Pact.ID = patient.Pact.ID;//this.cmbPayKind.Tag.ToString();//合同单位
            //this.regObj.Pact.Name = this.cmbPayKind.Text;

            Neusoft.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("获取代码为:" + this.regObj.Pact.ID + "的合同单位信息出错!" + this.conMgr.Err, "提示");
                return -1;
            }
            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind.Name = pact.PayKind.Name;
            this.regObj.Pact.PayKind.ID = pact.PayKind.ID;
            //this.regObj.SSN = this.txtMcardNo.Text.Trim();//医疗证号

            //if (pact.IsNeedMCard && this.regObj.SSN == "")
            //{
            //    MessageBox.Show("需要输入医疗证号!", "提示");
            //    this.txtMcardNo.Focus();
            //    return -1;
            //}
            ////人员黑名单判断
            //if (this.validMcardNo(this.regObj.Pact.ID, this.regObj.SSN) == -1)
            //    return -1;

            #endregion

            this.regObj.PhoneHome = patient.PhoneHome;//Neusoft.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'");//联系电话
            this.regObj.AddressHome = patient.AddressHome;//Neusoft.FrameWork.Public.String.TakeOffSpecialChar(this.txtAddress.Text.Trim(), "'");//联系地址
            //this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();

            #region 预约时间段
            //if (this.regObj.RegType == Neusoft.HISFC.Models.Base.EnumRegType.Pre)//预约号扣排班限额
            //{
            //    this.regObj.IDCard = (this.txtOrder.Tag as Neusoft.HISFC.Models.Registration.Booking).IDCard;
            //    this.regObj.DoctorInfo.Templet.Noon.ID = (this.txtOrder.Tag as Neusoft.HISFC.Models.Registration.Booking).DoctorInfo.Templet.Noon.ID;
            //    this.regObj.DoctorInfo.Templet.IsAppend = (this.txtOrder.Tag as Neusoft.HISFC.Models.Registration.Booking).DoctorInfo.Templet.IsAppend;
            //    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
            //    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间
            //    this.regObj.DoctorInfo.Templet.ID = (this.txtOrder.Tag as Neusoft.HISFC.Models.Registration.Booking).DoctorInfo.Templet.ID;
            //}
            //else if (level.IsSpecial || level.IsExpert || level.IsFaculty)//专家、专科、特诊号扣排班限额
            //{
            //    this.regObj.DoctorInfo.Templet.Noon.ID = (this.dtBookingDate.Tag as Neusoft.HISFC.Models.Registration.Schema).Templet.Noon.ID;
            //    this.regObj.DoctorInfo.Templet.IsAppend = (this.dtBookingDate.Tag as Neusoft.HISFC.Models.Registration.Schema).Templet.IsAppend;
            //    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
            //    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间
            //    this.regObj.DoctorInfo.Templet.ID = (this.dtBookingDate.Tag as Neusoft.HISFC.Models.Registration.Schema).Templet.ID;

            //}
            //else//其他号不扣限额
            //{
            //    this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
            //    this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
            //            this.dtBegin.Value.ToString("HH:mm:ss"));
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
            //            this.dtEnd.Value.ToString("HH:mm:ss"));

            //    ///如果挂号日期大于今天,为预约挂明日的号,更新挂号时间
            //    ///
            //    if (this.regObj.DoctorInfo.SeeDate.Date < this.dtBookingDate.Value.Date)
            //    {
            //        this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //            this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
            //        this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //        this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //            this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间

            //        this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.Templet.Begin);
            //    }
            //    else
            //    {
            //        this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);
            //    }


            //    if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
            //    {
            //        MessageBox.Show("未维护午别信息,请先维护!", "提示");
            //        return -1;
            //    }
            //    this.regObj.DoctorInfo.Templet.ID = "";
            //}
            #endregion

            //if (this.regObj.Pact.PayKind.ID == "03")//公费日限判断
            //{
            //    if (this.IsAllowPubReg(this.regObj.PID.CardNO, this.regObj.DoctorInfo.SeeDate) == -1)
            //        return -1;
            //}

            regObj.DoctorInfo.Templet.Noon.ID = "1";
            #region 挂号费
            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                MessageBox.Show("获取挂号费出错!" + this.regFeeMgr.Err, "提示");
                //this.cmbRegLevel.Focus();
                return -1;
            }
            if (rtn == 1)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("该挂号级别未维护挂号费,请先维护挂号费"), "提示");
                //this.cmbRegLevel.Focus();
                return -1;
            }

            //获得患者应收、报销
            ConvertCostToObject(regObj, out lstAccFee);

            #endregion

            //处方号
            //  this.regObj.InvoiceNO = this.txtRecipeNo.Text.Trim();
            this.regObj.RecipeNO = this.conMgr.GetConstansObj("RegRecipeNo", regMgr.Operator.ID).Name;//this.txtRecipeNo.Text.Trim();


            this.regObj.IsFee = false;
            this.regObj.Status = Neusoft.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            //add by niuxinyuan
            this.regObj.DoctorInfo.SeeDate = this.regObj.InputOper.OperTime;
            //this.regObj.DoctorInfo.Templet.Noon.Name = this.QeryNoonName(this.regObj.DoctorInfo.Templet.Noon.ID);
            // add by niuxinyuan
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            ArrayList al = new ArrayList();

            //if (this.chbEncrpt.Checked)
            //{
            //    this.regObj.IsEncrypt = true;
            //    this.regObj.NormalName = Neusoft.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
            //    this.regObj.Name = "******";
            //}

            //this.regObj.IDCard = this.txtIdNO.Text;
            this.regObj.IsFee = true;
            return 0;
        }

        /// <summary>
        /// 挂号实体
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register regObj;

        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 更新医生或科室的看诊序号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//医生
                Subject = doctID;
            }
            else
            {
                Type = "2";//科室
                Subject = deptID;
            }

            #endregion

            //更新看诊序号
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //获取看诊序号		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(Neusoft.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //更新看诊序号
            //全院是全天大排序，所以午别不生效，默认 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //获取全院看诊序号
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 系统参数控制
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int ConvertRegFeeToObject(Neusoft.HISFC.Models.Registration.Register obj)
        {
            decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;

            int rtn = this.GetRegFee(obj.Pact.ID, obj.DoctorInfo.Templet.RegLevel.ID,
                          ref regFee, ref chkFee, ref digFee, ref othFee);

            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;

            return rtn;
        }

        private int iFeeDiagReg = 3;

        /// <summary>
        /// 获取挂号费
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee, ref decimal digFee, ref decimal othFee)
        {
            Neusoft.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(pactID, regLvlID);
            if (p == null)//出错
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//没有维护挂号费
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            //判断是否只收取挂号费
            switch (this.iFeeDiagReg)
            {
                case 1:
                    // 收取挂号费
                    chkFee = 0;
                    digFee = 0;
                    break;
                case 2:
                    // 收取诊金
                    regFee = 0;
                    break;
                case 3:
                    // 不收取诊金、挂号
                    regFee = 0;
                    chkFee = 0;
                    digFee = 0;
                    break;

                default:
                    // 默认都收取
                    break;

            }

            return 0;
        }

        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new Neusoft.HISFC.BizLogic.Registration.RegLvlFee();

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lstCardFee"></param>
        private void ConvertCostToObject(Neusoft.HISFC.Models.Registration.Register obj, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            decimal othFee = 0, ownCost = 0, pubCost = 0;
            othFee = obj.RegLvlFee.OthFee; //add by niux
            //lstCardFee = this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
            //        ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            lstCardFee = new List<AccountCardFee>();

            AccountCardFee cardFee = new AccountCardFee();

            cardFee.FeeType = AccCardFeeType.RegFee;
            cardFee.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;
            cardFee.IStatus = 1;
            cardFee.Own_cost = ownCost;
            cardFee.Pub_cost = pubCost;
            cardFee.Tot_cost = ownCost + pubCost;
            lstCardFee.Add(cardFee);

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

        }

        string registerLevel = "1";

        /// <summary>
        /// 默认挂号级别
        /// </summary>
        public string RegisterLevel
        {
            get
            {
                return registerLevel;
            }
            set
            {
                registerLevel = value;
            }
        }

        string registerDept = "2001";

        /// <summary>
        /// 默认挂号科室
        /// </summary>
        public string RegisterDept
        {
            get
            {
                return registerDept;
            }
            set
            {
                registerDept = value;
            }
        }

        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void PrintReg(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.HISFC.BizLogic.Registration.Register regmr)
        {
            #region 屏蔽
            /*if( this.PrintWhat == "Invoice")//打印发票
            {
                this.ucInvoice.Registeration = regObj ;
			
                System.Drawing.Printing.PaperSize size ;

                if( PrintCnt % 2 == 0)
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice1", 425 ,288);
                }
                else
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice2",425,280) ;
                }

                PrintCnt ++ ;

                printer.SetPageSize(size);
                printer.PrintPage(0,0,ucInvoice) ;
            }
            else//打印处方
            {
                //fuck
                neusoft.neuFC.Object.neuObject obj = this.conMgr.Get("PrintRecipe",regObj.RegDept.ID) ;

                //不包含的，都打印
                if( obj == null || obj.ID == "")
                {
                    this.ucBill.Register = regObj ;
					
                    System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Recipe", 670 ,1120);
                    printer.SetPageSize(size);
                    printer.PrintPage(0,0,this.ucBill) ;
                }
            }*/
            #endregion
            #region by niuxy
            /*
            try
            {
                if (IRegPrint != null)
                {
                    this.IRegPrint.RegInfo = regObj;
                    this.IRegPrint.Print();
                }
            }
            catch { }
             */
            #endregion
            Neusoft.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            
            regprint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Registration.IRegPrint)) as Neusoft.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show(Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //regprint.SetPrintValue(regObj,regmr);
            if (regObj.IsEncrypt)
            {
                regObj.Name = Neusoft.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.regObj.NormalName);
            }

            regprint.SetPrintValue(regObj);
            int i = regprint.Print();
            //regprint.PrintView();

        }

        #region 身份证自动读取

        /// <summary>
        /// 
        /// </summary>
        private bool isReaderIDCard = false;

        /// <summary>
        /// 
        /// </summary>
        [Category("控件设置"), Description("是否自动读取身份证"), DefaultValue(false)]
        public bool IsReaderIDCard
        {
            get
            {
                return isReaderIDCard;
            }
            set
            {
                isReaderIDCard = value;

                this.timer1.Enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.ucRegPatientInfo1.Clear(true);
            int i = this.ucRegPatientInfo1.AutoReadIDCard();

            if (i == -2)
            {
                this.timer1.Enabled = false;

                return;
            }
            if (i == 1)
            {
                this.ucRegPatientInfo1_OnEnterSelectPatient(null, null);
                if (this.isQueryPatientInfoByReadIDCard)
                {
                    this.QueryPatientInfo();
                }
                if (this.ucRegPatientInfo1.IsJudgePactByIdno)
                {
                    this.ucRegPatientInfo1.JudgePactByIdno();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucCardManager_Leave(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucCardManager_Enter(object sender, EventArgs e)
        {
            if (IsReaderIDCard)
            {
                this.timer1.Enabled = true;
            }
        }

        #endregion

        /// <summary>
        /// {877A4CAD-6F3A-4c8a-8B8B-949E9160404C}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            string  CardNo = this.txtCardNO.Text.Trim();
            if ( CardNo == string.Empty)
            {
                return;
            }
            CardNo = CardNo.PadLeft(10, '0');
            txtCardNO.Text = CardNo;
            this.ucRegPatientInfo1.CardNO = CardNo;
        }
    }
        
}
