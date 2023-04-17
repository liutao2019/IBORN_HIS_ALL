using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucSimpleChange : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSimpleChange()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// TabPage��ǩ ����˵�����ε��ù���
        /// </summary>
        public string TitleLabel
        {
            set
            {
                this.tabPage1.Text = value;
            }
        }
        /// <summary>
        /// ��Ҫ����Ϣ˵��
        /// </summary>
        public string InfoLabel
        {
            set
            {
                this.neuLabel1.Text = value;
            }
        }
        /// <summary>
        /// ���ε��ò���˵��
        /// </summary>
        public string OperInfo
        {
            set
            {
                this.neuLabel2.Text = value;
            }
        }
        private ArrayList infoItems;
        /// <summary>
        /// Combox��ѡ������ 
        /// </summary>
        public ArrayList InfoItems
        {
            get
            {
                if (infoItems == null)
                    infoItems = new ArrayList();
                return infoItems;
            }
            set
            {
                if (value != null)
                    infoItems = value;
                this.neuComboBox1.AddItems(value);
            }
        }
        private object returnInfo;
        /// <summary>
        /// ȷ����ť�󷵻ص���Ϣ
        /// </summary>
        public object ReturnInfo
        {
            get
            {
                if (this.returnInfo == null)
                    this.returnInfo = new object();
                return this.returnInfo;
            }
        }
        private int iReturn = 0;
        public int IReturn
        {
            get
            {
                return iReturn;
            }
        }
        #endregion

        private void ucSimpleChange_Load(object sender, EventArgs e)
        {
            this.neuComboBox1.AddItems(this.InfoItems);
            this.neuComboBox1.Focus();

            #region �¼�����
            this.neuComboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            #endregion
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.returnInfo = this.neuComboBox1.alItems[this.neuComboBox1.SelectedIndex] as object;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.iReturn = 1;
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.iReturn = 0;
            this.FindForm().Close();
        }
    }
}
