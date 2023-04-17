using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace UFC.Pharmacy.Controls
{
    /// <summary>
    /// BedWeave<br></br>
    /// [��������: ҩƷFp����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-14]<br></br>
    /// [����˵��: �ɶԲ�ͬ���趨�����б� ���趨��ʾҩƷ��Ŀ
    ///            ĿǰҩƷ�б���δ����]
    /// <br></br>
    /// 
    /// </summary>
    public partial class FpItem : Neusoft.NFC.Interface.Controls.NeuSpread
    {
        public FpItem()
        {
            InitializeComponent();
        }

        public FpItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// ��Ŀѡ�񴥷�
        /// </summary>
        public event System.EventHandler SelectItem;

        /// <summary>
        /// ����
        /// </summary>
        public event System.EventHandler KeyEvent;

        #region �б����

        /// <summary>
        /// �����б���
        /// </summary>
        private ArrayList Lists = null;

        /// <summary>
        /// �Ƿ�ʹ�� ���б�
        /// </summary>
        private bool listBoxEnabled = false;

        /// <summary>
        /// �����б���������
        /// </summary>
        private ArrayList sheetList;

        /// <summary>
        /// �б���
        /// </summary>
        private int intWidth = 150;

        /// <summary>
        /// �б�߶�
        /// </summary>
        private int intHeight = 200;

        /// <summary>
        /// �赯���б�������
        /// </summary>
        private int[] listRows = null;

        /// <summary>
        /// �б���
        /// </summary>
        public int ListWidth
        {
            set
            {
                this.intWidth = value;
            }
        }

        /// <summary>
        /// �б�߶�
        /// </summary>
        public int ListHeight
        {
            set
            {
                this.intHeight = value;
            }
        }

        /// <summary>
        /// �赯���б�������
        /// </summary>
        public int[] ListRows
        {
            set
            {
                this.listRows = value;
            }
        }

        #endregion

        #region ҩƷ�б����

        /// <summary>
        /// ����ҩƷ�б�������
        /// </summary>
        private int phaListColumnIndex = 0;

        /// <summary>
        /// �Ƿ�ʹ��ҩƷ�б�
        /// </summary>
        private bool phaListEnabled = true;

        /// <summary>
        /// ����ҩƷ�б�������
        /// </summary>
        public int PhaListColumnIndex
        {
            set
            {
                this.phaListColumnIndex = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��ҩƷ�б�
        /// </summary>
        public bool PhaListEnabled
        {
            set
            {
                this.phaListEnabled = value;
            }
        }

        #endregion

        #region �б��ʼ��

        /// <summary>
        /// ���� �ڵ�ǰ�SheetView�� �������б���ʾ
        /// </summary>
        /// <param name="sheetView">����ʾ��SheetView</param>
        /// <param name="al">�б�����</param>
        /// <param name="iColumnIndex">����ʾ���б����</param>
        public void SetColumnList(FarPoint.Win.Spread.SheetView sheetView, ArrayList al, params int[] iColumnIndex)
        {
            if (this.Lists == null)
                this.Lists = new ArrayList();

            int iListIndex = this.Lists.Count;

            this.listBoxEnabled = true;

            InputMap im;

            im = base.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);


            Neusoft.NFC.Interface.Controls.NeuListBox obj = new Neusoft.NFC.Interface.Controls.NeuListBox();
            obj.AddItems(al);
            obj.SelectItem += new neusoft.neuFC.Interface.Controls.ListBox.MyDelegate(obj_SelectItem);
            this.Controls.Add(obj);
            obj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            obj.Size = new System.Drawing.Size(this.intWidth, this.intHeight);
            obj.Visible = false;

            this.Lists.Add(obj);

            string str = "";
            foreach (int i in iColumnIndex)
            {
                str = string.Format("{0}|{1}{2}", iListIndex.ToString(), sheetView.SheetName, i.ToString());
                if (this.sheetList == null)
                    this.sheetList = new ArrayList();
                this.sheetList.Add(str);
            }
        }

        /// <summary>
        /// ҩƷ�б��ʼ��
        /// </summary>
        public void Init()
        {
            if (this.ucPhaItem1 == null)
                this.ucPhaItem1 = new ucItemList();

            this.ucPhaItem1.Init();
            this.ucPhaItem1.SelectItem += new EventHandler(ucPhaItem1_SelectItem);

            this.Controls.Add(this.ucPhaItem1);

            this.ucPhaItem1.BringToFront();
            this.ucPhaItem1.Visible = false;
        }

        #endregion

        #region �б�λ���趨
        /// <summary>
        /// ҩƷ�����б�λ���趨
        /// </summary>
        protected void SetListLocation()
        {
            Control _cell = base.EditingControl;
            if (_cell == null)
                return;
            if (this.ActiveSheet.ActiveColumnIndex == this.phaListColumnIndex)
            {
                int y = _cell.Top + _cell.Height + this.ucPhaItem1.Height + 5;
                if (y <= this.Height)
                    this.ucPhaItem1.Location = new Point(_cell.Left + 20, y - this.ucPhaItem1.Height);
                else
                    this.ucPhaItem1.Location = new Point(_cell.Left + 20, _cell.Top - this.ucPhaItem1.Height - 5);
            }
            return;
        }

        /// <summary>
        /// ���ÿؼ�λ��
        /// </summary>
        /// <param name="obj">�����б�ؼ�</param>
        private void SetListLocation(neusoft.neuFC.Interface.Controls.ListBox obj)
        {
            Control _cell = base.EditingControl;
            if (_cell == null) return;
            int y = _cell.Top + _cell.Height + obj.Height;
            if (y <= this.Height)
                obj.Location = new System.Drawing.Point(_cell.Left, y - obj.Height);
            else
            {
                if (_cell.Top > obj.Height)
                    obj.Location = new System.Drawing.Point(_cell.Left, _cell.Top - obj.Height);
                else
                    obj.Location = new System.Drawing.Point(_cell.Left, _cell.Top + _cell.Height);
            }
        }

        #endregion

        #region �б����
        /// <summary>
        /// �����ַ�����ȡ��Ӧ�б�����
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>�ɹ����ض�Ӧ�б����� ʧ�ܷ���-1</returns>
        private int GetListIndex(string str)
        {
            string strTemp = "";
            foreach (string info in this.sheetList)
            {
                strTemp = info.Substring(info.IndexOf("|") + 1);
                if (strTemp == str)
                {
                    strTemp = info.Substring(0, info.IndexOf("|"));
                    return NConvert.ToInt32(strTemp);
                }
            }
            return -1;
        }
        /// <summary>
        /// ���ɼ�
        /// </summary>
        public void VisibleAllList()
        {
            for (int i = 0; i < this.Lists.Count; i++)
            {
                if (this.Lists[i] != null)
                {
                    (this.Lists[i] as neusoft.neuFC.Interface.Controls.ListBox).Visible = false;
                }
            }
        }
        /// <summary>
        /// �ж��Ƿ���ָ�����������б�
        /// </summary>
        /// <param name="iRow">ָ��������</param>
        /// <returns>������True ���򷵻�False</returns>
        private bool JudgeListRow(int iRow)
        {
            if (this.Lists == null || !this.listBoxEnabled)
                return false;
            if (this.listRows == null)
                return true;
            foreach (int i in this.listRows)
            {
                if (i == iRow)
                    return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// ����ת
        /// </summary>
        /// <param name="newColumnIndex">����תĿ����</param>
        /// <param name="newRow">�Ƿ���ת����</param>
        public void JumpColumn(int newColumnIndex, bool newRow)
        {
            if (this.ucPhaItem1.Visible)
                return;
            if (newRow)
            {
                if (this.ActiveSheet.ActiveRowIndex == this.ActiveSheet.Rows.Count - 1)
                    this.ActiveSheet.AddRows(this.ActiveSheet.Rows.Count, 1);
                this.ActiveSheet.ActiveRowIndex++;
            }

            if (newColumnIndex > this.ActiveSheet.Columns.Count - 1)
                newColumnIndex = this.ActiveSheet.Columns.Count - 1;
            this.ActiveSheet.ActiveColumnIndex = newColumnIndex;
        }

        /// <summary>
        /// �������б��ȡ��ѡ����Ŀ
        /// </summary>
        private neusoft.neuFC.Object.neuObject GetItemFormList()
        {
            neusoft.neuFC.Object.neuObject obj = new neusoft.neuFC.Object.neuObject();

            string str = this.ActiveSheet.SheetName + this.ActiveSheet.ActiveColumnIndex.ToString();
            int iIndex = this.GetListIndex(str);
            if (iIndex == -1 || iIndex >= this.Lists.Count)
                return null;
            neusoft.neuFC.Interface.Controls.ListBox current = null;
            current = this.Lists[iIndex] as neusoft.neuFC.Interface.Controls.ListBox;
            if (current != null && current.Visible)
            {
                if (current.GetSelectedItem(out obj) == -1)
                    return null;
                current.Visible = false;
                return obj;
            }
            return null;
        }


        private void ucPhaItem1_SelectItem(object sender, EventArgs e)
        {
            if (this.SelectItem != null)
            {
                this.SelectItem(sender, e);
            }
        }


        protected override void OnEditChange(FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.phaListEnabled && this.ucPhaItem1 != null)
            {
                if (e.Column == this.phaListColumnIndex && this.ucPhaItem1 != null)
                {
                    this.ucPhaItem1.BringToFront();
                    if (!this.ucPhaItem1.Visible)
                        this.ucPhaItem1.Visible = true;

                    this.ucPhaItem1.Filter(this.ActiveSheet.ActiveCell.Text);
                }
            }
            if (this.listBoxEnabled && this.Lists != null && this.Lists.Count > 0)
            {
                if (this.JudgeListRow(e.Row))
                {
                    string str = this.ActiveSheet.SheetName + e.Column.ToString();
                    int iIndex = this.GetListIndex(str);
                    if (iIndex != -1)
                    {
                        neusoft.neuFC.Interface.Controls.ListBox current = null;
                        current = this.Lists[iIndex] as neusoft.neuFC.Interface.Controls.ListBox;
                        if (current != null)
                        {
                            current.Filter(e.EditingControl.Text.Trim());
                            if (current.Visible == false)
                                current.Visible = true;
                        }
                    }
                }
            }
            base.OnEditChange(e);
        }
        protected override void OnEditModeOn(EventArgs e)
        {
            if (this.phaListEnabled && this.ucPhaItem1 != null && this.ActiveSheet.ActiveRowIndex == this.phaListColumnIndex)
            {
                this.SetListLocation();

                if (this.ActiveSheet.ActiveColumnIndex != this.phaListColumnIndex)
                    this.ucPhaItem1.Visible = false;
            }
            if (this.listBoxEnabled && this.Lists != null && this.Lists.Count > 0)
            {
                this.VisibleAllList();

                if (this.JudgeListRow(this.ActiveSheet.ActiveRowIndex))
                {
                    if (this.ActiveSheet.ActiveColumn.Visible)
                    {
                        #region ��ǰ��Ϊ��ʾ״̬ʱ �Ž��д���
                        string str = this.ActiveSheet.SheetName + this.ActiveSheet.ActiveColumnIndex.ToString();
                        int iIndex = this.GetListIndex(str);
                        if (iIndex != -1)
                        {
                            neusoft.neuFC.Interface.Controls.ListBox current = null;
                            current = this.Lists[iIndex] as neusoft.neuFC.Interface.Controls.ListBox;
                            if (current != null)
                            {
                                //����λ��
                                this.SetListLocation(current);
                                if (current.Visible == false)
                                {
                                    current.Visible = true;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            base.OnEditModeOn(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.phaListEnabled && this.ucPhaItem1 != null && this.ucPhaItem1.Visible && this.ContainsFocus && this.ActiveSheet.ActiveColumnIndex == this.phaListColumnIndex)
            {
                #region ����ҩƷ�б�
                if (keyData == Keys.Enter || keyData == Keys.Escape)
                {
                    this.ucPhaItem1.Key(keyData);
                }
                else if (keyData == Keys.Up || keyData == Keys.Down)
                {
                    if (this.ucPhaItem1.Visible)
                    {
                        this.ucPhaItem1.Key(keyData);
                        return true;
                    }
                }
                #endregion
            }
            else if (this.listBoxEnabled && this.Lists != null && this.Lists.Count > 0)
            {
                #region �����б�
                if (keyData == Keys.Up || keyData == Keys.Down)
                {
                    #region ���¼�
                    string str = this.ActiveSheet.SheetName + this.ActiveSheet.ActiveColumnIndex.ToString();
                    int iIndex = this.GetListIndex(str);
                    neusoft.neuFC.Interface.Controls.ListBox current = null;
                    if (iIndex != -1)
                        current = this.Lists[iIndex] as neusoft.neuFC.Interface.Controls.ListBox;
                    if (current != null && current.Visible)
                    {
                        if (keyData == Keys.Up)
                            current.PriorRow();
                        else
                            current.NextRow();
                    }
                    else
                    {
                        if (keyData == Keys.Up)
                            this.ActiveSheet.ActiveRowIndex--;
                        else
                            this.ActiveSheet.ActiveRowIndex++;
                    }
                    #endregion
                }
                else if (keyData == Keys.Enter)
                {
                    this.obj_SelectItem(keyData);
                }
                #endregion
            }
            if (this.KeyEvent != null)
                this.KeyEvent(keyData, System.EventArgs.Empty);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnComboDropDown(EditorNotifyEventArgs e)
        {
            if (e.EditingControl == null)
                return;

            base.OnComboDropDown(e);
        }

        private int obj_SelectItem(Keys key)
        {
            neusoft.neuFC.Object.neuObject obj = this.GetItemFormList();
            if (obj != null)
            {
                if (this.SelectItem != null)
                    this.SelectItem(obj, System.EventArgs.Empty);
            }
            return 0;
        }

    }
}
