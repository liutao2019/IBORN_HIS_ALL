using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IBalanceInvoicePrintmy
{
    public partial class ucInInvoice : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy
    {
        public ucInInvoice()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 数据库连接
        /// </summary>
        private System.Data.IDbTransaction trans;        
        private FS.HISFC.Models.Base.EBlanceType isMidwayBalance;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;
        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion


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
            throw new Exception("The method or operation is not implemented.");
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

            p.IsHaveGrid = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("ZYFP", 787, 400);

            p.SetPageSize(ps);

            //获得打印机名
            string printer = this.controlIntegrate.GetControlParam<string>("ZYFP", true, "");
            if (!string.IsNullOrEmpty(printer))
            {
                p.PrintDocument.PrinterSettings.PrinterName = printer;
            }
          
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsCanCancel = false;
            p.PrintPage(0, 0, this);
            return 1;
        }

        public int PrintPreview()
        {

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();            //清空控件边框
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
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("newbalance", 787, 400);
            p.SetPageSize(size);
            p.PrintPreview(30, 0, this);

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, true);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
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
        /// 获取费用大类名称控件
        /// </summary>
        /// <param name="i">序号</param>
        /// <returns></returns>
        protected Control GetFeeNameLable(int i)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "lblPreFeeName" + i.ToString())
                {
                    c.Visible = true;
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取费用大类金额控件
        /// </summary>
        /// <param name="i">序号</param>
        protected Control GetFeeCostLable(int i)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "lblPriCost" + i.ToString())
                {
                    c.Visible = true;
                    return c;
                }
            }
            return null;
        }

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
            //this.lblPreFeeName17.Visible = true;
            //this.lblPreFeeName18.Visible = true;
            //this.lblPreFeeName17.Visible = false;
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

            lblHosName.Text = FS.FrameWork.Management.Connection.Hospital.Name;

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

            if (this.isMidwayBalance == FS.HISFC.Models.Base.EBlanceType.Mid || balanceHead.BalanceType.ID.ToString() == "I")
            {
                this.lblPriDateIn.Text = balanceHead.BeginTime.ToShortDateString();
                this.lblPriDateOut.Text = balanceHead.EndTime.ToShortDateString();
            }
            else
            {
                this.lblPriDateIn.Text = balanceHead.BeginTime.ToShortDateString();
                this.lblPriDateOut.Text = patientInfo.PVisit.OutTime.ToShortDateString();
                //this.lblPriDateIn.Text = balanceHead.BeginTime.ToShortDateString() + "至" + patientInfo.PVisit.OutTime.ToShortDateString() + "共 " + days.ToString() + " 天";

            }
            #endregion


            //string PactName = "";
            //PactName = myInpatient.GetComDictionaryNameByID("PACTUNIT", patientInfo.Pact.ID);
            //if (PactName == null)
            //{
            //    MessageBox.Show(myInpatient.Err);
            //    return -1;
            //}
            this.lblPriPayKind.Text = patientInfo.Pact.Name; //合同单位
            this.neuLblBalanceType.Text = balanceHead.BalanceType.Name.ToString();//结算方式
            //if (patientInfo.Pact.PayKind.ID == "03")
            //{
            //    //公费病人显示医疗证号及比例：
            //    //取合同单位的比例
            //    FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();


            //    FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);

            //    if (PactUnitInfo == null)
            //    {
            //        this.lblPriPayKind.Text = patientInfo.SSN + " " + balanceHead.BalanceType.Name.ToString();
            //    }
            //    else
            //    {
            //        this.lblPriPayKind.Text = patientInfo.SSN + " "
            //            + (PactUnitInfo.Rate.PayRate * 100).ToString() + "% " + balanceHead.BalanceType.Name.ToString();
            //    }
            //}
            //else if (patientInfo.Pact.PayKind.ID == "02")
            //{
            //    this.lblPriPayKind.Text = "广州医保" + balanceHead.BalanceType.Name.ToString();
            //}
            //else if (patientInfo.Pact.PayKind.ID == "06")
            //{
            //    this.lblPriPayKind.Text = "深圳医保" + balanceHead.BalanceType.Name.ToString();
            //}
            //else
            //{
            //    this.lblPriPayKind.Text = balanceHead.BalanceType.Name.ToString();//[2007/10/22]更改的
            //}

            //操作员

            this.lblPriOper.Text = balanceHead.BalanceOper.ID;

            //减免信息
            if (balanceHead.FT.DerateCost == null || balanceHead.FT.DerateCost == 0)
            {
                this.lblEcoCost.Text = "";
            }
            else
            {
                this.lblEcoCost.Text = "减免："+balanceHead.FT.DerateCost.ToString();
            }

            //票面信息
            decimal checkFee = 0; // 汇总检查费
            decimal cureFee = 0; //汇总治疗费
            decimal ZFFee = 0m;//自费药汇总

            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList Blist = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                Blist = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                //显示费用名称
                if (IsPreview)
                {
                    System.Windows.Forms.Label lblFeeName;
                    if (Blist.FeeCodeStat.SortID < 1 || Blist.FeeCodeStat.SortID > 36)
                    {
                        continue;
                    }
                    lblFeeName = (System.Windows.Forms.Label)this.GetFeeNameLable(Blist.FeeCodeStat.SortID);
                    if (lblFeeName == null)
                    {
                        MessageBox.Show("没有找到费用大类为" + Blist.FeeCodeStat.StatCate.Name + "的打印序号!");
                        return -1;
                    }
                    try
                    {
                        lblFeeName.Text = Blist.FeeCodeStat.StatCate.Name;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return -1;
                    }
                }
                //费用金额赋值
                System.Windows.Forms.Label lblFeeCost;
                lblFeeCost = (System.Windows.Forms.Label)this.GetFeeCostLable(Blist.FeeCodeStat.SortID);
                if (lblFeeCost == null)
                {
                    MessageBox.Show("没有找到费用大类为" + Blist.FeeCodeStat.StatCate.Name + "的打印序号!");
                    return -1;
                }
                lblFeeCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(Blist.BalanceBase.FT.TotCost, 2);
                //把二级明细统计保存
                if (Blist.FeeCodeStat.SortID >= 12 && Blist.FeeCodeStat.SortID <= 18)
                {
                    checkFee = checkFee + Blist.BalanceBase.FT.TotCost;
                }
                else if (Blist.FeeCodeStat.SortID >= 20 && Blist.FeeCodeStat.SortID <= 25)
                {
                    cureFee = cureFee + Blist.BalanceBase.FT.TotCost;
                }
              
                 //把二级明细统计保存 没有二级明细了
                //if (Blist.FeeCodeStat.SortID >= 12 && Blist.FeeCodeStat.SortID <= 18)
                //{
                //    checkFee = checkFee + Blist.BalanceBase.FT.TotCost;
                //}
                //else if (Blist.FeeCodeStat.SortID >= 20 && Blist.FeeCodeStat.SortID <= 25)
                //{
                //    cureFee = cureFee + Blist.BalanceBase.FT.TotCost;
                //}
            }


            //如果要显示自费金额 请使用
            ZFFee = balanceHead.FT.DrugOwnCost;
            //汇总检查费、治疗费

            if (ZFFee <= 0)
            {
               
            }
            else
            {

            }

            if (balanceHead.FT.PubCost <= 0)
            {
                //this.lblPrePub.Text = string.Empty;
                this.lblPriPub.Text = string.Empty;
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

            if (patientInfo.Pact.PayKind.ID == "03")
            {

                //个人缴费金额 -- By Maokb 
                //公费包含自费和自负部分
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost
                    + balanceHead.FT.PayCost, 2) + "(自付:" + FS.FrameWork.Public.String.FormatNumberReturnString(
                    balanceHead.FT.PayCost, 2) + " 元)";
            }

            if (patientInfo.Pact.ID == "2")
            {
                //个人缴费金额
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost + balanceHead.FT.PayCost, 2);

            }
            //小写金额显示全额
            this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);

            string returnMoney = "";
            string supplyMoney = "";
            foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay b in alBalancePay)
            {
                if (b.TransKind.ID == "1")//结算款
                {
                    if (b.RetrunOrSupplyFlag == "2")//返还
                    {
                        returnMoney = returnMoney + " " + b.PayType.Name + ":" + b.FT.TotCost.ToString();
                    }
                    else
                    {
                        string payType = "";//2010-01-12 肿瘤添加，支付方式简写
                        switch (b.PayType.Name)
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

                        supplyMoney = supplyMoney + " " + payType + b.FT.TotCost.ToString();
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
                this.lblPriSupply.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.SupplyCost, 2);
              
                //佛山项目不用  
                //if (supplyMoney != "")
                //{

                //    this.lblPriSupply.Text += " (" + supplyMoney + ")";//2010-01-12 肿瘤添加，显示支付方式
                //}
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
            this.lblRealInvoiceNo.Text = balanceHead.PrintedInvoiceNO; // 实际发票号
            return 0;
        }

        private string Convert(char ch)
        {
            string[] sNumber ={ "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
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

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy);
                return type;
            }
        }

        #endregion
    }
}
