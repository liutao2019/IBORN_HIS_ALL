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
   // {130C7CAA-7CB1-49b9-84DC-BE0775140BF3}
    public partial class ucLXBXPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        FS.HISFC.Models.Registration.Register currentRegister;

        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.HISFC.BizLogic.Registration.Register registerOutMgr = new FS.HISFC.BizLogic.Registration.Register();

        private IBorn.SI.GuangZhou.SILocalManager siLocalManager = new IBorn.SI.GuangZhou.SILocalManager();

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        private FS.HISFC.Models.RADT.PatientInfo pinfo = new FS.HISFC.Models.RADT.PatientInfo();
        public ucLXBXPrint()
        {
            InitializeComponent();
            //fpSpread1_Sheet1.RowCount = 0;
            //this.ucOutPatientInfo1.GetRegisterByCardNO += QueryRegister;
            Init();
        }
        private void Init()
        {
            dtpbegin.Value = DateTime.Now.AddMonths(-6);
            dtpend.Value = DateTime.Now;
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            var regDeptList =managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);;
            if (regDeptList == null)
            {
                MessageBox.Show("初始化挂号科室出错!" + managerIntegrate.Err);
                return;
            }
            this.cmbRegDept.AddItems(regDeptList);
            for (int i = 0; i < regDeptList.Count; i++)
			{
                if((regDeptList[i] as FS.HISFC.Models.Base.Department).Name=="妇产科门诊")
                {
                this.cmbRegDept.SelectedIndex=i;
                }
			 
			}
            var doctList = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                MessageBox.Show("初始化医生列表出错!" + managerIntegrate.Err);
                return;
            }

            this.cmbDoct.AddItems(doctList);
        }

        void SetShowValue(string clinicNO, string invoiceNO, string pactCode)
        {
            currentRegister = registerOutMgr.GetByClinic(clinicNO);
            currentRegister.SIMainInfo.InvoiceNo = invoiceNO;
            currentRegister.Pact.ID = pactCode;
            //ucOutPatientInfo1.SetPatientInfo(currentRegister);

        }

        //private void txtRegNO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtRegNO.Text))
        //    {
        //        SetShowBillByClincode(txtRegNO.Text);
        //    }
        //}

        private void SetShowBillByClincode(string cardno,string begin,string end )
        {
           // FS.HISFC.Models.Registration.Register currentRegister = registerOutMgr.GetByClinic(clincode);
            //  ucOutPatientInfo1.SetPatientInfo(currentRegister);

            System.Collections.ArrayList noZerolist = new System.Collections.ArrayList();
            System.Collections.ArrayList templist = GetOutFeeInfoByClinCode(cardno,begin,end);

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfo in templist)
            {
                if (feeinfo.Item.Price.ToString("0.0000") != "0.0000")
                {
                    noZerolist.Add(feeinfo);
                }
            }
            if (checkBox1.Checked == true)
            {
                ucOutPatientFeeDetailBill1.ShowBill(noZerolist,this.pinfo,cmbRegDept.Text);
            }
            else
            {
                ucOutPatientFeeDetailBill1.ShowBill(templist,this.pinfo,cmbRegDept.Text);
            }


            // ucOutPatientFeeDetailBill1.ShowBill(, currentRegister);
        }

//        void QueryRegister(string cardno)
//        {
//            // todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
//            string sql = string.Format(@" select r.clinic_code 门诊流水号,r.name 姓名,fun_get_dept_name(r.see_dpcd) 看诊科室,r.see_date 看诊时间,fun_get_employee_name(r.see_docd) 看诊医生 
//                                            from fin_opr_register r    
//                                            where  
//                                             r.valid_flag='1'  
//                                            and
//                                            r.card_no='{0}' and 
//                                            r.reg_date>trunc(sysdate)-180
//                                            order by r.see_date desc", cardno);
//            DataSet dsRes = new DataSet();
//            registerMgr.ExecQuery(sql, ref dsRes);
//            if (dsRes == null || dsRes.Tables[0] == null || dsRes.Tables[0].Rows.Count == 0)
//            {
//                MessageBox.Show("没有找到相关门诊结算信息，请检查该卡号是否有效或该卡号是否结算过");
//                return;
//            }


//            string clinicNO = dsRes.Tables[0].Rows[0]["门诊流水号"].ToString();
//            //string invoiceNO = ucChoose.SelectedNO;
//            //string pactCode = ucChoose.SelectedRemarkNO;
//            //SetShowValue(clinicNO, invoiceNO, pactCode);
//            //string regNo = getJYDJHByRegister(clinicNO, invoiceNO);
//            // SetShowBillByJYDJH(getJYDJHByClinicNo(clinicNO));
//            SetShowBillByClincode(clinicNO);

//        }
        protected override int OnQuery(object sender, object neuObject)
        {
            SetShowBillByClincode(this.pinfo.PID.CardNO, dtpbegin.Text, dtpend.Text);
            return 1;
        }

      
        /// <summary>
        /// 通过门诊号构造患者费用列表
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetOutFeeInfoByClinCode(string cardno,string begin,string end)
        {
            System.Collections.ArrayList feeList = new System.Collections.ArrayList();

            System.Data.DataTable dt = siLocalManager.GetOutpatientBalanceListAsFee(cardno,begin,end,this.cmbRegDept.SelectedItem.ID);

            foreach (DataRow dr in dt.Rows)
            {
                //设置对应的医保信息引用到feeinfo中
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeinfo.Item.Name = dr["名称"].ToString();
                feeinfo.Item.Specs = dr["规格"].ToString();
                feeinfo.Item.Price = Convert.ToDecimal(dr["医保价"].ToString());
                feeinfo.Item.Qty = Convert.ToDecimal(dr["数量"].ToString());
                feeinfo.FT.TotCost = Convert.ToDecimal(dr["金额"].ToString());
                feeinfo.Item.PriceUnit = dr["单位"].ToString();//药品单位
                feeinfo.FeeOper.OperTime = Convert.ToDateTime(dr["费用时间"]);
                feeinfo.User01 =dr["费用类别"].ToString();
                feeList.Add(feeinfo);

                //ucOutPatientFeeDetailBill1.SetBillYBFY(Convert.ToDecimal(dr["ZYZJE"].ToString()), Convert.ToDecimal(dr["SBZFJE"].ToString()), Convert.ToDecimal(dr["GRZFJE1"].ToString()));
            }
            return feeList;
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


        //{8AA24CF1-D42B-4978-9D60-7083330080E5}
        protected override int OnPrint(object sender, object neuObject)
        {
            ucOutPatientFeeDetailBill1.PrintPreview();

            return 1;
        }
        public override int Export(object sender, object neuObject)
        {

            ucOutPatientFeeDetailBill1.Export();// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}// {2961D24B-E987-4ed5-B2B6-373847410ED2}

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
                    this.pinfo = frmQuery.PatientInfo;
                    // this.ucOutPatientInfo1.setCardNo(card);
                    //txtMedicalCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                   //  QueryRegister(pinfo.Card.ID);
                    SetShowBillByClincode(pinfo.PID.CardNO, dtpbegin.Text, dtpend.Text);
                }
            }
        }

    }
}
