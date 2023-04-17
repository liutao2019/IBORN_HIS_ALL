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
    public partial class ucAuditFee : FS.FrameWork.WinForms.Controls.ucBaseControl
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
        public ucAuditFee()
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
            toolBarService.AddToolButton("审核", "审核", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            //toolBarService.AddToolButton("取消审核", "取消审核", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
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
                case "审核":
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
            string sql="";
            registerMgr.Sql.GetSql("Fee.Account.GetLimitAccountFee", ref sql);
            sql = string.Format(sql,
                 this.dateTimePicker1.Value.ToShortDateString(),
                 this.dateTimePicker2.Value.ToShortDateString());
                 //FS.FrameWork.Function.NConvert.ToInt32(this.chkValidFlag.Checked));
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread1.DataSource = dsRes;

            foreach (FarPoint.Win.Spread.Row row in this.fpSpread1_Sheet1.Rows)
            {
                if (this.fpSpread1_Sheet1.Cells[row.Index, 2].Text == "审核")
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
            string cardno = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;

            string sql = "update gzsi_his_accountdetail set state=1 where card_no='{0}' and createtime>= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and createtime < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') + 1";
            sql = string.Format(sql,cardno,
                 this.dateTimePicker1.Value.ToShortDateString(),
                 this.dateTimePicker2.Value.ToShortDateString());
            DataSet dsRes = new DataSet();
            int result=registerMgr.ExecNoQuery(sql);

            if(result>0)
               MessageBox.Show("审核操作成功");
            else
               MessageBox.Show("审核操作失败");
            QueryBalancedPatients();
            return 1;
        }
    }
}
