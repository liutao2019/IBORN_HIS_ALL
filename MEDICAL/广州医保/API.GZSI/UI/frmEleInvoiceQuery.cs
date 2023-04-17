using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.UI
{
    public partial class frmEleInvoiceQuery : Form
    {
        public frmEleInvoiceQuery(string patientid)
        {
            InitializeComponent();
            this.fpEleInvoice.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpEleInvoiceInfo_ButtonClicked);
            PatientId = patientid;
            SetEleInvoiceList(patientid);
        }

        private string hospitalId = "IBORNGZ";

        private string PatientId = "";

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        private void SetEleInvoiceList(string patientid)
        {
            DataTable dt = this.localMgr.QueryInvoiceInfoList(patientid);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有查询到患者的发票号，请向收费员确定是否开票！！");
            }
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
                bct.Text = "重新获取";
                this.fpEleInvoice_sheetView1.Cells[i, 8].CellType = bct;

                this.fpEleInvoice_sheetView1.Rows[i].Tag = dt.Rows[i]["reqxml"].ToString(); ;
            }

            string hospitalname = FS.FrameWork.Management.Connection.Hospital.Name;
            if (hospitalname.Contains("广州"))
                hospitalId = "IBORNGZ";
            else if (hospitalname.Contains("顺德"))
                hospitalId = "IBORNSD";

        }

        FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        //重新获取发票
        void fpEleInvoiceInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int a = e.Column;
            int b = e.Row;

            string serialNo = this.fpEleInvoice_sheetView1.Cells[b, 7].Value.ToString();

            if (a == 8) //重新获取
            {
                string reqbyInvoicResult = "<req><isOfferInvoiceDetail>0</isOfferInvoiceDetail><orderNos></orderNos><serialNos>" + serialNo + "</serialNos><invoiceno>" + "" + "</invoiceno><reqxml>" + "" + "</reqxml><hospitalid>" + hospitalId + "</hospitalid></req>";

                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.HanTianInvoice(reqbyInvoicResult, "HanTianInvoice", "HanTianQueryInvoiceResult");

                updateHanTianQueryInvoiceResult(resultXml, serialNo);
                MessageBox.Show(resultXml);

                SetEleInvoiceList(PatientId);
               
            }
        }

        public int updateHanTianQueryInvoiceResult(string result, string serialNo)
        {
            API.GZSI.Class.JsonResult jsonData = API.GZSI.Common.JsonHelper.DeSerializeEntity<API.GZSI.Class.JsonResult>(result);
            string code = jsonData.code.ToString();
            string b = jsonData.describe.ToString();
            string c = jsonData.result[0].invoiceCode.ToString();
            string d = jsonData.result[0].invoiceNo.ToString();
            string opertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string updateEleInvoiceInfo = @"update com_opb_eleinvoiceopeninfo set STATUS='{0}',STATUSMSG='{1}',invoicecode='{2}',invoiceno='{3}' where SERIALNO='{4}'";

            string sql = string.Format(updateEleInvoiceInfo, jsonData.result[0].status, jsonData.result[0].statusMsg, jsonData.result[0].invoiceCode, jsonData.result[0].invoiceNo, serialNo);


            sql = sql.Replace("&gt;", "").Replace("&lt;", "");

            inpatientFeeManager.ExecNoQuery(sql);

            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SetEleInvoiceList(this.txtName.Text);
        }


    }
}
