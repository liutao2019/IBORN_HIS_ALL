using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.PubReport.Components
{
    public partial class ucPubReport : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPubReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucPubReport_Load);
        }

        SOC.Local.PubReport.BizLogic.PubReport pub = new SOC.Local.PubReport.BizLogic.PubReport();
        SOC.Local.PubReport.Components.Report re = new Report();
        private void InitControl()
        {
            try
            {
                SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();
                Neusoft.FrameWork.Models.NeuObject staticTime = pubMgr.GetLastStaticTime();
                this.txtStaticMonth.Text = staticTime.ID + staticTime.Memo;

                //Neusoft.FrameWork.Models.NeuObject objc = this.pub.GetStaticTime();
                //this.dtBegin.Value = Neusoft.NFC.Function.NConvert.ToDateTime(objc.User01);
                //this.dtEnd.Value = Neusoft.NFC.Function.NConvert.ToDateTime(objc.User02);

                //初始化查询树
                SOC.Local.PubReport.BizLogic.StatReport stat = new SOC.Local.PubReport.BizLogic.StatReport();
                ArrayList alreport = new ArrayList();
                alreport = stat.GetReportIndex();
                TreeNode root1 = new TreeNode("公费报表", 0, 1);
                root1.Tag = "REPORT";
                this.tvPactList.Nodes.Add(root1);
                if (alreport == null)
                {
                    MessageBox.Show("获取报表维护出错！" + stat.Err);
                    return;
                }
                foreach (Neusoft.FrameWork.Models.NeuObject obj in alreport)
                {
                    TreeNode node = new TreeNode();
                    node.Text = obj.Name;
                    node.Tag = obj.ID;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    root1.Nodes.Add(node);
                }
                this.tvPactList.ExpandAll();
            }
            catch
            {
            }
        }
        private void Qurey()
        {

            if (this.tvPactList.SelectedNode == null)
            {
                return;
            }
            string reportKind = this.tvPactList.SelectedNode.Tag.ToString();
            string reportName = this.tvPactList.SelectedNode.Text;
            if (reportKind == "REPORT")
            {
                return;
            }
            /*
            if (reportKind == "SGYMX")
            {
                dsShiGfMx dsShiGfMx1 = new dsShiGfMx();
                DataSet dstemp = pub.GetShiGfDetail(begin, end, reportKind);
                if (dstemp == null)
                    return;
                foreach (DataRow row in dstemp.Tables[0].Rows)
                {
                    dsShiGfMx1.Tables[0].Rows.Add(row.ItemArray);
                }
                crShiGyMx crShiGyDetail = new crShiGyMx();
                this.re.ShowReport(this.panel2, crShiGyDetail, dsShiGfMx1);
                return;
            }
             */
            if (reportKind.IndexOf("CHECK,") != -1)
            {
                string eare = reportKind.Replace("CHECK,", "");
                DsSpecialCheck dsSpecialCheck = new DsSpecialCheck();
                DataSet dstemp = pub.GetSpecialCheck(this.txtStaticMonth.GetStaticMonth(), eare);
                if (dstemp == null)
                    return;
                foreach (DataRow row in dstemp.Tables[0].Rows)
                {
                    row["diqu"] = reportName;
                    DateTime dt = this.txtStaticMonth.GetStaticMonth();
                    row["static_month"] = dt.Year.ToString() + "年" + dt.Month.ToString().PadLeft(2, '0')+ "月";
                    dsSpecialCheck.Tables[0].Rows.Add(row.ItemArray);
                }
                if (eare == "SGY" || eare == "SGYM")
                {
                    CrShengSpecialCheck crSpecialCheck = new CrShengSpecialCheck();
                    int sizes = PaperSizeGetter.Get_PaperSizes("Epson LQ-1600K", "gongfei");
                    crSpecialCheck.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);
                    this.re.ShowReport(this.panel2, crSpecialCheck, dsSpecialCheck);
                    return;
                }
                else
                {
                    CrSpecialCheck crSpecialCheck = new CrSpecialCheck();
                    CrystalDecisions.CrystalReports.Engine.TextObject txtKaihu = crSpecialCheck.ReportDefinition.ReportObjects["kaihu"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                    CrystalDecisions.CrystalReports.Engine.TextObject txtZhanghao = crSpecialCheck.ReportDefinition.ReportObjects["zhanghao"] as CrystalDecisions.CrystalReports.Engine.TextObject;

                    txtZhanghao.Text = "帐号:44-032701040006806";
                    txtKaihu.Text = "农业银行恒福支行";

                    //中行先烈南路支行
                    int sizes = PaperSizeGetter.Get_PaperSizes("Epson LQ-1600K", "gongfei");
                    crSpecialCheck.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);
                    this.re.ShowReport(this.panel2, crSpecialCheck, dsSpecialCheck);
                    return;
                }
            }

            SOC.Local.PubReport.Components.dsGFhzReport dsGfhz = new dsGFhzReport();
            DataSet ds = new DataSet();
            if (reportKind == "SGY" || reportKind == "SGYM")//省公医
            {
                ds = pub.GetShengGfhzReport(this.txtStaticMonth.GetStaticMonth().ToString(), reportKind);
            }
            else if (reportKind == "SSGY" || reportKind == "SQGY")
            {
                ds = pub.GetGfhzReportCity(this.txtStaticMonth.GetStaticMonth().ToString(), reportKind);
            }
            else
            {
                ds = pub.GetGfhzReport(this.txtStaticMonth.GetStaticMonth().ToString(), reportKind);
            }
            if (ds == null)
                return;
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    object[] o = new object[dsGfhz.Tables[0].Columns.Count];
                    for (int i = 0; i < o.Length; i++)
                    {
                        o[i] = row.ItemArray[i];
                    }
                    dsGfhz.Tables[0].Rows.Add(o);
                }
                crShengGy cr = new crShengGy();
                crShiGy crSI = new crShiGy();
                crQuGy crQI = new crQuGy();

                int sizes = PaperSizeGetter.Get_PaperSizes("Epson LQ-1600K", "gongfei");
                cr.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);
                crSI.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);
                crQI.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);

                CrystalDecisions.CrystalReports.Engine.TextObject ShiType = crSI.ReportDefinition.ReportObjects["ShiType"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                CrystalDecisions.CrystalReports.Engine.TextObject QuType = crQI.ReportDefinition.ReportObjects["QuType"] as CrystalDecisions.CrystalReports.Engine.TextObject;

               if (reportKind == "SSGY" || reportKind == "SQGY")//市公医&区公医
                {
                    //传递参数报表，标识报表名称 label：ShiType					
                    ShiType.Text = reportName;
                    this.re.ShowReport(this.panel2, crSI, dsGfhz);
                }
                else if (reportKind == "SGY" || reportKind == "SGYM")//省公医
                {
                    //传递参数报表，标识报表名称 label：ShiType	
                    if (reportKind == "SGYM")
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject txtquanmian = cr.ReportDefinition.ReportObjects["quanmian"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                        txtquanmian.Text = "(全免部分)";
                    }
                    ShiType.Text = reportName;
                    this.re.ShowReport(this.panel2, cr, dsGfhz);
                }
                else 
                {
                    QuType.Text = reportName;
                    this.re.ShowReport(this.panel2, crQI, dsGfhz);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        protected override int OnQuery(object sender, object neuObject)
        {
            this.Qurey();
            return base.OnQuery(sender, neuObject);
        }

        private void ucPubReport_Load(object sender, System.EventArgs e)
        {
            this.InitControl();
        }
    }
}

