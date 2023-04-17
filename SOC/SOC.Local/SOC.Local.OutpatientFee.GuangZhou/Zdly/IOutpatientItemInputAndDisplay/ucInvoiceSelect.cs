using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    public partial class ucInvoiceSelect : UserControl
    {
        public ucInvoiceSelect()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ǰѡ��Ʊ
        /// </summary>
        protected FS.HISFC.Models.Fee.Outpatient.Balance selectedBalance = null;

        #endregion

        #region ����

        /// <summary>
        /// ��ǰѡ��Ʊ
        /// </summary>
        public FS.HISFC.Models.Fee.Outpatient.Balance SelectedBalance 
        {
            get 
            {
                return this.selectedBalance;
            }
            set 
            {
                this.selectedBalance = value;
            }
        }

        #endregion

        #region ö��

        /// <summary>
        /// ��
        /// </summary>
        private enum Columns
        {
            /// <summary>
            /// ��������
            /// </summary>
            PatientName,

            /// <summary>
            /// ����
            /// </summary>
            CardNO,

            /// <summary>
            /// ��Ʊ��
            /// </summary>
            InvoiceNO,

            /// <summary>
            /// ��Ʊ��ˮ��
            /// </summary>
            InvoiceComb

        }

        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="list"></param>
        public void Add(ArrayList list)
        {
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance obj in list)
            {
                this.fpSpread1_Sheet1.Rows.Add(0, 1);
                this.fpSpread1_Sheet1.Cells[0, (int)Columns.PatientName].Text = obj.Patient.Name;
                this.fpSpread1_Sheet1.Cells[0, (int)Columns.CardNO].Text = obj.Patient.PID.CardNO;
                this.fpSpread1_Sheet1.Cells[0, (int)Columns.InvoiceNO].Text = obj.Invoice.ID;
                this.fpSpread1_Sheet1.Cells[0, (int)Columns.InvoiceComb].Text = obj.CombNO;
                this.fpSpread1_Sheet1.Rows[0].Tag = obj;
            }
        }

        #endregion

        #region �¼�

        private void ucInvoiceSelect_Load(object sender, System.EventArgs e)
        {
            this.fpSpread1.Select();
            this.fpSpread1.Focus();
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            selectedBalance = (FS.HISFC.Models.Fee.Outpatient.Balance)this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag;
            this.FindForm().Close();
        }

        private void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            selectedBalance = (FS.HISFC.Models.Fee.Outpatient.Balance)this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag;
            this.FindForm().Close();
        }

        #endregion
        

       
    }
}
