using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Local.Printer.Package.IPackageInvoice
{
    public partial class ucPackageInvoiceIborn : UserControl, FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice
    {

        #region

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package feepackageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode feepaymodeMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode();
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice feepinvoiceMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();

        private FS.HISFC.BizLogic.MedicalPackage.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();

        
        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        FS.FrameWork.Public.ObjectHelper payModesHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 发票管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice invoiceManager = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        private bool isPreview = false;

        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        #endregion

        public ucPackageInvoiceIborn()
        {
            InitializeComponent();
        }

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

                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZFP", 787, 400);
                //FS.HISFC.Models.Base.PageSize ps = null;
                //ps = psManager.GetPageSize("TCFP");

                if (ps == null )
                {
                    //|| string.IsNullOrEmpty(ps.Printer)
                    //MessageBox.Show("没有找到打印机【TCFP】");
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
                //if (ps != null && !string.IsNullOrEmpty(ps.Printer))
                //{
                //    print.PrintDocument.PrinterSettings.PrinterName = ps.Printer;
                //}
                print.PrintDocument.PrinterSettings.PrinterName = printerName;
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

        /// <summary>
        /// 设置打印值
        /// </summary>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int SetPrintValue(string InvoiceNO)
        {

            try
            {

                if (string.IsNullOrEmpty(InvoiceNO))
                {
                    return -1;
                }

                FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
                this.neuLabel1.Text = curDepartment.HospitalName;

                //设置控件显示
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Length > 6 && "lblPre".Equals(c.Name.Substring(0, 6)))
                    {
                        c.Visible = false;
                    }

                    if (c.Name.Length > 3 && "lbl".Equals(c.Name.Substring(0, 3)))
                    {
                        System.Windows.Forms.Label lblControl = c as System.Windows.Forms.Label;
                        lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    }
                }

                ArrayList packageList = feepackageMgr.QueryByInvoiceNO(InvoiceNO, "0");
                ArrayList payModeList = feepaymodeMgr.QueryByInvoiceNO(InvoiceNO, "0");
                FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice = feepinvoiceMgr.QueryByInvoiceNO(InvoiceNO, "1");

                if (invoice.Cancel_Flag != "0")
                {
                    return -1;
                }

                if (packageList == null || packageList.Count == 0)
                {
                    return -1;
                }

                FS.HISFC.Models.MedicalPackage.Fee.Package tmp = packageList[0] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                this.patientInfo = accountMgr.GetPatientInfoByCardNO(tmp.Patient.PID.CardNO);

                //患者信息
                if (this.patientInfo == null)
                {
                    return -1;
                }

                this.lblInvoice.Text = invoice.InvoiceNO;
                this.lblPriSwBalanceType.Text = invoice.PrintInvoiceNO;
                this.lblPriSwYear.Text = invoice.OperTime.Year.ToString();
                this.lblPriSwMonth.Text = invoice.OperTime.Month.ToString();
                this.lblPriSwDay.Text = invoice.OperTime.Day.ToString();
                this.lblFeeDate.Text = invoice.OperTime.ToString("HH:mm:ss");
                this.lblCardNO.Text = this.patientInfo.PID.CardNO;
                this.lblPriOper.Text = curEmployee.ID;
                this.lblTitle.Text = curDepartment.HospitalName + "套餐收费票据";

                this.lblPriName.Text = this.patientInfo.Name;
                if (patientInfo.Sex.ID.ToString() == "M")//性别错误更改
                {
                    //{7B6E7381-3A6E-4178-8319-9ED40F0A842C}
                    this.lblPriSexM.Text = "√";
                    this.lblPriSexM.Visible = true;
                }
                else if (patientInfo.Sex.ID.ToString() == "F")
                {
                    this.lblPriSexW.Text = "√";
                    this.lblPriSexW.Visible = true;
                }

                //总金额
                this.lbAddTotCost.Text = "";
                this.lblPriPayKind.Text = this.patientInfo.Pact.Name;

                ////费用大类名称
                this.lblPreFeeName1.Text = "套餐费";
                this.lblPreFeeName1.Visible = true;
                this.lblPreFeeName2.Text = "";
                this.lblPreFeeName2.Visible = true;
                this.lblPreFeeName3.Text = "";
                this.lblPreFeeName3.Visible = true;
                this.lblPreFeeName4.Text = "";
                this.lblPreFeeName4.Visible = true;
                this.lblPreFeeName5.Text = "";
                this.lblPreFeeName5.Visible = true;
                this.lblPreFeeName6.Text = "";
                this.lblPreFeeName6.Visible = true;
                this.lblPreFeeName7.Text = "";
                this.lblPreFeeName7.Visible = true;
                this.lblPreFeeName8.Text = "";
                this.lblPreFeeName8.Visible = true;
                this.lblPreFeeName9.Text = "";
                this.lblPreFeeName9.Visible = true;
                this.lblPreFeeName10.Text = "";
                this.lblPreFeeName10.Visible = true;
                this.lblPreFeeName11.Text = "";
                this.lblPreFeeName11.Visible = true;
                this.lblPreFeeName12.Text = "";
                this.lblPreFeeName12.Visible = true;

                //总价格
                this.lblPriCost1.Text = "";
                //支付方式
                this.lbPactName.Text = "";
                //套餐信息
                this.lblPackageInfo.Text = "";

                //{9D840956-6A25-494b-8407-4718EF31D99F}
                decimal TotCost = 0.0m;
                decimal EcoCost = 0.0m;
                decimal RealCost = 0.0m;
                string payKind = "";
                string packageName = "";

                //支付方式
                if (payModesHelper.ArrayObject == null || payModesHelper.ArrayObject.Count == 0)
                {
                    payModesHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
                }

                for (int i = 0; i < payModeList.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode = payModeList[i] as FS.HISFC.Models.MedicalPackage.Fee.PayMode;

                    TotCost += payMode.Tot_cost;

                    //优惠金额
                    if (payMode.Mode_Code == "RC")
                    {
                        EcoCost += payMode.Tot_cost;
                        continue;
                    }
                    //实收金额
                    RealCost += payMode.Tot_cost;

                    payKind += " " + payModesHelper.GetObjectFromID(payMode.Mode_Code).Name
                        + " " + FS.FrameWork.Public.String.FormatNumber(payMode.Tot_cost, 2) + "；  ";
                }

                for (int i = 0; i < packageList.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = packageList[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    package.PackageInfo = packageMgr.QueryPackageByID(package.PackageInfo.ID);
                    packageName += package.PackageInfo.Name;
                }

                this.lblPackageInfo.Text = packageName;
                this.lbPactName.Text = payKind;

                //总金额大小写
                this.lblPriLower.Text = this.lbAddTotCost.Text = this.lblPriCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(TotCost, 2);

                string[] strMoney = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(TotCost, 2));
                this.lblPriF.Text = strMoney[0];
                this.lblPriJ.Text = strMoney[1];
                this.lblPriY.Text = strMoney[3];
                this.lblPriS.Text = strMoney[4];
                this.lblPriB.Text = strMoney[5];
                this.lblPriQ.Text = strMoney[6];
                this.lblPriW.Text = strMoney[7];
                this.lblPriSW.Text = strMoney[8];

                //{9D840956-6A25-494b-8407-4718EF31D99F}
                this.lblPriEcoLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(EcoCost, 2);
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


                string strSQL = @"select wm_concat(pay_type)
                                    from ( select distinct (select y.name|| '  ' || to_char(f.tot_cost) 
                                                              from com_dictionary y
                                                             where y.type='PAYMODES'
                                                               and (y.code=f.mode_code or (f.mode_code  = 'DE' and y.code=f.realated_code ))) pay_type
                                             from exp_packagepaymode f
                                            where f.invoice_no='{0}'
                                                 and cancel_flag = '0')";

                strSQL = string.Format(strSQL, InvoiceNO);
                string strPayType = this.invoiceManager.ExecSqlReturnOne(strSQL);

                //缴费方式
                //this.lblPayType.Text = strPayType;

                //this.lbinvoiceNO.Text = InvoiceNO;

                strSQL = @"select sum(tot_cost)
                             from exp_packagepaymode
                            where cancel_flag = '0'
                              and invoice_no = '{0}'";
                strSQL = string.Format(strSQL, InvoiceNO);
                string strCost = this.invoiceManager.ExecSqlReturnOne(strSQL);

                //this.lbCost.Text = strCost;


                strSQL = @" select wm_concat(packagename)
                                 from ( select (select package_name from bd_com_package a  where a.package_id = t.package_id ) packagename
                                          from exp_package t
                                         where t.invoice_no = '{0}'
                                           and t.cancel_flag = '0')";

                strSQL = string.Format(strSQL, InvoiceNO);

                string strNameList = this.invoiceManager.ExecSqlReturnOne(strSQL);
                //this.lbPackageList.Text = strNameList;

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

        /// <summary>
        /// 设置为打印模式
        /// </summary>
        public void SetToPrintMode()
        {
            //将预览项设为不可见
            //SetLableVisible(false, lblPreview);
            //foreach (Label lbl in lblPrint)
            //{
            //    lbl.BorderStyle = BorderStyle.None;
            //    lbl.BackColor = SystemColors.ControlLightLight;
            //}
        }

        /// <summary>
        /// 设置为预览模式
        /// </summary>
        public void SetToPreviewMode()
        {
            //将预览项设为可见
            //SetLableVisible(true, lblPreview);
            //foreach (Label lbl in lblPrint)
            //{
            //    lbl.BorderStyle = BorderStyle.None;
            //    lbl.BackColor = SystemColors.ControlLightLight;
            //}
        }

    }
}
