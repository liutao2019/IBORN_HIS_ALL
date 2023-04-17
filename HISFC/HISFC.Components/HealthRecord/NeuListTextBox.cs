using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
namespace FS.HISFC.Components.HealthRecord
{
    /// <summary>
    /// NeuListTextBox<br></br>
    /// [��������: �������б���ı������]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-5]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    class NeuListTextBox : FS.FrameWork.WinForms.Controls.NeuTextBox
    {
        #region  ˽�б���
        FS.FrameWork.WinForms.Controls.PopUpListBox listBox = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        bool keyEnterVisiable = false;//����ؼ�ʱ�����б��ɼ�
        FS.FrameWork.Models.NeuObject selectObj; //��ǰѡ�е���Ŀ
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();
        bool IsExist = false; //�Ƿ��Ѿ�����
        private System.Windows.Forms.Control parentControl; //�����ؼ�
        int LocaltionX; //λ��X
        int LocaltionY;//λ��Y
        #endregion

        #region ����
        [Description("����ؼ�ʱ�����б��ɼ�")]
        public bool EnterVisiable
        {
            get
            {
                return keyEnterVisiable;
            }
            set
            {
                keyEnterVisiable = false;
            }
        }
        [Description("������Ŀ��")]
        public int ListBoxWidth
        {
            get
            {
                return listBox.Width;
            }
            set
            {
                listBox.Width = value;
            }
        }
        [Description("������Ŀ��")]
        public int ListBoxHeight
        {
            get
            {
                return listBox.Height;
            }
            set
            {
                listBox.Height = value;
            }
        }
        [Description("ģ����ѯ")]
        public bool OmitFilter
        {
            get
            {
                return listBox.OmitFilter;
            }
            set
            {
                listBox.OmitFilter = value;
            }
        }
        /// <summary>
        /// ����Tag
        /// </summary>
        [Description("����Tag")]
        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                base.Tag = value;
                if (base.Tag != null)
                {
                    this.Text = objHelper.GetName(base.Tag.ToString());
                }

            }
        }
        #endregion

        #region �����к���
        /// <summary>
        /// ɸѡ�¼�
        /// </summary>
        public NeuListTextBox()
        {
            listBox.Width = this.Width;
            listBox.Height = 100;
            //parentControl = this;
            this.TextChanged += new EventHandler(NeuListTextBox_TextChanged);
            this.Enter += new EventHandler(NeuListTextBox_Enter);
            this.KeyDown += new KeyEventHandler(NeuListTextBox_KeyDown);

            //Controls.Add(listBox);
            //����
            listBox.Hide();
            //���ñ߿�
            listBox.BorderStyle = BorderStyle.FixedSingle;
            //listBox.BringToFront();
            //�����¼�
            listBox.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(listBox_SelectItem); //new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(ICDListBox_SelectItem);
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddItems(ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            objHelper.ArrayObject = list;
            return listBox.AddItems(list);
        }
        #endregion

        #region  ˽�к���
        int listBox_SelectItem(Keys key)
        {
            return GetSelectItem();
        }
        int GetSelectItem()
        {
            int rtn = listBox.GetSelectedItem(out selectObj);
            if (selectObj == null)
            {
                return -1;
            }
            if (selectObj.ID != "")
            {
                base.Tag = selectObj.ID;
                this.Text = selectObj.Name;
            }
            else
            {
                this.listBox.Tag = null;
            }
            listBox.Focus(); //��ý���
            this.listBox.Visible = false;
            this.Focus();
            return rtn;
        }
        /// <summary>
        /// ����ؼ�ʱ �����б��Ƿ�ɼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NeuListTextBox_Enter(object sender, EventArgs e)
        {
            this.listBox.Visible = this.EnterVisiable;
            AddControl();
        }
        /// <summary>
        /// ɸѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NeuListTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetLocation();
                this.listBox.Visible = true;
                this.listBox.Filter(this.Text.Trim());
            }
            catch { }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NeuListTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                listBox.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                listBox.NextRow();
            }
        }
        /// <summary>
        /// ���ؿؼ�
        /// </summary>
        void AddControl()
        {
            if (!IsExist)
            {
                LocaltionX = 0;
                LocaltionY = 0;
                parentControl = GetParent(this);
                parentControl.Controls.Add(listBox);
                IsExist = true;

                if (listBox.Width < this.Width)
                {
                    listBox.Width = this.Width;
                }
                if (this.parentControl.Width < LocaltionX + listBox.Width)
                {
                    //if ((parentControl.Width - listBox.Width - LocaltionX) > 0)
                    //{
                    //    LocaltionX = LocaltionX - (parentControl.Width - listBox.Width - LocaltionX);
                    //}
                    //else
                    //{
                    LocaltionX = LocaltionX - System.Math.Abs(listBox.Width + LocaltionX - parentControl.Width);
                    //}
                }

                if (this.parentControl.Height < LocaltionY + listBox.Height)
                {
                    if (parentControl.Height - listBox.Height - this.Height > 0)
                    {
                        LocaltionY = LocaltionY  - listBox.Height - this.Height - 2;
                    }
                }
            }
            listBox.BringToFront();
        }
        /// <summary>
        /// ��ȡ�����ؼ�
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private Control GetParent(Control control)
        {
            try
            {
                if (control.Parent != null)
                {
                    LocaltionX += control.Location.X;
                    LocaltionY += control.Location.Y;
                    return GetParent(control.Parent);
                }
                else
                {
                    return control;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return control;
            }
        }
        /// <summary>
        /// ����λ��
        /// </summary>
        void SetLocation()
        {
            listBox.Location = new System.Drawing.Point(LocaltionX + 2, LocaltionY + Height + 2);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (listBox.Visible)
                {
                    GetSelectItem();
                }
            }
            if (keyData == Keys.Escape)
            {
                listBox.Visible = false;
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
