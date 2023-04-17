using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy
{
    public partial class frmEasyData : Form
    {
        public frmEasyData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ѡ��ؼ������Ʊ�����ʾ
        /// </summary>
        public string EasyLabel
        {
            set
            {
                this.lbName.Text = value;
            }
        }

        /// <summary>
        /// ������󳤶�
        /// </summary>
        public int MaxLength
        {
            set
            {
                this.txtData.MaxLength = value;
            }
        }

        /// <summary>
        /// �û���������
        /// </summary>
        public string EasyData
        {
            get
            {
                return this.txtData.Text;
            }
            set
            {
                this.txtData.Text = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtData.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����������"));
                return;
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}