using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DiseasePay
{
    public partial class frmInsertSIFee : Form
    {
        public frmInsertSIFee()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.Models.RADT.PatientInfo inPatientInfo = null;

        private FoShanDiseasePay.BizLogic.InManager inMgr = new FoShanDiseasePay.BizLogic.InManager();

        private FS.HISFC.BizLogic.RADT.InPatient registerMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// 合同单位编码
        /// </summary>
        private string pactCode = "";
        /// <summary>
        /// 合同单位名称
        /// </summary>
        private string pactName = "";

        private void btQuery_Click(object sender, EventArgs e)
        {

            this.lblPatientInfo.Text = "患者基本信息：";
            inPatientInfo = null;

            string strNo = this.txtPatientNO.Text.Trim();
            //strClNo = strNo;
            if (string.IsNullOrEmpty(strNo))
            {
                return;

            }
            if (string.IsNullOrEmpty(this.txtInTimes.Text.Trim()))
            {
                MessageBox.Show("请输入住院次数！");
                return;
            }

            strNo = strNo.PadLeft(10, '0');
            string strClNo = this.getInpatientNo(strNo, this.txtInTimes.Text);
            if (string.IsNullOrEmpty(strClNo))
            {
                MessageBox.Show("找不到患者信息！");
                return;
            }
            inPatientInfo = this.radtIntegrate.GetPatientInfomation(strClNo);

            this.lblPatientInfo.Text = "患者基本信息：\n" 
                + "住院流水号：" + inPatientInfo.ID
                + "\n患者名字：" + inPatientInfo.Name 
                + "\n入院时间：" + inPatientInfo.PVisit.InTime.ToString("yyyy-MM-dd")
                + "\n出院时间：" + inPatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");

        }

        private void btClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            this.lblPatientInfo.Text = "患者基本信息：";
            this.txtInTimes.Text = "";
            this.txtPatientNO.Text = "";
            this.txtMNo.Text = "";
            inPatientInfo = null;
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMNo.Text))
            {
                MessageBox.Show("请输入结算单号！");
                this.txtMNo.Focus();
                return;
            }
            if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
            {
                MessageBox.Show("请先查询患者！");
                this.txtPatientNO.Focus();
                return;                
            }
            string sql = "select count(*) from fin_ipr_siinmaininfo s where s.inpatient_no = '{0}' and s.remark = '{1}'";
            sql = string.Format(sql, inPatientInfo.ID,this.txtMNo.Text);
            string res = this.con.ExecSqlReturnOne(sql);

            if (!string.IsNullOrEmpty(res) && int.Parse(res) > 0)
            {
                MessageBox.Show("信息已经保存，如需重新上传，请先删除！");
                return; 

            }




            DataTable currentDtUploadFee = inMgr.QueryInPatientNeedUploadFeeDetail(inPatientInfo.ID);


            if (currentDtUploadFee == null)
            {
                MessageBox.Show("没有找到需要结算的费用信息！");
                return;
            }

            ArrayList alFeeItemLists = this.ToFeeItem(currentDtUploadFee);

            if (alFeeItemLists == null || alFeeItemLists.Count <= 0)
            {
                MessageBox.Show("没有需要上传的费用明细。");
                return;
            }
           string invoiceNo = "";
            string totCost = "";
            GetInvoiceInfo(inPatientInfo.ID, ref invoiceNo, ref totCost);
            if (string.IsNullOrEmpty(invoiceNo) || string.IsNullOrEmpty(totCost))
            {
                MessageBox.Show("发票信息为空。");
                return;
            }
            inPatientInfo.SIMainInfo.Memo = this.txtMNo.Text.Trim();
            inPatientInfo.SIMainInfo.RegNo = this.txtMNo.Text.Trim();
            inPatientInfo.SIMainInfo.InvoiceNo = invoiceNo;
            inPatientInfo.SIMainInfo.TotCost = decimal.Parse(totCost);
            //if (string.IsNullOrEmpty(pactCode) || string.IsNullOrEmpty(pactName))
            //{
            //    this.GetPactInfo(ref pactCode, ref pactName);
            //}
            //if (string.IsNullOrEmpty(pactCode) || string.IsNullOrEmpty(pactName))
            //{
            //    MessageBox.Show("请维护表com_dictionary的type = 'HTDW01'的合同单位！");
            //    return;
            //}

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            inMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //设置事务


            if (inMgr.InsertSiInmaininfo(inPatientInfo.ID, this.txtMNo.Text.Trim()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("插入fin_ipr_siinmaininfo信息失败！" + this.inMgr.Err);
                return;

            }

            if (inMgr.DeletePatientUploadFeeDetail(inPatientInfo.ID, inPatientInfo.SIMainInfo.RegNo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除上传明细信息失败！" + this.inMgr.Err);
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fTemp in alFeeItemLists)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = fTemp.Clone();
                // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                string ownFeeInputCode = inMgr.QueryFeeOwnOrPut(f.Order.ID, f.Item.ID, "2");
                if (!string.IsNullOrEmpty(ownFeeInputCode))
                {
                    f.Item.UserCode = ownFeeInputCode;
                }
                if (inMgr.InsertInPatientUploadFeeDetail(inPatientInfo, f) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("上传明细信息失败！" + this.inMgr.Err);
                    return;
                }
            }
            if (inMgr.UpdateBlanceSIPatient(inPatientInfo.ID, inPatientInfo.SIMainInfo.RegNo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废医保信息失败！" + this.inMgr.Err);
                    return;
                }

            if (inMgr.SaveBlanceSIInPatient(inPatientInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存医保信息失败！" + this.inMgr.Err);
                return;
            }
            
            

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功！");
        }

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <param name="inPatientID"></param>
        /// <param name="InvoiceNo"></param>
        /// <param name="totcost"></param>
        private void GetInvoiceInfo(string inPatientID, ref string InvoiceNo, ref string totcost)
        {
            InvoiceNo = "";
            totcost = "";
            string sql = "select wm_concat(d.invoice_no) invoice_no,sum(d.tot_cost) tot_cost from fin_ipb_balancehead d where d.inpatient_no = '{0}' ";
            sql = string.Format(sql, inPatientID);
            System.Data.DataSet dsResult = null;
            if (this.con.ExecQuery(sql, ref dsResult) == -1)
            {
                return;
            }
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                InvoiceNo = dr["invoice_no"].ToString();
                totcost = dr["tot_cost"].ToString();
                break;
            }

        }

        /// <summary>
        /// 需要上传医保的费用转换// {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ArrayList ToFeeItem(System.Data.DataTable dtFeeDetail)
        {
            ArrayList alFeeItem = new ArrayList();
            if (dtFeeDetail != null)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = null;
                foreach (System.Data.DataRow dr in dtFeeDetail.Rows)
                {
                    f = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    f.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(dr["MCYL"].ToString());//数量
                    f.Item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["JG"].ToString());//价格
                    f.Item.PriceUnit = dr["current_unit"].ToString();//计价单位
                    f.Item.UserCode = dr["XMBH"].ToString();//自定义码
                    f.Item.Name = dr["XMMC"].ToString();//项目名称
                    f.Item.Specs = dr["YPGG"].ToString();//规格
                    f.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["JE"].ToString());//金额
                    f.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(dr["FYRQ"].ToString());//费用日期
                    f.Item.MinFee.ID = dr["FEE_CODE"].ToString();//最小费用代码
                    f.Item.Specs = dr["YPGG"].ToString();//规格

                    alFeeItem.Add(f);


                }
            }
            else
            {
                return null;
            }

            return alFeeItem;
        }
        private void btDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将删除更改的信息，是否继续删除?", "疑问", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtMNo.Text))
            {
                MessageBox.Show("请输入结算单号！");
                this.txtMNo.Focus();
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            inMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //设置事务

            if (inMgr.DeleteSiInmaininfo(inPatientInfo.ID, this.txtMNo.Text.Trim()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除fin_ipr_siinmaininfo信息失败！" + this.inMgr.Err);
                return;
            }
            if (inMgr.UpdateBlanceSIPatient(inPatientInfo.ID, this.txtMNo.Text.Trim()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("作废医保结算信息失败！" + this.inMgr.Err);
                return;
            }
            if (inMgr.DeletePatientUploadFeeDetail(inPatientInfo.ID, this.txtMNo.Text.Trim()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除医保上传的费用明细失败！" + this.inMgr.Err);
                return;
            }
            
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("删除成功！");
        }

        private void frmInsertSIFee_Load(object sender, EventArgs e)
        {
            //清屏
            this.Clear();

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.btDel.Visible = true;
            }
            else
            {
                this.btDel.Visible = false;
            }
            //this.GetPactInfo(ref pactCode, ref pactName);

            //if (string.IsNullOrEmpty(pactCode) || string.IsNullOrEmpty(pactName))
            //{
            //    MessageBox.Show("请维护表com_dictionary的type = 'HTDW01'的合同单位！");
            //}
        }
        public string getInpatientNo(string patientNo, string qty)
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

        private void GetPactInfo(ref string pactCode1, ref string pactName1)
        {
            string sql = "select y.code,y.name from com_dictionary y where y.type = 'HTDW01' and y.valid_state = '1' ";
            System.Data.DataSet dsResult = null;
            if (this.con.ExecQuery(sql, ref dsResult) == -1)
            {
                return;
            }
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                pactCode1 = dr["code"].ToString();
                pactName1 = dr["name"].ToString();
                break;
            }

        }
    }
}
