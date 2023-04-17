using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FoShanYDSI.Controls
{
    public partial class ucLiqudDetail : UserControl
    {
        public ucLiqudDetail()
        {
            InitializeComponent();
        }

        private void SetPrintValue(ArrayList alPatientInfo)
        {
            int seqNo = 1;
            decimal totCost = 0;
            decimal pubCost = 0;
            decimal ownCost = 0;
            decimal bigCost = 0;

            int rowCount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatientInfo)
            {
                totCost += patient.FT.TotCost;
                pubCost += patient.FT.PubCost;
                ownCost += patient.FT.OwnCost;
                bigCost += patient.FT.PayCost;

                rowCount = this.neuSpread1_Sheet1.RowCount;
                this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

                //patientInfo.SSN, cardType, socialSerialNumber, patientInfo.Name, dists[0].ToString(), patientInfo.PID.HealthNO,
                //patientInfo.SIMainInfo.RegNo, patientInfo.PVisit.InTime.ToString("yyyyMMdd"), patientInfo.PVisit.OutTime.ToString("yyyyMMdd"), patientInfo.PVisit.ICULocation.ID,
                //patientInfo.BalanceDate.ToString("yyyyMMdd"), patientInfo.ClinicDiagnose, patientInfo.SIMainInfo.ApplyType.Name, patientInfo.FT.TotCost, patientInfo.FT.PubCost, patientInfo.FT.PrepayCost,
                //"21", patientInfo.SIMainInfo.EmplType, patientInfo.FT.OwnCost, "", patientInfo.PID.PatientNO

                string[] dist = patient.PID.Memo.Split('|');
                string socialSerialNumber = string.Empty;
                if (!string.IsNullOrEmpty(patient.SSN) && patient.SSN != "-")
                {
                    socialSerialNumber = patient.SSN;
                }
                else
                {
                    socialSerialNumber = patient.IDCard;
                }

                this.neuSpread1_Sheet1.Cells[rowCount, 0].Text = seqNo.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 1].Text = dist[0];
                this.neuSpread1_Sheet1.Cells[rowCount, 2].Text = socialSerialNumber;
                this.neuSpread1_Sheet1.Cells[rowCount, 3].Text = patient.PID.PatientNO;
                this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = patient.PVisit.InTime.ToString("yyyy-MM-dd");
                this.neuSpread1_Sheet1.Cells[rowCount, 5].Text = patient.PVisit.OutTime.ToString("yyyyMMdd");
                this.neuSpread1_Sheet1.Cells[rowCount, 6].Text = patient.PVisit.ICULocation.ID;
                this.neuSpread1_Sheet1.Cells[rowCount, 7].Text = patient.BalanceDate.ToString("yyyyMMdd");
                this.neuSpread1_Sheet1.Cells[rowCount, 8].Text = patient.ClinicDiagnose;
                this.neuSpread1_Sheet1.Cells[rowCount, 9].Text = patient.SIMainInfo.ApplyType.Name;
                this.neuSpread1_Sheet1.Cells[rowCount, 10].Text = patient.FT.TotCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 11].Text = patient.FT.PrepayCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 12].Text = patient.FT.PubCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 13].Text = patient.FT.OwnCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 14].Text = "普通住院";
                this.neuSpread1_Sheet1.Cells[rowCount, 15].Text = "";

                seqNo++;
            }

            rowCount = this.neuSpread1_Sheet1.RowCount;
            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
            this.neuSpread1_Sheet1.Cells[rowCount, 10].Text = totCost.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 11].Text = ownCost.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 12].Text = pubCost.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 13].Text = bigCost.ToString();
        }

        public int print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            p.IsHaveGrid = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("ipbalance", 784, 400);
            p.SetPageSize(ps);

            p.PrintPage(15, 3, this);
            return 1;
        }
    }
}
