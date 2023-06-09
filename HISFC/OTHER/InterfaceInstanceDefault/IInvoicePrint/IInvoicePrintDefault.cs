using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace InterfaceInstanceDefault.IInvoicePrint
{
    /// <summary>
    /// 门诊发票打印接口实现
    /// </summary>
    public partial class IInvoicePrintDefault : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint
    {
        public IInvoicePrintDefault()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 分发票后的支付方式
        /// </summary>
        private string setPayModeType;
        /// <summary>
        /// 分发票后的支付方式
        /// </summary>
        private string splitinvoicepaymode;
        /// <summary>
        /// 设置是否为预览模式
        /// </summary>
        private bool isPreView = false;
        /// <summary>
        /// 数据库连接
        /// </summary>
        private System.Data.IDbTransaction trans;
        #endregion

        #region 函数
        #region IInvoicePrint 成员

        /// <summary>
        /// 控件描述，最好填写。
        /// </summary>
        public string Description
        {
            get { return "发票打印实例"; }
        }

        /// <summary>
        /// 设置是否为预览模式
        /// </summary>
        public bool IsPreView
        {
            set { this.isPreView = value; }
        }
        /// <summary>
        /// 打印自身
        /// </summary>
        /// <returns>-1 失败 1 成功</returns>
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
                string paperName=string.Empty ;
                //if (this.InvoiceType == "MZ05")
                //{
                //    paperName = "MZTK";

                //}
                //else if (this.InvoiceType == "MZ01")
                //{
                paperName = "MZFP";
                //}

                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();
                ////纸张宽度
                //ps.Width = this.Width;
                ////纸张高度
                //ps.Height = this.Height;
                //上边距
                ps.Top = 0;
                //左边距
                ps.Left = 0;               
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
                while (alName.Count>0)
                {
                    SetPrintValueOther();
                    PrintOther(); 
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// 打印自身
        /// </summary>
        /// <returns>-1 失败 1 成功</returns>
        public int PrintOther()
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
                string paperName = string.Empty;
                //if (this.InvoiceType == "MZ05")
                //{
                //    paperName = "MZTK";

                //}
                //else if (this.InvoiceType == "MZ01")
                //{
                paperName = "MZFP";
                //}

                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize(paperName, 0, 0);
                ////纸张宽度
                //ps.Width = this.Width;
                ////纸张高度
                //ps.Height = this.Height;
                //上边距
                ps.Top = 0;
                //左边距
                ps.Left = 0;
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 打印其他内容
        /// </summary>
        /// <returns>-1 失败 1 成功</returns>
        public int PrintOtherInfomation()
        {
            return 1;
        }
        /// <summary>
        /// 分发票后的支付方式
        /// </summary>
        public string SetPayModeType
        {
            set { this.setPayModeType = value; }
        }
        /// <summary>
        /// 设置是否为预览模式
        /// </summary>
        public void SetPreView(bool isPreView)
        {
            this.isPreView = isPreView;
        }
        /// <summary>
        /// 设置打印其他内容
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="Invoices">所有主发票信息</param>
        /// <param name="invoiceDetails">所有发票明细信息</param>
        /// <param name="feeDetails">所有费用信息</param>
        /// <returns></returns>
        public int SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, System.Collections.ArrayList Invoices, System.Collections.ArrayList invoiceDetails, System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

       

        Control c;
        ArrayList alName = new ArrayList();
        ArrayList alValue = new ArrayList();
        decimal  drugTotCost = 0;
        FS.HISFC.Models.Registration.Register regInfoOther;
        FS.HISFC.Models.Fee.Outpatient.Balance invoiceOther;

        /// <summary>
        /// 设置发票打印内容
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="invoice">发票主表信息</param>
        /// <param name="alInvoiceDetail">发票明细信息</param>
        /// <param name="alFeeItemList">费用明细信息</param>
        /// <param name="isPreview">是否预览模式</param>
        /// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList alInvoiceDetail,ArrayList alPayMode,
            ArrayList alFeeItemList,
            bool isPreview)
        {
           //this.isPreView = false  ;
            try
            {
                this.regInfoOther = regInfo.Clone();
                invoiceOther = invoice.Clone();
                this.Register = regInfo;
                this.Controls.Clear();
                //如果费用明细为空，则返回
                if (alFeeItemList.Count <= 0)
                {
                    return -1;
                }
                #region 克隆一个费用明细信息列表，因为后面操作需要对列表元素有删除操作．
                ArrayList alInvoiceDetailClone = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList det in alInvoiceDetail)
                {
                    alInvoiceDetailClone.Add(det.Clone());
                }
                #endregion
                if (this.InvoiceType == "MZ01")
                {
                    c = new ucMZFP();
                    while (c.Controls.Count > 0)
                    {
                        this.Controls.Add(c.Controls[0]);
                    }
                    this.Size = c.Size;
                    this.InitReceipt();
                    SetMZFPPrintValue (regInfo,
                           invoice,
                           alInvoiceDetailClone,
                           alFeeItemList,
                           isPreview);
                }
              
                //控制根据打印和预览显示选项
                if (isPreview)
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
        /// <summary>
        /// 设置发票打印内容
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="invoice">发票主表信息</param>
        /// <param name="alInvoiceDetail">发票明细信息</param>
        /// <param name="alFeeItemList">费用明细信息</param>
        /// <param name="isPreview">是否预览模式</param>
        /// <returns></returns>
        public int SetPrintValueOther()
        {
            //this.isPreView = false  ;
            try
            {
                this.Controls.Clear();
                if (this.InvoiceType == "MZ01")
                {
                    c = new ucMZFP();
                    while (c.Controls.Count > 0)
                    {
                        this.Controls.Add(c.Controls[0]);
                    }
                    this.Size = c.Size;
                    this.InitReceiptOther ();
                  
                    SetMZFPPrintValueOther ();
                }

                ////控制根据打印和预览显示选项
                //if (isPreview)
                //{
                //    SetToPreviewMode();
                //}
                //else
                //{
                SetToPrintMode();
                //}
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 设置现金发票打印内容
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="invoice">发票主表信息</param>
        /// <param name="alInvoiceDetail">发票明细信息</param>
        /// <param name="alFeeItemList">费用明细信息</param>
        /// <param name="isPreview">是否预览模式</param>
        /// <returns></returns>
        private int SetMZFPPrintValueOther()
        {
            try
            {
                ucMZFP   ucReceipt = (ucMZFP)c;
                int otherIdx = 0;
                while (alName.Count > 0)
                {
                    if (otherIdx < 3)
                    {
                        Label lName = GetFeeNameLable("lblOtherInvoiceNO" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoiceOther .Invoice.ID;
                        lName = GetFeeNameLable("lblOtherName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfoOther.Name;
                        lName = GetFeeNameLable("lblOtherDate" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoiceOther.PrintTime.ToShortDateString();
                        lName = GetFeeNameLable("lblOtherDoctorInfoTempletDeptName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfoOther.DoctorInfo.Templet.Dept.Name;
                        lName = GetFeeNameLable("lblOtherFeeName" + otherIdx.ToString(), lblPrint);
                        lName.Text = alName[0].ToString();
                        lName = GetFeeNameLable("lblOtherFeeInfo" + otherIdx.ToString(), lblPrint);
                        lName.Text = FS.FrameWork.Public.String.FormatNumberReturnString(System.Convert.ToDecimal(alValue[0]), 2).PadLeft(9, ' ');
                        otherIdx++;
                        alName.RemoveAt(0);
                        alValue.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }
                ucReceipt.lblScrap.Visible = true;
                ucReceipt.lblScrap1.Visible = true;
                ucReceipt.lblScrap.Text = "作废";
                ucReceipt.lblScrap1.Text = "作废";
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 设置现金发票打印内容
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="invoice">发票主表信息</param>
        /// <param name="alInvoiceDetail">发票明细信息</param>
        /// <param name="alFeeItemList">费用明细信息</param>
        /// <param name="isPreview">是否预览模式</param>
        /// <returns></returns>
        private int SetMZFPPrintValue(
            FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList alInvoiceDetail,
            ArrayList alFeeItemList,
            bool isPreview)
        {
            try
            {
                #region 设置发票打印内容
                ucMZFP ucReceipt = (ucMZFP)c;

                #region 医疗机构
                //ucReceipt.lblYiLiaoJiGou.Text = "阜新市新邱区第二人民医院";
                #endregion

                #region 门诊号
                ucReceipt.lblCardNO.Text = regInfo.PID.CardNO;
                ucReceipt.lblCardNO1.Text = regInfo.PID.CardNO;
                #endregion

                #region 打印时间
                ucReceipt.lblDate.Text = invoice.PrintTime.ToString("yyyy  MM  dd");
                ucReceipt.lblDateTime.Text = invoice.PrintTime.ToShortTimeString();
                ucReceipt.lblDate1.Text = invoice.PrintTime.ToString("yyyy  MM  dd");
                //ucReceipt.lblDateTime1.Text = invoice.PrintTime.ToShortTimeString();
                #endregion

                #region 发票号
                ucReceipt.lblInvoiceNO.Text = invoice.Invoice.ID;
                ucReceipt.lblInvoiceNO1.Text = invoice.Invoice.ID;
                #endregion

                #region 收费员
                FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
                string operUserCode = string.Empty;
                operUserCode = person.GetPersonByID(invoice.BalanceOper.ID).Name;
                ucReceipt.lblOperName.Text = operUserCode;
                ucReceipt.lblOperName1.Text = operUserCode;
                #endregion

                #region 合同单位
                //if (invoice.Patient.Pact.PayKind.ID == "01" || invoice.Patient.Pact.PayKind.ID == "02")
                //{
                ucReceipt.lblPactName.Text = regInfo.Pact.Name;
                ucReceipt.lblPactName1.Text = regInfo.Pact.Name;
                //} 
                #endregion


                #region 姓名
                ucReceipt.lblName.Text = regInfo.Name;
                ucReceipt.lblName1.Text = regInfo.Name;
                #endregion

                #region 科别
                //FS.HISFC.Models.Fee.Outpatient.FeeItemList detFeeItemList = alFeeItemList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                //ucReceipt.lblExeDept.Text =  detFeeItemList.ExecOper.Dept.Name;
                #endregion
                #region 处方医师
                //ucReceipt.lblRecipeOperName.Text = regInfo.DoctorInfo.Templet.Dept .Name ; 
                #endregion
                #region 费用大类
                //票面信息
                decimal[] FeeInfo =
                    //---------------------1-----------2------------3------------4-------------5-----------------
                    new decimal[15] { decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      //decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      //decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};
                //票面信息
                string[] FeeInfoName =
                    //---------------------1-----------2------------3------------4-------------5-----------------
                    new string[15] { string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
                                     string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
                                     //string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
                                     //string.Empty,string.Empty,string.Empty,string.Empty,
                                     string.Empty,string.Empty,string.Empty,string.Empty,string.Empty};

                //统计大类项目可以直接取
                for (int i = 0; i < alInvoiceDetail.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
                    detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];
                    if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
                    {
                        FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
                        if (detail.FeeCodeStat.Name.Length > 5)
                        {
                            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name.Substring(0, 5);
                        }
                        else
                        {
                            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name;
                        }
                        //  FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name.Substring(0,5); 
                    }
                }
                int feeInfoNameIdx = 0;
                int FeeInfoIndex = 0;
                foreach (decimal d in FeeInfo)
                {//测试用
                    //FeeInfo[FeeInfoIndex] = 999999.99m;
                    //名称
                    Label lName = GetFeeNameLable("lblFeeName" + feeInfoNameIdx.ToString(), lblPrint);
                    //值
                    Label lValue = GetFeeNameLable("lblFeeInfo" + feeInfoNameIdx.ToString(), lblPrint);
                    if (lName != null)
                    {
                        if (FeeInfo[FeeInfoIndex] > 0)
                        {
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "普通人工器官材料费";
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "";
                            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9, ' ');
                            //操作下一组控件

                            if (FeeInfoIndex > 2)
                            {
                                alName.Add(FeeInfoName[FeeInfoIndex]);
                                alValue.Add(FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2));
                            }
                            else
                            {
                                drugTotCost += FeeInfo[FeeInfoIndex];
                            }

                        }
                    }
                    //名称
                    lName = GetFeeNameLable("lbl1FeeName" + feeInfoNameIdx.ToString(), lblPrint);
                    //值
                    lValue = GetFeeNameLable("lbl1FeeInfo" + feeInfoNameIdx.ToString(), lblPrint);
                    if (lName != null)
                    {
                        if (FeeInfo[FeeInfoIndex] > 0)
                        {
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "普通人工器官材料费";
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "";
                            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9, ' ');
                        }
                    }
                    feeInfoNameIdx++;
                    FeeInfoIndex++;
                }


                #endregion
                int otherIdx = 0;
                if (drugTotCost > 0)
                {
                    ucReceipt.lblOtherInvoiceNO0.Text = invoice.Invoice.ID;
                    ucReceipt.lblOtherName0.Text = regInfo.Name;
                    ucReceipt.lblOtherDate0.Text = invoice.PrintTime.ToShortDateString();
                    ucReceipt.lblOtherDoctorInfoTempletDeptName0.Text = regInfo.DoctorInfo.Templet.Dept.Name;
                    ucReceipt.lblOtherFeeName0.Text = "药费";
                    ucReceipt.lblOtherFeeInfo0.Text = FS.FrameWork.Public.String.FormatNumberReturnString(drugTotCost, 2).PadLeft(9, ' ');
                    otherIdx++;
                }

                while (alName.Count > 0)
                {
                    if (otherIdx < 3)
                    {
                        Label lName = GetFeeNameLable("lblOtherInvoiceNO" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoice.Invoice.ID;
                        lName = GetFeeNameLable("lblOtherName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfo.Name;
                        lName = GetFeeNameLable("lblOtherDate" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoice.PrintTime.ToShortDateString();
                        lName = GetFeeNameLable("lblOtherDoctorInfoTempletDeptName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfo.DoctorInfo.Templet.Dept.Name;
                        lName = GetFeeNameLable("lblOtherFeeName" + otherIdx.ToString(), lblPrint);
                        lName.Text = alName[0].ToString();
                        lName = GetFeeNameLable("lblOtherFeeInfo" + otherIdx.ToString(), lblPrint);
                        lName.Text = FS.FrameWork.Public.String.FormatNumberReturnString(System.Convert.ToDecimal(alValue[0]), 2).PadLeft(9, ' ');
                        otherIdx++;
                        alName.RemoveAt(0);
                        alValue.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                #region 医保信息



                if (invoice.Patient.Pact.PayKind.ID == "02")
                {
                    ucReceipt.lblSSN.Text = regInfo.SSN;
                    ucReceipt.lblOwnCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OwnCost, 2);
                    ucReceipt.lblPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PayCost, 2);
                    ucReceipt.lblPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PubCost, 2);
                    ucReceipt.lblOverCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OverCost, 2);
                    ucReceipt.lblIndividualBalance.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance, 2);


                    ucReceipt.lblSSN1.Text = regInfo.SSN;
                    ucReceipt.lblOwnCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OwnCost, 2);
                    ucReceipt.lblPayCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PayCost, 2);
                    ucReceipt.lblPubCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PubCost, 2);
                    //ucReceipt.lblOverCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OverCost, 2);
                    ucReceipt.lblIndividualBalance1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance, 2);

                    //-----1---------------------------2----------------3-----------------------4-----------------------5-----------------6----------------
                    //|医疗费总额TotCost|个人帐户支付金额PayCost|统筹支付金额PubCost|个人现金支付OwnCost|救助金支出金额OverCost|公务员补助支出金额OfficalCost
                    //-----------------7------------------8-----------------------9-----------
                    //|保健对象补贴支出BaseCost|离休人员统筹支出PubOwnCost|医院付担金额HosCost
                    //-------------10-------------11----------------------12------------13------------------
                    //|上次进入统筹医疗费用累计|本次进入统筹医疗费用金额|上次个人帐户余额|个人自费金额
                    //----14---------------15-------------16--------------------------------17----------
                    //|乙类药品个人自理|起付标准自付金额|分段自理金额|超过封顶线个人自付金额|住院封顶线以上公务员补助支出金额
                    //----18---------------------------19------20--------------21--------------------
                    //|住院自付部分公务员补助支出金额|住院人次|工伤基金支付金额|生育基金支付金额|
                    string[] temp = regInfo.SIMainInfo.Memo.Split('|');
                    ucReceipt.lblTemp20.Text = temp[20];
                    ucReceipt.lblTemp21.Text = temp[21];
                    ucReceipt.lbl1Temp20.Text = temp[20];
                    ucReceipt.lbl1Temp21.Text = temp[21];
                    ucReceipt.lblIndividualBalanceNew.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance - regInfo.SIMainInfo.PayCost , 2);
                    ucReceipt.lblIndividualBalanceNew1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance - regInfo.SIMainInfo.PayCost , 2);
                }


                #endregion

                #region 小写总金额
                ucReceipt.lblDownTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);
                ucReceipt.lblDownTotCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);
                #endregion

                #region 大写总金额
                ucReceipt.lblUpTotCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost).PadLeft(10,' ');
                ucReceipt.lblUpTotCost1.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost).PadLeft(10,' ');
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
        }
        /// <summary>
        /// 分发票后的支付方式
        /// </summary>
        public string SplitInvoicePayMode
        {
            set { this.splitinvoicepaymode = value; }
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbTransaction Trans
        {
            set { this.trans = value; }
        }

        #endregion

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

        /// <summary>
        /// 打印用的标签集合
        /// </summary>
        public Collection<Label> lblPrint;
        /// <summary>
        /// 预览用的标签集合
        /// </summary>
        public Collection<Label> lblPreview;

        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据ｔａｇ标签的值区分开，用于需要追加新票据时
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
                            #region 将代印字的打印项值清空
                            if (!string.IsNullOrEmpty(l.Text) || l.Text == "印")
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
        /// 发票只打印大写数字 打印到十万
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        public static string GetUpperCashByNumber(decimal Cash)
        {
            #region 大写总金额
            string returnValue = string.Empty;
            string[] strMoney = new string[8];
            //---------------------------|\*/|-----这个＂点＂字没有用，纯属凑数！
            string[] unit = { "分", "角", "点", "元", "拾", "佰", "仟", "万", "十万" };
            strMoney = GetUpperCashNumberByNumber(FS.FrameWork.Public.String.FormatNumber(Cash, 2));
            bool isStart = false;
            string tempDaXie = string.Empty;
            for (int i = 0; i < strMoney.Length; i++)
            {
                #region 从非零位开始打印
                if (!isStart)
                {
                    if (strMoney[i] != "零")
                    {
                        isStart = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                #endregion
                if (strMoney[i] != null)
                {
                    if (strMoney[i] != "零")
                    {
                        tempDaXie = strMoney[i] + unit[i] + tempDaXie;
                        returnValue = tempDaXie + returnValue;
                        tempDaXie = string.Empty;
                    }
                    else
                    {
                        tempDaXie = "零";
                    }
                }
            }
            return returnValue;
            #endregion
        }

        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据ｔａｇ标签的值区分开
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            //foreach (Control c in this.Controls[0].Controls)
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
        } /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据ｔａｇ标签的值区分开
        /// </remarks>
        private void InitReceiptOther()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            //foreach (Control c in this.Controls[0].Controls)
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
                            l.Text = "";
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        l.Text = "";
                        lblPreview.Add(l);
                    }
                    l.Text = "";
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
        /// 获得指定名称输入框
        /// </summary>
        /// <param name="n">名称</param>
        /// <returns>费用名称输入框控件</returns>
        public System.Windows.Forms.Label GetFeeNameLable(string n, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                if (lbl.Name == n)
                {
                    return lbl;
                }
            }
            return null;
        }
        /// <summary>
        /// 获得指定名称输入框
        /// </summary>
        /// <param name="n">名称</param>
        /// <returns>费用名称输入框控件</returns>
        public System.Windows.Forms.Label GetFeeNameLable(string n, System.Windows.Forms.Control control)
        {
            foreach (System.Windows.Forms.Control c in control.Controls)
            {
                if (c.Name == n)
                {
                    return (System.Windows.Forms.Label)c;
                }
            }
            return null;
        }
        /// <summary>
        /// 发票只打印大写数字 打印到十万
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        public static string[] GetUpperCashNumberByNumber(decimal Cash)
        {
            string[] sNumber = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] sReturn = new string[9];
            string strCash = null;
            //填充位数
            int iLen = 0;
            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 9)
            {
                strCash = strCash.Substring(strCash.Length - 9);
            }

            //填充位数
            iLen = 9 - strCash.Length;
            for (int j = 0; j < iLen; j++)
            {
                int k = 0;
                k = 8 - j;
                sReturn[k] = "零";
            }
            for (int i = 0; i < strCash.Length; i++)
            {
                string Temp = null;

                Temp = strCash.Substring(strCash.Length - 1 - i, 1);

                if (Temp == ".")
                {
                    continue;
                }
                sReturn[i] = sNumber[int.Parse(Temp)];
            }
            return sReturn;
        }
        #endregion
        private string invoiceType;

        public string InvoiceType
        {
            get { return invoiceType; }
        }

        private FS.HISFC.Models.Registration.Register register;
        public FS.HISFC.Models.Registration.Register Register
        {
            set
            {
                //register = value;
                //if (register.Pact.ID == "7")
                //{
                //    invoiceType = "MZ05";
                //}
                //else
                //{
                invoiceType = "MZ01";
                //}
            }
        }

        #region IInvoicePrint 成员


        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
