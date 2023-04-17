using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucInjec : UserControl
    {
        public ucInjec()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="injecs"></param>
        public delegate void myDelegate(decimal injecs);
        
        /// <summary>
        /// 
        /// </summary>
        public event myDelegate WhenInputInjecs;

        #endregion

        #region ����

        /// <summary>
        /// �������Ĵ���
        /// </summary>
        /// <returns></returns>
        private decimal GetInjecs()
        {
            decimal injecs = 0;
            try
            {
                injecs = FS.FrameWork.Function.NConvert.ToDecimal(this.tbInjec.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ֲ��Ϸ�!����������!" + ex.Message);
                this.tbInjec.SelectAll();
                this.tbInjec.Focus();
                return -1;
            }
            if (injecs < 0)
            {
                MessageBox.Show("Ժע��������С��0");
                this.tbInjec.SelectAll();
                this.tbInjec.Focus();
                return -1;
            }

            return injecs;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInjec_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal injecs = 0;
                injecs = GetInjecs();
                if (injecs < 0)
                {
                    return;
                }
                WhenInputInjecs(injecs);
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbOk_Click(object sender, System.EventArgs e)
        {
            decimal injecs = 0;
            injecs = GetInjecs();
            if (injecs == -1)
            {
                return;
            }
            WhenInputInjecs(injecs);
            this.FindForm().Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCancel_Click(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
            WhenInputInjecs(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInjec_Load(object sender, System.EventArgs e)
        {
            this.tbInjec.Text = "";
            this.tbInjec.Focus();
            try
            {
                this.FindForm().Text = "Ժ��ע�����";
            }
            catch
            { }
        }

        #endregion
    }
}
