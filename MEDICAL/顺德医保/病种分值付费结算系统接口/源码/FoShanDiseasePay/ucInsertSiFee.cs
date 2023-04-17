using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using FS.HISFC.Models.Fee;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using FoShanDiseasePay.DataBase;
using FoShanDiseasePay.Jobs;
using System.Text.RegularExpressions;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 手工报销的患者插入收费记录
    /// </summary>
    public partial class ucInsertSiFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucInsertSiFee()
        {
            InitializeComponent();


        }

        #region 事件
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();


        FS.HISFC.Models.RADT.PatientInfo inPatientInfo = null;

        /// <summary>
        /// 合同单位编码
        /// </summary>
        private string pactCode = "";
        /// <summary>
        /// 合同单位名称
        /// </summary>
        private string pactName = "";
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //清屏
            this.Clear();

            this.GetPactInfo(ref pactCode, ref pactName);
            if (string.IsNullOrEmpty(pactCode) || string.IsNullOrEmpty(pactName))
            {
                MessageBox.Show("请维护表com_dictionary的type = 'HTDW01'的合同单位！");
            }
            base.OnLoad(e);
        }

        private void GetPactInfo(ref string pactCode, ref string pactName)
        {
            string sql = "select y.code,y.name from com_dictionary y where y.type = 'HTDW01'";
            System.Data.DataSet dsResult = null;
            if (this.con.ExecQuery(sql, ref dsResult) == -1)
            {
                return;
            }
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                pactCode = dr["code"].ToString();
                pactName = dr["name"].ToString();
            }

        }

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            //this.toolBarService.AddToolButton("待上传患者", "待上传患者查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            //this.toolBarService.AddToolButton("一键上传", "一键上传病种分值付费接口", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

       

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //switch (e.ClickedItem.Text)
            //{
            //    case "待上传患者":
            //        this.Clear();
            //        break;
            //}

            base.ToolStrip_ItemClicked(sender, e);
        }

        private string strClNo = "";
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            //this.lblQty.Text = "";
            //this.txtNo.Text = "";
            this.txtDJH.Text = "";
            this.lblPatientInfo.Text = "";
            inPatientInfo = null;
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Clear();
            string strNo = this.txtNo.Text.Trim();
            //strClNo = strNo;
            if (string.IsNullOrEmpty(strNo))
            {
                return -1;

            }
            if (string.IsNullOrEmpty(this.lblQty.Text.Trim()))
            {
                MessageBox.Show("请输入住院次数！");
                return -1;
            }

            strNo = strNo.PadLeft(10, '0');
            strClNo = this.getInpatientNo(strNo, this.lblQty.Text);
            if (string.IsNullOrEmpty(strClNo))
            {
                MessageBox.Show("找不到患者信息！");
                return -1;
            }
             inPatientInfo = this.radtIntegrate.GetPatientInfomation(strClNo);
            if (inPatientInfo == null)
            {
                MessageBox.Show("找不到患者信息1！");
                return -1;
            }

            this.lblPatientInfo.Text = "住院流水号：" + inPatientInfo.ID 
                + "\n患者名字：" + inPatientInfo.Name +"\n入院时间："+inPatientInfo.PVisit.InTime.ToString("yyyy-MM-dd")
                + "\n出院时间：" + inPatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");
             
            return base.OnQuery(sender, neuObject);
        }

        public string getInpatientNo(string patientNo ,string qty)
        {
            string sql = "select a.inpatient_no from fin_ipr_inmaininfo a where a.patient_no = '{0}' and a.in_times = '{1}'";
            sql = string.Format(sql, patientNo, qty);
            string str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }

            return str;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (this.inPatientInfo == null)
            {
                MessageBox.Show("请查询一个患者！");
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtDJH.Text.Trim()))
            {
                MessageBox.Show("请输入收费单据号！");
                this.txtDJH.Focus();
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            con.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //设置事务

            if (UpdatePact(inPatientInfo.ID) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新fin_ipr_inmaininfo合同身份失败！" + this.con.Err);
                return -1;
            }
            if (UpdatePact1(inPatientInfo.ID) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新fin_ipb_balancehead合同身份失败！" + this.con.Err);
                return -1;
            }
            if (InsertSiInmaininfo(inPatientInfo.ID, this.txtDJH.Text.Trim()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("插入fin_ipr_siinmaininfo信息失败！" + this.con.Err);
                return -1;

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("更新成功！");
            return base.OnSave(sender, neuObject);
        }


        private int InsertSiInmaininfo(string inPatientNo, string DJH)
        {
            if (string.IsNullOrEmpty(inPatientNo) || string.IsNullOrEmpty(DJH))
            {
                this.con.Err = "住院信息为空！";
                return -1;
            }
            string sql = @"insert into fin_ipr_siinmaininfo s
                              (s.inpatient_no,
                               s.fee_times,
                               s.balance_no,
                               s.card_no,
                               s.name,
                               s.sex_code,
                               s.dept_code,
                               s.paykind_code,
                               s.pact_code,
                               s.pact_name,
                               s.in_date,
                               s.balance_date,
                               s.oper_code,
                               s.oper_date,
                               s.remark,
                               s.invoice_no ,
                               s.TYPE_CODE
                               ) values
                               (
                               '{0}',--住院流水号
                               '0',
                               '1',
                               (select i.card_no from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.name from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.sex_code from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.dept_code from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               '02',
                               (select i.pact_code from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.pact_name from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.in_date from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.balance_date from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               '009999',
                               sysdate ,
                               '{1}' ,--社保结算号   
                               (select d.invoice_no from fin_ipb_balancehead d where d.inpatient_no = '{0}' and rownum = '1'),
                               '2'
                               )";
            sql = string.Format(sql,inPatientNo,DJH);


            return this.con.ExecNoQuery(sql);
        }

        private int UpdatePact(string inPatientNo)
        {
            if (string.IsNullOrEmpty(inPatientNo))
            {
                this.con.Err = "住院信息为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(pactCode) || string.IsNullOrEmpty(pactName))
            {                
                this.con.Err = "请维护表com_dictionary的type = 'HTDW01'的合同单位！";
                return -1;
            }
            string sql = @"update fin_ipr_inmaininfo a
                        set a.pact_code = '{1}',
                            a.pact_name = '{2}'
                        where a.inpatient_no = '{0}'";

            sql = string.Format(sql, inPatientNo, pactCode, pactName);

            return this.con.ExecNoQuery(sql);

        }

        private int UpdatePact1(string inPatientNo)
        {
            if (string.IsNullOrEmpty(inPatientNo))
            {
                this.con.Err = "住院信息为空1！";
                return -1;
            }
            if (string.IsNullOrEmpty(pactCode))
            {
                this.con.Err = "请维护表com_dictionary的type = 'HTDW01'的合同单位！";
                return -1;
            }
            string sql = @"update fin_ipb_balancehead d
                            set d.pact_code = '{1}'
                            where d.inpatient_no = '{0}'";

            sql = string.Format(sql, inPatientNo, pactCode);

            return this.con.ExecNoQuery(sql);

        }
        #endregion


        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.txtNo.Text.Trim()))
                {
                    this.OnQuery(null,null);
                }
                else
                {
                    this.Clear();
                }

            }
        }

    }
}
