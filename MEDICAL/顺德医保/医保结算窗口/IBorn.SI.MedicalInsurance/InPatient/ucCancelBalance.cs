using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.MedicalInsurance.FoShan.InPatient
{
    public partial class ucCancelBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// 医保待遇算法
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
       
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.HISFC.Models.RADT.PatientInfo currentRegister;
        public ucCancelBalance()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
        }

        void QueryBalancedPatients()
        {
            //todo:需优化，代码整合
            string sql = string.Format(@"select r.Inpatient_No 住院流水号,
       j.REG_NO       就医登记号,
       substr(j.remark,0,8)   医保结算表ID, 
       j.invoice_no 结算发票号,
       r.name         姓名,
       r.dept_name    住院科室,
       r.in_date      入院日期,
       j.oper_date    医保结算日期,
       j.pub_cost     医保结算金额
  from fin_ipr_inmaininfo r
  join fin_ipr_siinmaininfo j
    on j.Inpatient_No = r.Inpatient_No
   and j.valid_flag = '1'
   and j.type_code = '2'
 where j.oper_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and j.oper_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') + 1
 order by j.oper_date", FS.FrameWork.Function.NConvert.ToInt32(this.chkValidFlag.Checked), this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread1.DataSource = dsRes;
           
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
            toolBarService.AddToolButton("取消医保结算", "取消广州医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);
            return this.toolBarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            QueryBalancedPatients();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "取消医保结算":
                    try
                    {
                        if (fpSpread1_Sheet1.RowCount == 0)
                        {
                            return;
                        }
                        int rowIndex = fpSpread1_Sheet1.ActiveRowIndex;
                        string inPatientNO = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
                        if (string.IsNullOrEmpty(inPatientNO))
                        {
                            return;
                        }
                        if (MessageBox.Show("HIS本地只保存了医保客户端的结算信息，取消结算是指取消HIS本地的医保结算信息，真正取消结算还需到医保客户端操作取消，请问是否继续？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        currentRegister = registerMgr.QueryPatientInfoByInpatientNO(inPatientNO);
                        currentRegister.SIMainInfo.RegNo = this.fpSpread1_Sheet1.Cells[rowIndex, 1].Text;
                        currentRegister.SIMainInfo.BalNo = this.fpSpread1_Sheet1.Cells[rowIndex, 2].Text;
                        currentRegister.SIMainInfo.InvoiceNo = this.fpSpread1_Sheet1.Cells[rowIndex, 3].Text;
                        if (string.IsNullOrEmpty(currentRegister.SIMainInfo.RegNo))
                        {
                            MessageBox.Show("医保结算就诊登记号获取不正确");
                            return;
                        }
                         System.Collections.ArrayList feeDetails = new System.Collections.ArrayList();
                         // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
                         long returnValue = medcareInterfaceProxy.SetPactCode(currentRegister.Pact.ID);

                         if (returnValue != 1)
                         {
                             MessageBox.Show("接口未实现！" + this.medcareInterfaceProxy.ErrMsg);
                             return;
                         }

                         if (medcareInterfaceProxy.CancelBalanceInpatient(currentRegister, ref feeDetails) < 0)
                        {
                            MessageBox.Show("取消医保结算失败，"+medcareInterfaceProxy.ErrMsg);
                            return;
                        }
                        MessageBox.Show("取消医保结算操作成功");
                        QueryBalancedPatients();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


    }
}
