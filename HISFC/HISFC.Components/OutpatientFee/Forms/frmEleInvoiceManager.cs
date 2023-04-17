using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TecService;


namespace FS.HISFC.Components.OutpatientFee.Forms
{
    public partial class frmEleInvoiceManager : Form
    {
        #region 变量

        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        private string hospitalId = "";

        #endregion

        public frmEleInvoiceManager()
        {
            InitializeComponent();

            this.fpEleInvoice.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpEleInvoiceInfo_ButtonClicked);
            SetEleInvoiceList();
        }

        protected void SetEleInvoiceList()
        {
            this.fpEleInvoice_sheetView1.RowCount = 0;

            //获取结算明细信息

            string sql = @"select t.createtime 创建时间,t.orderno 订单号,t.notifyemail 邮箱地址,t.invoicecode 发票代码,t.invoiceno 发票号码,
                    t.payername 发票抬头,t.extaxamount 含税金额,t.statusmsg 开票状态,t.serialno 序列号,reqxml from com_opb_eleinvoiceopeninfo t order by t.createtime desc";

            DataSet ds = new DataSet();
            outpatientManager.ExecQuery(sql, ref ds);

            if (ds.Tables[0].Rows.Count < 0)
            {
                return;
            }

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.fpEleInvoice_sheetView1.Rows.Add(this.fpEleInvoice_sheetView1.Rows.Count, 1);
                //添加结算明细
                this.fpEleInvoice_sheetView1.Cells[i, 0].Value = dt.Rows[i]["创建时间"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 1].Value = dt.Rows[i]["订单号"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 2].Value = dt.Rows[i]["邮箱地址"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 3].Value = dt.Rows[i]["发票代码"].ToString() + "/" + dt.Rows[i]["发票号码"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 4].Value = dt.Rows[i]["发票抬头"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 5].Value = dt.Rows[i]["含税金额"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 6].Value = dt.Rows[i]["开票状态"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 7].Value = dt.Rows[i]["序列号"].ToString();

                FarPoint.Win.Spread.CellType.ButtonCellType bct = new FarPoint.Win.Spread.CellType.ButtonCellType();
                bct.Text = "冲红";
                this.fpEleInvoice_sheetView1.Cells[i, 8].CellType = bct;

                FarPoint.Win.Spread.CellType.ButtonCellType bct1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
                bct1.Text = "重新获取";

                this.fpEleInvoice_sheetView1.Cells[i, 9].CellType = bct1;
                this.fpEleInvoice_sheetView1.Rows[i].Tag = dt.Rows[i]["reqxml"].ToString(); ;
            }

            string hospitalname = FS.FrameWork.Management.Connection.Hospital.Name;
            if (hospitalname.Contains("广州"))
                hospitalId = "IBORNGZ";
            else if (hospitalname.Contains("顺德"))
                hospitalId = "IBORNSD";

