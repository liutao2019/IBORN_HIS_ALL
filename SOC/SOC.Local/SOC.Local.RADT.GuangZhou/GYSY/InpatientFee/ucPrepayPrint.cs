using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Local.RADT.GuangZhou.GYSY.InpatientFee
{
    public partial class ucPrepayPrint : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint
    {
        public ucPrepayPrint()
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
            ps = pgMgr.GetPageSize("PrepayPrint");
            //if (ps != null && ps.Printer.ToLower() == "default")
            //{
            //    ps.Printer = "";
            //}
            if (ps == null)
            {
                //默认大小
                ps = new FS.HISFC.Models.Base.PageSize("PrepayPrint", 830, 450);                
            }
            print.SetPageSize(ps);
            print.PrintPage(ps.Left, ps.Top, this);

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            return;
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay Prepay)
        {            
            this.lblDeptName.Text = PatientInfo.PVisit.PatientLocation.Dept.Name;//患者所在科室
            this.lblName.Text = PatientInfo.Name;//患者姓名
            this.lblOper.Text = Prepay.PrepayOper.ID;//开票
            this.lblFeeOper.Text = Prepay.PrepayOper.ID;//收费员
            this.lblPatientNo.Text = PatientInfo.PID.ID;
            try
            {
                this.lblNurse.Text = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            }
            catch { }


            this.lblPrepayCost.Text = FS.FrameWork.Public.String.FormatNumber(Prepay.FT.PrepayCost, 2).ToString();//金额合计

            this.lblRecipe.Text = Prepay.RecipeNO;//预交金发票号

            //
            this.labBill.Text = Prepay.User03;

            #region //日期
            this.lblDate.Text = Prepay.PrepayOper.OperTime.ToShortDateString();
            this.lblYear.Text = Prepay.PrepayOper.OperTime.Year.ToString();
            this.lblMonth.Text = Prepay.PrepayOper.OperTime.Month.ToString().PadLeft(2, '0');
            this.lblDay.Text = Prepay.PrepayOper.OperTime.Day.ToString().PadLeft(2, '0');
            #endregion

            #region //显示小写金额
            string[] strLower = this.GetLowerCashbyNumber(Prepay.FT.PrepayCost);
            this.lblLowF.Text = strLower[0];
            this.lblLowJ.Text = strLower[1];
            this.lblLowY.Text = strLower[3];
            this.lblLowS.Text = strLower[4];
            this.lblLowB.Text = strLower[5];
            this.lblLowQ.Text = strLower[6];
            this.lblLowW.Text = strLower[7];
            this.lblLowSW.Text = strLower[8];
            this.lblLowBW.Text = strLower[9];

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
                case "CH":   //支票  
                    Prepay.PayType.Name = "支票";
                    this.lblBank.Text = "开户行:" + Prepay.Bank.Name;//开户银行
                    this.lblBankAcount.Text = "账号:" + Prepay.Bank.Account;//账号
                    //					this.labCardNo.Text = Prepay.AccountBank.ID;//卡号
                    this.labWorkName.Text = "单位：" + Prepay.Bank.WorkName.ToString();//交款单位				
                    break;
                case "PO"://汇票
                    Prepay.PayType.Name = "汇票";
                    this.lblBank.Text = Prepay.Bank.Name;//开户银行
                    this.lblBankAcount.Text = Prepay.Bank.Account;//账号
                    this.labWorkName.Text = Prepay.Bank.WorkName.ToString();//交款单位
                    break;
                case "DB":
                    Prepay.PayType.Name = "借记卡";
                    this.lblBank.Text = "开户行:" + Prepay.Bank.Name;//开户银行
                    this.lblBankAcount.Text = "卡号:" + Prepay.Bank.Account;//账号
                    //					this.labCardNo.Text = Prepay.AccountBank.ID;//卡号
                    this.labWorkName.Text = "单位：" + Prepay.Bank.WorkName.ToString();//交款单位	
                    break;
                case "CD":
                    Prepay.PayType.Name = "信用卡";
                    this.lblBank.Text = "开户行:" + Prepay.Bank.Name;//开户银行
                    this.lblBankAcount.Text = "卡号:" + Prepay.Bank.Account;//账号
                    //					this.labCardNo.Text = Prepay.AccountBank.ID;//卡号
                    this.labWorkName.Text = "单位：" + Prepay.Bank.WorkName.ToString();//交款单位				
                    break;
                default:
                    Prepay.PayType.Name = "其他";
                    break;
            }
            this.lblType.Text = Prepay.PayType.Name;//付款方式
           
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


        //{014680EC-6381-408b-98FB-A549DAA49B82}
        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, System.Collections.ArrayList alPrepay)
        {
            return -1;
        }

        #endregion
    }
}
