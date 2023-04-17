using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using TecService;
using Neusoft.SOC.HISFC.InpatientFee.Components.Balance;

namespace FS.SOC.HISFC.InpatientFee.Components.Invoice
{

    /// <summary>
    /// [功能描述: 发票重打表现类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class ucRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {


        public ucRePrint()
        {
            InitializeComponent();

            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);
            this.txtInvoice.KeyDown += new KeyEventHandler(txtInvoice_KeyDown);
            this.fpBalanceInvoice.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpBalanceInvoice_SelectionChanged);
        }
        //// {ED91FE21-D029-438f-8A4B-5E6C43A1C990}增加日结时间--2015-01-23
        public enum EnumCol
        {
            选择,
            住院号,
            姓名,
            收据号,
            收据编号,
            收费时间, 住院起止日期, 结账方式, 应收金额, 预收金额, 实收金额, 欠费金额, 药费限额, 付款方式, 结账员, 作废员工, 作废时间,日结时间
        }
        #region 变量
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtManager = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy printer = null;
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;


        ArrayList BalanceList = null;

        #endregion

        #region 方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            //清空结算费用信息
            this.fpBalance_Sheet1.Rows.Count = 0;
            this.fpBalanceInvoice_Sheet1.RowCount = 0;
            this.fpPrepay_Sheet1.RowCount = 0;
            this.txtInvoice.Text = string.Empty;
            this.patientInfo = null;
            this.SetPatientInfo(this.patientInfo);
        }

        /// <summary>
        /// 利用患者信息实体进行控件赋值
        /// </summary>
        /// <param name="patientInfo">患者基本信息实体</param>
        protected void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }

            // 姓名
            this.txtName.Text = patientInfo.Name;
            // 科室
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            // 合同单位
            this.txtPact.Text = patientInfo.Pact.Name;
            //床号
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            //生日
            if (patientInfo.Birthday == DateTime.MinValue)
            {
                this.txtBirthday.Text = string.Empty;
            }
            else
            {
                txtBirthday.Text = patientInfo.Birthday.ToShortDateString();
            }
            //所属病区
            txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            //入院日期
            if (patientInfo.PVisit.InTime == DateTime.MinValue)
            {
                this.txtDateIn.Text = string.Empty;
            }
            else
            {
                txtDateIn.Text = patientInfo.PVisit.InTime.ToString();
            }
            //出院日期
            if (patientInfo.PVisit.OutTime == DateTime.MinValue)
            {
                this.txtDateOut.Text = string.Empty;
            }
            else
            {
                this.txtDateOut.Text = patientInfo.PVisit.OutTime.ToString();
            }
            // 医生
            txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            //住院号
            this.ucQueryInpatientNo1.TextBox.Text = patientInfo.PID.PatientNO;
            this.checkBox1.Checked =getOpenEleInvoiceFlag(patientInfo.ID);

        }

        private bool getOpenEleInvoiceFlag(string inpatientno) 
        {
            string sql = "select * from com_opb_eleinvoiceopeninfo where inpatientnoorinvoiceno='" + inpatientno + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            inpatientFeeManager.ExecQuery(sql, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        

        /// <summary>
        /// 设置发票信息
        /// </summary>
        /// <param name="alInvoice"></param>
        /// <param name="invoiceNO"></param>
        private void SetInvoiceInfo(ArrayList alInvoice, string invoiceNO)
        {
            if (alInvoice == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in alInvoice)
            {
                if (balance.TransType== FS.HISFC.Models.Base.TransTypes.Negative)//临时发票号不显示
                {
                    continue;
                }

                this.fpBalanceInvoice_Sheet1.RowCount++;
                int row = this.fpBalanceInvoice_Sheet1.RowCount - 1;
                if (balance.Invoice.ID.Equals(invoiceNO))
                {
                    this.fpBalanceInvoice_Sheet1.Cells[row, 0].Value = true;
                }
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.住院号].Value = patientInfo.PID.PatientNO;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.姓名].Value = patientInfo.Name;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.收据号].Value = balance.PrintedInvoiceNO;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.收据编号].Value = balance.Invoice.ID;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.收费时间].Value = balance.BalanceOper.OperTime.ToString();
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.住院起止日期].Value = balance.BeginTime.ToShortDateString() + "~" + balance.EndTime.ToShortDateString();
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.结账方式].Value = balance.BalanceType.ToString();
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.应收金额].Value = balance.FT.TotCost;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.预收金额].Value = balance.FT.PrepayCost;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.实收金额].Value = balance.FT.TotCost - balance.FT.ArrearCost;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.欠费金额].Value = balance.FT.ArrearCost;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.药费限额].Value = patientInfo.Pact.DayQuota;
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.付款方式].Value = "";
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.结账员].Value = CommonController.CreateInstance().GetEmployeeName(balance.BalanceOper.ID);
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.作废员工].Value = CommonController.Instance.GetEmployeeName(balance.CancelOper.ID);
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.作废时间].Value = balance.CancelOper.OperTime.ToString();

                //// {ED91FE21-D029-438f-8A4B-5E6C43A1C990}增加日结时间--2015-01-23
                this.fpBalanceInvoice_Sheet1.Cells[row, (int)EnumCol.日结时间].Value = balance.DayTime.ToString();
                this.fpBalanceInvoice_Sheet1.Rows[row].Tag = balance;

                if (balance.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
                {
                    this.fpBalanceInvoice_Sheet1.Rows[row].ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 显示结算信息
        /// </summary>
        private void SetBalanceInfo()
        {
            if (this.fpBalanceInvoice_Sheet1.RowCount == 0 || this.fpBalanceInvoice_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            FS.HISFC.Models.Fee.Inpatient.Balance balance = this.fpBalanceInvoice_Sheet1.Rows[this.fpBalanceInvoice_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.Inpatient.Balance;
            this.SetBalanceList(balance.ID,balance.Invoice.ID);
            this.SetBalancePrepay(balance.ID);
        }

        /// <summary>
        /// 设置结算预交金信息
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        private int SetBalancePrepay(string balanceNO)
        {
            this.fpPrepay_Sheet1.RowCount = 0;
            ArrayList alPrepay = this.inpatientFeeManager.QueryPrepaysByInpatientNOAndBalanceNO(this.patientInfo.ID, balanceNO);
            if (alPrepay == null)
            {
                CommonController.CreateInstance().MessageBox("获取结算预交金失败，原因："+this.inpatientFeeManager.Err, MessageBoxIcon.Warning);
                return -1;
            }

            FS.HISFC.Models.Fee.Inpatient.Prepay prepay;
            for (int i = 0; i < alPrepay.Count; i++)
            {
                prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)alPrepay[i];
                this.fpPrepay_Sheet1.Rows.Add(this.fpPrepay_Sheet1.Rows.Count, 1);
                this.fpPrepay_Sheet1.Cells[i, 0].Value = prepay.RecipeNO;
                this.fpPrepay_Sheet1.Cells[i, 1].Value = prepay.PayType.Name;
                this.fpPrepay_Sheet1.Cells[i, 2].Value = prepay.FT.PrepayCost;
                this.fpPrepay_Sheet1.Cells[i, 3].Value = CommonController.CreateInstance().GetEmployeeName(prepay.BalanceOper.ID);
                this.fpPrepay_Sheet1.Cells[i, 4].Value = prepay.BalanceOper.OperTime.ToString();
            }

            return 1;
        }

        /// <summary>
        /// 显示本次召回的balancelist信息
        /// </summary>
        /// <param name="balanceNO">结算序号</param>
        /// <returns>1成功－1失败</returns>
        protected virtual int SetBalanceList(string balanceNO,string invoiceNO)
        {
            this.fpBalance_Sheet1.RowCount = 0;
            //获取结算明细信息
            ArrayList alBalanceList = this.inpatientFeeManager.QueryBalanceListsByInpatientNOAndBalanceNO(this.patientInfo.ID, invoiceNO,FS.FrameWork.Function.NConvert.ToInt32(balanceNO));

            BalanceList = alBalanceList;
            
            if (alBalanceList == null)
            {
                CommonController.CreateInstance().MessageBox("获取结算信息失败，原因：" + this.inpatientFeeManager.Err, MessageBoxIcon.Warning);
                return -1;
            }

            FS.HISFC.Models.Fee.Inpatient.BalanceList balanceList;

            for (int i = 0; i < alBalanceList.Count; i++)
            {
                balanceList = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                this.fpBalance_Sheet1.Rows.Add(this.fpBalance_Sheet1.Rows.Count, 1);
                //添加结算明细
                this.fpBalance_Sheet1.Cells[i, 0].Value = balanceList.FeeCodeStat.StatCate.Name;
                this.fpBalance_Sheet1.Cells[i, 1].Value = balanceList.BalanceBase.FT.TotCost;
                this.fpBalance_Sheet1.Cells[i, 2].Value = CommonController.CreateInstance().GetEmployeeName(balanceList.BalanceBase.BalanceOper.ID);
                this.fpBalance_Sheet1.Cells[i, 3].Value = balanceList.BalanceBase.BalanceOper.OperTime.ToString();
            }

            return 1;
        }

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.Fee.Inpatient.Balance> GetInvoiceInfo()
        {
            if (this.patientInfo == null)
            {
                return null;
            }

            if (this.fpBalanceInvoice_Sheet1.RowCount == 0)
            {
                return null;
            }

            List<FS.HISFC.Models.Fee.Inpatient.Balance> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            //取选择的发票信息
            for (int i = 0; i < this.fpBalanceInvoice_Sheet1.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpBalanceInvoice_Sheet1.Cells[i, 0].Value))
                {
                    balanceList.Add(this.fpBalanceInvoice_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Inpatient.Balance);
                }
            }

            return balanceList;
        }

        /// <summary>
        /// 住院号回车处理
        /// </summary>
        private void QueryByInPatientNO(string inpatientNO,string invoiceNO)
        {
            this.fpBalanceInvoice_Sheet1.RowCount = 0;
            //通过住院号获取住院基本信息
            this.patientInfo = this.radtManager.GetPatientInfo(inpatientNO);
            if (this.patientInfo == null)
            {
                CommonController.CreateInstance().MessageBox("查找患者信息失败，" + radtManager.Err, MessageBoxIcon.Warning);
                return;
            }

            ArrayList alAllBill = this.inpatientFeeManager.QueryBalancesByInpatientNO(this.patientInfo.ID, "ALL");//出院结算发票。
            if (alAllBill == null)
            {
                CommonController.CreateInstance().MessageBox("获取发票号出错，" + this.inpatientFeeManager.Err, MessageBoxIcon.Warning);
                return;
            }
            if (alAllBill.Count < 1)
            {
                CommonController.CreateInstance().MessageBox("该患者没有已结算的发票,请通过发票号查询!", MessageBoxIcon.Warning);
                return;
            }
            this.SetPatientInfo(this.patientInfo);
            this.SetInvoiceInfo(alAllBill, invoiceNO);
            this.SetBalanceInfo();
            this.GetNextInvoiceNO();
        }

        /// <summary>
        /// 根据发票号查询信息
        /// </summary>
        private void QueryByInoviceNO(string invoiceNO)
        {
            //获取输入发票实体信息
            ArrayList al = this.inpatientFeeManager.QueryBalancesByInvoiceNO(invoiceNO);
            if (al == null || al.Count != 1)
            {
                CommonController.CreateInstance().MessageBox("查询指定发票信息失败！", MessageBoxIcon.Warning);
                return;
            }

            FS.HISFC.Models.Fee.Inpatient.Balance balance = al[0] as FS.HISFC.Models.Fee.Inpatient.Balance;
            this.QueryByInPatientNO(balance.Patient.ID,invoiceNO);
        }
        /// <summary>
        /// 住院凭证打印// {249D5ADE-8BE6-4c90-A8D1-F99540D24A64}
        /// </summary>
        private void PatientCertificatePrint()
        {
            ///以下代码可运行
            if (patientInfo == null)
            {
                return;
            }
            FS.HISFC.Components.RADT.Controls.ucPatientCertificatePrint print = new FS.HISFC.Components.RADT.Controls.ucPatientCertificatePrint();
            print.SetPatientInfo(patientInfo);
            print.PrintPreview();
            //print.Print();
        }

        /// <summary>
        /// 补打发票，补打不走号
        /// </summary>
        private void RePrintInvoice(bool isNewInvoiceNO, bool isPrintPreview)
        {
            List<FS.HISFC.Models.Fee.Inpatient.Balance> balanceList = this.GetInvoiceInfo();
            if (balanceList == null||balanceList.Count==0)
            {
                CommonController.CreateInstance().MessageBox("请选择勾选需要打印的发票记录", MessageBoxIcon.Information);
                return;
            }

            if (isNewInvoiceNO)
            {
                //作废重打发票，日结之后不能-有待完善?gumzh
                if ((balanceList[0] as FS.HISFC.Models.Fee.Inpatient.Balance).IsDayBalanced)
                {
                    MessageBox.Show("该发票已经日结，不允许作废重打发票!");
                    return;
                }

                //作废重打发票，不能交叉打印
                if (this.inpatientFeeManager.Operator.ID != (balanceList[0] as FS.HISFC.Models.Fee.Inpatient.Balance).BalanceOper.ID)
                {
                    MessageBox.Show("作废重打发票，不允许交叉打印!");
                    return;
                }

                //处理重新结算
                FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();
                if (balanceManager.ReBalance(this.patientInfo, balanceList) < 0)
                {
                    CommonController.CreateInstance().MessageBox("重新结算失败，原因：" + balanceManager.Err, MessageBoxIcon.Warning);
                    return;

                }
            }

            this.GetNextInvoiceNO();

            if (balanceList.Count > 0)
            {
                string errText = "";
                if (isPrintPreview)
                {
                    if (Function.PrintPreviewInvoice(this.patientInfo, balanceList, ref errText) < 0)
                    {
                        CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (Function.PrintInvoice(this.patientInfo, balanceList, ref errText) < 0)
                    {
                        CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            this.Clear();
        }

        /// <summary>
        /// 获取下一发票号
        /// </summary>
        private void GetNextInvoiceNO()
        {
            lblNextInvoiceNO.Text = "";
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string invoiceNO = "";
            string realInvoiceNO = "";
            string errText = "";

            this.feeIntegrate.GetInvoiceNO(oper, "I", ref invoiceNO, ref realInvoiceNO, ref errText);

            if (string.IsNullOrEmpty(invoiceNO))
            {
                //未领取发票则弹出窗口输入
                FS.HISFC.Components.Common.Forms.frmUpdateInvoice frm = new FS.HISFC.Components.Common.Forms.frmUpdateInvoice();
                frm.InvoiceType = "I";
                frm.ShowDialog(this);

                int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "I", ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                    return;
                }
            }

            lblNextInvoiceNO.Text = "电脑号： " + invoiceNO + ", 印刷号：" + realInvoiceNO;
        }
        #endregion

        #region 重载

        public override string Text
        {
            get
            {
                return "住院发票补打（不走号）";
            }
            set
            {
                base.Text = value;
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("补打", "补打发票，不走号！", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);
            toolBarService.AddToolButton("重打", "重打发票，作废原发票，产生新的发票号！", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("清屏", "清除所有信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("收据副本", "收据副本打印，不走号！", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);
            toolBarService.AddToolButton("住院凭证打印", "住院凭证打印", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);

            toolBarService.AddToolButton("医保自费清单打印", "医保自费清单打印", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);

            toolBarService.AddToolButton("生成电子发票", "生成电子发票", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);
            toolBarService.AddToolButton("冲红电子发票", "冲红电子发票", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "补打":
                    this.RePrintInvoice(false,false);
                    break;
                case "重打":
                    this.RePrintInvoice(true,false);
                    break;
                case "清屏":
                    this.Clear();
                    break;
                case "收据副本":
                    this.RePrintInvoice(false, true);
                    break;
                case "住院凭证打印":
                    this.PatientCertificatePrint();
                    break;
                case "医保自费清单打印":
                    this.PrintSIItemList();
                    break;
                case "生成电子发票":
                    frmEleInvoice frmEle = new frmEleInvoice();
                    frmEle.SetPatientInfo(this.patientInfo);
                    frmEle.StartPosition = FormStartPosition.CenterScreen;
                    frmEle.Show();
                    //this.SerializeEntityNew();
                    break;
                case "冲红电子发票":
                    MessageBox.Show("冲红电子发票");
                    frmEleInvoiceManager frm = new frmEleInvoiceManager();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.Show();
                    //this.HanTianinInvoiceRedApplyNew();
                    break;
                default: 
                    break;

            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #region  旧电子发票

        //public void SerializeEntityNew()
        //{
        //    if (this.textBox1.Text.Trim().ToString() == "")
        //    {
        //        MessageBox.Show("邮箱地址不能为空！");
        //        return;
        //    }

        //    FS.HISFC.Models.Fee.Inpatient.Balance balance = this.fpBalanceInvoice.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Fee.Inpatient.Balance;
        //    MessageBox.Show(balance.Invoice.ID + "   " + patientInfo.PID.ID+"  "+patientInfo.PID.PatientNO);
        //    return;

        //    //    <req> 
        //    //    <feeList> 
        //    //     <feeItem> 
        //    //      <taxExcludedAmount/>  
        //    //      <num/>  
        //    //      <price/>  
        //    //      <goodsName/>  
        //    //      <taxIncludedAmount/> 
        //    //     </feeItem> 
        //    //    </feeList>  
        //    //    <checker/>  
        //    //    <payee/>  
        //    //    <email/>
        //    //    <clerk/>
        //    //    <orderNo/>  
        //    //    <buyerName/>  
        //    //    <invoiceDate/> 
        //    //</req>

        //    string req = "<req><feeList>{0}</feeList><checker>{1}</checker><payee>{2}</payee><email>{3}</email><orderNo>{4}</orderNo><buyerName>{5}</buyerName><invoiceType>1</invoiceType></req>";
        //    string feeList = "";
        //    string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount></feeItem>";

        //    if (BalanceList != null)
        //    {
        //        FS.HISFC.Models.Fee.Inpatient.BalanceList balanceList;

        //        for (int i = 0; i < BalanceList.Count;i++ )
        //        {
        //            balanceList = (FS.HISFC.Models.Fee.Inpatient.BalanceList)BalanceList[i];

        //            string totalPrice = (balanceList.BalanceBase.FT.OwnCost + balanceList.BalanceBase.FT.PayCost + balanceList.BalanceBase.FT.PubCost).ToString();
        //            string goodsName = balanceList.FeeCodeStat.Name;// fi.FeeCodeStat.Name + " " + fi.Name;

        //            feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice);
        //        }
        //    }

        //   // BalanceList b = comBalanceLists[0] as BalanceList;

        //    string orderno = "";// this.tbInvoiceNo.Text.Trim().ToString();

        //    string name = "";// this.tbOperName.Text.Trim().ToString();// ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).Name;

        //   //req = string.Format(req, feeList, name, name, this.neuTextBox1.Text.Trim().ToString(), orderno, this.tbPName.Text.Trim().ToString());
        //    req = string.Format(req, feeList, name, name, this.textBox1.Text.Trim().ToString(), orderno,"".ToString());


        //    //FS.HISFC.Models.Fee.Inpatient.Balance balance=this.fpBalanceInvoice.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Fee.Inpatient.Balance;

        //    if (true)
        //    {
        //        return;
        //    }


        //    string url = "http://localhost:8081/IbornCrmService.asmx";
        //    // string req = "";
        //    //ArrayList a = new ArrayList();
        //    //string jsonData = this.SerializeEntityNew();

        //    string resultXml = WSHelper.InvokeWebService(url, "HanTianinrequestBillingNewNew", new string[] { req }) as string;
        //    MessageBox.Show(resultXml);

        //    //开票成功=》查询开票信息
        //    string invoiceno = "";// this.tbInvoiceNo.Text.Trim().ToString();

        //    req = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + resultXml + "</serialNos><invoiceno>" + invoiceno + "</invoiceno></req>";

        //    resultXml = WSHelper.InvokeWebService(url, "HanTianQueryInvoiceResult", new string[] { req }) as string;
        //    MessageBox.Show(resultXml);
        //}


        //public void HanTianinInvoiceRedApplyNew()
        //{
        //    if (this.textBox1.Text.Trim().ToString() == "")
        //    {
        //        MessageBox.Show("邮箱地址不能为空！");
        //        return;
        //    }

        //    //    <req> 
        //    //    <feeList> 
        //    //     <feeItem> 
        //    //      <taxExcludedAmount/>  
        //    //      <num/>  
        //    //      <price/>  
        //    //      <goodsName/>  
        //    //      <taxIncludedAmount/> 
        //    //     </feeItem> 
        //    //    </feeList>  
        //    //    <checker/>  
        //    //    <payee/>  
        //    //    <email/>
        //    //    <clerk/>
        //    //    <orderNo/>  
        //    //    <buyerName/>  
        //    //    <invoiceDate/> 
        //    //</req>

        //    string req = "<req><feeList>{0}</feeList><checker>{1}</checker><payee>{2}</payee><email>{3}</email><orderNo>{4}</orderNo><buyerName>{5}</buyerName><invoiceType>2</invoiceType></req>";
        //    string feeList = "";
        //    string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount></feeItem>";

        //    if (BalanceList!= null)
        //    {
        //        FS.HISFC.Models.Fee.Inpatient.BalanceList balanceList;

        //        for (int i = 0; i < BalanceList.Count;i++ )
        //        {
        //            balanceList = (FS.HISFC.Models.Fee.Inpatient.BalanceList)BalanceList[i];


        //            string totalPrice = (balanceList.BalanceBase.FT.OwnCost + balanceList.BalanceBase.FT.PayCost + balanceList.BalanceBase.FT.PubCost).ToString();
        //            string goodsName = balanceList.FeeCodeStat.Name;// fi.FeeCodeStat.Name + " " + fi.Name;

        //            feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice);
        //        }
        //    }

        //    //BalanceList b = comBalanceLists[0] as BalanceList;

        //    string orderno = "";// this.tbInvoiceNo.Text.Trim().ToString();

        //    string name = "";// this.tbOperName.Text.Trim().ToString();// ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).Name;

        //    //req = string.Format(req, feeList, name, name, this.neuTextBox1.Text.Trim().ToString(), orderno, this.tbPName.Text.Trim().ToString());

        //    req = string.Format(req, feeList, name, name, this.textBox1.Text.Trim().ToString(), orderno, "".ToString());


        //    string url = "http://localhost:8081/IbornCrmService.asmx";
        //    // string req = "";
        //    //ArrayList a = new ArrayList();
        //    //string jsonData = this.SerializeEntityNew();

        //    string resultXml = WSHelper.InvokeWebService(url, "HanTianinrequestBillingNewNew", new string[] { req }) as string;
        //    MessageBox.Show(resultXml);

        //    //开票成功=》查询开票信息
        //    string invoiceno = "";// this.tbInvoiceNo.Text.Trim().ToString();

        //    req = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + resultXml + "</serialNos><invoiceno>" + invoiceno + "</invoiceno></req>";

        //    resultXml = WSHelper.InvokeWebService(url, "HanTianQueryInvoiceResult", new string[] { req }) as string;
        //    MessageBox.Show(resultXml);
        //}

        #endregion

        //{C4D0129A-200C-4b8d-8F4C-D550531C7967}

        public void SerializeEntityNew()
        {
            if (this.textBox1.Text.Trim().ToString() == "")
            {
                MessageBox.Show("邮箱地址不能为空！");
                return;
            }

            string req = "<req><feeList>{0}</feeList><checker>{1}</checker><payee>{2}</payee><email>{3}</email><orderNo>{4}</orderNo><buyerName>{5}</buyerName><clerk>{6}</clerk><invoiceType>1</invoiceType></req>";
            string feeList = "";
            string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount></feeItem>";

            FS.HISFC.Models.Fee.Inpatient.Balance balance = this.fpBalanceInvoice.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Fee.Inpatient.Balance;
            //    MessageBox.Show(balance.Invoice.ID + "   " + patientInfo.PID.ID+"  "+patientInfo.PID.PatientNO);

            string orderno = balance.Invoice.ID.ToString();// this.tbInvoiceNo.Text.Trim().ToString();

            string clerk=((FS.HISFC.Models.Base.Employee)this.inpatientFeeManager.Operator).Name;
            string checker = "夏涵";
            string payee = CommonController.CreateInstance().GetEmployeeName(balance.BalanceOper.ID);


            

            string name = "吴测试";// this.tbOperName.Text.Trim().ToString();// ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).Name;

            //req = string.Format(req, feeList, name, name, this.neuTextBox1.Text.Trim().ToString(), orderno, this.tbPName.Text.Trim().ToString());

            string invoiceno = balance.Invoice.ID.ToString();// this.tbInvoiceNo.Text.Trim().ToString();
            string patientno = patientInfo.PID.PatientNO.ToString();
            //patientInfo.Patient.

            //查该发票是否上传有医保费用
            DataSet ds = new DataSet();

            ds = this.QueryInvoiceNoByYB(invoiceno, patientno);

            #region 医保

            if (ds != null)
            {
                DataTable ybdt = ds.Tables[0];

                for (int i = 0; i < ybdt.Rows.Count; i++)
                {
                    string totalPrice = ybdt.Rows[i]["feetype"].ToString();
                    string goodsName = ybdt.Rows[i]["fee_stat_name"].ToString();
                    feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice);
                }

                //req = string.Format(req, feeList, name, name, this.neuTextBox1.Text.Trim().ToString(), "yb" + orderno, this.tbPName.Text.Trim().ToString());
                //req = string.Format(req, feeList, name, name, this.textBox1.Text.Trim().ToString(), "yb" + orderno, this.txtName.Text.ToString());
                req = string.Format(req, feeList, checker, payee, this.textBox1.Text.Trim().ToString(), "yb" + orderno, this.txtName.Text.ToString(), clerk);


                HanTianinrequestBillingNew(req, "yb" + invoiceno);

                if (MessageBox.Show("是否生成普通电子发票？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    req = "<req><feeList>{0}</feeList><checker>{1}</checker><payee>{2}</payee><email>{3}</email><orderNo>{4}</orderNo><buyerName>{5}</buyerName><invoiceType>1</invoiceType></req>";
                    feeList = "";
                    feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount></feeItem>";


                    DataSet ptfpds = new DataSet();
                    ptfpds = QueryInvoiceNoByPTFP(invoiceno);
                    if (ptfpds != null)
                    {
                        string totalPrice = "";
                        string goodsName = "";

                        DataTable ptfpdt = ptfpds.Tables[0];
                        for (int i = 0; i < ptfpdt.Rows.Count; i++)
                        {
                            for (int b = 0; b < ybdt.Rows.Count; b++)
                            {
                                totalPrice = "";
                                if (ptfpdt.Rows[i]["stat_code"].ToString() == ybdt.Rows[b]["fee_stat_cate"].ToString())
                                {
                                    //普通发票的费用=总费用-医保费用
                                    totalPrice = (Convert.ToDouble(ptfpdt.Rows[i]["totalprice"]) - Convert.ToDouble(ybdt.Rows[b]["feetype"])).ToString();
                                    break;
                                }
                                //else
                                //{
                                //    totalPrice = ptfpdt.Rows[i]["totalprice"].ToString();
                                //}
                            }

                            if (totalPrice == "")
                            {
                                totalPrice = ptfpdt.Rows[i]["totalprice"].ToString();
                            }

                            goodsName = ptfpdt.Rows[i]["stat_name"].ToString();

                            feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice);


                        }

                        //req = string.Format(req, feeList, name, name, this.neuTextBox1.Text.Trim().ToString(), "pt" + orderno, this.tbPName.Text.Trim().ToString());

                        req = string.Format(req, feeList, checker, payee, this.textBox1.Text.Trim().ToString(), "pt" + orderno, this.txtName.Text.ToString(),clerk);


                        HanTianinrequestBillingNew(req, "pt" + invoiceno);

                        return;

                    }
                }

                return;

            }

            #endregion

            DataSet ptfpds1 = new DataSet();
            ptfpds1 = QueryInvoiceNoByPTFP(invoiceno);
            if (ptfpds1 != null)
            {
                string totalPrice = "";
                string goodsName = "";

                DataTable ptfpdt1 = ptfpds1.Tables[0];
                for (int i = 0; i < ptfpdt1.Rows.Count; i++)
                {

                    totalPrice = ptfpdt1.Rows[i]["totalprice"].ToString();
                    goodsName = ptfpdt1.Rows[i]["stat_name"].ToString();

                    feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice);

                }


            }

            //req = string.Format(req, feeList, name, name, this.textBox1.Text.Trim().ToString(), "pt" + orderno, this.tbPName.Text.Trim().ToString());
            //req = string.Format(req, feeList, name, name, this.textBox1.Text.Trim().ToString(), "pt" + orderno, this.txtName.Text.ToString());
            req = string.Format(req, feeList, checker, payee, this.textBox1.Text.Trim().ToString(), "pt" + orderno, this.txtName.Text.ToString(), clerk);


            HanTianinrequestBillingNew(req, "pt" + invoiceno);


            if (true) { return; }


        }


        public void HanTianinrequestBillingNew(string req, string invoiceno)
        {
            string url = "http://localhost:8081/IbornCrmService.asmx";// "http://192.168.34.10:8082/IbornCrmService.asmx";
            // string req = "";
            //ArrayList a = new ArrayList();
            //string jsonData = this.SerializeEntityNew();
            string reqxml = req;
            string resultXml = WSHelper.InvokeWebService(url, "HanTianinrequestBillingNewNew", new string[] { req }) as string;
            //string resultXml = req;
            MessageBox.Show(resultXml);

            //开票成功=》查询开票信息


            string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + resultXml + "</serialNos><invoiceno>" + invoiceno + "</invoiceno><reqxml>" + reqxml + "</reqxml></req>";

            resultXml = WSHelper.InvokeWebService(url, "HanTianQueryInvoiceResult", new string[] { reqbyInvoicResult }) as string;
            MessageBox.Show(resultXml);
        }

        public DataSet QueryInvoiceNoByYB(string invoiceno,string patientno)
        {
            string sql = @" select t.mdtrt_id,t.setl_id,t.invoice_no,t.* from fin_ipr_siinmaininfo t
                        where t.type_code = '2'   --1门诊 2住院
                        and t.valid_flag = '1'
                        and t.balance_state = '1'
                        --and t.oper_date > trunc(sysdate) - 7
                        --门诊：and t.invoice_no = '210514870004'
                        and t.patient_no = '{0}' and t.invoice_no='{1}'";

            sql = string.Format(sql,patientno, invoiceno);
            DataSet ds = new DataSet();

            int result = inpatientFeeManager.ExecQuery(sql, ref ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string mdtrt_id = dt.Rows[0]["mdtrt_id"].ToString();
                string setl_id = dt.Rows[0]["setl_id"].ToString();

                sql = @"  select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
                      sum(t.det_item_fee_sumamt) feetype,t3.fee_stat_cate
                      from gzsi_feedetail t  
                      left join gzsi_his_cfxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
                      left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code and t3.report_code = 'MZ01'
                      where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
                      group by t3.fee_stat_name,t3.fee_stat_cate";

                sql = string.Format(sql, mdtrt_id, setl_id);

                DataSet ybfeeds = new DataSet();

                result = inpatientFeeManager.ExecQuery(sql, ref ybfeeds);

                if (ybfeeds.Tables[0].Rows.Count > 0)
                {
                    return ybfeeds;
                }

            }

            return null;

        }

        public DataSet QueryInvoiceNoByPTFP(string invoiceno)
        {
            string sql = @" select t.name,sum(t.tot_cost)-sum(t.eco_cost) totalprice,t.stat_code,t.stat_name from fin_ipb_balancelist t
                         where t.invoice_no='{0}' and t.trans_type=1 group by t.name,t.stat_code,t.stat_name ";

            sql = string.Format(sql, invoiceno);
            DataSet ds = new DataSet();

            int result = inpatientFeeManager.ExecQuery(sql, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }

            return null;

        }

        /// <summary>
        /// 打印医保患者的自费清单
        /// </summary>
        private void PrintSIItemList()
        {
            if (patientInfo == null)
            {
                return;
            }
            IBorn.GZSI.ucSIFeeDetail ucSIFeeDetail = new IBorn.GZSI.ucSIFeeDetail();
            ucSIFeeDetail.SetValue(patientInfo, null);
            ucSIFeeDetail.PrintPage();
        }

        #endregion

        #region 事件

        void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == "")
            {
                CommonController.CreateInstance().MessageBox("此住院号不存在请重新输入！", MessageBoxIcon.Warning);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            this.QueryByInPatientNO(this.ucQueryInpatientNo1.InpatientNo,null);
        }

        void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string invoiceNO = this.txtInvoice.Text.Trim();
                if (!string.IsNullOrEmpty(invoiceNO))
                {
                    invoiceNO = invoiceNO.PadLeft(12, '0');
                    this.txtInvoice.Text = invoiceNO;
                    this.QueryByInoviceNO(invoiceNO);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.GetNextInvoiceNO();
            base.OnLoad(e);
        }

        void fpBalanceInvoice_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.SetBalanceInfo();
        }

        #endregion

        private void txtOutDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbPatientInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
