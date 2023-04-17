using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou.BalanceInvoicePrint
{
    public partial class ucSDFYBalanceInvoicePrintNew : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy//, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        private FS.HISFC.Models.Base.EBlanceType isMidwayBalance;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;

        public ucSDFYBalanceInvoicePrintNew()
        {
            InitializeComponent();
        }

        #region IBalanceInvoicePrintmy 成员
        public FS.HISFC.Models.Base.EBlanceType IsMidwayBalance
        {
            get
            {
                return this.isMidwayBalance;
            }
            set
            {
                this.isMidwayBalance = value;
            }
        }
        #endregion

        #region IBalanceInvoicePrint 成员

        public int Clear()
        {
            return 1;
        }

        public string InvoiceType
        {
            get
            {
                return "ZY01";
            }
        }

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                this.patientInfo = value;
            }
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = new FS.HISFC.Models.Base.PageSize("ZYFP", 787, 400);
            p.SetPageSize(ps);
            p.PrintDocument.PrinterSettings.PrinterName = "ZYFP";
            p.IsHaveGrid = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsCanCancel = false;
            p.PrintPage(0, 0, this);

            return 1;
        }

        public int PrintPreview()
        {
            //FS.NFC.Interface.Classes.Print p = new FS.NFC.Interface.Classes.Print();
            //FS.UFC.Common.Classes.Function.GetPageSize("ipbalance", ref p);

            //p.PrintPreview(0, 0, this);
            //return 1;

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //清空控件边框
            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString().Contains("Label"))
                {
                    System.Windows.Forms.Label lblControl;
                    lblControl = (System.Windows.Forms.Label)c;
                    lblControl.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("newbalance", 694, 400);//这个newbalance name是不是固定的？
            p.SetPageSize(size);
            p.PrintPreview(30, 0, this);

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, true);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, false);
        }

        public IDbTransaction Trans
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 打印控件赋值
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <param name="Pinfo"></param>
        /// <param name="al">balancelist数据</param>
        /// <param name="IsPreview">是否打印其余可显示部分</param>
        /// <returns></returns>
        protected int SetPrintValue(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceHead, ArrayList alBalanceList, bool IsPreview)
        {
            if (alBalanceList.Count <= 0) return -1;
            if (!IsPreview)
            {
                lblIn.Visible = true;
                //清空控件边框
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Substring(0, 3) == "lbl")
                    {
                        System.Windows.Forms.Label lblControl;
                        lblControl = (System.Windows.Forms.Label)c;
                        lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                        c.Visible = true;
                    }
                }
            }
            //控制根据打印和预览显示选项
            if (IsPreview)
            {
                lblIn.Visible = false;
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Length > 6)
                    {
                        if (c.Name.Substring(0, 6) == "lblPre")
                            c.Visible = IsPreview;
                    }
                }
            }
            else
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Length > 6)
                    {

                        if (c.Name.Substring(0, 6) == "lblPre")
                            c.Visible = IsPreview;
                    }

                }

            }
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            ArrayList alBalancePay = new ArrayList();
            alBalancePay = myInpatient.QueryBalancePaysByInvoiceNOAndBalanceNO(balanceHead.Invoice.ID, int.Parse(balanceHead.ID));
            if (alBalancePay == null)
            {
                MessageBox.Show("获得患者支付方式出错!");
                return -1;
            }
            //赋值

            //基本信息
            this.lblPriPatientNo.Text = patientInfo.PID.PatientNO;  //住院号

            this.lblPriNurseCell.Text = patientInfo.PVisit.PatientLocation.Dept.Name; //病区 --此处用科室name 发票空白太短 中山原有系统无病区概念

            this.lblPriSwYear.Text = balanceHead.BalanceOper.OperTime.Year.ToString(); //年

            this.lblPriSwMonth.Text = balanceHead.BalanceOper.OperTime.Month.ToString(); //月

            this.lblPriSwDay.Text = balanceHead.BalanceOper.OperTime.Day.ToString(); //日

            //this.lblPriBalanceType.Text = balanceHead.BalanceType.Name.ToString();//结算类型//[2007/10/22]更改的

            this.lblPriName.Text = patientInfo.Name;//姓名


            #region //住院日期  -- By Maokb
            /*
             * 住院日期根据本次结算的起止时间
             * 因为住院天数的计算为 计头不计尾
             * 所以出院结算 出院日期-入院日期
             * 如果是中途结算则+1
             */
            if (patientInfo.Pact.PayKind.ID == "03")
            {
                if (balanceHead.BeginTime < new DateTime(2008, 4, 18))
                {
                    balanceHead.BeginTime = new DateTime(2008, 4, 18);
                }
            }
            //int days = 0;
            //if (this.isMidwayBalance == FS.HISFC.Integrate.FeeInterface.EBlanceType.Mid || balanceHead.BalanceType.ID.ToString() == "I")
            //{
            //    days = ((TimeSpan)(balanceHead.EndTime.Date - balanceHead.BeginTime.Date)).Days + 1;
            //}
            //else
            //{
            //    days = ((TimeSpan)(patientInfo.PVisit.OutTime.Date - balanceHead.BeginTime.Date)).Days;
            //}

            //if (days <= 0)
            //{
            //    days = 1;
            //}
            if (this.isMidwayBalance == FS.HISFC.Models.Base.EBlanceType.Mid || balanceHead.BalanceType.ID.ToString() == "I")
            {
                this.lblPriDateIn.Text = balanceHead.BeginTime.ToString("yyyy.MM.dd");
                this.lblPriDateOut.Text = balanceHead.EndTime.ToString("yyyy.MM.dd");
            }
            else
            {
                this.lblPriDateIn.Text = balanceHead.BeginTime.ToString("yyyy.MM.dd");
                this.lblPriDateOut.Text = patientInfo.PVisit.OutTime.ToString("yyyy.MM.dd");
                //this.lblPriDateIn.Text = balanceHead.BeginTime.ToShortDateString() + "至" + patientInfo.PVisit.OutTime.ToShortDateString() + "共 " + days.ToString() + " 天";

            }
            #endregion

            string PactName = "";
            PactName = myInpatient.GetComDictionaryNameByID("PACTUNIT", patientInfo.Pact.ID);
            if (PactName == null)
            {
                MessageBox.Show(myInpatient.Err);
                return -1;
            }

            //this.lblPriPayKind.Text = PactName + " " + balanceHead.Invoice.ID; //合同单位+电脑发票号;//[2007/10/22]更改的

            if (patientInfo.Pact.PayKind.ID == "03")
            {
                //公费病人显示医疗证号及比例：
                //取合同单位的比例
                //FS.HISFC.Management.Fee.PactUnitInfo PactManagment = new FS.HISFC.Management.Fee.PactUnitInfo();
                FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                //FS.HISFC.Object.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);
                FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);

                if (PactUnitInfo == null)
                {
                    this.lblPriPayKind.Text = patientInfo.SSN + " " + balanceHead.BalanceType.Name.ToString();
                }
                else
                {
                    this.lblPriPayKind.Text = patientInfo.SSN + " "
                        + (PactUnitInfo.Rate.PayRate * 100).ToString() + "% " + balanceHead.BalanceType.Name.ToString();
                }
            }
            else if (patientInfo.Pact.PayKind.ID == "02")
            {
                this.lblPriPayKind.Text = "医保" + balanceHead.BalanceType.Name.ToString();
            }
            else if (patientInfo.Pact.PayKind.ID == "06")
            {
                this.lblPriPayKind.Text = "广州医保" + balanceHead.BalanceType.Name.ToString();
            }
            else
            {
                this.lblPriPayKind.Text = balanceHead.BalanceType.Name.ToString();//[2007/10/22]更改的
            }

            //操作员
            //this.lblPriOper.Text = balanceHead.BalanceOper.ID;
            this.lblPriOper.Text = new FS.HISFC.BizLogic.Manager.Person().GetPersonByID(balanceHead.BalanceOper.ID).ID;

            //票面信息
            decimal[] feeCostList = new decimal[38];
            string[] feeNameList = new string[38];
            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList Blist = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                Blist = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                //显示费用名称
                if (IsPreview)
                {
                    if (Blist.FeeCodeStat.SortID < 1 || Blist.FeeCodeStat.SortID > 37)
                    {
                        continue;
                    }
                    try
                    {
                        feeNameList[Blist.FeeCodeStat.SortID] = Blist.FeeCodeStat.StatCate.Name;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return -1;
                    }
                }
                //费用金额赋值
                feeCostList[Blist.FeeCodeStat.SortID] = Blist.BalanceBase.FT.TotCost;
            }
            //1西药费
            lblPriCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[1], 2);
            if (lblPriCost1.Text == "0.00" || lblPriCost1.Text == "")
            {
                lblPreFeeName1.Text = "";
                lblPriCost1.Text = "";
            }
            else
            {
                lblPreFeeName1.Visible = true;
            }
            //2化验费
            lblPriCost2.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[2], 2);
            if (lblPriCost2.Text == "0.00" || lblPriCost2.Text == "")
            {
                lblPreFeeName2.Text = "";
                lblPriCost2.Text = "";
            }
            else
            {
                lblPreFeeName2.Visible = true;
            }
            //3输血费
            lblPriCost3.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[3], 2);
            if (lblPriCost3.Text == "0.00" || lblPriCost3.Text == "")
            {
                lblPreFeeName3.Text = "";
                lblPriCost3.Text = "";
            }
            else
            {
                lblPreFeeName3.Visible = true;
            }
            //4床位费
            lblPriCost4.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[4], 2);
            if (lblPriCost4.Text == "0.00" || lblPriCost4.Text == "")
            {
                lblPreFeeName4.Text = "";
                lblPriCost4.Text = "";
            }
            else
            {
                lblPreFeeName4.Visible = true;
            }
            //5中成药
            lblPriCost5.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[5], 2);
            if (lblPriCost5.Text == "0.00" || lblPriCost5.Text == "")
            {
                lblPreFeeName5.Text = "";
                lblPriCost5.Text = "";
            }
            else
            {
                lblPreFeeName5.Visible = true;
            }
            //6检查费
            lblPriCost6.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[6], 2);
            if (lblPriCost6.Text == "0.00" || lblPriCost6.Text == "")
            {
                lblPreFeeName6.Text = "";
                lblPriCost6.Text = "";
            }
            else
            {
                lblPreFeeName6.Visible = true;
            }
            //7手术费
            lblPriCost7.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[7], 2);
            if (lblPriCost7.Text == "0.00" || lblPriCost7.Text == "")
            {
                lblPreFeeName7.Text = "";
                lblPriCost7.Text = "";
            }
            else
            {
                lblPreFeeName7.Visible = true;
            }
            //8护理费
            lblPriCost8.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[8], 2);
            if (lblPriCost8.Text == "0.00" || lblPriCost8.Text == "")
            {
                lblPreFeeName8.Text = "";
                lblPriCost8.Text = "";
            }
            else
            {
                lblPreFeeName8.Visible = true;
            }
            //9中草药
            lblPriCost9.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[9], 2);
            if (lblPriCost9.Text == "0.00" || lblPriCost9.Text == "")
            {
                lblPreFeeName9.Text = "";
                lblPriCost9.Text = "";
            }
            else
            {
                lblPreFeeName9.Visible = true;
            }
            //10其他诊查
            lblPriCost10.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[10], 2);
            if (lblPriCost10.Text == "0.00" || lblPriCost10.Text == "")
            {
                lblPreFeeName10.Text = "";
                lblPriCost10.Text = "";
            }
            else
            {
                lblPreFeeName10.Visible = true;
            }
            //11其它治疗
            lblPriCost11.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[11], 2);
            if (lblPriCost11.Text == "0.00" || lblPriCost11.Text == "")
            {
                lblPreFeeName11.Text = "";
                lblPriCost11.Text = "";
            }
            else
            {
                lblPreFeeName11.Visible = true;
            }
            //12其它费用
            lblPriCost12.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[12], 2);
            if (lblPriCost12.Text == "0.00" || lblPriCost12.Text == "")
            {
                lblPreFeeName12.Text = "";
                lblPriCost12.Text = "";
            }
            else
            {
                lblPreFeeName12.Visible = true;
            }

            if (balanceHead.FT.PubCost <= 0)
            {
                this.lblPriPub.Text = string.Empty; //公医医保记账
            }
            else
            {
                //记帐金额
                this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PubCost, 2);
            }

            if ((balanceHead.FT.OwnCost + balanceHead.FT.PayCost) <= 0)
            {
                this.lblPriPay.Text = string.Empty;
            }
            else
            {
                //个人缴费金额
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost
                    + balanceHead.FT.PayCost, 2);
            }

            if (balanceHead.FT.DerateCost > 0)
            {
                this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.DerateCost, 2);
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                    balanceHead.FT.OwnCost + balanceHead.FT.PayCost - balanceHead.FT.DerateCost, 2);
            }

            if (patientInfo.Pact.PayKind.ID == "03")
            {
                this.lblPriPay.Text = "";
                //个人缴费金额 -- By Maokb 
                //公费包含自费和自负部分
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost
                    + balanceHead.FT.PayCost, 2) + "(自付:" + FS.FrameWork.Public.String.FormatNumberReturnString(
                    balanceHead.FT.PayCost, 2) + " 元)";
            }
            //if (patientInfo.Pact.ID == "2")
            //{
            //    //个人缴费金额
            //    this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost + balanceHead.FT.PayCost, 2);
            //}
            //小写金额显示全额
            this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);

            string returnMoney = "";
            string supplyMoney = "";
            //foreach (FS.HISFC.Models.Fee.Inpatient.Balance b in alBalancePay)
            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList b in alBalanceList)
            {
                if (b.BalanceBase.TransType == FS.HISFC.Models.Base.TransTypes.Positive)//结算款
                {
                    if (b.BalanceBase.FT.ReturnCost > 0)//返还
                    {
                        //returnMoney = returnMoney + " " + b.BalanceBase.FT.TotCost.ToString();
                        returnMoney = returnMoney + " " + b.BalanceBase.Name + ":" + b.BalanceBase.FT.TotCost.ToString();
                    }
                    else
                    {
                        string payType = "";//2010-01-12 肿瘤添加，支付方式简写
                        switch (b.BalanceBase.Name)
                        {
                            case "现金":
                                payType = "现";
                                break;
                            case "借记卡":
                            case "信用卡":
                                payType = "卡";
                                break;
                            case "支票":
                                payType = "支";
                                break;
                            default:
                                payType = "其他";
                                break;
                        }

                        //supplyMoney = supplyMoney + " " + b.BalanceBase.FT.TotCost.ToString();
                        supplyMoney = supplyMoney + " " + payType + b.BalanceBase.FT.TotCost.ToString();

                        supplyMoney = supplyMoney.Trim();
                    }
                }
            }

            if (balanceHead.FT.PrepayCost <= 0)
            {
                this.lblPriPrepay.Text = string.Empty;
            }
            else
            {
                //预收
                this.lblPriPrepay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PrepayCost, 2);
                //this.lblpayType.Text = returnMoney + supplyMoney;//[2007/12/13]这个是显示,支付方式和支付总额的控件,肿瘤医院不用了,所以屏蔽
            }

            if (balanceHead.FT.SupplyCost <= 0)
            {
                this.lblPriSupply.Text = string.Empty;
            }
            else
            {
                //补收
                this.lblPriSupply.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.SupplyCost, 2);// +" (" + supplyMoney + ")";//2010-01-12 肿瘤添加，显示支付方式
            }

            if (balanceHead.FT.ReturnCost <= 0)
            {
                this.lblPriReturn.Text = string.Empty;
            }
            else
            {
                //退款
                this.lblPriReturn.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.ReturnCost, 2);
            }

            string upperMoney = string.Empty;
            string upper = string.Empty;
            //大写显示全额 - by maokb
            upperMoney = FS.FrameWork.Public.String.FormatNumber(balanceHead.FT.TotCost, 2).ToString();
            upper = ((int)balanceHead.FT.TotCost).ToString().Trim().PadLeft(6, '#');

            char[] dotpos = upperMoney.ToCharArray(upperMoney.IndexOf('.') + 1, 2);
            this.lblPriJ.Text = this.Convert(dotpos[0]);
            this.lblPriF.Text = this.Convert(dotpos[1]);

            char[] zspos = upper.ToCharArray();

            for (int i = 0, j = zspos.Length; i < j; i++)
            {
                switch (i)
                {
                    case 0:
                        this.lblPriSW.Text = this.Convert(zspos[i]);
                        break;
                    case 1:
                        this.lblPriW.Text = this.Convert(zspos[i]);
                        break;
                    case 2:
                        this.lblPriQ.Text = this.Convert(zspos[i]);
                        break;
                    case 3:
                        this.lblPriB.Text = this.Convert(zspos[i]);
                        break;
                    case 4:
                        this.lblPriS.Text = this.Convert(zspos[i]);
                        break;
                    case 5:
                        this.lblPriY.Text = this.Convert(zspos[i]);
                        break;
                }
            }

            this.lblInvoice.Text = balanceHead.Invoice.ID;
            this.lblPriSwBalanceType.Text = balanceHead.PrintedInvoiceNO;
            return 0;
        }

        private string Convert(char ch)
        {
            string[] sNumber = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string rets = string.Empty;
            switch (ch)
            {
                case '0':
                    rets = sNumber[0];
                    break;
                case '1':
                    rets = sNumber[1];
                    break;
                case '2':
                    rets = sNumber[2];
                    break;
                case '3':
                    rets = sNumber[3];
                    break;
                case '4':
                    rets = sNumber[4];
                    break;
                case '5':
                    rets = sNumber[5];
                    break;
                case '6':
                    rets = sNumber[6];
                    break;
                case '7':
                    rets = sNumber[7];
                    break;
                case '8':
                    rets = sNumber[8];
                    break;
                case '9':
                    rets = sNumber[9];
                    break;
                default:
                    rets = "零";
                    break;
            }
            return rets;
        }

        #endregion

        #region IBalanceInvoicePrint 成员
        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList)
        {
            return this.SetPrintValue(this.patientInfo, balanceInfo, alBalanceList, true);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList)
        {
            return this.SetPrintValue(this.patientInfo, balanceInfo, alBalanceList, false);
        }
        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                //type[0]=typeof(FS.HISFC.BizProcess.Integrate.FeeInterface.i)
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy);
                return type;
            }
        }
        #endregion
    }
}
