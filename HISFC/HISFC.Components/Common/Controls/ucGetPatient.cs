using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucGetPatient : UserControl
    {
        #region ���� 
        
        private ArrayList al = new ArrayList();
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        #endregion

        #region ����
        public ArrayList AlPatient
        {
            get
            {
                return al;
            }
            set
            {
                al = value;
                if (al == null) return;
                SetFpPatient();
            }
        }

        #endregion

        public ucGetPatient()
        {
            InitializeComponent();
        }

        #region ����

        private void InitFP()
        {
            InputMap im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);

            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        private void SetFpPatient()
        {
            int count =this.neuSpread1_Sheet1.Rows.Count;
            if ( count == 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, count);
            }

            for (int i = 0; i < al.Count; i++)
            {
                count = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(count, 1);
                if (al[i].GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                {
                    FS.HISFC.Models.RADT.PatientInfo p = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                    this.neuSpread1_Sheet1.Cells[count, 0].Text = p.PVisit.PatientLocation.Bed.ID.Substring(4, p.PVisit.PatientLocation.Bed.ID.Length - 4); //p.PVisit.PatientLocation.Bed.ID;
                    this.neuSpread1_Sheet1.Cells[count, 1].Text = p.PID.PatientNO;
                    this.neuSpread1_Sheet1.Cells[count, 2].Text = p.Name;
                    this.neuSpread1_Sheet1.Cells[count, 3].Text = p.Sex.Name;
                    this.neuSpread1_Sheet1.Cells[count, 4].Text = inpatientManager.GetAge(p.Birthday);
                    this.neuSpread1_Sheet1.Cells[count, 5].Text = p.Pact.Name;
                    this.neuSpread1_Sheet1.Rows[count].Tag = p;
                }
                else
                {
                    FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                    this.neuSpread1_Sheet1.Cells[count, 0].Text = obj.ID.Substring(4, obj.ID.Length - 4);
                    this.neuSpread1_Sheet1.Cells[count, 1].Text = obj.Name.Substring(4, 10);
                    this.neuSpread1_Sheet1.Cells[count, 2].Text = obj.Memo;
                }

            }
        }

        private void GetPatient(int rowIndex)
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
            FS.HISFC.Models.RADT.PatientInfo p = this.neuSpread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo;
            this.FindForm().Close();
            PatientEvent.DoGetPatient(p);
        }

        #endregion

        #region �¼�
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            GetPatient(e.Row);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.GetPatient(this.neuSpread1_Sheet1.ActiveRowIndex);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            if (keyData == Keys.Enter)
            {
                this.GetPatient(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        private void ucGetPatient_Load(object sender, EventArgs e)
        {
            InitFP();
        }
        #endregion

     
    }
    public class PatientEvent
    {
        public delegate void GetPatient(FS.HISFC.Models.RADT.PatientInfo p);
        public static event GetPatient GetPatientEvent;
        public static void DoGetPatient(FS.HISFC.Models.RADT.PatientInfo p)
        {
            if(GetPatientEvent != null)
            {
                GetPatientEvent(p);
            }
        }
    }
}
