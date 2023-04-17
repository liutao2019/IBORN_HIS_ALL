using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.MetCas
{
    public partial class ucMetPatiCard : Common.ucQueryBaseForDataWindow
    {
        public ucMetPatiCard()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department( );

        #endregion 

        protected override int OnRetrieve( params object[] objects )
        {
            string str��ʼסԺ�� = string.Empty;
            if (base.GetQueryTime( ) == -1)
            {
                return -1;
            }
            str��ʼסԺ�� = this.txt��ʼסԺ��.Text.Trim();
            if (str��ʼסԺ�� == null || str��ʼסԺ�� == "")
            {
                str��ʼסԺ�� = "ALL";
            }
            else
            {
                str��ʼסԺ�� = str��ʼסԺ��.PadLeft(10, '0');
            }
            base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,str��ʼסԺ��);
            this.txt������.Text = "������:" + base.dwMain.RowCount.ToString();
            return 1;
        }

        private void ucMetPatiCard_Load( object sender, EventArgs e )
        {
            DateTime sysTime = this.dept.GetDateTimeFromSysDateTime( );
            this.dtpBeginTime.Text = sysTime.AddDays(-1).ToShortDateString( ) + " 00:00:00";
            this.dtpEndTime.Text = sysTime.AddDays(-1).ToShortDateString( ) + " 23:59:59";

        }
    }
}
