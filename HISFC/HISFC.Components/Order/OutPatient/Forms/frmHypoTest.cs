using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    /// <summary>
    /// Ƥ�Խ��ѡ��
    /// </summary>
    public partial class frmHypoTest : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmHypoTest()
        {
            InitializeComponent();
        }

        #region MyRegion
        private bool isEditMode = false;

        #endregion



        /// <summary>
        /// �Ƿ�༭ģʽ
        /// </summary>
        public bool IsEditMode
        {
            get
            {
                return this.isEditMode;
            }
            set
            {
                this.isEditMode = value;
                if (this.isEditMode)
                {
                    this.rb1.Enabled = true;
                    this.rb2.Enabled = true;
                }
                else
                {
                    this.rb1.Enabled = false;
                    this.rb2.Enabled = false;
                }
            }
        }

        /// <summary>
        /// ҩƷ����
        /// </summary>
        public string ItemName
        {
            set
            {
                this.lblDrugName.Text = value;
            }
        }

        /// <summary>
        /// Ƥ�Խ��
        /// </summary>
        public int Hypotest
        {
            get
            {
                if (this.rb1.Checked)
                {
                    return 1;
                }
                else if (this.rb2.Checked)
                {
                    return 4;
                }
                else if (this.rb3.Checked)//{BCF43AF9-C17E-43e3-8E21-E273CE96975D}
                {
                    return 2;
                }
                else if (this.rb4.Checked)
                {
                    return 3;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (value == 1)
                {
                    this.rb1.Checked = true;
                }
                else if (value == 4)
                {
                    this.rb2.Checked = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
            if (!this.rb4.Checked && !this.rb3.Checked && !this.rb2.Checked && !this.rb1.Checked)
            {
                MessageBox.Show("��������һ��״̬");
                return;
            }
            this.Close();
        }

    }
}