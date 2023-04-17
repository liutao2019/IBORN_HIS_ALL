using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// �ʻ�������Ϣ��ѯ
    /// </summary>
    public partial class ucAccountQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAccountQuery()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �ʻ�����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �����Ͱ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelp = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ����

        [Description("�Ƿ���ʾ���￨�б�"), Category("����")]
        public bool IsShowCardSheet
        {
            get
            {
                return this.neuTabControl1.Contains(this.tabPage1);
            }
            set
            {
                if (!value)
                {
                    if (this.neuTabControl1.Contains(this.tabPage1))
                    {
                        this.neuTabControl1.TabPages.Remove(this.tabPage1);
                    }
                }
                else
                {
                    if (!this.neuTabControl1.Contains(this.tabPage1))
                    {
                        this.neuTabControl1.TabPages.Add(this.tabPage1);
                    }
                }
            }
        }

        #endregion

        #region ����

        protected void Clear()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;
            this.fpFeeDetail_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// �����ʻ���¼
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        protected virtual void GetAccountRecord(string cardNO,string begin,string end)
        {
            // {B5773956-8C45-4b05-87A4-E8F8FA74706F}
            this.neuSpread2_Sheet1.RowCount = 0;
            //�����ʻ����׼�¼
            List<FS.HISFC.Models.Account.AccountRecord> list = accountManager.GetAccountRecordList(cardNO, begin, end);
            if (list == null)
            {
                MessageBox.Show(accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.neuSpread2_Sheet1.Rows.Count = list.Count+1;
            int index=0;
            decimal vacancy = 0;
            foreach (FS.HISFC.Models.Account.AccountRecord account in list)
            {
                //��������
                neuSpread2_Sheet1.Cells[index, 0].Text = account.OperType.Name;
                //���׽��
                neuSpread2_Sheet1.Cells[index, 1].Text = account.BaseCost.ToString();
                //���׺����
                neuSpread2_Sheet1.Cells[index, 2].Text = account.BaseVacancy.ToString();
                //��������
                neuSpread2_Sheet1.Cells[index, 3].Text = account.FeeDept.Name;
                //������
                neuSpread2_Sheet1.Cells[index, 4].Text = account.Oper.Name;
                //����ʱ��
                neuSpread2_Sheet1.Cells[index, 5].Text = account.OperTime.ToString();
                //��ע
                neuSpread2_Sheet1.Cells[index, 6].Text = account.ReMark;
                neuSpread2_Sheet1.Cells[index, 7].Text = account.EmpowerPatient.Name;
                neuSpread2_Sheet1.Cells[index, 8].Text = account.EmpowerCost.ToString();
                neuSpread2_Sheet1.Rows[index].Tag = account;
                vacancy += account.BaseCost;
                ++index;
            }
            neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.Rows.Count-1, 0].Text = "�ϼƣ�";
            neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.Rows.Count-1, 1].Text = vacancy.ToString();

        }

        /// <summary>
        /// ���ҿ�ʹ�ü�¼
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        protected virtual void GetAccountCardRecord(string cardNO, string begin, string end)
        {
            //�����ʻ���������¼
            List<FS.HISFC.Models.Account.AccountCardRecord> list = accountManager.GetAccountCardRecord(cardNO, begin, end);
            if (list == null)
            {
                MessageBox.Show(accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            neuSpread1_Sheet1.Rows.Count = list.Count;
            int index=0;
            foreach (FS.HISFC.Models.Account.AccountCardRecord accountCardRecord in list)
            {
                //������
                if (markHelp != null)
                {
                    neuSpread1_Sheet1.Cells[index, 0].Text = markHelp.GetName(accountCardRecord.MarkType.ID);
                }
                //����
                neuSpread1_Sheet1.Cells[index, 1].Text = accountCardRecord.MarkNO;
                //��������
                neuSpread1_Sheet1.Cells[index, 2].Text = accountCardRecord.OperateTypes.Name;
                //������
                neuSpread1_Sheet1.Cells[index, 3].Text = managerIntegrate.GetEmployeeInfo(accountCardRecord.Oper.ID).Name;
                //����ʱ��
                neuSpread1_Sheet1.Cells[index, 4].Text = accountCardRecord.Oper.OperTime.ToString();
                ++index;
            }
        }

        /// <summary>
        /// �������￨��
        /// </summary>
        /// <returns>true �ɹ� falseʧ��</returns>
        private bool GetCardNO(ref string cardNO)
        {
            string markNo = this.txtMardNO.Text.Trim();
            HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = accountManager.GetCardByRule(markNo,ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                return false;
            }
            this.txtMardNO.Text = accountCard.MarkNO;
            this.cmbCardType.Tag = accountCard.MarkType.ID;
            if (accountCard.Patient != null)
            {
                cardNO = accountCard.Patient.PID.CardNO;
            }
            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual void GetQueryData()
        {
            this.Clear();

            if (this.txtMardNO.Tag == null) 
            {
                MessageBox.Show("������￨�ź���س�ȷ�ϣ�");
                return;
            }
            string cardNo = this.txtMardNO.Tag.ToString();
            //��ֹʱ��
            string begin = this.dtpbegin.Value.ToShortDateString() + " 0:00:00";
            string end = this.dtpend.Value.ToShortDateString() + " 23:59:59";
            try
            {
                //�����ʻ���¼
                GetAccountRecord(cardNo, begin, end);
                //���ҿ�ʹ�ü�¼
                GetAccountCardRecord(cardNo, begin, end);
                txtMardNO.Focus();
                txtMardNO.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        
        #region �¼�
        private void ucAccountQuery_Load(object sender, EventArgs e)
        {
            DateTime dt = accountManager.GetDateTimeFromSysDateTime();
            int dayindex = dt.Day - 1;
            this.dtpbegin.Value = dt.AddDays(-dayindex);
            this.dtpend.Value = dt;
            this.ActiveControl = this.txtMardNO;
            System.Collections.ArrayList al = this.managerIntegrate.GetConstantList("MarkType");
            if (al == null) return;
            markHelp.ArrayObject = al;
            this.cmbCardType.AddItems(al);

            try
            {
                
                this.fpFeeDetail_Sheet1.DataAutoSizeColumns = false;
                this.fpFeeDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
                this.fpFeeDetail_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Range;
                this.fpFeeDetail_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
                this.fpFeeDetail_Sheet1.Columns[0].Width = 200;
                this.fpFeeDetail_Sheet1.Columns[1].Width = 180;
                this.fpFeeDetail_Sheet1.Columns[2].Width = 120;
                this.fpFeeDetail_Sheet1.Columns[3].Width = 100;
                this.fpFeeDetail_Sheet1.Columns[5].Width = 120;
               // this.fpFeeDetail_Sheet1.Columns[6].Width = 180;
                this.neuTabControl1.TabPages.Remove(this.tbFeeDetail);
            }
            catch
            {
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            GetQueryData();
            return base.OnQuery(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("ˢ��", "ˢ��", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return toolbarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ˢ��":
                    {
                        string mCardNo = "";
                        string error = "";
                        if (Function.OperMCard(ref mCardNo, ref error) == -1)
                        {
                            MessageBox.Show(error, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        txtMardNO.Text = ";"+mCardNo;
                        this.txtMardNO_KeyDown(null, new KeyEventArgs(Keys.Enter));
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        protected override int OnPrint(object sender, object neuObject)
        {

            //{A1814BD4-BB4B-445f-BCD4-33E4DB4377B6} ��Ӵ�ӡ��ѡ��
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))
            {
                MessageBox.Show("��ѡ��һ̨��ӡ��");
                return -1;
            }
            print.PrintDocument.PrinterSettings.PrinterName = printerName;

            print.PrintPage(0, 0, this.neuTabControl1);
            return base.OnPrint(sender, neuObject);
        }

        private void txtMardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string cardNo = string.Empty;
                if (!this.GetCardNO(ref cardNo)) return;
                this.txtMardNO.Tag = cardNo;
                this.GetQueryData();
            }
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuTabControl1.Controls.Contains(this.tbFeeDetail))
            {
                this.neuTabControl1.Controls.Remove(this.tbFeeDetail);
            }
            if (this.neuSpread2_Sheet1.RowCount < 1)
            {
                return;
            }

            if (this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.ActiveRowIndex].Tag == null) return;

            FS.HISFC.Models.Account.AccountRecord accountRecord = this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Account.AccountRecord;

            string invoiceNO = accountRecord.ReMark;
            string invoiceType = accountRecord.InvoiceType.ID;
            if (invoiceNO == "" || invoiceType == string.Empty)
            {
                return;
            }
            //���﷢Ʊ����C
            if (invoiceType != "C") return;

            if (accountRecord.OperType.ID.ToString() != ((int)OperTypes.Pay).ToString() || accountRecord.OperType.ID.ToString() != ((int)OperTypes.CancelPay).ToString()) return;
            bool isQuite = accountRecord.OperType.ID.ToString() == ((int)OperTypes.Pay).ToString() ? true : false;
            //������Ʊ������ϸ
            DataSet dsFeeDetail = accountManager.GetFeeDetailByInvoiceNO(invoiceNO, isQuite);
            if (dsFeeDetail == null) return;

            if (dsFeeDetail.Tables[0].Rows.Count == 0)
            {
                return;
            }
            DataRow row = dsFeeDetail.Tables[0].NewRow();

            row[1] = "�ϼƣ�";
            row[5] = dsFeeDetail.Tables[0].Compute("sum(���)", "");
            dsFeeDetail.Tables[0].Rows.Add(row);
            this.fpFeeDetail.DataSource = dsFeeDetail;
            this.neuTabControl1.TabPages.Add(this.tbFeeDetail);
            this.neuTabControl1.SelectedTab = this.tbFeeDetail;

        }
        #endregion

        private void txtName_KeyDown(object sender, KeyEventArgs e)// {D55B4DFA-DA91-42b0-8163-27036100E89E}
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {

                    this.txtMardNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    this.txtMardNO.Focus();
                    txtMardNO_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }

        

 
    }
}
