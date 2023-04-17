using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.Print.uc4101
{
    public partial class ucPrint4101 : UserControl
    {
        #region 属性

        private int rowHeight = 30;
        /// <summary>
        /// 行高
        /// </summary>
        public int RowHeight
        {
            get { return rowHeight; }
            set { rowHeight = value; }
        }

        private int printRowCount = 20;
        /// <summary>
        /// 一页打印行数
        /// </summary>
        public int PrintRowCount
        {
            get { return printRowCount; }
            set { printRowCount = value; }
        }

        private int topHeight = 135;
        /// <summary>
        /// 顶栏高度
        /// </summary>
        public int TopHeight
        {
            get { return topHeight; }
            set { topHeight = value; }
        }

        private int bottomHeight = 80;
        /// <summary>
        /// 底栏高度
        /// </summary>
        public int BottomHeight
        {
            get { return bottomHeight; }
            set { bottomHeight = value; }
        }

        #endregion

        #region 打印相关

        /// <summary>
        /// 页码选择框
        /// </summary>
        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        /// <summary>
        /// 打印实体对象
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        /// <summary>
        /// 打印参数
        /// </summary>
        System.Drawing.Printing.PrintPageEventArgs printPageEventArgs = null;

        /// <summary>
        /// 打印预览
        /// </summary>
        PrintPreviewDialog printPreviewDialog = null;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        private int maxPageNO = 1;        
        
        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        private int curPageNO = 1;

        #endregion 

        #region 值对象

        Models.Request.RequestGzsiModel4101 requestModel = new API.GZSI.Models.Request.RequestGzsiModel4101();
        /// <summary>
        /// 请求对象
        /// </summary>
        public Models.Request.RequestGzsiModel4101 RequestModel
        {
            get { return requestModel; }
            set { requestModel = value; }
        }

        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }

        #endregion

        #region 业务类

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        public ucPrint4101()
        {
            InitializeComponent();
            InitControls();
            InitEvents();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {  
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvents()
        {
            PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
            PrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
        }

        /// <summary>
        /// 控件赋值
        /// </summary>
        /// <returns></returns>
        public int SetValue()
        {
            this.SetInfo();
            this.SetPanelInfo();
            this.SetOpspdiseinfo();
            this.SetDiseInfo();
            this.SetOprnInfo();
            this.SetICUInfo();
            this.SetItemInfo();
            return 1;
        }

        /// <summary>
        /// 设置当前页面的一些信息
        /// </summary>
        private void SetInfo()
        {
            //lb信息赋值
            this.lbSetlId.Text = this.RequestModel.setlinfo.setl_id;
            this.lbFixmedinsName.Text = this.RequestModel.setlinfo.fixmedins_name;
            this.lbFixmedinsCode.Text = this.RequestModel.setlinfo.fixmedins_code;
            this.lbHiSetlLv.Text = string.IsNullOrEmpty(this.RequestModel.setlinfo.hi_setl_lv) ? "二级甲等" : this.RequestModel.setlinfo.hi_setl_lv;
            this.lbHiNo.Text = this.RequestModel.setlinfo.hi_no;
            this.lbMedcasno.Text = this.RequestModel.setlinfo.medcasno;
            this.lbDclaTime.Text = this.RequestModel.setlinfo.dcla_time;
            this.lbMedinsFillDept.Text = this.RequestModel.setlinfo.medins_fill_dept;
            this.lbMedinsFillPsn.Text = this.RequestModel.setlinfo.medins_fill_psn;
            this.lbMedinsFillDept2.Text = this.RequestModel.setlinfo.hsorg;
            this.lbMedinsFillPsn2.Text = this.RequestModel.setlinfo.hsorg_opter;

            this.fpSpread_Sheet1.Cells["OpspdiseDept"].Tag = "就诊科别：" + this.requestModel.setlinfo.adm_caty;
            this.fpSpread_Sheet1.Cells["OpspdiseInDate"].Tag = "就诊时间：" + this.requestModel.setlinfo.adm_time.Split()[0];


            this.fpSpread_Sheet1.Cells["业务流水号"].Value = "业务流水号：" + this.requestModel.setlinfo.biz_sn;
            this.fpSpread_Sheet1.Cells["票据代码"].Value = "票据代码：" + this.requestModel.setlinfo.bill_code;
            this.fpSpread_Sheet1.Cells["票据号码"].Value = "票据号码：" + this.requestModel.setlinfo.bill_no;
            this.fpSpread_Sheet1.Cells["结算时间"].Value = "结算时间：" + this.requestModel.setlinfo.setl_begn_date.Split()[0] + "~" + this.requestModel.setlinfo.setl_end_date.Split()[0];

            #region ZF
            //decimal amtSum = 0.0m;
            //decimal claa_sumfeeSum = 0.0m;
            //decimal clab_amtSum = 0.0m;
            //decimal fulamt_ownpay_amtSum = 0.0m;
            //decimal oth_amtSum = 0.0m;

            ////费用信息赋值
            //foreach (Models.Request.RequestGzsiModel4101.Iteminfo itemInfo in this.RequestModel.iteminfo)
            //{
            //    string rowTag = consMgr.GetConstant("GZSI_med_chrgitm_type", itemInfo.med_chrgitm).Name;
            //    int row = this.fpSpread_Sheet1.Cells[rowTag].Row.Index;
            //    this.fpSpread_Sheet1.Cells[row, 6].Value = itemInfo.amt;
            //    this.fpSpread_Sheet1.Cells[row, 8].Value = itemInfo.claa_sumfee;
            //    this.fpSpread_Sheet1.Cells[row, 10].Value = itemInfo.clab_amt;
            //    this.fpSpread_Sheet1.Cells[row, 12].Value = itemInfo.fulamt_ownpay_amt;
            //    this.fpSpread_Sheet1.Cells[row, 14].Value = itemInfo.oth_amt;

            //    amtSum += decimal.Parse(itemInfo.amt);
            //    claa_sumfeeSum += decimal.Parse(itemInfo.claa_sumfee);
            //    clab_amtSum += decimal.Parse(itemInfo.clab_amt);
            //    fulamt_ownpay_amtSum += decimal.Parse(itemInfo.fulamt_ownpay_amt);
            //    oth_amtSum += decimal.Parse(itemInfo.oth_amt);
            //}

            ////合计列
            //int sumRow = this.fpSpread_Sheet1.Cells["金额合计"].Row.Index;
            //this.fpSpread_Sheet1.Cells[sumRow, 6].Value = amtSum.ToString();
            //this.fpSpread_Sheet1.Cells[sumRow, 8].Value = claa_sumfeeSum.ToString();
            //this.fpSpread_Sheet1.Cells[sumRow, 10].Value = clab_amtSum.ToString();
            //this.fpSpread_Sheet1.Cells[sumRow, 12].Value = fulamt_ownpay_amtSum.ToString();
            //this.fpSpread_Sheet1.Cells[sumRow, 14].Value = oth_amtSum.ToString();
            #endregion

            //基金支付信息
            //this.fpSpread_Sheet1.Cells["hifp_pay"].Value = this.patientInfo.SIMainInfo.Hifp_pay.ToString();
            //this.fpSpread_Sheet1.Cells["hifmi_pay"].Value = this.patientInfo.SIMainInfo.Hifmi_pay.ToString();
            //this.fpSpread_Sheet1.Cells["maf_pay"].Value = this.patientInfo.SIMainInfo.Maf_pay.ToString();
            //this.fpSpread_Sheet1.Cells["cvlserv_pay"].Value = this.patientInfo.SIMainInfo.Cvlserv_pay.ToString();
            //this.fpSpread_Sheet1.Cells["hifob_pay"].Value = this.patientInfo.SIMainInfo.Hifob_pay.ToString();
            //this.fpSpread_Sheet1.Cells["hifes_pay"].Value = this.patientInfo.SIMainInfo.Hifes_pay.ToString();

            //this.fpSpread_Sheet1.Cells["psn_part_am"].Value = this.patientInfo.SIMainInfo.Psn_part_am.ToString();
            //this.fpSpread_Sheet1.Cells["ownpay_amt"].Value = this.patientInfo.SIMainInfo.Ownpay_amt.ToString();
            //this.fpSpread_Sheet1.Cells["acct_pay"].Value = this.patientInfo.SIMainInfo.Acct_pay.ToString();
            //this.fpSpread_Sheet1.Cells["cash_payamt"].Value = this.patientInfo.SIMainInfo.Cash_payamt.ToString();

            this.fpSpread_Sheet1.Cells["psn_part_am"].Value = this.requestModel.setlinfo.psn_selfpay;
            this.fpSpread_Sheet1.Cells["ownpay_amt"].Value = this.requestModel.setlinfo.psn_ownpay;
            this.fpSpread_Sheet1.Cells["acct_pay"].Value = this.requestModel.setlinfo.acct_pay;
            this.fpSpread_Sheet1.Cells["cash_payamt"].Value = this.requestModel.setlinfo.psn_cashpay;

        }

        /// <summary>
        /// 设置当前页面的一些信息
        /// </summary>
        private void SetItemInfo() {
            #region iteminfo 
            foreach (Models.Request.RequestGzsiModel4101.Iteminfo data in this.requestModel.iteminfo)
            {
                this.fpSpread_Sheet1.Cells["金额" + data.med_chrgitm].Text = data.amt;
                this.fpSpread_Sheet1.Cells["甲类" + data.med_chrgitm].Text = data.claa_sumfee;
                this.fpSpread_Sheet1.Cells["乙类" + data.med_chrgitm].Text = data.clab_amt;
                this.fpSpread_Sheet1.Cells["自费" + data.med_chrgitm].Text = data.fulamt_ownpay_amt;
                this.fpSpread_Sheet1.Cells["其他" + data.med_chrgitm].Text = data.oth_amt;
            }
            //费用合计
            this.fpSpread_Sheet1.Cells["金额ALL"].Text = Convert.ToDecimal( this.requestModel.iteminfo.Sum(m => Convert.ToDecimal(m.amt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["甲类ALL"].Text = Convert.ToDecimal( this.requestModel.iteminfo.Sum(m => Convert.ToDecimal(m.claa_sumfee))).ToString("f2");
            this.fpSpread_Sheet1.Cells["乙类ALL"].Text = Convert.ToDecimal( this.requestModel.iteminfo.Sum(m => Convert.ToDecimal(m.clab_amt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["自费ALL"].Text = Convert.ToDecimal( this.requestModel.iteminfo.Sum(m => Convert.ToDecimal(m.fulamt_ownpay_amt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["其他ALL"].Text = Convert.ToDecimal( this.requestModel.iteminfo.Sum(m => Convert.ToDecimal(m.oth_amt))).ToString("f2");
            #endregion

            #region payinfo -no
            //foreach (GZAPI4101.Payinfo data in this.requestModel.payinfo)
            //{
            //    this.fpSpread_Sheet1.Cells["基金支付"].Text = data.fund_payamt;
            //}
            this.fpSpread_Sheet1.Cells["hifp_pay"].Text = Convert.ToDecimal(this.requestModel.payinfo.FindAll(m => m.fund_pay_type.Contains( "3901") || m.fund_pay_type.Contains( "3101")).Sum(m => Convert.ToDecimal(m.fund_payamt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["hifmi_pay"].Text = Convert.ToDecimal(this.requestModel.payinfo.FindAll(m => m.fund_pay_type == "390200" || m.fund_pay_type == "310300" || m.fund_pay_type == "390300").Sum(m => Convert.ToDecimal(m.fund_payamt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["maf_pay"].Text = Convert.ToDecimal(this.requestModel.payinfo.FindAll(m => m.fund_pay_type == "610100").Sum(m => Convert.ToDecimal(m.fund_payamt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["cvlserv_pay"].Text = Convert.ToDecimal(this.requestModel.payinfo.FindAll(m => m.fund_pay_type == "320100").Sum(m => Convert.ToDecimal(m.fund_payamt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["hifob_pay"].Text = Convert.ToDecimal(this.requestModel.payinfo.FindAll(m => m.fund_pay_type == "330100").Sum(m => Convert.ToDecimal(m.fund_payamt))).ToString("f2");
            this.fpSpread_Sheet1.Cells["hifes_pay"].Text = Convert.ToDecimal(this.requestModel.payinfo.FindAll(m => m.fund_pay_type == "370100").Sum(m => Convert.ToDecimal(m.fund_payamt))).ToString("f2");
            #endregion
        }


        /// <summary>
        /// 设置部分基本信息
        /// </summary>
        private void SetPanelInfo()
        {
            CellType4101 baseInfo = new CellType4101();
            ucBaseInfo ucBaseInfo1 = new ucBaseInfo();
            ucBaseInfo1.RequestModel = this.RequestModel;
            baseInfo.uc = ucBaseInfo1;
            this.fpSpread_Sheet1.Cells["baseInfopnl"].Row.Height = baseInfo.uc.Height;
            this.fpSpread_Sheet1.Cells["baseInfopnl"].CellType = baseInfo;

            CellType4101 diseInfo = new CellType4101();
            ucDiseInfo ucDiseInfo1 = new ucDiseInfo();
            ucDiseInfo1.RequestModel = this.RequestModel;
            diseInfo.uc = ucDiseInfo1;
            this.fpSpread_Sheet1.Cells["diseInfopnl"].Row.Height = diseInfo.uc.Height;
            this.fpSpread_Sheet1.Cells["diseInfopnl"].CellType = diseInfo;

            CellType4101 diseInfoCount = new CellType4101();
            ucDiseInfoCount ucDiseInfoCount1 = new ucDiseInfoCount();
            ucDiseInfoCount1.RequestModel = this.RequestModel;
            diseInfoCount.uc = ucDiseInfoCount1;
            this.fpSpread_Sheet1.Cells["diseInfoCountpnl"].Row.Height = diseInfoCount.uc.Height;
            this.fpSpread_Sheet1.Cells["diseInfoCountpnl"].CellType = diseInfoCount;

            CellType4101 oprnInfoCount = new CellType4101();
            ucOprnInfoCount ucOprnInfoCount1 = new ucOprnInfoCount();
            ucOprnInfoCount1.RequestModel = this.RequestModel;
            oprnInfoCount.uc = ucOprnInfoCount1;
            this.fpSpread_Sheet1.Cells["oprnInfoCountpnl"].Row.Height = oprnInfoCount.uc.Height;
            this.fpSpread_Sheet1.Cells["oprnInfoCountpnl"].CellType = oprnInfoCount;

            CellType4101 icuInfo = new CellType4101();
            ucICUInfo ucICUInfo1 = new ucICUInfo();
            ucICUInfo1.RequestModel = this.RequestModel;
            icuInfo.uc = ucICUInfo1;
            this.fpSpread_Sheet1.Cells["icuInfopnl"].Row.Height = icuInfo.uc.Height;
            this.fpSpread_Sheet1.Cells["icuInfopnl"].CellType = icuInfo;

            CellType4101 payType = new CellType4101();
            ucPayType ucPayType1 = new ucPayType();
            ucPayType1.RequestModel = this.RequestModel;
            payType.uc = ucPayType1;
            this.fpSpread_Sheet1.Cells["payTypepnl"].Row.Height = payType.uc.Height;
            this.fpSpread_Sheet1.Cells["payTypepnl"].CellType = payType;
        }

        /// <summary>
        /// 设置慢病诊疗信息
        /// </summary>
        private void SetOpspdiseinfo()
        {
            int count = this.RequestModel.opspdiseinfo.Count;
            int row = this.fpSpread_Sheet1.Cells["Opspdiseinfo"].Row.Index;

            if (count > 1)
            {
                this.fpSpread_Sheet1.AddRows(row + 1, count - 1);
                //单元格格式设置（合并单元格）
                for (int i = 1; i < count; i++)
                {
                    this.fpSpread_Sheet1.Cells[row + i, 0].ColumnSpan = 4;
                    this.fpSpread_Sheet1.Cells[row + i, 4].ColumnSpan = 4;
                    this.fpSpread_Sheet1.Cells[row + i, 8].ColumnSpan = 4;
                    this.fpSpread_Sheet1.Cells[row + i, 12].ColumnSpan = 4;
                }
            }

            int rowIndex = row;
            foreach (API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo opspdise in this.RequestModel.opspdiseinfo)
            {
                this.fpSpread_Sheet1.Cells[rowIndex, 0].Value = opspdise.diag_name;
                this.fpSpread_Sheet1.Cells[rowIndex, 4].Value = opspdise.diag_code;
                this.fpSpread_Sheet1.Cells[rowIndex, 8].Value = opspdise.oprn_oprt_name;
                this.fpSpread_Sheet1.Cells[rowIndex, 12].Value = opspdise.oprn_oprt_code;
                rowIndex++;
            }
        }

        /// <summary>
        /// 设置诊断信息
        /// </summary>
        private void SetDiseInfo()
        {

            int mainCount = 0;
            int otherCount = 0;
            foreach (API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo dise in this.RequestModel.diseinfo)
            {
                if (dise.maindiag_flag == "1")
                {
                    mainCount++;
                }
                else
                {
                    otherCount++;
                }
            }

            int rowMain = this.fpSpread_Sheet1.Cells["DiseinfoMain"].Row.Index;
            if (mainCount > 1)
            {
                this.fpSpread_Sheet1.AddRows(rowMain + 1, mainCount - 1);
                //单元格格式设置（合并单元格）
                for (int i = 1; i < mainCount; i++)
                {
                    this.fpSpread_Sheet1.Cells[rowMain + i, 0].ColumnSpan = 3;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 3].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 5].ColumnSpan = 3;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 8].ColumnSpan = 3;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 11].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 13].ColumnSpan = 3;
                }
            }

            int rowOther = this.fpSpread_Sheet1.Cells["DiseinfoOther"].Row.Index;
            if (otherCount > 1)
            {
                this.fpSpread_Sheet1.AddRows(rowOther + 1, otherCount - 1);
                //单元格格式设置（合并单元格）
                for (int i = 1; i < otherCount; i++)
                {
                    this.fpSpread_Sheet1.Cells[rowOther + i, 0].ColumnSpan = 3;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 3].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 5].ColumnSpan = 3;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 8].ColumnSpan = 3;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 11].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 13].ColumnSpan = 3;
                }
            }

            int rowMainIndex = rowMain;
            int rowOhterIndex = rowOther;
            foreach (API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo dise in this.RequestModel.diseinfo)
            {
                if (dise.maindiag_flag == "1")
                {
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 0].Value = dise.diag_name;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 3].Value = dise.diag_code;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 5].Value = consMgr.GetConstant("RYBQ", dise.adm_cond_type).Name;
                    rowMainIndex++;
                }
                else
                {
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 0].Value = dise.diag_name;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 3].Value = dise.diag_code;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 5].Value = consMgr.GetConstant("RYBQ", dise.adm_cond_type).Name;
                    rowOhterIndex++;
                }
            }
        }

        /// <summary>
        /// 设置手术信息
        /// </summary>
        private void SetOprnInfo()
        {
            int mainCount = 0;
            int otherCount = 0;
            foreach (API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo oprn in this.RequestModel.oprninfo)
            {
                mainCount++;
            }

            int rowMain = this.fpSpread_Sheet1.Cells["oprnInfoMain"].Row.Index;

            if (mainCount > 1)
            {
                this.fpSpread_Sheet1.AddRows(rowMain + 1, mainCount - 1);
                //单元格格式设置（合并单元格）
                for (int i = 1; i < mainCount; i++)
                {
                    this.fpSpread_Sheet1.Cells[rowMain + i, 0].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 2].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 4].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 6].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 8].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 10].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 12].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowMain + i, 14].ColumnSpan = 2;
                }
            }

            int rowOther = this.fpSpread_Sheet1.Cells["oprnInfoOther"].Row.Index;
            if (otherCount > 1)
            {
                this.fpSpread_Sheet1.AddRows(rowOther + 1, otherCount);
                for (int i = 1; i < otherCount; i++)
                {
                    this.fpSpread_Sheet1.Cells[rowOther + i, 0].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 2].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 4].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 6].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 8].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 10].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 12].ColumnSpan = 2;
                    this.fpSpread_Sheet1.Cells[rowOther + i, 14].ColumnSpan = 2;
                }
            }

            int rowMainIndex = rowMain;
            int rowOhterIndex = rowMain;
            foreach (API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo oprn in this.RequestModel.oprninfo)
            {
                if (true)
                {
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 0].Value = oprn.oprn_oprt_name;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 2].Value = oprn.oprn_oprt_code;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 4].Value = oprn.oprn_oprt_date;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 6].Value = consMgr.GetConstant("MZFS", oprn.anst_way).Name;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 8].Value = oprn.oper_dr_name;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 10].Value = oprn.oper_dr_code;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 12].Value = oprn.anst_dr_name;
                    this.fpSpread_Sheet1.Cells[rowMainIndex, 14].Value = oprn.anst_dr_code;
                    rowMainIndex++;
                }
                else
                {
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 0].Value = oprn.oprn_oprt_name;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 2].Value = oprn.oprn_oprt_code;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 4].Value = oprn.oprn_oprt_date;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 6].Value = consMgr.GetConstant("MZFS", oprn.anst_way).Name;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 8].Value = oprn.oper_dr_name;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 10].Value = oprn.oper_dr_code;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 12].Value = oprn.anst_dr_name;
                    this.fpSpread_Sheet1.Cells[rowOhterIndex, 14].Value = oprn.anst_dr_code;
                    rowOhterIndex++;
                }
            }
        }

        /// <summary>
        /// 设置ICU信息
        /// </summary>
        private void SetICUInfo()
        {
            int count = this.RequestModel.icuinfo.Count;
            int row = this.fpSpread_Sheet1.Cells["icuInfo"].Row.Index;

            if (count > 1)
            {
                this.fpSpread_Sheet1.AddRows(row + 1, count - 1);
                //单元格格式设置（合并单元格）
                for (int i = 1; i < count; i++)
                {
                    this.fpSpread_Sheet1.Cells[row + i, 0].ColumnSpan = 5;
                    this.fpSpread_Sheet1.Cells[row + i, 5].ColumnSpan = 4;
                    this.fpSpread_Sheet1.Cells[row + i, 9].ColumnSpan = 4;
                    this.fpSpread_Sheet1.Cells[row + i, 13].ColumnSpan = 3;
                }
            }

            int rowIndex = row;
            foreach (API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo icu in this.RequestModel.icuinfo)
            {
                this.fpSpread_Sheet1.Cells[rowIndex, 0].Value = icu.scs_cutd_ward_type;
                this.fpSpread_Sheet1.Cells[rowIndex, 5].Value = icu.scs_cutd_inpool_time;
                this.fpSpread_Sheet1.Cells[rowIndex, 9].Value = icu.scs_cutd_exit_time;
                this.fpSpread_Sheet1.Cells[rowIndex, 13].Value = icu.scs_cutd_sum_dura;
                rowIndex++;
            }
        }

        #region 打印相关

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            Graphics graphics = Graphics.FromHwnd(this.Handle);
            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = true;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            //printInfo.ShowRowHeaders = this.fpSpread_Sheet1.RowHeader.Visible;
            this.fpSpread_Sheet1.PrintInfo = printInfo;

            maxPageNO = fpSpread.GetOwnerPrintPageCount(graphics, new Rectangle(this.Margin.Left, this.Margin.Top + this.TopHeight, this.fpSpread.Width, this.fpSpread.Height), 0);

            socPrintPageSelectDialog.MaxPageNO = maxPageNO;
            if (maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return 1;
                }
            }

            this.PrintPage(null);

            return 1;
        }

        /// <summary>
        /// 开始打印
        /// </summary>
        /// <param name="paperSize"></param>
        protected void PrintPage(System.Drawing.Printing.PaperSize paperSize)
        {
            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))  
                return;
            this.PrintDocument.PrinterSettings.PrinterName = printerName;

            this.SetPaperSize(paperSize);
            this.PrintDocument.Print();
        }

        /// <summary>
        /// 纸张尺寸
        /// </summary>
        /// <param name="paperSize"></param>
        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("4101", 850, 1102);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        /// <summary>
        /// 打印开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.curPageNO = 1;
        }

        /// <summary>
        /// 打印过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            printPageEventArgs = e;
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                this.curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                e.HasMorePages = true;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            foreach (Control c in this.pnlTop.Controls)
            {
                if (c is System.Windows.Forms.Label)
                {
                    graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), c.Location.X, c.Location.Y);
                }
            }
            #endregion

            #region Farpoint绘制

            this.fpSpread.OwnerPrintDraw(graphics, new Rectangle(this.Margin.Left + this.pnlLeft.Width, this.Margin.Top + this.TopHeight, this.fpSpread.Width, this.fpSpread.Height), 0, this.curPageNO);

            #endregion

            #region 页尾绘制
            foreach (Control c in this.pnlBottom.Controls)
            {
                if (c is System.Windows.Forms.Label)
                {
                    if (c.Name == "lblPageIndex")
                    {
                        c.Text = string.Format("第{0}页/共{1}页", this.curPageNO.ToString(), this.maxPageNO.ToString());
                    }
                    graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), c.Location.X + this.pnlBottom.Location.X, c.Location.Y + pnlBottom.Location.Y);
                }
            }
            #endregion

            #region 分页
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < this.maxPageNO)
            {
                e.HasMorePages = true;
                this.curPageNO++;
            }
            else
            {
                this.curPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        /// <summary>
        /// 打印结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (printPreviewDialog != null && this.PrintDocument.PrintController.IsPreview == false)
            {
                printPreviewDialog.Close();
                printPreviewDialog.Dispose();
            }
        }

        #endregion

        #region 自定义显示cellType

        public class CellType4101 : FarPoint.Win.Spread.CellType.GeneralCellType
        {
            //需要绘制的控件
            public UserControl uc;

            public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor)
            {
                if (uc == null)
                {
                    g.FillRectangle(Brushes.White, r);
                    g.DrawLine(Pens.LightGray, new Point(r.X, r.Y + r.Height), new Point(r.X + r.Width, r.Y));
                    return;
                }

                g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;

                foreach (Control control in uc.Controls)
                {
                    if (control.Visible == false)
                    {
                        continue;
                    }

                    if (control is System.Windows.Forms.Label)
                    {
                        System.Windows.Forms.Label label = control as System.Windows.Forms.Label;
                        RectangleF lableTextRect = _LabelTextRect(g, r, label, 0);
                        g.DrawString(label.Text, label.Font, Brushes.Black, lableTextRect);
                    }
                    else if (control is System.Windows.Forms.Panel)
                    {
                        System.Windows.Forms.Panel panel = control as System.Windows.Forms.Panel;
                        g.DrawLine(new Pen(Brushes.Black), new PointF(r.Left + panel.Location.X, r.Top + panel.Location.Y), new PointF(r.Left + panel.Location.X + panel.Size.Width, r.Top + panel.Location.Y));
                    }
                }
            }

            public void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.DrawPath(pen, path);
                }
            }
            public void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.FillPath(brush, path);
                }
            }

            internal GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
            {
                GraphicsPath roundedRect = new GraphicsPath();
                roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
                roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
                roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
                roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
                roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
                roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
                roundedRect.CloseFigure();
                return roundedRect;
            }

            #region 自定义CellType辅助函数
            private static RectangleF _LabelTextRect(Graphics graphics, RectangleF rect, System.Windows.Forms.Label label, float leftMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(label.Text, label.Font);
                return new RectangleF(new PointF(rect.Left + label.Location.X, rect.Top + label.Location.Y), size);
            }

            private static RectangleF _TextAlignLeftTop(Graphics graphics, RectangleF rect, string text, Font font, float leftMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Left + leftMargin, rect.Top + 5), size);
            }

            private static RectangleF _TextAlignLeftMiddle(Graphics graphics, RectangleF rect, string text, Font font, float leftMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Left + leftMargin, rect.Top + (rect.Height - size.Height) / 2), size);
            }

            private static RectangleF _TextAlignLeftBottom(Graphics graphics, RectangleF rect, string text, Font font, float leftMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Left + leftMargin, rect.Top + (rect.Height - size.Height)), size);
            }

            private static RectangleF _TextAlignCenterTop(Graphics graphics, RectangleF rect, string text, Font font)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Left + (rect.Width - size.Width) / 2, rect.Top + 5), size);
            }

            private static RectangleF _TextAlignCenterMiddle(Graphics graphics, RectangleF rect, string text, Font font)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Left + (rect.Width - size.Width) / 2, rect.Top + (rect.Height - size.Height) / 2), size);
            }

            private static RectangleF _TextAlignCenterBottom(Graphics graphics, RectangleF rect, string text, Font font)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Left + (rect.Width - size.Width) / 2, rect.Top + (rect.Height - size.Height)), size);
            }

            private static RectangleF _TextAlignRightTop(Graphics graphics, RectangleF rect, string text, Font font, float rightMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Right - rightMargin - size.Width, rect.Top + 5), size);
            }

            private static RectangleF _TextAlignRightMiddle(Graphics graphics, RectangleF rect, string text, Font font, float rightMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Right - rightMargin - size.Width, rect.Top + (rect.Height - size.Height) / 2), size);
            }

            private static RectangleF _TextAlignRightBottom(Graphics graphics, RectangleF rect, string text, Font font, float rightMargin /*= 0.0f*/)
            {
                var size = graphics.MeasureString(text, font);
                return new RectangleF(new PointF(rect.Right - rightMargin - size.Width, rect.Top + (rect.Height - size.Height)), size);
            }

            #endregion
        }

        #endregion
    }
}
