using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IRegPrint
{
    /// <summary>
    /// 挂号发票打印
    /// </summary>
    public partial class ucRegInvoicePrint : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public ucRegInvoicePrint()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        private FS.HISFC.BizLogic.Manager.DepartmentStatManager deptstatmgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        private FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        private FS.HISFC.BizLogic.Manager.Department deptMgt = new FS.HISFC.BizLogic.Manager.Department();

        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        //FS.FrameWork.Public.ObjectHelper payModesHelper = new FS.FrameWork.Public.ObjectHelper();

        //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
        private FS.HISFC.BizLogic.Registration.RegDetail detailMgr = new FS.HISFC.BizLogic.Registration.RegDetail();

        /// <summary>
        /// 打印用的标签集合
        /// </summary>
        public Collection<Label> lblPrint;

        /// <summary>
        /// 预览用的标签集合
        /// </summary>
        public Collection<Label> lblPreview;

        private bool isPreview = false;

        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        #endregion

        #region IRegPrint 成员

        public int Clear()
        {
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);
                    return -1;
                }

                FS.HISFC.Models.Base.PageSize ps = null;
                ps = psManager.GetPageSize("GHFP");

                if (ps == null || string.IsNullOrEmpty(ps.Printer))
                {
                    //没有维护，就采取默认设置
                    //ps = new FS.HISFC.Models.Base.PageSize("GHFP", 500, 275);
                    //ps.Top = 0;
                    //ps.Left = 0;
                    MessageBox.Show("没有找到打印机【GHFP】");
                    return -1;
                }

                //{13E56BF8-16D4-48b9-AD1F-E352C3DDFD73}
                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                //{3E7EFECA-5375-420b-A435-323463A0E56C}
                if (string.IsNullOrEmpty(printerName))
                {
                    return 1;
                }
                print.SetPageSize(ps);
                if (ps != null && !string.IsNullOrEmpty(ps.Printer))
                {
                    print.PrintDocument.PrinterSettings.PrinterName = printerName;//ps.Printer;
                }
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(ps.Left, ps.Top, this);
                }
                else
                {
                    print.PrintPage(ps.Left, ps.Top, this);
                }

                //打印文档的左侧硬页边距的X坐标，这个硬边距和打印机有关，如果硬边距>0，可以设置打印控件的边距值为负数
                //print.PrintDocument.PrinterSettings.DefaultPageSettings.HardMarginX;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this);
            return 0;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                this.InitReceipt();

                #region 费用赋值

                if (register.LstCardFee != null && register.LstCardFee.Count > 0)
                {
                    #region 挂号记录和费用分离

                    decimal regFee = 0m;
                    decimal chkFee = 0m;
                    decimal cardFee = 0m;
                    decimal caseFee = 0m;
                    decimal diaFee = 0m;

                    foreach (FS.HISFC.Models.Account.AccountCardFee accFee in register.LstCardFee)
                    {
                        if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.RegFee)
                        {
                            //挂号费
                            regFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.CaseFee)
                        {
                            //病历本费
                            caseFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.CardFee)
                        {
                            //卡工本费
                            cardFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.ChkFee)
                        {
                            //诊金费
                            chkFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.DiaFee)
                        {
                            //诊金费
                            diaFee += accFee.Tot_cost;
                        }
                    }
                    
                    //挂号费
                    this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regFee, 2) + "元";

                    //诊察费
                    this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(diaFee, 2) + "元";
                    if (register.DoctorInfo.Templet.RegLevel.Name.Contains("特需"))
                    {
                        lblChkFeeTitle.Visible = true;
                    }

                    this.lblBabyCheckTitle.Visible = false;
                    this.lblBabyCheck.Visible = false;
                    if(chkFee>0)
                    {
                        this.lblBabyCheckTitle.Text = "婴儿体检费：";
                        this.lblBabyCheck.Text = FS.FrameWork.Public.String.FormatNumberReturnString(chkFee, 2) + "元";
                        this.lblBabyCheckTitle.Visible = true;
                        this.lblBabyCheck.Visible = true;
                    }

                    //病历本费
                    if (caseFee > 0)
                    {
                        this.lblCaseBook.Text = "病历本费";
                        this.lblCaseBookCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(caseFee, 2) + "元";
                    }

                    // 卡工本费
                    if (cardFee > 0)
                    {
                        this.lblCard.Text = "卡工本费";
                        this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(cardFee, 2) + "元";
                    }

                    //合计
                    this.lblTotCost.Text = "合计：" + (regFee + chkFee + caseFee + cardFee+diaFee).ToString("F2") + " 元";

                    #endregion
                }
                else
                {
                    #region 费用还是保持在fin_opr_register(旧)

                    //挂号费
                    this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.RegFee, 2) +
                        "元";
                    //诊察费
                    this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                         register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                        "元";


                    //病历手册
                    if (register.RegLvlFee.OthFee > 0)
                    {
                        this.lblCaseBook.Text = "病历本费";
                        this.lblCaseBookCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.OthFee, 2) +
                        "元";
                    }

                    // 卡工本费
                    decimal decCardFee = 0;
                    if (register.User01 == "CARDFEE")
                    {
                        decCardFee = FS.FrameWork.Function.NConvert.ToDecimal(register.User02);
                    }

                    if (decCardFee > 0)
                    {
                        this.lblCard.Text = "卡工本费";
                        this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decCardFee, 2) + "元";
                    }
                    this.lblBabyCheckTitle.Visible = false;
                    this.lblBabyCheck.Visible = false;
                    if (register.RegLvlFee.ChkFee > 0)
                    {
                        this.lblBabyCheckTitle.Text = "婴儿检查费";
                        this.lblBabyCheck.Text = FS.FrameWork.Public.String.FormatNumberReturnString(register.RegLvlFee.ChkFee, 2) + "元";
                        this.lblBabyCheckTitle.Visible = true;
                        this.lblBabyCheck.Visible = true;
                    }

                    #endregion

                    //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
                    //List<FS.HISFC.Models.Registration.RegisterFeeDetail> detailList = detailMgr.QueryDetailByInvoiceNO(register.InvoiceNO);

                    //decimal regFee = 0.0m;
                    //decimal giftFee = 0.0m;
                    //decimal cardFee = 0.0m;
                    //decimal ecoFee = 0.0m;

                    //if(detailList != null)
                    //{
                    //    foreach (FS.HISFC.Models.Registration.RegisterFeeDetail detail in detailList)
                    //    {
                    //        if (detail.Memo != "CardFee")
                    //        {
                    //            regFee += detail.Real_cost;
                    //            giftFee += detail.Gift_cost;
                    //            ecoFee += detail.Etc_cost;
                    //        }
                    //        else
                    //        {
                    //            cardFee += detail.Real_cost;
                    //        }
                    //    }
                    //}

                    ////挂号费
                    //this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regFee + giftFee, 2) + "元";

                    ////卡费
                    //if (cardFee > 0)
                    //{
                    //    this.lblCard.Text = "卡费";
                    //    this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(cardFee, 2) + "元";
                    //}

                    ////优惠
                    //if (ecoFee > 0)
                    //{
                    //    this.lblBabyCheckTitle.Text = "优惠";
                    //    this.lblBabyCheck.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ecoFee, 2) + "元";
                    //    this.lblBabyCheckTitle.Visible = true;
                    //    this.lblBabyCheck.Visible = true;
                    //}

                }

                #endregion

                if (string.IsNullOrEmpty(register.DoctorInfo.Templet.Doct.ID)
                    && string.IsNullOrEmpty(register.DoctorInfo.Templet.Dept.ID))
                {
                    //单独病历本挂号票,挂号科室、挂号医生均为空
                    #region 单独病历本挂号票

                    //医院名称
                    this.lblHospitalName.Text = manageIntegrate.GetHospitalName();

                    //挂号发票号
                    this.lblInvoiceNo.Text = register.InvoiceNO;

                    //姓名
                    this.lblPatientName.Text = register.Name + "(" + register.Sex.Name + ") " + regMgr.GetAge(register.Birthday);

                    //挂号员号
                    //{DFC492A3-C37D-43e8-98C0-B459E2924C45}
                   // this.lblRegOper.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(register.InputOper.ID) + "(" + register.InputOper.ID + ")";
                    //{F800DB0B-CD6D-4265-B716-D8649CE7550E}
                    this.lblRegOper.Text = register.InputOper.ID;
                    //挂号时间
                    //{DFC492A3-C37D-43e8-98C0-B459E2924C45}
                    this.lblTime.Text = register.InputOper.OperTime.ToString();

                    //就诊卡号
                    //this.lblCardNo.Text = "就诊卡号：" + register.PID.CardNO;
                    this.lblCardNo.Text = register.PID.CardNO;

                    string strSQL = @"select wm_concat(pay_type)
                                    from (
                                    select  distinct (select y.name from com_dictionary y
                                                where y.type='PAYMODES'
                                                and y.code=f.pay_type) pay_type
                                    from fin_opb_accountcardfee f
                                    --order by f.fee_date desc
                                    where f.invoice_no='{0}')";
                    strSQL = string.Format(strSQL, register.InvoiceNO);
                    string strPayType = assignMgr.ExecSqlReturnOne(strSQL);

                    //缴费方式
                    this.lblPayType.Text = strPayType;

                    #endregion
                }
                else
                {
                    #region 正常情况
                    //医院名称
                    this.lblHospitalName.Text = manageIntegrate.GetHospitalName();

                    //挂号发票号
                    this.lblInvoiceNo.Text = register.InvoiceNO;
                    //挂号科室
                    this.lblDeptName.Text = register.DoctorInfo.Templet.Dept.Name;

                    //挂号级别
                    this.lblRegLevel.Text = register.DoctorInfo.Templet.RegLevel.Name + "    " + (register.RegExtend != null ? register.RegExtend.BookingTypeName : string.Empty);

                    //姓名
                    this.lblPatientName.Text = register.Name + "(" + register.Sex.Name + ") " + regMgr.GetAge(register.Birthday);

                    //挂号员号
                    // //{DFC492A3-C37D-43e8-98C0-B459E2924C45}
                    //this.lblRegOper.Text =  SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(register.InputOper.ID) + "(" + register.InputOper.ID + ")";
                    //{F800DB0B-CD6D-4265-B716-D8649CE7550E}
                    this.lblRegOper.Text = register.InputOper.ID;
                    //就诊日期
                    //this.lblRegDate.Text = register.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm") + FS.SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(register.DoctorInfo.Templet.Begin).Name;
                    this.lblRegDate.Text = register.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd");

                    //合同单位类别
                    this.lblPactName.Text = register.Pact.Name;
                    if (string.IsNullOrEmpty(register.SSN) == false)
                    {
                        this.lblPactNo.Text = "医疗证号：" + register.SSN;
                    }
                    //this.lblTime.Text = "挂号时间：" + register.InputOper.OperTime.ToString();
                    this.lblTime.Text =  register.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd") + " " + register.InputOper.OperTime.ToString("HH:mm:ss");
                    //this.lblCardNo.Text = "就诊卡号：" + register.PID.CardNO;
                    this.lblCardNo.Text = register.PID.CardNO;

                    FS.HISFC.Models.Base.Employee doct = personMgr.GetPersonByID(register.DoctorInfo.Templet.Doct.ID);
                    FS.HISFC.Models.Base.Department dept = dept = deptMgt.GetDeptmentById(register.DoctorInfo.Templet.Dept.ID);

                    //获取科室备注
                    string mark = Function.GetDeptMemo("00", register.DoctorInfo.Templet.Dept.ID);
                    FS.FrameWork.Models.NeuObject neuObject = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetConstant("RegDocAddress", register.DoctorInfo.Templet.Doct.ID);
                    if (neuObject != null && !string.IsNullOrEmpty(neuObject.Name))
                    {
                        mark = neuObject.Name;
                    }

                    FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)register.DoctorInfo.Templet.RegLevel;
                    bool isPrintDocLevel = false;
                    string orderNo = string.Empty;
                    ArrayList arrLevelList = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.QueryConstant("RegPrintDocLevel");

                    foreach (object obj in arrLevelList)
                    {
                        FS.FrameWork.Models.NeuObject levelObject = obj as FS.FrameWork.Models.NeuObject;

                        if (levelObject.ID == Level.ID)
                        {
                            isPrintDocLevel = true;
                            break;
                        }
                    }
                    this.lblDoctor.Text = doct.Name;//打印级别都没有维护，专家职级也没有按排班职级来判断// {6BF1F99D-7307-4d05-B747-274D24174895}

                    if (doct != null && (Level.IsExpert || isPrintDocLevel))
                    {

                        if (DateTime.Compare(register.DoctorInfo.Templet.Begin.Date, register.InputOper.OperTime.Date) > 0)
                        {
                            orderNo = "【预】" + register.OrderNO.ToString() + "(" + register.DoctorInfo.SeeNO + ")";
                        }
                        else
                        {
                            orderNo = register.OrderNO.ToString() + "(" + register.DoctorInfo.SeeNO + ")";
                        }
                    }
                    else
                    {
                        if (DateTime.Compare(register.DoctorInfo.Templet.Begin.Date, register.InputOper.OperTime.Date) > 0)
                        {
                            orderNo = "【预】" + register.OrderNO.ToString() + "(" + register.DoctorInfo.SeeNO + ")";
                        }
                        else
                        {
                            orderNo = register.OrderNO.ToString();
                        }
                    }

                    //查找分诊信息，如果有，则加上分诊内容
                    FS.HISFC.Models.Nurse.Assign assign = assignMgr.QueryByClinicID(register.ID);
                    if (assign != null)
                    {
                        mark += assign.Queue.SRoom.Name;
                    }

                    this.neulblDeptAddress.Text = mark;

                    if (assign != null)
                    {
                        //门诊号
                        this.lblSeeNo.Text = "排队号：" + assign.SeeNO.ToString();

                        FS.HISFC.BizLogic.Nurse.Queue queMgr = new FS.HISFC.BizLogic.Nurse.Queue();
                        ArrayList queobjList = queMgr.QueryByQueueID(assign.Queue.ID);

                        FS.HISFC.Models.Nurse.Queue queobj = null;
                        if (queobjList.Count > 0)
                        {
                            queobj = queobjList[0] as FS.HISFC.Models.Nurse.Queue;
                        }
                        int tmpCnt = queobj.WaitingCount - 1;//减掉自己
                        if (tmpCnt < 0)//HIS遗留问题，可能为负数
                        {
                            tmpCnt = 0;
                        }
                        lblWaitNum.Text = "前面等候人数：" + tmpCnt.ToString();
                    }

                    //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
