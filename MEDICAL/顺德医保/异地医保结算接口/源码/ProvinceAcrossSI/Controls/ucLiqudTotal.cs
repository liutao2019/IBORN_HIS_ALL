using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ProvinceAcrossSI.Controls
{
    public partial class ucLiqudTotal : UserControl
    {
        public ucLiqudTotal()
        {
            InitializeComponent();
        }

        Hashtable hsDistPatient = new Hashtable();

        private void SetPrintValue(ArrayList alPatientInfo)
        {
            //按照各个市分开
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatientInfo)
            {
                string[] dists = patient.PID.Memo.Split('|');
                if (hsDistPatient.ContainsKey(dists[0]))
                {
                    ArrayList patientList = hsDistPatient[dists[0]] as ArrayList;
                    patientList.Add(patient);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(patient);
                    hsDistPatient.Add(dists[0], al);
                }
            }

            int seqNo = 1;
            int personCount = 1;
            decimal totCost = 0;
            decimal pubCost = 0;
            decimal ownCost = 0;
            decimal bigCost = 0;

            decimal xtotCost = 0;
            decimal xpubCost = 0;
            decimal xownCost = 0;
            decimal xbigCost = 0;

            int rowCount = 0;
            foreach (string key in hsDistPatient.Keys)
            {
                xtotCost = 0;
                xpubCost = 0;
                xownCost = 0;
                xbigCost = 0;

                ArrayList patientLists = hsDistPatient[key] as ArrayList;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientLists)
                {
                    totCost += patient.FT.TotCost;
                    pubCost += patient.FT.PubCost;
                    ownCost += patient.FT.OwnCost;
                    bigCost += patient.FT.PayCost;

                    xtotCost += patient.FT.TotCost;
                    xpubCost += patient.FT.PubCost;
                    xownCost += patient.FT.OwnCost;
                    xbigCost += patient.FT.PayCost;
                }

                personCount += patientLists.Count;

                rowCount = this.neuSpread1_Sheet1.RowCount;
                this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

                this.neuSpread1_Sheet1.Cells[rowCount, 0].Text = seqNo.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 1].Text = key;
                this.neuSpread1_Sheet1.Cells[rowCount, 2].Text = patientLists.Count.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 3].Text = patientLists.Count.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = xtotCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 5].Text = xownCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 6].Text = xpubCost.ToString();
                this.neuSpread1_Sheet1.Cells[rowCount, 7].Text = xbigCost.ToString();

                seqNo++;
            }

            rowCount = this.neuSpread1_Sheet1.RowCount;
            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

            this.neuSpread1_Sheet1.Cells[rowCount, 0].Text = "合计："+hsDistPatient.Keys.Count+" 个参保地";
            this.neuSpread1_Sheet1.Cells[rowCount, 2].Text = personCount.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 3].Text = personCount.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = totCost.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 5].Text = ownCost.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 6].Text = pubCost.ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, 7].Text = bigCost.ToString();
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
