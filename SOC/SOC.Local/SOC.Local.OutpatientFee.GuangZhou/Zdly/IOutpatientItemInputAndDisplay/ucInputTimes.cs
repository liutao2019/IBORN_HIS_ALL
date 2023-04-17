using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    /// <summary>
    /// ucInputTimes<br></br>
    /// [��������: ������ϴ���UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-2-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucInputTimes : UserControl
    {
        public ucInputTimes()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        private int times = 1;

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        public int Times
        {
            get
            {
                return this.times;
            }
            set
            {
                this.times = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ô���
        /// </summary>
        /// <returns></returns>
        private int GetInjecs()
        {
            int injecs = 1;
            try
            {
                injecs = FS.FrameWork.Function.NConvert.ToInt32(this.tbInjec.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ֲ��Ϸ�!����������!" + ex.Message);
                this.tbInjec.SelectAll();
                this.tbInjec.Focus();
                return -1;
            }
            if (injecs < 1)
            {
                MessageBox.Show("��������С��1");
                this.tbInjec.SelectAll();
                this.tbInjec.Focus();
                return -1;
            }
            this.times = injecs;

            return injecs;
        }

        #endregion

        #region �¼�

        private void tbOk_Click(object sender, System.EventArgs e)
        {
            int injecs = GetInjecs();
            if (injecs == -1)
            {
                return;
            }
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        private void tbCancel_Click(object sender, System.EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

        private void ucInjec_Load(object sender, System.EventArgs e)
        {
            this.tbInjec.Text = "1";
            this.tbInjec.Focus();
            this.tbInjec.SelectAll();
            try
            {
                this.FindForm().Text = "���뱶��";
            }
            catch
            { }
        }

        private void tbInjec_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.tbOk_Click(null, null);
            }
        }

        #endregion
    }
}
