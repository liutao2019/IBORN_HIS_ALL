using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.DayBalance
{
    public partial class ucClinicDayBalanceReport : UserControl
    {
        public ucClinicDayBalanceReport()
        {
            InitializeComponent();
        }

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitUC()
        {
            // ����ҽԺ����
            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            this.labelHospital.Text = constant.GetHospitalName() + "�����շ�Ա�ɿ��ձ���";
        }

        #endregion
    }
}
