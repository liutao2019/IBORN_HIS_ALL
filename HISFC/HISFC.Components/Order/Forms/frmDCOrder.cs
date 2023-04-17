using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    /// <summary>
    /// [��������: ҽ��ֹͣ����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmDCOrder : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmDCOrder()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmDCOrder_Load(object sender, EventArgs e)
        {
            try
            {
                this.dateTimeBox1.Value = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddMinutes(5);
                this.dateTimeBox1.MinDate = this.dateTimeBox1.Value.Date;
                //this.cmbDC.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DCREASON));
                System.Collections.ArrayList al = CacheManager.GetConList("DCREASON");
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("��ѯֹͣԭ�����" + CacheManager.InterMgr.Err);
                    return;
                }
                this.cmbDC.AddItems(al);
                if (this.cmbDC.Items.Count > 0)
                {
                    this.cmbDC.SelectedIndex = 0;
                }
            }
            catch
            {
            }
        }


        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DateTime now = CacheManager.InterMgr.GetDateTimeFromSysDateTime();
            if (this.dateTimeBox1.Value.Date < now.Date)
            {
                MessageBox.Show("ֹͣ���ڲ���С�ڵ�ǰ���ڣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch { }
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// ֹͣʱ��
        /// </summary>
        public DateTime DCDateTime
        {
            get
            {
                return this.dateTimeBox1.Value;
            }
        }
        /// <summary>
        /// ֹͣԭ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject DCReason
        {
            get
            {
                if (this.cmbDC.Text == "")
                {
                    return new FS.FrameWork.Models.NeuObject();
                }
                return this.cmbDC.alItems[this.cmbDC.SelectedIndex] as FS.FrameWork.Models.NeuObject;
            }
        }
    }
}