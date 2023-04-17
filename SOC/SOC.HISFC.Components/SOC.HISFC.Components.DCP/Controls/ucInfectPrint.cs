using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// 传染病打印界面

    /// 2012-4-10 
    /// </summary>
    public partial class ucInfectPrint : UserControl
    {
        public ucInfectPrint()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 控件赋值

        /// </summary>
        /// <param name="reportInfo"></param>
        public void SetValues(FS.HISFC.DCP.Object.CommonReport reportInfo)
        {
            this.Clear();

            this.lblReport_No.Text = reportInfo.ReportNO;
            if (reportInfo.CorrectedReportNO == null || reportInfo.CorrectedReportNO == "")
            {
                this.lblCorrect_Flag1.Text = "√";
            }
            else
            {
                this.lblCorrect_Flag2.Text = "√";
            }
            this.lblPatient_Name.Text = reportInfo.Patient.Name;
            this.lblPatient_Parents.Text = reportInfo.PatientParents;
            if (this.lblPatient_Parents.Text == "")
            {
                this.lblPatient_Parents.Text = "__________";
            }
            if (reportInfo.Patient.IDCard != null && reportInfo.Patient.IDCard.Length >= 15)
            {
                this.lblPatient_id1.Text = reportInfo.Patient.IDCard.Substring(0, 1);
                this.lblPatient_id2.Text = reportInfo.Patient.IDCard.Substring(1, 1);
                this.lblPatient_id3.Text = reportInfo.Patient.IDCard.Substring(2, 1);
                this.lblPatient_id4.Text = reportInfo.Patient.IDCard.Substring(3, 1);
                this.lblPatient_id5.Text = reportInfo.Patient.IDCard.Substring(4, 1);
                this.lblPatient_id6.Text = reportInfo.Patient.IDCard.Substring(5, 1);
                this.lblPatient_id7.Text = reportInfo.Patient.IDCard.Substring(6, 1);
                this.lblPatient_id8.Text = reportInfo.Patient.IDCard.Substring(7, 1);
                this.lblPatient_id9.Text = reportInfo.Patient.IDCard.Substring(8, 1);
                this.lblPatient_id10.Text = reportInfo.Patient.IDCard.Substring(9, 1);
                this.lblPatient_id11.Text = reportInfo.Patient.IDCard.Substring(10, 1);
                this.lblPatient_id12.Text = reportInfo.Patient.IDCard.Substring(11, 1);
                this.lblPatient_id13.Text = reportInfo.Patient.IDCard.Substring(12, 1);
                this.lblPatient_id14.Text = reportInfo.Patient.IDCard.Substring(13, 1);
                this.lblPatient_id15.Text = reportInfo.Patient.IDCard.Substring(14, 1);
                if (reportInfo.Patient.IDCard.Length == 18)
                {
                    this.lblPatient_id16.Text = reportInfo.Patient.IDCard.Substring(15, 1);
                    this.lblPatient_id17.Text = reportInfo.Patient.IDCard.Substring(16, 1);
                    this.lblPatient_id18.Text = reportInfo.Patient.IDCard.Substring(17, 1);
                }
            }
            if (reportInfo.Patient.Sex.ID.ToString() == "M")
            {
                this.cbSexM.Checked = true;
            }
            else
            {
                this.cbSexF.Checked = true;
            }
            if (reportInfo.Patient.Birthday.Year > 1800)
            {
                this.lblBirthdayYear.Text = reportInfo.Patient.Birthday.Year.ToString();
                this.lblBirthdayMonth.Text = reportInfo.Patient.Birthday.Month.ToString();
                this.lblBirthdayDay.Text = reportInfo.Patient.Birthday.Day.ToString();
            }
            
            this.lblAge.Text = reportInfo.Patient.Age;
            if (reportInfo.AgeUnit == "岁")
            {
                this.lblAgeUnitYear.Text = "√";
            }
            else if (reportInfo.AgeUnit == "月")
            {
                this.lblAgeUnitMonth.Text = "√";
            }
            else
            {
                this.lblAgeUnitDay.Text = "√";
            }
            this.lblWork_place.Text = reportInfo.Patient.CompanyName;
            this.lblTelephone.Text = reportInfo.Patient.PhoneHome;
            switch (reportInfo.HomeArea)
            {
                case "本县区":
                    this.cbHome_area1.Checked = true;
                    break;
                case "本市其他县区":
                    this.cbHome_area2.Checked = true;
                    break;
                case "本省其他地市":
                    this.cbHome_area3.Checked = true;
                    break;
                case "外省":
                    this.cbHome_area4.Checked = true;
                    break;
                case "港澳台":
                    this.cbHome_area5.Checked = true;
                    break;
                case "外籍":
                    this.cbHome_area6.Checked = true;
                    break;
                default :
                    break;
            }
            this.lblHome_place.Text = reportInfo.Patient.AddressHome;
            switch (reportInfo.Patient.Profession.ID)
            {
                case "0001":
                    this.cbpatient_profession1.Checked = true;
                    break;
                case "0002":
                    this.cbpatient_profession2.Checked = true;
                    break;
                case "0003":
                    this.cbpatient_profession3.Checked = true;
                    break;
                case "0004":
                    this.cbpatient_profession4.Checked = true;
                    break;
                case "0005":
                    this.cbpatient_profession5.Checked = true;
                    break;
                case "0006":
                    this.cbpatient_profession6.Checked = true;
                    break;
                case "0008":
                    this.cbpatient_profession7.Checked = true;
                    break;
                case "0009":
                    this.cbpatient_profession8.Checked = true;
                    break;
                case "0010":
                    this.cbpatient_profession9.Checked = true;
                    break;
                case "0011":
                    this.cbpatient_profession10.Checked = true;
                    break;
                case "0012":
                    this.cbpatient_profession11.Checked = true;
                    break;
                case "0013":
                    this.cbpatient_profession12.Checked = true;
                    break;
                case "0014":
                    this.cbpatient_profession13.Checked = true;
                    break;
                case "0016":
                    this.cbpatient_profession14.Checked = true;
                    break;
                case "0017":
                    this.cbpatient_profession15.Checked = true;
                    break;
                case "0018":
                    this.cbpatient_profession16.Checked = true;
                    break;
                case "0019":
                    this.cbpatient_profession17.Checked = true;
                    break;
                case "0020":
                    this.cbpatient_profession18.Checked = true;
                    break;
                default:
                    this.cbpatient_profession17.Checked = true;
                    break;
            }
            switch (reportInfo.CaseClass1.ID)
            {
                case "3":
                    this.cbCase_class11.Checked = true;
                    break;
                case "1":
                    this.cbCase_class12.Checked = true;
                    break;
                case "2":
                    this.cbCase_class13.Checked = true;
                    break;
                case "4":
                    this.cbCase_class14.Checked = true;
                    break;
                case "5":
                    this.cbCase_class15.Checked = true;
                    break;
                default:
                    break;
            }
            if (reportInfo.Disease.ID.ToString() == "1005" || reportInfo.Disease.ID.ToString() == "1040")
            {
                if (reportInfo.CaseClass2 == "0")
                {
                    this.cbCase_class21.Checked = true;
                }
                else if (reportInfo.CaseClass2 == "1")
                {
                    this.cbCase_class22.Checked = true;
                }
                else
                {
                    this.cbCase_class22.Checked = true;
                }
            }

            this.lblInfect_dateYear.Text = reportInfo.InfectDate.Year.ToString();
            this.lblInfect_dateMonth.Text = reportInfo.InfectDate.Month.ToString();
            this.lblInfect_dateDay.Text = reportInfo.InfectDate.Day.ToString();
            this.lblDiagnosis_dateYear.Text = reportInfo.DiagnosisTime.Year.ToString();
            this.lblDiagnosis_dateMonth.Text = reportInfo.DiagnosisTime.Month.ToString();
            this.lblDiagnosis_dateDay.Text = reportInfo.DiagnosisTime.Day.ToString();
            this.lblDiagnosis_dateHour.Text = reportInfo.DiagnosisTime.Hour.ToString();
            if (reportInfo.DeadDate.Year > 2000)
            {
                this.lblDead_dateYear.Text = reportInfo.DeadDate.Year.ToString();
                this.lblDead_dateMonth.Text = reportInfo.DeadDate.Month.ToString();
                this.lblDead_dateDay.Text = reportInfo.DeadDate.Day.ToString();
            }
            else
            {
                this.lblDead_dateYear.Text = "____";
                this.lblDead_dateMonth.Text = "__";
                this.lblDead_dateDay.Text = "__";
            }
            switch (reportInfo.Disease.ID)
            {
                case "0001":
                    this.cbDisease_J1.Checked = true;
                    break;
                case "0002":
                    this.cbDisease_J2.Checked = true;
                    break;
                case "1001":
                    this.cbDisease_y1.Checked = true;
                    break;
                case "1002":
                    this.cbDisease_y2.Checked = true;
                    break;
                case "1003":
                    this.cbDisease_y3.Checked = true;
                    break;
                case "1045":
                    this.cbDisease_y4.Checked = true;
                    break;
                case "1004":
                    this.cbDisease_y5.Checked = true;
                    break;
                case "1005":
                    this.cbDisease_y6.Checked = true;
                    break;
                case "1006":
                    this.cbDisease_y7.Checked = true;
                    break;
                case "1007":
                    this.cbDisease_y8.Checked = true;
                    break;
                case "1008":
                    this.cbDisease_y9.Checked = true;
                    break;
                case "1009":
                    this.cbDisease_y10.Checked = true;
                    break;
                case "1010":
                    this.cbDisease_y11.Checked = true;
                    break;
                case "1044":
                    this.cbDisease_y12.Checked = true;
                    break;
                case "1011":
                    this.cbDisease_y13.Checked = true;
                    break;
                case "1012":
                    this.cbDisease_y14.Checked = true;
                    break;
                case "1013":
                    this.cbDisease_y15.Checked = true;
                    break;
                case "1014":
                    this.cbDisease_y16.Checked = true;
                    break;
                case "1015":
                    this.cbDisease_y17.Checked = true;
                    break;
                case "1016":
                    this.cbDisease_y18.Checked = true;
                    break;
                case "1017":
                    this.cbDisease_y19.Checked = true;
                    break;
                case "1018":
                    this.cbDisease_y20.Checked = true;
                    break;
                case "1019":
                    this.cbDisease_y21.Checked = true;
                    break;
                case "1020":
                    this.cbDisease_y22.Checked = true;
                    break;
                case "1021":
                    this.cbDisease_y23.Checked = true;
                    break;
                case "1023":
                    this.cbDisease_y24.Checked = true;
                    break;
                case "1022":
                    this.cbDisease_y25.Checked = true;
                    break;
                case "1024":
                    this.cbDisease_y26.Checked = true;
                    break;
                case "1025":
                    this.cbDisease_y27.Checked = true;
                    break;
                case "1026":
                    this.cbDisease_y28.Checked = true;
                    break;
                case "1027":
                    this.cbDisease_y29.Checked = true;
                    break;
                case "1028":
                    this.cbDisease_y30.Checked = true;
                    break;
                case "1029":
                    this.cbDisease_y31.Checked = true;
                    break;
                case "1030":
                    this.cbDisease_y32.Checked = true;
                    break;
                case "1031":
                    this.cbDisease_y33.Checked = true;
                    break;
                case "1032":
                    this.cbDisease_y34.Checked = true;
                    break;
                case "1033":
                    this.cbDisease_y35.Checked = true;
                    break;
                case "1034":
                    this.cbDisease_y36.Checked = true;
                    break;
                case "1035":
                    this.cbDisease_y37.Checked = true;
                    break;
                case "1036":
                    this.cbDisease_y38.Checked = true;
                    break;
                case "1038":
                    this.cbDisease_y39.Checked = true;
                    break;
                case "1037":
                    this.cbDisease_y40.Checked = true;
                    break;
                case "1039":
                    this.cbDisease_y41.Checked = true;
                    break;
                case "1040":
                    this.cbDisease_y42.Checked = true;
                    break;
                case "1041":
                    this.cbDisease_y43.Checked = true;
                    break;
                case "1042":
                    this.cbDisease_y44.Checked = true;
                    break;
                case "1043":
                    this.cbDisease_y45.Checked = true;
                    break;
                case "2001":
                    this.cbDisease_b1.Checked = true;
                    break;
                case "2002":
                    this.cbDisease_b2.Checked = true;
                    break;
                case "2003":
                    this.cbDisease_b3.Checked = true;
                    break;
                case "2004":
                    this.cbDisease_b4.Checked = true;
                    break;
                case "2005":
                    this.cbDisease_b5.Checked = true;
                    break;
                case "2012":
                    this.cbDisease_b6.Checked = true;
                    break;
                case "2007":
                    this.cbDisease_b7.Checked = true;
                    break;
                case "2008":
                    this.cbDisease_b8.Checked = true;
                    break;
                case "2009":
                    this.cbDisease_b9.Checked = true;
                    break;
                case "2010":
                    this.cbDisease_b10.Checked = true;
                    break;
                case "2011":
                    this.cbDisease_b11.Checked = true;
                    break;
                case "2013":
                    this.cbDisease_b12.Checked = true;
                    break;
                case "2014":
                    this.cbDisease_b13.Checked = true;
                    break;
                case "5001":
                    this.cbDisease_q1.Checked = true;
                    break;
                case "5002":
                    this.cbDisease_q2.Checked = true;
                    break;
                case "5003":
                    this.cbDisease_q3.Checked = true;
                    break;
                case "5004":
                    this.cbDisease_q4.Checked = true;
                    break;
                case "5005":
                    this.cbDisease_q5.Checked = true;
                    break;
                case "5006":
                    this.cbDisease_q6.Checked = true;
                    break;
                case "5007":
                    this.cbDisease_q7.Checked = true;
                    break;
                case "5008":
                    this.cbDisease_q8.Checked = true;
                    break;
                case "5009":
                    this.cbDisease_q9.Checked = true;
                    break;
                case "5010":
                    this.cbDisease_q10.Checked = true;
                    break;
                case "5011":
                    this.cbDisease_q11.Checked = true;
                    break;
                case "5012":
                    this.cbDisease_q12.Checked = true;
                    break;
                case "5013":
                    this.cbDisease_q13.Checked = true;
                    break;
                default:
                    break;
            }

            this.lblcorrected_name.Text = "__________";
            this.lblBackBecause.Text = reportInfo.OperCase;
            if (this.lblBackBecause.Text == "")
            {
                this.lblBackBecause.Text = "__________";
            }
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj = con.GetConstant("INFHOPITALNAME", "1");
            if (obj != null && obj.ID != "")
            {
                this.lblHopitalName.Text = obj.Memo;
            }
            obj = con.GetConstant("INFHOPITALPHONE", "1");
            if (obj != null && obj.ID != "")
            {
                this.lblHopitalPhone.Text = obj.Memo;
            }
            if(this.lblHopitalPhone.Text=="")
            {
                this.lblHopitalPhone.Text = "";
                this.lblHopitalPhone.Text = "__________";
            }

            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.Models.Base.Employee empl = person.GetPersonByID(reportInfo.ReportDoctor.ID);
            if (empl != null)
            {
                this.lblReport_doctor.Text = empl.Name;
            }
            this.lblReportDateYear.Text = reportInfo.ReportTime.Year.ToString();
            this.lblReportDateMonth.Text = reportInfo.ReportTime.Month.ToString();
            this.lblReportDateDay.Text = reportInfo.ReportTime.Day.ToString();
            this.lblMark.Text = reportInfo.Memo;
        }
        /// <summary>
        /// 清空控件
        /// </summary>
        private void Clear()
        {
            this.lblReport_No.Text = "";
            this.lblCorrect_Flag1.Text = "";
            this.lblCorrect_Flag2.Text = "";
            this.lblPatient_Name.Text = "";
            this.lblPatient_Parents.Text = "";
            this.lblPatient_id1.Text = "";
            this.lblPatient_id2.Text = "";
            this.lblPatient_id3.Text = "";
            this.lblPatient_id4.Text = "";
            this.lblPatient_id5.Text = "";
            this.lblPatient_id6.Text = "";
            this.lblPatient_id7.Text = "";
            this.lblPatient_id8.Text = "";
            this.lblPatient_id9.Text = "";
            this.lblPatient_id10.Text = "";
            this.lblPatient_id11.Text = "";
            this.lblPatient_id12.Text = "";
            this.lblPatient_id13.Text = "";
            this.lblPatient_id14.Text = "";
            this.lblPatient_id15.Text = "";
            this.lblPatient_id16.Text = "";
            this.lblPatient_id17.Text = "";
            this.lblPatient_id18.Text = "";
            this.cbSexF.Checked = false;
            this.cbSexM.Checked = false;
            this.lblAgeUnitYear.Text = "";
            this.lblBirthdayMonth.Text = "";
            this.lblBirthdayDay.Text = "";
            this.lblAge.Text = "";
            this.lblAgeUnitYear.Text = "";
            this.lblAgeUnitMonth.Text = "";
            this.lblAgeUnitDay.Text = "";
            this.lblWork_place.Text = "";
            this.lblTelephone.Text = "";
            this.cbHome_area1.Checked = false;
            this.cbHome_area2.Checked = false;
            this.cbHome_area3.Checked = false;
            this.cbHome_area4.Checked = false;
            this.cbHome_area5.Checked = false;
            this.cbHome_area6.Checked = false;
            this.lblHome_place.Text = "";
            this.cbpatient_profession1.Checked = false;
            this.cbpatient_profession2.Checked = false;
            this.cbpatient_profession3.Checked = false;
            this.cbpatient_profession4.Checked = false;
            this.cbpatient_profession5.Checked = false;
            this.cbpatient_profession6.Checked = false;
            this.cbpatient_profession7.Checked = false;
            this.cbpatient_profession8.Checked = false;
            this.cbpatient_profession9.Checked = false;
            this.cbpatient_profession10.Checked = false;
            this.cbpatient_profession11.Checked = false;
            this.cbpatient_profession12.Checked = false;
            this.cbpatient_profession13.Checked = false;
            this.cbpatient_profession14.Checked = false;
            this.cbpatient_profession15.Checked = false;
            this.cbpatient_profession16.Checked = false;
            this.cbpatient_profession17.Checked = false;
            this.cbpatient_profession18.Checked = false;

            this.cbCase_class11.Checked = false;
            this.cbCase_class12.Checked = false;
            this.cbCase_class13.Checked = false;
            this.cbCase_class14.Checked = false;
            this.cbCase_class15.Checked = false;
            this.cbCase_class21.Checked = false;
            this.cbCase_class22.Checked = false;

            this.lblInfect_dateYear.Text = "";
            this.lblInfect_dateMonth.Text = "";
            this.lblInfect_dateDay.Text = "";
            this.lblDiagnosis_dateYear.Text = "";
            this.lblDiagnosis_dateMonth.Text = "";
            this.lblDiagnosis_dateDay.Text = "";
            this.lblDiagnosis_dateHour.Text = "";
            this.lblDead_dateYear.Text = "";
            this.lblDead_dateMonth.Text="";
            this.lblDead_dateDay.Text="";

            this.cbDisease_J1.Checked = false;
            this.cbDisease_J2.Checked = false;

            this.cbDisease_y1.Checked = false;
            this.cbDisease_y2.Checked = false;
            this.cbDisease_y3.Checked = false;
            this.cbDisease_y4.Checked = false;
            this.cbDisease_y5.Checked = false;
            this.cbDisease_y6.Checked = false;
            this.cbDisease_y7.Checked = false;
            this.cbDisease_y8.Checked = false;
            this.cbDisease_y9.Checked = false;
            this.cbDisease_y10.Checked = false;
            this.cbDisease_y11.Checked = false;
            this.cbDisease_y12.Checked = false;
            this.cbDisease_y13.Checked = false;
            this.cbDisease_y14.Checked = false;
            this.cbDisease_y15.Checked = false;
            this.cbDisease_y16.Checked = false;
            this.cbDisease_y17.Checked = false;
            this.cbDisease_y18.Checked = false;
            this.cbDisease_y19.Checked = false;
            this.cbDisease_y20.Checked = false;
            this.cbDisease_y21.Checked = false;
            this.cbDisease_y22.Checked = false;
            this.cbDisease_y23.Checked = false;
            this.cbDisease_y24.Checked = false;
            this.cbDisease_y25.Checked = false;
            this.cbDisease_y26.Checked = false;
            this.cbDisease_y27.Checked = false;
            this.cbDisease_y28.Checked = false;
            this.cbDisease_y29.Checked = false;
            this.cbDisease_y30.Checked = false;
            this.cbDisease_y31.Checked = false;
            this.cbDisease_y32.Checked = false;
            this.cbDisease_y33.Checked = false;
            this.cbDisease_y34.Checked = false;
            this.cbDisease_y35.Checked = false;
            this.cbDisease_y36.Checked = false;
            this.cbDisease_y37.Checked = false;
            this.cbDisease_y38.Checked = false;
            this.cbDisease_y39.Checked = false;
            this.cbDisease_y40.Checked = false;
            this.cbDisease_y41.Checked = false;
            this.cbDisease_y42.Checked = false;
            this.cbDisease_y43.Checked = false;
            this.cbDisease_y44.Checked = false;
            this.cbDisease_y45.Checked = false;

            this.cbDisease_b1.Checked = false;
            this.cbDisease_b2.Checked = false;
            this.cbDisease_b3.Checked = false;
            this.cbDisease_b4.Checked = false;
            this.cbDisease_b5.Checked = false;
            this.cbDisease_b6.Checked = false;
            this.cbDisease_b7.Checked = false;
            this.cbDisease_b8.Checked = false;
            this.cbDisease_b9.Checked = false;
            this.cbDisease_b10.Checked = false;
            this.cbDisease_b11.Checked = false;
            this.cbDisease_b12.Checked = false;
            this.cbDisease_b13.Checked = false;

            this.cbDisease_q1.Checked = false;
            this.cbDisease_q2.Checked = false;
            this.cbDisease_q3.Checked = false;
            this.cbDisease_q4.Checked = false;
            this.cbDisease_q5.Checked = false;
            this.cbDisease_q6.Checked = false;
            this.cbDisease_q7.Checked = false;
            this.cbDisease_q8.Checked = false;
            this.cbDisease_q9.Checked = false;
            this.cbDisease_q10.Checked = false;
            this.cbDisease_q11.Checked = false;
            this.cbDisease_q12.Checked = false;
            this.cbDisease_q13.Checked = false;

            this.lblcorrected_name.Text = "";
            this.lblBackBecause.Text = "";
            this.lblHopitalName.Text = "";
            this.lblHopitalPhone.Text = "";
            this.lblReport_doctor.Text = "";
            this.lblReportDateYear.Text = "";
            this.lblReportDateMonth.Text = "";
            this.lblReportDateDay.Text = "";
            this.lblMark.Text = "";
        }

        private void ucInfectPrint_Load(object sender, EventArgs e)
        {

        }
    }
}
