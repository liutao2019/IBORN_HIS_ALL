using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbDeptFeeIncome3 : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbDeptFeeIncome3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���ڴ洢��������
        /// </summary>
        private System.Collections.ArrayList inStateList = new System.Collections.ArrayList();
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant contManager = new FS.HISFC.BizLogic.Manager.Constant();

        protected override void OnLoad(EventArgs e)
        {
            this.isAcross = true;
            this.isSort = false;
            base.OnLoad(e);

            // ͳ�ƴ��������б�
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList list_bigtype = manager.GetConstantList("FEECODESTAT");

            foreach (FS.HISFC.Models.Base.Const con in list_bigtype)
            {
                this.cmbReportType.Items.Add(con);
            }

            this.cmbReportType.alItems.AddRange(list_bigtype);
            this.cmbReportType.SelectedIndex = 0;

            this.cmbType.SelectedIndex = 0;
            this.neuComboBox1.SelectedIndex = 1;
        }

        protected override int OnRetrieve(params object[] objects)
        {

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            string selectType = this.cmbType.Text;
            string reportType = this.cmbReportType.SelectedItem.ID;
            string inState = this.neuComboBox1.Text;

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, selectType, inState, reportType);
        }

    }
}