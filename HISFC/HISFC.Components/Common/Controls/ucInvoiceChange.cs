using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// ��Ʊ����
    /// </summary>
    public partial class ucInvoiceChange : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public ucInvoiceChange()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        //private FS.HISFC.Models.Fee.InvoiceTypeEnumService myInvoiceType = new FS.HISFC.Models.Fee.InvoiceTypeEnumService();
        private FS.FrameWork.Models.NeuObject myInvoiceType = new FS.FrameWork.Models.NeuObject();
        
        /// <summary>
        /// ��Ʊҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();
        /// <summary>
        /// ��Աҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager employeeManager = new FS.HISFC.BizProcess.Integrate.Manager();
        
        private FS.HISFC.Models.Base.Employee myOperator = new FS.HISFC.Models.Base.Employee();
        #endregion

        #region ����

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        //public FS.HISFC.Models.Fee.InvoiceTypeEnumService InvoiceType
        //{
        //    get
        //    {
        //        return this.myInvoiceType;
        //    }

        //    set
        //    {
        //        this.myInvoiceType = value;
        //    }
        //}

        public FS.FrameWork.Models.NeuObject InvoiceType
        {
            get
            {
                return this.myInvoiceType;
            }
            set
            {
                this.myInvoiceType = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.myOperator = this.invoiceServiceManager.Operator as FS.HISFC.Models.Base.Employee;
            
            //this.cmbInvoiceType.AddItems(FS.HISFC.Models.Fee.InvoiceTypeEnumService.List());
            FS.HISFC.BizLogic.Manager.Constant myCon = new FS.HISFC.BizLogic.Manager.Constant ();
            this.cmbInvoiceType.AddItems(myCon.GetList("GetInvoiceType"));
            this.cmbInvoiceType.SelectedIndex = 0;
            this.QueryInvoice();
        }

        private string GetEmployeeName(string employeeID)
        {
            FS.HISFC.Models.Base.Employee employee = null;
            employee = this.employeeManager.GetEmployeeInfo(employeeID);
            return employee.Name;
        }

        /// <summary>
        /// ��ѯ��Ʊ
        /// </summary>
        private void QueryInvoice()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            ArrayList alInvoice = new ArrayList();
            alInvoice = this.invoiceServiceManager.QueryInvoices(this.myOperator.ID, this.myInvoiceType.ID);
            foreach (FS.HISFC.Models.Fee.Invoice invoice in alInvoice)
            {
                if (invoice.ValidState == "1")
                {
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    this.neuSpread1_Sheet1.SetValue(0, 0, this.myOperator.Name);
                    this.neuSpread1_Sheet1.SetValue(0, 1, this.myInvoiceType.Name);
                    this.neuSpread1_Sheet1.SetValue(0, 2, invoice.BeginNO);
                    this.neuSpread1_Sheet1.SetValue(0, 3, invoice.UsedNO);
                    this.neuSpread1_Sheet1.SetValue(0, 4, invoice.EndNO);
                    this.neuSpread1_Sheet1.SetValue(0, 5, invoice.AcceptTime);
                    this.neuSpread1_Sheet1.Rows[0].Tag = invoice;
                }
            }
            
        }

        /// <summary>
        /// ��������
        /// </summary>
        private int SaveInvoiceChange()
        {
            FS.HISFC.Models.Fee.InvoiceChange invoiceChange = null;
            FS.HISFC.Models.Fee.Invoice invoice = null;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                int row = this.neuSpread1_Sheet1.ActiveRow.Index;
                if (row >= 0)
                {
                    invoice = this.neuSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Invoice;
                }
            }
            invoiceChange = this.SetInvoiceChange();
            if (this.CheckData(invoiceChange) < 0)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.invoiceServiceManager.Connection);
            //trans.BeginTransaction();

            this.invoiceServiceManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iReturn = -1;
            iReturn = this.invoiceServiceManager.InsertInvoiceChange(invoiceChange);
            if (iReturn < 0)
            {
                MessageBox.Show("���뷢Ʊ�����ʧ�ܣ�" + this.invoiceServiceManager.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            iReturn = this.invoiceServiceManager.UpdateInvoiceUsedNO(invoiceChange.EndNO, invoice.AcceptOper.ID, invoice.AcceptTime);
            if (iReturn < 0)
            {
                MessageBox.Show("���·�Ʊ���ú���ʧ�ܣ�" + this.invoiceServiceManager.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�");
            this.QueryInvoice();
            return 0;
        }

        /// <summary>
        /// ���������Ч��
        /// </summary>
        /// <param name="invoiceChange"></param>
        /// <returns></returns>
        private int CheckData(FS.HISFC.Models.Fee.InvoiceChange invoiceChange)
        {
            FS.HISFC.Models.Fee.Invoice invoice = null;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                int row = this.neuSpread1_Sheet1.ActiveRow.Index;
                if (row >= 0)
                {
                    invoice = this.neuSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Invoice;
                }
            }

            for (int i = 0, j = this.txtUsedNO.Text.Length; i < j; i++)
            {
                if (!char.IsDigit(this.txtUsedNO.Text, i))
                {
                    //����˵���ǵڼ����ַ�������
                    MessageBox.Show("��������÷�Ʊ�ű���������", "��ʾ", MessageBoxButtons.OK);
                    return -1;
                }
            }

            if (Convert.ToInt64(invoiceChange.EndNO) < Convert.ToInt64(invoice.UsedNO))
            {
                MessageBox.Show("��������÷�Ʊ�Ų���С�ڵ�ǰ���ú�");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ���ɷ�Ʊ���ʵ��
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.InvoiceChange SetInvoiceChange()
        {
            FS.HISFC.Models.Fee.InvoiceChange myInvoiceChange = new FS.HISFC.Models.Fee.InvoiceChange();
            FS.HISFC.Models.Fee.Invoice invoice = null;
            long beginNO = 0;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                int row = this.neuSpread1_Sheet1.ActiveRow.Index;
                if (row >= 0)
                {
                    invoice = this.neuSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Invoice;
                }
            }
            myInvoiceChange.HappenNO = this.invoiceServiceManager.GetInvoiceChangeHappenNO(this.myOperator.ID);
            myInvoiceChange.GetOper.ID = invoice.AcceptOper.ID;
            myInvoiceChange.InvoiceType = invoice.Type;
            beginNO = Convert.ToInt64(invoice.UsedNO) + 1;
            //myInvoiceChange.BeginNO = beginNO.ToString().PadLeft(12, '0');
            myInvoiceChange.BeginNO = beginNO.ToString();
            //myInvoiceChange.EndNO = this.txtUsedNO.Text.ToString().PadLeft(12, '0');
            myInvoiceChange.EndNO = this.txtUsedNO.Text.ToString();
            myInvoiceChange.ShiftType = "2";
            myInvoiceChange.Oper.ID = this.myOperator.ID;
            myInvoiceChange.Memo = "��Ʊ����";
            return myInvoiceChange;
        }

        #endregion

        #region �¼�

        private void ucInvoiceChange_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void cmbInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.myInvoiceType.ID = this.cmbInvoiceType.Tag.ToString();
            this.myInvoiceType.Name = this.cmbInvoiceType.Text;
            this.QueryInvoice();
            this.txtUsedNO.Text = "";
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.Fee.Invoice selectedInvoice = null;
            
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                int row = this.neuSpread1_Sheet1.ActiveRow.Index;
                if (row >= 0)
                {
                    selectedInvoice = this.neuSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Invoice;
                    this.txtUsedNO.Text = selectedInvoice.UsedNO;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveInvoiceChange();
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        
    }
}

