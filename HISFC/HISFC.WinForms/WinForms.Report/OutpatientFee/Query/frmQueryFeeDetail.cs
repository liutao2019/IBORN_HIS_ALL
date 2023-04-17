using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.Query
{
    public partial class frmQueryFeeDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public frmQueryFeeDetail()
        {
            InitializeComponent();
        }

        #region ��������
        /// <summary>
        /// ��ѯ����
        /// </summary>
        int intQueryType = 1;

        FS.FrameWork.WinForms.Forms.frmWait frmWait = new FS.FrameWork.WinForms.Forms.frmWait();

        #endregion

        #region ����

        #region �л�������ʽ
        /// <summary>
        /// �л�������ʽ
        /// </summary>
        public void ChangeQueryType()
        {
            // ת����ѯ����
            if (intQueryType == 3)
            {
                intQueryType = 1;
            }
            else
            {
                intQueryType++;
            }

            // ���ò�ѯ������ʾ�ı�
            switch (this.intQueryType)
            {
                case 1:
                    this.labQueryName.Text = "��Ʊ��(F2�л�)";
                    break;
                case 2:
                    this.labQueryName.Text = "������(F2�л�)";
                    break;
                case 3:
                    this.labQueryName.Text = "��  ��(F2�л�)";
                    break;
            }

            // ���ý��㵽�����
            this.tbInput.Focus();
            this.tbInput.SelectAll();

            // ����ʱ�临ѡ��
            if (this.intQueryType != 1)
            {
                this.cbDataDate.Enabled = true;
            }
            else
            {
                this.cbDataDate.Enabled = false;
            }
        }
        #endregion

        #region ��ȡ������(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ��ȡ������(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="strCode">���صļ�����</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetInput(ref string strCode)
        {
            strCode = this.tbInput.Text.Trim();

            // �жϺϷ���
            if (this.intQueryType == 2)
            {
                try
                {
                    long.Parse(strCode);
                }
                catch
                {
                    MessageBox.Show("����Ĳ������ű�����������ʽ,�볢���÷�Ʊ�Ų�ѯ");
                    this.tbInput.Text = "";
                    this.tbInput.Focus();
                    return -1;
                }
            }

            // ������
            switch (this.intQueryType)
            {
                case 1:
                    // ���շ�Ʊ�Ų�ѯ��12λ
                    strCode = strCode.PadLeft(12, '0');
                    break;
                case 2:
                    // �������ţ�10λ
                    strCode = strCode.PadLeft(10, '0');
                    break;
            }
            this.tbInput.Text = strCode;

            return 1;
        }
        #endregion

        #region ��ȡ��ѯ����
        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <param name="dtFrom">���ص���ʼ����</param>
        /// <param name="dtTo">���صĽ�ֹ����</param>
        public void GetQueryDate(ref DateTime dtFrom, ref DateTime dtTo)
        {
            // ��������շ�Ʊ�Ų�ѯ���ſ��Դ�ʱ���������Ϊ��Ʊ����Ψһ��
            if (this.intQueryType != 1)
            {
                // ���ʱ��ѡ��ѡ�У�������Чѡ������
                if (this.cbDataDate.Checked)
                {
                    dtFrom = this.dtpFromDate.Value;
                    dtTo = this.dtpDateTo.Value;

                    dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day);
                    dtTo = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day + 1);
                }
                else
                {
                    // ������ʼ����Ϊ��С���ڣ���ֹ����Ϊ�������
                    dtFrom = new DateTime(2000, 11, 11, 11, 11, 11);
                    dtTo = new DateTime(2020, 11, 11, 11, 11, 11);
                }
            }
        }
        #endregion

        #region ������Ʊ������Ϣ
        /// <summary>
        /// QueryInvoiceInformation
        /// </summary>
        public void QueryInvoiceInformation()
        {
            this.frmWait.Show();
            int intReturn = 0;
            // ���ݲ�ͬ�Ĳ�ѯ���ִ�в�ͬ�Ĳ�ѯ
            switch (this.intQueryType)
            {
                case 1:
                    intReturn = this.QueryInvoiceInfromationByInvoiceNo();
                    break;
                case 2:
                    intReturn = this.QueryInvoiceInfromationByCardNo();
                    break;
                case 3:
                    intReturn = this.QueryInvoiceInfromationByName();
                    break;
            }

            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return;
            }

            // ��ʾ��һҳ
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;

            this.frmWait.Hide();
        }
        #endregion

        #region ����Ʊ�ż�����Ʊ������Ϣ
        /// <summary>
        /// ����Ʊ�ż�����Ʊ������Ϣ
        /// </summary>
        public int QueryInvoiceInfromationByInvoiceNo()
        {
            // ��������
            int intReturn = 0;
            string strCode = "";
            System.Data.DataSet dsResult1 = new DataSet();
            System.Data.DataSet dsResult2 = new DataSet();
            System.Data.DataSet dsResult3 = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // ��ȡ������
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // ִ�в�ѯ
            intReturn = outpatient.QueryBalancesByInvoiceNO(strCode, ref dsResult1);
            if (-1 == intReturn)
            {
                MessageBox.Show("��ȡ��Ʊ������Ϣʧ��" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet1.DataSource = dsResult1;

            // ����Ʊ�Ų�ѯͬʱ��ѯ��Ʊ��ϸ�ͷ�����ϸ
            intReturn = outpatient.QueryBalanceListsByInvoiceNO(strCode, ref dsResult2);
            if (-1 == intReturn)
            {
                MessageBox.Show("��ȡ��Ʊ��ϸʧ��" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet2.DataSource = dsResult2;

            intReturn = outpatient.QueryFeeItemListsByInvoiceNO(strCode, ref dsResult3);
            if (-1 == intReturn)
            {
                MessageBox.Show("��ȡ������ϸʧ��" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet3.DataSource = dsResult3;
            if (this.fpSpread1_Sheet3.RowCount > 0)
            {
                this.SetSheet3DisplayData();
            }
            return 1;
        }
        #endregion

        #region �������ż�����Ʊ������Ϣ
        /// <summary>
        /// �������ż�����Ʊ������Ϣ
        /// </summary>
        public int QueryInvoiceInfromationByCardNo()
        {
            // ��������
            int intReturn = 0;
            string strCode = "";
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MaxValue;
            System.Data.DataSet dsResult = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // ��ȡ������
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // ��ȡʱ�䷶Χ
            this.GetQueryDate(ref dtFrom, ref dtTo);

            // ִ�в�ѯ
            intReturn = outpatient.QueryBalancesByCardNO(strCode, dtFrom, dtTo, ref dsResult);
            if (-1 == intReturn)
            {
                this.frmWait.Hide();
                MessageBox.Show("��ȡ��Ʊ������Ϣʧ��" + outpatient.Err);
                return -1;
            }

            // ���ò�ѯ���
            this.fpSpread1_Sheet1.DataSource = dsResult;
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet3.DataSource = null;

            return 1;
        }
        #endregion

        #region ������������Ʊ������Ϣ
        /// <summary>
        /// ������������Ʊ������Ϣ
        /// </summary>
        public int QueryInvoiceInfromationByName()
        {

            // ��������
            int intReturn = 0;
            string strCode = "";
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MaxValue;
            System.Data.DataSet dsResult = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // ��ȡ������
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // ��ȡʱ�䷶Χ
            this.GetQueryDate(ref dtFrom, ref dtTo);

            // ִ�в�ѯ
            intReturn = outpatient.QueryBalancesByPatientName(strCode, dtFrom, dtTo, ref dsResult);
            if (-1 == intReturn)
            {
                this.frmWait.Hide();
                MessageBox.Show("��ȡ��Ʊ������Ϣʧ��" + outpatient.Err);
                return -1;
            }

            // ���ò�ѯ���
            this.fpSpread1_Sheet1.DataSource = dsResult;
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet3.DataSource = null;

            return 1;
        }
        #endregion

        #region �����е���ʾ���
        /// <summary>
        /// �����е���ʾ���
        /// </summary>
        /// <param name="intSheet">SHEET����</param>
        public void SetSheetWidth(int intSheet)
        {
            // �����п��
            for (int i = 0; i < this.fpSpread1.Sheets[intSheet].Columns.Count; i++)
            {
                this.fpSpread1.Sheets[intSheet].Columns[i].Width = 80;
            }

            // ���������ֶε�ֵ���������5��ҽ�����8��������14���Ƿ����16����Ʊ״̬17�����ϲ���Ա19���Ƿ�˲�21���˲���22���Ƿ��Ѿ��ս�24���ս���25���Է��ս����28
        }
        #endregion

        #region ���õ���ҳ�����ֶε�ֵ
        /// <summary>
        /// 
        /// </summary>
        public void SetSheet3DisplayData()
        {
            for (int i = 0; i < this.fpSpread1_Sheet3.RowCount; i++)
            {
                // ��������
                FS.HISFC.Models.Base.SysClassEnumService enuSysClass = new FS.HISFC.Models.Base.SysClassEnumService();

                // ϵͳ���7
                if (this.fpSpread1_Sheet3.Cells[i, 22].Text != "" || this.fpSpread1_Sheet3.Cells[i, 22].Text != null)
                {
                    enuSysClass.ID = this.fpSpread1_Sheet3.Cells[i, 22].Text;
                    this.fpSpread1_Sheet3.Cells[i, 22].Text = enuSysClass.Name;
                }
            }
        }
        #endregion

        #region ��ӡ
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void PringSheet()
        {
            if (MessageBox.Show("�Ƿ��ӡ��ǰ���ҳ", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            this.fpSpread1.PrintSheet(this.fpSpread1.ActiveSheetIndex);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void ExportSheet()
        {
            DialogResult drExport = new DialogResult();
            drExport = this.saveFileDialog1.ShowDialog();
            if (drExport == DialogResult.OK)
            {
                this.fpSpread1.SaveExcel(this.saveFileDialog1.FileName);
            }
        }
        #endregion

        #endregion

        #region �¼�

        #region ���ڰ����¼�
        /// <summary>
        /// ���ڰ����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                // �л���ѯ��ʽ
                this.ChangeQueryType();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                // �л�����ʱ�临ѡ���״̬
                this.cbDataDate.Checked = !this.cbDataDate.Checked;
                this.tbInput.Focus();
                return true;
            }
            else if (keyData == Keys.F4)
            {
                // ��Ʊ��ѯ
                this.QueryInvoiceInformation();
                return true;
            }
            else if (keyData == Keys.F6)
            {
                // ��ӡ
                this.PringSheet();
                return true;
            }
            else if (keyData == Keys.F7)
            {
                // ����
                this.ExportSheet();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                // ����
                return true;
            }
            else if (keyData == Keys.F12)
            {
                // �˳�
                this.FindForm().Close();
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.A.GetHashCode())
            {
                // �л����㵽�����
                this.tbInput.Focus();
                this.tbInput.SelectAll();
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region ���ڸ�ѡ��ѡ��״̬�ı��¼�
        /// <summary>
        /// ���ڸ�ѡ��ѡ��״̬�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDataDate_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dtpFromDate.Enabled = this.cbDataDate.Checked;
            this.dtpDateTo.Enabled = this.cbDataDate.Checked;
            if (this.cbDataDate.Checked)
            {
                this.dtpFromDate.Focus();
            }
            else
            {
                this.tbInput.Focus();
            }
        }

        #endregion

        #region �ڼ����������س��¼�
        /// <summary>
        /// �ڼ����������س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.tbInput.Text == "")
                {
                    return;
                }
                this.QueryInvoiceInformation();
            }
        }

        #endregion

        #region ˫������һҳ�����ݷ�Ʊ�Ų�ѯ��Ʊ��ϸ�ͷ�����ϸ
        /// <summary>
        /// ˫������һҳ�����ݷ�Ʊ�Ų�ѯ��Ʊ��ϸ�ͷ�����ϸ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    this.frmWait.Show();
                    // ����ֵ
                    int intReturn = 0;
                    // �������� = ��ѡ�еķ�Ʊ��
                    string strCode = this.fpSpread1_Sheet1.Cells[e.Row, 0].Text;
                    // ���ص�����Դ
                    System.Data.DataSet dsResult1 = new DataSet();
                    System.Data.DataSet dsResult2 = new DataSet();
                    // ҵ���
                    FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

                    // ��ѯ���߷�Ʊ��ϸ
                    intReturn = outpatient.QueryBalanceListsByInvoiceNO(strCode, ref dsResult1);
                    if (-1 == intReturn)
                    {
                        this.frmWait.Hide();
                        MessageBox.Show("��ȡ��Ʊ��ϸʧ��" + outpatient.Err);
                        return;
                    }
                    this.fpSpread1_Sheet2.DataSource = dsResult1;
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                    // ��ѯ���߷�����ϸ
                    intReturn = outpatient.QueryFeeItemListsByInvoiceNO(strCode, ref dsResult2);
                    if (-1 == intReturn)
                    {
                        this.frmWait.Hide();
                        MessageBox.Show("��ȡ������ϸʧ��" + outpatient.Err);
                        return;
                    }
                    this.fpSpread1_Sheet3.DataSource = dsResult2;
                    if (this.fpSpread1_Sheet3.RowCount > 0)
                    {
                        this.SetSheet3DisplayData();
                    }

                    this.frmWait.Hide();
                }
            }
        }

        #endregion

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "������ѯ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J���, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����") 
            {
                this.ExportSheet();
            }
            if (e.ClickedItem.Text == "��ѯ") 
            {
                this.QueryInvoiceInformation();
            }

            if (e.ClickedItem.Text == "��ӡ") 
            {
                this.PringSheet();
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryInvoiceInformation();
            
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportSheet();
            return base.Export(sender, neuObject);
        }

        //#endregion

        #region ���ڼ���
        /// <summary>
        /// ���ڼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryFeeDetail_Load(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.Fee.Outpatient function = new FS.HISFC.BizLogic.Fee.Outpatient();
            this.dtpFromDate.Value = function.GetDateTimeFromSysDateTime();
            this.dtpDateTo.Value = function.GetDateTimeFromSysDateTime();
            frmWait.Tip = "���ڲ�ѯ�������ݣ���ȴ�......";
        }

        #endregion

        #endregion
    }
}