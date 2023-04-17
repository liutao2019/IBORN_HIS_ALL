using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Collections;
using FS.FrameWork.WinForms.Classes;

namespace FS.HISFC.Components.InpatientFee.Prepay
{
    /// <summary>
    /// ucPrepayNew<br></br>
    /// [功能描述: 结算控件]<br></br>
    /// [创 建 者: lingk]<br></br>
    /// [创建时间: 2013-08-19]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>

    public partial class ucPrepayNew : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucPrepayNew()
        {
            this.InitializeComponent();
        }

        #region "变量"
        /// <summary>
        /// 患者基本信息综合实体

        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 入出转integrate层

        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 住院费用业务层


        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 住院费用组合业务层


        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        //控制参数判断
        /// <summary>
        /// 是否打印冲红发票
        /// </summary>
        bool IsPrintReturn = false;

        /// <summary>
        /// 负发票是否走新票号
        /// </summary>
        bool IsReturnNewInvoice = false;

        /// <summary>
        /// 是否可以作废，重打隔天票据
        /// </summary>
        private bool isCanDealBefore = true;

        /// <summary>
        /// 是否可以交叉退预交金
        /// </summary>
        private bool isCanQuitOtherOper = true;

        /// <summary>
        /// 是否打印预交金发票
        /// </summary>
        private bool isPrintInvoice = true;

        /// <summary>
        /// 是否打印签名卡
        /// </summary>
        private bool isPrintPatientSign = false;

        /// <summary>
        /// 发票打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint prepayPrint = null;

        #endregion "属性"

