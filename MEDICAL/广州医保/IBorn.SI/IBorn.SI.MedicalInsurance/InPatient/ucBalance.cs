using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace IBorn.SI.MedicalInsurance.InPatient
{
    public partial class ucBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        IBorn.SI.MedicalInsurance.Proxy.MedicalFactoryProxy medicalInterfaceProxy = new Proxy.MedicalFactoryProxy();
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.Models.RADT.PatientInfo currentRegister;

        DataTable currentDtUploadFee;

        public ucBalance()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
            this.ucInPatientInfo1.GetRegisterByPatientNO += QueryRegister;
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
        }

        void QueryRegister(string patientNO)
        {
            if (string.IsNullOrEmpty(patientNO))
            {
                return;
            }           
            DataTable dtPatient = registerMgr.QuerySIPatient(patientNO);
            if (dtPatient == null || dtPatient.Rows.Count == 0)
            {
                MessageBox.Show("没有找到相关住院结算信息，请检查该住院号是否有效");
                return;
            }
            else if (dtPatient.Rows.Count == 1)
            {
                string inPatientNO = dtPatient.Rows[0][0].ToString();
                //string invoiceNO = dtPatient.Rows[0][1].ToString();
                //string pactCode = dtPatient.Rows[0][2].ToString();
                string pactCode = dtPatient.Rows[0][1].ToString();
                SetShowValue(inPatientNO, pactCode);
            }
            else
            {
                IBorn.SI.MedicalInsurance.BaseControls.ucBaseChoose ucChoose = new BaseControls.ucBaseChoose(dtPatient);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucChoose);
                if (ucChoose.IsOK)
                {
                    string inPatientNO = ucChoose.SelectedID;
                    //string invoiceNO = ucChoose.SelectedNO;
                    //string pactCode = ucChoose.SelectedRemarkNO;
                    string pactCode = ucChoose.SelectedNO;
                    SetShowValue(inPatientNO, pactCode);
                }
            }
        }

        void QueryNeedUpLoadFeePatients()
        {
            //{734FB96F-44D7-463e-B3A3-04F27F463180}
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            /*
            string sql = @"select r.inpatient_no,i.invoice_no,i.pact_code,r.patient_no,r.name,fun_get_dept_name(r.dept_code) dept,r.out_date,
decode(i.Balance_Type,'I','在院结算','O','出院结算','D','直接结算','4','重结算', '5','结转','Q','欠费结算',i.Balance_Type) 结算类型
from fin_ipr_inmaininfo r join fin_ipb_balancehead i on r.inpatient_no=i.inpatient_no 
where not  exists (select 1 from GZSI_HIS_FYJS c where c.inpatient_no=r.inpatient_no and c.invoice_no=i.invoice_no and c.valid_flag='1') 
and i.pact_code in(select d.code from com_dictionary d where d.type='SI_PACT' and d.valid_state='1') and i.WASTE_FLAG='1' order by r.out_date";
             */
            string sql = @"select distinct r.inpatient_no,i.pact_code,r.patient_no,r.name,fun_get_dept_name(r.dept_code) dept,r.out_date
from fin_ipr_inmaininfo r join fin_ipb_balancehead i on r.inpatient_no=i.inpatient_no 
where not  exists (select 1 from GZSI_HIS_FYJS c where c.inpatient_no=r.inpatient_no --and instr(c.invoice_no,i.invoice_no)>0 
and c.valid_flag='1') 
and i.pact_code in(select d.code from com_dictionary d where d.type='SI_PACT' and d.valid_state='1') and i.WASTE_FLAG='1'  and r.dept_code !='1010' order by r.out_date";
            //{734FB96F-44D7-463e-B3A3-04F27F463180}

            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread2.DataSource = dsRes;
        }

        string GetCurrentInvoices()
        {
            var query = from c in currentDtUploadFee.AsEnumerable()
                        group c by new { invoice = c.Field<string>("invoice_no") }
                            into s
                            orderby s.Key.invoice
                            select s.Key.invoice;
            var invoices = query.ToList<string>();
            StringBuilder sbRes = new StringBuilder();
            for (int i = 0; i < invoices.Count; i++)
            {
                if (i == invoices.Count - 1)
                {
                    sbRes.Append(invoices[i]);
                }
                else
                {
                    sbRes.AppendFormat("{0},", invoices[i]);
                }
            }
            return sbRes.ToString();
        }

        //双击选择待患者
        private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string inpatientNO = this.fpSpread2_Sheet1.Cells[e.Row, 0].Text;
            //string invoiceNo = this.fpSpread2_Sheet1.Cells[e.Row, 1].Text;
            //string pactCode = this.fpSpread2_Sheet1.Cells[e.Row, 2].Text;
            string pactCode = this.fpSpread2_Sheet1.Cells[e.Row, 1].Text;
            //this.fpSpread1_Sheet1.DataSource = currentDtUploadFee;
            SetShowValue(inpatientNO, pactCode);
        }

        void SetShowValue(string inpatientNO, string pactCode)
        {
            currentRegister = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
            //currentRegister.SIMainInfo.InvoiceNo = invoiceNo;
            currentRegister.Pact.ID = pactCode;
            ucInPatientInfo1.SetPatientInfo(currentRegister);
            if (currentDtUploadFee != null)
            {
                currentDtUploadFee.Clear();
            }
            currentDtUploadFee = medicalInterfaceProxy.QueryInPatientUploadFee(currentRegister);
            if (currentDtUploadFee == null)
            {
                MessageBox.Show(medicalInterfaceProxy.ErrorMsg);
                return;
            }
            //{424C6741-4424-4002-89A7-091F310DA986}
            this.fpSpread1_Sheet1.DataSource = currentDtUploadFee.DefaultView;
            this.fpSpread3_Sheet1.DataSource = medicalInterfaceProxy.GetInPatientNotUploadFeeDetail(currentRegister);
            label4.Text = "总金额：" + GetTotalPrice(currentDtUploadFee);
        }
        //{424C6741-4424-4002-89A7-091F310DA986}
        private decimal GetTotalPrice(DataTable dt)
        {
            decimal total = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal tmpprice = Convert.ToDecimal(dt.Rows[i]["JE"]);
                total = total + tmpprice;
            }
            return total;
        }
        private void ucUpLoadFeeDetail_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
                this.fpSpread2_Sheet1.DataAutoSizeColumns = false;
                this.fpSpread3_Sheet1.DataAutoSizeColumns = false;
                QueryNeedUpLoadFeePatients();
            }
        }

        /// <summary>
        /// 基础控件Init事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("医保结算", "广州医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
            return this.toolBarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            QueryNeedUpLoadFeePatients();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "医保结算":
                    if (currentRegister == null)
                    {
                        MessageBox.Show("请在左边列表中双击选择患者再进行操作。");
                        return;
                    }
                    if (this.currentDtUploadFee == null || this.currentDtUploadFee.Rows.Count == 0)
                    {
                        MessageBox.Show("没有需要上传的费用明细。");
                        return;
                    }
                    currentRegister.SIMainInfo.InvoiceNo = GetCurrentInvoices();
                    if (medicalInterfaceProxy.BalanceInPatient(currentRegister) < 0)
                    {
                        MessageBox.Show(medicalInterfaceProxy.ErrorMsg);
                        return;
                    }
                    QueryNeedUpLoadFeePatients();
                    ucInPatientInfo1.Clear();
                    if (currentDtUploadFee != null)
                    {
                        this.currentDtUploadFee.Clear();
                    }
                    this.fpSpread1_Sheet1.RowCount = 0;
                    this.fpSpread3_Sheet1.DataSource = null;
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        //上传费用明细列表双击事件对应方法
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.currentRegister == null)
            {
                return;
            }
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int rowIndex = e.Row;
            string invoiceNO = fpSpread1_Sheet1.GetText(rowIndex, 1);
            string itemCustomCode = fpSpread1_Sheet1.GetText(rowIndex, 4);
            int exeRes = 0;
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            string sql1 = string.Format(@"update fin_ipb_medicinelist t set t.upload_flag='2' where t.inpatient_no='{0}' and t.invoice_no='{1}' and t.drug_code in(select b.drug_code from pha_com_baseinfo b where b.custom_code ='{2}')", currentRegister.ID, invoiceNO, itemCustomCode);
            exeRes = registerMgr.ExecNoQuery(sql1);
            if (exeRes <= 0)
            {
                string sql2 = string.Format(@"update fin_ipb_itemlist t set t.upload_flag='2' where t.inpatient_no='{0}' and t.invoice_no='{1}' and t.item_code in(select u.item_code from fin_com_undruginfo u where u.input_code ='{2}')", currentRegister.ID, invoiceNO, itemCustomCode);
                exeRes = registerMgr.ExecNoQuery(sql2);
            }
            if (exeRes > 0)
            {
                SetShowValue(currentRegister.ID, currentRegister.Pact.ID);
                if (!string.IsNullOrEmpty(this.txtUploadFeeDetailFilter.Text.Trim()))
                {
                    this.txtUploadFeeDetailFilter_TextChanged(null, null);
                }
            }
        }

        //不上传项目列表双击事件对应方法
        private void fpSpread3_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.currentRegister == null)
            {
                return;
            }
            if (this.fpSpread3_Sheet1.RowCount == 0)
            {
                return;
            }
            string invoiceNO = fpSpread3_Sheet1.GetText(e.Row, 1);
            string itemCustomCode = fpSpread3_Sheet1.GetText(e.Row, 4);
            int exeRes = 0;
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            string sql1 = string.Format(@"update fin_ipb_medicinelist t set t.upload_flag='0' where t.inpatient_no='{0}' and t.invoice_no='{1}' and t.drug_code in(select b.drug_code from pha_com_baseinfo b where b.custom_code ='{2}')", currentRegister.ID, invoiceNO, itemCustomCode);
            exeRes = registerMgr.ExecNoQuery(sql1);
            if (exeRes <= 0)
            {
                string sql2 = string.Format(@"update fin_ipb_itemlist t set t.upload_flag='0' where t.inpatient_no='{0}' and t.invoice_no='{1}' and t.item_code in(select u.item_code from fin_com_undruginfo u where u.input_code ='{2}')", currentRegister.ID, invoiceNO, itemCustomCode);
                exeRes = registerMgr.ExecNoQuery(sql2);
            }
            if (exeRes > 0)
            {
                SetShowValue(currentRegister.ID, currentRegister.Pact.ID);
                if (!string.IsNullOrEmpty(this.txtUploadFeeDetailFilter.Text.Trim()))
                {
                    this.txtUploadFeeDetailFilter_TextChanged(null, null);
                }
            }
        }

        private void txtUploadFeeDetailFilter_TextChanged(object sender, EventArgs e)
        {
            if (currentDtUploadFee == null || currentDtUploadFee.DefaultView==null)
            {
                return;
            }
            this.currentDtUploadFee.DefaultView.RowFilter = string.Format("XMBH like '%{0}%' or XMMC like '%{0}%'", this.txtUploadFeeDetailFilter.Text.Trim());
        }

    }
}
