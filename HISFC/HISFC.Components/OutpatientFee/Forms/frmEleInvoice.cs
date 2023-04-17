using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TecService;
using System.Collections;


namespace FS.HISFC.Components.OutpatientFee.Forms
{
    public partial class frmEleInvoice : Form
    {

        #region 变量

        private decimal ybtotalprice;
        private DataSet ybDs = null;

        private string hospitalId = "";

        private string pid = "";
        private string balanceoper = "";

        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();


        #endregion

        public frmEleInvoice()
        {
            InitializeComponent();
        }

        public frmEleInvoice(string invoiceno)
        {
            InitializeComponent();


            this.QueryInvoiceNoByYB(invoiceno);
            this.QueryInvoiceNoByPTFP(invoiceno);
        }

        private void frmEleInvoice_Load(object sender, EventArgs e)
        {
            this.neuComboBox6.AddItems(constantMgr.GetList("NuoNuoBillingNewType"));

            this.neuComboBox6.Tag = (constantMgr.GetList("NuoNuoBillingNewType").ToArray()[0] as FS.HISFC.Models.Base.Const).ID;
        }

        public void SetPatientInfo(FS.HISFC.Models.Fee.Outpatient.Balance currentBalance)
        {
            if (currentBalance == null)
            {
                return;
            }

            this.tbInvoiceNo.Text = currentBalance.Invoice.ID;
            this.tbPName.Text = currentBalance.Patient.Name;
            this.textBox2.Text = "";

            //this.neuTextBox3.Text = FS.FrameWork.Management.Connection.Hospital.Name;

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(currentBalance.BalanceOper.ID);
            if (employee == null)
            {
                MessageBox.Show("获得当前发票操作员信息失败");
                return;
            }

            this.tbOperName.Text = employee.Name;
            this.tbPactInfo.Text = currentBalance.Patient.Pact.Name;
            this.tbInvoiceDate.Text = currentBalance.BalanceOper.OperTime.ToString();

            string invoiceno = currentBalance.Invoice.ID;
            this.QueryInvoiceNoByYB(invoiceno);
            this.QueryInvoiceNoByPTFP(invoiceno);

            string hospitalname = FS.FrameWork.Management.Connection.Hospital.Name;
            if (hospitalname.Contains("广州"))
            {
                hospitalId = "IBORNGZ";
            }
            else if (hospitalname.Contains("顺德"))
            {
                hospitalId = "IBORNSD";
                this.richTextBox1.Text = "就诊日期" + DateTime.Now.Year + "年 月 日"; 
            }
            
        }


