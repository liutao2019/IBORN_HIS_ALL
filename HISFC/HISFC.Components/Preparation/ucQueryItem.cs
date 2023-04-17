using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UFC.Preparation
{
    public partial class ucQueryItem : UserControl
    {
        public ucQueryItem()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            InitializeComponent();

            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(txtQueryCode_KeyDown);
            this.txtQuery.TextChanged += new EventHandler(txtQueryCode_TextChanged);
            this.txtQuery.Enter += new EventHandler(txtQuery_Enter);
            this.txtQuery.Leave += new EventHandler(txtQuery_Leave);
            this.chkNew.Checked = isCheck;
            this.chkNew.Visible = isShow;
        }

        /// <summary>
        /// ��Ŀѡ��
        /// </summary>
        public event System.EventHandler SelectItem;

        /// <summary>
        ///  �����ѯ�� ���¼�ʱ���� ��ǰʹ�������б����ʱ ���������¼�
        /// </summary>
        public event System.Windows.Forms.KeyEventHandler TextKeyDown;

        /// <summary>
        /// �����ѯ�� �ַ������仯ʱ���� ��ǰʹ�������б����ʱ ���������¼�
        /// </summary>
        public new event System.EventHandler TextChanged;

        /// <summary>
        /// ҩƷ��ѯ                         
        /// </summary>
        protected frmItemList frmItem = null;

        /// <summary>
        /// �Ƿ�ʹ�������Text����ʾ
        /// </summary>
        private bool isUseSpeText = true;

        /// <summary>
        ///  �Ƿ����
        /// </summary>
        private bool isFilter = true;

        public bool isCheck = true;
        public bool isShow = true;

        #region ����
        /// <summary>
        /// TextBox�ؼ��ڵ�ǰ�ַ�
        /// </summary>
        public string TxtStr
        {
            get
            {
                return this.txtQuery.Text;
            }
        }
        /// <summary>
        /// ��ѯ��ǩ����
        /// </summary>
        public string LabelName
        {
            set
            {
                this.lblQuery.Text = value;
            }
        }
        /// <summary>
        /// �Ƿ�����CheckBox ������Ϊ������ ����Ҫѡ�иð�ť�ſ��Ե���ҩƷ���˽���
        /// </summary>
        public bool IsHideAdd
        {
            set
            {
                if (value)
                {
                    this.chkNew.Checked = true;
                    this.Width = 270;
                }
                else
                {
                    this.chkNew.Checked = false;
                    this.Width = 333;
                }
            }
        }
        /// <summary>
        /// �Ƿ�ʹ�������Text����ʾ
        /// </summary>
        public bool IsUseSpeText
        {
            set
            {
                this.isUseSpeText = value;
            }
        }
        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="isShow">��ʼ�����Ƿ���ʾ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int Init(bool isShow,string drugType)
        {
            if (frmItem == null)
            {
                frmItem = new frmItemList();
                frmItem.Init(drugType);
                frmItem.Owner = this.FindForm();
                //System.Drawing.Point loc = new Point(this.Left, this.Top + this.txtQuery.Height);
                //frmItem.Location = this.txtQuery.PointToScreen(loc);
                frmItem.SelectItem += new EventHandler(frmItem_SelectItem);
            }
            frmItem.Hide();
            return 1;
        }

        /// <summary>
        /// ����Text������ʾ
        /// </summary>
        /// <param name="isInit"></param>
        public void SetTextFace(bool isInit)
        {
            if (isInit)
            {
                this.isFilter = false;
                if (this.frmItem != null)
                    this.frmItem.FrmVisible = false;
                this.txtQuery.ForeColor = System.Drawing.Color.DarkGray;
                this.txtQuery.Text = "ͨ�������ѯҩƷ";
            }
            else
            {
                this.isFilter = true;
                this.txtQuery.ForeColor = System.Drawing.Color.Black;
                this.txtQuery.Text = "";
            }
        }

        private void txtQueryCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.frmItem != null && this.chkNew.Checked)
            {
                frmItem.Key(e.KeyCode);
                e.Handled = true;
                if (e.KeyCode != Keys.Enter)
                    this.txtQuery.Focus();
            }
            else
            {
                if (this.TextKeyDown != null)
                {
                    this.TextKeyDown(sender, e);
                }
            }
        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            if (this.isFilter)
            {
                if (this.chkNew.Checked)
                {
                    #region �������б����
                    if (frmItem == null)
                    {
                        this.Init(true,"E");
                    }
                    else
                    {
                        #region λ�ó�ʼ��
                                System.Drawing.Point loc = new Point(this.Left, this.Top + this.txtQuery.Height);
                                frmItem.Location = this.txtQuery.PointToScreen(loc);
                        #endregion
                    }

                    if (this.frmItem.FrmVisible)
                    {
                        frmItem.Filter(this.txtQuery.Text);
                    }
                    else
                    {
                        frmItem.FrmVisible = true;
                        frmItem.Filter(this.txtQuery.Text);
                    }
                    this.txtQuery.Focus();
                    #endregion
                }
                else
                {
                    if (this.TextChanged != null)
                    {
                        this.TextChanged(sender, e);
                    }
                }
            }
        }

        private void frmItem_SelectItem(object sender, EventArgs e)
        {
            if (this.SelectItem != null)
            {
                this.SelectItem(sender, System.EventArgs.Empty);
            }
        }

        private void txtQuery_Enter(object sender, EventArgs e)
        {
            if (this.isUseSpeText)
                this.SetTextFace(false);
        }

        private void txtQuery_Leave(object sender, EventArgs e)
        {
            if (this.isUseSpeText)
                this.SetTextFace(true);
        }
    }
}