        #region IInterfaceContainer 成员

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint);

                return type;
            }
        }

        #endregion

        #region "属性"

        public FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint PrepayPrint
        {
            set
            {
                this.prepayPrint = value;
            }
        }

        /// <summary>
        /// 是否允许补打以前的发票

        /// </summary>
        [Category("控件设置"), Description("是否可以作废，重打隔天票据")]
        public bool IsCanDealBefore
        {
            get
            {
                return isCanDealBefore;
            }
            set
            {
                isCanDealBefore = value;
            }
        }


        [Category("控件设置"), Description("是否可以交叉退预交金")]
        public bool IsCanQuitOtherOper
        {
            get { return isCanQuitOtherOper; }
            set { isCanQuitOtherOper = value; }
        }

        [Category("控件设置"), Description("是否打印预交金收据")]
        public bool IsPrintInvoice
        {
            get
            {
                return isPrintInvoice;
            }
            set
            {
                isPrintInvoice = value;
            }
        }

        [Category("控件设置"), Description("查询显示患者的状态设置")]
        public FS.HISFC.Components.Common.Controls.enuShowState ShowState
        {
            get
            {
                return this.ucQueryInpatientNo.ShowState;
            }
            set
            {
                this.ucQueryInpatientNo.ShowState = value;
            }
        }

        [Category("控件设置"), Description("是否打印签名卡 是=true，否=false")]
        public bool IsPrintPatientSign
        {
            get
            {
                return isPrintPatientSign;
            }
            set
            {
                isPrintPatientSign = value;
            }
        }

        string strBankPayMode = string.Empty;
        [Category("控件设置"), Description("显示银行与开户账号的支付方式，用'|'分开")]
        public string ShowBankPayMode
        {
            get
            {
                return strBankPayMode;
            }
            set
            {
                strBankPayMode = value;
            }
        }

        #endregion

        #region "方法"
        /// <summary>
        /// 初始化控件信息

        /// </summary>
        public virtual void initControl()
        {
            //初始化默认现金方式

            this.cmbPayType.Tag = "CA";
            this.cmbPayType.Text = "现金";

            //确定选择方式
            this.cmbPayType.IsListOnly = true;
            this.cmbTransType1.IsListOnly = true;
            this.cmbTransType2.IsListOnly = true;
            //初始化farpoint属性

            this.fpPrepay_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpPrepay_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            //初始化住院号控件
            this.ucQueryInpatientNo.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucQueryInpatientNo.TextBox.Size = new System.Drawing.Size(116, 21);
            this.ucQueryInpatientNo.TextBox.Location = new System.Drawing.Point(52, 5);
            this.ucQueryInpatientNo.TextBox.BringToFront();
            //添加支付方式控件事件
            this.cmbPayType.KeyDown += new KeyEventHandler(cmbPayType_KeyDown);
            this.cmbPayType.KeyPress += new KeyPressEventHandler(cmbPayType_KeyPress);
            this.cmbPayType.SelectedIndexChanged += new EventHandler(cmbPayType_SelectedIndexChanged);

            this.cmbTransType1.KeyDown += new KeyEventHandler(cmbTransType1_KeyDown);
            this.cmbTransType1.KeyPress += new KeyPressEventHandler(cmbTransType1_KeyPress);
            this.cmbTransType1.SelectedIndexChanged += new EventHandler(cmbTransType1_SelectedIndexChanged);

            this.cmbTransType2.KeyDown += new KeyEventHandler(cmbTransType2_KeyDown);
            this.cmbTransType2.KeyPress += new KeyPressEventHandler(cmbTransType2_KeyPress);
            this.cmbTransType2.SelectedIndexChanged += new EventHandler(cmbTransType2_SelectedIndexChanged);
            //显示下一张收据号
            this.GetNextInvoiceNO();


            ArrayList alBanks = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (alBanks == null || alBanks.Count <= 0)
            {
                MessageBox.Show("获取银行列表失败!");
                return;
            }
            this.cmbBank.AddItems(alBanks);

            this.cmbBank.KeyDown += new KeyEventHandler(cmbBank_KeyDown);
            //  this.cmbBank.SelectedIndexChanged +=new EventHandler(cmbBank_SelectedIndexChanged);

            this.txtPreCost.KeyDown += new KeyEventHandler(txtPreCost_KeyDown);
            this.txtPreCost1.KeyDown += new KeyEventHandler(txtPreCost1_KeyDown);
            this.txtPreCost2.KeyDown += new KeyEventHandler(txtPreCost2_KeyDown);

            this.txtMark.KeyDown += new KeyEventHandler(txtMark_KeyDown);

            this.pnlBankInfo.Visible = false;



        }

        /// <summary>
        /// 读取控制类信息
        /// </summary>
        private int ReadControlInfo()
        {
            FS.FrameWork.Management.ControlParam controlParm = new FS.FrameWork.Management.ControlParam();
            try
            {
                this.IsPrintReturn = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100015"));
                this.IsReturnNewInvoice = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100016"));
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("读取控制类信息出错!", 211);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 查询患者预交金信息
        /// </summary>
        /// <param name="patientInfo">住院患者基本信息实体</param>
        /// <returns>1 成功 －1失败</returns>
        protected virtual int QueryPatientPrepay(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //添加行


            ArrayList al = new ArrayList();

            try
            {
                //根据住院号提取患者预交金信息到ArrayList
                al = this.feeInpatient.QueryPrepays(patientInfo.ID);
                if (al == null) return 0;
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(ex.Message, 211);
                return -1;
            }
            this.fpPrepay_Sheet1.RowCount = 0;
            this.fpPrepay_Sheet1.RowCount = al.Count;
            //交款次数
            int PayCount = 0;
            //返款次数
            int WasteCount = 0;
            Hashtable hsCount = new Hashtable();
            Hashtable hsOrgInfo = new Hashtable();

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
                prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)al[i];

                string PrepayState = "";
                if (prepay.FT.PrepayCost > 0)
                {
                    if (!hsCount.ContainsKey(prepay.RecipeNO))
                    {
                        hsCount.Add(prepay.RecipeNO, prepay.RecipeNO);
                        PayCount++;
                    }
                    PrepayState = "收取";
                }
                else
                {
                    WasteCount++;
                    switch (prepay.PrepayState)
                    {
                        case "1":
                            PrepayState = "作废";

                            break;
                        case "2":
                            PrepayState = "补打";
                            break;
                        default:
                            PrepayState = "收取";
                            break;
                    }
                }
                //更新一些没有的字段()
                string PrepaySource = "";
                if (prepay.TransferPrepayState == "0")
                {
                    PrepaySource = "预交金";
                }
                else
                {
                    PrepaySource = "转押金";
                }
                //结算标记
                string BalanceFlag = "";
                if (prepay.BalanceState == "0")
                {
                    BalanceFlag = "未结算";
                }
                else
                {
                    BalanceFlag = "已结算";
                }
                //收款员姓名


                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = managerIntergrate.GetEmployeeInfo(prepay.PrepayOper.ID);

                if (empl == null)
                { prepay.PrepayOper.Name = ""; }
                else
                {
                    prepay.PrepayOper.Name = empl.Name;
                }
                //取到原有的预交金收取人
                if (!hsOrgInfo.ContainsKey(prepay.RecipeNO))
                {
                    if (prepay.BalanceState == "1") //记录已结算的，可能存在结算召回显示问题
                    {
                        hsOrgInfo.Add(prepay.RecipeNO, prepay.PrepayOper.Name);
                    }
                }
                else
                {
                    if (prepay.BalanceState == "0") //结算召回
                    {
                        prepay.PrepayOper.Name = hsOrgInfo[prepay.RecipeNO].ToString();
                    }
                }
                //支付方式

                FS.FrameWork.Models.NeuObject payObj = this.managerIntegrate.GetConstant("PAYMODES", prepay.PayType.ID);
                if (payObj == null)
                {
                    MessageBox.Show("获得支付方式定义信息出错!" + this.managerIntegrate.Err);

                    return -1;
                }

                //添加farpoint显示内容
                //{4E569A30-8655-4461-86B8-450BD5D245D4}
                //Object[] o = new Object[] { prepay.RecipeNO, PrepayState, prepay.FT.PrepayCost, payObj.Name, PrepaySource, BalanceFlag, prepay.PrepayOper.Name, prepay.PrepayOper.OperTime.ToString() };
                Object[] o = new Object[] { prepay.RecipeNO, PrepayState, prepay.FT.PrepayCost, payObj.Name, PrepaySource, BalanceFlag, prepay.PrepayOper.Name, prepay.OrgInvoice.ID, prepay.PrepayOper.OperTime.ToString(), prepay.User02 };

                for (int j = 0; j <= o.GetUpperBound(0); j++)
                {
                    try
                    {
                        fpPrepay_Sheet1.Cells[i, j].Value = o[j];
                    }
                    catch (Exception ex)
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg(ex.Message, 211);
                        return -1;
                    }
                }
                if (prepay.PrepayState != "0") this.fpPrepay_Sheet1.Cells[i, 1].ForeColor = System.Drawing.Color.Red;
                fpPrepay_Sheet1.Rows[i].Tag = prepay;
            }
            //返还交款次数
            this.txtPayNum.Text = PayCount.ToString();
            this.txtBackNum.Text = WasteCount.ToString();
            //余额
            if (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtFreeCost.Text), 2) < 0)
            {
                this.txtFreeCost.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.txtFreeCost.ForeColor = System.Drawing.Color.Black;
            }
            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        protected virtual void Clear()
        {

            this.patientInfo = null;
            txtSumPreCost.Text = "";
            this.txtTotCost.Text = "";
            this.txtName.Text = "";
            this.txtDept.Text = "";
            this.txtPact.Text = "";
            this.txtBedNo.Text = "";
            this.txtOwnCost.Text = "";
            txtFreeCost.Text = "";
            txtBirthday.Text = "";
            txtNurseStation.Text = "";
            txtDateIn.Text = "";
            txtDoctor.Text = "";
            this.cmbPayType.Tag = "CA";
            this.cmbPayType.Text = "现金";
            this.cmbPayType.bank = new FS.HISFC.Models.Base.Bank();
            this.fpPrepay_Sheet1.RowCount = 0;
            this.txtPayNum.Text = "";
            this.txtBackNum.Text = "";
            this.txtPreCost.Text = "";//预交金额清空
            this.txtPreCost2.Text = "";//预交金额清空
            this.txtPreCost1.Text = "";//预交金额清空
            this.cmbTransType1.Text = "";
            this.cmbTransType2.Text = "";
            this.txtIntimes.Text = "";
            this.txtClinicDiagnose.Text = "";
            this.txtDIST.Text = "";
            this.txtBirthArea.Text = "";
            //显示下一张收据号
            this.GetNextInvoiceNO();
        }

        /// <summary>
        /// 利用患者信息实体进行控件赋值
        /// </summary>
        /// <param name="patientInfo">患者基本信息实体</param>
        protected virtual void EvaluteByPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }
            //预交金总额
            this.txtSumPreCost.Text = patientInfo.FT.PrepayCost.ToString();
            //费用金额
            this.txtTotCost.Text = patientInfo.FT.TotCost.ToString();
            // 姓名
            this.txtName.Text = patientInfo.Name;
            // 科室
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            // 合同单位
            this.txtPact.Text = patientInfo.Pact.Name;
            //床号
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            //自费金额
            this.txtOwnCost.Text = patientInfo.FT.OwnCost.ToString();
            //余额
            txtFreeCost.Text = patientInfo.FT.LeftCost.ToString();
            //生日
            txtBirthday.Text = patientInfo.Birthday.ToString("yyyy-MM-dd");
            //所属病区



            txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            //入院日期
            txtDateIn.Text = patientInfo.PVisit.InTime.ToString("yyyy-MM-dd");
            // 医生
            txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            //住院号

            //备注
            this.txtMark.Text = patientInfo.Memo.ToString();
          
            //籍贯
            try
            {
                this.txtDIST.Text = patientInfo.DIST.ToString();
                //出生地
                this.txtBirthArea.Text = patientInfo.AreaCode.ToString();
            }
            catch { }

            this.ucQueryInpatientNo.Text = patientInfo.PID.PatientNO;

            //门诊诊断
            this.txtClinicDiagnose.Text = patientInfo.ClinicDiagnose;
            //住院次数
            this.txtIntimes.Text = patientInfo.InTimes.ToString();

        }

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("收取", "收取患者的预交金", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J借入, true, false, null);
            toolBarService.AddToolButton("返还", "返还患者预交金", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J借出, true, false, null);
            //toolBarService.AddToolButton("作废", "返还患者预交金", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J借出, true, false, null);
            toolBarService.AddToolButton("清屏", "清空信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("重打", "预交金发票重打(走号)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);
            toolBarService.AddToolButton("补打", "预交金发票补打(不走号)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("帮助", "打开帮助文件", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);

            toolBarService.AddToolButton("补打退费发票", "补打退费签名单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            toolBarService.AddToolButton("更新发票号", "更新下一发票号", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F分票, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //string tempText = string.Empty;

            //try
            //{
            //    tempText = this.hsToolBar[e.ClickedItem.Text].ToString();
            //}
            //catch (Exception ex)
            //{
            //    return;
            //}

            ButtonClicked(e.ClickedItem.Text);

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 响应键盘、鼠标事件
        /// </summary>
        /// <param name="tempText">工具栏按钮名称</param>
        private void ButtonClicked(string tempText)
        {
            switch (tempText)
            {
                case "收取":

                    this.ReceivePrepay();
                    break;
                case "返还":
                case "作废":

                    this.ReturnPrepay();
                    break;
                case "清屏":

                    this.Clear();
                    this.ucQueryInpatientNo.Text = "";
                    this.ucQueryInpatientNo.Focus();
                    break;
                case "重打":
                    this.ReprintPrepay();
                    break;
                case "补打":
                    this.QueryAndPrintPrepay();
                    break;
                case "帮助":
                    break;
                case "退出":
                    {
                        this.FindForm().Close();
                        break;
                    }
                case "补打退费发票":
                    this.RePrintPrepayPatientSign();
                    break;
                case "更新发票号":
                    FS.HISFC.Components.Common.Forms.frmUpdateUsedInvoiceNo frmUpdate = new FS.HISFC.Components.Common.Forms.frmUpdateUsedInvoiceNo();
                    frmUpdate.InvoiceType = "P";
                    frmUpdate.ShowDialog();

                    GetNextInvoiceNO();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 打印预交金
        /// 王宇修改， 控制冲红票打印负数，并且注明作废字样
        /// 增加了[bool]isReturn参数，如果是冲红票为True,正常收取票为False
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="prepay"></param>
        /// <param name="isReturn"></param>
        protected virtual void PrintPrepayInvoice(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alPrepay, bool isReturn)
        {
            if (patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            this.prepayPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint;
            //regprint.SetPrintValue(regObj,regmr);
            //this.prepayPrint = new ucPrepayPrint();
            if (this.prepayPrint == null)
            {
                //this.prepayPrint = new ucPrepayPrint();
                return;
            }


            this.prepayPrint.SetValue(patientInfo, alPrepay);
            this.prepayPrint.Print();


        }

        /// <summary>
        /// 打印预交金
        /// 王宇修改， 控制冲红票打印负数，并且注明作废字样
        /// 增加了[bool]isReturn参数，如果是冲红票为True,正常收取票为False
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="prepay"></param>
        /// <param name="isReturn"></param>
        protected virtual void PrintPrepayInvoice(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay prepay, bool isReturn)
        {
            if (patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            this.prepayPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint;
            //regprint.SetPrintValue(regObj,regmr);
            //this.prepayPrint = new ucPrepayPrint();
            if (this.prepayPrint == null)
            {
                //this.prepayPrint = new ucPrepayPrint();
                return;
            }


            this.prepayPrint.SetValue(patientInfo, prepay);
            this.prepayPrint.Print();


        }

        protected virtual void PrintPrepayPatientSign(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            if (patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint)) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (IBillPrint == null)
            {
                return;
            }
            string errInfo = string.Empty;
            if (IBillPrint.SetData(patientInfo, prepay, ref errInfo) < 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("设置病人签名卡打印数据失败，原因：" + errInfo, 211);
                return;
            }
            IBillPrint.Print();
        }

        /// <summary>
        /// 预交金收取
        /// </summary>
        protected virtual void ReceivePrepay()
        {
            //{645F3DDE-4206-4f26-9BC5-307E33BD882C}
            string errText = string.Empty;
            if (!feeIntegrate.AfterDayBalanceCanFee(this.feeInpatient.Operator.ID, true, ref errText))
            {
                MessageBox.Show(errText);
                return;
            }

            //判断患者
            if (this.patientInfo == null)
            {
                return;
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return;
            }
            //金额判断
            decimal prepayCost = 0m;
            decimal prepayCost1 = 0m;
            decimal prepayCost2 = 0m;
            try
            {
                prepayCost = decimal.Parse(this.txtPreCost.Text);
            }
            catch
            {
                prepayCost = 0;
                this.txtPreCost.Text = "0.00";
            }
            try
            {
                prepayCost1 = decimal.Parse(this.txtPreCost1.Text);
            }
            catch
            {
                prepayCost1 = 0;
                this.txtPreCost1.Text = "0.00";
            }
            try
            {
                prepayCost2 = decimal.Parse(this.txtPreCost2.Text);
            }
            catch
            {
                prepayCost2 = 0;
                this.txtPreCost2.Text = "0.00";
            }
            if (prepayCost == 0 && prepayCost2 == 0 && prepayCost1 == 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请输入预交金金额!", 111);
                this.txtPreCost.Focus();
                this.txtPreCost.SelectAll();
                return;
            }
            if ((prepayCost + prepayCost2 + prepayCost1) < 0)
            {

                FS.FrameWork.WinForms.Classes.Function.Msg("预交金额应大于零!", 111);
                this.txtPreCost.Focus();
                this.txtPreCost.SelectAll();
                return;

            }
            string strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(prepayCost + prepayCost2 + prepayCost1);
            if (MessageBox.Show(strTemp, "预交金金额：", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            //判断支付方式
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择支付方式！", 111);
                this.cmbPayType.Focus();
                return;
            }
            //判断回车确认住院号

            if (this.patientInfo.PID.PatientNO != this.ucQueryInpatientNo.Text)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请回车确认住院号", 111);
                return;
            }
            //判断封帐
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", 111);
                return;
            }

            //事务连接
            //FS.FrameWork.Management.Transaction t = new Transaction(this.feeInpatient.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.Person.SetTrans(t.Trans);
            //建立新插入预交金实体
            FS.HISFC.Models.Fee.Inpatient.Prepay newPrepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            FS.HISFC.Models.Fee.Inpatient.Prepay newPrepay1 = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            FS.HISFC.Models.Fee.Inpatient.Prepay newPrepay2 = new FS.HISFC.Models.Fee.Inpatient.Prepay();

            //提取发票号码
            //发票类型-预交金
            ArrayList alPrepay = new ArrayList();

            string InvoiceNo = "";
            //InvoiceNo = this.feeIntegrate.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.P);
            InvoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");
            if (InvoiceNo == null || InvoiceNo.Trim() == "")
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.Msg("请领取发票!", 111);
                return;
            }
            //财务组


            FS.FrameWork.Models.NeuObject finGroup = new FS.FrameWork.Models.NeuObject();
            finGroup = this.feeInpatient.GetFinGroupInfoByOperCode(this.feeInpatient.Operator.ID);

            newPrepay.RecipeNO = InvoiceNo;

            //实体赋值
            if (this.pnlBankInfo.Visible)
            {
                FS.HISFC.Models.Base.Bank bank = new FS.HISFC.Models.Base.Bank();
                bank.ID = this.cmbBank.Tag.ToString();
                if (!string.IsNullOrEmpty(bank.ID))
                {
                    bank.Name = this.cmbBank.SelectedItem.Name;
                    bank.Account = this.txtBankAccount.Text.Trim();
                    cmbPayType.bank = bank;
                }

            }

            if (this.cmbPayType.Tag != null && decimal.Parse(this.txtPreCost.Text) > 0)
            {
                newPrepay.Name = this.patientInfo.Name;
                newPrepay.PrepayOper.ID = this.feeInpatient.Operator.ID;
                newPrepay.PrepayOper.Name = this.feeInpatient.Operator.Name;
                newPrepay.FT.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPreCost.Text);
                newPrepay.Bank = this.cmbPayType.bank.Clone();
                newPrepay.PayType.ID = this.cmbPayType.Tag.ToString();
                newPrepay.Dept = this.patientInfo.PVisit.PatientLocation.Dept.Clone();
                newPrepay.BalanceState = "0";
                newPrepay.BalanceNO = 0;
                newPrepay.PrepayState = "0";
                newPrepay.IsTurnIn = false;
                newPrepay.FinGroup.ID = finGroup.ID;
                newPrepay.PrepayOper.OperTime = DateTime.Parse(this.feeInpatient.GetSysDateTime());
                newPrepay.TransferPrepayState = "0";

                //正常收或退预交金 ext_falg = "1";与结算召回区分，用字段 User01  By Maokb 060804
                newPrepay.User01 = "1";

                //增加备注字段
                newPrepay.User02 = this.txtMark.Text;

                //调用业务层组合业务


                if (this.feeInpatient.PrepayManager(this.patientInfo, newPrepay) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("收取失败!" + feeInpatient.Err, 211);
                    return;
                }
                else
                {
                    alPrepay.Add(newPrepay);
                }
            }

            if (this.cmbTransType1.Tag != null && decimal.Parse(this.txtPreCost1.Text) > 0)
            {
                if (this.cmbTransType1.Tag.ToString() == this.cmbPayType.Tag.ToString())
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("前两种支付方式相同!", 211);
                    return;
                }

                //实体赋值，银行实体暂时不理，貌似这边没有要求
                newPrepay1.RecipeNO = InvoiceNo;

                newPrepay1.Name = this.patientInfo.Name;
                newPrepay1.PrepayOper.ID = this.feeInpatient.Operator.ID;
                newPrepay1.PrepayOper.Name = this.feeInpatient.Operator.Name;
                newPrepay1.FT.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPreCost1.Text);
                newPrepay1.Bank = this.cmbPayType.bank.Clone();
                newPrepay1.PayType.ID = this.cmbTransType1.Tag.ToString();
                newPrepay1.Dept = this.patientInfo.PVisit.PatientLocation.Dept.Clone();
                newPrepay1.BalanceState = "0";
                newPrepay1.BalanceNO = 0;
                newPrepay1.PrepayState = "0";
                newPrepay1.IsTurnIn = false;
                newPrepay1.FinGroup.ID = finGroup.ID;
                newPrepay1.PrepayOper.OperTime = DateTime.Parse(this.feeInpatient.GetSysDateTime());
                newPrepay1.TransferPrepayState = "0";

                //正常收或退预交金 ext_falg = "1";与结算召回区分，用字段 User01  By Maokb 060804
                newPrepay1.User01 = "1";

                //增加备注字段
                newPrepay1.User02 = this.txtMark.Text;

                if (this.feeInpatient.PrepayManager(this.patientInfo, newPrepay1) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("收取失败!" + feeInpatient.Err, 211);
                    return;
                }
                else
                {
                    alPrepay.Add(newPrepay1);
                }
            }

            if (this.cmbTransType2.Tag != null && decimal.Parse(this.txtPreCost2.Text) > 0)
            {
                if (this.cmbTransType2.Tag.ToString() == this.cmbPayType.Tag.ToString())
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("最后支付方式与第一支付方式相同!", 211);
                    return;
                }
                if (this.cmbTransType2.Tag.ToString() == this.cmbTransType1.Tag.ToString())
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("后两种支付方式相同!", 211);
                    return;
                }

                //实体赋值，银行实体暂时不理，貌似这边没有要求
                newPrepay2.RecipeNO = InvoiceNo;

                newPrepay2.Name = this.patientInfo.Name;
                newPrepay2.PrepayOper.ID = this.feeInpatient.Operator.ID;
                newPrepay2.PrepayOper.Name = this.feeInpatient.Operator.Name;
                newPrepay2.FT.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPreCost2.Text);
                newPrepay2.Bank = this.cmbPayType.bank.Clone();
                newPrepay2.PayType.ID = this.cmbTransType2.Tag.ToString();
                newPrepay2.Dept = this.patientInfo.PVisit.PatientLocation.Dept.Clone();
                newPrepay2.BalanceState = "0";
                newPrepay2.BalanceNO = 0;
                newPrepay2.PrepayState = "0";
                newPrepay2.IsTurnIn = false;
                newPrepay2.FinGroup.ID = finGroup.ID;
                newPrepay2.PrepayOper.OperTime = DateTime.Parse(this.feeInpatient.GetSysDateTime());
                newPrepay2.TransferPrepayState = "0";

                //正常收或退预交金 ext_falg = "1";与结算召回区分，用字段 User01  By Maokb 060804
                newPrepay2.User01 = "1";

                //增加备注字段
                newPrepay2.User02 = this.txtMark.Text;

                if (this.feeInpatient.PrepayManager(this.patientInfo, newPrepay2) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("收取失败!" + feeInpatient.Err, 211);
                    return;
                }
                else
                {
                    alPrepay.Add(newPrepay2);
                }
            }

            //刷新余额标记
            this.txtFreeCost.Text = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtFreeCost.Text), 2) + newPrepay.FT.PrepayCost + newPrepay1.FT.PrepayCost + newPrepay2.FT.PrepayCost).ToString();
            this.txtSumPreCost.Text = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtSumPreCost.Text), 2) + newPrepay.FT.PrepayCost + newPrepay1.FT.PrepayCost + newPrepay2.FT.PrepayCost).ToString();

            #region HL7发送消息到平台
            if (InterfaceManager.GetIADT() != null)
            {
                ArrayList alprepay = new ArrayList();
                alprepay.Add(newPrepay);
                if (InterfaceManager.GetIADT().Prepay(this.patientInfo, alprepay, "2") < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this, "个人体检取消登记失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;

                }
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.Msg("预交金收取成功!", 111);
            //打印预交金发票


            //重新检索预交金记录
            this.QueryPatientPrepay(this.patientInfo);

            if (isPrintInvoice)
            {
                this.PrintPrepayInvoice(this.patientInfo, alPrepay, false);
            }

            //DialogResult dia;
            //frmNotice frmNotice = new frmNotice();
            //frmNotice.label1.Text = "是否收取预交金?";

            //frmNotice.ShowDialog();

            //dia = frmNotice.dr;

            //if (dia == DialogResult.No)
            //{
            //    //DialogResult diaWarning = MessageBox.Show("确定预览时没有打印预交金发票吗？误操作会造成浪费一张发票！", "警告！",
            //    //                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            //    //if (diaWarning == DialogResult.Yes)
            //    //{
            //    //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    //    return;
            //    //}
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    return;
            //}

            //FS.FrameWork.Management.PublicTrans.Commit();
            //FS.FrameWork.WinForms.Classes.Function.Msg("预交金收取成功!", 111);

            //
            this.txtPreCost.Text = "";
            this.txtPreCost1.Text = "";
            this.txtPreCost2.Text = "";
            this.txtMark.Text = "";
            //显示下一张收据号
            this.GetNextInvoiceNO();
            this.ucQueryInpatientNo.Focus();

        }

        /// <summary>
        /// 预交金返还判断
        /// </summary>
        /// <param name="prepay"></param>
        /// <returns></returns>
        private bool ValidReturnPrepay(FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            //{645F3DDE-4206-4f26-9BC5-307E33BD882C}
            string errText = string.Empty;
            if (!feeIntegrate.AfterDayBalanceCanFee(this.feeInpatient.Operator.ID, true, ref errText))
            {
                MessageBox.Show(errText);
                return false;
            }

            if (prepay.PrepayState == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该预交金已经作废!不能进行再次作废操作!", 111);
                return false;
            }
            if (prepay.PrepayState == "2")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该预交金已经进行过补打操作,已经成为作废发票,不能再作废!", 111);
                return false;
            }
            if (prepay.BalanceState == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该票据已经结算过不能作废!!", 111);
                return false;
            }
            #region 作废,日结后也可以做返还操作
            //if (prepay.Memo == "1")
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg("该票据已经日结不能作废!!", 111);
            //    return false;
            //}
            #endregion
            if (prepay.TransferPrepayState == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该预交金为结算的转押金还没有进行正常打印操作,不能作废!", 111);
                return false;
            }
            if (!isCanDealBefore)
            {
                if (prepay.PrepayOper.OperTime < feeInpatient.GetDateTimeFromSysDateTime().Date)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("不能作废当天前的预交金!", 111);
                    return false;
                }
            }

            if (!isCanQuitOtherOper)
            {
                if ((prepay.PrepayOper.ID != feeInpatient.Operator.ID) && (prepay.PrepayOper.OperTime.Date == feeInpatient.GetDateTimeFromSysDateTime().Date))
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("该发票为操作员" + prepay.PrepayOper.ID + "收取,您没有权限作废！", 111);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 预交金返还
        /// </summary>
        protected virtual void ReturnPrepay()
        {
            //有效操作判断
            if (this.fpPrepay_Sheet1.ActiveRowIndex < 0) return;
            if (this.fpPrepay_Sheet1.Rows.Count <= 0) return;
            if (this.patientInfo == null)
            {
                return;
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return;
            }

            FS.HISFC.Models.Fee.Inpatient.Prepay prepayOne = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            prepayOne = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.ActiveRow.Tag;

            if (prepayOne == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择一条预交金记录", 111);
                return;
            }

            prepayOne = feeInpatient.QueryPrePay(this.patientInfo.ID, prepayOne.ID);
            if (prepayOne == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("查询预交金信息失败！" + feeInpatient.Err, 111);
                return;
            }

            //存储需要退的预交金实体数组
            ArrayList alPrepay = new ArrayList();

            if (!ValidReturnPrepay(prepayOne)) return;

            if (!string.IsNullOrEmpty(prepayOne.RecipeNO))
            {
                for (int i = 0; i < this.fpPrepay_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.Prepay prepayTmp = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.Rows[i].Tag;
                    if (prepayTmp != null)
                    {
                        if (!string.IsNullOrEmpty(prepayTmp.RecipeNO) && prepayTmp.RecipeNO.ToString() == prepayOne.RecipeNO.ToString()
                            && prepayTmp.BalanceState == prepayOne.BalanceState)
                        {
                            prepayTmp = feeInpatient.QueryPrePay(this.patientInfo.ID, prepayTmp.ID);
                            if (prepayTmp == null)
                            {
                                FS.FrameWork.WinForms.Classes.Function.Msg("查询预交金信息失败！" + feeInpatient.Err, 111);
                                return;
                            }
                            if (!ValidReturnPrepay(prepayTmp))
                                return;
                            alPrepay.Add(prepayTmp);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            DialogResult r = FS.FrameWork.WinForms.Classes.Function.Msg("是否作废发票号为" + prepayOne.RecipeNO + "的预交金?", 422);
            if (r == DialogResult.No) return;
            //判断封帐
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", 111);
                return;
            }
            if (alPrepay == null || alPrepay.Count == 0)
            {
                return;
            }

            HISFC.Components.InpatientFee.Controls.frmMulTransType frmMul = new FS.HISFC.Components.InpatientFee.Controls.frmMulTransType();
            frmMul.PrePays = alPrepay;
            DialogResult dr = frmMul.ShowDialog();
            if (dr == DialogResult.OK)
            {
                alPrepay = frmMul.PrePays;
            }
            else
            {
                return;
            }

            //退费支付方式提示
            string strRetPrepayInfo = frmMul.GetRetPrePaysInfo(); 

            //事务连接
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in alPrepay)
            {
                //原有发票号码
                prepay.OrgInvoice.ID = prepay.RecipeNO;
                //判断负记录走新发票号码
                if (this.IsReturnNewInvoice)
                {
                    //提取发票号码
                    //发票类型-预交金
                    string InvoiceNo = "";
                    //InvoiceNo = this.feeIntegrate.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.P);
                    InvoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");
                    if (InvoiceNo == null || InvoiceNo == "")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.Msg("提取发票出错!", 211);
                        return;
                    }
                    prepay.RecipeNO = InvoiceNo;
                }
                //{EC04199C-9F39-48f1-BCFE-98430D9962E6}
                prepay.IsPrint = IsPrintReturn;

                #region 作废
                //HISFC.Components.InpatientFee.Controls.ucTransType trasType = new FS.HISFC.Components.InpatientFee.Controls.ucTransType(prepay);
                //if (trasType.ShowDialog() != DialogResult.OK)
                //{
                //    return;
                //}
                #endregion

                //调用业务层组合业务返还预交金
                if (this.feeInpatient.PrepayManagerReturn(this.patientInfo, prepay) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err + "作废失败!", 211);
                    return;
                }
                //刷新余额标记
                this.txtFreeCost.Text = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtFreeCost.Text), 2) + prepay.FT.PrepayCost).ToString();
                this.txtSumPreCost.Text = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtSumPreCost.Text), 2) + prepay.FT.PrepayCost).ToString();

                #region HL7发送消息到平台
                if (InterfaceManager.GetIADT() != null)
                {
                    ArrayList alprepay = new ArrayList();
                    alprepay.Add(prepay);
                    if (InterfaceManager.GetIADT().Prepay(this.patientInfo, alprepay, "1") < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, "个人体检取消登记失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;

                    }
                }

                #endregion
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.Msg("预交金退费成功!\n" + strRetPrepayInfo, 111);
            //重新检索预交金记录
            this.QueryPatientPrepay(this.patientInfo);
            //打印冲红发票;
            if (this.IsPrintReturn)
            {
                this.PrintPrepayInvoice(this.patientInfo, alPrepay, true);
            }

            if (this.isPrintPatientSign)
            {
                this.PrintPrepayPatientSign(this.patientInfo, prepayOne);
            }
        }

        /// <summary>
        /// 预交金重打，作废原来号码，产生新的一个号
        /// </summary>
        protected virtual void ReprintPrepay()
        {
            if (this.fpPrepay_Sheet1.ActiveRowIndex < 0) return;
            if (this.fpPrepay_Sheet1.Rows.Count <= 0) return;
            if (this.patientInfo == null)
            {
                return;
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return;
            }

            ArrayList alPrepay = new ArrayList();

            FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.ActiveRow.Tag;

            if (prepay == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择一条预交金记录", 111);
                return;
            }
            if (!string.IsNullOrEmpty(prepay.RecipeNO))
            {
                for (int i = 0; i < this.fpPrepay_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.Prepay prepayTmp = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.Rows[i].Tag;
                    if (prepayTmp != null)
                    {
                        if (!string.IsNullOrEmpty(prepayTmp.RecipeNO) && prepayTmp.RecipeNO.ToString() == prepay.RecipeNO.ToString())
                        {
                            alPrepay.Add(prepayTmp);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            if (alPrepay == null || alPrepay.Count == 0)
            {
                return;
            }
            else
            {
                FS.FrameWork.Management.ControlParam controlParm = new FS.FrameWork.Management.ControlParam();
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prePayT in alPrepay)
                {
                    if (prePayT.PrepayState == "1")
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("该预交金已经作废!不能进行重打操作!", 111);
                        return;
                    }
                    if (prePayT.PrepayState == "2")
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("该预交金已经进行过重打操作,已经成为作废发票,不能再重打!", 111);
                        return;
                    }
                    if (prePayT.BalanceState == "1")
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("该票据已经结算过不能重打!!", 111);
                        return;
                    }
                    if (prePayT.TransferPrepayState == "1")
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("该预交金为结算的转押金还没有进行正常打印操作,不能重打!", 111);
                        return;
                    }

                    string limitDays = "";

                    limitDays = controlParm.QueryControlerInfo("100022");
                    if (limitDays == null || limitDays == "")
                        limitDays = "";
                    if (limitDays.Trim() != "")
                    {
                        if ((this.feeInpatient.GetDateTimeFromSysDateTime().Date - prePayT.PrepayOper.OperTime.Date).Days > FS.FrameWork.Function.NConvert.ToInt32(limitDays))
                        {
                            FS.FrameWork.WinForms.Classes.Function.Msg("预交金发生间隔超过" + limitDays + "天,不能进行重打操作!", 111);
                            return;
                        }
                    }
                }
            }
            DialogResult r = FS.FrameWork.WinForms.Classes.Function.Msg("是否重打发票号为" + prepay.RecipeNO + "的预交金?", 422);
            if (r == DialogResult.No) return;
            //判断封帐
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", 111);
                return;
            }
            //事务连接
            //Transaction t = new Transaction(this.feeInpatient.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //提取发票号码
            //发票类型-预交金
            ArrayList alprepay = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepayRp in alPrepay)
            {
                string returnInvoice = "";
                if (this.IsReturnNewInvoice)
                {
                    returnInvoice = this.feeIntegrate.GetNewInvoiceNO("P");
                    if (returnInvoice == null || returnInvoice == "")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.Msg("提取发票出错!", 211);
                        return;
                    }
                }

                string invoiceNo = "";
                invoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");
                if (invoiceNo == null || invoiceNo == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg("提取发票出错!", 211);
                    return;
                }
                //调用组合业务处理正负记录
                FS.HISFC.Models.Fee.Inpatient.Prepay returnPrepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
                //{EC04199C-9F39-48f1-BCFE-98430D9962E6}
                prepayRp.IsPrint = IsPrintReturn;
                if (this.feeInpatient.PrepaySignOperation(prepayRp, this.patientInfo, invoiceNo, returnInvoice, ref returnPrepay) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err + "重打失败!", 211);
                    return;
                }

                alprepay.Add(returnPrepay);
                #region HL7发送消息到平台
                if (InterfaceManager.GetIADT() != null)
                {
                    alprepay.Add(prepayRp);

                    if (InterfaceManager.GetIADT().Prepay(this.patientInfo, alprepay, "2") < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, "个人体检取消登记失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;

                    }
                }

                #endregion
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (this.IsPrintReturn)
            {
                this.PrintPrepayInvoice(this.patientInfo, alprepay, true);
            }
            //打印预交金发票
            this.PrintPrepayInvoice(this.patientInfo, alPrepay, false);
            //重新检索预交金记录
            this.QueryPatientPrepay(this.patientInfo);
            //显示下一张收据号
            this.GetNextInvoiceNO();
            FS.FrameWork.WinForms.Classes.Function.Msg("重打完毕！", 111);
        }

        /// <summary>
        /// 预交金补打，直接查询预交金信息打印，不走号
        /// </summary>
        protected virtual void QueryAndPrintPrepay()
        {
            if (this.fpPrepay_Sheet1.ActiveRowIndex < 0) return;
            if (this.fpPrepay_Sheet1.Rows.Count <= 0) return;
            if (this.patientInfo == null)
            {
                return;
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return;
            }

            ArrayList alPrepay = new ArrayList();
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.ActiveRow.Tag;

            if (prepay == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择一条预交金记录", 111);
                return;
            }
            if (!string.IsNullOrEmpty(prepay.RecipeNO))
            {
                for (int i = 0; i < this.fpPrepay_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.Prepay prepayTmp = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.Rows[i].Tag;
                    if (prepayTmp != null)
                    {
                        if (!string.IsNullOrEmpty(prepayTmp.RecipeNO) && prepayTmp.RecipeNO.ToString() == prepay.RecipeNO.ToString())
                        {
                            alPrepay.Add(prepayTmp);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            if (alPrepay == null || alPrepay.Count == 0)
            {
                return;
            }

            bool isCallBack = false;
            bool isReprint = false;
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepayRp in alPrepay)
            {
                if (prepayRp.PrepayState == "1" && prepayRp.BalanceState == "1")
                {
                    isCallBack = true;
                }
                if (prepayRp.PrepayState == "0" && prepayRp.BalanceState == "0")
                {
                    isReprint = true;
                }
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepayRp in alPrepay)
            {
                if (isCallBack && isReprint)
                {
                    patientInfo.User01 = "RePrint";
                }
                else
                {
                    if (prepayRp.PrepayState == "1")
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("该预交金已经作废!不能进行补打操作!", 111);
                        return;
                    }

                    if (prepayRp.BalanceState == "1")
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("该票据已经结算过不能补打!!", 111);
                        return;
                    }
                }
               
                if (prepayRp.PrepayState == "2")
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("该预交金已经进行过补打操作,已经成为作废发票,不能再补打!", 111);
                    return;
                }
               
                if (prepayRp.TransferPrepayState == "1")
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("该预交金为结算的转押金还没有进行正常打印操作,不能补打!", 111);
                    return;
                }
            }

            DialogResult r = FS.FrameWork.WinForms.Classes.Function.Msg("是否补打发票号为" + prepay.RecipeNO + "的预交金?", 422);
            if (r == DialogResult.No) return;
            //判断封帐
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", 111);
                return;
            }


            //打印预交金发票

            this.PrintPrepayInvoice(this.patientInfo, alPrepay, false);

            FS.FrameWork.WinForms.Classes.Function.Msg("补打完毕！", 111);

            //重新检索预交金记录
            this.QueryPatientPrepay(this.patientInfo);
        }

        /// <summary>
        /// 补打退费签名单
        /// </summary>
        protected void RePrintPrepayPatientSign()
        {
            if (this.fpPrepay_Sheet1.ActiveRowIndex < 0) return;
            if (this.fpPrepay_Sheet1.Rows.Count <= 0) return;
            if (this.patientInfo == null)
            {
                return;
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return;
            }

            FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.ActiveRow.Tag;

            if (prepay == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择一条预交金记录", 111);
                return;
            }

            if (prepay.PrepayState != "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该预交金还没有返回!不能进行退费补打签名单操作!", 111);
                return;
            }

            if (prepay.FT.PrepayCost > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("退费补打签名操作的金额必须为负数!", 111);
                return;
            }

            DialogResult r = FS.FrameWork.WinForms.Classes.Function.Msg("是否补打发票号为" + prepay.RecipeNO + "的预交金退费签名单?", 422);
            if (r == DialogResult.No) return;
            //判断封帐
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", 111);
                return;
            }

            if (this.isPrintPatientSign)
            {
                this.PrintPrepayPatientSign(this.patientInfo, prepay);
            }

            FS.FrameWork.WinForms.Classes.Function.Msg("补打完毕！", 111);

            //重新检索预交金记录
            this.QueryPatientPrepay(this.patientInfo);
        }

        /// <summary>
        /// 获取下一打印发票号
        /// {4914954F-6464-41e9-AFCB-4F0ABFD626AE}
        /// </summary>
        protected void GetNextInvoiceNO()
        {
            lblNextInvoiceNO.Text = "";
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string invoiceNO = "";
            string realInvoiceNO = "";
            string errText = "";

            this.feeIntegrate.GetInvoiceNO(oper, "P", ref invoiceNO, ref realInvoiceNO, ref errText);

            if (string.IsNullOrEmpty(invoiceNO))
            {
                //未领取发票则弹出窗口输入
                FS.HISFC.Components.Common.Forms.frmUpdateInvoice frm = new FS.HISFC.Components.Common.Forms.frmUpdateInvoice();
                frm.InvoiceType = "P";
                frm.ShowDialog(this);

                int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "P", ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    MessageBox.Show(errText);
                    return;
                }
            }

            lblNextInvoiceNO.Text = "电脑号： " + invoiceNO + ", 印刷号：" + realInvoiceNO;
        }

        /// <summary>
        /// 获取支付方式信息(用于界面提示)
        /// </summary>
        /// <returns></returns>
        private string GetPayModesMsg()
        {
            string ret = string.Empty;
            if (this.cmbPayType.SelectedItem != null && FS.FrameWork.Function.NConvert.ToDecimal(this.txtPreCost.Text) > 0)
            {
                ret += this.cmbPayType.SelectedItem.Name + "：" + this.txtPreCost.Text + "\n";
            }
            if (this.cmbTransType1.SelectedItem != null && FS.FrameWork.Function.NConvert.ToDecimal(this.txtPreCost1.Text) > 0)
            {
                ret += this.cmbTransType1.SelectedItem.Name + "：" + this.txtPreCost1.Text + "\n";
            }
            if (this.cmbTransType2.SelectedItem != null && FS.FrameWork.Function.NConvert.ToDecimal(this.txtPreCost2.Text) > 0)
            {
                ret += this.cmbTransType2.SelectedItem.Name + "：" + this.txtPreCost2.Text + "\n";
            }
            return ret;
        }

        #endregion

        #region "事件"

        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPrepay_Load(object sender, EventArgs e)
        {
            //初始化控件

            this.initControl();
            //重新初始化工具栏
            //try
            //{
            //    Function.RefreshToolBar(this.hsToolBar, ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).toolBar1, "预交金管理");
            //}
            //catch { }

            //设置窗体控件的输入法状态为半角
            FS.HISFC.Components.Common.Classes.Function.SetIme(this);

            this.ucQueryInpatientNo.Focus();
            this.ucQueryInpatientNo.Select();
        }

        void cmbPayType_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtPreCost.Focus();
        }

        void cmbTransType1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtPreCost1.Focus();
        }

        void cmbTransType2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtPreCost2.Focus();
        }

        void cmbPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string[] strBanks = this.strBankPayMode.Split('|');
                foreach (string str in strBanks)
                {
                    if (string.IsNullOrEmpty(str)) continue;
                    if (str == this.cmbPayType.Tag.ToString())
                    {
                        this.pnlBankInfo.Visible = true;
                        this.txtPreCost.SelectAll();
                        txtPreCost.Focus();
                        break;
                    }
                }

                this.pnlBankInfo.Visible = false;
                this.txtPreCost.SelectAll();
                txtPreCost.Focus();
            }
        }

        void cmbTransType1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string[] strBanks = this.strBankPayMode.Split('|');
                foreach (string str in strBanks)
                {
                    if (string.IsNullOrEmpty(str))
                        continue;
                    if (str == this.cmbTransType1.Tag.ToString())
                    {
                        this.pnlBankInfo.Visible = true;
                        this.txtPreCost1.SelectAll();
                        txtPreCost1.Focus();
                        break;
                    }
                }

                this.pnlBankInfo.Visible = false;
                this.txtPreCost1.SelectAll();
                txtPreCost1.Focus();
            }
        }

        void cmbTransType2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string[] strBanks = this.strBankPayMode.Split('|');
                foreach (string str in strBanks)
                {
                    if (string.IsNullOrEmpty(str))
                        continue;
                    if (str == this.cmbTransType2.Tag.ToString())
                    {
                        this.pnlBankInfo.Visible = true;
                        this.txtPreCost2.SelectAll();
                        txtPreCost2.Focus();
                        break;
                    }
                }

                this.pnlBankInfo.Visible = false;
                this.txtPreCost2.SelectAll();
                txtPreCost2.Focus();
            }
        }

        void cmbPayType_SelectedIndexChanged(object sender, EventArgs e)
        {

            string[] strBanks = this.strBankPayMode.Split('|');
            foreach (string str in strBanks)
            {
                if (string.IsNullOrEmpty(str)) continue;
                if (str == this.cmbPayType.Tag.ToString())
                {
                    this.pnlBankInfo.Visible = true;
                    this.txtPreCost.SelectAll();
                    txtPreCost.Focus();
                    return;
                }
            }
            this.pnlBankInfo.Visible = false;
            this.txtPreCost.SelectAll();
            txtPreCost.Focus();
        }

        void cmbTransType1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string[] strBanks = this.strBankPayMode.Split('|');
            foreach (string str in strBanks)
            {
                if (string.IsNullOrEmpty(str))
                    continue;
                if (str == this.cmbTransType1.Tag.ToString())
                {
                    this.pnlBankInfo.Visible = true;
                    this.txtPreCost1.SelectAll();
                    txtPreCost1.Focus();
                    return;
                }
            }
            this.pnlBankInfo.Visible = false;
            this.txtPreCost1.SelectAll();
            txtPreCost1.Focus();
        }

        void cmbTransType2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string[] strBanks = this.strBankPayMode.Split('|');
            foreach (string str in strBanks)
            {
                if (string.IsNullOrEmpty(str))
                    continue;
                if (str == this.cmbTransType2.Tag.ToString())
                {
                    this.pnlBankInfo.Visible = true;
                    this.txtPreCost2.SelectAll();
                    txtPreCost2.Focus();
                    return;
                }
            }
            this.pnlBankInfo.Visible = false;
            this.txtPreCost2.SelectAll();
            txtPreCost2.Focus();
        }

        void cmbBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.txtBankAccount.SelectAll();
                txtBankAccount.Focus();
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                if (this.cmbBank.SelectedIndex < this.cmbBank.Items.Count - 1)
                    this.cmbBank.SelectedIndex++;
                else
                    this.cmbBank.SelectedIndex = 0;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                if (this.cmbBank.SelectedIndex > 0)
                    this.cmbBank.SelectedIndex--;
                else
                {
                    this.cmbBank.SelectedIndex = this.cmbBank.Items.Count - 1;
                }
            }
        }
        void cmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBankAccount.SelectAll();
            txtBankAccount.Focus();
        }

        void txtPreCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string[] strBanks = this.strBankPayMode.Split('|');
                foreach (string str in strBanks)
                {
                    if (string.IsNullOrEmpty(str)) continue;
                    if (str == this.cmbPayType.Tag.ToString())
                    {
                        this.cmbBank.SelectAll();
                        cmbBank.Focus();
                        return;
                    }
                }
                this.cmbTransType1.SelectAll();
                this.cmbTransType1.Focus();
            }
        }

        void txtPreCost1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string[] strBanks = this.strBankPayMode.Split('|');
                foreach (string str in strBanks)
                {
                    if (string.IsNullOrEmpty(str))
                        continue;
                    if (str == this.cmbTransType1.Tag.ToString())
                    {
                        this.cmbBank.SelectAll();
                        cmbBank.Focus();
                        return;
                    }
                }
                this.cmbTransType2.SelectAll();
                this.cmbTransType2.Focus();
            }
        }

        void txtPreCost2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string[] strBanks = this.strBankPayMode.Split('|');
                foreach (string str in strBanks)
                {
                    if (string.IsNullOrEmpty(str))
                        continue;
                    if (str == this.cmbTransType2.Tag.ToString())
                    {
                        this.cmbBank.SelectAll();
                        cmbBank.Focus();
                        return;
                    }
                }

                this.txtMark.SelectAll();
                this.txtMark.Focus();

            }
           
        }

        void txtMark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string msg = this.GetPayModesMsg();
                if (string.IsNullOrEmpty(msg) == false)
                {
                    DialogResult dr = MessageBox.Show(msg + "确认收取?", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        this.ReceivePrepay();
                    }
                    else
                    {
                        this.cmbPayType.Focus();
                    }
                }
                else
                {
                    this.ReceivePrepay();
                }
            }
        }

        private void ucQueryInpatientNo_myEvent()
        {
            //清空
            this.Clear();
            this.fpPrepay_Sheet1.RowCount = 0;

            //判断是否有该患者
            if (this.ucQueryInpatientNo.InpatientNo == null || this.ucQueryInpatientNo.InpatientNo.Trim() == "")
            {
                if (string.IsNullOrEmpty(ucQueryInpatientNo.Err))
                {
                    ucQueryInpatientNo.Err = "此患者不在院!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo.Err, 211);

                this.ucQueryInpatientNo.Focus();
                return;
            }
            //获取住院号赋值给实体
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo.InpatientNo);

            if (this.patientInfo == null) MessageBox.Show(this.radtIntegrate.Err);



            if ((FS.HISFC.Models.Base.EnumInState)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumInState), this.patientInfo.PVisit.InState.ID.ToString()) == FS.HISFC.Models.Base.EnumInState.N
                || (FS.HISFC.Models.Base.EnumInState)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumInState), this.patientInfo.PVisit.InState.ID.ToString()) == FS.HISFC.Models.Base.EnumInState.O)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者已经出院!", 111);

                this.patientInfo.ID = null;

                return;
            }

            //控件赋值患者信息



            this.EvaluteByPatientInfo(this.patientInfo);



            //读取控制类参数

            if (this.ReadControlInfo() == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("提取控制信息出错!", 211);
                this.Clear();
                return;
            }

            //判断未打印的转押金



            ArrayList alForegift = new ArrayList();
            //判断是否存在未打印转押金
            alForegift = this.feeInpatient.QueryForegif(this.patientInfo.ID);
            if (alForegift == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                this.Clear();
                return;
            }
            //{64BD57CE-9361-41f6-AE91-2618CBA5047A}
            ArrayList alCallBacePrepay = feeInpatient.QueryCallBackPrePay(this.patientInfo.ID);
            if (alCallBacePrepay == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                this.Clear();
                return;
            }

            if (!IsPrintReturn)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay p in alCallBacePrepay)
                {
                    if (p.PrepayState != "0")
                    {
                        alCallBacePrepay.Remove(p);
                    }
                }
            }

            int count = alCallBacePrepay.Count + alForegift.Count;

            if (count > 0)
            {
                //{64BD57CE-9361-41f6-AE91-2618CBA5047A}
                DialogResult r = MessageBox.Show("患者有" + count.ToString() + "笔预交金没有打印,是否打印?", "提示", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {

                    string errText = string.Empty;
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in alForegift)
                    {
                        //提取发票号码
                        //发票类型-预交金
                        //string InvoiceNo = "";
                        //InvoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");

                        //if (InvoiceNo == null || InvoiceNo == "")
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err,211);
                        //    return;
                        //}
                        ////					
                        //prepay.RecipeNO = InvoiceNo;
                        //prepay.PrepayOper.ID = this.feeInpatient.Operator.ID;
                        //prepay.PrepayOper.Name = this.feeInpatient.Operator.Name;

                        ////打印转押金发票
                        //this.PrintPrepayInvoice(this.patientInfo, prepay, false);

                        if (PrintForgift(prepay, ref errText) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(errText, 211);
                            return;
                        }
                        //更新转押金发票号码和状态
                        if (feeInpatient.UpdateForgift(this.patientInfo, prepay) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                            return;
                        }

                    }

                    foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in alCallBacePrepay)
                    {
                        if (PrintForgift(prepay, ref errText) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(errText, 211);
                            return;
                        }

                        if (feeInpatient.UpdateCallBackPrePay(patientInfo, prepay) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                            return;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();


                    FS.FrameWork.WinForms.Classes.Function.Msg("发票打印完毕!", 111);
                }

            }

            if (this.QueryPatientPrepay(this.patientInfo) == -1)
            {
                this.Clear();
                this.fpPrepay_Sheet1.Rows.Count = 0;
                return;
            }
            this.cmbPayType.Focus();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucQueryInpatientNo_myEvent();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 打印转押金和召回发票
        /// </summary>
        /// <param name="prepay"></param>
        /// <param name="errText"></param>
        private int PrintForgift(FS.HISFC.Models.Fee.Inpatient.Prepay prepay, ref string errText)
        {
            string InvoiceNo = "";
            InvoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");

            if (InvoiceNo == null || InvoiceNo == "")
            {
                errText = this.feeInpatient.Err;
                return -1;
            }
            //					
            prepay.RecipeNO = InvoiceNo;
            prepay.PrepayOper.ID = this.feeInpatient.Operator.ID;
            prepay.PrepayOper.Name = this.feeInpatient.Operator.Name;

            //打印转押金发票
            this.PrintPrepayInvoice(this.patientInfo, prepay, false);
            return 1;
        }
        #endregion

        #region 快捷键


        /// <summary>
        /// toolBar映射
        /// </summary>
        protected Hashtable hsToolBar = new Hashtable();

        /// <summary>
        /// 按键设置
        /// </summary>
        /// <param name="keyData">当前按键</param>
        /// <returns>继续执行True False 当前处理结束</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 执行快捷键

        /// </summary>
        /// <param name="key">当前按键</param>
        private bool ExecuteShotCut(Keys key)
        {
            string opName = Function.GetOperationName("预交金管理", key.GetHashCode().ToString());

            if (opName == "") return false;

            ButtonClicked(opName);

            return true;

        }

        #endregion

        /// <summary>
        /// 单击时全选

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPreCost_Click(object sender, EventArgs e)
        {
            this.txtPreCost.SelectAll();
            txtPreCost.Focus();
        }


        #region 树操作
        /// <summary>
        /// 接收树选择的患者基本信息
        /// </summary>
        /// <param name="neuObject">患者基本信息实体</param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID == "")
            {
                return -1;
            }

            QueryInpatientNo(this.patientInfo.ID);
            return 0;
        }

        private void QueryInpatientNo(string inpatientno)
        {
            //清空
            this.Clear();
            this.fpPrepay_Sheet1.RowCount = 0;

            //判断是否有该患者
            if (inpatientno == null || inpatientno == "")
            {
                if (this.ucQueryInpatientNo.Err == "")
                {
                    ucQueryInpatientNo.Err = "此患者不在院!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo.Err, 211);

                this.ucQueryInpatientNo.Focus();
                return;
            }
            //获取住院号赋值给实体
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(inpatientno);

            if (this.patientInfo == null) MessageBox.Show(this.radtIntegrate.Err);



            //if ((FS.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == FS.HISFC.Models.Base.EnumInState.N
            //    || (FS.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == FS.HISFC.Models.Base.EnumInState.O)
            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者已经出院!", 111);

                this.patientInfo.ID = null;

                return;
            }

            //控件赋值患者信息



            this.EvaluteByPatientInfo(this.patientInfo);



            //读取控制类参数

            if (this.ReadControlInfo() == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("提取控制信息出错!", 211);
                this.Clear();
                return;
            }

            //判断未打印的转押金



            ArrayList alForegift = new ArrayList();
            //判断是否存在未打印转押金
            alForegift = this.feeInpatient.QueryForegif(this.patientInfo.ID);
            if (alForegift == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                this.Clear();
                return;
            }
            //{64BD57CE-9361-41f6-AE91-2618CBA5047A}
            ArrayList alCallBacePrepay = feeInpatient.QueryCallBackPrePay(this.patientInfo.ID);
            if (alCallBacePrepay == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                this.Clear();
                return;
            }

            if (!IsPrintReturn)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay p in alCallBacePrepay)
                {
                    if (p.PrepayState != "0")
                    {
                        alCallBacePrepay.Remove(p);
                    }
                }
            }

            int count = alCallBacePrepay.Count + alForegift.Count;

            if (count > 0)
            {
                //{64BD57CE-9361-41f6-AE91-2618CBA5047A}
                DialogResult r = MessageBox.Show("患者有" + count.ToString() + "笔预交金没有打印,是否打印?", "提示", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {

                    string errText = string.Empty;
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in alForegift)
                    {
                        //提取发票号码
                        //发票类型-预交金
                        //string InvoiceNo = "";
                        //InvoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");

                        //if (InvoiceNo == null || InvoiceNo == "")
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err,211);
                        //    return;
                        //}
                        ////					
                        //prepay.RecipeNO = InvoiceNo;
                        //prepay.PrepayOper.ID = this.feeInpatient.Operator.ID;
                        //prepay.PrepayOper.Name = this.feeInpatient.Operator.Name;

                        ////打印转押金发票
                        //this.PrintPrepayInvoice(this.patientInfo, prepay, false);

                        if (PrintForgift(prepay, ref errText) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(errText, 211);
                            return;
                        }
                        //更新转押金发票号码和状态
                        if (feeInpatient.UpdateForgift(this.patientInfo, prepay) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                            return;
                        }

                    }

                    foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in alCallBacePrepay)
                    {
                        if (PrintForgift(prepay, ref errText) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(errText, 211);
                            return;
                        }

                        if (feeInpatient.UpdateCallBackPrePay(patientInfo, prepay) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err, 211);
                            return;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();


                    FS.FrameWork.WinForms.Classes.Function.Msg("发票打印完毕!", 111);
                }

            }

            if (this.QueryPatientPrepay(this.patientInfo) == -1)
            {
                this.Clear();
                this.fpPrepay_Sheet1.Rows.Count = 0;
                return;
            }
            this.cmbPayType.Focus();
        }

        #endregion

        private void gbPrepayInfo_Enter(object sender, EventArgs e)
        {
            GetNextInvoiceNO();
        }

    }
}
