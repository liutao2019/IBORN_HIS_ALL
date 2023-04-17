using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucClinicDayBalanceDateControl : UserControl
    {
        public ucClinicDayBalanceDateControl()
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
        Function.ClinicDayBalance clinicDayBalance = new FS.Report.Finance.FinOpb.Function.ClinicDayBalance();

        /// <summary>
        /// �ϴ��ս�ʱ��
        /// </summary>
        public DateTime dtLastBalance = DateTime.MinValue;

        #endregion

        #region ��ȡ�ս��ֹʱ��(1���ɹ�/-1���Ƿ�)
        /// <summary>
        /// ��ȡ�ս��ֹʱ��(1���ɹ�/-1���Ƿ�)
        /// </summary>
        /// <param name="balanceDate">���ص��ս��ֹʱ��</param>
        /// <returns>1���ɹ�/-1���Ƿ�</returns>
        public int GetBalanceDate(ref string balanceDate)
        {
            //
            // ��������
            //
            // �û�¼����ս��ֹʱ��
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
