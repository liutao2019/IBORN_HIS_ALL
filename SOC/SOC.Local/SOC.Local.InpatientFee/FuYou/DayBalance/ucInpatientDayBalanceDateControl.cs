using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Local.InpatientFee.FuYou.Function;

namespace FS.SOC.Local.InpatientFee.FuYou.DayBalance
{
    public partial class ucInpatientDayBalanceDateControl : UserControl
    {
        public ucInpatientDayBalanceDateControl()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime dtNow = DateTime.MinValue;
        /// <summary>
        /// �ϴ��ս�ʱ��
        /// </summary>
        public DateTime dtLastBalance = DateTime.MinValue;
        /// <summary>
        /// �ս�ҵ���
        /// </summary>
        InpatientDayBalanceManage inpatientDayBalanceManage = new InpatientDayBalanceManage();

        #endregion
        /// <summary>
        /// �����ϴ��ս�ʱ��
        /// </summary>
        public DateTime DtLastBalance
        {
            get { return dtLastBalance; }
            set 
            {
                dtLastBalance = value;
                this.tbLastDate.Text = dtLastBalance.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// ���������ʾ
        /// </summary>
        private void Clear()
        {
        }

        #region ��ȡ�ս��ֹʱ��(1���ɹ�/-1���Ƿ�)

        /// <summary>
        /// ��ȡ�ս��ֹʱ��(1���ɹ�/-1���Ƿ�)
        /// </summary>
        /// <param name="balanceDate">���ص��ս��ֹʱ��</param>
        /// <returns>1���ɹ�/-1���Ƿ�</returns>
        public int GetBalanceDate(ref string balanceDate)
        {
            DateTime dtInput = DateTime.MinValue;

            // ��ȡ��ǰʱ��
            this.dtNow = this.inpatientDayBalanceManage.GetDateTimeFromSysDateTime();

            // ��ȡ�û�������ս��ֹʱ��
            dtInput = this.dtpBalanceDate.Value;

            // �ж��û�����ĺϷ���
            if (dtInput > this.dtNow)
            {
                MessageBox.Show("�ս�ʱ�䲻�ܴ��ڵ�ǰʱ��");
                this.dtpBalanceDate.Value = this.dtNow;
                this.dtpBalanceDate.Focus();
                return -1;
            }
            else if (dtInput < this.dtLastBalance)
            {
                MessageBox.Show("�ս�ʱ�䲻��С���ϴ��ս�ʱ��");
                this.dtpBalanceDate.Value = this.dtNow;
                return -1;
            }

            // ���÷���ֵ
            balanceDate = dtInput.ToString();

            return 1;
        }

        #endregion
    }
}
