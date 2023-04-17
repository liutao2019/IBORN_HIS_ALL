using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.Controls.SIBalance
{
    public partial class ucBalanceCancel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 医保代理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        // {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register register;

        private string pact_code_SI = "4";// {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
        /// <summary>
        /// 医保合同单位
        /// </summary>
        public string Pact_code_SI
        {
            get { return pact_code_SI; }
            set { pact_code_SI = value; }
        }

        /// <summary>
        /// 门诊医保结算取消
        /// </summary>
        public ucBalanceCancel()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
            this.pact_code_SI = ctlParamManage.GetControlParam<string>("YB0001");// {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
        }

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 任务栏初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("取消医保结算", "取消佛山医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            QueryBalancedPatients();
            return 1;
        }

        /// <summary>
        /// 任务栏点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "取消医保结算":
                    this.saveSIQuit();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 查询已结算患者
        /// </summary>
        private void QueryBalancedPatients()
        {
            //todo:需优化，代码整合
            // {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
            string sql = @"select decode(t3.pact_code,'{0}','正常结算','补录结算') 结算方式,
                                 t.inpatient_no 门诊流水号,
                                 t.reg_no 就诊ID,
                                 t.setl_id 结算ID,
                                 t2.reg_date 就诊日期,
                                 t.name 姓名,
                                 t.dept_name 科室,
                                 t.dise_code 病种编码,
                                 t.dise_name 病种名称,
                                 t.invoice_no 医保发票号,
                                 t.medfee_sumamt 医保结算金额,
                                 t.balance_date 医保结算日期,
                                 t.pact_code 合同单位编码,
                                 t.pact_name 合同单位,
                                 t.med_type 医疗类别编码,
                                 (select a.name from com_dictionary a where a.type = 'GZSI_med_type' and a.code = t.med_type) 医疗类别
                          from fin_ipr_siinmaininfo t 
                          left join fin_opr_register t2 on t.inpatient_no = t2.clinic_code
                          left join fin_opb_invoiceinfo t3 on t.inpatient_no = t3.clinic_code 
                                                          and t.invoice_no = t3.invoice_no 
                                                          and t3.cancel_flag in ('1','2')
                         where t.balance_state = '1'
                           and t.type_code = '1'
                           --and t3.pact_code = '9' 
                           and t.balance_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                           and t.balance_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') + 1
                           and t.valid_flag = '{3}'
                         order by t.balance_date desc";
            sql = string.Format(sql, pact_code_SI,
                 this.dateTimePicker1.Value.ToShortDateString(), 
                 this.dateTimePicker2.Value.ToShortDateString(),
                 FS.FrameWork.Function.NConvert.ToInt32(this.chkValidFlag.Checked));
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread1.DataSource = dsRes;

            foreach (FarPoint.Win.Spread.Row row in this.fpSpread1_Sheet1.Rows)
            {
                if (this.fpSpread1_Sheet1.Cells[row.Index, 0].Text == "正常结算")
                {
                    row.ForeColor = Color.Red;
                }
                else
                {
                    row.ForeColor = Color.Black;
                }
            }
        }

        private int saveSIQuit()
        {
            int rowIndex = fpSpread1_Sheet1.ActiveRowIndex;
            string balanceType = this.fpSpread1_Sheet1.Cells[rowIndex,0].Text;
            string clinicNO = this.fpSpread1_Sheet1.Cells[rowIndex, 1].Text;

            if (balanceType == "正常结算")
            {
                MessageBox.Show("此记录不允许在此取消结算，请在门诊退费中进行退费处理！");
                return -1;
            }

            if (string.IsNullOrEmpty(clinicNO))
            {
                return -1;
            }

            register = registerMgr.GetByClinic(clinicNO);

            if (this.register == null)
            {
                MessageBox.Show("未选择患者");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.medcareInterfaceProxy.BeginTranscation();
            long returnValue = medcareInterfaceProxy.SetPactCode(pact_code_SI);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医疗待遇接口初始化失败：" + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //连接待遇接口
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                MessageBox.Show("医疗待遇接口连接失败：" + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            this.register.SIMainInfo.InvoiceNo = this.fpSpread1_Sheet1.Cells[rowIndex, 9].Text;
            returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.register);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口或得患者信息失败") + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            ArrayList feeDetail = new ArrayList();
            //取消门诊结算
            returnValue = medcareInterfaceProxy.CancelBalanceOutpatient(this.register, ref feeDetail);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("取消结算失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //断开医保连接
            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医保断开连接失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            this.medcareInterfaceProxy.Commit();
            this.medcareInterfaceProxy.Disconnect();
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("取消医保结算操作成功");
            QueryBalancedPatients();
            return 1;
        }
    }
}