//                    string strSQL = @"select wm_concat(pay_type)
//                                    from (
//                                    select  distinct (select y.name from com_dictionary y
//                                                where y.type='PAYMODES'
//                                                and y.code=f.pay_type) pay_type
//                                    from fin_opb_accountcardfee f
//                                    --order by f.fee_date desc
//                                    where f.invoice_no='{0}')";

                    string strSQL = @"select wm_concat(pay_type)
                                        from ( select distinct (select y.name|| '  ' || to_char(f.tot_cost) 
                                                                  from com_dictionary y
                                                                 where y.type='PAYMODES'
                                                                   and y.code=f.mode_code) pay_type
                                                 from exp_register_paymode f
                                                where f.invoice_no='{0}')";

                    strSQL = string.Format(strSQL, register.InvoiceNO);
                    string strPayType = assignMgr.ExecSqlReturnOne(strSQL);

                    //缴费方式
                    this.lblPayType.Text = strPayType;
                    #endregion
                }

                //控制根据打印和预览显示选项
                if (IsPreview)
                {
                    SetToPreviewMode();
                }
                else
                {
                    SetToPrintMode();
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public IDbTransaction Trans
        {
            get
            {
                return this.trans.Trans;
            }
            set
            {
                this.trans.Trans = value;
            }
        }

        #endregion

        #region 打印方法

        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据tag标签的值区分开
        /// </remarks>
        private void InitReceipt(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            if (!string.IsNullOrEmpty(l.Text) || l.Text == "印")
                            {
                                l.Text = "";
                            }
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }


        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据tag标签的值区分开
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region 将代印字的打印项值清空
                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "印")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }

        /// <summary>
        /// 设置标签集合的可见性
        /// </summary>
        /// <param name="v">是否可见</param>
        /// <param name="l">标签集合</param>
        private void SetLableVisible(bool v, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Visible = v;
            }
        }


        /// <summary>
        /// 设置打印集合的值
        /// </summary>
        /// <param name="t">值数组</param>
        /// <param name="l">标签集合</param>
        private void SetLableText(string[] t, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Text = "";
            }
            if (t != null)
            {
                if (t.Length <= l.Count)
                {
                    int i = 0;
                    foreach (string s in t)
                    {
                        l[i].Text = s;
                        i++;
                    }
                }
                else
                {
                    if (t.Length > l.Count)
                    {
                        int i = 0;
                        foreach (Label lbl in l)
                        {
                            lbl.Text = t[i];
                            i++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置为打印模式
        /// </summary>
        public void SetToPrintMode()
        {
            //将预览项设为不可见
            SetLableVisible(false, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        /// <summary>
        /// 设置为预览模式
        /// </summary>
        public void SetToPreviewMode()
        {
            //将预览项设为可见
            SetLableVisible(true, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        #endregion

        private void ucRegInvoicePrint_Load(object sender, EventArgs e)
        {

        }
    }
}
