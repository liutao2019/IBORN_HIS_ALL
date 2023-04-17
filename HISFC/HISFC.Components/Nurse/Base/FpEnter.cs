using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Nurse.Base
{
    public partial class FpEnter : FarPoint.Win.Spread.FpSpread
    {
        public FpEnter()
        {
            InitializeComponent();
            this.Init();
        }

        public FpEnter(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            this.Init();
        }

        #region �¼�����
        //		/// <summary>
        //		/// ��ǰView
        //		/// </summary>
        //		public FarPoint.Win.Spread.SheetView SheetView=new SheetView();
        /// <summary>
        /// ��Ӧ�����¼�
        /// </summary>
        public delegate int keyDown(Keys key);
        /// <summary>
        /// ��������б�ѡ����
        /// </summary>
        public delegate int setItem(FS.FrameWork.Models.NeuObject obj);
        /// <summary>
        /// �����¼�:Enter,Up,Down,Escape������...
        /// </summary>
        public event keyDown KeyEnter;
        /// <summary>
        /// ѡ�������б���Ŀ
        /// </summary>
        public event setItem SetItem;
        #endregion

        #region ˽�б���

        /// <summary>
        /// ��� 
        /// </summary>
        private int intWidth = 150;

        /// <summary>
        /// ����
        /// </summary>
        private int intHeight = 200;

        /// <summary>
        /// �趨�����б�Ĭ�ϲ�ѡ���κ���
        /// </summary>
        private bool selectNone = false;

        /// <summary>
        /// ��cell�õ�����ʱ,�Ƿ���ʾ�����б�
        /// </summary>
        private bool showListWhenOfFocus = false;

        #endregion

        #region ˽�к���


        private void FpEnter_ItemSelected(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = this.GetCurrentList(this.ActiveSheet, this.ActiveSheet.ActiveColumnIndex);

            if (current == null) return ;
            obj = current.GetSelectedItem();

            if (obj == null)
            {
                return ;
            }

            if (this.SetItem != null)
                this.SetItem(obj);

            current.Visible = false;

            return ;
        }

        /// <summary>
        /// ѡ����Ŀ�б�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int FpEnter_SelectItem(Keys key)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = this.GetCurrentList(this.ActiveSheet, this.ActiveSheet.ActiveColumnIndex);

            if (current == null) return -1;
            obj = current.GetSelectedItem();

            if (obj == null)
            {
                return -1;
            }

            if (this.SetItem != null)
                this.SetItem(obj);

            current.Visible = false;

            return 0;
        }

        /// <summary>
        /// ��������Ŀ�б�ģ��Զ����й���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpEnter_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = this.GetCurrentList(this.ActiveSheet,
                    this.ActiveSheet.ActiveColumnIndex);

                if (current == null) return;

                string Text = e.EditingControl.Text.Trim();

                current.Filter(Text);

                this.ActiveSheet.SetTag(this.ActiveSheet.ActiveRowIndex, this.ActiveSheet.ActiveColumnIndex, null);

                if (current.Visible == false) current.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// ���ÿؼ�λ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpEnter_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                this.noVisible();

                FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = this.GetCurrentList(this.ActiveSheet,
                    this.ActiveSheet.ActiveColumnIndex);

                if (current == null) return;

                //����λ��
                this.setLocal(current);

                if (this.showListWhenOfFocus && current.Visible == false)
                {
                    current.Filter(this.ActiveSheet.ActiveCell.Text);
                    current.Visible = true;
                }
            }
            catch { }
        }

        /// <summary>
        /// ���ÿؼ�λ��
        /// </summary>
        /// <param name="obj"></param>
        private void setLocal(FS.FrameWork.WinForms.Controls.NeuListBoxPopup obj)
        {
            Control _cell = base.EditingControl;
            if (_cell == null) return;

            int y = _cell.Top + _cell.Height + obj.Height;//+SystemInformation.Border3DSize.Height*2;
            if (y <= this.Height)
                obj.Location = new System.Drawing.Point(_cell.Left, y - obj.Height);
            else
                obj.Location = new System.Drawing.Point(_cell.Left, _cell.Top - obj.Height);

        }

        /// <summary>
        /// ���ɼ�
        /// </summary>
        private void noVisible()
        {
            for (int i = 0; i < this.Lists.Length; i++)
            {
                if (this.Lists[i] != null)
                {
                    this.Lists[i].Visible = false;
                }
            }
        }

        #endregion

        #region ������Ա

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            this.InitFp();

            //this.Sheets.Add(SheetView);
            this.EditChange += new EditorNotifyEventHandler(FpEnter_EditChange);
            this.EditModeOn += new EventHandler(FpEnter_EditModeOn);
        }

        /// <summary>
        /// ��ʼ��Fp,�����ض�������Ĭ���¼�
        /// </summary>
        protected void InitFp()
        {
            InputMap im;

            im = base.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = base.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = base.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = base.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //ʼ�մ��ڿɱ༭״̬
            base.EditModePermanent = true;
            base.EditModeReplace = true;
        }

        /// <summary>
        /// ��Ӧ�����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.KeyEnter != null)
                        this.KeyEnter(Keys.Enter);
                }
                else if (keyData == Keys.Up)
                {
                    FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = this.GetCurrentList(this.ActiveSheet, this.ActiveSheet.ActiveColumnIndex);
                    
                    if (current != null && current.Visible)
                        current.PriorRow();
                    else
                    {
                        if (this.ActiveSheet.ActiveRowIndex > 0)
                            this.ActiveSheet.ActiveRowIndex--;
                    }

                    if (this.KeyEnter != null)
                        this.KeyEnter(Keys.Up);
                }
                else if (keyData == Keys.Down)
                {
                    FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = this.GetCurrentList(this.ActiveSheet, this.ActiveSheet.ActiveColumnIndex);
                    if (current != null && current.Visible)
                        current.NextRow();
                    else
                    {
                        if (this.ActiveSheet.ActiveRowIndex < this.ActiveSheet.RowCount - 1)
                            this.ActiveSheet.ActiveRowIndex++;
                    }

                    if (this.KeyEnter != null)
                        this.KeyEnter(Keys.Down);
                }
                else if (keyData == Keys.Escape)
                {
                    this.noVisible();

                    if (this.KeyEnter != null)
                        this.KeyEnter(Keys.Escape);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region  ���õ����������Ϊ "" ʱ ,Ĭ�ϲ�ѡ���κ���
        public bool SelectNone
        {
            get
            {
                return selectNone;
            }
            set
            {
                selectNone = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// �����б���
        /// </summary>
        public FS.FrameWork.WinForms.Controls.NeuListBoxPopup[] Lists = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup[10];

        /// <summary>
        /// ��cell�õ�����ʱ,�Ƿ���ʾ�����б�
        /// </summary>		
        public bool ShowListWhenOfFocus
        {
            get { return this.showListWhenOfFocus; }
            set { this.showListWhenOfFocus = value; }
        }

        #endregion

        /// <summary>
        /// �趨���е������б����ɼ� 
        /// </summary>
        /// <returns></returns>
        public int SetAllListBoxUnvisible()
        {
            try
            {
                if (Lists != null)
                {
                    foreach (FS.FrameWork.WinForms.Controls.NeuListBox currentList in Lists)
                    {
                        if (currentList != null)
                        {
                            currentList.Visible = false;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// �趨��һ����ʾ/����ʾID ���� �ɹ����� 1 ʧ�ܷ��� 0 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="IsVisiable"></param>
        /// <returns></returns>
        public int SetIDVisiable(FarPoint.Win.Spread.SheetView view, int col, bool IsVisiable)
        {
            //string name = view.SheetName + "_" + col.ToString();
            //for (int i = 0; i < this.Lists.Length; i++)
            //{
            //    if (this.Lists[i] != null && (this.Lists[i] as FS.FrameWork.WinForms.Controls.NeuListBoxPopup).Name == name)
            //    {
            //        Lists[i].IsShowID = IsVisiable;
            //        return 1;
            //    }
            //}
            return 0;

        }

        /// <summary>
        /// ���������б�Ŀ�Ⱥ͸߶� 
        /// </summary>
        public void SetWidthAndHeight(int width, int height)
        {
            intWidth = width;
            intHeight = height;
        }

        /// <summary>
        /// ����cell�����б�
        /// </summary>
        /// <param name="view"></param>
        /// <param name="col"></param>
        /// <param name="al"></param>
        public void SetColumnList(FarPoint.Win.Spread.SheetView view, int col, ArrayList al)
        {
            string name = view.SheetName + "_" + col.ToString();

            for (int i = 0; i < this.Lists.Length - 1; i++)
            {
                if (this.Lists[i] != null && (this.Lists[i] as FS.FrameWork.WinForms.Controls.NeuListBoxPopup).Name == name)
                {
                    return;
                }
            }

            FS.FrameWork.WinForms.Controls.NeuListBoxPopup obj = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
            obj.Name = name;
            obj.AddItems(al);
            //�õ�����б���
            int Index = -1;

            for (int i = 0; i < this.Lists.Length; i++)
            {
                if (this.Lists[i] == null)
                {
                    Index = i;
                    break;
                }
            }
            if (Index == -1)
            {
                MessageBox.Show("�б��Ѿ����������10", "��ʾ");
                return;
            }

            this.Lists[Index] = obj;
            this.Lists[Index].ItemSelected += new EventHandler(FpEnter_ItemSelected);
            //this.Lists[Index].ItemSelected += new FS.FrameWork.WinForms.Controls.NeuListBoxPopup(FpEnter_SelectItem);
            this.Controls.Add(this.Lists[Index]);
            this.Lists[Index].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lists[Index].Cursor = Cursors.Hand;
            this.Lists[Index].Size = new System.Drawing.Size(intWidth, intHeight);
            this.Lists[Index].Visible = false;
            //this.Lists[Index].SelectNone = selectNone;
        }

        /// <summary>
        /// ��ȡ��ǰcell�Ƿ��������б�
        /// </summary>
        /// <param name="view"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public FS.FrameWork.WinForms.Controls.NeuListBoxPopup GetCurrentList(FarPoint.Win.Spread.SheetView view, int col)
        {
            string name = view.SheetName + "_" + col.ToString();
            for (int i = 0; i < this.Lists.Length; i++)
            {
                if (this.Lists[i] != null && (this.Lists[i] as FS.FrameWork.WinForms.Controls.NeuListBox).Name == name)
                    return this.Lists[i];
            }
            return null;
        }

    }
}
