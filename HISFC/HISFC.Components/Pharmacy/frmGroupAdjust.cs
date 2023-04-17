using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ������������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    internal partial class frmGroupAdjust : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmGroupAdjust()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ������Ŀ
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.Item> alAllItem;

        /// <summary>
        /// ������Ŀ
        /// </summary>
        private ArrayList adjustItems;

        /// <summary>
        /// �۸�ʽ
        /// </summary>
        private string priceException = "";

        #endregion

        #region ����

        /// <summary>
        /// ���ι���ѡ��������Ŀ
        /// </summary>
        public List<FS.HISFC.Models.Pharmacy.Item> AllItem
        {
            set
            {
                this.alAllItem = value;
            }
        }
      
        /// <summary>
        /// ������Ŀ
        /// </summary>
        public ArrayList AdjustItems
        {
            get
            {
                return this.adjustItems;
            }
        }

        /// <summary>
        /// �۸�ʽ
        /// </summary>
        public string PriceException
        {
            get
            {
                return this.priceException;
            }
        }

        #endregion


        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            this.adjustItems = new ArrayList();

            this.priceException = "";
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            this.priceException = this.txtException.Text;

            if (this.priceException == "")
                return true;

            string priceStr = "";

            try
            {
                priceStr = string.Format(this.priceException, "1");
            }
            catch (Exception e)
            {
                MessageBox.Show("���۹�ʽ���벻��ȷ �������ʾ��Ϣ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            object o = FS.FrameWork.Public.String.ExpressionVal(priceStr);
            if (o == null)
            {
                MessageBox.Show("��ʽ���ò����Ϲ��� �����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ���ݴ����ҩƷ��Ϣ���ݹ�ʽ���������ۼ�
        /// </summary>
        /// <param name="item">����ҩƷ��Ϣ</param>
        /// <returns>�ɹ����������ۼ� ������-1</returns>
        public decimal GetNewPrice(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (this.rbRetailFlag.Checked)
                return this.GetNewPrice(item.PriceCollection.RetailPrice);
            else
                return this.GetNewPrice(item.PriceCollection.PurchasePrice);
        }

        /// <summary>
        /// ��ȡ�¼۸�
        /// </summary>
        /// <param name="oldPrice"></param>
        /// <returns></returns>
        public decimal GetNewPrice(decimal oldPrice)
        {
            this.priceException = this.txtException.Text;

            string priceStr = "";

            try
            {
                priceStr = string.Format(this.priceException, oldPrice.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("���۹�ʽ���벻��ȷ �������ʾ��Ϣ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            object o = FS.FrameWork.Public.String.ExpressionVal(priceStr);
            if (o == null)
            {
                return -1;
            }
            else
            {
                return FS.FrameWork.Function.NConvert.ToDecimal(o);
            }
        }


        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (!this.Valid())
                return;

            this.adjustItems = new ArrayList();

            if (this.alAllItem != null && !this.ckOnlyPrice.Checked)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item info in this.alAllItem)
                {
                    if (info.Type.ID == "P" && this.ckDrugP.Checked)
                    {
                        this.adjustItems.Add(info);
                        continue;
                    }
                    if (info.Type.ID == "C" && this.ckDrugC.Checked)
                    {
                        this.adjustItems.Add(info);
                        continue;
                    }
                    if (info.Type.ID == "Z" && this.ckDrugZ.Checked)
                    {
                        this.adjustItems.Add(info);
                    }
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void ckOnlyPrice_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ckOnlyPrice.Checked)
                this.neuGroupBox1.Enabled = false;
            else
                this.neuGroupBox1.Enabled = true;
        }

    }
}