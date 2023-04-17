using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
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
    public class NeuListTextBox : System.Windows.Forms.TextBox
    {
        #region  ˽�б���
        //public FS.UFC.HealthRecord.Common.PopUpListBox listBox = new FS.UFC.HealthRecord.Common.PopUpListBox();
        public FS.HISFC.Components.HealthRecord.CaseFirstPage.PopUpListBox listBox = new PopUpListBox();
  
        bool keyEnterVisiable = false;//����ؼ�ʱ�����б��ɼ�
        FS.FrameWork.Models.NeuObject selectObj; //��ǰѡ�е���Ŀ
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();
        bool IsExist = false; //�Ƿ��Ѿ�����
        private System.Windows.Forms.Control parentControl; //�����ؼ�
        int LocaltionX; //λ��X
        int LocaltionY;//λ��Y
        bool isFind = false;//���������Ҳ�������ʱ�������
        private bool specalFlag = true; //����ѡ����Ŀ��ɼ����ɼ��Ĵ���

        private System.Drawing.Font listFont = new System.Drawing.Font("����", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

        public delegate void ChangObjName(string name);

        public event ChangObjName ChangObjNameEvent;

        public delegate void SelectItemChangDele();

        public event SelectItemChangDele SelectItemChangEvent;

        public delegate void SendToNextDele();

        public event SendToNextDele SendToNextEvent;

        /// <summary>
        /// ���ı�������ʾID
        /// </summary>
        private bool isShowID = false;

        [Description("���ı�������ʾID")]
        public bool IsShowID
        {
            get
            {
                return isShowID;
            }
            set
            {
                isShowID = value;
            }
        }

        string itemText = "";

        public string ItemText
        {
            get
            {
                return itemText;
            }
            set
            {
                if (this.ChangObjNameEvent != null)
                {
                    this.ChangObjNameEvent(value);
                }
            }
        }

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
                keyEnterVisiable = value;
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
        [Description("�Ƿ�Ĭ�ϲ�ѡ���κ���")]
        public bool SelectNone
        {
            get
            {
                return listBox.SelectNone;
            }
            set
            {
                listBox.SelectNone = value;
            }
        }
        [Description("���������Ҳ�������ʱ�������")]
        public bool IsFind
        {
            get
            {
                return isFind;
            }
            set
            {
                isFind = value;
            }
        }
        [Description("�Ƿ���ʾ�б�� ID")]
        public bool ShowID
        {
            get
            {
                return listBox.IsShowID;
            }
            set
            {
                listBox.IsShowID = value;
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
                this.Text = "";
                this.listBox.Visible = false;
                if (base.Tag != null)
                {
                    string str = objHelper.GetName(base.Tag.ToString());
                    if (!FS.FrameWork.Public.String.StringEqual(this.Text, str))
                    {
                        this.Text = str;
                    }
                }
                this.listBox.Visible = false;
            }
        }
        /// <summary>
        /// ��ǰѡ�е���Ŀ
        /// </summary>
        public FS.FrameWork.Models.NeuObject SelectedItem
        {
            get
            {
                return selectObj;
            }
            set
            {
                selectObj = value; ;
            }
        }


        public System.Drawing.Font SetListFont
        {
            get
            {
                return this.listFont;
            }
            set
            {
                this.listFont = value;
                this.listBox.Font = value;
            }
        }

        bool isSendToNext = false;

        /// <summary>
        /// �Ƿ�������Զ�������һ��
        /// </summary>
        public bool IsSendToNext
        {
            get
            {
                return isSendToNext;
            }
            set
            {
                isSendToNext = value;
                listBox.IsSendToNext = value;
            }
        }

        /// <summary>
        /// ��ǰ�������Ƿ���ʾ
        /// </summary>
        public bool ListBoxVisible
        {
            get
            {
                return this.listBox.Visible;
            }
            set
            {
                this.listBox.Visible = value;
            }
        }

        bool isSelctNone = true;

        public bool IsSelctNone
        {
            get
            {
                return isSelctNone;
            }
            set
            {
                isSelctNone = value;
                this.listBox.IsSelctNone = value;
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
            listBox.SelectNone = true;
            //parentControl = this;
            this.TextChanged += new EventHandler(NeuListTextBox_TextChanged);
            this.Enter += new EventHandler(NeuListTextBox_Enter);
            this.KeyDown += new KeyEventHandler(NeuListTextBox_KeyDown);
            this.Leave += new EventHandler(NeuListTextBox_Leave);
            //Controls.Add(listBox);
            //����
            listBox.Hide();
            //���ñ߿�
            listBox.BorderStyle = BorderStyle.FixedSingle;
            //listBox.BringToFront();
            //�����¼�
            listBox.SelectItem += new FS.HISFC.Components.HealthRecord.CaseFirstPage.PopUpListBox.MyDelegate(listBox_SelectItem);  //new FS.UFC.HealthRecord.Common.PopUpListBox.MyDelegate(ICDListBox_SelectItem);
            listBox.AutoSelectItemEvent += new FS.HISFC.Components.HealthRecord.CaseFirstPage.PopUpListBox.AutoSelectItemDele(listBox_AutoSelectItemEvent);
        }

        void listBox_AutoSelectItemEvent()
        {
            this.Focus();
            specalFlag = false;
            GetSelectItem1();
        }

        #region ������Ŀ
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

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public void Reset()
        {
            this.Tag = null;
            this.Text = "";
        }
        #endregion
        #endregion

        #region  ˽�к���

        #region �������� ���ػ�ɼ� ʱ
        void ParentForm_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                this.listBox.Visible = false;
            }
            catch { };
        }
        #endregion
        #region �뿪�ؼ�ʱ
        void NeuListTextBox_Leave(object sender, EventArgs e)
        {
            //if (!listBox.Focused)
            //{
            //    specalFlag = true;
            //    this.listBox.Visible = false;
            //    if (isFind) // ����textֵУ������, �����������ر������������
            //    {
            //        if (this.Text == null || this.Text == "")
            //        {//û������ ֱ�����
            //            this.Tag = null;
            //            return;
            //        }
            //        else
            //        {//������ ,���� 
            //            string tagID = objHelper.GetID(this.Text);
            //            if (tagID == null || tagID == "")
            //            {//û���ҵ����
            //                this.Tag = null;
            //            }
            //        }
            //    }
            //}
            
            bool temp = sendFlag;
            sendFlag = false;
            try
            {
                if (!listBox.Focused)
                {
                    specalFlag = true;
                    this.listBox.Visible = false;
                    if (this.IsShowID)
                    {
                        int rtn = listBox.GetSelectedItem(out selectObj);
                        if (selectObj == null)
                        {
                            this.Reset();
                            return;
                        }
                        if (selectObj.ID != "")
                        {
                            base.Tag = selectObj.ID;
                            this.Text = selectObj.Name;
                            if (this.IsShowID)
                            {
                                this.Text = selectObj.ID;
                                this.ItemText = selectObj.Name;
                            }
                        }
                        else
                        {
                            base.Tag = null;
                            if (isFind)
                            {
                                listBox.Text = "";
                                this.ItemText = "";
                            }
                        }
                        //listBox.Focus(); //��ý���
                        this.listBox.Visible = false;

                        return;
                    }
                    if (isFind) // ����textֵУ������, �����������ر������������
                    {
                        if (this.Text == null || this.Text == "")
                        {//û������ ֱ�����

                            this.Tag = null;
                            this.ItemText = "";
                            return;
                        }
                        else
                        {//������ ,���� 
                            string tagID = objHelper.GetID(this.Text);
                            if (tagID == null || tagID == "")
                            {//û���ҵ����
                                this.Tag = null;
                                this.Text = "";
                            }
                            else
                            {
                                this.Tag = tagID;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                sendFlag = temp;
            }

        }
        #endregion
        int listBox_SelectItem(Keys key)
        {
            //this.Focus();
            //add �ܹؼ�������ط�����ʹ���˫����ʱ��NeuListTextBox��ý��㣬���Ӳ������ڱ༭��ʱ�����������ᱻ���Ӳ������������Σ�chengym 2011-5-26
            this.Enter -= new EventHandler(NeuListTextBox_Enter);
            this.Focus();
            this.Enter += new EventHandler(NeuListTextBox_Enter);
            //end
            specalFlag = false;
            return GetSelectItem();
        }
        int GetSelectItem()
        {
            int rtn = listBox.GetSelectedItem(out selectObj);
            if (selectObj == null)
            {
                this.Reset();
                return -1;
            }
            if (selectObj.ID != "")
            {
                base.Tag = selectObj.ID;
                this.Text = selectObj.Name;
                if (this.IsShowID)
                {
                    this.Text = selectObj.ID;
                    this.ItemText = selectObj.Name;
                }
            }
            else
            {
                base.Tag = null;
                if (isFind)
                {
                    listBox.Text = "";
                    this.ItemText = "";
                }
            }
            //listBox.Focus(); //��ý���
            this.listBox.Visible = false;
            this.Focus();
            if (this.SelectItemChangEvent != null)
            {
                this.SelectItemChangEvent();
            }
            return rtn;
        }

        int GetSelectItem1()
        {
            int rtn = listBox.GetSelectedItem(out selectObj);
            if (selectObj == null)
            {
                this.Reset();
                return -1;
            }
            if (selectObj.ID != "")
            {
                base.Tag = selectObj.ID;
                this.Text = selectObj.Name;
                if (this.IsShowID)
                {
                    this.Text = selectObj.ID;
                    this.ItemText = selectObj.Name;
                }
            }
            else
            {
                base.Tag = null;
                if (isFind)
                {
                    listBox.Text = "";
                    this.ItemText = "";
                }
            }
            //listBox.Focus(); //��ý���
            this.listBox.Visible = false;
            this.Focus();
            if (this.SelectItemChangEvent != null)
            {
                this.SelectItemChangEvent();
            }

            if (this.SendToNextEvent != null)
            {
                this.SendToNextEvent();
            }
            return rtn;
        }

        /// <summary>
        /// ����ؼ�ʱ �����б��Ƿ�ɼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NeuListTextBox_Enter(object sender, EventArgs e)
        {
            AddControl();
            SetLocation();
            if (specalFlag)
            {
                //this.listBox.Visible = this.EnterVisiable;
                //this.listBox.Filter(this.Text.Trim()); 
                if (this.Text.Trim() == "")
                {
                    this.listBox.Visible = this.EnterVisiable;
                    this.listBox.Filter(this.Text.Trim());
                }

            }
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
                if (this.Focused)
                {
                    SetLocation();
                    this.listBox.Visible = true;
                    //if (Text.Trim() != "")
                    //{
                    //this.listBox.Filter(this.Text.Trim());
                    //}
                    if (this.IsSendToNext && sendFlag)
                    {
                        this.listBox.Filter1(this.Text.Trim());
                    }
                    else
                    {
                        this.listBox.Filter(this.Text.Trim());
                    }
                }
            }
            catch { }
        }
        bool sendFlag = true;
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
                sendFlag = false;
            }
            else if (e.KeyData == Keys.Down)
            {
                listBox.NextRow();
                sendFlag = false;
            }
            else if (e.KeyData == Keys.Delete)
            {
                sendFlag = false;
            }
            else if (e.KeyData == Keys.Back)
            {
                sendFlag = false;
            }
            else
            {
                sendFlag = true;
            }
        }
        /// <summary>
        /// ���ؿؼ�
        /// </summary>
        void AddControl()
        {
            if (!IsExist)
            {
                this.FindForm().VisibleChanged += new EventHandler(ParentForm_VisibleChanged);
                this.FindForm().SizeChanged += new EventHandler(ParentForm_SizeChanged);
            }
            LocaltionX = 0;
            LocaltionY = 0;
            parentControl = GetParent(this);
            parentControl.Controls.Add(listBox);
            IsExist = true;
            Form f = this.FindForm().ParentForm;
            if (f != null)
            {
                if (f.IsMdiContainer)
                {
                    this.LocaltionX += System.Math.Abs(f.Location.X);
                    this.LocaltionY += 32; //�˵��Ŀ��
                }
            }
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

            if (this.parentControl.Height < LocaltionY + listBox.Height + this.Height)
            {
                if (parentControl.Height - listBox.Height - this.Height > 0)
                {
                    LocaltionY = LocaltionY - listBox.Height - this.Height - 2;
                }
            }
            //}
            listBox.BringToFront();
        }

        void ParentForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.listBox.Visible = false;
            }
            catch { };
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
