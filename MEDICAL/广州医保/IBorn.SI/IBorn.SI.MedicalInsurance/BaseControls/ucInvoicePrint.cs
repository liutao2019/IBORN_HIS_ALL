using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace IBorn.SI.MedicalInsurance.BaseControls
{
    public partial class ucInvoicePrint : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInvoicePrint()
        {
            InitializeComponent();
        }

        #region 变量

        private string description = "广州爱博恩妇产医院";

        private string invoiceType = "MZ01";

        private bool isPreView = false;

        private FS.HISFC.Models.Registration.Register register;

        private string setPayModeType;

        private string splitInvoicePayMode;

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        FS.FrameWork.Public.ObjectHelper payModesHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 控制参数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得费用名称输入框
        /// </summary>
        /// <param name="i">序号</param>
        /// <returns></returns>
        private Control GetFeeNameLable(int i)
        {
            Control c = this.Controls[string.Concat("lblPreFeeName", i.ToString())];
            if (c != null)
            {
                c.Visible = true;
            }

            return c;
        }

        /// <summary>
        /// 获得费用金额输入框
        /// </summary>
        /// <param name="i">序号</param>
        /// <returns></returns>
        private Control GetFeeCostLable(int i)
        {
            Control c = this.Controls[string.Concat("lblPriCost", i.ToString())];
            if (c != null)
            {
                c.Visible = true;
            }

            return c;
        }

        /// <summary>
        /// 获取发票打印大写数字数组(只打印到十万)
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        private string[] GetUpperCashbyNumber(decimal Cash)
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

        #region IInvoicePrint 成员

        public string Description
        {
            get { return this.description; }
        }

        public string InvoiceType
        {
            get { return this.invoiceType; }
        }

        /// <summary>
        /// 设置是否打印副本 true发票副本 false发票套打
        /// </summary>
        public bool IsPreView
        {
            set { this.isPreView = value; }
        }

        public int Print()
        {
            try
            {
                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                //{3E7EFECA-5375-420b-A435-323463A0E56C}
                if (string.IsNullOrEmpty(printerName))
                {
                    return 1;
                }
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZFP", 787, 400);
                print.SetPageSize(ps);
                if (isPreView)
                {
                    //发票副本
                    print.PrintDocument.PrinterSettings.PrinterName = printerName; //this.controlIntegrate.GetControlParam<string>("MZFPFB", false, "MZFPFB");
                }
                else
                {
                    //发票套打
                    print.PrintDocument.PrinterSettings.PrinterName = printerName;//this.controlIntegrate.GetControlParam<string>("MZFP", false, "MZFP");
                }

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                print.PrintPage(0, 0, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        public int PrintOtherInfomation()
        {
            return 1;
        }

        public FS.HISFC.Models.Registration.Register Register
        {
            set { this.register = value; }
        }

        /// <summary>
        /// 设置支付方式模式 1使用SplitInvoicePayMode 否则使用SetPrintValue中的发票参数
        /// </summary>
        public string SetPayModeType
        {
            set { this.setPayModeType = value; }
        }

        /// <summary>
        /// 发票支付方式
        /// </summary>
        public string SplitInvoicePayMode
        {
            set { this.splitInvoicePayMode = value; }
        }

        public void SetPreView(bool isPreView)
        {
            this.isPreView = isPreView;
        }

        public int SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, ArrayList Invoices, ArrayList invoiceDetails, ArrayList feeDetails)
        {
            return 1;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview)
        {
            return 1;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList alPayModes, bool isPreview)
        {
            this.isPreView = isPreview;

            if (feeDetails.Count <= 0)
            {
                return -1;
            }
            //{2B6B02FF-9244-49af-B6A9-D5759033A3D7}
            //this.neuLabel1.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
            this.neuLabel1.Text = curDepartment.HospitalName;

            this.lblTitle.Text = curDepartment.HospitalName + "医疗收费票据";

            //{0DD049BF-4D25-49a6-B8DD-AA7789733242}
            //if (curDepartment.HospitalID == "IBORN")//{7B6E7381-3A6E-4178-8319-9ED40F0A842C}佛山打印显示头
            //{
            //    this.lblTitle.Visible = false;
            //}

            //设置控件显示
            foreach (Control c in this.Controls)
            {
                if (c.Name.Length > 6 && "lblPre".Equals(c.Name.Substring(0, 6)))
                {
                    c.Visible = isPreview;
                }

                if (isPreview == false)
                {
                    if (c.Name.Length > 3 && "lbl".Equals(c.Name.Substring(0, 3)))
                    {
                        System.Windows.Forms.Label lblControl = c as System.Windows.Forms.Label;
                        lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                        //lblControl.TextAlign = ContentAlignment.BottomCenter;
                    }
                }
            }

            //设置基本信息
            this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();
            this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();
            this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();
            string strOperTemp="";
            if(!string.IsNullOrEmpty(invoice.BalanceOper.Name))
            {
                strOperTemp="("+invoice.BalanceOper.Name+")";
            }
            this.lblPriOper.Text = invoice.BalanceOper.ID+strOperTemp;
            this.lblInvoice.Text = invoice.Invoice.ID;
            this.lblFeeDate.Text = invoice.PrintTime.ToString("HH:mm:ss");
            //if (isPreview == true)
            //{
            //    this.lblPriSwYear.Text = regInfo.DoctorInfo.SeeDate.Year.ToString();
            //    this.lblPriSwMonth.Text = regInfo.DoctorInfo.SeeDate.Month.ToString();
            //    this.lblPriSwDay.Text = regInfo.DoctorInfo.SeeDate.Day.ToString();
            //    this.lblFeeDate.Text = "";
            //}
            this.lblPriSwBalanceType.Text = invoice.PrintedInvoiceNO;
            this.lblPriName.Text = regInfo.Name;
            if (regInfo.Sex.Name.ToString() != "" && regInfo.Sex.Name.ToString() != null && regInfo.Sex.Name.ToString() == "男" && isPreview == false)
            {
                this.lblPriSexM.Text = "√";
                this.lblPriSexM.Visible = true;
            }
            else if (regInfo.Sex.Name.ToString() != "" && regInfo.Sex.Name.ToString() != null && regInfo.Sex.Name.ToString() == "女" && isPreview == false)
            {
                this.lblPriSexW.Text = "√";
                this.lblPriSexW.Visible = true;
            }
            else if (isPreview == true)
            {
                this.lblPriSexM.Text = "男";
                this.lblPriSexW.Text = "女";
                this.lblPriSexM.Visible = true;
            }
          
            this.lblPriPayKind.Text = regInfo.Pact.Name;
            if (regInfo.SSN != "")
            {
                this.lbPactName.Text += "," + "医疗证号：" + regInfo.SSN;
            }
            this.neuLabel5.Visible = true;
            this.neuLabel5.Text = "";

            this.lblPriNurseCell.Text = regInfo.DoctorInfo.Templet.Dept.Name;
            this.lblCardNO.Text = regInfo.PID.CardNO;


            //{502CEC7B-EF3F-484a-8A8D-FFF5C0DC16A2}
            ////费用大类名称
            this.lblPreFeeName1.Text = "西药费";
            this.lblPreFeeName1.Visible = true;
            this.lblPreFeeName2.Text = "中药费";
            this.lblPreFeeName2.Visible = true;
            this.lblPreFeeName3.Text = "挂号费";
            this.lblPreFeeName3.Visible = true;
            this.lblPreFeeName4.Text = "诊查费";
            this.lblPreFeeName4.Visible = true;
            this.lblPreFeeName5.Text = "检查费";
            this.lblPreFeeName5.Visible = true;
            this.lblPreFeeName6.Text = "检验费";
            this.lblPreFeeName6.Visible = true;
            this.lblPreFeeName7.Text = "治疗费";
            this.lblPreFeeName7.Visible = true;
            this.lblPreFeeName8.Text = "手术费";
            this.lblPreFeeName8.Visible = true;
            this.lblPreFeeName9.Text = "麻醉费";
            this.lblPreFeeName9.Visible = true;
            this.lblPreFeeName10.Text = "护理费";
            this.lblPreFeeName10.Visible = true;
            this.lblPreFeeName11.Text = "材料费";
            this.lblPreFeeName11.Visible = true;
            this.lblPreFeeName12.Text = "其他费";
            this.lblPreFeeName12.Visible = true;
            //支付方式
            if (payModesHelper.ArrayObject == null || payModesHelper.ArrayObject.Count == 0)
            {
                payModesHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            }
            string payKind = "";

            //{9D840956-6A25-494b-8407-4718EF31D99F}
            decimal EcoCost = 0.0m;
            decimal RealCost = 0.0m;

            if ("1".Equals(this.setPayModeType))
            {
                payKind = this.splitInvoicePayMode;
            }
            else
            {
                for (int i = 0; i < alPayModes.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = alPayModes[i] as FS.HISFC.Models.Fee.Outpatient.BalancePay;

                    //{9D840956-6A25-494b-8407-4718EF31D99F}
                    //优惠金额
                    if (payMode.PayType.ID == "RC")
                    {
                        EcoCost += payMode.FT.TotCost;
                        continue;
                    }
                    //实收金额
                    RealCost += payMode.FT.TotCost;

                    payKind += " " + payModesHelper.GetObjectFromID(payMode.PayType.ID).Name
                        + " " + FS.FrameWork.Public.String.FormatNumber(payMode.FT.TotCost, 2) + "；  ";
                }
            }
            lbPactName.Font = new Font("宋体", 8);
            lbPactName.Text = payKind;

            string strTemp = "";
            bool boolTemp = false;
            if(regInfo.SIMainInfo.AddTotCost!=0)
            {
                strTemp = "本次核准金额：" + regInfo.SIMainInfo.AddTotCost.ToString();
                boolTemp = true;
            }
            if (regInfo.SIMainInfo.YearPubCost != 0)
            {
                strTemp = strTemp + "累计核准金额：" + regInfo.SIMainInfo.YearPubCost.ToString();
                boolTemp = true;
            }
            this.lbAddTotCost.Visible = boolTemp;
            this.lbAddTotCost.Text = strTemp;
           

            //费用大类信息
            for (int i = 0; i < invoiceDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.BalanceList detail = invoiceDetails[i] as FS.HISFC.Models.Fee.Outpatient.BalanceList;
                if (detail.InvoiceSquence < 1 || detail.InvoiceSquence > 24)
                {
                    continue;
                }

                ////费用大类名称
                //System.Windows.Forms.Label lblFeeName = this.GetFeeNameLable(detail.InvoiceSquence) as System.Windows.Forms.Label;
                //if (lblFeeName == null)
                //{
                //    MessageBox.Show("没有找到费用大类为" + detail.FeeCodeStat.Name + "的打印序号!");
                //    return -1;
                //}
                //lblFeeName.Text = detail.FeeCodeStat.Name;
                //lblFeeName.Visible = true;

                //费用大类金额
                System.Windows.Forms.Label lblFeeCost = this.GetFeeCostLable(detail.InvoiceSquence) as System.Windows.Forms.Label;
                if (lblFeeCost == null)
                {
                    MessageBox.Show("没有找到费用大类为" + detail.FeeCodeStat.Name + "的打印序号!");
                    return -1;
                }
                lblFeeCost.Text = detail.BalanceBase.FT.TotCost.ToString();

            }

            //费用信息
            this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2);
            this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PayCost + invoice.FT.OwnCost, 2);

            //显示领药药房信息(去除重复)
            if (string.IsNullOrEmpty(invoice.DrugWindowsNO) == false)
            {
                string[] drugWindows = invoice.DrugWindowsNO.Split('|');
                ArrayList alDrugWindow = new ArrayList();
                string disPlayWindow = "";
                for (int i = 0; i < drugWindows.Length; i++)
                {
                    if (alDrugWindow.Contains(drugWindows[i]) == false)
                    {
                        alDrugWindow.Add(drugWindows[i]);
                        disPlayWindow += drugWindows[i] + ",";
                    }
                }
                this.lblDrugWindow.Visible = true;
                this.lblDrugWindow.Text = disPlayWindow.TrimEnd(',');
            }
            else
            {
                this.lblDrugWindow.Visible = false;
            }

            //总金额大小写
            this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);

            string[] strMoney = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(invoice.FT.TotCost, 2));
            this.lblPriF.Text = strMoney[0];
            this.lblPriJ.Text = strMoney[1];
            this.lblPriY.Text = strMoney[3];
            this.lblPriS.Text = strMoney[4];
            this.lblPriB.Text = strMoney[5];
            this.lblPriQ.Text = strMoney[6];
            this.lblPriW.Text = strMoney[7];
            this.lblPriSW.Text = strMoney[8];

            //{9D840956-6A25-494b-8407-4718EF31D99F}
            this.lblPriEcoLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(EcoCost,2);
            string[] strMoneyEco = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(EcoCost, 2));
            this.lblPriEcoF.Text = strMoneyEco[0];
            this.lblPriEcoJ.Text = strMoneyEco[1];
            this.lblPriEcoY.Text = strMoneyEco[3];
            this.lblPriEcoS.Text = strMoneyEco[4];
            this.lblPriEcoB.Text = strMoneyEco[5];
            this.lblPriEcoQ.Text = strMoneyEco[6];
            this.lblPriEcoW.Text = strMoneyEco[7];
            this.lblPriEcoSW.Text = strMoneyEco[8];

            //{9D840956-6A25-494b-8407-4718EF31D99F}
            this.lblPriRealLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(RealCost, 2);
            string[] strMoneyReal = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(RealCost, 2));
            this.lblPriRealF.Text = strMoneyReal[0];
            this.lblPriRealJ.Text = strMoneyReal[1];
            this.lblPriRealY.Text = strMoneyReal[3];
            this.lblPriRealS.Text = strMoneyReal[4];
            this.lblPriRealB.Text = strMoneyReal[5];
            this.lblPriRealQ.Text = strMoneyReal[6];
            this.lblPriRealW.Text = strMoneyReal[7];
            this.lblPriRealSW.Text = strMoneyReal[8];

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public IDbTransaction Trans
        {
            set { }
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint);
                return type;
            }
        }

        #endregion     
    }
}
