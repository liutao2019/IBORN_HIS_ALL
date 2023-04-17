using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using TecService;
using System.Collections;
using System.Net;

namespace Neusoft.SOC.HISFC.InpatientFee.Components.Balance
{
    //{962111D3-7FAB-470b-8941-1A6D16FEEFD2}
    public partial class frmEleInvoice : Form
    {

        #region 变量

        private decimal ybtotalprice;
        private DataSet ybDs = null;

        private string pid = "";
        private string balanceoper = "";
        private string hospitalId = "";

        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
        FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        public frmEleInvoice()
        {
            InitializeComponent();
        }


        private void frmEleInvoice_Load(object sender, EventArgs e)
        {
            this.neuComboBox6.AddItems(constantMgr.GetList("NuoNuoBillingNewType"));

            this.neuComboBox6.Tag = (constantMgr.GetList("NuoNuoBillingNewType").ToArray()[0] as FS.HISFC.Models.Base.Const).ID;
        }

        public void SetPatientInfo(PatientInfo patientinfo)
        {

            this.patientInfo = patientinfo;

            if (patientInfo == null)
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                return;
            }

            string inpatientno = patientInfo.ID.ToString();
            this.QueryInvoiceNoByYB(inpatientno);
            this.QueryInvoiceNoByPTFP(inpatientno);

            if (patientInfo != null)
            {
                string id = patientInfo.ID;
                pid = patientinfo.ID;

                string sql = @"select BALANCE_OPERCODE from fin_ipb_balancelist where INPATIENT_NO='{0}' and rownum<=1";
                sql=string.Format(sql,pid);
                DataSet ds=new DataSet();
                inpatientFeeManager.ExecQuery(sql, ref ds);
                if (ds != null) 
                {
                    balanceoper = CommonController.CreateInstance().GetEmployeeName(ds.Tables[0].Rows[0]["BALANCE_OPERCODE"].ToString());
                }
            }


            // 姓名
            this.txtName.Text = patientInfo.Name;
            //手机号
            //{68ABD454-1708-4922-B265-1B715F66C7EF}电子发票优化
            this.textBox2.Text = "";// patientInfo.PhoneHome;
            // 科室
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            // 合同单位
            this.txtPact.Text = patientInfo.Pact.Name;

            //所属病区
            txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;

            // 医生
            txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;

            string hospitalname = FS.FrameWork.Management.Connection.Hospital.Name;
            string year = DateTime.Now.Year.ToString();
            if (hospitalname.Contains("广州"))
            {
                hospitalId = "IBORNGZ";
                this.richTextBox1.Text = "住院日期：" + year + "年月日至" + year + "年月日\r\n医保类型：在职人员，医保编号：，医保统筹基金支付：元，其他支付：0元，个人账户支付：0元，个人现金支付：0元，个人自付：0元，个人自费：元。";
            }
            else if (hospitalname.Contains("顺德"))
            {
                hospitalId = "IBORNSD";
                this.richTextBox1.Text = "入院日期:" + year + "年 月 日\r\n" +
                                        "分娩日期:" + year + "年 月 日\r\n" +
                                        "出院日期:" + year + "年 月 日\r\n" +
                                        "总费用:\r\n" +
                                        "基金支付:\r\n" +
                                        "个人支付:";
            }

        }

        public void HanTianinrequestBillingNew(string req, string invoiceno)
        {
            string reqxml = req;
            string method = "HanTianinrequestBillingNewNew";
            if (hospitalId == "IBORNSD")
            {
                method = "HanTianOpeMplatformrequestBillingNew";
            }
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(req, "HanTianInvoice", method);
            MessageBox.Show(resultXml);

            //开票成功=》查询开票信息

            string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + resultXml + "</serialNos><invoiceno>" + invoiceno + "</invoiceno><reqxml>" + reqxml + "</reqxml><hospitalid>"+hospitalId+"</hospitalid></req>";

            resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(reqbyInvoicResult, "HanTianInvoice", "HanTianQueryInvoiceResult");

            insertHanTianQueryInvoiceResult(resultXml, invoiceno, reqxml);

            MessageBox.Show(resultXml);
        }

