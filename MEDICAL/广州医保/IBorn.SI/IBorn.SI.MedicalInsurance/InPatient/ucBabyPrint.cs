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
    //todo:需能查询到该发票明细实体对应的结算项目明细{5A04A8EF-06C3-45b9-9E6C-E5D152836257}
    public partial class ucBabyPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        private IBorn.SI.GuangZhou.SILocalManager siLocalManager = new IBorn.SI.GuangZhou.SILocalManager();

        private FS.HISFC.BizLogic.Fee.Item feeManager = new FS.HISFC.BizLogic.Fee.Item();
        public ucBabyPrint()
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
           
            //ucInPatientFeeDetailBill1.ShowBill(new System.Collections.ArrayList(), currentRegister);
            //{B00AF53E-2C65-4a21-8598-4DE4C851626C}
            DataTable dtemp = new DataTable();
            dtemp=GetFeeInfoByJYDJH();

            DataTable dtnoZero = dtemp.Clone();


            foreach (DataRow  row in dtemp.Rows)
            {
                try
                {
                    string ybprice = row[3].ToString();
                    if (ybprice != "0")
                    {
                        dtnoZero.ImportRow(row);
                    }      
                }
                catch (Exception)
                {
                    
                }
                
            }
            if (checkBox1.Checked == true)
            {
                ucInPatientFeeDetailBill1.ShowBill(dtnoZero, currentRegister);
            }
            else
            {
                ucInPatientFeeDetailBill1.ShowBill(dtemp, currentRegister);
            }
        }

       

        void SetShowValue(string inpatientNO, string invoiceNo, string pactCode)
        {
            //设置患者信息 (todo:可以考虑去掉这个控件，不显示也无关紧要)
            var register = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
            register.SIMainInfo.InvoiceNo = invoiceNo;
            register.Pact.ID = pactCode;
            ucInPatientInfo1.SetPatientInfo(register); 
        }

        //private void txtRegNO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtRegNO.Text))
        //    {
        //        String inpatientNo = siLocalManager.GetInpatientNoByJYDJH(txtRegNO.Text);

        //        if (inpatientNo == null)
        //        {
        //            MessageBox.Show("没有找到相关住院结算信息，请检查该就医登记号是否有效");
        //            return;
        //        }

        //        //根据结算发票号获得患者信息
        //        var currentRegister = registerMgr.QueryPatientInfoByInpatientNONew(inpatientNo);

        //        ucInPatientInfo1.SetPatientInfo(currentRegister);

        //        //ucInPatientFeeDetailBill1.ShowBill(new System.Collections.ArrayList(), currentRegister);
        //        ucInPatientFeeDetailBill1.ShowBill(GetFeeInfoByJYDJH(), currentRegister);
        //    }
        //}

        void QueryRegister(string patientNO)
        {
            if (string.IsNullOrEmpty(patientNO))
            {
                return;
            }            
            DataTable dtPatient = registerMgr.QuerySIPatientAll(patientNO);
            if (dtPatient == null || dtPatient.Rows.Count == 0)
            {
                //MessageBox.Show("没有找到相关住院结算信息，请检查该住院号是否有效");
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
        /// 通过住院号构造患者费用列表
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <returns></returns>
        public DataTable GetFeeInfoByJYDJH()
        {
            FS.HISFC.BizLogic.RADT.InPatient Inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
            string liushuihao = this.ucQueryInfo.InpatientNo;

            string inpatientno = this.ucQueryInfo.Text;

            string jydjh = liushuihao;
            System.Collections.ArrayList feeList = new System.Collections.ArrayList();
            System.Data.DataTable dt = new DataTable();
            if (inpatientno.Contains("B"))
            {
                //  string inpatientnoMother = "00" + inpatientno.Substring(2);
                string motherpatienno = Inpatient.QueryBabyMotherInpatientNO(jydjh);
                if (!string.IsNullOrEmpty(motherpatienno))
                {
                    //   string liushuihaomother = ((FS.FrameWork.Models.NeuObject)alInpatientNos[0]).ID;

                    dt = feeManager.GetPatientChildTotalFeeDTInfoByInPatientNOBaByYB2(motherpatienno);// GetInpatientSiBalanceListAsFee(jydjh);
                }

            }
            else
            {
                dt = feeManager.GetPatientChildTotalFeeDTInfoByInPatientNOBaByYB1(jydjh);// GetInpatientSiBalanceListAsFee(jydjh);
            }
            return dt;
        }


       

        //{8AA24CF1-D42B-4978-9D60-7083330080E5}
        protected override int OnPrint(object sender, object neuObject)
        {
            ucInPatientFeeDetailBill1.PrintPreview();

            return 1;
        }
        public override int Export(object sender, object neuObject)
        {

            ucInPatientFeeDetailBill1.Export();// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}

            return 1;
        }
        private void ucQueryInfo_Load(object sender, EventArgs e)
        {

        }

    }
}
