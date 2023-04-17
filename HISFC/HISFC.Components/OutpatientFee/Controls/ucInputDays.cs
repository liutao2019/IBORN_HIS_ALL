using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucInputDays : UserControl
    {
        public ucInputDays()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        private int days;

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        private string combNO = string.Empty;

        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        private bool isSelect = false;

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        public int Days
        {
            get
            {
                return days;
            }
        }

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        public string CombNO
        {
            get
            {
                return combNO;
            }
            set
            {
                this.tbCombNo.Text = value;
            }
        }

        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        public bool IsSelect 
        {
            get 
            {
                return this.isSelect;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int GetDays()
        {
            string tempDays = this.tbDays.Text;
            if (tempDays == null || tempDays.Trim() == "")
            {
                MessageBox.Show(Language.Msg("�����븶��!"));
                this.tbDays.SelectAll();
                this.tbDays.Focus();

                return -1;
            }
            if (tempDays.Length >= 3)
            {
                MessageBox.Show(Language.Msg("�����븶��С��100!"));
                this.tbDays.SelectAll();
                this.tbDays.Focus();

                return -1;
            }
            if (tempDays.IndexOf(".") >= 0)
            {
                MessageBox.Show(Language.Msg("��������������!"));
                this.tbDays.SelectAll();
                this.tbDays.Focus();

                return -1;
            }
            try
            {
                days = int.Parse(tempDays);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("�������벻�Ϸ�!") + ex.Message);
                this.tbDays.SelectAll();
                this.tbDays.Focus();

                return -1;
            }
            string tmpCombNo = this.tbCombNo.Text;
            if (tmpCombNo != null && tmpCombNo.Length > 14)
            {
                MessageBox.Show(Language.Msg("��Ϻ��������,���ܳ���14λ!����������"));
                this.tbCombNo.SelectAll();
                this.tbCombNo.Focus();

                return -1;
            }
            this.combNO = tmpCombNo;

            return 1;
        }

        #endregion

        #region �¼�
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.isSelect = false;
                this.FindForm().Close();
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GetDays() != -1)
            {
                this.isSelect = true;
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.tbCombNo.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCombNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        #endregion
    }
}
