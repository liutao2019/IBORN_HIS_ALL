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
    public partial class ucNurse : UserControl
    {
        public ucNurse()
        {
            InitializeComponent();
        }

        #region ����
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        #endregion

        private void ucNurse_Load(object sender, EventArgs e)
        {
            //InitFP();
            ArrayList alDept = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            if (alDept == null)
            {
                MessageBox.Show("��ѯ������Ϣʧ�ܣ�");
                return;
            }
            int count = 0;
            count = this.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, count);
            }

            if (alDept.Count == 0) return;

            foreach (FS.FrameWork.Models.NeuObject neuObject in alDept)
            {
                    count = this.neuSpread1_Sheet1.Rows.Count;
                    this.neuSpread1_Sheet1.Rows.Add(count, 1);
                    this.neuSpread1_Sheet1.Cells[count, 0].Text = neuObject.ID;
                    this.neuSpread1_Sheet1.Cells[count, 1].Text = neuObject.Name;
            }
        }

        private void InitFP()
        {
            InputMap im =this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);

            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            QueryPatient(e.Row);

        }

        private void QueryPatient(int rowIndex)
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
            string nurseCode = this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text;
            string nurseName = this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text;

            ArrayList al = radtIntegrate.QueryPatientByNurseCellAndState(nurseCode, FS.HISFC.Models.Base.EnumInState.I);
            if (al == null)
            {
                MessageBox.Show("��ѯ����" + nurseName + "��Ժ������Ϣʧ�ܣ�");
                return;
            }
            if (al.Count == 0)
            {
                MessageBox.Show("�ÿ���" + nurseName + "��������Ժ���ߣ�");
                return;
            }
            
            ucGetPatient uc = new ucGetPatient();
            uc.AlPatient = al;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            this.FindForm().Close();
        }


        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            QueryPatient(this.neuSpread1_Sheet1.ActiveRowIndex);
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            if (keyData == Keys.Enter)
            {
                QueryPatient(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        
    }
}
