using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance.SIBalance
{
    public partial class ucBalanceCancel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.RADT.InPatient radtMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// 医保代理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 当前操作患者
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo;

        public ucBalanceCancel()
        {
            InitializeComponent();
            fpSpread1_Sheet1.RowCount = 0;
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
            toolBarService.AddToolButton("取消医保结算", "取消广州医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);
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
            string sql = @"select t.inpatient_no 住院流水号,
                                 t.reg_no 就诊ID,
                                 t.pact_code 合同单位编码,
                                 t.pact_name 合同单位,
                                 t.med_type 医疗类别编码,
                                 (select a.name from com_dictionary a where a.type = 'GZSI_med_type' and a.code = t.med_type) 医疗类别,
                                 t.dise_code 病种编码,
                                 t.dise_name 病种名称,
                                 t.setl_id 结算ID,
                                 t.invoice_no 医保发票号,
                                 t.name 姓名,
                                 t.dept_name 科室,
                                 t.in_date 入院日期,
                                 t.balance_date 医保结算日期,
                                 t.medfee_sumamt 医保结算金额
                          from fin_ipr_siinmaininfo t
                        where t.valid_flag = '1'
                           and t.balance_state = '1'
                           and t.type_code = '2'
                           and t.balance_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                           and t.balance_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') + 1
                           and (t.name='{3}' or '{3}'='ALL')
                         order by  t.balance_date desc";

            string name =this.tbName.Text.Trim();

            //增加姓名查询条件
            if (this.tbName.Text == "")
                name = "ALL";

            sql = string.Format(sql, FS.FrameWork.Function.NConvert.ToInt32(this.chkValidFlag.Checked), 
                 this.dateTimePicker1.Value.ToShortDateString(), 
                 this.dateTimePicker2.Value.ToShortDateString(),
                 name);
            DataSet dsRes = new DataSet();
            radtMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread1.DataSource = dsRes;
        }

        private int saveSIQuit()
        {
            int rowIndex = fpSpread1_Sheet1.ActiveRowIndex;
            string inPatientNO = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
            if (string.IsNullOrEmpty(inPatientNO))
            {
                return -1;
            }
            patientInfo = radtMgr.QueryPatientInfoByInpatientNO(inPatientNO);

            if (this.patientInfo == null)
            {
                MessageBox.Show("未选择患者");
                return -1;
            }

            string errText = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.medcareInterfaceProxy.BeginTranscation();
            long returnValue = medcareInterfaceProxy.SetPactCode(this.patientInfo.Pact.ID);
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

            ArrayList feeDetail = new ArrayList();
            //医保预结算
            returnValue = medcareInterfaceProxy.CancelBalanceInpatient(this.patientInfo, ref feeDetail);
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
