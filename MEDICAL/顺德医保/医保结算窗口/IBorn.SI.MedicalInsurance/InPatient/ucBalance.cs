using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace IBorn.SI.MedicalInsurance.FoShan.InPatient
{
    public partial class ucBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 医保待遇算法
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.Models.RADT.PatientInfo currentRegister;
        /// <summary>
        /// 控制参数业务层 --com_controlargument
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 业务层变量
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        DataTable currentDtUploadFee;

        /// <summary>
        /// 省外异地患者
        /// </summary>
        private string swydPact = "";
        /// <summary>
        /// 省内异地患者
        /// </summary>
        private string snydPact = "";

        /// <summary>
        /// 结算费用明细
        /// </summary>
        protected ArrayList alFeeItemLists = new ArrayList();

        public ucBalance()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
            this.ucInPatientInfo1.GetRegisterByPatientNO += QueryRegister;
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            swydPact = this.controlParam.GetControlParam<string>("SWYD01");// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            snydPact = this.controlParam.GetControlParam<string>("SNYD01");// {80867F3F-AE91-4525-82AF-0618AB01C92B}
        }

        void QueryRegister(string patientNO)
        {
            if (string.IsNullOrEmpty(patientNO))
            {
                return;
            }
            DataTable dtPatient = registerMgr.QueryFoShanSIPatient(patientNO);
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
                string invoiceNo = "";//dtPatient.Rows[0][6].ToString();
                SetShowValue(inPatientNO, pactCode, invoiceNo);
            }
            else
            {
                IBorn.SI.MedicalInsurance.FoShan.BaseControls.ucBaseChoose ucChoose = new BaseControls.ucBaseChoose(dtPatient);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucChoose);
                if (ucChoose.IsOK)
                {
                    string inPatientNO = ucChoose.SelectedID;
                    //string invoiceNO = ucChoose.SelectedNO;
                    //string pactCode = ucChoose.SelectedRemarkNO;
                    string pactCode = ucChoose.SelectedNO;
                    string invoiceNo = ucChoose.SelectedInvoiceNo;
                    SetShowValue(inPatientNO, pactCode, invoiceNo);
                }
            }
        }

        void QueryNeedUpLoadFeePatients()
        {
            string sql = "";
            if (registerMgr.Sql.GetCommonSql("RADT.Inpatient.Patient.Select.ForFoShanSI", ref sql) == 0)
            {
                sql = string.Format(sql, "ALL");
                DataSet dsRes = new DataSet();
                registerMgr.ExecQuery(sql, ref dsRes);
                this.fpSpread2.DataSource = dsRes;
            }
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
            string invoiceNo = "";//this.fpSpread2_Sheet1.Cells[e.Row, 6].Text;
            //this.fpSpread1_Sheet1.DataSource = currentDtUploadFee;
            SetShowValue(inpatientNO, pactCode, invoiceNo);
        }

        void SetShowValue(string inpatientNO, string pactCode,string invoiceNo)
        {
            currentRegister = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
            //currentRegister.SIMainInfo.InvoiceNo = invoiceNo;
            if (currentRegister == null)
            {
                MessageBox.Show("查询患者基本信息失败！" + registerMgr.Err);
                return;
            }
            currentRegister.Pact.ID = pactCode;

           long returnValue = medcareInterfaceProxy.SetPactCode(currentRegister.Pact.ID);

            if (returnValue != 1)
            {
                MessageBox.Show("接口未实现！"+this.medcareInterfaceProxy.ErrMsg);
            }

            ucInPatientInfo1.SetPatientInfo(currentRegister);
            if (currentDtUploadFee != null)
            {
                currentDtUploadFee.Clear();
            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            DataTable currentDtUploadFeeTemp = registerMgr.QueryInPatientNeedUploadFeeDetail(inpatientNO);
            if (currentDtUploadFeeTemp != null)
            {
                //DataRow[] rows = currentDtUploadFeeTemp.Select("INVOICE_NO = '" + invoiceNo + "'");
                //if (rows != null && rows.Length > 0)
                //{
                //    currentDtUploadFee = rows[0].Table.Clone();
                //    foreach (DataRow row in rows)
                //    {
                //        currentDtUploadFee.Rows.Add(row.ItemArray);
                //    }
                //}

                currentDtUploadFee = currentDtUploadFeeTemp;
            }

            if (currentDtUploadFee != null)
            {
                this.fpSpread1_Sheet1.DataSource = currentDtUploadFee.DefaultView;
            }
            else
            {
                MessageBox.Show("没有找到需要结算的费用信息！");
            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            DataTable currentDtNotUploadFee = registerMgr.QueryInPatientNotUploadFeeDetail(inpatientNO);
            //if (currentDtUploadFeeTemp != null)
            //{
                //DataRow[] rowsNot = currentDtNotUploadFeeTemp.Select("INVOICE_NO = '" + invoiceNo + "'");
                //if (rowsNot != null && rowsNot.Length > 0)
                //{
                //    currentDtNotUploadFee = rowsNot[0].Table.Clone();
                //    foreach (DataRow row in rowsNot)
                //    {
                //        currentDtNotUploadFee.Rows.Add(row.ItemArray);
                //    }
                //}
            //}
            if (currentDtNotUploadFee != null)
            {
                this.fpSpread3_Sheet1.DataSource = currentDtNotUploadFee.DefaultView;
            }
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
            toolBarService.AddToolButton("医保结算", "佛山医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
            toolBarService.AddToolButton("异地医保结算", "异地医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
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
                    if (this.currentRegister.Pact.ID == this.swydPact || this.currentRegister.Pact.ID == this.snydPact)
                    {
                        MessageBox.Show("该患者为异地患者，请选择异地医保结算。");
                        return;
                        
                    }
                    if (this.currentDtUploadFee == null || this.currentDtUploadFee.Rows.Count == 0)
                    {
                        MessageBox.Show("没有需要上传的费用明细。");
                        return;
                    }
                    currentRegister.SIMainInfo.InvoiceNo = GetCurrentInvoices();

                    if (string.IsNullOrEmpty(this.currentRegister.IDCard))
                    {
                        MessageBox.Show("患者的身份证号为空不允许结算，请填写正确的身份证号码后结算！");
                        return;

                    }
                    //待遇算法
                    long returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentRegister.Pact.ID);

                    if (returnValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("接口未实现！" + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    returnValue = this.medcareInterfaceProxy.Connect();

                    returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(this.currentRegister);
                    if (returnValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("获取医保患者登记信息失败！" + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    this.alFeeItemLists = new ArrayList();
                    // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
                    currentDtUploadFee = registerMgr.QueryInPatientNeedUploadFeeDetail(this.currentRegister.ID);

                    //DataTable currentDtUploadFeeTemp = registerMgr.QueryInPatientNeedUploadFeeDetail(this.currentRegister.ID);
                    //if (currentDtUploadFeeTemp == null)
                    //{
                    //    MessageBox.Show("查询患者费用信息失败！" + registerMgr.Err);
                    //    return;
                    //}
                    //DataRow[] rows = currentDtUploadFeeTemp.Select("INVOICE_NO = '" + currentRegister.SIMainInfo.InvoiceNo + "'");
                    //if (rows != null && rows.Length > 0)
                    //{
                    //    currentDtUploadFee = rows[0].Table.Clone();
                    //    foreach (DataRow row in rows)
                    //    {
                    //        currentDtUploadFee.Rows.Add(row.ItemArray);
                    //    }
                    //}


                    if (currentDtUploadFee == null)
                    {
                        MessageBox.Show("没有找到需要结算的费用信息！");
                        return;
                    }
                    // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
                    this.alFeeItemLists = this.ToFeeItem(currentDtUploadFee);

                    ////非药品明细
                    //ArrayList alItemList = new ArrayList();

                    ////药品明细
                    //ArrayList alMedicineList = new ArrayList();


                    ////先直接获取费用信息，后期进行优化
                    ////查询
                    //alItemList = this.feeInpatient.QueryItemListsForYIBAOBalance(this.currentRegister.ID);
                    //if (alItemList == null)
                    //{
                    //    MessageBox.Show("查询患者非药品信息出错" + this.feeInpatient.Err);
                    //    return;
                    //}

                    //alMedicineList = this.feeInpatient.QueryMedicineListsForYIBAOBalance(this.currentRegister.ID);
                    //if (alItemList != null)
                    //{
                    //    this.alFeeItemLists.AddRange(alItemList);
                    //}

                    //if (alMedicineList != null)
                    //{
                    //    this.alFeeItemLists.AddRange(alMedicineList);
                    //}

                    if (alFeeItemLists == null || alFeeItemLists.Count <= 0)
                    {
                        MessageBox.Show("没有需要上传的费用明细。");
                        return;
                    }

                    returnValue = this.medcareInterfaceProxy.PreBalanceInpatient(this.currentRegister, ref this.alFeeItemLists);

                    if (returnValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show("医保预结算失败！" + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }
                    

                    returnValue = this.medcareInterfaceProxy.BalanceInpatient(this.currentRegister, ref this.alFeeItemLists);

                    if (returnValue != 1)
                    {

                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show("医保结算失败！" + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    //-----------------待遇接口计算完毕.
                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.medcareInterfaceProxy.Commit();
                    this.medcareInterfaceProxy.Disconnect();
                    MessageBox.Show("医保结算成功！");
                    QueryNeedUpLoadFeePatients();
                    ucInPatientInfo1.Clear();
                    if (currentDtUploadFee != null)
                    {
                        this.currentDtUploadFee.Clear();
                    }
                    this.fpSpread1_Sheet1.RowCount = 0;
                    this.fpSpread3_Sheet1.DataSource = null;
                    break;

                case "异地医保结算": 
                    if (currentRegister == null)
                    {
                        MessageBox.Show("请在左边列表中双击选择患者再进行操作。");
                        return;
                    }
                    if (this.currentRegister.Pact.ID != this.swydPact && this.currentRegister.Pact.ID != this.snydPact)
                    {
                        MessageBox.Show("该患者为本地医保患者，请选择医保结算。");
                        return;

                    }
                    if (this.currentDtUploadFee == null || this.currentDtUploadFee.Rows.Count == 0)
                    {
                        MessageBox.Show("没有需要上传的费用明细。");
                        return;
                    }
                    currentRegister.SIMainInfo.InvoiceNo = GetCurrentInvoices();

                    if (string.IsNullOrEmpty(this.currentRegister.IDCard))
                    {
                        MessageBox.Show("患者的身份证号为空不允许结算，请填写正确的身份证号码后结算！");
                        return;

                    }
                    //待遇算法
                    returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentRegister.Pact.ID);

                    if (returnValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("接口未实现！" + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    returnValue = this.medcareInterfaceProxy.Connect();


                    returnValue = this.medcareInterfaceProxy.BalanceInpatient(this.currentRegister, ref this.alFeeItemLists);

                    if (returnValue != 1)
                    {

                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show("医保结算失败！" + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


        /// <summary>
        /// 需要上传医保的费用转换// {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ArrayList ToFeeItem(System.Data.DataTable dtFeeDetail)
        {
            ArrayList alFeeItem = new ArrayList();
            if (dtFeeDetail != null)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = null;
                foreach (System.Data.DataRow dr in dtFeeDetail.Rows)
                {
                    f = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    f.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(dr["MCYL"].ToString());//数量
                    f.Item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["JG"].ToString());//价格
                    f.Item.PriceUnit = dr["current_unit"].ToString();//计价单位
                    f.Item.UserCode = dr["XMBH"].ToString();//自定义码
                    f.Item.Name = dr["XMMC"].ToString();//项目名称
                    f.Item.Specs = dr["YPGG"].ToString();//规格
                    f.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["JE"].ToString());//金额
                    f.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(dr["FYRQ"].ToString());//费用日期
                    f.Item.MinFee.ID = dr["FEE_CODE"].ToString();//最小费用代码
                    f.Item.Specs = dr["YPGG"].ToString();//规格

                    alFeeItem.Add(f);


                }
            }
            else
            {
                return null;
            }

            return alFeeItem;
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
                SetShowValue(currentRegister.ID, currentRegister.Pact.ID, invoiceNO);
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
                SetShowValue(currentRegister.ID, currentRegister.Pact.ID, invoiceNO);
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
            this.fpSpread1_Sheet1.DataSource = currentDtUploadFee.DefaultView;//{405B92DC-4786-4c78-9476-8841F28FF5FE}
        }

    }
}
