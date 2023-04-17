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
    public partial class ucReprintDateControl : UserControl
    {
        public ucReprintDateControl()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime dtNowDateTime = DateTime.MinValue;
        /// <summary>
        /// �ս�ҵ���
        /// </summary>
        InpatientDayBalanceManage inpatientDayBalanceManage = new InpatientDayBalanceManage();
        #endregion

        #region �����û����õ��ս��ѯʱ��(1���ɹ���ȡ/-1������Ƿ�)
        /// <summary>
        /// �����û����õ��ս��ѯʱ��(1���ɹ���ȡ/-1������Ƿ�)
        /// </summary>
        /// <param name="dtFrom">�û��������ʼʱ��</param>
        /// <param name="dtTo">�û�����Ľ�ֹʱ��</param>
        /// <returns>1���ɹ���ȡ/-1������Ƿ�</returns>
        public int GetInputDateTime(ref DateTime dtFrom, ref DateTime dtTo)
        {
            //
            // ��ȡ�û�����ʱ���ϵͳ��ǰʱ��
            //
            // ��ȡ��ǰʱ��
            this.dtNowDateTime = this.inpatientDayBalanceManage.GetDateTimeFromSysDateTime();
            // �û�¼�����ʼʱ��
            dtFrom = this.dtpDateFrom.Value;
            // �û�¼��Ľ�ֹʱ��
            dtTo = this.dtpDateTo.Value;

            //
            // �ж��û�����ĺϷ���
            //
            if (this.dtNowDateTime < dtTo)
            {
                MessageBox.Show("��ֹ���ڲ��ܴ��ڵ�ǰ����");
                this.dtpDateTo.Value = this.dtNowDateTime;
                this.dtpDateTo.Focus();
                return -1;
            }
            if (dtFrom > dtTo)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڿ�ʼʱ��");
                this.dtpDateFrom.Focus();
                return -1;
            }

            return 1;
        }
        #endregion

        #region ��UC�����¼�
        /// <summary>
        /// ��UC�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucReprintDateControl_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            // ���ý�ֹʱ��Ϊ��ǰ������ʱ��
            this.dtpDateFrom.Value = inpatientDayBalanceManage.GetDateTimeFromSysDateTime();
            this.dtpDateTo.Value = inpatientDayBalanceManage.GetDateTimeFromSysDateTime();
        }
        #endregion
    }
}
