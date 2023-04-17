using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.MedicalInsurance.OutPatient
{
    //todo:需构建对应的发票明细实体，构建对应的发票信息实体，可以考虑视图
    //todo:需能查询到该发票明细实体对应的结算项目明细
    public partial class ucOutPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        FS.HISFC.Models.Registration.Register currentRegister;

        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.HISFC.BizLogic.Registration.Register registerOutMgr = new FS.HISFC.BizLogic.Registration.Register();

        private IBorn.SI.GuangZhou.SILocalManager siLocalManager = new IBorn.SI.GuangZhou.SILocalManager();
        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        public ucOutPrint()
        {
            InitializeComponent();
            //fpSpread1_Sheet1.RowCount = 0;
            this.ucOutPatientInfo1.GetRegisterByCardNO += QueryRegister;
        }

        //void SetShowValue(string card_no, string invoiceNo, string pactCode)
        //{
        //    //设置患者信息 (todo:可以考虑去掉这个控件，不显示也无关紧要)
        //    //var register = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
        //    //register.SIMainInfo.InvoiceNo = invoiceNo;
        //    //register.Pact.ID = pactCode;

        //    FS.HISFC.Models.Registration.Register currentRegister = registerOutMgr.Query(card_no);

        //    currentRegister.SIMainInfo.InvoiceNo = invoiceNo;
        //    currentRegister.Pact.ID = pactCode;

        //    ucOutPatientInfo1.SetPatientInfo(currentRegister);
        //    //ucOutPatientInfo1.SetPatientInfo(register);
        //    //设置发票信息
        //    //this.ucInvoicePrint1.SetPrintValue();

        //    //设置清单信息
        //    //this.ucInPatientFeeDetailBill1.ShowBill();
        //}
        void SetShowValue(string clinicNO, string invoiceNO, string pactCode)
        {
            currentRegister = registerOutMgr.GetByClinic(clinicNO);
            currentRegister.SIMainInfo.InvoiceNo = invoiceNO;
            currentRegister.Pact.ID = pactCode;
            ucOutPatientInfo1.SetPatientInfo(currentRegister);

        }

        private void txtRegNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtRegNO.Text))
            {
                SetShowBillByJYDJH(txtRegNO.Text);
            }
        }

        private void SetShowBillByJYDJH(string JYDJH)
        {
            String card_no = siLocalManager.GetCardNoByJYDJH(JYDJH);
            if (card_no == null)
            {
                MessageBox.Show("此诊疗暂无结算，请选择已结算项目");
                return;
            }

            //根据结算发票号获得患者信息
            //var currentRegister = registerMgr.QueryComPatientInfo(card_no);
            FS.HISFC.Models.Registration.Register currentRegister = registerOutMgr.Query(card_no);

            ucOutPatientInfo1.SetPatientInfo(currentRegister);

            //ucInPatientFeeDetailBill1.ShowBill(new System.Collections.ArrayList(), currentRegister);
            ucOutPatientFeeDetailBill1.ShowBill(GetFeeInfoByJYDJH(JYDJH), currentRegister);
        }

        void QueryRegister(string patientNO)
        {
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            string sql = string.Format(@" 
 select r.clinic_code 门诊流水号,i.invoice_no 结算发票号,i.pact_code,r.name 姓名,fun_get_dept_name(r.see_dpcd) 看诊科室,r.see_date 看诊时间,fun_get_employee_name(r.see_docd) 看诊医生 
from fin_opr_register r join fin_opb_invoiceinfo i on r.clinic_code=i.clinic_code   right join GZSI_HIS_MZJS ghm on ghm.clinic_code=r.clinic_code
where  i.pact_code in(select d.code from com_dictionary d where d.type='SI_PACT' and d.valid_state='1')
and r.valid_flag='1' and i.cancel_flag='1' and 
r.card_no='{0}' and 
r.reg_date>trunc(sysdate)-90 order by r.see_date", patientNO);
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
                //string regNo = getJYDJHByRegister(clinicNO, invoiceNO);
                SetShowBillByJYDJH(getJYDJHByClinicNo(clinicNO));
            }
        }

        public string getJYDJHByRegister(string clinicNo,string invoiceNo)
        {
            //todo:需优化，代码需整合挪移
            string sql = string.Format(@"select i.reg_no,j.invoice_no from fin_ipr_siinmaininfo i left join GZSI_HIS_MZJS j 
on i.inpatient_no=j.clinic_code and i.reg_no=j.jydjh and i.invoice_no=j.invoice_no  and j.valid_flag='1' and j.invoice_no='{1}'
where i.valid_flag='1' and i.inpatient_no='{0}'  and i.reg_no is not null", clinicNo, invoiceNo);
            DataSet dsRes = new DataSet();
            pactManager.ExecQuery(sql, ref dsRes);
            string regNO = string.Empty;
            if (dsRes != null)
            {
                var dtRow = dsRes.Tables[0];
                if (dtRow.Rows.Count > 0)
                {
                    regNO = dtRow.Rows[0]["reg_no"].ToString();
                }
            }
            else
            {
                return null;
            }
            return regNO;
        }

        public string getJYDJHByClinicNo(string clinicNo)
        {
            //todo:需优化，代码需整合挪移
            string sql = string.Format(@" select t.Jydjh from GZSI_HIS_MZJS t WHERE t.Clinic_Code='{0}'", clinicNo);
            DataSet dsRes = new DataSet();
            pactManager.ExecQuery(sql, ref dsRes);
            string JYDJH = string.Empty;
            if (dsRes != null)
            {
                var dtRow = dsRes.Tables[0];
                if (dtRow.Rows.Count > 0)
                {
                    JYDJH = dtRow.Rows[0]["JYDJH"].ToString();
                }
            }
            else
            {
                return null;
            }
            return JYDJH;
        }

        /// <summary>
        /// 根据发票号查询信息
        /// </summary>
        private void QueryByInoviceNO(string invoiceNO)
        {
            ////获取输入发票实体信息
            //ArrayList al = this.inpatientFeeManager.QueryBalancesByInvoiceNO(invoiceNO);
            //if (al == null || al.Count != 1)
            //{
            //    CommonController.CreateInstance().MessageBox("查询指定发票信息失败！", MessageBoxIcon.Warning);
            //    return;
            //}

            //FS.HISFC.Models.Fee.Inpatient.Balance balance = al[0] as FS.HISFC.Models.Fee.Inpatient.Balance;
            //this.QueryByInPatientNO(balance.Patient.ID, invoiceNO);
        }

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.Fee.Inpatient.Balance> GetInvoiceInfo()
        {
            //if (this.patientInfo == null)
            //{
            //    return null;
            //}

            //if (this.fpBalanceInvoice_Sheet1.RowCount == 0)
            //{
            //    return null;
            //}

            //List<FS.HISFC.Models.Fee.Inpatient.Balance> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            ////取选择的发票信息
            //for (int i = 0; i < this.fpBalanceInvoice_Sheet1.RowCount; i++)
            //{
            //    if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpBalanceInvoice_Sheet1.Cells[i, 0].Value))
            //    {
            //        balanceList.Add(this.fpBalanceInvoice_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Inpatient.Balance);
            //    }
            //}

            //return balanceList;
            return null;
        }

        /// <summary>
        /// 通过住院就医登记号构造患者费用列表
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetFeeInfoByJYDJH(string jydjh)
        {
            System.Collections.ArrayList feeList = new System.Collections.ArrayList();

            System.Data.DataTable dt = siLocalManager.GetOutpatientSiBalanceListAsFee(jydjh);

            foreach (DataRow dr in dt.Rows)
            {
                //设置对应的医保信息引用到feeinfo中
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeinfo.Item.Name = dr["XMMC"].ToString();
                feeinfo.Item.Specs = dr["YPGG"].ToString();
                feeinfo.Item.Price = Convert.ToDecimal(dr["JG"].ToString());
                feeinfo.Item.Qty = Convert.ToDecimal(dr["MCYL"].ToString());
                feeinfo.FT.TotCost = Convert.ToDecimal(dr["JE"].ToString());
                //feeinfo.FT.PriceUnit = dr["JG"].ToString();药品单位
                feeList.Add(feeinfo);

                ucOutPatientFeeDetailBill1.SetBillYBFY(Convert.ToDecimal(dr["ZYZJE"].ToString()), Convert.ToDecimal(dr["SBZFJE"].ToString()), Convert.ToDecimal(dr["GRZFJE1"].ToString()));
            }
            return feeList;
        }

        /// <summary>
        /// 通过门诊就医登记号构造患者费用列表
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetOutFeeInfoByJYDJH(string jydjh)
        {
            System.Collections.ArrayList feeList = new System.Collections.ArrayList();

            System.Data.DataTable dt = siLocalManager.GetOutpatientSiBalanceListAsFee(jydjh);

            foreach (DataRow dr in dt.Rows)
            {
                //设置对应的医保信息引用到feeinfo中
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeinfo.Item.Name = dr["XMMC"].ToString();
                feeinfo.Item.Specs = dr["YPGG"].ToString();
                feeinfo.Item.Price = Convert.ToDecimal(dr["JG"].ToString());
                feeinfo.Item.Qty = Convert.ToDecimal(dr["MCYL"].ToString());
                feeinfo.FT.TotCost = Convert.ToDecimal(dr["JE"].ToString());
                //feeinfo.FT.PriceUnit = dr["JG"].ToString();药品单位
                feeList.Add(feeinfo);

                ucOutPatientFeeDetailBill1.SetBillYBFY(Convert.ToDecimal(dr["ZYZJE"].ToString()), Convert.ToDecimal(dr["SBZFJE"].ToString()), Convert.ToDecimal(dr["GRZFJE1"].ToString()));
            }
            return feeList;
        }

        //{8AA24CF1-D42B-4978-9D60-7083330080E5}
        protected override int OnPrint(object sender, object neuObject)
        {
            ucOutPatientFeeDetailBill1.PrintPreview();

            return 1;
        }

        //{7784630A-37D7-4eb9-95C5-8D2CD37AC2FB}
        private void searchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(searchTxt.Text))
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.searchTxt.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(@"^\d+$");
                if (rx.IsMatch(QueryStr))
                {
                    frmQuery.QueryByPhone(QueryStr);
                }
                else
                {
                    frmQuery.QueryByName(QueryStr);
                }
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    string card = frmQuery.PatientInfo.PID.CardNO;
                    this.ucOutPatientInfo1.setCardNo(card);
                    //txtMedicalCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                    QueryRegister(card);
                }
            }
        }

    }
}
