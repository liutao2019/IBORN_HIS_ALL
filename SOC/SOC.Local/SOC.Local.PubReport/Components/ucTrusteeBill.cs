using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class ucTrusteeBill : UserControl, FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucTrusteeBill()
        {
            InitializeComponent();
        }

        #region 变量
        DateTime dtBegin;
        DateTime dtEnd;
        #endregion

        #region 属性
        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime DtBegin
        {
            set
            {
                dtBegin = value;
            }
        }
        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime DtEnd
        {
            set
            {
                dtEnd = value;
            }
        }
        public FS.HISFC.Models.RADT.PatientInfo PInfo
        {
            set
            {
                patientInfo = value;
            }
        }

        #endregion

        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        //private Local.Report.Management.PubReport pubMgr = new Local.Report.Management.PubReport();
        //private SOC.Local.PubReport.Models.PubReport pubRep = null;
        private SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();
        private SOC.Local.PubReport.Models.PubReport pubRep = null;

        public string PubTotCost = "0";

        #region 方法
        private void SetValue(FS.HISFC.Models.RADT.PatientInfo pInfo, DateTime dtBegin, DateTime dtEnd)
        {
            if (pInfo == null || pInfo.ID == "")
            {
                return;
            }

            SOC.Local.PubReport.Models.PubReport report = new SOC.Local.PubReport.Models.PubReport();

            report = pubMgr.GetPubReport(pInfo.ID, dtBegin.ToString(), dtEnd.ToString());
            if (report == null)
            {
                MessageBox.Show("没有该患者的托收信息!" + pubMgr.Err);
                return;
            }
            this.SetData(pInfo, report);
        }
        private void SetFee(decimal cost, string feeName, ref int i)
        {
            if (cost != 0)
            {
                int j = i / 5;
                this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j), feeName);
                this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j) + 1, cost);
                i++;
            }
        }
        /// <summary>
        /// 设置托收单数据
        /// </summary>
        /// <param name="pinfo">患者信息</param>
        /// <param name="al">费用明细</param>
        public void SetData(FS.HISFC.Models.RADT.PatientInfo pinfo, SOC.Local.PubReport.Models.PubReport report)
        {
            //设置患者信息
            this.lblPatientNo.Text = pinfo.PID.PatientNO.ToString();
            this.lblName.Text = pinfo.Name;
            this.lblSex.Text = pinfo.Sex.Name;//pinfo.Patient.Sex.Name;
            this.lblDept.Text = pinfo.PVisit.PatientLocation.Dept.Name;
            this.lblPrint.Text = DateTime.Now.ToShortDateString();
            //			if(pinfo.PVisit.In_State.ID.ToString() != "I")
            //			{
            //				this.lblDate.Text = "(出院)" + "日期:"+report.Begin.ToShortDateString()+"至"+pinfo.PVisit.Date_Out.ToShortDateString()+"共"+report.Amount.ToString()+"天";
            //			}
            //			else
            //			{
            //				this.lblDate.Text = "日期:"+report.Begin.ToShortDateString()+"至"+report.End.ToShortDateString()+"共"+report.Amount.ToString()+"天";
            //			}

            //if(report.End.Minute == 59 && report.End.Second == 59 && report.End.Hour == 23)

            System.TimeSpan dt = pinfo.PVisit.OutTime.Date.Subtract(report.Begin.Date);


            if (report.IsInHos == "1")//在院
            {
                this.lblDate.Text = "日期:" + report.Begin.ToShortDateString() + "至" + report.End.ToShortDateString() + "共" + report.Amount.ToString() + "天";
            }
            else //出院
            {
                //				this.lblDate.Text = "(出院)" + "日期:"+report.Begin.ToShortDateString()+"至"+report.End.ToShortDateString()+"共"+report.Amount.ToString()+"天";
                //出院的托收单默然统计时间为出院时间
                this.lblDate.Text = "(出院)" + "日期:" + report.Begin.ToShortDateString() + "至" + pinfo.PVisit.OutTime.ToShortDateString() + "共" + (dt.Days+1).ToString() + "天";

            }



            this.lblComp.Text = pinfo.CompanyName;//pinfo.Patient.CompanyName;
            this.lblMedCard.Text = pinfo.SSN;//pinfo.Patient.SSN;


            int i = 0;

            SetFee(report.Bed_Fee, "床位", ref i);
            SetFee(report.YaoPin - report.SpDrugFeeTot, "药品", ref i);
            SetFee(report.ChengYao, "成药", ref i);
            SetFee(report.HuaYan, "化验", ref i);
            SetFee(report.JianCha, "检查", ref i);
            SetFee(report.FangShe, "放射", ref i);
            SetFee(report.ZhiLiao, "治疗", ref i);
            SetFee(report.ShouShu, "手术", ref i);
            SetFee(report.ShuXue, "输血", ref i);
            SetFee(report.ShuYang, "输氧", ref i);
            SetFee(report.JieSheng, "接生", ref i);
            SetFee(report.GaoYang, "高氧", ref i);
            SetFee(report.MR, "MR", ref i);
            SetFee(report.CT, "CT", ref i);
            SetFee(report.XueTou, "血透", ref i);
            SetFee(report.ZhenJin, "诊金", ref i);
            SetFee(report.CaoYao, "草药", ref i);
            SetFee(report.TeJian, "其他", ref i);
            SetFee(report.ShenYao, "护理", ref i);
            SetFee(report.JianHu, "监护", ref i);
            if (report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" ||
                report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4)
            {
                lbszText.Visible = true;
                lbsz.Visible = true;
            }
            else
            {
                lbszText.Visible = false;
                lbsz.Visible = false;
            }
            this.lbsz.Text = report.ShengZhen.ToString();
            this.lblJizhang.Text = (report.Tot_Cost - report.SpDrugFeeTot).ToString();
            this.lblJizhangSp.Text = report.SpDrugFeeTot.ToString();
            this.lblJizhangTot.Text = report.Tot_Cost.ToString();

            this.lblRate.Text = ((int)(report.Pay_Rate * 100)).ToString() + "%";
            this.lblRateSp.Text = "30%";

            this.lblPaycost.Text = (report.Tot_Cost - report.Pub_Cost - Math.Round(report.SpDrugFeeTot * (decimal)0.3, 2)).ToString();
            this.lblPaycostSp.Text = Math.Round(report.SpDrugFeeTot * (decimal)0.3, 2).ToString();
            this.lblPaycostTot.Text = (report.Tot_Cost - report.Pub_Cost).ToString();

            this.lblShiJi.Text = (report.Pub_Cost - (report.SpDrugFeeTot - Math.Round(report.SpDrugFeeTot * (decimal)0.3, 2))).ToString();
            this.lblShiJiSp.Text = (report.SpDrugFeeTot - Math.Round(report.SpDrugFeeTot * (decimal)0.3, 2)).ToString();
            this.lblShiJiTot.Text = report.Pub_Cost.ToString();
            this.lblzhidan.Text = pubMgr.Operator.ID;
            this.lblBillNo.Text = report.Bill_No;

            if (report.SpDrugFeeTot == 0)
            {
                this.lblShiJiSp.Visible = false;
                this.lblShiJiTot.Visible = false;

                this.lblJizhangSp.Visible = false;
                this.lblJizhangTot.Visible = false;

                this.lblRateSp.Visible = false;

                this.lblPaycostSp.Visible = false;
                this.lblPaycostTot.Visible = false;

                this.label17.Visible = false;
                this.label22.Visible = false;
                this.label20.Visible = false;
                this.label19.Visible = false;
                this.label28.Visible = false;
                this.label25.Visible = false;
                this.label23.Visible = false;
            }
            else
            {
                this.lblShiJiSp.Visible = true;
                this.lblShiJiTot.Visible = true;

                this.lblJizhangSp.Visible = true;
                this.lblJizhangTot.Visible = true;

                this.lblRateSp.Visible = true;

                this.lblPaycostSp.Visible = true;
                this.lblPaycostTot.Visible = true;

                this.label17.Visible = true;
                this.label22.Visible = true;
                this.label20.Visible = true;
                this.label19.Visible = true;
                this.label28.Visible = true;
                this.label25.Visible = true;
                this.label23.Visible = true;
            }



        }
        public int BeforePrint()
        {
            FS.HISFC.BizLogic.Fee.InPatient fee = new FS.HISFC.BizLogic.Fee.InPatient();
            if (this.pubRep != null)
            {
                if (this.pubMgr.InsertPubReport(this.pubRep) < 0) return -1;
            }
            return fee.UpdatePrintFlag(this.patientInfo.ID, FrameWork.Function.NConvert.ToDateTime(this.patientInfo.User01), FrameWork.Function.NConvert.ToDateTime(this.patientInfo.User02));

        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            //////			if(this.BeforePrint()== -1)
            //////				return;
            ////FS.neuFC.Interface.Classes.Print p = new FS.neuFC.Interface.Classes.Print();
            ////p.IsDataAutoExtend = false;
            ////p.ControlBorder = FS.neuFC.Interface.Classes.enuControlBorder.None;
            ////FS.Common.Class.Function.GetPageSize("bill", ref p);
            ////p.PrintPage(0, 0, this.panel1);

            //////p.PrintPreview(this.panel1);

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize ps = (new FS.HISFC.BizLogic.Manager.PageSize()).GetPageSize("bill");
            p.SetPageSize(ps);

            p.PrintPage(ps.Left, ps.Top, this.panel1);
        }
        private void Clear()
        {

        }

        #endregion

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }


        #region IControlPrintable 成员

        int FS.FrameWork.WinForms.Classes.IControlPrintable.BeginHorizontalBlankWidth
        {
            get
            {
                return 0;
            }
            set
            {
               
            }
        }

        int FS.FrameWork.WinForms.Classes.IControlPrintable.BeginVerticalBlankHeight
        {
            get
            {
                return 0;
            }
            set
            {
               
            }
        }

        ArrayList FS.FrameWork.WinForms.Classes.IControlPrintable.Components
        {
            get
            {
                return null;
            }
            set
            {
    
            }
        }

        Size FS.FrameWork.WinForms.Classes.IControlPrintable.ControlSize
        {
            get { return new Size(712, 230); }
        }

        object FS.FrameWork.WinForms.Classes.IControlPrintable.ControlValue
        {
            get
            {
                return null;
            }
            set
            {
                this.patientInfo = value as FS.HISFC.Models.RADT.PatientInfo;
                this.SetValue(this.patientInfo, FS.FrameWork.Function.NConvert.ToDateTime(this.patientInfo.User01),
                    FS.FrameWork.Function.NConvert.ToDateTime(this.patientInfo.User02));
           
            }
        }

        int FS.FrameWork.WinForms.Classes.IControlPrintable.HorizontalBlankWidth
        {
            get
            {
                return 0;
            }
            set
            {
               
            }
        }

        int FS.FrameWork.WinForms.Classes.IControlPrintable.HorizontalNum
        {
            get
            {
                return 1;
            }
            set
            {

            }
        }

        bool FS.FrameWork.WinForms.Classes.IControlPrintable.IsCanExtend
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        bool FS.FrameWork.WinForms.Classes.IControlPrintable.IsShowGrid
        {
            get
            {
                return false;
            }
            set
            {
         
            }
        }

        int FS.FrameWork.WinForms.Classes.IControlPrintable.VerticalBlankHeight
        {
            get
            {
                return 20;
            }
            set
            {
 
            }
        }

        int FS.FrameWork.WinForms.Classes.IControlPrintable.VerticalNum
        {
            get
            {
                return 1;
            }
            set
            {

            }
        }

        #endregion
    }
}
