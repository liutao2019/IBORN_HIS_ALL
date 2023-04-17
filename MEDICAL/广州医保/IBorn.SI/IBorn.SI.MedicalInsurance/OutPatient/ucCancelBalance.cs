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
    public partial class ucCancelBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
        IBorn.SI.MedicalInsurance.Proxy.MedicalFactoryProxy medicalInterfaceProxy = new Proxy.MedicalFactoryProxy();
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.HISFC.Models.Registration.Register currentRegister;
        public ucCancelBalance()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
        }

        void QueryBalancedPatients()
        {
            //todo:需优化，代码整合
            string sql = string.Format(@"select r.clinic_code 门诊流水号,j.jydjh 就医登记号,j.jsid 医保结算表ID,j.invoice_no 结算发票号,r.name 姓名,fun_get_dept_name(r.see_dpcd) 看诊科室,r.see_date 看诊时间,fun_get_employee_name(r.see_docd) 看诊医生,j.jsrq 结算日期
from fin_opr_register r join GZSI_HIS_MZJS j on j.clinic_code=r.clinic_code and j.valid_flag='1' 
where r.valid_flag='{0}' and j.jsrq >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and j.jsrq < to_date('{2}','yyyy-mm-dd hh24:mi:ss')+1 order by r.see_date", FS.FrameWork.Function.NConvert.ToInt32(this.chkValidFlag.Checked), this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());
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
                        string clinicNO = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
                        if (string.IsNullOrEmpty(clinicNO))
                        {
                            return;
                        }
                        if (MessageBox.Show("HIS本地只保存了医保客户端的结算信息，取消结算是指取消HIS本地的医保结算信息，真正取消结算还需到医保客户端操作取消，请问是否继续？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        currentRegister = registerMgr.GetByClinic(clinicNO);
                        currentRegister.SIMainInfo.RegNo = this.fpSpread1_Sheet1.Cells[rowIndex, 1].Text;
                        currentRegister.SIMainInfo.BalNo = this.fpSpread1_Sheet1.Cells[rowIndex, 2].Text;
                        //currentRegister.SIMainInfo.InvoiceNo = this.fpSpread1_Sheet1.Cells[rowIndex, 3].Text;
                        if (string.IsNullOrEmpty(currentRegister.SIMainInfo.RegNo))
                        {
                            MessageBox.Show("获取医保结算ID出错");
                            return;
                        }
                        if (string.IsNullOrEmpty(currentRegister.SIMainInfo.BalNo))
                        {
                            MessageBox.Show("获取医保结算ID出错");
                            return;
                        }
                        if (medicalInterfaceProxy.CancelBalanceOutPatient(currentRegister) < 0)
                        {
                            MessageBox.Show(medicalInterfaceProxy.ErrorMsg);
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
