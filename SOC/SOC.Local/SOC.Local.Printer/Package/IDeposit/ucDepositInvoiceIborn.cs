using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Local.Printer.Package.IDeposit
{
    public partial class ucDepositInvoiceIborn : UserControl
    {
        #region

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 押金管理
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Fee.Deposit depositMgr = new FS.HISFC.Models.MedicalPackage.Fee.Deposit();

        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        FS.FrameWork.Public.ObjectHelper payModesHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 发票管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit depositManager = new FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit();

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


        public ucDepositInvoiceIborn()
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

                //FS.HISFC.Models.Base.PageSize ps = null;
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("TCYJ", 787, 400);

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
        public int SetPrintValue(System.Collections.ArrayList InvoiceList)
        {
            try
            {

                string InvoiceNO = string.Empty;
                string InvoiceNOStr = string.Empty;

                //支付方式
                if (payModesHelper.ArrayObject == null || payModesHelper.ArrayObject.Count == 0)
                {
                    payModesHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
                }

                FS.HISFC.Models.MedicalPackage.Fee.Deposit tmp = InvoiceList[0] as FS.HISFC.Models.MedicalPackage.Fee.Deposit;
                this.patientInfo = accountMgr.GetPatientInfoByCardNO(tmp.CardNO);

                decimal TotPay = 0.0m;
                string payMode = string.Empty;
                string Memo = string.Empty;

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in InvoiceList)
                {
                    //InvoiceNO += "'" + deposit.ID + "'" + ",";
                    //InvoiceNOStr += deposit.ID + ",";

                    TotPay += deposit.Amount;

                    if (!string.IsNullOrEmpty(deposit.Memo))
                    {
                        Memo += deposit.Memo + ";";
                    }

                    payMode += " " + payModesHelper.GetObjectFromID(deposit.Mode_Code).Name
                        + " " + FS.FrameWork.Public.String.FormatNumber(deposit.Amount, 2) + "；  ";
                }

                this.lblCardNO.Text = patientInfo.PID.CardNO;
                this.lblName.Text = patientInfo.Name;

                if (this.patientInfo.Sex.ID.ToString() == "M")
                {
                    this.lblSex.Text = "男";
                }
                else if (this.patientInfo.Sex.ID.ToString() == "F")
                {
                    this.lblSex.Text = "女";
                }
                else
                {
                    this.lblSex.Text = "未知";
                }

                this.lblAmount.Text = TotPay.ToString();
                this.lblMemo.Text = Memo;
                this.lblPaymode.Text = payMode;
                this.lblFeeOper.Text = FS.FrameWork.Management.Connection.Operator.ID;
                this.lblTime.Text = tmp.OperTime.ToString();

                FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
                this.lblTitle.Text = curDepartment.HospitalName + "定金收据"; //{C26B1D96-11D1-4022-A123-29F593840556}  {39615b46-f5bd-44fc-a53d-25976788f4fb}

//                if (InvoiceNO.Length > 0)
//                {
//                    InvoiceNO = InvoiceNO.TrimEnd(',');
//                }

//                if (InvoiceNOStr.Length > 0)
//                {
//                    InvoiceNOStr = InvoiceNOStr.TrimEnd(',');
//                }

//                if (string.IsNullOrEmpty(InvoiceNO))
//                {
//                    return 0;
//                }

//                string strSQL = @"select wm_concat(pay_type)
//                                    from ( select distinct (select y.name|| '  ' || to_char(f.amount) 
//                                                              from com_dictionary y
//                                                             where y.type='PAYMODES'
//                                                               and y.code=f.mode_code ) pay_type
//                                             from exp_packagedeposit f
//                                            where f.deposit_no in ({0})
//                                                 and cancel_flag = '0')";

//                strSQL = string.Format(strSQL, InvoiceNO);
//                string strPayType = this.depositManager.ExecSqlReturnOne(strSQL);

//                //缴费方式
//                this.lblPayType.Text = strPayType;
//                this.lbinvoiceNO.Text = InvoiceNOStr;


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