        private void ptbtn_Click(object sender, EventArgs e)
        {
            //开票类型 电票或纸票
            //string opentype = this.neuComboBox6.Text.ToString() == "电票" ? "p" : "c";

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
            string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount><goodsCode>{5}</goodsCode></feeItem>";

            string orderno = System.Guid.NewGuid().ToString("N").Substring(0, 20);
            string clerk = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
            string checker = "夏涵";
            string payee = this.tbOperName.Text.ToString();
            string name = "吴测试";
            string invoiceno = this.tbInvoiceNo.Text.ToString();

            Hashtable hashIn = new Hashtable();
            hashIn["姓名"] = this.tbPName.Text;
            hashIn["手机号"] = this.textBox2.Text;

            bool check = false;

            for (int i = 0; i < fpElePTInvoice.Sheets[0].Rows.Count - 1; i++)
            {
                if ((bool)fpElePTInvoice.Sheets[0].Cells[i, 0].Value)
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                for (int i = 0; i < fpElePTInvoice.Sheets[0].Rows.Count - 1; i++)
                {
                    if ((bool)fpElePTInvoice.Sheets[0].Cells[i, 0].Value)
                    {
                        string totalprice = fpElePTInvoice_Sheet1.Cells[i, 3].Value.ToString();
                        string goodsname = fpElePTInvoice_Sheet1.Cells[i, 1].Value.ToString();

                        string goodcode = fpElePTInvoice_Sheet1.Cells[i, 4].Value.ToString();

                        hashIn.Add(goodsname, totalprice);

                        feeList += string.Format(feeItem, totalprice, "1", totalprice, goodsname, totalprice, goodcode);
                    }
                }

                req = string.Format(req, checker, payee, this.textBox1.Text.Trim().ToString(), orderno, this.tbPName.Text.ToString(), clerk, this.neuTextBox1.Text, this.neuTextBox4.Text, this.neuTextBox4.Text, this.neuTextBox3.Text, this.textBox2.Text, hospitalId, opentype, this.richTextBox1.Text, pushMode);
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

        private void ybbtn_Click(object sender, EventArgs e)
        {
            if (ybDs != null)
            {
                //开票类型 电票或纸票

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
                string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount><goodsCode>{5}</goodsCode></feeItem>";

                string orderno = System.Guid.NewGuid().ToString("N").Substring(0, 20);

                string clerk = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
                string checker = "夏涵";
                string payee = this.tbOperName.Text.ToString();// balanceoper;
                string name = "吴测试";
                string invoiceno = this.tbInvoiceNo.Text.ToString();

                Hashtable hashIn = new Hashtable();
                hashIn["姓名"] = this.tbPName.Text;
                hashIn["手机号"] = this.textBox2.Text;

                if (ybDs != null)
                {
                    DataTable ybdt = ybDs.Tables[0];

                    for (int i = 0; i < ybdt.Rows.Count; i++)
                    {
                        string totalPrice = ybdt.Rows[i]["feetype"].ToString();
                        string goodsName = ybdt.Rows[i]["fee_stat_name"].ToString();
                        string goodsCode = ybdt.Rows[i]["med_chrgitm"].ToString();

                        hashIn.Add(goodsName, totalPrice);

                        feeList += string.Format(feeItem, totalPrice.ToString(), "1", totalPrice, goodsName, totalPrice, goodsCode);
                    }

                    req = string.Format(req, checker, payee, this.textBox1.Text.Trim().ToString(), orderno, this.tbPName.Text.ToString(), clerk, this.neuTextBox1.Text, this.neuTextBox4.Text, this.neuTextBox4.Text, this.neuTextBox3.Text, this.textBox2.Text, hospitalId, opentype, this.richTextBox1.Text, pushMode);
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

        public DataSet QueryInvoiceNoByYB(string invoiceno)
        {


            string sql = @" select t.mdtrt_id,t.setl_id,t.* from fin_ipr_siinmaininfo t
                        where t.type_code = '1'  --1 门诊 2住院
                        and t.valid_flag = '1'
                        and t.balance_state = '1'
                        --and t.oper_date > trunc(sysdate) - 7
                        and t.invoice_no ='{0}'";

            sql = string.Format(sql, invoiceno);
            DataSet ds = new DataSet();

            int result = outpatientManager.ExecQuery(sql, ref ds);

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
                        fun_get_dictionary('GZSI_med_chrgitm_type',t.med_chrgitm) fee_stat_name,t.med_chrgitm 
                        from v_jsqd_iteminfo t where t.mdtrt_id='{0}'";

                sql = string.Format(sql, mdtrt_id);
                //}

                DataSet ybfeeds = new DataSet();

                result = outpatientManager.ExecQuery(sql, ref ybfeeds);

                if (ybfeeds.Tables[0].Rows.Count > 0)
                {
                    int count = 0;
                    decimal totalprice = 0.00m;
                    DataTable ybfeedt = ybfeeds.Tables[0];
                    for (int i = 0; i < ybfeedt.Rows.Count; i++)
                    {
                        this.fpEleYBInvoice_Sheet1.Rows.Add(this.fpEleYBInvoice_Sheet1.Rows.Count, 1);
                        //添加结算明细
                        this.fpEleYBInvoice_Sheet1.Cells[i, 0].Value = ybfeedt.Rows[i]["fee_stat_name"].ToString();
                        this.fpEleYBInvoice_Sheet1.Cells[i, 1].Value = ybfeedt.Rows[i]["feetype"].ToString();
                        this.fpEleYBInvoice_Sheet1.Cells[i, 1].Value = ybfeedt.Rows[i]["med_chrgitm"].ToString();

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
            return null;
        }

        public DataSet QueryInvoiceNoByPTFP(string invoiceno)
        {
            string sql = @" select t.invo_name,nvl(sum(t1.own_cost)-sum(t1.eco_cost+t1.donate_cost),0) totalprice,t.invo_code  from fin_opb_invoicedetail t 
                        left join fin_opb_feedetail t1 on t.invoice_no=t1.invoice_no and t.invo_sequence=t1.invo_sequence 
                        where t.invoice_no='{0}' group by t.invo_name,t.invo_code";

            sql = string.Format(sql, invoiceno);
            DataSet ds = new DataSet();

            int result = outpatientManager.ExecQuery(sql, ref ds);

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

                        this.fpElePTInvoice_Sheet1.Cells[i, 1].Value = ptfeedt.Rows[i]["invo_name"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 2].Value = ptfeedt.Rows[i]["totalprice"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 3].Value = pttotalprice == 0 ? 0 : Math.Round((pttotalprice - ybtotalprice) * (Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]) / pttotalprice), 2); //开完医保发票后 开普通发票金额的比例
                        this.fpElePTInvoice_Sheet1.Cells[i, 4].Value = ptfeedt.Rows[i]["invo_code"].ToString();
                        count = i;
                        totalprice += pttotalprice == 0 ? 0 : (pttotalprice - ybtotalprice) * (Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]) / pttotalprice);
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
                        this.fpElePTInvoice_Sheet1.Rows.Add(this.fpElePTInvoice_Sheet1.Rows.Count, 1);

                        FarPoint.Win.Spread.CellType.CheckBoxCellType cbct = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                        this.fpElePTInvoice_Sheet1.Cells[i, 0].CellType = cbct;
                        this.fpElePTInvoice_Sheet1.Cells[i, 0].Value = false;

                        this.fpElePTInvoice_Sheet1.Cells[i, 1].Value = ptfeedt.Rows[i]["invo_name"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 2].Value = ptfeedt.Rows[i]["totalprice"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 3].Value = ptfeedt.Rows[i]["totalprice"].ToString();
                        this.fpElePTInvoice_Sheet1.Cells[i, 4].Value = ptfeedt.Rows[i]["invo_code"].ToString();

                        pttotalprice += Convert.ToDecimal(ptfeedt.Rows[i]["totalprice"]);
                        count = i;
                    }
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 1].Value = "总费用";
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 2].Value = pttotalprice;// totalprice;
                    this.fpElePTInvoice_Sheet1.Cells[count + 1, 3].Value = pttotalprice;// totalprice;
                }
                return ds;
            }
            return null;
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

            string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + resultXml + "</serialNos><invoiceno>" + invoiceno + "</invoiceno><reqxml>" + reqxml + "</reqxml><hospitalid>" + hospitalId + "</hospitalid></req>";

            
            resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(reqbyInvoicResult, "HanTianInvoice", "HanTianQueryInvoiceResult");


            insertHanTianQueryInvoiceResult(resultXml, invoiceno, reqxml);

            MessageBox.Show(resultXml);
        }

        public int insertHanTianQueryInvoiceResult(string result, string invoiceno, string reqxml)
        {
            Bussiness.Util.JsonResult jsonData = FS.HISFC.Components.OutpatientFee.JsonUntity.DeSerializeEntity<Bussiness.Util.JsonResult>(result);
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
                                             , opertime, reqxml, this.tbInvoiceNo.Text

                );
            sql = sql.Replace("&gt;", "").Replace("&lt;", "");
            int i = outpatientManager.ExecNoQuery(sql);

            return 1;
        }



    }
}
