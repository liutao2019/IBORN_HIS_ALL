using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class frmDiscountCardReturn : Form
    {
        #region ����

        private FS.HISFC.Models.Fee.DiscountCard CurrDiscountCard;
        private long UsedNo;
        private string UsedNoStr;
        private long EndNo;
        private string EndNoStr;
        private long Qty;

        #endregion

        public frmDiscountCardReturn(FS.HISFC.Models.Fee.DiscountCard discountCard)
        {
            InitializeComponent();
            this.CurrDiscountCard = discountCard;
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
            this.txtUsedno.Text = CurrDiscountCard.UsedNo;

            if (CurrDiscountCard.UsedState == "0")
            {
                UsedNo = FS.FrameWork.Public.String.GetNumber(CurrDiscountCard.StartNo);
                UsedNoStr = CurrDiscountCard.StartNo;
            }
            else
            {
                UsedNo = FS.FrameWork.Public.String.GetNumber(CurrDiscountCard.UsedNo) + 1;
                UsedNoStr = FS.FrameWork.Public.String.AddNumber(CurrDiscountCard.UsedNo, 1);
            }
            EndNo = FS.FrameWork.Public.String.GetNumber(CurrDiscountCard.EndNo);
            EndNoStr = CurrDiscountCard.EndNo;

            Qty = EndNo-UsedNo + 1;



            this.txtStartNo.Text = UsedNoStr;
            this.txtEndNo.Text = CurrDiscountCard.EndNo;
            this.txtQty.Text = Qty.ToString();

        }

        /// <summary>
        /// ���������Ч��
        /// </summary>
        /// <returns></returns>
        private bool ValidateValue()
        {

            long startNo =FS.FrameWork.Public.String.GetNumber(this.txtStartNo.Text.Trim());
            long endNo = FS.FrameWork.Public.String.GetNumber(this.txtEndNo.Text.Trim());
            long useNo = FS.FrameWork.Public.String.GetNumber(this.txtUsedno.Text.Trim());

            if (startNo > endNo)
            {
                MessageBox.Show("��ʼ�Ų��ܴ�����ֹ�ţ�", "��ʾ", MessageBoxButtons.OK);

                return false;
            }
            if (startNo < UsedNo || startNo > EndNo || endNo > EndNo)
            {
                MessageBox.Show("���յĿ�������Χ��", "��ʾ", MessageBoxButtons.OK);
                return false;

            }

            CurrDiscountCard.StartNo = this.txtStartNo.Text.Trim();
            CurrDiscountCard.EndNo = this.txtEndNo.Text.Trim();
            CurrDiscountCard.QTY = (endNo - startNo).ToString();

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
                MessageBox.Show("��ʼ�Ź���", "��ʾ", MessageBoxButtons.OK);
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