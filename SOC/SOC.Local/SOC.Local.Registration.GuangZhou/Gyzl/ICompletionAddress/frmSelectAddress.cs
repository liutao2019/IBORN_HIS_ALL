using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.ICompletionAddress
{
    /// <summary>
    /// ��ѯ�Ļ�����Ϣ����һ��ѡ����UC
    /// </summary>
    public partial class frmSelectAddress : Form
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmSelectAddress()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
        }

        #region ����
        /// <summary>
        /// ��ѡ������ַ��Ϣ
        /// </summary>
        public delegate void GetAddress(String address);

        /// <summary>
        /// ��ѡ������ַ�󴥷�
        /// </summary>
        public event GetAddress SelectedAddress;

        /// <summary>
        /// ��ǰѡ��ĵ�ַ
        /// </summary>
        private string address;
        #endregion

        #region ����

        public string Address
        {
            get
            {
                if (this.address != null)
                {
                    return this.address;
                }
                else
                {
                    if (this.fpSpread1_Sheet1.RowCount == 0)
                    {
                        return "";
                    }
                    else
                    {
                        int row = this.fpSpread1_Sheet1.ActiveRowIndex;
                        return this.fpSpread1_Sheet1.GetText(row, 0);
                    }
                }

            }
        }

        private CompletionAddressManager completionAddressManager = new CompletionAddressManager();

        #endregion

        #region ����

        /// <summary>
        /// ��ѯ��ȫ��ַ
        /// </summary>
        /// <param name="curAddress"></param>
        /// <returns>���Բ�ȫ�ĵ�ַ����</returns>
        public int QueryAddress(string curAddress)
        {
            ArrayList addressList = null;
            //��ѯ��ȫ��ַ
            addressList = completionAddressManager.QueryAddressList(curAddress);

            if (addressList == null)
            {
                return 0;
            }

            if (addressList.Count == 1)
            {
                this.address = addressList[0] as string;
                return 1;
            }
            else if (addressList.Count > 1)
            {
                setValue(addressList);
                return addressList.Count;
            }
            else
            {
                return -1;
            }
        }

        private void setValue(ArrayList al)
        {
            //�������
            this.fpSpread1_Sheet1.RowCount = 0;
            int curRow = 0;
            foreach (string strAddress in al)
            {
                curRow = this.fpSpread1_Sheet1.RowCount++;
                this.fpSpread1_Sheet1.Cells[curRow, 0].Text = strAddress;
                this.fpSpread1_Sheet1.Cells[curRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            }
        }

        #endregion

        #region �¼�
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text))
                    {
                        this.SelectAddress(this.fpSpread1_Sheet1.ActiveRowIndex);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// ˫��FP�¼�,ѡ���ַ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpSpread1_Sheet1.RowCount == 0) return;
            int row = e.Row;
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (string.IsNullOrEmpty(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text))
                {
                    this.SelectAddress(row);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// ˫�����س���ѡ���ַ
        /// </summary>
        /// <param name="row">��ǰ��</param>
        private void SelectAddress(int row)
        {
            if (this.SelectedAddress != null)
            {
                this.SelectedAddress(this.fpSpread1_Sheet1.Cells[row, 0].Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                if (this.SelectedAddress != null)
                {
                    this.SelectedAddress(null);
                }
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ���ؼ���ý����ʱ��,��FP��ý���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryPatientByName_Enter(object sender, EventArgs e)
        {
            this.fpSpread1.Focus();
        }

        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0
                || this.fpSpread1_Sheet1.SelectionCount == 0)
            {
                return;
            }
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (string.IsNullOrEmpty(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text))
                {
                    this.SelectAddress(row);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}