            return;
        }

        //冲红发票
        void fpEleInvoiceInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int a = e.Column;
            int b = e.Row;

            string serialNo = this.fpEleInvoice_sheetView1.Cells[b, 7].Value.ToString();

            if (a == 9) //重新获取
            {
                string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + serialNo + "</serialNos><invoiceno>" + "" + "</invoiceno><reqxml>" + "" + "</reqxml><hospitalid>" + hospitalId + "</hospitalid></req>";

                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(reqbyInvoicResult, "HanTianInvoice", "HanTianQueryInvoiceResult");

                updateHanTianQueryInvoiceResult(resultXml, serialNo);
                MessageBox.Show(resultXml);

                SetEleInvoiceList();
                return;
            }

            string reqxml = this.fpEleInvoice_sheetView1.ActiveRow.Tag.ToString();

            if (reqxml == "")
            {
                MessageBox.Show("还没有生成电子发票");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(reqxml);

            XmlNode xn = doc.SelectSingleNode("req/invoiceType");
            xn.InnerText = "2";

            string invoiceno = doc.SelectSingleNode("req/orderNo").InnerText;

            DataTable dt = queryEleInvoiceInfo(serialNo);

            string INVOICENO = "";
            string INVOICECODE = "";

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    INVOICENO = dt.Rows[0]["INVOICENO"].ToString();
                    INVOICECODE = dt.Rows[0]["INVOICECODE"].ToString();
                }
                else
                {
                    MessageBox.Show("没有找到相关开票信息！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("没有找到相关开票信息！");
                return;
            }

            XmlNode parent = doc.SelectSingleNode("req");

            XmlNode invoicenode = doc.CreateNode(XmlNodeType.Element, "INVOICENO", "");
            invoicenode.InnerText = INVOICENO;
            parent.AppendChild(invoicenode);

            XmlNode invoicecodenode = doc.CreateNode(XmlNodeType.Element, "INVOICECODE", "");
            invoicecodenode.InnerText = INVOICECODE;
            parent.AppendChild(invoicecodenode);

            reqxml = doc.OuterXml;

            this.HanTianinrequestBillingNew(reqxml, invoiceno);

            MessageBox.Show(reqxml);

            SetEleInvoiceList();
        }

        public int updateHanTianQueryInvoiceResult(string result, string serialNo)
        {
            Bussiness.Util.JsonResult jsonData = FS.HISFC.Components.OutpatientFee.JsonUntity.DeSerializeEntity<Bussiness.Util.JsonResult>(result);
            string code = jsonData.code.ToString();
            string b = jsonData.describe.ToString();
            string c = jsonData.result[0].invoiceCode.ToString();
            string d = jsonData.result[0].invoiceNo.ToString();
            string opertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string updateEleInvoiceInfo = @"update com_opb_eleinvoiceopeninfo set STATUS='{0}',STATUSMSG='{1}',invoicecode='{2}',invoiceno='{3}' where SERIALNO='{4}'";

            string sql = string.Format(updateEleInvoiceInfo, jsonData.result[0].status, jsonData.result[0].statusMsg, jsonData.result[0].invoiceCode, jsonData.result[0].invoiceNo, serialNo);

            sql = sql.Replace("&gt;", "").Replace("&lt;", "");

            outpatientManager.ExecNoQuery(sql);

            return 1;
        }

        public DataTable queryEleInvoiceInfo(string serialNo)
        {
            try
            {
                string queryEleInvoiceInfo = @"select * from com_opb_eleinvoiceopeninfo where serialno='{0}'";

                queryEleInvoiceInfo = string.Format(queryEleInvoiceInfo, serialNo);

                DataSet ds = new DataSet();

                outpatientManager.ExecQuery(queryEleInvoiceInfo, ref ds);

                DataTable dt = new DataTable();

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void HanTianinrequestBillingNew(string req, string invoiceno)
        {
            string reqxml = req;
            string method = "HanTianinrequestBillingNewNew";
            if (hospitalId == "IBORNSD")
            {
                method = "HanTianInvoiceRedConfirm";
            }
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(req, "HanTianInvoice", method);
            MessageBox.Show(resultXml);

            if (hospitalId != "IBORNSD")
            {
                string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + resultXml + "</serialNos><invoiceno>" + invoiceno + "</invoiceno><reqxml>" + reqxml + "</reqxml><hospitalid>" + hospitalId + "</hospitalid></req>";

                resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(reqbyInvoicResult, "HanTianInvoice", "HanTianQueryInvoiceResult");
                MessageBox.Show(resultXml);
                insertHanTianQueryInvoiceResult(resultXml, invoiceno, reqxml);
            }
           
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
                                             , opertime, reqxml, invoiceno
                );

            sql = sql.Replace("&gt;", "").Replace("&lt;", "");
            outpatientManager.ExecNoQuery(sql);
            return 1;
        }

        private void frmEleInvoiceManager_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string where = " where (createtime between to_date('{0}','yyyy-MM-dd') and to_date('{1}','yyyy-MM-dd'))";

            if (this.textBox1.Text.ToString() != "")
                where += " and payername='{2}'";

            string sql = @"select t.createtime 创建时间,t.orderno 订单号,t.notifyemail 邮箱地址,t.invoicecode 发票代码,t.invoiceno 发票号码,
                    t.payername 发票抬头,t.extaxamount 含税金额,t.statusmsg 开票状态,t.serialno 序列号,reqxml from com_opb_eleinvoiceopeninfo t" + where;

            sql = string.Format(sql, Convert.ToDateTime(this.dtpBegin.Text.ToString()).ToString("yyyy-MM-dd"), Convert.ToDateTime(this.dtpEnd.Text.ToString()).ToString("yyyy-MM-dd"), this.textBox1.Text.Trim().ToString());

            DataSet ds = new DataSet();
            outpatientManager.ExecQuery(sql, ref ds);

            if (ds.Tables[0].Rows.Count < 0)
            {
                return;
            }

            DataTable dt = ds.Tables[0];

            this.fpEleInvoice_sheetView1.Rows.Count = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.fpEleInvoice_sheetView1.Rows.Add(this.fpEleInvoice_sheetView1.Rows.Count, 1);
                //添加结算明细
                this.fpEleInvoice_sheetView1.Cells[i, 0].Value = dt.Rows[i]["创建时间"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 1].Value = dt.Rows[i]["订单号"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 2].Value = dt.Rows[i]["邮箱地址"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 3].Value = dt.Rows[i]["发票代码"].ToString() + "/" + dt.Rows[i]["发票号码"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 4].Value = dt.Rows[i]["发票抬头"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 5].Value = dt.Rows[i]["含税金额"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 6].Value = dt.Rows[i]["开票状态"].ToString();
                this.fpEleInvoice_sheetView1.Cells[i, 7].Value = dt.Rows[i]["序列号"].ToString();

                FarPoint.Win.Spread.CellType.ButtonCellType bct = new FarPoint.Win.Spread.CellType.ButtonCellType();
                bct.Text = "冲红";

                this.fpEleInvoice_sheetView1.Cells[i, 8].CellType = bct;

                FarPoint.Win.Spread.CellType.ButtonCellType bct1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
                bct1.Text = "重新获取";

                this.fpEleInvoice_sheetView1.Cells[i, 9].CellType = bct1;
                this.fpEleInvoice_sheetView1.Rows[i].Tag = dt.Rows[i]["reqxml"].ToString(); ;

            }
            return;
        }

    }
}
