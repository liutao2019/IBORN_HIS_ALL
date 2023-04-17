using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Local.OutpatientFee.GYZL;

namespace FS.SOC.Local.OutpatientFee.GYZL.DayBalance
{
    public partial class ucOutPatientDayBalanceDateControl : UserControl
    {
        public ucOutPatientDayBalanceDateControl()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime dtNow = DateTime.MinValue;

        /// <summary>
        /// �ս᷽����
        /// </summary>
        FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance clinicDayBalance = new FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance();

        /// <summary>
        /// �ϴ��ս�ʱ��
        /// </summary>
        public DateTime dtLastBalance = DateTime.MinValue;

        #endregion

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
            this.dtNow = this.clinicDayBalance.GetDateTimeFromSysDateTime();

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
