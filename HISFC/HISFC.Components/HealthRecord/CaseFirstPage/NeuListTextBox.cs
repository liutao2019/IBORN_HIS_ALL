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
    /// [功能描述: 带下拉列表的文本输入框]<br></br>
    /// [创 建 者: 张俊义]<br></br>
    /// [创建时间: 2007-04-5]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class NeuListTextBox : System.Windows.Forms.TextBox
    {
        #region  私有变量
        //public FS.UFC.HealthRecord.Common.PopUpListBox listBox = new FS.UFC.HealthRecord.Common.PopUpListBox();
        public FS.HISFC.Components.HealthRecord.CaseFirstPage.PopUpListBox listBox = new PopUpListBox();
  
        bool keyEnterVisiable = false;//进入控件时下拉列表即可见
        FS.FrameWork.Models.NeuObject selectObj; //当前选中的项目
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();
        bool IsExist = false; //是否已经加载
        private System.Windows.Forms.Control parentControl; //父级控件
        int LocaltionX; //位置X
        int LocaltionY;//位置Y
        bool isFind = false;//根据名称找不到编码时清空数据
        private bool specalFlag = true; //单击选择项目后可见不可见的处理

        private System.Drawing.Font listFont = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

        public delegate void ChangObjName(string name);

        public event ChangObjName ChangObjNameEvent;

        public delegate void SelectItemChangDele();

        public event SelectItemChangDele SelectItemChangEvent;

        public delegate void SendToNextDele();

        public event SendToNextDele SendToNextEvent;

        /// <summary>
        /// 在文本框中显示ID
        /// </summary>
        private bool isShowID = false;

        [Description("在文本框中显示ID")]
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

        #region 属性
        [Description("进入控件时下拉列表即可见")]
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
        [Description("下拉筐的宽度")]
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
        [Description("下拉筐的宽度")]
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
        [Description("模糊查询")]
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
        [Description("是否默认不选中任何项")]
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
        [Description("根据名称找不到编码时清空数据")]
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
        [Description("是否显示列表的 ID")]
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
        /// 设置Tag
        /// </summary>
        [Description("设置Tag")]
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
        /// 当前选中的项目
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
        /// 是否输入后自动跳到下一个
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
        /// 当前下拉框是否显示
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

        #region 共有有函数
        /// <summary>
        /// 筛选事件
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
            //隐藏
            listBox.Hide();
            //设置边框
            listBox.BorderStyle = BorderStyle.FixedSingle;
            //listBox.BringToFront();
            //单击事件
            listBox.SelectItem += new FS.HISFC.Components.HealthRecord.CaseFirstPage.PopUpListBox.MyDelegate(listBox_SelectItem);  //new FS.UFC.HealthRecord.Common.PopUpListBox.MyDelegate(ICDListBox_SelectItem);
            listBox.AutoSelectItemEvent += new FS.HISFC.Components.HealthRecord.CaseFirstPage.PopUpListBox.AutoSelectItemDele(listBox_AutoSelectItemEvent);
        }

        void listBox_AutoSelectItemEvent()
        {
            this.Focus();
            specalFlag = false;
            GetSelectItem1();
        }

        #region 加载项目
        /// <summary>
        /// 加载项目
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

        #region 清空数据
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        public void Reset()
        {
            this.Tag = null;
            this.Text = "";
        }
        #endregion
        #endregion

        #region  私有函数

        #region 父级窗口 隐藏或可见 时
        void ParentForm_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                this.listBox.Visible = false;
            }
            catch { };
        }
        #endregion
        #region 离开控件时
        void NeuListTextBox_Leave(object sender, EventArgs e)
        {
            //if (!listBox.Focused)
            //{
            //    specalFlag = true;
            //    this.listBox.Visible = false;
            //    if (isFind) // 根据text值校验数据, 如果不存在相关编码则清空数据
            //    {
            //        if (this.Text == null || this.Text == "")
            //        {//没有数据 直接清空
            //            this.Tag = null;
            //            return;
            //        }
            //        else
            //        {//有数据 ,查找 
            //            string tagID = objHelper.GetID(this.Text);
            //            if (tagID == null || tagID == "")
            //            {//没有找到清空
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
                        //listBox.Focus(); //获得焦点
                        this.listBox.Visible = false;

                        return;
                    }
                    if (isFind) // 根据text值校验数据, 如果不存在相关编码则清空数据
                    {
                        if (this.Text == null || this.Text == "")
                        {//没有数据 直接清空

                            this.Tag = null;
                            this.ItemText = "";
                            return;
                        }
                        else
                        {//有数据 ,查找 
                            string tagID = objHelper.GetID(this.Text);
                            if (tagID == null || tagID == "")
                            {//没有找到清空
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
            //add 很关键：这个地方可以使鼠标双击的时候NeuListTextBox获得焦点，电子病历窗口编辑的时：鼠标操作不会被电子病历主界面屏蔽；chengym 2011-5-26
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
            //listBox.Focus(); //获得焦点
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
            //listBox.Focus(); //获得焦点
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
        /// 进入控件时 下拉列表是否可见
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
        /// 筛选数据
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
        /// 按键
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
        /// 加载控件
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
                    this.LocaltionY += 32; //菜单的宽度
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
        /// 获取父级控件
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
        /// 设置位置
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
