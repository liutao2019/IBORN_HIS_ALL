using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.PubReport.Components
{
    public partial class ucAnasysPubReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAnasysPubReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucAnasysPubReport_Load);
        }
        enum Col
        {
            PACT_HEAD,
            MZRS,
            MZFY1,
            MZFY2,
            MZFY3,
            MZFY4,
            MZFY5,
            MZFY6,
            MZFY7,
            MZFY8,
            MZFY9,
            MZFY10,
            MZFY11,
            MZTEYAO,
            MZFY13,
            MZFY14,
            MZFY15,
            MZFY16,
            MZFY17,
            MZFY18,
            MZFY19,
            MZFY20,
            MZFY51,
            MZJZZJ,
            MZSJJZ,
            MZCANCERDRUG,       //门诊审批肿瘤药费
            MZOVERLIMITDRUG,    //门诊药费超标金额
            MZSELFPAY,          //门诊自费金额
            MZTOTALFEE,         //门诊医疗费总金额
            ZYRS,
            // ZYTS,
            ZYFY1,
            ZYFY2,
            ZYFY3,
            ZYFY4,
            ZYFY5,
            ZYFY6,
            ZYFY7,
            ZYFY8,
            ZYFY9,
            ZYFY10,
            ZYFY11,
            ZYTEYAO,
            ZYFY13,
            ZYFY14,
            ZYFY15,
            ZYFY16,
            ZYFY17,
            ZYFY18,
            ZYFY19,
            ZYFY20,
            ZYFY51,
            ZYJZZJ,
            ZYSJJZ,
            ZYCANCERDRUG,       //住院审批肿瘤药费
            ZYBEDFEE_JIANHU,    //住院监护床位费
            ZYBEDFEE_CENGLIU,   //住院层流床位费
            ZYOVERLIMITDRUG,    //住院药费超标金额
            ZYCOMPANYPAY,       //住院单位支付金额
            ZYSELFPAY,          //住院自费金额
            ZYTOTALFEE,         //住院医疗费总额
            AMOUNT,
            IS_LX,
            REPORT_KIND,
            REPORT_LABEL,
            SORT_ID

        }

        SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbar.AddToolButton("解封", "解封", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z作废信息, true, false, null);
            return this.toolbar;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "解封")
            {
                if (this.pubMgr.MonthUnLock(this.txtStaticMonth.GetStaticMonth()) == -1)
                {
                    MessageBox.Show("解封失败"+ pubMgr.Err);
                }
                MessageBox.Show("解封成功");
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            ArrayList alPubFee = new ArrayList();
            alPubFee = this.pubMgr.AnasysPubFeeStat(this.txtStaticMonth.GetStaticMonth());
            this.fpSpread1_Sheet1.RowCount = 0;
            if (alPubFee == null)
            {
                MessageBox.Show("没有查到统计数据 " + this.pubMgr.Err);
                return -1;
            }
            int c = 0;
            foreach (SOC.Local.PubReport.Models.PubReportStatic stat in alPubFee)
            {
                c = this.fpSpread1_Sheet1.RowCount;
                this.fpSpread1_Sheet1.AddRows(c, 1);
                AddToFp(stat, c);
            }
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            pubMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (pubMgr.MonthLock(this.txtStaticMonth.GetStaticMonth()) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("封账失败 " + pubMgr.Err);
                return -1;
            }

            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++ )
            {
                SOC.Local.PubReport.Models.PubReportStatic stat = ReadDataFromFp(i);
                stat.ClinicPub.Static_Month = this.txtStaticMonth.GetStaticMonth();

                int iReturn = 0;

                iReturn = pubMgr.UpdatePubFeeStat(stat);
                if (iReturn == 0)
                {
                    iReturn = pubMgr.InsertPubFeeStat(stat);
                }
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("公费统计保存失败 " + pubMgr.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("公费统计保存成功");
            return base.OnSave(sender, neuObject);
        }

        private void AddToFp(SOC.Local.PubReport.Models.PubReportStatic stat, int rowIndex)
        {
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.PACT_HEAD].Text = stat.ClinicPub.Pact.Memo;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZRS].Value = stat.ClinicPub.Amount;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY1].Value = stat.ClinicPub.Bed_Fee;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY2].Value = stat.ClinicPub.YaoPin;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY3].Value = stat.ClinicPub.ChengYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY4].Value = stat.ClinicPub.HuaYan;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY5].Value = stat.ClinicPub.JianCha;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY6].Value = stat.ClinicPub.FangShe;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY7].Value = stat.ClinicPub.ZhiLiao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY8].Value = stat.ClinicPub.ShouShu;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY9].Value = stat.ClinicPub.ShuXue;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY10].Value = stat.ClinicPub.ShuYang;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY11].Value = stat.ClinicPub.TeZhi;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZTEYAO].Value = stat.ClinicPub.SpDrugFeeSj;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY13].Value = stat.ClinicPub.MR;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY14].Value = stat.ClinicPub.CT;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY15].Value = stat.ClinicPub.XueTou;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY16].Value = stat.ClinicPub.ZhenJin;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY17].Value = stat.ClinicPub.CaoYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY18].Value = stat.ClinicPub.TeJian;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY19].Value = stat.ClinicPub.ShenYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY20].Value = stat.ClinicPub.JianHu;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY51].Value = stat.ClinicPub.ShengZhen;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZJZZJ].Value = stat.ClinicPub.Tot_Cost;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZSJJZ].Value = stat.ClinicPub.Pub_Cost;

            #region [2010-02-01] zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZCANCERDRUG].Value = stat.ClinicPub.CancerDrugFee;//门诊审批肿瘤药费
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZOVERLIMITDRUG].Value = stat.ClinicPub.OverLimitDrugFee;//门诊药费超标金额
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZSELFPAY].Value = stat.ClinicPub.SelfPay;//门诊自费金额
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZTOTALFEE].Value = stat.ClinicPub.TotalFee; //门诊医疗费总金额
            #endregion

            //this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col].Text = stat.InPub.Static_Month;
            //this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col].Text = stat.InPub.Pact.ID;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYRS].Value = stat.InPub.Amount;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY1].Value = stat.InPub.Bed_Fee;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY2].Value = stat.InPub.YaoPin;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY3].Value = stat.InPub.ChengYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY4].Value = stat.InPub.HuaYan;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY5].Value = stat.InPub.JianCha;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY6].Value = stat.InPub.FangShe;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY7].Value = stat.InPub.ZhiLiao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY8].Value = stat.InPub.ShouShu;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY9].Value = stat.InPub.ShuXue;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY10].Value = stat.InPub.ShuYang;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY11].Value = stat.InPub.TeZhi;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYTEYAO].Value = stat.InPub.SpDrugFeeSj;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY13].Value = stat.InPub.MR;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY14].Value = stat.InPub.CT;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY15].Value = stat.InPub.XueTou;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY16].Value = stat.InPub.ZhenJin;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY17].Value = stat.InPub.CaoYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY18].Value = stat.InPub.TeJian;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY19].Value = stat.InPub.ShenYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY20].Value = stat.InPub.JianHu;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY51].Value = stat.InPub.ShengZhen;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYJZZJ].Value = stat.InPub.Tot_Cost;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYSJJZ].Value = stat.InPub.Pub_Cost;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.AMOUNT].Value = stat.InPub.User01;
            
            #region [2010-02-01] zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYCANCERDRUG].Value = stat.InPub.CancerDrugFee;//住院审批肿瘤药费
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYBEDFEE_JIANHU].Value = stat.InPub.BedFee_JianHu;//住院监护床位费
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYBEDFEE_CENGLIU].Value = stat.InPub.BedFee_CengLiu;//住院层流床位费
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYOVERLIMITDRUG].Value = stat.InPub.OverLimitDrugFee;//住院药费超标金额
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYCOMPANYPAY].Value = stat.InPub.CompanyPay;//住院单位支付金额
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYSELFPAY].Value = stat.InPub.SelfPay;//住院自费金额
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYTOTALFEE].Value = stat.InPub.TotalFee;//住院医疗费总额
            #endregion
            
            this.fpSpread1_Sheet1.Rows[rowIndex].Tag = stat;
           
        }

        private SOC.Local.PubReport.Models.PubReportStatic ReadDataFromFp(int rowIndex)
        {
            return this.fpSpread1_Sheet1.Rows[rowIndex].Tag as SOC.Local.PubReport.Models.PubReportStatic;
            /*
            //DateTime statMonth = new DateTime(this.dtBegin.Value.Year,this.dtBegin.Value.Month,1);
            stat.ClinicPub.Pact.ID = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.PACT_HEAD].Text;
            //this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.s].Text = statMonth.ToString();
            stat.ClinicPub.Pact.ID = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.PACT_HEAD].Text;
            stat.ClinicPub.Amount =  this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZRS].Value;
            stat.ClinicPub.Bed_Fee = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY1].Value;
            stat.ClinicPub.YaoPin = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY2].Value;
            stat.ClinicPub.ChengYao = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY3].Value;
            stat.ClinicPub.HuaYan = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY4].Value;
            stat.ClinicPub.JianCha = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY5].Value;
            stat.ClinicPub.FangShe = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY6].Value;
            stat.ClinicPub.ZhiLiao = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY7].Value;
            stat.ClinicPub.ShouShu = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY8].Value;
            stat.ClinicPub.ShuXue = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY9].Value;
            stat.ClinicPub.ShuYang = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY10].Value;
            stat.ClinicPub.JieSheng = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY11].Value;
            stat.ClinicPub.GaoYang = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY12].Value;
            stat.ClinicPub.MR = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY13].Value;
            stat.ClinicPub.CT = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY14].Value;
            stat.ClinicPub.XueTou = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY15].Value;
            stat.ClinicPub.ZhenJin = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY16].Value;
            stat.ClinicPub.CaoYao = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY17].Value;
            stat.ClinicPub.TeJian = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY18].Value;
            stat.ClinicPub.ShenYao = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY19].Value;
            stat.ClinicPub.JianHu = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY20].Value;
            stat.ClinicPub.ShengZhen = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZFY51].Value;
            stat.ClinicPub.Tot_Cost = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZJZZJ].Value;
            stat.ClinicPub.Pub_Cost = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.MZSJJZ].Value;

            //this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col].Text = stat.InPub.Static_Month;
            //this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col].Text = stat.InPub.Pact.ID;
            stat.InPub.Amount = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYRS].Value;
            stat.InPub.Bed_Fee = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY1].Value;
            stat.InPub.YaoPin = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY2].Value;
            stat.InPub.ChengYao = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY3].Value;
            stat.InPub.HuaYan = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY4].Value;
            stat.InPub.JianCha = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY5].Value;
            stat.InPub.FangShe = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY6].Value;
            stat.InPub.ZhiLiao = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY7].Value;
            stat.InPub.ShouShu = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY8].Value;
            stat.InPub.ShuXue = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY9].Value;
            stat.InPub.ShuYang = this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY10].Value;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY11].Value = stat.InPub.JieSheng;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY12].Value = stat.InPub.GaoYang;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY13].Value = stat.InPub.MR;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY14].Value = stat.InPub.CT;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY15].Value = stat.InPub.XueTou;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY16].Value = stat.InPub.ZhenJin;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY17].Value = stat.InPub.CaoYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY18].Value = stat.InPub.TeJian;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY19].Value = stat.InPub.ShenYao;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY20].Value = stat.InPub.JianHu;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYFY51].Value = stat.InPub.ShengZhen;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYJZZJ].Value = stat.InPub.Tot_Cost;
            this.fpSpread1_Sheet1.Cells[rowIndex, (int)Col.ZYSJJZ].Value = stat.InPub.Pub_Cost;
            */

        }

        private void SetFpFormat()
        {
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Count= 2;
            this.fpSpread1_Sheet1.Columns.Count = 61;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 29;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "门诊部分";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = Color.Red;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 29).ColumnSpan = 32;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 29).BackColor = Color.Green;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 29).Text = "住院部分";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.PACT_HEAD].Label = "字头";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.MZRS].Label = "门诊人数";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY1].Label = "床位费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY2].Label = "药品";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY3].Label = "成药";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY4].Label = "化验";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY5].Label = "检查";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY6].Label = "放射";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY7].Label = "治疗";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY8].Label = "手术";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY9].Label = "输血";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY10].Label = "输氧";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY11].Label = "特治";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZTEYAO].Label = "特药";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY13].Label = "MR";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY14].Label = "CT";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY15].Label = "血透";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY16].Label = "诊金";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY17].Label = "草药";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY18].Label = "特检费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY19].Label = "审药费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY20].Label = "监护费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZFY51].Label = "省诊";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZJZZJ].Label = "合计金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[ (int)Col.MZSJJZ].Label = "实际金额";

            #region [2010-02-01] zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.MZCANCERDRUG].Label = "审批肿瘤药费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.MZOVERLIMITDRUG].Label = "药费超标金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.MZSELFPAY].Label = "自费金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.MZTOTALFEE].Label = "医疗费总金额";
            #endregion

            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYRS].Label = "门诊人数";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY1].Label = "床位费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY2].Label = "药品";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY3].Label = "成药";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY4].Label = "化验";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY5].Label = "检查";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY6].Label = "放射";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY7].Label = "治疗";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY8].Label = "手术";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY9].Label = "输血";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY10].Label = "输氧";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY11].Label = "特治";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYTEYAO].Label = "特药";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY13].Label = "MR";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY14].Label = "CT";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY15].Label = "血透";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY16].Label = "诊金";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY17].Label = "草药";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY18].Label = "特检费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY19].Label = "审药费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY20].Label = "监护费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYFY51].Label = "省诊";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYJZZJ].Label = "合计金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYSJJZ].Label = "实际金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.AMOUNT].Label = "住院天数";

            #region [2010-02-01] zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYCANCERDRUG].Label = "住院审批肿瘤药费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYBEDFEE_JIANHU].Label = "住院监护床位费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYBEDFEE_CENGLIU].Label = "住院层流床位费";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYOVERLIMITDRUG].Label = "住院药费超标金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYCOMPANYPAY].Label = "住院单位支付金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYSELFPAY].Label = "住院自费金额";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(int)Col.ZYTOTALFEE].Label = "住院医疗费总额";
            #endregion

            FarPoint.Win.Spread.CellType.TextCellType ctText = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1_Sheet1.Columns[(int)Col.PACT_HEAD].CellType = ctText;
        }

        void ucAnasysPubReport_Load(object sender, EventArgs e)
        {
            SetFpFormat();
            SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();
            FS.FrameWork.Models.NeuObject staticTime = pubMgr.GetLastStaticTime();
            this.txtStaticMonth.Text = staticTime.ID + staticTime.Memo;
        }
    }
}
