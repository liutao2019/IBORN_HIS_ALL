using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace IBorn.SI.MedicalInsurance.OutPatient
{
    public partial class ucBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
        IBorn.SI.MedicalInsurance.Proxy.MedicalFactoryProxy medicalInterfaceProxy = new Proxy.MedicalFactoryProxy();
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.Models.Registration.Register currentRegister;

        DataTable currentDtUploadFee;

        public ucBalance()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
            this.ucOutPatientInfo1.GetRegisterByCardNO += QueryRegister;
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
        }

        void QueryRegister(string patientNO)
        {
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            string sql = string.Format(@"select r.clinic_code 门诊流水号,i.invoice_no 结算发票号,i.pact_code,r.name 姓名,fun_get_dept_name(r.see_dpcd) 看诊科室,r.see_date 看诊时间,fun_get_employee_name(r.see_docd) 看诊医生 
from fin_opr_register r join fin_opb_invoiceinfo i on r.clinic_code=i.clinic_code 
where not  exists (select 1 from GZSI_HIS_MZJS c where c.clinic_code=r.clinic_code and c.invoice_no=i.invoice_no and c.valid_flag='1') and i.pact_code in(select d.code from com_dictionary d where d.type='SI_PACT' and d.valid_state='1')
and r.valid_flag='1' and i.cancel_flag='1' and r.card_no='{0}' and r.reg_date>trunc(sysdate)-90 order by r.see_date", patientNO);
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            if (dsRes == null || dsRes.Tables[0] == null || dsRes.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有找到相关门诊结算信息，请检查该卡号是否有效或该卡号是否结算过");
                return;
            }
            IBorn.SI.MedicalInsurance.BaseControls.ucBaseChoose ucChoose = new BaseControls.ucBaseChoose(dsRes.Tables[0]);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            FS.FrameWork.WinForms.Classes.Function.ShowControl(ucChoose);
            if (ucChoose.IsOK)
            {
                string clinicNO = ucChoose.SelectedID;
                string invoiceNO = ucChoose.SelectedNO;
                string pactCode = ucChoose.SelectedRemarkNO;
                SetShowValue(clinicNO, invoiceNO, pactCode);
            }
        }

        void QueryNeedUpLoadFeePatients()
        {
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            string sql = @"select r.clinic_code,i.invoice_no,i.pact_code,r.name,fun_get_dept_name(r.see_dpcd) dept,r.see_date,fun_get_employee_name(r.see_docd) doctor from fin_opr_register r join fin_opb_invoiceinfo i on r.clinic_code=i.clinic_code 
where not  exists (select 1 from GZSI_HIS_MZJS c where c.clinic_code=r.clinic_code and c.invoice_no=i.invoice_no and c.valid_flag='1') and r.valid_flag='1' and i.pact_code in(select d.code from com_dictionary d where d.type='SI_PACT' and d.valid_state='1') and i.cancel_flag='1' order by r.see_date";
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread2.DataSource = dsRes;
        }



        //双击选择待患者
        private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string clinicNO = this.fpSpread2_Sheet1.Cells[e.Row, 0].Text;
            string invoiceNo = this.fpSpread2_Sheet1.Cells[e.Row, 1].Text;
            string pactCode = this.fpSpread2_Sheet1.Cells[e.Row, 2].Text;
            SetShowValue(clinicNO, invoiceNo, pactCode);
        }


        void SetShowValue(string clinicNO, string invoiceNO, string pactCode)
        {
            currentRegister = registerMgr.GetByClinic(clinicNO);
            currentRegister.SIMainInfo.InvoiceNo = invoiceNO;
            currentRegister.Pact.ID = pactCode;
            ucOutPatientInfo1.SetPatientInfo(currentRegister);
            if (currentDtUploadFee != null)
            {
                currentDtUploadFee.Clear();
            }
            currentDtUploadFee = medicalInterfaceProxy.QueryOutPatientUploadFee(currentRegister);
            if (currentDtUploadFee == null)
            {
                MessageBox.Show(medicalInterfaceProxy.ErrorMsg);
                return;
            }
            this.fpSpread1_Sheet1.DataSource = currentDtUploadFee;
        }

        private void ucUpLoadFeeDetail_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
                this.fpSpread2_Sheet1.DataAutoSizeColumns = false;
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
                        MessageBox.Show("请在选择确定患者再进行操作。");
                        return;
                    }
                    if (medicalInterfaceProxy.BalanceOutPatient(currentRegister) < 0)
                    {
                        MessageBox.Show(medicalInterfaceProxy.ErrorMsg);
                        return;
                    }
                    QueryNeedUpLoadFeePatients();
                    ucOutPatientInfo1.Clear();
                    if (currentDtUploadFee != null)
                    {
                        this.currentDtUploadFee.Clear();
                    }
                    this.fpSpread1_Sheet1.RowCount = 0;
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

    }
}
