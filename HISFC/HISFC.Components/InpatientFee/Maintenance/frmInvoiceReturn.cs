using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class frmInvoiceReturn : Form
    {
        #region ����

        private FS.HISFC.Models.Fee.Invoice CurrInvoice;
        private long invoiceUsedNo;
        private string invoiceUsedNoStr;
        private long invoiceEndNo;
        private string invoiceEndNoStr;
        private long invoiceQty;

        #endregion

        public frmInvoiceReturn(FS.HISFC.Models.Fee.Invoice invoiceInfo)
        {
            InitializeComponent();
            this.CurrInvoice = invoiceInfo;
            Init();
        }

        #region ����

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.txtUsedno.Text = CurrInvoice.UsedNO;

            if (CurrInvoice.ValidState == "0")
            {
                invoiceUsedNo = FS.FrameWork.Public.String.GetNumber(CurrInvoice.BeginNO);
                invoiceUsedNoStr = CurrInvoice.BeginNO;
            }
            else
            {
                invoiceUsedNo = FS.FrameWork.Public.String.GetNumber(CurrInvoice.UsedNO) + 1;
                invoiceUsedNoStr = FS.FrameWork.Public.String.AddNumber(CurrInvoice.UsedNO, 1);
            }
            invoiceEndNo = FS.FrameWork.Public.String.GetNumber(CurrInvoice.EndNO);
            invoiceEndNoStr = CurrInvoice.EndNO;

            invoiceQty =invoiceEndNo-invoiceUsedNo + 1;



            this.txtStartNo.Text = invoiceUsedNoStr;
            this.txtEndNo.Text = CurrInvoice.EndNO;
            this.txtQty.Text = invoiceQty.ToString();

        }

        /// <summary>
        /// ���������Ч��
        /// </summary>
        /// <returns></returns>
        private bool ValidateValue()
        {
            //2007-5-29�����ӵĴ��� ·־��
            //for (int i = 0, j = this.txtStartNo.Text.Length; i < j; i++)
            //{
            //    if (!char.IsDigit(this.txtStartNo.Text, i))
            //    {
            //        //����˵���ǵڼ����ַ�������
            //        MessageBox.Show("��ʼ��Ʊ�Ÿ�ʽ����ȷ�����������룡", "��ʾ", MessageBoxButtons.OK);
            //        txtStartNo.Focus();
            //        txtStartNo.SelectAll();
            //        return false;
            //    }
            //}
            

            long startNo =FS.FrameWork.Public.String.GetNumber(this.txtStartNo.Text.Trim());
            long endNo = FS.FrameWork.Public.String.GetNumber(this.txtEndNo.Text.Trim());
            long useNo = FS.FrameWork.Public.String.GetNumber(this.txtUsedno.Text.Trim());

            if (startNo > endNo)
            {
                MessageBox.Show("��ʼ�Ų��ܴ�����ֹ�ţ�", "��ʾ", MessageBoxButtons.OK);

                return false;
            }
            if (startNo < invoiceUsedNo || startNo > invoiceEndNo || endNo > invoiceEndNo)
            {
                MessageBox.Show("���յķ�Ʊ������Χ��", "��ʾ", MessageBoxButtons.OK);
                return false;

            }

            CurrInvoice.BeginNO = this.txtStartNo.Text.Trim();
            CurrInvoice.EndNO = this.txtEndNo.Text.Trim();
            CurrInvoice.Qty = (int)(endNo - startNo);

            long Count1 = 0; //Ҫ���յ�����
            long Count2 = endNo - useNo + 1; //ʵ���ܻ��յ�����

            // [2007/02/06] �����ӵĴ���
            for (int i = 0, j = this.txtQty.Text.Length; i < j; i++)
            {
                if (!char.IsDigit(this.txtQty.Text, i))
                {
                    //����˵���ǵڼ����ַ�������
                    MessageBox.Show("���յ���������������", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }
            }
            //�����ӵĴ������

            Count1 = Convert.ToInt32(this.txtQty.Text); //��д��Ҫ���յ�����
            if (Count1 > Count2)
            {
                MessageBox.Show("��������", "��ʾ", MessageBoxButtons.OK);
                this.txtQty.Focus();
                return false;
            }
            if (endNo - startNo< 0)
            {
                MessageBox.Show("��ʼ��Ʊ�Ź���", "��ʾ", MessageBoxButtons.OK);
                this.txtStartNo.Focus();
                return false;
            }
            this.txtStartNo.Text = FS.FrameWork.Public.String.AddNumber(this.txtEndNo.Text.Trim(), -Count1 + 1);// Convert.ToString(Convert.ToInt64(this.txtEndNo.Text) - Count1 + 1);


            return true;

        }

        #endregion

        #region ���з���

        #endregion

        #region �¼�

        private void txtQty_Leave(object sender, EventArgs e)
        {

            // [2007/05/29] �����ӵĴ���
            for (int i = 0, j = this.txtQty.Text.Length; i < j; i++)
            {
                if (!char.IsDigit(this.txtQty.Text, i))
                {
                    //����˵���ǵڼ����ַ�������
                    MessageBox.Show("���յ���������������", "��ʾ", MessageBoxButtons.OK);
                    txtQty.Focus();
                    txtQty.SelectAll();
                    return;
                }
            }

            long Count1 = 0; //Ҫ���յ�����
            long Count2 = Convert.ToInt64(this.txtEndNo.Text) - Convert.ToInt64(this.txtUsedno.Text) + 1; //ʵ���ܻ��յ�����
            Count1 = Convert.ToInt32(this.txtQty.Text); //��д��Ҫ���յ�����
            if (Count1 > Count2)
            {
                MessageBox.Show("Ҫ���յ���������");
                return;
            }
            this.txtStartNo.Text = Convert.ToString(Convert.ToInt64(this.txtEndNo.Text) - Count1 + 1).PadLeft(12, '0');
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateValue())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
                                
    }
}