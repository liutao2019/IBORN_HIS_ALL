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
    public partial class ucPubReportPanel : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPubReportPanel()
        {
            InitializeComponent();
        }
       
        #region 变量
        /*
        public string Begin
        {
            get
            {
                return this.dtBegin.Value.ToShortDateString() + " 0:00:00";
            }
        }
        public string End
        {
            get
            {
                return this.dtEnd.Value.ToShortDateString() + " 23:59:59";
            }
        }
        */
        private SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();
        #endregion

        int GetDays(DateTime dtBegin, DateTime dtEnd)
        {
            int days;
            DateTime dtE = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 0, 0, 0);
            DateTime dtS = new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day, 0, 0, 0);
            TimeSpan t = dtE - dtS;
            if (t.Days == 0)
            {
                days = 1;
            }
            else
            {
                days = t.Days;
            }
            return days;
        }

        #region 事件
        private void Query()
        {
            if (this.tabControl1.SelectedTab == this.tbDetial || this.tabControl1.SelectedTab == this.tbExportPage)
            {
                #region 住院明细
                this.crystalReportViewer1.ReportSource = null;
                int Number = 0;
                for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    for (int j = 0; j < this.tvList.Nodes[i].GetNodeCount(true); j++)
                        if (this.tvList.Nodes[i].Nodes[j].Checked == true)
                        {
                            Number = i;
                            break;
                        }
                #region 省公医
                if (Number == 0)
                {
                    //省公医
                    dsMedFeeDetail dsShengGY = new dsMedFeeDetail();
                    dsPubFeeDetailExport dsShengGYExport = new dsPubFeeDetailExport(); //add by hgx 2007.4.26

                    //				foreach(TreeNode node in this.tvList.Nodes)
                    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    {
                        for (int j = 0; j < this.tvList.Nodes[i].GetNodeCount(true); j++)
                            if (this.tvList.Nodes[i].Nodes[j].Checked)
                            {
                                SOC.Local.PubReport.Models.StatReport s = (SOC.Local.PubReport.Models.StatReport)this.tvList.Nodes[i].Nodes[j].Tag;

                                DataSet ds = this.pubMgr.GetDetailByCardForPro(this.txtStaticMonth.GetStaticMonth().ToString(), s.Card_No, s.stat.ID);
                                if (ds == null)
                                {
                                    MessageBox.Show(this.pubMgr.Err);
                                    return;
                                }
                                # region 插入数据
                                string pa = "";
                                try
                                {
                                    foreach (DataRow row in ds.Tables[0].Rows)
                                    {
                                        DataRow rowTot = dsShengGY.Tables[0].NewRow();
                                        DateTime dbegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[33].ToString());
                                        DateTime dend = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[34].ToString());
                                        rowTot["dtStart"] = dbegin;
                                        rowTot["dtEnd"] = dend;
                                        rowTot["workUnit"] = row[39];
                                        rowTot["name"] = row[38];
                                        rowTot["mCardNo"] = row[35];
                                        rowTot["dayBeginEnd"] = dbegin.Month.ToString().PadLeft(2, '0') + dbegin.Day.ToString().PadLeft(2, '0') +
                                            "-" + dend.Month.ToString().PadLeft(2, '0') + dend.Day.ToString().PadLeft(2, '0');
                                        //rowTot["days"] = row[4];
                                        rowTot["days"] = row[4];
                                        rowTot["medicine"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[6].ToString())//药品
                                            + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[23].ToString())//审药
                                            + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());//30%
                                        rowTot["chengyao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[7].ToString());//中药
                                        rowTot["caoyao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[21].ToString());//草药

                                        rowTot["huayan"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[8].ToString());//化验
                                        rowTot["normalCheck"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[9].ToString())//检查
                                            + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString())//特检
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[10].ToString())//放射
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[17].ToString())//MR
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[18].ToString())//ct
                                          + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString());//高检
                                        rowTot["normalCure"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[24].ToString())//监护 ->住院费
                                         + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[11].ToString())//治疗
                                            + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[15].ToString())//特殊治疗
                                            + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[14].ToString())//输氧
                                            + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[19].ToString());   //血透
                                            rowTot["shoushu"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[12].ToString());//手术
                                            rowTot["shuxue"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString());//输血
                                                 
                                                
                                                // + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[16].ToString())//特治疗比例
                                         
                                               rowTot["diagFee"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString());//诊金
                                            
                                        //rowTot["normalBed"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());//床位费
                                        rowTot["keepFee"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());//床位费

                                        rowTot["totCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString());/*总费用*/
                                        rowTot["payPercent"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负比例
                                        rowTot["realCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//实报金额;
                                        if (row[37].ToString() == "0")
                                        {
                                            rowTot["memo"] = "出院";
                                        }
                                        else
                                        {
                                            rowTot["memo"] = "在院";
                                        }
                                        rowTot["payKind"] = s.stat.Name;
                                        switch (s.stat.Name)
                                        {
                                            case "80":
                                            case "81":
                                            case "82":
                                            case "83":
                                                rowTot["type"] = "公";
                                                break;
                                            case "J8":
                                                rowTot["type"] = "交";
                                                break;
                                            case "90":
                                                rowTot["type"] = "统筹";
                                                break;
                                        }
                                        rowTot["ownPay"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString())
                                            - Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//自付
                                       // rowTot["diagFee"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[25].ToString());//诊金--省诊
                                        //rowTot["totCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString())
                                        //    + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[25].ToString());//实记账/*实报+省诊*/
                                        pa = row[40].ToString();
                                        rowTot["patientNo"] = row[40].ToString().Substring(3);//住院号

                                        dsShengGY.Tables[0].Rows.Add(rowTot);

                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("aaaa" + pa);
                                }
                                # endregion
                                SetExport(ref dsShengGYExport, ds);    //add by hgx 2007.4.26 公费数据导出

                            }
                    }

                    SetDataSour(this.fpSpread1_Sheet1, dsShengGYExport);//add by hgx 2007.4.26 公费数据导出

                    CrMedFeeDetail rpt = new CrMedFeeDetail();
                    CrystalDecisions.CrystalReports.Engine.TextObject txtTjyf = rpt.ReportDefinition.ReportObjects["tjyf"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                    txtTjyf.Text = this.txtStaticMonth.GetStaticMonth().Year.ToString() + "年" + this.txtStaticMonth.GetStaticMonth().Month.ToString() + "月";

                    int sizes = PaperSizeGetter.Get_PaperSizes("Epson LQ-1600K", "gongfei");
                    //将该size 赋值给报表对象
                    rpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);

                    rpt.SetDataSource(dsShengGY);
                    this.crystalReportViewer1.ReportSource = rpt;
                    this.crystalReportViewer1.RefreshReport();
                }
                #endregion
                
                #region 本院与区公医一样 将原本院屏蔽
                //if (Number == 1)
                //{
                //    //省公医
                //    dsMedFeeDetailLocal dsThisHos = new dsMedFeeDetailLocal();
                //    dsPubFeeDetailExport dsThisHosExport = new dsPubFeeDetailExport(); //add by hgx 2007.4.26

                //    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                //    {
                //        for (int j = 0; j < this.tvList.Nodes[i].GetNodeCount(true); j++)
                //            if (this.tvList.Nodes[i].Nodes[j].Checked)
                //            {
                //                SOC.Local.PubReport.Models.StatReport s = (SOC.Local.PubReport.Models.StatReport)this.tvList.Nodes[i].Nodes[j].Tag;

                //                DataSet ds = this.pubMgr.GetDetailForThisHos(this.txtStaticMonth.GetStaticMonth().ToString(), s.Card_No, s.Memo);
                //                if (ds == null)
                //                {
                //                    MessageBox.Show(this.pubMgr.Err);
                //                    return;
                //                }
                //                # region 插入数据
                //                foreach (DataRow row in ds.Tables[0].Rows)
                //                {
                //                    DataRow rowTot = dsThisHos.Tables[0].NewRow();
                //                    DateTime dbegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[33].ToString());
                //                    DateTime dend = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[34].ToString());
                //                    rowTot["dtStart"] = this.txtStaticMonth.GetStaticMonth();
                //                    rowTot["dtEnd"] = this.txtStaticMonth.GetStaticMonth();
                //                    rowTot["workUnit"] = row[39];
                //                    rowTot["name"] = row[38];
                //                    rowTot["mCardNo"] = row[35];
                //                    rowTot["dayBeginEnd"] = dbegin.Month.ToString().PadLeft(2, '0') + dbegin.Day.ToString().PadLeft(2, '0') +
                //                        "-" + dend.Month.ToString().PadLeft(2, '0') + dend.Day.ToString().PadLeft(2, '0');
                //                    rowTot["days"] = row[4];
                //                    rowTot["medicine"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[6].ToString())//药品
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[7].ToString())//中药
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[21].ToString())//草药
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[23].ToString())//审药
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());
                //                    rowTot["normalCheck"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[8].ToString())//化验
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[9].ToString())//检查
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[10].ToString())//放射
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[17].ToString())//MR
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[18].ToString());//CT
                //                    rowTot["normalCure"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[11].ToString())//治疗
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[12].ToString())//手术
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString())//输血
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[14].ToString())//输氧
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[15].ToString())//接生
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[16].ToString())//高氧
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[19].ToString())//血透
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString())//诊金
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString());//特检									
                //                    rowTot["normalBed"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());//床位费
                //                    rowTot["keepFee"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[24].ToString());//监护

                //                    rowTot["subTot"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString());/*总费用-省诊*/
                //                    rowTot["payPercent"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负比例
                //                    rowTot["realCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//实报金额
                //                    if (row[37].ToString() == "0")
                //                    {
                //                        rowTot["memo"] = "出院";
                //                    }
                //                    else
                //                    {
                //                        rowTot["memo"] = "在院";
                //                    }
                //                    rowTot["payKind"] = s.stat.Name;
                //                    rowTot["ownPay"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString())
                //                        - Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//自付
                //                    rowTot["diagFee"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[25].ToString());//诊金--省诊
                //                    rowTot["totCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString())
                //                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[25].ToString());//实记账
                //                    rowTot["patientNo"] = row[40].ToString().Substring(3);
                //                    dsThisHos.Tables[0].Rows.Add(rowTot);

                //                }
                //                # endregion
                //                SetExport(ref dsThisHosExport, ds);    //add by hgx 2007.4.26 公费数据导出

                //            }
                //    }

                //    SetDataSour(this.fpSpread1_Sheet1, dsThisHosExport);//add by hgx 2007.4.26 公费数据导出

                //    CrMedFeeDetailLocal rpt = new CrMedFeeDetailLocal();
                //    rpt.SetDataSource(dsThisHos);
                //    this.crystalReportViewer1.ReportSource = rpt;
                //    this.crystalReportViewer1.RefreshReport();
                //}
                #endregion

                #region 特托简表
                if (Number == 2)
                {
                    //特托简表
                    dsTTDetail dstt = new dsTTDetail();
                    dsPubFeeDetailExport dsttExport = new dsPubFeeDetailExport(); //add by hgx 2007.4.26

                    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    {
                        for (int j = 0; j < this.tvList.Nodes[i].GetNodeCount(true); j++)
                            if (this.tvList.Nodes[i].Nodes[j].Checked)
                            {
                                //							SOC.Local.PubReport.Models.StatReport s = (SOC.Local.PubReport.Models.StatReport)this.tvList.Nodes[i].Nodes[j].Tag;

                                DataSet ds = this.pubMgr.GetDetailForTT(txtStaticMonth.GetStaticMonth().ToString());
                                if (ds == null)
                                {
                                    MessageBox.Show(this.pubMgr.Err);
                                    return;
                                }
                                # region 插入数据
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    DataRow rowTot = dstt.Tables[0].NewRow();
                                    DateTime dbegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[33].ToString());
                                    DateTime dend = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[34].ToString());
                                    rowTot["NO"] = row[35];
                                    rowTot["姓名"] = row[38];
                                    rowTot["住院号"] = row[40].ToString().Substring(3);
                                    rowTot["病区"] = row[41].ToString();
                                    rowTot["起始日"] = dbegin.Year.ToString().Substring(2) + dbegin.Month.ToString().PadLeft(2, '0') + dbegin.Day.ToString().PadLeft(2, '0') +
                                        "-" + dend.Year.ToString().Substring(2) + dend.Month.ToString().PadLeft(2, '0') + dend.Day.ToString().PadLeft(2, '0');
                                    rowTot["截止日"] = dend;//.Year.ToString().Substring(2)+dend.Month.ToString().PadLeft(2,'0')+dend.Day.ToString().PadLeft(2,'0');
                                    rowTot["天数"] = row[4];
                                    rowTot["住院费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());//床位费
                                    rowTot["药费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[6].ToString())//药品
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[7].ToString())//中药
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[23].ToString())//审药
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());
                                    rowTot["化验费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[8].ToString());//化验
                                    rowTot["CT费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[18].ToString());//CT
                                    rowTot["手术"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[12].ToString());//手术
                                    rowTot["治疗费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[10].ToString())//放射
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[11].ToString())//治疗
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[15].ToString())//接生
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[16].ToString())//高氧
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[17].ToString())//MR
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[19].ToString())//血透
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString())//诊金
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString())//特检
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[21].ToString())//草药;
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[25].ToString());//省诊

                                    rowTot["检查费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[9].ToString());//检查
                                    rowTot["监护费"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[24].ToString());//监护费
                                    rowTot["输血"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString());//输血
                                    rowTot["输氧"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[14].ToString());//输氧

                                    rowTot["合计"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString());/*总记账*/
                                    rowTot["自付比例"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负比例
                                    rowTot["自付金额"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString())
                                        - Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());
                                    rowTot["实报金额"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//实报金额

                                    if (row[37].ToString() == "0")
                                    {
                                        rowTot["备注"] = "出院";
                                    }
                                    else
                                    {
                                        rowTot["备注"] = "在院";
                                    }
                                    dstt.Tables[0].Rows.Add(rowTot);




                                }
                                # endregion

                                SetExport(ref dsttExport, ds);    //add by hgx 2007.4.26 公费数据导出

                            }
                    }

                    SetDataSour(this.fpSpread1_Sheet1, dsttExport);//add by hgx 2007.4.26 公费数据导出

                    crTTDetail rpt = new crTTDetail();
                    rpt.SetDataSource(dstt);
                    this.crystalReportViewer1.ReportSource = rpt;
                    this.crystalReportViewer1.RefreshReport();

                }
                #endregion
                #region 市公医
                if (Number == 3)
                {//市公医
                    dsPubFeeDetail dsShiGY = new dsPubFeeDetail();
                    dsPubFeeDetailExport dsShiGYExport = new dsPubFeeDetailExport(); //add by hgx 2007.4.26

                    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    {
                        for (int j = 0; j < this.tvList.Nodes[i].GetNodeCount(true); j++)
                        {
                            if (this.tvList.Nodes[i].Nodes[j].Checked)
                            {
                                SOC.Local.PubReport.Models.StatReport s = (SOC.Local.PubReport.Models.StatReport)this.tvList.Nodes[i].Nodes[j].Tag;
                                DataSet ds = this.pubMgr.GetDetailByCardForCity(this.txtStaticMonth.GetStaticMonth().ToString(), s.Card_No);
                                if (ds == null)
                                {
                                    MessageBox.Show(this.pubMgr.Err);
                                    return;
                                }
                                //neusoft.HISFC.Management.Case.Diagnose diag = new neusoft.HISFC.Management.Case.Diagnose();
                                #region 数据赋值
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    DataRow rowTot = dsShiGY.Tables[0].NewRow();
                                    DateTime dtBegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[33].ToString());
                                    DateTime dtEnd = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[34].ToString());
                                    //if (row[41].ToString() == "Y")
                                    //{
                                    //    //										ArrayList al =diag.QueryMainDiagnose(row[32].ToString(),true,neusoft.HISFC.Management.Case.frmTypes.DOC);
                                    //    //										if(al==null)
                                    //    //										{
                                    //    //											MessageBox.Show("出错");
                                    //    //											return;
                                    //    //										}
                                    //    //										if(al.Count==0)
                                    //    //										{
                                    //    //											rowTot["diagnose"]="";
                                    //    //										}
                                    //    //										else
                                    //    //										{
                                    //    //											neusoft.HISFC.Object.Case.Diagnose obj= al[0] as neusoft.HISFC.Object.Case.Diagnose;
                                    //    //											rowTot["diagnose"]=obj.DiagInfo.ICD10.Name;
                                    //    //										}
                                    //    rowTot["diagnose"] = row[42];
                                    //}
                                    //else
                                    //{
                                    //    rowTot["diagnose"] = "";
                                    //}
                                    rowTot["dtStart"] =  dtBegin.ToShortDateString();
                                    
                                    rowTot["dtEnd"] = dtEnd.ToShortDateString();
                                    rowTot["inpatientNo"] = row[40].ToString().Substring(3);//住院号
                                    rowTot["workUnit"] = row[39];//工作单位
                                    rowTot["name"] = row[38];
                                    rowTot["mCardNo"] = row[35];
                                    //rowTot["dayBeginEnd"] = dbegin.Month.ToString().PadLeft(2, '0') + dbegin.Day.ToString().PadLeft(2, '0') +
                                     //   "-" + dend.Month.ToString().PadLeft(2, '0') + dend.Day.ToString().PadLeft(2, '0');
                                    //;

                                    //rowTot["days"] = row[4];
                                   // DateTime dtE = new DateTime(dtEnd.Year,dtEnd.Month,dtEnd.Day,0,0,0);
                                   // DateTime dtS = new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day, 0, 0, 0);
                                   // TimeSpan t = dtE - dtS;
                                   //if (t.Days == 0)
                                   //{
                                   //    rowTot["days"] = 1;
                                   //}
                                   //else
                                   //{
                                   //    rowTot["days"] = t.Days;
                                   //}

                                    rowTot["days"] = row[4];
                                    rowTot["teshuyao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());//30%
                                    rowTot["xiyao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[6].ToString());//药品
                                    rowTot["zhongyao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[7].ToString())//中药
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[21].ToString());//草药
                                        //+ Neusoft.FrameWork.Function.NConvert.ToDecimal(row[23].ToString())//审药
                                        //- Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());//30%

                                    rowTot["tejian"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString());
                                    rowTot["jiancha"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[8].ToString())//化验
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[9].ToString())//检查
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[10].ToString())//CT
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[17].ToString())//MR
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[18].ToString());//检查;
                                    //rowTot["over150"] = 0;
                                    rowTot["zhiliao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[11].ToString())//治疗
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[14].ToString())//输氧
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[15].ToString())//接生
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[16].ToString())//高氧
                                        +Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString())//输血
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString());//诊金
                                    rowTot["shoushu"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[12].ToString());//手术
                                    rowTot["chuangwei"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());//床位费;
                                       // + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString());//诊金
                                    rowTot["jianhu"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[24].ToString());//监护;
                                    rowTot["huli1"] = 0;
                                    rowTot["huli2"] = 0;
                                    //rowTot["blood"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString());//输血
                                    rowTot["tot_cost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString());//总费用
                                    rowTot["payPercent"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负比例;
                                    rowTot["realCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//实报金额;
                                    rowTot["owncost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString()) - Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());

                                    //if (row[37].ToString() == "0")
                                    //{
                                    //    rowTot["checkCost"] = "出院";
                                    //}
                                    //else
                                    //{
                                    //    rowTot["checkCost"] = "在院";
                                    //}
                                    rowTot["paykind"] = s.stat.Name;
                                    dsShiGY.Tables[0].Rows.Add(rowTot);
                                }

                                #endregion

                                SetExport(ref dsShiGYExport, ds);    //add by hgx 2007.4.26 公费数据导出
                            }
                        }
                    }
                    CrPubFeeDetail rpt = new CrPubFeeDetail();
                    CrystalDecisions.CrystalReports.Engine.TextObject txtObj = rpt.ReportDefinition.ReportObjects["tjyf"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                    txtObj.Text = this.txtStaticMonth.GetStaticMonth().Year + "年" + this.txtStaticMonth.GetStaticMonth().Month.ToString() + "月";
                    rpt.SetDataSource(dsShiGY);
                    int sizes = PaperSizeGetter.Get_PaperSizes("Epson LQ-1600K", "gongfei");
                    //将该size 赋值给报表对象
                    rpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);
                    this.crystalReportViewer1.ReportSource = rpt;
                    this.crystalReportViewer1.RefreshReport();

                    //this.fpSpread1_Sheet1.DataSource=dsShiGYExport;  //add by hgx 2007.4.26 公费数据导出
                    SetDataSour(this.fpSpread1_Sheet1, dsShiGYExport);//add by hgx 2007.4.26 公费数据导出

                }
                #endregion
                #region 区公医
                if (Number == 4 || Number == 1)
                {//区公医
                    dsPubFeeKindDetail dsQuGY = new dsPubFeeKindDetail();
                    dsPubFeeDetailExport dsQuGYExport = new dsPubFeeDetailExport(); //add by hgx 2007.4.26

                    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    {
                        for (int j = 0; j < this.tvList.Nodes[i].GetNodeCount(true); j++)
                        {
                            if (this.tvList.Nodes[i].Nodes[j].Checked)
                            {

                                DataSet ds = new DataSet();
                                if (Number == 4)
                                {
                                   ds = this.pubMgr.GetDetailForArea(this.txtStaticMonth.GetStaticMonth().ToString(), this.tvList.Nodes[i].Nodes[j].Tag.ToString());
                                }
                                else if (Number == 1)
                                {
                                 ds = this.pubMgr.GetDetailForLocal(this.txtStaticMonth.GetStaticMonth().ToString(), this.tvList.Nodes[i].Nodes[j].Tag.ToString());
                                }
                                if (ds == null)
                                {
                                    MessageBox.Show(this.pubMgr.Err);
                                    return;
                                }
                                #region 数据赋值
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    DataRow rowTot = dsQuGY.Tables[0].NewRow();
                                    DateTime dbegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[33].ToString());
                                    DateTime dend = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[34].ToString());
                                    rowTot["paykind"] = this.tvList.Nodes[i].Nodes[j].Text;
                                    rowTot["dtStart"] = this.txtStaticMonth.GetStaticMonth();
                                    ;
                                    rowTot["dtEnd"] = this.txtStaticMonth.GetStaticMonth();
                                    rowTot["inpatientNo"] = row[40].ToString().Substring(3);//住院号
                                    switch (row[3].ToString())
                                    {
                                        case "Y0":
                                        case "Y10":
                                        case "Y11":
                                            rowTot["workUnit"] = "广州医学院附属肿瘤医院";//工作单位
                                            break;
                                        case "H1":
                                            rowTot["workUnit"] = "中大北校区";
                                            break;
                                        case "H2":
                                            rowTot["workUnit"] = "中大眼科";
                                            break;
                                        default:
                                            rowTot["workUnit"] = row[39];//工作单位
                                            break;
                                    }
                                    
                                    rowTot["name"] = row[38];
                                    rowTot["mCardNo"] = row[35];
                                    rowTot["dayBeginEnd"] = dbegin.Year.ToString().Substring(2,2) + dbegin.Month.ToString().PadLeft(2, '0') + dbegin.Day.ToString().PadLeft(2, '0') +
                                        "-" + dend.Year.ToString().Substring(2, 2) + dend.Month.ToString().PadLeft(2, '0') + dend.Day.ToString().PadLeft(2, '0');
                                    //DateTime dtB = new DateTime(dbegin.Year, dbegin.Month, dbegin.Day, 0, 0, 0);
                                    //DateTime dtE = new DateTime(dend.Year, dend.Month, dend.Day, 0, 0, 0);
                                    //TimeSpan ts = dtE - dtB;
                                    //if (ts.Days == 0)
                                    //{
                                    //    rowTot["days"] = 1;
                                    //}
                                    //rowTot["days"] = ts.Days;

                                    rowTot["days"] = row[4];

                                    rowTot["importMed"] = 0;
                                    rowTot["medicine"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[6].ToString()) + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());//药品(包含特殊药品)
                                    rowTot["zhongyao"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[7].ToString())//中药
                                       + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[21].ToString());//草药
                                        
                                    //+ Neusoft.FrameWork.Function.NConvert.ToDecimal(row[23].ToString())//审药
                                        //- Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString());//30%
                                    rowTot["specialCheck"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString());//特检
                                    rowTot["normalCheck"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[8].ToString())
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[9].ToString())
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[10].ToString())
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[17].ToString())
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[18].ToString());//检查;
                                    rowTot["over150"] = 0;
                                    rowTot["normalCure"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[11].ToString())//治疗
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString())//输血
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[12].ToString())//手术
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[14].ToString())//输氧
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[15].ToString())//接生
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[16].ToString())//高氧
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[19].ToString());
                                    rowTot["normalBed"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString())//床位费;
                                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString());//诊金
                                    rowTot["specialBeed"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[24].ToString());//监护;
                                    rowTot["firstDeal"] = 0;
                                    rowTot["secDeal"] = 0;
                                    //rowTot["blood"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString());//输血
                                    rowTot["tot_cost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString());//总费用
                                    rowTot["payPercent"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负比例;
                                    rowTot["realCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//实报金额;;
                                    if (row[37].ToString() == "0")
                                    {
                                        rowTot["checkCost"] = "出院";
                                    }
                                    else
                                    {
                                        rowTot["checkCost"] = "在院";
                                    }
                                    //rowTot["payKind"] = row[41].ToString();
                                    dsQuGY.Tables[0].Rows.Add(rowTot);
                                #endregion

                                    SetExport(ref dsQuGYExport, ds);    //add by hgx 2007.4.26 公费数据导出

                                }
                            }
                        }
                    }
                    SetDataSour(this.fpSpread1_Sheet1, dsQuGYExport);//add by hgx 2007.4.26 公费数据导出


                    CrPubFeeKindDetail rpt = new CrPubFeeKindDetail();
                    rpt.SetDataSource(dsQuGY);
                    int sizes = PaperSizeGetter.Get_PaperSizes("Epson LQ-1600K","gongfei");
                    //将该size 赋值给报表对象
                    rpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(sizes);
                    this.crystalReportViewer1.ReportSource = rpt;
                    this.crystalReportViewer1.RefreshReport();
                }
                #endregion
                #endregion
            }
            else if (this.tabControl1.SelectedTab == this.tbSum)
            {
                #region 省公医
                //this.ucSIGYRep1.SetValue(this.Begin, this.End);
                #endregion
            }
        }

        private void Print()
        {
            if (this.tabControl1.SelectedTab == this.tbDetial)
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument p = this.crystalReportViewer1.ReportSource as CrystalDecisions.CrystalReports.Engine.ReportDocument;
                p.PrintOptions.PrinterName = "Epson LQ-1600K";
                p.PrintToPrinter(1, true, 0, 0);
            }
            else if (this.tabControl1.SelectedTab == this.tbSum)
            {
                this.ucSIGYRep1.Print();
            }

        }


        private void Export()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                dlg.FileName = "012GY" + this.txtStaticMonth.Text + "01";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 导出数据赋值 add by hgx 2007.4.27
        /// </summary>
        /// <param name="dsTmp">Output</param>
        /// <param name="ds">Input dataset</param>
        private void SetExport(ref dsPubFeeDetailExport dsTmp, DataSet ds)
        {
            try
            {
                #region 数据赋值
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataRow rowTot = dsTmp.Tables[0].NewRow();
                    DateTime dbegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[33].ToString());
                    DateTime dend = Neusoft.FrameWork.Function.NConvert.ToDateTime(row[34].ToString());

                    rowTot["workUnit"] = row[39];//工作单位
                    rowTot["name"] = row[38];
                    rowTot["mCardNo"] = row[35];

                    rowTot["dtStart"] = Neusoft.FrameWork.Function.NConvert.ToDateTime(dbegin);
                    rowTot["dtEnd"] = rowTot["dtEnd"] = Neusoft.FrameWork.Function.NConvert.ToDateTime(dend);
                    rowTot["days"] = row[4];
                    //rowTot["inpatientNo"]=row[40].ToString().Substring(3);//住院号		
                    rowTot["medicine1"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[6].ToString())//药品
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[36].ToString())//30%
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[23].ToString());//审药
                    rowTot["medicine2"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[7].ToString())//中药
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[21].ToString());//草药

                    rowTot["zhenjin"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[19].ToString())//诊金
                       + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[25].ToString());//省诊金


                    rowTot["normalCheck"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[9].ToString())//检查
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[10].ToString())//CT
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[17].ToString())//MR
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[18].ToString())//检查;
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[22].ToString());//特检

                    rowTot["normalCure"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[11].ToString())//治疗					
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[14].ToString())//输氧
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[15].ToString())//接生
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[16].ToString());//高氧
                    rowTot["gaojian"] = 0;
                    rowTot["sepecialmed"] = 0;

                    rowTot["operations"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[12].ToString());//手术
                    rowTot["blood"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[13].ToString());//输血
                    rowTot["huayan"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[8].ToString());//化验


                    rowTot["normalBed"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[5].ToString())//床位费;
                        + Neusoft.FrameWork.Function.NConvert.ToDecimal(row[20].ToString());//诊金
                    rowTot["specialBeed"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[24].ToString());//监护;

                    rowTot["tot_cost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString());//总费用
                    rowTot["payPercent"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负比例;
                    rowTot["pay_cost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[26].ToString())
                        * Neusoft.FrameWork.Function.NConvert.ToDecimal(row[30].ToString());//自负金额;

                    rowTot["realCost"] = Neusoft.FrameWork.Function.NConvert.ToDecimal(row[27].ToString());//实报金额;
                    //rowTot["CheckCost"]=0;	
                    dsTmp.Tables[0].Rows.Add(rowTot);
                }

                #endregion
            }
            catch
            {
            }

        }

        private void SetDataSour(FarPoint.Win.Spread.SheetView fpSheet1, dsPubFeeDetailExport dsExport)
        {
            fpSheet1.DataSource = dsExport;
        }
        #endregion

        #region 界面事件
        private void InitTreeView()
        {
            TreeNode root1 = new TreeNode("省公医", 0, 1);
            root1.Tag = "ShengGY";
            this.tvList.Nodes.Add(root1);
            TreeNode root5 = new TreeNode("本院", 0, 1);
            root5.Tag = "ThisHos";
            this.tvList.Nodes.Add(root5);
            TreeNode root4 = new TreeNode("特约单位", 0, 1);
            root4.Tag = "TeYue";
            this.tvList.Nodes.Add(root4);
            TreeNode root2 = new TreeNode("市公医", 0, 1);
            root2.Tag = "ShiGY";
            this.tvList.Nodes.Add(root2);
            TreeNode root3 = new TreeNode("区公医", 0, 1);
            root3.Tag = "QuGY";
            this.tvList.Nodes.Add(root3);

            SOC.Local.PubReport.BizLogic.StatReport statMgr = new SOC.Local.PubReport.BizLogic.StatReport();
            try
            {
                #region 省公医
                ArrayList al = statMgr.GetStatByID("2");//省公医
                if (al == null)
                {
                    MessageBox.Show("获取报表维护出错！" + statMgr.Err);
                    return;
                }
                foreach (SOC.Local.PubReport.Models.StatReport stat in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = stat.stat.Name;
                    node.Tag = stat;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    root1.Nodes.Add(node);

                }
                #endregion
                #region 本院
                al = null;
                al = statMgr.GetStatByID("6");//本院
                if (al == null)
                {
                    MessageBox.Show("获取报表维护出错！" + statMgr.Err);
                    return;
                }
                foreach (SOC.Local.PubReport.Models.StatReport stat in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = stat.stat.Name;
                    node.Tag = stat.stat.ID;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    root5.Nodes.Add(node);
                }
                #endregion
                #region 特约单位
                TreeNode node1 = new TreeNode();
                node1.Text = "特托简表";
                node1.Tag = "TeTuo";
                node1.ImageIndex = 1;
                node1.SelectedImageIndex = 2;
                root4.Nodes.Add(node1);
                #endregion
                #region 市公医
                al = null;
                al = statMgr.GetStatByID("3");//市公医
                if (al == null)
                {
                    MessageBox.Show("获取报表维护出错！" + statMgr.Err);
                    return;
                }
                foreach (SOC.Local.PubReport.Models.StatReport stat in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = stat.stat.Name;
                    node.Tag = stat;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    root2.Nodes.Add(node);
                }
                #endregion
                #region 区公医
                al = null;
                al = statMgr.GetStatForArea();//区公医
                if (al == null)
                {
                    MessageBox.Show("获取报表维护出错！" + statMgr.Err);
                    return;
                }
                ArrayList alList = new ArrayList();
                foreach (SOC.Local.PubReport.Models.StatReport stat in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = stat.stat.Name;
                    node.Tag = stat.stat.ID;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    root3.Nodes.Add(node);
                }
                #endregion
                this.tvList.ExpandAll();
            }
            catch
            {
            }

        }
        private void ucReportPanel_Load(object sender, System.EventArgs e)
        {
            Neusoft.FrameWork.Models.NeuObject staticTime = this.pubMgr.GetLastStaticTime();
            this.txtStaticMonth.Text = staticTime.ID + staticTime.Memo;
            this.InitTreeView();
        }

        private void tvList_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            TreeNode tempNode = new TreeNode();
            //如果是父节点
            if (e.Node.Parent == null)
            {
                //如果是选中
                if (e.Node.Checked == true)
                {
                    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    {
                        //当前节点
                        if (this.tvList.Nodes[i] == e.Node)
                        {
                            for (int j = 0; j < e.Node.Nodes.Count; j++)
                                e.Node.Nodes[j].Checked = true;
                        }
                        //其他节点
                        else
                        {
                            this.tvList.Nodes[i].Checked = false;
                            for (int j = 0; j < this.tvList.Nodes[i].Nodes.Count; j++)
                                this.tvList.Nodes[i].Nodes[j].Checked = false;
                        }
                    }
                }
                //没有选中
                else
                {
                    for (int j = 0; j < e.Node.Nodes.Count; j++)
                        e.Node.Nodes[j].Checked = false;
                }
            }
            //如果是子节点
            else
            {
                //如果是选中
                if (e.Node.Checked == true)
                {
                    for (int i = 0; i < this.tvList.Nodes.Count; i++)
                    {
                        //不是当前节点的父节点
                        if (this.tvList.Nodes[i] != e.Node.Parent)
                        {
                            this.tvList.Nodes[i].Checked = false;
                            for (int j = 0; j < this.tvList.Nodes[i].Nodes.Count; j++)
                            {
                                this.tvList.Nodes[i].Nodes[j].Checked = false;
                            }
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {

        }

        #region 工具栏事件

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return base.Export(sender, neuObject);
        }

        #endregion

        #endregion
    }
}