        public int insertHanTianQueryInvoiceResult(string result, string invoiceno, string reqxml)
        {
            Bussiness.Util.JsonResult jsonData = Bussiness.Util.JsonUntity.DeSerializeEntity<Bussiness.Util.JsonResult>(result);
            string code = jsonData.code.ToString();
            string b = jsonData.describe.ToString();
            string c = jsonData.result[0].invoiceCode.ToString();
            string d = jsonData.result[0].invoiceNo.ToString();
            string opertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string insertEleInvoiceInfo = @"insert into com_opb_eleinvoiceopeninfo values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'
                                                                    ,'{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}',
                                                                     '{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}',
                                                                      '{43}','{44}','{45}','{46}','{47}',to_date('{48}','yyyy-MM-dd HH24:mi:ss'),'{49}','{50}'
                                                                     )";

            string sql = string.Format(insertEleInvoiceInfo, invoiceno, jsonData.code, jsonData.describe
                                             , jsonData.result[0].address, jsonData.result[0].bankAccount, jsonData.result[0].checker, jsonData.result[0].clerk, jsonData.result[0].clerkId
                                             , jsonData.result[0].deptId, jsonData.result[0].imgUrls, jsonData.result[0].listFlag, jsonData.result[0].listName, jsonData.result[0].notifyEmail
                                             , jsonData.result[0].ofdUrl, jsonData.result[0].oldInvoiceCode, jsonData.result[0].oldInvoiceNo, jsonData.result[0].orderAmount, jsonData.result[0].payee
                                             , jsonData.result[0].phone, jsonData.result[0].productOilFlag, jsonData.result[0].remark, jsonData.result[0].saleName, jsonData.result[0].salerAccount
                                             , jsonData.result[0].salerAddress, jsonData.result[0].salerTaxNum, jsonData.result[0].salerTel, jsonData.result[0].telephone, jsonData.result[0].terminalNumber
                                             , jsonData.result[0].serialNo, jsonData.result[0].orderNo, jsonData.result[0].status, jsonData.result[0].statusMsg, jsonData.result[0].failCause
                                             , jsonData.result[0].pdfUrl, jsonData.result[0].pictureUrl, jsonData.result[0].invoiceTime, jsonData.result[0].invoiceCode, jsonData.result[0].invoiceNo
                                             , jsonData.result[0].exTaxAmount, jsonData.result[0].taxAmount, jsonData.result[0].payerName, jsonData.result[0].payerTaxNo, jsonData.result[0].invoiceKind
                                             , jsonData.result[0].checkCode, jsonData.result[0].qrCode, jsonData.result[0].machineCode, jsonData.result[0].cipherText, jsonData.result[0].invoiceSerialNum
                                             , opertime, reqxml,this.patientInfo.ID.ToString()
                );

            sql = sql.Replace("&gt;", "").Replace("&lt;", "");

            inpatientFeeManager.ExecNoQuery(sql);

