using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.MedicalInsurance.InPatient
{
    //todo:需构建对应的发票明细实体，构建对应的发票信息实体，可以考虑视图
    //todo:需能查询到该发票明细实体对应的结算项目明细
    public partial class ucPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        private IBorn.SI.GuangZhou.SILocalManager siLocalManager = new IBorn.SI.GuangZhou.SILocalManager();

        public ucPrint()
        {
            InitializeComponent();
            this.ucInPatientInfo1.GetRegisterByPatientNO += QueryRegister;

            //{7784630A-37D7-4eb9-95C5-8D2CD37AC2FB}
            this.ucQueryInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInfo_myEvent);

        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        private void ucQueryInfo_myEvent()
        {
            string errText = "";
            //ucQueryInfo.settbPatientNO(this.ucQueryInfo.InpatientNo);
            //{7784630A-37D7-4eb9-95C5-8D2CD37AC2FB}
            QueryRegister(this.ucQueryInfo.Text);
            string liushuihao = this.ucQueryInfo.InpatientNo;
            string inpatientno = this.ucQueryInfo.Text;
            //设置bill信息
            //根据结算发票号获得患者信息
            var currentRegister = registerMgr.QueryPatientInfoByInpatientNONew(liushuihao);

            ucInPatientInfo1.SetPatientInfo(currentRegister);
            string JYDJH = this.txtRegNO.Text;//QueryJYDJHByLiushuihao(liushuihao);

            //ucInPatientFeeDetailBill1.ShowBill(new System.Collections.ArrayList(), currentRegister);
            ucInPatientFeeDetailBill1.ShowBill(GetFeeInfoByJYDJH(JYDJH), currentRegister);
        }

        private string QueryJYDJHByLiushuihao(string liushuihao)
        {
            //todo:需优化，代码需整合挪移
            string sql = string.Format(@"select t.jydjh from gzsi_his_fyjs t where INPATIENT_NO = '{0}'", liushuihao);
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

        void SetShowValue(string inpatientNO, string invoiceNo, string pactCode)
        {
            //设置患者信息 (todo:可以考虑去掉这个控件，不显示也无关紧要)
            var register = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
            register.SIMainInfo.InvoiceNo = invoiceNo;
            register.Pact.ID = pactCode;
            ucInPatientInfo1.SetPatientInfo(register);

            //{6A7E6EED-9AC4-4198-BD90-DEE5057BF5F6}
            //获取就医登记号
            string regNO = string.Empty;
            string invoiceNO = string.Empty;
            if (this.pactManager.GetSiRegisterNO(register.ID, invoiceNo, ref regNO, ref invoiceNO) > 0)
            {

                if (string.IsNullOrEmpty(regNO))
                {
                    //lbSIInfo.Text = "未关联医保登记";
                }
                else
                {
                    //lbSIInfo.Text = string.Format("医保登记号:{0}  医保已结算", regNO);
                }
            }
            txtRegNO.Text = regNO;
        }

        private void txtRegNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtRegNO.Text))
            {
                String inpatientNo = siLocalManager.GetInpatientNoByJYDJH(txtRegNO.Text);

                if (inpatientNo == null)
                {
                    MessageBox.Show("没有找到相关住院结算信息，请检查该就医登记号是否有效");
                    return;
                }

                //根据结算发票号获得患者信息
                var currentRegister = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNo);

                ucInPatientInfo1.SetPatientInfo(currentRegister);

                //ucInPatientFeeDetailBill1.ShowBill(new System.Collections.ArrayList(), currentRegister);
                ucInPatientFeeDetailBill1.ShowBill(GetFeeInfoByJYDJH(txtRegNO.Text), currentRegister);
            }
        }

        void QueryRegister(string patientNO)
        {
            if (string.IsNullOrEmpty(patientNO))
            {
                return;
            }            
            DataTable dtPatient = registerMgr.QuerySIPatientAll(patientNO);
            if (dtPatient == null || dtPatient.Rows.Count == 0)
            {
                MessageBox.Show("没有找到相关住院结算信息，请检查该住院号是否有效");
                return;
            }
            else if (dtPatient.Rows.Count == 1)
            {
                string inPatientNO = dtPatient.Rows[0][0].ToString();
                string invoiceNO = dtPatient.Rows[0][1].ToString();
                string pactCode = dtPatient.Rows[0][2].ToString();
                SetShowValue(inPatientNO, invoiceNO, pactCode);
            }
            else
            {
                IBorn.SI.MedicalInsurance.BaseControls.ucBaseChoose ucChoose = new BaseControls.ucBaseChoose(dtPatient);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucChoose);
                if (ucChoose.IsOK)
                {
                    string inPatientNO = ucChoose.SelectedID;
                    string invoiceNO = ucChoose.SelectedNO;
                    string pactCode = ucChoose.SelectedRemarkNO;
                    SetShowValue(inPatientNO, invoiceNO, pactCode);
                }
            }
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
        /// 通过就医登记号构造患者费用列表
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetFeeInfoByJYDJH(string jydjh)
        {
            System.Collections.ArrayList feeList = new System.Collections.ArrayList();

            System.Data.DataTable dt = siLocalManager.GetInpatientSiBalanceListAsFee(jydjh);

            foreach (DataRow dr in dt.Rows)
            {
                //设置对应的医保信息引用到feeinfo中
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeinfo.Item.Name = dr["XMMC"].ToString();
                feeinfo.Item.Specs = dr["YPGG"].ToString();
                feeinfo.Item.Price = Convert.ToDecimal(dr["JG"].ToString());
                feeinfo.Item.Qty = Convert.ToDecimal(dr["MCYL"].ToString());
                feeinfo.FT.TotCost = Convert.ToDecimal(dr["JE"].ToString());
                feeinfo.User01 = dr["chrgitm_type"].ToString();
                //feeinfo.FT.PriceUnit = dr["JG"].ToString();药品单位
                feeList.Add(feeinfo);

                ucInPatientFeeDetailBill1.SetBillYBFY(Convert.ToDecimal(dr["ZYZJE"].ToString()), Convert.ToDecimal(dr["SBZFJE"].ToString()), Convert.ToDecimal(dr["GRZFJE1"].ToString()));
            }
            return feeList;
        }

        //protected override void Print(PaintEventArgs e)
        //{
            //PrintView();
            //base.OnPrint(e);
            //ucInPatientFeeDetailBill1.PrintPreview();
        //}

        //{8AA24CF1-D42B-4978-9D60-7083330080E5}
        protected override int OnPrint(object sender, object neuObject)
        {
            ucInPatientFeeDetailBill1.PrintPreview();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            return ucInPatientFeeDetailBill1.Export();
        }

    }
}
