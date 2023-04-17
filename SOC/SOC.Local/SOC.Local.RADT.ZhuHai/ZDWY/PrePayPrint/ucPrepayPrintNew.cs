using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.PrePayPrint
{
    public partial class ucPrepayPrintNew : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint
    {
        public ucPrepayPrintNew()
        {
            InitializeComponent();
        }
      

        private bool isReturn = false; //是否作废发票，控制显示"作废字样";
        public bool IsRetrun
        {
            set
            {
                isReturn = value;
                if (isReturn)
                {
                    this.lbRetrun.Visible = true;
                }
                else
                {
                    this.lbRetrun.Visible = false;
                }
            }
        }
                       
        /// <summary>
        ///gy医住院发票只打印大写数字 打印到b万
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        protected string[] GetUpperCashbyNumber(decimal Cash)
        {
            string[] sNumber = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] sReturn = new string[10];
            string strCash = "";
            //填充位数
            int iLen = 0;
            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 10)//return null;
            {
                strCash = strCash.Substring(strCash.Length - 10);
            }

            //填充位数
            iLen = 10 - strCash.Length;
            for (int j = 0; j < iLen; j++)
            {
                int k = 0;
                k = 9 - j;
                sReturn[k] = "零";
            }
            for (int i = 0; i < strCash.Length; i++)
            {
                string Temp = "";



                Temp = strCash.Substring(strCash.Length - 1 - i, 1);
                if (Temp == ".") continue;

                sReturn[i] = sNumber[int.Parse(Temp)];


            }

            return sReturn;
        }

        protected string[] GetLowerCashbyNumber(decimal Cash)
        {
            string[] sReturn = new string[10];
            string strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 10)//return null;
            {
                strCash = strCash.Substring(strCash.Length - 10);
            }
            for (int i = 0; i < strCash.Length; i++)
            {
                sReturn[i] = strCash.Substring(strCash.Length - 1 - i, 1);
                sReturn[i + 1] = "￥";
            }
            return sReturn;
        }

        

        #region IPrepayPrint 成员

        public int Clear()
        {
            return 0;
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();            
            FS.HISFC.Models.Base.PageSize ps = null;
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
          ///  ps = pgMgr.GetPageSize("PrepayPrint");
            //if (ps != null && ps.Printer.ToLower() == "default")
            //{
            //    ps.Printer = "";
            //}
            ////if (ps == null)
            ////{
            ////    //默认大小
            ////    ps = new FS.HISFC.Models.Base.PageSize("PrepayPrint", 830, 420);                
             ps = new FS.HISFC.Models.Base.PageSize("CZSJ", 787, 400);
            //print.SetPageSize(ps);

            print.SetPageSize(ps);
            //print.PrintDocument.PrinterSettings.PrinterName = ps.Printer.ToString();
            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))
            {
                return 1;
            }
            print.PrintDocument.PrinterSettings.PrinterName = printerName;
            if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager)
            {
                print.PrintPreview(ps.Left, ps.Top, this);
            }
            else
            {
                print.PrintPage(ps.Left, ps.Top, this);
            }

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            return;
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo PatientInfo, ArrayList alPrepay)
        {
            FS.HISFC.Models.Fee.Inpatient.Prepay PrepayTmp = alPrepay[0] as FS.HISFC.Models.Fee.Inpatient.Prepay; 
            FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.Models.Base.Employee emp = SOC.HISFC.BizProcess.CommonInterface.DefaultCommonController.CreateInstance().GetEmployee(PrepayTmp.PrepayOper.ID);
            string empName = string.Empty;
            if (PatientInfo.User01 == "RePrint")
            {
                this.lblReprint.Visible = true;
            }
            else
            {
                this.lblReprint.Visible = false;
            }
            if (emp != null && !string.IsNullOrEmpty(emp.ID))
            {
                empName = emp.Name;
            }
            else
            {
                empName = inpatientManager.Operator.Name;
            }
            //{F774DAE4-8289-4b10-AD72-E799E58302AD}更改预交金打印
            this.lblDeptName.Text =  PatientInfo.PVisit.PatientLocation.Dept.Name;//患者所在科室
            this.lblName.Text = PatientInfo.Name;//患者姓名
            this.lblOper.Text ="开票人: "+ empName + "(" + PrepayTmp.PrepayOper.ID + ")";//开票
            this.lblFeeOper.Text ="收费员: "+ empName + "(" + PrepayTmp.PrepayOper.ID + ")";//收费员
            this.lblPatientNo.Text = PatientInfo.PID.ID;
            try
            {
                this.lblNurse.Text =  PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            }
            catch
            {
            }
            

            //this.lblPrepayCost.Text = FS.FrameWork.Public.String.FormatNumber(PrepayTmp.FT.PrepayCost, 2).ToString();//金额合计

            this.lblRecipe.Text = PrepayTmp.RecipeNO;//预交金发票号

            lblHongBao.Text = "入院日期：" + PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");

            //
            this.labBill.Text = PrepayTmp.User03;

            #region //日期
            this.lblDate.Text = PrepayTmp.PrepayOper.OperTime.ToShortDateString();
           // this.lblYear.Text = PrepayTmp.PrepayOper.OperTime.ToShortDateString(); //PrepayTmp.PrepayOper.OperTime.Year.ToString();
            //this.lblMonth.Text = PrepayTmp.PrepayOper.OperTime.Month.ToString().PadLeft(2, '0');
            //this.lblDay.Text = PrepayTmp.PrepayOper.OperTime.Day.ToString().PadLeft(2, '0');
            #endregion
            //总金额
            decimal totCost = decimal.Zero;
            //计数器
            int k = 0;
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay Prepay in alPrepay)
            {
                #region //显示付款单位

                switch (Prepay.PayType.ID.Trim())
                {
                    case "CA":
                        Prepay.PayType.Name = "现金";
                        break;
                    case "PB":
                        Prepay.PayType.Name = "记账";
                        break;
                    case "CD":
                        Prepay.PayType.Name = "信用卡";
                        break;
                    case "UP":
                        Prepay.PayType.Name = "POS";
                        break;
                    case "CI":
                        Prepay.PayType.Name = "商保记账";
                        break;
                    case "PO":
                        Prepay.PayType.Name = "转账";
                        break;
                    case "MCZH":
                        Prepay.PayType.Name = "社会保障卡(珠海)";
                        break;
                    case "MCZS":
                        Prepay.PayType.Name = "社会保障卡(中山)";
                        break;
                    case "PBZH":
                        Prepay.PayType.Name = "珠海医保统筹";
                        break;
                    case "PBZS":
                        Prepay.PayType.Name = "中山医保统筹";
                        break;
                    case "RC":
                        Prepay.PayType.Name = "优惠";
                        break;
                    case "CH":
                        Prepay.PayType.Name = "支票";
                        break;
                    case "GZ":
                        Prepay.PayType.Name = "社会保障卡(广州)";
                        break;
                    case "WP":
                        Prepay.PayType.Name = "微信";
                        break;
                    case "ZB":
                        Prepay.PayType.Name = "支付宝";
                        break;
                    default:
                        Prepay.PayType.Name = "其他";
                        break;
                }
                if (k == 0)
                {
                    this.lblType.Text =  Prepay.PayType.Name;//付款方式
                    #region //显示小写金额
                    //string[] strLower = this.GetLowerCashbyNumber(Prepay.FT.PrepayCost);
                    //this.lblLowF.Text = strLower[0];
                    //this.lblLowJ.Text = strLower[1];
                    //this.lblLowY.Text = strLower[3];
                    //this.lblLowS.Text = strLower[4];
                    //this.lblLowB.Text = strLower[5];
                    //this.lblLowQ.Text = strLower[6];
                    //this.lblLowW.Text = strLower[7];
                    //this.lblLowSW.Text = strLower[8];
                    this.lblLowBW.Text = "￥" + FS.FrameWork.Public.String.FormatNumber(Prepay.FT.PrepayCost, 2).ToString("############.00"); //strLower[9];

                    #endregion
                }
                else if (k == 1)
                {
                    this.lblType1.Text =  Prepay.PayType.Name ;//付款方式
                    #region //显示小写金额
                    //string[] strLower = this.GetLowerCashbyNumber(Prepay.FT.PrepayCost);
                    //this.lblLowF1.Text = strLower[0];
                    //this.lblLowJ1.Text = strLower[1];
                    //this.lblLowY1.Text = strLower[3];
                    //this.lblLowS1.Text = strLower[4];
                    //this.lblLowB1.Text = strLower[5];
                    //this.lblLowQ1.Text = strLower[6];
                    //this.lblLowW1.Text = strLower[7];
                    //this.lblLowSW1.Text = strLower[8];
                    this.lblLowBW1.Text = "￥" + FS.FrameWork.Public.String.FormatNumber(Prepay.FT.PrepayCost, 2).ToString("############.00"); //strLower[9];

                    #endregion
                }
                else
                {
                    this.lblType2.Text = "￥" + Prepay.PayType.Name;//付款方式
                    #region //显示小写金额
                    //string[] strLower = this.GetLowerCashbyNumber(Prepay.FT.PrepayCost);
                    //this.lblLowF2.Text = strLower[0];
                    //this.lblLowJ2.Text = strLower[1];
                    //this.lblLowY2.Text = strLower[3];
                    //this.lblLowS2.Text = strLower[4];
                    //this.lblLowB2.Text = strLower[5];
                    //this.lblLowQ2.Text = strLower[6];
                    //this.lblLowW2.Text = strLower[7];
                    //this.lblLowSW2.Text = strLower[8];
                    this.lblLowBW2.Text = FS.FrameWork.Public.String.FormatNumber(Prepay.FT.PrepayCost, 2).ToString("############.00");// strLower[9];

                    #endregion
                }

                #endregion

                totCost += Prepay.FT.PrepayCost;
                k++;
            }

            #region //显示大写金额
            string[] strMoney = this.GetUpperCashbyNumber(System.Math.Abs(totCost));
            //this.lblF.Text = strMoney[0];
            //this.lblJ.Text = strMoney[1];
            //this.lblY.Text = strMoney[3];
            //this.lblS.Text = strMoney[4];
            //this.lblB.Text = strMoney[5];
            //this.lblQ.Text = strMoney[6];
            //this.lblW.Text = strMoney[7];
            //this.lblSW.Text = strMoney[8];
            //this.lblBW.Text = strMoney[9];
            this.lblBW.Text = "金额大写 " + FS.FrameWork.Function.NConvert.ToCapital(System.Math.Abs(totCost)); //string.Join("", strMoney);
            #endregion

            this.lblPrepayCost.Text = "合计 ￥"+totCost.ToString("F2");

            return 1;
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay Prepay)
        {            
            this.lblDeptName.Text ="科室:"+ PatientInfo.PVisit.PatientLocation.Dept.Name;//患者所在科室
            this.lblName.Text = PatientInfo.Name;//患者姓名
            this.lblOper.Text = Prepay.PrepayOper.Name + "(" + Prepay.PrepayOper.ID + ")";//开票
            this.lblFeeOper.Text = Prepay.PrepayOper.Name+"("+ Prepay.PrepayOper.ID+")";//收费员
            this.lblPatientNo.Text = PatientInfo.PID.ID;
            try
            {
                this.lblNurse.Text ="病区:"+ PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            }
            catch { }


            this.lblPrepayCost.Text ="￥"+ FS.FrameWork.Public.String.FormatNumber(Prepay.FT.PrepayCost, 2).ToString();//金额合计

            this.lblRecipe.Text = Prepay.RecipeNO;//预交金发票号

            lblHongBao.Text = "入院日期：" + PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");

            //
            this.labBill.Text = Prepay.User03;

            #region //日期
            this.lblDate.Text = Prepay.PrepayOper.OperTime.ToShortDateString();
            //this.lblYear.Text = Prepay.PrepayOper.OperTime.ToShortDateString();//Prepay.PrepayOper.OperTime.Year.ToString();
          //  this.lblMonth.Text = Prepay.PrepayOper.OperTime.Month.ToString().PadLeft(2, '0');
           // this.lblDay.Text = Prepay.PrepayOper.OperTime.Day.ToString().PadLeft(2, '0');
            #endregion

            #region //显示小写金额
            string[] strLower = this.GetLowerCashbyNumber(Prepay.FT.PrepayCost);
            //this.lblLowF.Text = strLower[0];
            //this.lblLowJ.Text = strLower[1];
            //this.lblLowY.Text = strLower[3];
            //this.lblLowS.Text = strLower[4];
            //this.lblLowB.Text = strLower[5];
            //this.lblLowQ.Text = strLower[6];
            //this.lblLowW.Text = strLower[7];
            //this.lblLowSW.Text = strLower[8];
            this.lblLowBW.Text = FS.FrameWork.Public.String.FormatNumber(Prepay.FT.PrepayCost, 2).ToString("############.00");

            #endregion

            #region //显示大写金额
            string[] strMoney = this.GetUpperCashbyNumber(System.Math.Abs(Prepay.FT.PrepayCost));
            this.lblF.Text = strMoney[0];
            this.lblJ.Text = strMoney[1];
            this.lblY.Text = strMoney[3];
            this.lblS.Text = strMoney[4];
            this.lblB.Text = strMoney[5];
            this.lblQ.Text = strMoney[6];
            this.lblW.Text = strMoney[7];
            this.lblSW.Text = strMoney[8];
            this.lblBW.Text = strMoney[9];
            #endregion

            #region //显示付款单位

            switch (Prepay.PayType.ID.Trim())
            {
                case "CA":   //现金
                    Prepay.PayType.Name = "现金";
                    break;
                case "CD":   //支票  
                    Prepay.PayType.Name = "中行磁卡";		
                    break;
                case "DB"://汇票
                    Prepay.PayType.Name = "商行磁卡";
                    break;
                case "CH":
                    Prepay.PayType.Name = "支票";
                    break;
                case "PO":
                    Prepay.PayType.Name = "汇款";		
                    break;
                case "RC":
                    Prepay.PayType.Name = "优惠";
                    break;
                case "PY":
                    Prepay.PayType.Name = "医保卡";
                    break;
                case "PB":
                    Prepay.PayType.Name = "记账";
                    break;
                default:
                    Prepay.PayType.Name = "其他";
                    break;
            }
            this.lblType.Text = "("+Prepay.PayType.Name+")";//付款方式
           
            return 1;
            #endregion
        }

        public IDbTransaction Trans
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        private void lab_Click(object sender, EventArgs e)
        {

        }
    }
}