            return 1;
        }

        public DataSet QueryInvoiceNoByYB(string inpatientno)
        {
            string sql = @" select t.mdtrt_id,t.setl_id,t.invoice_no,t.* from fin_ipr_siinmaininfo t
                        where t.type_code = '2'   --1门诊 2住院
                        and t.valid_flag = '1'
                        and t.balance_state = '1'
                        --and t.oper_date > trunc(sysdate) - 7
                        --门诊：and t.invoice_no = '210514870004'
                        and t.INPATIENT_NO = '{0}'";

            sql = string.Format(sql, inpatientno);
            DataSet ds = new DataSet();

            int result = inpatientFeeManager.ExecQuery(sql, ref ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string mdtrt_id = dt.Rows[0]["mdtrt_id"].ToString();
                string setl_id = dt.Rows[0]["setl_id"].ToString();

                string hospitalname = FS.FrameWork.Management.Connection.Hospital.Name;
                //if (hospitalname.Contains("广州"))
                //    hospitalId = "IBORNGZ";
                //else if (hospitalname.Contains("顺德"))
                //    hospitalId = "IBORNSD";

                //                if (hospitalname.Contains("广州"))
                //                {
                //                    sql = @"  select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
                //                        sum(t.det_item_fee_sumamt) feetype
                //                        from gzsi_feedetail t  
                //                        left join gzsi_his_mzxm t2 on substr(t.feedetl_sn,1,17) = t2.xmxh and t.mdtrt_id = t2.jydjh
                //                        left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code and t3.report_code = 'SIMZ01'
                //                        where t.mdtrt_id = '{0}' and t.setl_id = '{1}' and t.memo is null
                //                        group by t3.fee_stat_name";

                //                    sql = string.Format(sql, mdtrt_id, setl_id);
                //                }
                //                else if (hospitalname.Contains("顺德"))
                //                {
                sql = @"select t.amt feetype,
                        fun_get_dictionary('GZSI_med_chrgitm_type',t.med_chrgitm) fee_stat_name 
                        from v_jsqd_iteminfo t where t.mdtrt_id='{0}'";

                sql = string.Format(sql, mdtrt_id);
                //}

                DataSet ybfeeds = new DataSet();

                result = inpatientFeeManager.ExecQuery(sql, ref ybfeeds);

                if (ybfeeds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    decimal totalprice = 0.00m;
                    DataTable ybfeedt = ybfeeds.Tables[0];
                    for (int i = 0; i < ybfeedt.Rows.Count; i++)
                    {
                        //this.fpEleYBInvoice_Sheet1.Rows.Count = 0;
                        this.fpEleYBInvoice_Sheet1.Rows.Add(this.fpEleYBInvoice_Sheet1.Rows.Count, 1);
                        //添加结算明细
                        this.fpEleYBInvoice_Sheet1.Cells[i, 0].Value = ybfeedt.Rows[i]["fee_stat_name"].ToString();
                        this.fpEleYBInvoice_Sheet1.Cells[i, 1].Value = ybfeedt.Rows[i]["feetype"].ToString();
                        count = i;
                        totalprice += Convert.ToDecimal(ybfeedt.Rows[i]["feetype"]);
                    }
                    this.fpEleYBInvoice_Sheet1.Cells[count + 1, 0].Value = "总费用";
                    this.fpEleYBInvoice_Sheet1.Cells[count + 1, 1].Value = totalprice;

                    this.ybtotalprice = totalprice;

                    ybDs = ybfeeds;

                    return ybfeeds;
                }

            }
            else
            {
                #region 自费患者根据医保目录统计报销金额

                #region 自费患者可报销金额查询SQL
                sql = @"select sum(feetype) feetype,fee_stat_name from (
select
round((dr.unit_price2 * ls.qty),2) feetype,
case 
  when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%材料费%' then '材料费' 
    when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%检验费%' then '检验费' 
      when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%护理费%' then '护理费' 
        when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%检查费%' then '检查费' 
          when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%手术费%' then '手术费' 
            when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%麻醉费%' then '麻醉费' 
              when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%诊查费%' then '诊查费'
                when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%床位费%' then '床位费' 
                  when (fun_get_dictionary('MINFEE',ls.fee_code)) like '%治疗费%' then '治疗费'  
  else '其他费' end
   fee_stat_name 
from fin_ipb_itemlist ls 
inner join fin_com_undruginfo dr on ls.item_code = dr.item_code 
inner join fin_com_compare cp on dr.item_code = cp.his_code and cp.pact_code = '4'
where 1=1
and ls.recipe_no in (select ff.recipe_no from FIN_IPB_FEEINFO ff where ff.inpatient_no='{0}')
)
group by fee_stat_name
union all
select sum(feetype) feetype,fee_stat_name from (
select    
(ll.own_cost - ll.eco_cost - ll.donate_cost)  feetype,
case when fun_get_dictionary('MINFEE',ll.fee_code) = '中草药费' then '中药费' 
	else fun_get_dictionary('MINFEE',ll.fee_code) end fee_stat_name 
from fin_ipb_medicinelist ll 
inner join fin_com_compare cp on ll.drug_code = cp.his_code and cp.pact_code = '4'
where 1=1
and ll.recipe_no in (select ff.recipe_no from FIN_IPB_FEEINFO ff where ff.inpatient_no='{0}')
)
group by fee_stat_name
";
                #endregion 

                sql = string.Format(sql, inpatientno);
                DataSet zfybfeeds = new DataSet();
                result = inpatientFeeManager.ExecQuery(sql, ref zfybfeeds);

                if (zfybfeeds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    decimal totalprice = 0.00m;
                    DataTable zfybfeedt = zfybfeeds.Tables[0];
                    for (int i = 0; i < zfybfeedt.Rows.Count; i++)
                    {
                        this.fpEleYBInvoice_Sheet1.Rows.Add(this.fpEleYBInvoice_Sheet1.Rows.Count, 1);
                        //添加结算明细
                        this.fpEleYBInvoice_Sheet1.Cells[i, 0].Value = zfybfeedt.Rows[i]["fee_stat_name"].ToString();
                        this.fpEleYBInvoice_Sheet1.Cells[i, 1].Value = zfybfeedt.Rows[i]["feetype"].ToString();
                        count = i;
                        totalprice += Convert.ToDecimal(zfybfeedt.Rows[i]["feetype"]);
                    }
                    this.fpEleYBInvoice_Sheet1.Cells[count + 1, 0].Value = "总费用";
                    this.fpEleYBInvoice_Sheet1.Cells[count + 1, 1].Value = totalprice;

                    this.ybtotalprice = totalprice;

                    ybDs = zfybfeeds;
                    return zfybfeeds;
                }

                #endregion
            }

            return null;

        }

        public DataSet QueryInvoiceNoByPTFP(string invoiceno)
        {
            string sql = @" select t.name,sum(t.tot_cost)-sum(t.eco_cost+t.donate_cost) totalprice,t.stat_code,t.stat_name from fin_ipb_balancelist t
                         where t.INPATIENT_NO='{0}' group by t.name,t.stat_code,t.stat_name ";

            sql = string.Format(sql, invoiceno);
            DataSet ds = new DataSet();

            int result = inpatientFeeManager.ExecQuery(sql, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                decimal totalprice = 0.00m;
                DataTable ptfeedt = ds.Tables[0];
                if (this.ybtotalprice > 0)    //有医保费用
                {
                    decimal pttotalprice = 0.00m;

                    for (int i = 0; i < ptfeedt.Rows.Count; i++)
                    {
                        pttotalprice += Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]);
                    }

                    for (int i = 0; i < ptfeedt.Rows.Count; i++)
                    {
                        this.fpElePTInvoice_Sheet1.Rows.Add(this.fpElePTInvoice_Sheet1.Rows.Count, 1);

                        FarPoint.Win.Spread.CellType.CheckBoxCellType cbct = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                        this.fpElePTInvoice_Sheet1.Cells[i, 0].CellType = cbct;
                        this.fpElePTInvoice_Sheet1.Cells[i, 0].Value = false;

                        this.fpElePTInvoice_Sheet1.Cells[i, 1].Value = ptfeedt.Rows[i]["stat_name"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 2].Value = ptfeedt.Rows[i]["totalprice"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 3].Value = Math.Round((pttotalprice - ybtotalprice) * (Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]) / pttotalprice), 2); //开完医保发票后 开普通发票金额的比例
                        count = i;
                        totalprice += (pttotalprice - ybtotalprice) * (Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]) / pttotalprice);
                    }

                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 1].Value = "总费用";
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 2].Value = pttotalprice;
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 3].Value = totalprice;
                }
                else
                {
                    decimal pttotalprice = 0.00m;

                    for (int i = 0; i < ptfeedt.Rows.Count; i++)
                    {
                        pttotalprice += Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]);
                    }

                    for (int i = 0; i < ptfeedt.Rows.Count; i++)
                    {
                        this.fpElePTInvoice_Sheet1.Rows.Add(this.fpElePTInvoice_Sheet1.Rows.Count, 1);

                        FarPoint.Win.Spread.CellType.CheckBoxCellType cbct = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                        this.fpElePTInvoice_Sheet1.Cells[i, 0].CellType = cbct;
                        this.fpElePTInvoice_Sheet1.Cells[i, 0].Value = false;

                        this.fpElePTInvoice_Sheet1.Cells[i, 1].Value = ptfeedt.Rows[i]["stat_name"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 2].Value = ptfeedt.Rows[i]["totalprice"].ToString();
                        //this.fpElePTInvoice_Sheet1.Cells[i, 3].Value = ptfeedt.Rows[i]["totalprice"].ToString();

                        this.fpElePTInvoice_Sheet1.Cells[i, 3].Value = Math.Round((pttotalprice - ybtotalprice) * (Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]) / pttotalprice), 2); //开完医保发票后 开普通发票金额的比例
                        totalprice += (pttotalprice - ybtotalprice) * (Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]) / pttotalprice);
                        //pttotalprice += Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]);
                        count = i;
                    }
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 1].Value = "总费用";
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 2].Value = pttotalprice;// totalprice;
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 3].Value = totalprice;// pttotalprice;

                }


                return ds;
            }

            return null;

        }

        private void ybbtn_Click(object sender, EventArgs e)
        {
            if (ybDs != null)
            {
                //开票类型 电票或纸票
                //string opentype = this.comboBox1.Text.ToString() == "电票" ? "p" : "c";

                string opentype = this.neuComboBox6.Tag.ToString();

                if (opentype == "c" || opentype == "ec")
                {
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                }

                //推送方式 -1 不推送 0邮箱 1手机 2邮箱手机
                string pushMode = "-1";
                if (this.textBox1.Text.ToString() != "" && this.textBox2.Text != "")
                    pushMode = "2";
                else if (this.textBox1.Text.ToString() != "" && this.textBox2.Text == "")
                    pushMode = "0";
                else if (this.textBox1.Text.ToString() == "" && this.textBox2.Text != "")
                    pushMode = "1";

                string req = "<checker>{0}</checker><payee>{1}</payee><email>{2}</email><orderNo>{3}</orderNo><buyerName>{4}</buyerName><clerk>{5}</clerk><invoiceType>1</invoiceType><buyerTaxNum>{6}</buyerTaxNum><buyerTel>{7}</buyerTel><buyerAddress>{8}</buyerAddress><buyerAccount>{9}</buyerAccount><buyerPhone>{10}</buyerPhone><hospitalid>{11}</hospitalid><openType>{12}</openType><remark>{13}</remark><pushMode>{14}</pushMode>";
                string feeList = "";
                string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount></feeItem>";

                string orderno = System.Guid.NewGuid().ToString("N").Substring(0, 20);

                string clerk = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
                string checker = "夏涵";
                string payee = balanceoper;

                string name = "吴测试";

                string invoiceno = pid;
                string patientno = patientInfo.PID.PatientNO.ToString();


                bool check = false;

                Hashtable hashIn = new Hashtable();


                hashIn["姓名"] = patientInfo.Name;
                hashIn["手机号"] = patientInfo.PhoneHome;

                if (ybDs != null)
                {
                    DataTable ybdt = ybDs.Tables[0];

                    for (int i = 0; i < ybdt.Rows.Count; i++)
                    {
                        string totalPrice = ybdt.Rows[i]["feetype"].ToString();
                        string goodsName = ybdt.Rows[i]["fee_stat_name"].ToString();

                        hashIn.Add(goodsName, totalPrice);

                        feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice);
                    }

                    req = string.Format(req, checker, payee, this.textBox1.Text.Trim().ToString(), orderno, this.txtName.Text.ToString(), clerk, this.neuTextBox1.Text, this.neuTextBox4.Text, this.neuTextBox4.Text, this.neuTextBox3.Text, this.textBox2.Text, hospitalId, opentype, this.richTextBox1.Text, pushMode);
                    hashIn["req"] = req;

                    frmOpenEleInvoiceInfo openEleInvoiceInfo = new frmOpenEleInvoiceInfo();

                    openEleInvoiceInfo.SetOpenEleInfo(hashIn);
                    openEleInvoiceInfo.hanTianinrequestBillingNew = HanTianinrequestBillingNew;
                    openEleInvoiceInfo.StartPosition = FormStartPosition.CenterScreen;
                    openEleInvoiceInfo.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("该患者没有医保费用！");
            }
        }

        private void ptbtn_Click(object sender, EventArgs e)
        {

            //开票类型 电票或纸票
            //string opentype = this.comboBox1.Text.ToString() == "电票" ? "p" : "c";


            string opentype = this.neuComboBox6.Tag.ToString();

            if (opentype == "c" || opentype == "ec")
            {
                this.textBox1.Text = "";
                this.textBox2.Text = "";
            }

            //推送方式 -1 不推送 0邮箱 1手机 2邮箱手机
            string pushMode = "-1";
            if (this.textBox1.Text.ToString() != "" && this.textBox2.Text != "")
                pushMode = "2";
            else if (this.textBox1.Text.ToString() != "" && this.textBox2.Text == "")
                pushMode = "0";
            else if (this.textBox1.Text.ToString() == "" && this.textBox2.Text != "")
                pushMode = "1";

            string req = "<checker>{0}</checker><payee>{1}</payee><email>{2}</email><orderNo>{3}</orderNo><buyerName>{4}</buyerName><clerk>{5}</clerk><invoiceType>1</invoiceType><buyerTaxNum>{6}</buyerTaxNum><buyerTel>{7}</buyerTel><buyerAddress>{8}</buyerAddress><buyerAccount>{9}</buyerAccount><buyerPhone>{10}</buyerPhone><hospitalid>{11}</hospitalid><openType>{12}</openType><remark>{13}</remark><pushMode>{14}</pushMode>";
            string feeList = "";
            string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount></feeItem>";

            string orderno = System.Guid.NewGuid().ToString("N").Substring(0,20);

            string clerk = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
            string checker = "夏涵";
            string payee = balanceoper;

            string name = "吴测试";

            string invoiceno = pid;
            string patientno = patientInfo.PID.PatientNO.ToString();


            bool check = false;

            for (int i = 0; i < fpElePTInvoice.Sheets[0].Rows.Count - 1; i++)
            {
                if ((bool)fpElePTInvoice.Sheets[0].Cells[i, 0].Value)
                {
                    check = true;
                    break;
                }
            }

            Hashtable hashIn = new Hashtable();
            hashIn["姓名"] = this.txtName.Text;
            hashIn["手机号"] = patientInfo.PhoneHome;

            if (check == true)
            {
                for (int i = 0; i < fpElePTInvoice.Sheets[0].Rows.Count - 1; i++)
                {
                    if ((bool)fpElePTInvoice.Sheets[0].Cells[i, 0].Value)
                    {
                        string totalprice = fpElePTInvoice_Sheet1.Cells[i, 3].Value.ToString();
                        string goodsname = fpElePTInvoice_Sheet1.Cells[i, 1].Value.ToString();

                        hashIn.Add(goodsname,totalprice);


                        feeList += string.Format(feeItem, totalprice, "1", totalprice, goodsname, totalprice);
                    }
                }

                req = string.Format(req, checker, payee, this.textBox1.Text.Trim().ToString(), orderno, this.txtName.Text.ToString(), clerk, this.neuTextBox1.Text, this.neuTextBox4.Text, this.neuTextBox4.Text, this.neuTextBox3.Text, this.textBox2.Text, hospitalId, opentype, this.richTextBox1.Text, pushMode);
                hashIn["req"] = req;

                frmOpenEleInvoiceInfo openEleInvoiceInfo = new frmOpenEleInvoiceInfo();
                
                openEleInvoiceInfo.SetOpenEleInfo(hashIn);
                openEleInvoiceInfo.hanTianinrequestBillingNew = HanTianinrequestBillingNew;
                openEleInvoiceInfo.StartPosition = FormStartPosition.CenterScreen;
                openEleInvoiceInfo.ShowDialog();

            }
            else
            {
                MessageBox.Show("请选择要开发票项目！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.34.10:8082/IbornCrmService.asmx";  //"http://localhost:8081/IbornCrmService.asmx";//
            string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>21061018004603965185</serialNos><invoiceno>yb</invoiceno><reqxml><req><feeList><feeItem><taxExcludedAmount>171.45</taxExcludedAmount><num>1</num><price>171.45</price><goodsName>检查费</goodsName><taxIncludedAmount>171.45</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>58.50</taxExcludedAmount><num>1</num><price>58.50</price><goodsName>中成药费</goodsName><taxIncludedAmount>58.50</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>538.20</taxExcludedAmount><num>1</num><price>538.20</price><goodsName>化验费</goodsName><taxIncludedAmount>538.20</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>318.38</taxExcludedAmount><num>1</num><price>318.38</price><goodsName>西药费</goodsName><taxIncludedAmount>318.38</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>1201.50</taxExcludedAmount><num>1</num><price>1201.50</price><goodsName>手术费</goodsName><taxIncludedAmount>1201.50</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>291.60</taxExcludedAmount><num>1</num><price>291.60</price><goodsName>床位费</goodsName><taxIncludedAmount>291.60</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>8.10</taxExcludedAmount><num>1</num><price>8.10</price><goodsName>诊察费</goodsName><taxIncludedAmount>8.10</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>591.30</taxExcludedAmount><num>1</num><price>591.30</price><goodsName>治疗费</goodsName><taxIncludedAmount>591.30</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>468.41</taxExcludedAmount><num>1</num><price>468.41</price><goodsName>卫生材料费</goodsName><taxIncludedAmount>468.41</taxIncludedAmount></feeItem><feeItem><taxExcludedAmount>204.75</taxExcludedAmount><num>1</num><price>204.75</price><goodsName>护理费</goodsName><taxIncludedAmount>204.75</taxIncludedAmount></feeItem></feeList><checker>夏涵</checker><payee>杨胜男</payee><email>1152986822@qq.com</email><orderNo>yb61727</orderNo><buyerName>焦婧</buyerName><clerk>信息科</clerk><invoiceType>1</invoiceType></req></reqxml></req>";
            string reqxml = "111";
            string resultXml = WSHelper.InvokeWebService(url, "HanTianQueryInvoiceResult", new string[] { reqbyInvoicResult }) as string;

            insertHanTianQueryInvoiceResult(resultXml, "1111", reqxml);

            MessageBox.Show(resultXml);
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

       
    }
}
